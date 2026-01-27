' ---------------------------------------------------------------------------
' Campari Software
'
' FileDownloadProgressChangedEventArgs.cs
'
' Provides the delegate and associated derived EventArgs class used to provide
' the calling code a way to specify the callback to be used when the
' DownloadProgressChanged event.
'
' ---------------------------------------------------------------------------
' Copyright (C) 2006-2007 Campari Software
' All rights reserved.
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
' FITNESS FOR A PARTICULAR PURPOSE.
' ---------------------------------------------------------------------------

Namespace UpdaterCore
#Region "class FileDownloadProgressChangedEventArgs"
    ''' <summary>
    ''' Provides data for the <see cref="FileDownloader.DownloadProgressChanged"/> event. 
    ''' </summary>
    Public Class FileDownloadProgressChangedEventArgs
        Inherits EventArgs
#Region "events"

#End Region

#Region "class-wide fields"
        Private m_bytesReceived As Long
        Private m_totalBytesToReceive As Long
        Private m_progressPercentage As Integer

#End Region

#Region "private and internal properties and methods"

#Region "properties"

#End Region

#Region "methods"

#Region "constructor"
        Friend Sub New(ByVal bytesReceived As Long, ByVal totalBytesToReceive As Long)
            MyBase.New()
            Me.m_bytesReceived = bytesReceived
            Me.m_totalBytesToReceive = totalBytesToReceive
            Me.m_progressPercentage = CInt(Math.Truncate((CDbl(bytesReceived) / totalBytesToReceive) * 100))
        End Sub
#End Region

#End Region

#End Region

#Region "public properties and methods"

#Region "properties"

#Region "BytesReceived"
        ''' <summary>
        ''' Gets the number of bytes received.
        ''' </summary>
        ''' <value>An <see cref="Int64"/> value that indicates the number of bytes received.</value>
        ''' <remarks>To determine what percentage of the transfer has occurred, use the <see cref="ProgressPercentage"/> property.</remarks>
        Public ReadOnly Property BytesReceived() As Long
            Get
                Return Me.m_bytesReceived
            End Get
        End Property
#End Region

#Region "TotalBytesToReceive"
        ''' <summary>
        ''' Gets the total number of bytes to be received. 
        ''' </summary>
        ''' <value>An <see cref="Int64"/> value that indicates the number of bytes that will be received.</value>
        ''' <remarks>To determine the number of bytes already received, use the <see cref="BytesReceived"/> property.
        ''' To determine what percentage of the transfer has occurred, use the <see cref="ProgressPercentage"/> property.
        ''' </remarks>
        Public ReadOnly Property TotalBytesToReceive() As Long
            Get
                Return Me.m_totalBytesToReceive
            End Get
        End Property
#End Region

#Region "ProgressPercentage"
        ''' <summary>
        ''' Gets the task progress percentage. 
        ''' </summary>
        ''' <value>A percentage value indicating the task progress.</value>
        ''' <remarks>The <see cref="ProgressPercentage"/> property determines what percentage of a task has been completed.</remarks>
        Public ReadOnly Property ProgressPercentage() As Integer
            Get
                Return Me.m_progressPercentage
            End Get
        End Property
#End Region

#End Region

#Region "methods"

#End Region

#End Region
    End Class
#End Region
End Namespace
