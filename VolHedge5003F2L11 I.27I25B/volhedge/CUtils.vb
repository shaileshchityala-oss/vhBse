Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports ADOX
Imports ADODB
Imports System.Data.OleDb
Imports System.Text
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Threading
Imports System.Diagnostics
Public Class CUtils
    Public Shared Function StringToDouble(ByVal input As String) As Double
        If String.IsNullOrEmpty(input) Then
            Return 0
        End If
        Try
            Return Convert.ToDouble(input)
        Catch
            Return 0
        End Try
    End Function

    Public Shared Function StringToInt(ByVal input As String) As Integer

        If String.IsNullOrEmpty(input) Then
            Return 0
        End If

        Try
            Return Convert.ToInt32(input)
        Catch
            Return 0
        End Try
    End Function


    Public Shared Function IsConnectedToInternet(Optional testUrl As String = "http://www.google.com") As Boolean
        Try
            Dim req As WebRequest = WebRequest.Create(testUrl)
            req.Timeout = 3000 ' 3 seconds timeout
            Using resp As WebResponse = req.GetResponse()
                Return True
            End Using
        Catch
            Return False
        End Try
    End Function

    Public Shared Sub OpenWebsite(ByVal url As String)
        Try
            Dim chromePath As String = "C:\Program Files\Google\Chrome\Application\chrome.exe"
            Process.Start(chromePath, url)
        Catch ex As Exception
            ' MessageBox.Show("Failed to open URL: " & ex.Message)
        End Try
    End Sub

    Public Shared Function AccessDbDateTime(pDt As DateTime) As String
        Return pDt.ToString("MM/dd/yyyy HH:mm:ss")
    End Function
    Public Shared Function DateTimeToString(pDt As DateTime) As String
        Return pDt.ToString("dd-MMM-yyyy hh:mm:ss tt")
    End Function


    'Sub AddQuery()
    '    Dim cat As New Catalog()
    '    cat.ActiveConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=greek.mdb;"

    '    Dim queryName As String = "MyNewQuery"
    '    Dim sql As String = "SELECT TOP 10 * FROM MyTable ORDER BY ID DESC"

    '    ' Drop if exists
    '    For Each v As View In cat.Views
    '        If v.Name = queryName Then
    '            cat.Views.Delete(queryName)
    '            Exit For
    '        End If
    '    Next

    '    ' Add new query
    '    cat.Views.Append(queryName, sql)
    '    Console.WriteLine("Query added.")
    'End Sub


    'Sub AddColumn()
    '    Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=greek.mdb;"
    '    Using conn As New OleDbConnection(connStr)
    '        conn.Open()
    '        Dim cmd As New OleDbCommand("ALTER TABLE MyTable ADD COLUMN NewField TEXT(50)", conn)
    '        cmd.ExecuteNonQuery()
    '    End Using
    '    Console.WriteLine("Column added.")
    'End Sub

    'Sub AddTable()
    '    Dim connStr As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=greek.mdb;"
    '    Using conn As New OleDbConnection(connStr)
    '        conn.Open()
    '        Dim cmd As New OleDbCommand("
    '        CREATE TABLE MyNewTable (
    '            ID AUTOINCREMENT PRIMARY KEY,
    '            Name TEXT(100),
    '            CreatedDate DATETIME
    '        )", conn)
    '        cmd.ExecuteNonQuery()
    '    End Using
    '    Console.WriteLine("Table created.")
    'End Sub
    <StructLayout(LayoutKind.Sequential)>
    Private Structure NETRESOURCE
        Public dwScope As Integer
        Public dwType As Integer
        Public dwDisplayType As Integer
        Public dwUsage As Integer
        <MarshalAs(UnmanagedType.LPTStr)> Public lpLocalName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpRemoteName As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpComment As String
        <MarshalAs(UnmanagedType.LPTStr)> Public lpProvider As String
    End Structure

    ' Import WNetAddConnection2 from mpr.dll
    <DllImport("mpr.dll", CharSet:=CharSet.Auto)>
    Private Shared Function WNetAddConnection2(ByRef nr As NETRESOURCE,
                                               ByVal password As String,
                                               ByVal username As String,
                                               ByVal flags As Integer) As Integer
    End Function

    ' Connect to a UNC share using username/password
    Public Shared Sub Connect(ByVal uncPath As String, ByVal username As String, ByVal password As String)
        Dim nr As New NETRESOURCE
        nr.dwType = 1 ' Disk
        nr.lpRemoteName = uncPath

        Dim result As Integer = WNetAddConnection2(nr, password, username, 0)
        If result <> 0 Then
            Throw New Win32Exception(result)
        End If
    End Sub

    Public Shared Function FileGetSize(ByVal filePath As String, Optional ByVal unit As String = "B") As Long

        If Not File.Exists(filePath) Then
            Return -1 ' File does not exist
        End If

        Dim fileInfo As New FileInfo(filePath)
        Dim sizeInBytes As Long = fileInfo.Length

        Select Case unit.ToUpper()
            Case "KB"
                Return sizeInBytes / 1024
            Case "MB"
                Return sizeInBytes / 1024 / 1024
            Case Else
                Return sizeInBytes ' Default: bytes
        End Select
    End Function
    Public Shared Function GetCsvRowCount(filePath As String) As Integer
        Dim count As Integer = 0
        Try
            Using reader As New StreamReader(filePath)
                While Not reader.EndOfStream
                    reader.ReadLine()
                    count += 1
                End While
            End Using
            Return count
        Catch ex As Exception
            Return 0
        End Try

    End Function

    Public Shared Function GetCellValueString(ByVal dgv As DataGridView, ByVal rowIndex As Integer, ByVal colIndex As Integer) As String
        Try
            ' Ensure row and column exist
            If rowIndex < 0 OrElse rowIndex >= dgv.Rows.Count Then Return ""
            If colIndex < 0 OrElse colIndex >= dgv.Columns.Count Then Return ""

            Dim cellValue = dgv.Rows(rowIndex).Cells(colIndex).FormattedValue

            If cellValue Is Nothing OrElse String.IsNullOrWhiteSpace(cellValue.ToString()) Then
                Return "" ' Return blank if Nothing or empty
            End If

            Return cellValue.ToString().Trim()
        Catch ex As Exception
            Return "" ' Return blank if any unexpected error
        End Try
    End Function

    Public Shared Function GetCellValueInt(ByVal dgv As DataGridView, ByVal rowIndex As Integer, ByVal colIndex As Integer) As Integer
        Dim strVal As String = GetCellValueString(dgv, rowIndex, colIndex)
        Return CUtils.StringToInt(strVal)
    End Function

    Public Shared Sub RestartApplication()
        Try
            Dim exePath As String = Application.ExecutablePath
            Dim currentId As Integer = Process.GetCurrentProcess().Id
            Dim appName As String = Process.GetCurrentProcess().ProcessName

            ' Kill any duplicate instances (optional)
            For Each proc As Process In Process.GetProcessesByName(appName)
                If proc.Id <> currentId Then
                    Try : proc.Kill() : Catch : End Try
                End If
            Next

            ' Start new instance *after* small delay using CMD
            ' so this instance can fully exit first
            Dim psi As New ProcessStartInfo()
            psi.FileName = "cmd.exe"
            psi.Arguments = "/C timeout /T 1 /NOBREAK >nul & start """" """ & exePath & """"
            psi.CreateNoWindow = True
            psi.UseShellExecute = False
            Process.Start(psi)

            ' Exit current app
            Application.Exit()
            Environment.Exit(0)

        Catch ex As Exception
            MessageBox.Show("Restart failed: " & ex.Message)
        End Try
    End Sub

    Public Shared Function GetFilteredTable(sourceTable As DataTable, filterExpression As String) As DataTable

        ' Safety check
        If sourceTable Is Nothing Then
            Return Nothing
        End If

        Dim rows() As DataRow = sourceTable.Select(filterExpression)

        ' If rows found, copy them manually
        If rows.Length > 0 Then
            Dim dt As DataTable = sourceTable.Clone() ' Copy structure
            For Each r As DataRow In rows
                dt.ImportRow(r)
            Next
            Return dt
        End If

        ' No rows found → return empty table with structure
        Return sourceTable.Clone()

    End Function

    Public Shared Function DeleteWithTimeout(filePath As String, Optional timeoutMs As Integer = 2000) As Boolean
        Try
            If File.Exists(filePath) Then
                File.Delete(filePath)
            Else
                Return True ' Already deleted
            End If

            ' Retry wait loop
            Dim startTime As DateTime = DateTime.Now

            While File.Exists(filePath)
                Thread.Sleep(50) ' Small wait interval

                If (DateTime.Now - startTime).TotalMilliseconds >= timeoutMs Then
                    Return False ' Timed out
                End If
            End While

            Return True ' Successfully deleted

        Catch
            Return False
        End Try

    End Function

    Public Shared Function WaitUntilFree(filePath As String, Optional timeoutMs As Integer = 3000) As Boolean

        If Not File.Exists(filePath) Then Return True 'No file = not locked

        Dim start As DateTime = DateTime.Now

        While True

            If Not IsFileInUse(filePath) Then
                Return True ' File is now free
            End If

            ' Timeout check
            If (DateTime.Now - start).TotalMilliseconds >= timeoutMs Then
                Return False ' Still locked after timeout
            End If

            Thread.Sleep(100) ' Short wait before retry
        End While

    End Function

    ''' <summary>
    ''' Returns True if file is locked / in use, False if free.
    ''' </summary>
    Public Shared Function IsFileInUse(filePath As String) As Boolean

        If Not File.Exists(filePath) Then Return False

        Try
            Using fs As FileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
                Return False ' File unlocked
            End Using

        Catch ex As IOException
            Return True ' File locked

        Catch
            Return True ' Other access issues treat as locked

        End Try

    End Function

    Public Shared Function StringToDate(pStrDate As String, Optional format As String = "dd-MM-yyyy") As Date
        If String.IsNullOrEmpty(pStrDate) Then Return Nothing
        Dim result As Date
        If Date.TryParseExact(
            pStrDate.Trim(),
            format,
            Globalization.CultureInfo.InvariantCulture,
            Globalization.DateTimeStyles.None,
            result
        ) Then
            Return result
        End If

        Return Nothing
    End Function

    Public Shared Function GetDriveLetterIfExists(pPath As String) As String
        Try
            Dim root As String = Path.GetPathRoot(pPath)
            If String.IsNullOrEmpty(root) Then Return ""

            Dim drive As New DriveInfo(root.Substring(0, 1))
            If drive.IsReady Then
                Return drive.Name.Substring(0, 1)
            End If
        Catch
        End Try

        Return ""
    End Function

    Public Shared Function GetSumTableFieldCompute(table As DataTable, columnName As String, filter As String, pRound As Int16) As Double
        Try
            Dim obj = table.Compute("SUM(" & columnName & ")", filter)
            If IsDBNull(obj) Then Return 0
            Dim dblVal As Double = CDbl(obj)
            If pRound > 0 Then
                Return Math.Round(dblVal, pRound)
            Else
                Return dblVal
            End If
        Catch
            Return 0
        End Try

    End Function




End Class
