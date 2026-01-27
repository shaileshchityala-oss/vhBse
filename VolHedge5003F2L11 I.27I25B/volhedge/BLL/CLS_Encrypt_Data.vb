Public Class CLS_Encrypt_Data
    Dim Arr_Stack1 As New ArrayList()
    Dim Arr_Stack2 As New ArrayList()
    Dim Arr_Stack3 As New ArrayList()
    Dim Arr_Stack4 As New ArrayList()
    Dim Arr_Stack5 As New ArrayList()
    Dim Arr_Stack6 As New ArrayList()
    Dim Arr_Stack7 As New ArrayList()
    Dim Arr_Stack8 As New ArrayList()
    Dim Arr_Stack9 As New ArrayList()
    Dim Arr_Stack10 As New ArrayList()

    Sub New()
        Fill_Array_Stack(Arr_Stack1)
        Fill_Array_Stack(Arr_Stack2)
        Fill_Array_Stack(Arr_Stack3)
        Fill_Array_Stack(Arr_Stack4)
        Fill_Array_Stack(Arr_Stack5)
        Fill_Array_Stack(Arr_Stack6)
        Fill_Array_Stack(Arr_Stack7)
        Fill_Array_Stack(Arr_Stack8)
        Fill_Array_Stack(Arr_Stack9)
        Fill_Array_Stack(Arr_Stack10)
    End Sub

    Dim int_ASC_Count As Integer = 1
    Dim int_Array_Count As Integer = 1
    Private Sub Fill_Array_Stack(ByVal Arr_List As ArrayList)
        Select Case int_Array_Count
            Case 1
                int_ASC_Count = 75
            Case 2
                int_ASC_Count = 125
            Case 3
                int_ASC_Count = 25
            Case 4
                int_ASC_Count = 150
            Case 5
                int_ASC_Count = 200
            Case 6
                int_ASC_Count = 100
            Case 7
                int_ASC_Count = 225
            Case 8
                int_ASC_Count = 215
            Case 9
                int_ASC_Count = 50
            Case 10
                int_ASC_Count = 175
        End Select
        int_ASC_Count += 1
        For i As Integer = 0 To 10
            For i1 As Integer = 1 To 26
                If Arr_List.Contains(int_ASC_Count) = False Then
                    Arr_List.Add(int_ASC_Count)
                End If
                int_ASC_Count += 1
                If int_ASC_Count > 255 Then
                    int_ASC_Count = 0
                End If
            Next
        Next
        int_Array_Count += 1
    End Sub

    Public Function Encrypt_Data(ByVal Str_Data As String) As String
        Dim Str_Encrypt As String = ""
        Dim Chr_cnt As Integer = 1
        For Each TempChr As Char In Str_Data.ToCharArray
            Dim obj_idx As Integer = Chr_cnt Mod 10
            Select Case obj_idx
                Case 0
                    Str_Encrypt &= Chr(CInt(Arr_Stack10(Asc(TempChr))))
                Case 1
                    Str_Encrypt &= Chr(CInt(Arr_Stack1(Asc(TempChr))))
                Case 2
                    Str_Encrypt &= Chr(CInt(Arr_Stack2(Asc(TempChr))))
                Case 3
                    Str_Encrypt &= Chr(CInt(Arr_Stack3(Asc(TempChr))))
                Case 4
                    Str_Encrypt &= Chr(CInt(Arr_Stack4(Asc(TempChr))))
                Case 5
                    Str_Encrypt &= Chr(CInt(Arr_Stack5(Asc(TempChr))))
                Case 6
                    Str_Encrypt &= Chr(CInt(Arr_Stack6(Asc(TempChr))))
                Case 7
                    Str_Encrypt &= Chr(CInt(Arr_Stack7(Asc(TempChr))))
                Case 8
                    Str_Encrypt &= Chr(CInt(Arr_Stack8(Asc(TempChr))))
                Case 9
                    Str_Encrypt &= Chr(CInt(Arr_Stack9(Asc(TempChr))))
            End Select
            Chr_cnt += 1
        Next

        Return Str_Encrypt
    End Function

    Public Function Decrypt_Data(ByVal Str_Data As String) As String
        Dim Str_Decrypt As String = ""
        Dim Chr_cnt As Integer = 1
        For Each TempChr As Char In Str_Data.ToCharArray
            Dim obj_idx As Integer = Chr_cnt Mod 10
            Select Case obj_idx
                Case 0
                    Str_Decrypt &= Chr(CInt(Arr_Stack10.IndexOf(Asc(TempChr))))
                Case 1
                    Str_Decrypt &= Chr(CInt(Arr_Stack1.IndexOf(Asc(TempChr))))
                Case 2
                    Str_Decrypt &= Chr(CInt(Arr_Stack2.IndexOf(Asc(TempChr))))
                Case 3
                    Str_Decrypt &= Chr(CInt(Arr_Stack3.IndexOf(Asc(TempChr))))
                Case 4
                    Str_Decrypt &= Chr(CInt(Arr_Stack4.IndexOf(Asc(TempChr))))
                Case 5
                    Str_Decrypt &= Chr(CInt(Arr_Stack5.IndexOf(Asc(TempChr))))
                Case 6
                    Str_Decrypt &= Chr(CInt(Arr_Stack6.IndexOf(Asc(TempChr))))
                Case 7
                    Str_Decrypt &= Chr(CInt(Arr_Stack7.IndexOf(Asc(TempChr))))
                Case 8
                    Str_Decrypt &= Chr(CInt(Arr_Stack8.IndexOf(Asc(TempChr))))
                Case 9
                    Str_Decrypt &= Chr(CInt(Arr_Stack9.IndexOf(Asc(TempChr))))
            End Select
            Chr_cnt += 1
        Next

        Return Str_Decrypt
    End Function

End Class


'Public Class CLS_Encrypt_Data
'    Dim Arr_Stack1 As New ArrayList()
'    Dim Arr_Stack2 As New ArrayList()
'    Dim Arr_Stack3 As New ArrayList()
'    Dim Arr_Stack4 As New ArrayList()
'    Dim Arr_Stack5 As New ArrayList()
'    Dim Arr_Stack6 As New ArrayList()
'    Dim Arr_Stack7 As New ArrayList()
'    Dim Arr_Stack8 As New ArrayList()
'    Dim Arr_Stack9 As New ArrayList()
'    Dim Arr_Stack10 As New ArrayList()

'    Sub New()
'        Fill_Array_Stack(Arr_Stack1)
'        Fill_Array_Stack(Arr_Stack2)
'        Fill_Array_Stack(Arr_Stack3)
'        Fill_Array_Stack(Arr_Stack4)
'        Fill_Array_Stack(Arr_Stack5)
'        Fill_Array_Stack(Arr_Stack6)
'        Fill_Array_Stack(Arr_Stack7)
'        Fill_Array_Stack(Arr_Stack8)
'        Fill_Array_Stack(Arr_Stack9)
'        Fill_Array_Stack(Arr_Stack10)
'    End Sub

'    Dim int_ASC_Count As Integer = 1
'    Dim int_Array_Count As Integer = 1
'    Private Sub Fill_Array_Stack(ByVal Arr_List As ArrayList)
'        Select Case int_Array_Count
'            Case 1
'                int_ASC_Count = 75
'            Case 2
'                int_ASC_Count = 125
'            Case 3
'                int_ASC_Count = 25
'            Case 4
'                int_ASC_Count = 150
'            Case 5
'                int_ASC_Count = 200
'            Case 6
'                int_ASC_Count = 100
'            Case 7
'                int_ASC_Count = 225
'            Case 8
'                int_ASC_Count = 215
'            Case 9
'                int_ASC_Count = 50
'            Case 10
'                int_ASC_Count = 175
'        End Select
'        int_ASC_Count += 1
'        For i As Integer = 0 To 10
'            For i1 As Integer = 1 To 26
'                If Arr_List.Contains(int_ASC_Count) = False Then
'                    Arr_List.Add(int_ASC_Count)
'                End If
'                int_ASC_Count += 1
'                If int_ASC_Count > 255 Then
'                    int_ASC_Count = 1
'                End If
'            Next
'        Next

'        int_Array_Count += 1
'    End Sub

'    Public Function Encrypt_Data(ByVal Str_Data As String) As String
'        Dim Str_Encrypt As String = ""
'        Dim Chr_cnt As Integer = 1
'        For Each TempChr As Char In Str_Data.ToCharArray
'            Dim obj_idx As Integer = Chr_cnt Mod 10
'            Select Case obj_idx
'                Case 0
'                    Str_Encrypt &= Chr(CInt(Arr_Stack10(Asc(TempChr))))
'                Case 1
'                    Str_Encrypt &= Chr(CInt(Arr_Stack1(Asc(TempChr))))
'                Case 2
'                    Str_Encrypt &= Chr(CInt(Arr_Stack2(Asc(TempChr))))
'                Case 3
'                    Str_Encrypt &= Chr(CInt(Arr_Stack3(Asc(TempChr))))
'                Case 4
'                    Str_Encrypt &= Chr(CInt(Arr_Stack4(Asc(TempChr))))
'                Case 5
'                    Str_Encrypt &= Chr(CInt(Arr_Stack5(Asc(TempChr))))
'                Case 6
'                    Str_Encrypt &= Chr(CInt(Arr_Stack6(Asc(TempChr))))
'                Case 7
'                    Str_Encrypt &= Chr(CInt(Arr_Stack7(Asc(TempChr))))
'                Case 8
'                    Str_Encrypt &= Chr(CInt(Arr_Stack8(Asc(TempChr))))
'                Case 9
'                    Str_Encrypt &= Chr(CInt(Arr_Stack9(Asc(TempChr))))
'            End Select
'            Chr_cnt += 1
'        Next

'        Return Str_Encrypt
'    End Function

'    Public Function Decrypt_Data(ByVal Str_Data As String) As String
'        Dim Str_Decrypt As String = ""
'        Dim Chr_cnt As Integer = 1
'        For Each TempChr As Char In Str_Data.ToCharArray
'            Dim obj_idx As Integer = Chr_cnt Mod 10
'            Select Case obj_idx
'                Case 0
'                    Str_Decrypt &= Chr(CInt(Arr_Stack10.IndexOf(Asc(TempChr))))
'                Case 1
'                    Str_Decrypt &= Chr(CInt(Arr_Stack1.IndexOf(Asc(TempChr))))
'                Case 2
'                    Str_Decrypt &= Chr(CInt(Arr_Stack2.IndexOf(Asc(TempChr))))
'                Case 3
'                    Str_Decrypt &= Chr(CInt(Arr_Stack3.IndexOf(Asc(TempChr))))
'                Case 4
'                    Str_Decrypt &= Chr(CInt(Arr_Stack4.IndexOf(Asc(TempChr))))
'                Case 5
'                    Str_Decrypt &= Chr(CInt(Arr_Stack5.IndexOf(Asc(TempChr))))
'                Case 6
'                    Str_Decrypt &= Chr(CInt(Arr_Stack6.IndexOf(Asc(TempChr))))
'                Case 7
'                    Str_Decrypt &= Chr(CInt(Arr_Stack7.IndexOf(Asc(TempChr))))
'                Case 8
'                    Str_Decrypt &= Chr(CInt(Arr_Stack8.IndexOf(Asc(TempChr))))
'                Case 9
'                    Str_Decrypt &= Chr(CInt(Arr_Stack9.IndexOf(Asc(TempChr))))
'            End Select
'            Chr_cnt += 1
'        Next

'        Return Str_Decrypt
'    End Function

'End Class

