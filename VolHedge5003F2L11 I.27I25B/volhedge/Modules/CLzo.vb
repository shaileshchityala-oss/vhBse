Imports System.Runtime.InteropServices

Public Class CLzoCpm
    <DllImport("lzo.dll", CallingConvention:=CallingConvention.Cdecl)>
    Private Shared Function lzo1z_decompress(
        src As Byte(),
        src_len As Integer,
        dst As Byte(),
        ByRef dst_len As Integer,
        wrkmem As Byte()
    ) As Integer
    End Function

    ' Work memory (scratch buffer for LZO)
    Private _workMemory As Byte() = New Byte(65536 - 1) {}

    ' Decompress compressed buffer into destination buffer
    Public Function DeCompressBuff(ByRef compressed As Byte(),
                                   ByRef output As Byte(),
                                   ByRef outputLen As Integer) As Boolean

        Dim rc As Integer = lzo1z_decompress(compressed, compressed.Length,
                                             output, 1024, _workMemory)

        ' Return True if success, False otherwise
        If rc = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
End Class