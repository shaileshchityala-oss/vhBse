Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.IO


Public Class CBseIndexMsgHeader
        Public mMessageType As UInt32        ' 2020 or 2021
        Public mResevered1 As Int32
        Public mResevered2 As Int32
        Public mResevered3 As UInt16

        Public mHour As UInt16
        Public mMinute As UInt16
        Public mSecond As UInt16
        Public mMillisecond As UInt16

        Public mReserverd4 As UInt16
        Public mReserverd5 As UInt16

        Public mNoOfRecords As UInt16

        Public Sub Read(reader As BigEndianBinaryReader)
            mMessageType = reader.ReadUInt32()
            mResevered1 = reader.ReadInt32()
            mResevered2 = reader.ReadInt32()
            mResevered3 = reader.ReadUInt16()

            mHour = reader.ReadUInt16()
            mMinute = reader.ReadUInt16()
            mSecond = reader.ReadUInt16()
            mMillisecond = reader.ReadUInt16()

            mReserverd4 = reader.ReadUInt16()
            mReserverd5 = reader.ReadUInt16()
            mNoOfRecords = reader.ReadUInt16()
        End Sub

        Public Sub Reset()
            mMessageType = 0
            mResevered1 = 0
            mResevered2 = 0
            mResevered3 = 0

            mHour = 0
            mMinute = 0
            mSecond = 0
            mMillisecond = 0

            mReserverd4 = 0
            mReserverd5 = 0
            mNoOfRecords = 0
        End Sub
    End Class

    Public Class CIndexMsgInstrument
        Public mInstrumentCode As Int32

        Public mHigh As Int32
        Public mLow As Int32
        Public mOpen As Int32
        Public mClose As Int32
        Public mIndex As Int32
        Public mTemp As Int32

        Public mIndexID As Char()

        Public mResereved1 As Char()
        'Public mResevered2 As Char
        'Public mResevered3 As Char
        'Public mResevered4 As Char
        'Public mResevered5 As Char
        Public mCloseType As Int16
        Public mReserved6 As Int16

        Public mToday As DateTime

        Public Sub New()
            mToday = DateTime.Now
            mIndexID = New Char(6) {}
            mResereved1 = New Char(4) {}
        End Sub

        Public Sub Read(reader As BigEndianBinaryReader)
            mInstrumentCode = reader.ReadInt32()

            mHigh = reader.ReadInt32()
            mLow = reader.ReadInt32()
            mOpen = reader.ReadInt32()
            mClose = reader.ReadInt32()
            mIndex = reader.ReadInt32()
            'mTemp = reader.ReadInt32()

            mIndexID = reader.ReadChars(mIndexID.Length)
            mResereved1 = reader.ReadChars(mResereved1.Length)

            mCloseType = reader.ReadInt16()
            mReserved6 = reader.ReadInt16()
        End Sub

        Public Sub Reset()
            mInstrumentCode = 0
            mHigh = 0
            mLow = 0
            mOpen = 0
            mClose = 0
            mIndex = 0
        End Sub

        Public Sub PrintValues(pHeader As CBseIndexMsgHeader)
            Dim indexIdStr As String = If(mIndexID IsNot Nothing, New String(mIndexID), "null")
            Dim reserved1Str As String = If(mResereved1 IsNot Nothing, New String(mResereved1), "null")

            Console.WriteLine(
                "InstrumentCode={0}, High={1}, Low={2}, Open={3}, Close={4}, Index={5}, " &
                "IndexID={6}, Reserved1={7}, " &
                "CloseType={8}, Reserved6={9}",
                mInstrumentCode, mHigh, mLow, mOpen, mClose, mIndex,
                indexIdStr, reserved1Str,
                mCloseType, mReserved6
            )
        End Sub

        Public Sub WriteToInstrumentFile(pFolderPath As String, pHeader As CBseIndexMsgHeader)
            ' Ensure folder exists
            Directory.CreateDirectory(pFolderPath)

            Dim indexIdStr As String = New String(mIndexID)
            indexIdStr = indexIdStr.Substring(0, indexIdStr.Length - 1)
            ' Create file name based on InstrumentCode
            Dim filePath As String = Path.Combine(pFolderPath,
                String.Format("Code_{0}_{1}_{2}_{3}_{4}.csv",
                    mInstrumentCode, indexIdStr, mToday.Year, mToday.Month, mToday.Day))

            ' Format timestamp from header
            Dim timestamp As String = String.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                pHeader.mHour, pHeader.mMinute, pHeader.mSecond, pHeader.mMillisecond)

            ' Check if file exists -> add header if new
            If Not File.Exists(filePath) Then
                Dim headerLine As String = "Time,InstrumentCode,High,Low,Open,Close,Index"
                File.WriteAllText(filePath, headerLine + Environment.NewLine)
            End If

            ' Format line
            Dim line As String = String.Format("{0},{1},{2},{3},{4},{5},{6}",
                timestamp, mInstrumentCode, mHigh, mLow, mOpen, mClose, mIndex)

            ' Append line
            File.AppendAllText(filePath, line + Environment.NewLine)
        End Sub
    End Class

