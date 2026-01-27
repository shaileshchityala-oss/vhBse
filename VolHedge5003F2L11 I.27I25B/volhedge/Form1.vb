Imports System.Net
Imports System.DirectoryServices
Imports System.net.Sockets
Imports System.Net.Sockets.Socket
Public Class Form1
    Dim arr As New ArrayList

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim arry(3, 3, 3, 3, 3) As String
        ''arry(0, 0, 0) = "A"
        ''arry(0, 0, 1) = "B"
        ''arry(0, 0, 2) = "A"
        ''arry(0, 0, 3) = "A"

        ''arry(0, 1, 0) = "A"
        ''arry(0, 1, 1) = "A"
        ''arry(0, 1, 2) = "A"
        ''arry(0, 1, 3) = "A"

        ''arry(0, 2, 0) = "A"
        ''arry(0, 2, 1) = "A"
        ''arry(0, 2, 2) = "A"
        ''arry(0, 2, 3) = "A"

        ''arry(0, 3, 0) = "A"
        ''arry(0, 3, 1) = "A"
        ''arry(0, 3, 2) = "A"
        ''arry(0, 3, 3) = "A"

        ''arry(1, 0, 0) = "A"
        ''arry(1, 0, 1) = "B"
        ''arry(1, 0, 2) = "A"
        ''arry(1, 0, 3) = "A"

        ''arry(1, 1, 0) = "A"
        ''arry(1, 1, 1) = "A"
        ''arry(1, 1, 2) = "A"
        ''arry(1, 1, 3) = "A"

        ''arry(1, 2, 0) = "A"
        ''arry(1, 2, 1) = "A"
        ''arry(1, 2, 2) = "A"
        ''arry(1, 2, 3) = "A"

        ''arry(1, 3, 0) = "A"
        ''arry(1, 3, 1) = "A"
        ''arry(1, 3, 2) = "A"
        ''arry(1, 3, 3) = "A"

        ''arry(2, 0, 0) = "A"
        ''arry(2, 0, 1) = "B"
        ''arry(2, 0, 2) = "A"
        ''arry(2, 0, 3) = "A"

        ''arry(2, 1, 0) = "A"
        ''arry(2, 1, 1) = "B"
        ''arry(2, 1, 2) = "A"
        ''arry(2, 1, 3) = "A"

        ''arry(2, 2, 0) = "A"
        ''arry(2, 2, 1) = "B"
        ''arry(2, 2, 2) = "A"
        ''arry(2, 2, 3) = "A"

        ''arry(2, 3, 0) = "A"
        ''arry(2, 3, 1) = "B"
        ''arry(2, 3, 2) = "A"
        ''arry(2, 3, 3) = "A"

        ''arry(3, 0, 0) = "A"
        ''arry(3, 0, 1) = "B"
        ''arry(3, 0, 2) = "A"
        ''arry(3, 0, 3) = "A"

        ''arry(3, 1, 0) = "A"
        ''arry(3, 1, 1) = "B"
        ''arry(3, 1, 2) = "A"
        ''arry(3, 1, 3) = "A"

        ''arry(3, 2, 0) = "A"
        ''arry(3, 2, 1) = "B"
        ''arry(3, 2, 2) = "A"
        ''arry(3, 2, 3) = "A"

        ''arry(3, 3, 0) = "A"
        ''arry(3, 3, 1) = "B"
        ''arry(3, 3, 2) = "A"
        ''arry(3, 3, 3) = "A"



        'For i As Integer = 0 To 3
        '    For j As Integer = 0 To 3
        '        For k As Integer = 0 To 3
        '            For m As Integer = 0 To 3
        '                For n As Integer = 0 To 3
        '                    arry(i, j, k, m, n) = "a," & i & "," & j & "," & k & "," & m & "," & n
        '                Next
        '            Next
        '        Next
        '    Next
        'Next

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim objSec As New security_setting
        'MsgBox(objSec.Soft_count)

        arr = GetAllIP()
       
    End Sub
    Public Shared Function GetAllIP() As ArrayList
        'args in the signature is optional, without it
        'the function will simply get the hostname
        'of the local machine then go from there

        'Dim strHostName As New String("")
        'If args.Length = 0 Then
        '    ' Getting Ip address of local machine...
        '    ' First get the host name of local machine.
        '    strHostName = Dns.GetHostName()
        '    Console.WriteLine("Local Machine's Host Name: " + strHostName)
        'Else
        '    strHostName = args(0)
        'End If

        '' Then using host name, get the IP address list..
        'Dim ipEntry As IPHostEntry = Dns.GetHostByName(strHostName)
        'Dim addr As IPAddress() = ipEntry.AddressList

        'Dim i As Integer = 0
        'While i < addr.Length
        '    Debug.WriteLine(addr(i).ToString())
        '    System.Math.Max(System.Threading.Interlocked.Increment(i), i - 1)
        'End While
        Dim arr As IPAddress
        Dim arrip As New ArrayList
        Dim childEntry As DirectoryEntry
        Dim ParentEntry As New DirectoryEntry
        Try
            ParentEntry.Path = "WinNT:"
            For Each childEntry In ParentEntry.Children
                Select Case childEntry.SchemaClassName
                    Case "Domain"

                        Dim SubChildEntry As DirectoryEntry
                        Dim SubParentEntry As New DirectoryEntry
                        SubParentEntry.Path = "WinNT://" & childEntry.Name
                        For Each SubChildEntry In SubParentEntry.Children

                            Select Case SubChildEntry.SchemaClassName
                                Case "Computer"
                                    Try
                                        If SubChildEntry.Name <> Dns.GetHostName() Then
                                            Dim ipEntry As IPHostEntry = Dns.GetHostByName(SubChildEntry.Name)
                                            arr = New IPAddress(ipEntry.AddressList(0).Address)
                                            arrip.Add(arr.ToString)
                                        End If
                                    Catch ex As Exception
                                    End Try

                            End Select
                        Next
                End Select
            Next

        Catch Excep As Exception
            MsgBox("Error While Reading Directories.")
        Finally
            ParentEntry = Nothing
        End Try
        Return arrip
    End Function


    Private Function ping(ByVal arr As ArrayList) As Integer
        Dim count As Integer = 0
        Try
            For i As Integer = 0 To arr.Count - 1
                Try
                    Dim myIP As IPAddress = IPAddress.Parse(arr(i).ToString)
                    Dim tcpClient As New TcpClient()
                    tcpClient.Connect(myIP, 11003)
                    count += 1
                    tcpClient.Close()
                Catch e As Exception
                End Try
            Next
        Catch e As Exception
            'count = 0
            'Do what you want in here if the ping is unsuccessful
        End Try
        Return count
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        MsgBox(ping(arr))
    End Sub
End Class