Imports System.Management
Imports System.Security.Cryptography
Imports System.Text
Imports System.Runtime.InteropServices

Public Class CHardwareFingerprint

    Public Shared Function GetRawHardwareIds() As String
        Dim motherboard As String = GetWmiProperty("Win32_BaseBoard", "SerialNumber")
        Dim bios As String = GetWmiProperty("Win32_BIOS", "SerialNumber")
        Dim uuid As String = GetWmiProperty("Win32_ComputerSystemProduct", "UUID")

        Return "Motherboard: " & motherboard & Environment.NewLine &
               "BIOS: " & bios & Environment.NewLine &
               "System UUID: " & uuid
    End Function
    Public Shared Function GetHardwareFingerprint() As String
        Dim motherboard As String = GetWmiProperty("Win32_BaseBoard", "SerialNumber")
        Dim bios As String = GetWmiProperty("Win32_BIOS", "SerialNumber")
        Dim uuid As String = GetWmiProperty("Win32_ComputerSystemProduct", "UUID")

        Dim raw As String = motherboard & "|" & bios & "|" & uuid

        'Return GetSha256(raw)
        Return raw
    End Function
    Private Shared Function GetWmiProperty(wmiClass As String, propertyName As String) As String
        Try
            Dim mc As New ManagementClass(wmiClass)
            Dim moc As ManagementObjectCollection = mc.GetInstances()

            For Each mo As ManagementObject In moc
                Dim val As Object = mo(propertyName)
                If val IsNot Nothing Then
                    Return val.ToString().Trim()
                End If
            Next
        Catch
            ' ignore errors
        End Try
        Return ""
    End Function
    Private Shared Function GetSha256(input As String) As String
        Dim sha As SHA256 = SHA256.Create()
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(input)
        Dim hash As Byte() = sha.ComputeHash(bytes)

        Dim sb As New StringBuilder()
        For Each b As Byte In hash
            sb.Append(b.ToString("X2"))
        Next
        Return sb.ToString()
    End Function

    <DllImport("UniqueID.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function GetProcessorSeialNumber(ByVal str As Boolean) As String
    End Function

    <DllImport("UniqueID.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function LoadDiskInfo() As String
    End Function


End Class


Public Class UniqueIdHelper

    ' Correct binding to native export
    <DllImport("UniqueID.dll",
               EntryPoint:="LoadDiskInfo",
               CallingConvention:=CallingConvention.Cdecl,
               CharSet:=CharSet.Ansi)>
    Private Shared Function LoadDiskInfo(flag As Boolean) As IntPtr
    End Function

    ' Safe managed wrapper
    Public Shared Function GetProcessorSerial() As String
        Try
            Dim ptr As IntPtr = LoadDiskInfo(True)
            If ptr = IntPtr.Zero Then Return String.Empty
            ' Convert unmanaged ANSI string to VB.NET string
            Return Marshal.PtrToStringAnsi(ptr)
        Catch ex As Exception
            ' Return any error info for debugging
            Return "Error: " & ex.Message
        End Try
    End Function

End Class