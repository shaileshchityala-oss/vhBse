Imports VolHedge.DAL
Imports System.Data.SqlClient
Public Class SqlDbProcess
    Dim q_AppendTokens As New Queue(Of String)
    Dim Thr_RegunRegTokens As Threading.Thread

    
    Public Function Getdatatable(ByVal Sql As String) As DataTable
        Return DA_SQL.FillDatatable(Sql)
    End Function


    'Public Sub AppendFoToken(ByVal token As Long)
    '    DA_SQL.ParamClear()
    '    DA_SQL.AddParam("@Instance", SqlDbType.VarChar, 17, gVarInstanceID)
    '    DA_SQL.AddParam("@token_no", SqlDbType.BigInt, 12, token)
    '    DA_SQL.Cmd_Text = "dbo.Sp_InsertFoToken "
    '    DA_SQL.ExecuteNonQuery()
    'End Sub

    'Public Sub AppendEQToken(ByVal token As Long)
    '    DA_SQL.ParamClear()
    '    DA_SQL.AddParam("@Instance", SqlDbType.VarChar, 17, gVarInstanceID)
    '    DA_SQL.AddParam("@token_no", SqlDbType.BigInt, 12, token)
    '    DA_SQL.Cmd_Text = "dbo.Sp_InsertEqToken "
    '    DA_SQL.ExecuteNonQuery()
    'End Sub

    'Public Sub AppendCurToken(ByVal token As Long)
    '    DA_SQL.ParamClear()
    '    DA_SQL.AddParam("@Instance", SqlDbType.VarChar, 17, gVarInstanceID)
    '    DA_SQL.AddParam("@token_no", SqlDbType.BigInt, 12, token)
    '    DA_SQL.Cmd_Text = "dbo.Sp_InsertCurToken "
    '    DA_SQL.ExecuteNonQuery()
    'End Sub

    'Public Sub DeleteFoToken(Optional ByVal token As Long = 0)
    '    DA_SQL.ParamClear()
    '    DA_SQL.AddParam("@Instance", SqlDbType.VarChar, 17, gVarInstanceID)
    '    DA_SQL.AddParam("@token_no", SqlDbType.BigInt, 12, token)
    '    DA_SQL.Cmd_Text = "dbo.Sp_DeleteFoToken "
    '    DA_SQL.ExecuteNonQuery()
    'End Sub
    'Public Sub DeleteEqToken(Optional ByVal token As Long = 0)
    '    DA_SQL.ParamClear()
    '    DA_SQL.AddParam("@Instance", SqlDbType.VarChar, 17, gVarInstanceID)
    '    DA_SQL.AddParam("@token_no", SqlDbType.BigInt, 12, token)
    '    DA_SQL.Cmd_Text = "dbo.Sp_DeleteEqToken "
    '    DA_SQL.ExecuteNonQuery()
    'End Sub
    'Public Sub DeleteCurToken(Optional ByVal token As Long = 0)
    '    DA_SQL.ParamClear()
    '    DA_SQL.AddParam("@Instance", SqlDbType.VarChar, 17, gVarInstanceID)
    '    DA_SQL.AddParam("@token_no", SqlDbType.BigInt, 12, token)
    '    DA_SQL.Cmd_Text = "dbo.Sp_DeleteCurToken "
    '    DA_SQL.ExecuteNonQuery()
    'End Sub

    'Public Sub AppendFoToken(ByVal token As Long)
    '    DA_SQL.ParamClear()
    '    DA_SQL.AddParam("@F1", SqlDbType.VarChar, 17, gVarInstanceID)
    '    DA_SQL.AddParam("@FF1", SqlDbType.BigInt, 12, token)
    '    DA_SQL.Cmd_Text = "dbo.Sp_007"
    '    DA_SQL.ExecuteNonQuery()
    'End Sub

    'Public Sub AppendEQToken(ByVal token As Long)
    '    DA_SQL.ParamClear()
    '    DA_SQL.AddParam("@F1", SqlDbType.VarChar, 17, gVarInstanceID)
    '    DA_SQL.AddParam("@F2", SqlDbType.BigInt, 12, token)
    '    DA_SQL.Cmd_Text = "dbo.Sp_008"
    '    DA_SQL.ExecuteNonQuery()
    'End Sub

    'Public Sub AppendCurToken(ByVal token As Long)
    '    DA_SQL.ParamClear()
    '    DA_SQL.AddParam("@F1", SqlDbType.VarChar, 25, gVarInstanceID)
    '    DA_SQL.AddParam("@F2", SqlDbType.BigInt, 12, token)
    '    DA_SQL.Cmd_Text = "dbo.Sp_009"
    '    DA_SQL.ExecuteNonQuery()
    'End Sub

    'Public Sub AppendFoToken(ByVal token As Long)
    '    ''Write_Log2("AppendFoToken Process Start..")
    '    ' If bool_IsTelNet = False Then CheckTelNet()
    '    If bool_IsTelNet = False Then
    '        bool_IsTelNet = CheckTelNet()
    '    End If
    '    'bool_SqlSeverExist = IsSqlSeverExist()
    '    If bool_IsTelNet = True Then
    '        ' If bool_SqlSeverExist = True Then
    '        DA_SQL.IsConClose = True
    '        DA_SQL.ParamClear()
    '        DA_SQL.AddParam("@F1", SqlDbType.VarChar, 17, gVarInstanceID)
    '        DA_SQL.AddParam("@FF1", SqlDbType.BigInt, 12, token)
    '        DA_SQL.AddParam("@UCode", SqlDbType.NVarChar, 150, gvarInstanceCode)
    '        DA_SQL.Cmd_Text = "dbo.Sp_007"
    '        DA_SQL.ExecuteNonQuery()
    '        DA_SQL.IsConClose = False
    '        'End If
    '    End If
    '    ''Write_Log2("AppendFoToken Process Stop..")
    'End Sub

    'Public Sub AppendEQToken(ByVal token As Long)
    '    ''Write_Log2("AppendEQToken Process Start..")
    '    'If bool_IsTelNet = False Then CheckTelNet()
    '    If bool_IsTelNet = False Then
    '        bool_IsTelNet = CheckTelNet()
    '    End If
    '    'bool_SqlSeverExist = IsSqlSeverExist()
    '    If bool_IsTelNet = True Then
    '        '   If bool_SqlSeverExist = True Then
    '        DA_SQL.IsConClose = True
    '        DA_SQL.ParamClear()
    '        DA_SQL.AddParam("@F1", SqlDbType.VarChar, 17, gVarInstanceID)
    '        DA_SQL.AddParam("@F2", SqlDbType.BigInt, 12, token)
    '        DA_SQL.AddParam("@UCode", SqlDbType.NVarChar, 150, gvarInstanceCode)
    '        DA_SQL.Cmd_Text = "dbo.Sp_008"
    '        DA_SQL.ExecuteNonQuery()
    '        DA_SQL.IsConClose = False
    '        'End If
    '    End If
    '    ' 'Write_Log2("AppendEQToken Process Stop..")
    'End Sub

    'Public Sub AppendCurToken(ByVal token As Long)
    '    '  'Write_Log2("AppendCurToken Process Start..")
    '    'If bool_IsTelNet = False Then CheckTelNet()

    '    If bool_IsTelNet = False Then
    '        bool_IsTelNet = CheckTelNet()
    '    End If

    '    '  bool_SqlSeverExist = IsSqlSeverExist()
    '    If bool_IsTelNet = True Then
    '        '    If bool_SqlSeverExist = True Then
    '        DA_SQL.IsConClose = True
    '        DA_SQL.ParamClear()
    '        DA_SQL.AddParam("@F1", SqlDbType.VarChar, 25, gVarInstanceID)
    '        DA_SQL.AddParam("@F2", SqlDbType.BigInt, 12, token)
    '        DA_SQL.AddParam("@UCode", SqlDbType.NVarChar, 150, gvarInstanceCode)
    '        DA_SQL.Cmd_Text = "dbo.Sp_009"
    '        DA_SQL.ExecuteNonQuery()
    '        DA_SQL.IsConClose = False
    '    End If
    '    'End If
    '    ''Write_Log2("AppendCurToken Process Stop..")
    'End Sub

    Public Sub DeleteFoTokens(Optional ByVal tokens As String = "")
        Try
            q_AppendTokens.Enqueue("FO,DEL|" & tokens)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub DeleteFoToken(Optional ByVal token As Long = 0)
        Try
            q_AppendTokens.Enqueue("FO,DEL|" & CStr(token))
        Catch ex As Exception

        End Try

    End Sub

    Public Sub DeleteEqToken(Optional ByVal token As Long = 0)
        Try
            q_AppendTokens.Enqueue("EQ,DEL|" & CStr(token))
        Catch ex As Exception

        End Try

    End Sub
    Public Sub DeleteCurToken(Optional ByVal token As Long = 0)
        Try
            q_AppendTokens.Enqueue("CUR,DEL|" & CStr(token))
        Catch ex As Exception

        End Try

    End Sub

    'Public Sub AppendFoTokens(ByRef tokens As String)
    '    'DA_SQL.ParamClear()
    '    'DA_SQL.AddParam("@F1", SqlDbType.VarChar, 17, gVarInstanceID)
    '    'DA_SQL.AddParam("@FF1", SqlDbType.VarChar, 0, tokens)
    '    'DA_SQL.Cmd_Text = "dbo.Sp_028"
    '    'DA_SQL.ExecuteNonQuery()
    '    DA_SQL.Execute("dbo.Sp_028 '" & gVarInstanceID & "','" & tokens & "'")
    'End Sub

    'Public Sub AppendEQTokens(ByRef tokens As String)
    '    'DA_SQL.ParamClear()
    '    'DA_SQL.AddParam("@F1", SqlDbType.VarChar, 17, gVarInstanceID)
    '    'DA_SQL.AddParam("@FF1", SqlDbType.VarChar, 0, tokens)
    '    'DA_SQL.Cmd_Text = "dbo.Sp_029"
    '    'DA_SQL.ExecuteNonQuery()
    '    DA_SQL.Execute("dbo.Sp_029 '" & gVarInstanceID & "','" & tokens & "'")
    'End Sub

    'Public Sub AppendCurTokens(ByRef tokens As String)
    '    'DA_SQL.ParamClear()
    '    'DA_SQL.AddParam("@F1", SqlDbType.VarChar, 25, gVarInstanceID)
    '    'DA_SQL.AddParam("@FF1", SqlDbType.VarChar, 0, tokens)
    '    'DA_SQL.Cmd_Text = "dbo.Sp_030"
    '    DA_SQL.Execute("dbo.Sp_030 '" & gVarInstanceID & "','" & tokens & "'")
    '    'DA_SQL.ExecuteNonQuery()
    'End Sub
    Public Sub Queueclear()
        Try

            q_AppendTokens.Clear()
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AppendFoTokens(ByRef tokens As String)
        Try
            'If CLng(tokens) <> 0 Then

            If Not q_AppendTokens.Contains("FO,ADD|" & tokens) Then
                q_AppendTokens.Enqueue("FO,ADD|" & tokens)
            End If

            ' End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub AppendEQTokens(ByRef tokens As String)
        Try
            ' If CLng(tokens) <> 0 Then
            If Not q_AppendTokens.Contains("EQ,ADD|" & tokens) Then
                q_AppendTokens.Enqueue("EQ,ADD|" & tokens)
            End If

            ' End If

        Catch ex As Exception

        End Try
    End Sub
    Public Sub AppendCurTokens(ByRef tokens As String)
        Try
            'If CLng(tokens) <> 0 Then
            If Not q_AppendTokens.Contains("CUR,ADD|" & tokens) Then
                q_AppendTokens.Enqueue("CUR,ADD|" & tokens)
            End If

            ' End If

        Catch ex As Exception

        End Try
    End Sub

    'Public Function SelectFoToken() As SqlDataReader
    '    Return DA_SQL.FillListFo("dbo.Sp_SelectFo '" & gVarInstanceID & "'")
    'End Function

    ' Public Function SelectEqToken() As SqlDataReader
    '     Return DA_SQL.FillListEq("dbo.Sp_SelectEq '" & gVarInstanceID & "'")
    'End Function

    'Public Function SelectCurToken() As SqlDataReader
    '    Return DA_SQL.FillListCur("dbo.Sp_SelectCur '" & gVarInstanceID & "'")
    'End Function

    Private Sub AppendTokens()

        While True
            Try

                Threading.Thread.Sleep(100)
                If bool_IsTelNet = False Then
                    bool_IsTelNet = CheckTelNet()
                End If

                If bool_IsTelNet And (q_AppendTokens.Count > 0) And FlgTcpBCast = False Then
                    Dim str As String = ""
                    Dim strSegment As String = ""
                    Dim strAction As String = ""
                    Dim tokens As String = ""
                    str = q_AppendTokens.Dequeue
                    strSegment = str.Split("|")(0).Split(",")(0)
                    strAction = str.Split("|")(0).Split(",")(1)
                    tokens = str.Split("|")(1)



                    If strAction = "ADD" Then


                        Select Case strSegment
                            Case "FO"
                                '===Fo Append=================
                                DA_SQL.IsConClose = True
                                DA_SQL.Execute("dbo.Sp_028 '" & gVarInstanceID & "','" & tokens & "','" & gvarInstanceCode & "'")
                                DA_SQL.IsConClose = False
                                '===Fo Append=================
                            Case "EQ"
                                DA_SQL.IsConClose = True
                                DA_SQL.Execute("dbo.Sp_029 '" & gVarInstanceID & "','" & tokens & "','" & gvarInstanceCode & "'")
                                DA_SQL.IsConClose = False
                            Case "CUR"
                                DA_SQL.IsConClose = True
                                DA_SQL.Execute("dbo.Sp_030 '" & gVarInstanceID & "','" & tokens & "','" & gvarInstanceCode & "'")
                                DA_SQL.IsConClose = False
                            Case Else
                                Threading.Thread.Sleep(100)
                        End Select
                    Else
                        Select Case strSegment
                            Case "FO"
                                If tokens.Contains(",") Then
                                    '===Fo Append=================
                                    DA_SQL.IsConClose = True
                                    DA_SQL.ParamClear()
                                    DA_SQL.AddParam("@F1", SqlDbType.VarChar, 25, gVarInstanceID)
                                    DA_SQL.AddParam("@FF1", SqlDbType.VarChar, 1000, tokens)
                                    DA_SQL.Cmd_Text = "dbo.Sp_032"
                                    DA_SQL.ExecuteNonQuery()
                                    DA_SQL.IsConClose = False
                                    '===Fo Append=================
                                Else
                                    Dim token As Long = "0"
                                    token = CLng(tokens)
                                    DA_SQL.IsConClose = True
                                    DA_SQL.ParamClear()
                                    DA_SQL.AddParam("@F1", SqlDbType.VarChar, 25, gVarInstanceID)
                                    DA_SQL.AddParam("@FF1", SqlDbType.BigInt, 12, token)
                                    DA_SQL.Cmd_Text = "dbo.Sp_010"
                                    DA_SQL.ExecuteNonQuery()
                                    DA_SQL.IsConClose = False
                                End If

                            Case "EQ"
                                If Not tokens.Contains(",") Then
                                    Dim token As Long = "0"
                                    token = CLng(tokens)
                                    DA_SQL.IsConClose = True
                                    DA_SQL.ParamClear()
                                    DA_SQL.AddParam("@F1", SqlDbType.VarChar, 25, gVarInstanceID)
                                    DA_SQL.AddParam("@F2", SqlDbType.BigInt, 12, token)
                                    DA_SQL.Cmd_Text = "dbo.Sp_011"
                                    DA_SQL.ExecuteNonQuery()
                                    DA_SQL.IsConClose = False
                                End If
                            Case "CUR"
                                If Not tokens.Contains(",") Then
                                    Dim token As Long = "0"
                                    token = CLng(tokens)
                                    DA_SQL.IsConClose = True
                                    DA_SQL.ParamClear()
                                    DA_SQL.AddParam("@F1", SqlDbType.VarChar, 25, gVarInstanceID)
                                    DA_SQL.AddParam("@F2", SqlDbType.BigInt, 12, token)
                                    DA_SQL.Cmd_Text = "dbo.Sp_012"
                                    DA_SQL.ExecuteNonQuery()
                                    DA_SQL.IsConClose = False
                                End If
                            Case Else
                                Threading.Thread.Sleep(500)
                        End Select
                    End If

                Else
                Threading.Thread.Sleep(500)

                End If

            Catch ex As Exception

            End Try
        End While
    End Sub




    Public Sub New()
        'IsValidConnection = DA_SQL.IsValidConnection()

        Thr_RegunRegTokens = New Threading.Thread(AddressOf AppendTokens)
        Thr_RegunRegTokens.Name = "Thr_RegunRegTokens"
        Thr_RegunRegTokens.Start()



    End Sub

End Class
