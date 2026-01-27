Imports System
Imports System.IO


Public Class BigEndianBinaryReader
    Inherits BinaryReader

    Public Sub New(input As Stream)
        MyBase.New(input)
    End Sub

    Public Overrides Function ReadInt16() As Short
        Dim data = MyBase.ReadBytes(2)
        Array.Reverse(data)
        Return BitConverter.ToInt16(data, 0)
    End Function

    Public Overrides Function ReadInt32() As Integer
        Dim data = MyBase.ReadBytes(4)
        Array.Reverse(data)
        Return BitConverter.ToInt32(data, 0)
    End Function
    Public Overrides Function ReadInt64() As Long
        Dim data = MyBase.ReadBytes(8)
        Array.Reverse(data)
        Return BitConverter.ToInt64(data, 0)
    End Function

    Public Overrides Function ReadUInt16() As UShort
        Dim data = MyBase.ReadBytes(2)
        Array.Reverse(data)
        Return BitConverter.ToUInt16(data, 0)
    End Function

    Public Overrides Function ReadUInt32() As UInteger
        Dim data = MyBase.ReadBytes(4)
        Array.Reverse(data)
        Return BitConverter.ToUInt32(data, 0)
    End Function

    Public Overrides Function ReadUInt64() As ULong
        Dim data = MyBase.ReadBytes(8)
        Array.Reverse(data)
        Return BitConverter.ToUInt64(data, 0)
    End Function
End Class

