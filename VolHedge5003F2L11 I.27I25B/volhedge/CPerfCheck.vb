Imports System
Imports System.Diagnostics
Imports System.IO

Public Class CPerfCheck

    Public mStart As DateTime
    Public mEnd As DateTime
    Public mDiff As TimeSpan
    Public mEnable As Integer
    Public mHash As Hashtable = New Hashtable()
    Public mHashCounter As Hashtable = New Hashtable()

    Dim sloc As Object = New Object()


    Public Sub New()
        mEnable = 4
        mLogFile = "PerfLog"
    End Sub

    Public Function CalcDiff(pStr As String) As String
        mEnd = DateTime.Now
        mDiff = mEnd - mStart
        mStart = mEnd

        Dim retstr As String

        retstr = pStr & mDiff.TotalMilliseconds().ToString() + " ms"

        Return retstr

    End Function
    Public Sub ResetCounter(pKey As String)
        If mHashCounter.ContainsKey(pKey) Then
            mHashCounter(pKey) = 0
        End If
    End Sub

    Public Function DiffMs(pKey As String, pStr As String) As String
        mEnd = DateTime.Now
        Dim hdt As DateTime
        Dim cnterVal As Integer

        If mHash.ContainsKey(pKey) = False Then
            mHash.Add(pKey, mEnd)
            cnterVal = 0
            mHashCounter.Add(pKey, cnterVal)
        Else
            hdt = mHash(pKey)
            mDiff = mEnd - hdt
            mHash(pKey) = mEnd
            cnterVal = mHashCounter(pKey)
            cnterVal = cnterVal + 1
            mHashCounter(pKey) = cnterVal
        End If

        Dim retstr As String
        retstr = pStr & mDiff.TotalMilliseconds().ToString() + " ms"

        Return retstr

    End Function

    Public Sub Write_DiffMs(pKey As String, pStr As String)
        Dim str As String
        If mEnable = 0 Then
            Return
        End If

        str = DiffMs(pKey, pStr)
        WriteLogStr(str)
    End Sub

    Public Sub Write_Counter(pKey As String)
        Dim str As String
        If mEnable = 0 Then
            Return
        End If

        Dim cnt As UInt64
        cnt = 0
        If mHashCounter.ContainsKey(pKey) = False Then
            mHashCounter.Add(pKey, cnt)
        Else
            cnt = mHashCounter(pKey)
            cnt += 1
            mHashCounter(pKey) = cnt
        End If
        str = "-----------------> Counter " & pKey & " : " & mHashCounter(pKey).ToString()

        SyncLock sloc
            Dim fstream As System.IO.StreamWriter
            Try
                If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                    Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
                End If
                fstream = New StreamWriter(Application.StartupPath & "\VolhedgeLog\" + mLogFile + ".txt", True)
                fstream.WriteLine(Now.ToString & "==>" & str)
                Debug.WriteLine(str)
                fstream.Close()
            Catch ex As Exception

            End Try
        End SyncLock
    End Sub

    Public Sub WriteLogStr(pStr As String)
        SyncLock sloc
            Dim fstream As System.IO.StreamWriter
            Try
                If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                    Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
                End If
                fstream = New StreamWriter(Application.StartupPath & "\VolhedgeLog\" + mLogFile + ".txt", True)
                fstream.WriteLine(Now.ToString & "==>" & pStr)
                'Debug.WriteLine(str)
                fstream.Close()
            Catch ex As Exception

            End Try
        End SyncLock
    End Sub
    Public Sub WriteStr(pStr As String)
        SyncLock sloc
            Dim fstream As System.IO.StreamWriter
            Try
                If Not Directory.Exists(Application.StartupPath & "\VolhedgeLog") Then
                    Directory.CreateDirectory(Application.StartupPath & "\VolhedgeLog")
                End If
                fstream = New StreamWriter(Application.StartupPath & "\VolhedgeLog\" + mLogFile + ".txt", True)
                fstream.WriteLine(pStr)
                'Debug.WriteLine(str)
                fstream.Close()
            Catch ex As Exception

            End Try
        End SyncLock
    End Sub

    Sub PrintDataTable(ByVal dt As DataTable)
        If dt Is Nothing Then
            WriteStr("DataTable is Nothing.")
            Return
        End If

        ' Print column headers
        Dim strCols As String = ""
        For Each col As DataColumn In dt.Columns
            'strCols += col.ColumnName & vbTab
            strCols += col.ColumnName & "|"
        Next
        WriteStr(strCols)

        Dim strRow As String = ""
        ' Print each row
        For Each row As DataRow In dt.Rows
            For Each col As DataColumn In dt.Columns
                strRow += row(col).ToString() & "|"
            Next
            WriteStr(strRow)
        Next
    End Sub

    Sub PrintDataTable(pDtName As String, ByVal pDt As DataTable)
        WriteStr("---------------------------------------------------------")
        WriteStr(pDtName)
        WriteStr("---------------------------------------------------------")
        PrintDataTable(pDt)
    End Sub

    Public Sub PrintTopRecords(ByVal dt As DataTable, ByVal count As Integer)

        If dt Is Nothing Then
            WriteStr("DataTable is Nothing.")
            Return
        End If

        If dt.Rows.Count = 0 Then
            WriteStr("DataTable has no rows.")
            Return
        End If

        WriteStr($"--- TOP {count} RECORDS (Total={dt.Rows.Count}) ---")

        ' Print column headers
        Dim header As String = ""
        For Each col As DataColumn In dt.Columns
            header &= col.ColumnName & "|"
        Next
        WriteStr(header)

        Dim maxRows As Integer = Math.Min(count, dt.Rows.Count)

        For i As Integer = 0 To maxRows - 1
            Dim rowStr As String = ""
            For Each col As DataColumn In dt.Columns
                rowStr &= dt.Rows(i)(col).ToString() & "|"
            Next
            WriteStr(rowStr)
        Next

    End Sub

    Public Sub PrintBottomRecords(ByVal dt As DataTable, ByVal count As Integer)

        If dt Is Nothing Then
            WriteStr("DataTable is Nothing.")
            Return
        End If

        If dt.Rows.Count = 0 Then
            WriteStr("DataTable has no rows.")
            Return
        End If

        WriteStr($"--- BOTTOM {count} RECORDS (Total={dt.Rows.Count}) ---")

        ' Print column headers
        Dim header As String = ""
        For Each col As DataColumn In dt.Columns
            header &= col.ColumnName & "|"
        Next
        WriteStr(header)

        Dim startIndex As Integer = Math.Max(0, dt.Rows.Count - count)

        For i As Integer = startIndex To dt.Rows.Count - 1
            Dim rowStr As String = ""
            For Each col As DataColumn In dt.Columns
                rowStr &= dt.Rows(i)(col).ToString() & "|"
            Next
            WriteStr(rowStr)
        Next

    End Sub


    Public mLogFile As String
    Private stackName As New Stack(Of String)
    Public Sub SetFileName(pStr As String)
        mLogFile = pStr
    End Sub

    ' Push current file name if valid and different
    Public Sub Push(pName As String)
        If String.IsNullOrEmpty(pName) Then Exit Sub

        ' Only push if it's not the same as current file
        If String.Equals(pName, mLogFile, StringComparison.OrdinalIgnoreCase) Then Exit Sub

        ' Save current before switching
        stackName.Push(mLogFile)
        mLogFile = pName
    End Sub

    ' Pop previous file name from stack (no MsgBox, no parameter)
    Public Sub Pop()
        If stackName.Count = 0 Then
            mLogFile = "PerfLog"
            Exit Sub
        End If
        ' Restore previous file name
        mLogFile = stackName.Pop()
    End Sub

    ' Optional helper functions
    Public Function GetCurrentFile() As String
        Return mLogFile
    End Function

    Public Function GetStackCount() As Integer
        Return stackName.Count
    End Function
End Class



'Public Class CFileNm
'    Public mLogFile As String
'    Public Sub SetFileName(pStr As String)
'        mLogFile = pStr
'    End Sub
'End Class