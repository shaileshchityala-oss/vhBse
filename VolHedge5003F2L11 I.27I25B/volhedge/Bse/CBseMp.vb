Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Diagnostics
Imports System.Linq

Public Class CBseMarketPictureHeader
    Public mMessageType As UInteger        ' 2020 or 2021
    Public mReservedField1 As UInteger     ' For Internal Use
    Public mReservedField2 As UInteger     ' For Internal Use
    Public mReservedField3 As UShort       ' For Internal Use
    Public mHour As Short                  ' Time - HH
    Public mMinute As Short                ' Time - MM
    Public mSecond As Short                ' Time - SS
    Public mMillisecond As Short           ' Time - sss
    Public mReservedField4 As Short        ' For Internal Use
    Public mReservedField5 As Short        ' For Internal Use
    Public mNoOfRecords As Short           ' Number of market picture records (max 6)

    Public mListInstrument As List(Of CBseMarketPictureInstrument)

    Public Sub New()
        mListInstrument = New List(Of CBseMarketPictureInstrument)()
        Reset()
    End Sub

    Public Sub Reset()
        mMessageType = 0
        mReservedField1 = 0
        mReservedField2 = 0
        mReservedField3 = 0
        mHour = 0
        mMinute = 0
        mSecond = 0
        mMillisecond = 0
        mReservedField4 = 0
        mReservedField5 = 0
        mNoOfRecords = 0
        'mListInstrument.Clear()
    End Sub

    Public Sub Read(reader As BigEndianBinaryReader)
        mMessageType = reader.ReadUInt32()
        mReservedField1 = reader.ReadUInt32()
        mReservedField2 = reader.ReadUInt32()
        mReservedField3 = reader.ReadUInt16()
        mHour = reader.ReadInt16()
        mMinute = reader.ReadInt16()
        mSecond = reader.ReadInt16()
        mMillisecond = reader.ReadInt16()
        mReservedField4 = reader.ReadInt16()
        mReservedField5 = reader.ReadInt16()
        mNoOfRecords = reader.ReadInt16()
    End Sub

    Public Function ValidatePacket(receivedBytes As Byte()) As Boolean
        If receivedBytes Is Nothing OrElse receivedBytes.Length < 4 Then
            Return False
        End If

        Try
            Dim messageType As Int32 = ((receivedBytes(0) And &HFF) << 24) _
                         Or ((receivedBytes(1) And &HFF) << 16) _
                         Or ((receivedBytes(2) And &HFF) << 8) _
                         Or (receivedBytes(3) And &HFF)
            '           Dim messageType As Int32 = CInt((receivedBytes(0) << 24) Or (receivedBytes(1) << 16) Or (receivedBytes(2) << 8) Or receivedBytes(3))
            '  Debug.WriteLine(messageType.ToString() & " : " & receivedBytes.Length)
            If messageType = 2020 OrElse messageType = 2021 Then
                mMessageType = messageType
                Return True
            End If

            If messageType = 2011 OrElse messageType = 2012 Then
                mMessageType = messageType
                Return True
            End If

            Return False

        Catch
            Return False
        End Try
    End Function

    Public Shared Function ValidatePacket2020or2021(receivedBytes As Byte()) As Boolean
        If receivedBytes Is Nothing OrElse receivedBytes.Length < 4 Then
            Return False
        End If

        Dim msgTypeBytes As Byte() = receivedBytes.Take(4).ToArray()
        Array.Reverse(msgTypeBytes)
        Dim messageType As Integer = BitConverter.ToInt32(msgTypeBytes, 0)

        Return (messageType = 2020 OrElse messageType = 2021)
    End Function
End Class
