Imports System.Windows.Forms
Imports System.IO
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration
'Imports Microsoft.Win32
Imports System.Threading
Imports System.Net
Imports System.Net.Sockets
Imports System.Net.Sockets.Socket
Imports VolHedge.DAL
Imports System.Net.NetworkInformation
Imports System.Management
Imports System.Net.Sockets.NetworkStream
Imports System.Data.OleDb
Imports VolHedge.OptionG
Imports System.Runtime.InteropServices

Public Class Ref(Of T)
    Public Value As T
    Public Sub New(val As T)
        Value = val
    End Sub
End Class

Public Class CSettingRecord
    Public mKey As String
    Public mValue As String

    Public Sub New(pKey As String, pVal As String)
        mKey = pKey
        mValue = pVal
    End Sub
End Class

Public Class CSetting
    Public mDtSettings As DataTable
    Public mListofDefaultSettings As List(Of CSettingRecord)

    Sub New(ByRef pDt As DataTable)
        mDtSettings = pDt
        mListofDefaultSettings = New List(Of CSettingRecord)
        CheckAndDefaultRecords()
    End Sub

    Sub AddDefaultRecords()
        mListofDefaultSettings.Add(New CSettingRecord(CSetting.mKeyCompactMdbAuto, "0"))
        mListofDefaultSettings.Add(New CSettingRecord(CSetting.mKeyCompactMdbDays, "15"))
    End Sub

    Sub CheckAndDefaultRecords()
        AddDefaultRecords()
        Dim rec As CSettingRecord
        Dim dtAll As DataTable = Select_All_Settings()
        For index = 0 To mListofDefaultSettings.Count - 1
            rec = mListofDefaultSettings(index)
            If Not mIsExistRecord(rec.mKey) Then
                Set_Value(rec.mKey, rec.mValue)
            End If
        Next
    End Sub

    Public Property Uid() As Integer
    Private Const sp_insert_setting As String = "insert_setting"
    Private Const sp_Select_All As String = "select_settings"

#Region "SETTING NAMES"

    Public Shared ReadOnly mKeyCDate As String = "CDATE"
    Public Shared ReadOnly mCompactDateFormat As String = "dd-MM-yyy"
    Public Shared ReadOnly mKeyCompactMdbDate As String = "COMPACTEDDATE"
    Public Shared ReadOnly mKeyCompactMdbAuto As String = "COMPACT_AUTO"
    Public Shared ReadOnly mKeyCompactMdbDays As String = "COMPACT_MDB_CHECK_DAYS"

#End Region

    Public Function Select_All_Settings() As DataTable
        Try
            data_access.ParamClear()
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.Cmd_Text = sp_Select_All
            Return data_access.FillList()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub Update_setting(ByVal SettingName As String, ByVal pVal As String)
        Try
            Dim qry As String
            qry = "UPDATE settings SET SettingKey ='" + pVal + "' WHERE SettingName='" + SettingName + "';"
            data_access.ParamClear()
            data_access.ExecuteNonQuery(qry, CommandType.Text)

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Insert_setting(SettingName As String, pVal As String)
        data_access.ParamClear()
        data_access.AddParam("@SettingName", OleDbType.VarChar, 250, SettingName)
        data_access.AddParam("@SettingKey", OleDbType.VarChar, 3000, pVal)
        data_access.Cmd_Text = sp_insert_setting
        data_access.ExecuteNonQuery()
    End Sub
    Public Function Get_Value(pSettingName As String) As String
        Dim str As String = GdtSettings.Compute("max(SettingKey)", "SettingName='" & pSettingName & "'").ToString()
        If str Is Nothing Then
            Return ""
        Else
            Return str
        End If
    End Function

    Public Function GetValueInt(pSettingName As String) As Int32
        Dim str As String = Get_Value(pSettingName)
        Return CUtils.StringToInt(str)
    End Function

    Public Function mIsExistRecord(pSettingName As String) As Boolean
        Dim result() As DataRow
        GdtSettings.Select()
        result = mDtSettings.Select("SettingName = '" & pSettingName & "'")
        If result.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub Set_Value(pSettingName As String, pValue As String)
        Dim result() As DataRow
        GdtSettings.Select()
        result = mDtSettings.Select("SettingName = '" & pSettingName & "'")
        If result.Length > 0 Then
            Update_setting(pSettingName, pValue)
            UpdateDtSetting(pSettingName, pValue)
            'GdtSettings = Select_All_Settings()
            mDtSettings = GdtSettings
        Else
            Insert_setting(pSettingName, pValue)
            InsertDtSetting(pSettingName, pValue)
            'GdtSettings = Select_All_Settings()
            mDtSettings = GdtSettings
        End If
    End Sub

    'Public Sub Set_Value(pSettingName As String, pValue As String)
    '    Dim result() As DataRow
    '    result = mDtSettings.Select("SettingName = '" & pSettingName & "'")
    '    If result.Length > 0 Then
    '        Update_setting(pSettingName, pValue)
    '    Else
    '        Insert_setting(pSettingName, pValue)
    '    End If
    'End Sub

    Public Sub UpdSett()
        GdtSettings = Select_All_Settings()
        mDtSettings = GdtSettings
    End Sub

    Private Function GetNextUid() As Integer
        If GdtSettings.Rows.Count = 0 Then
            Return 1
        Else
            Return CInt(GdtSettings.Compute("MAX(uid)", "")) + 1
        End If
    End Function

    Public Sub InsertDtSetting(settingName As String, pVal As String)
        Dim newRow As DataRow = GdtSettings.NewRow()
        newRow("uid") = GetNextUid()
        newRow("settingname") = settingName
        newRow("settingkey") = pVal
        GdtSettings.Rows.Add(newRow)
    End Sub

    Private Function UpdateDtSetting(settingName As String, pVal As String) As Boolean
        Dim rows() As DataRow = GdtSettings.Select("settingname = '" & settingName.Replace("'", "''") & "'")
        If rows.Length > 0 Then
            rows(0)("settingkey") = pVal
            Return True
        End If
        Return False
    End Function


    Public Function GetValueBoolen(pSetKey As String) As Boolean
        If Get_Value(pSetKey) <> "1" Then
            Return False
        Else
            Return True
        End If
    End Function

End Class
