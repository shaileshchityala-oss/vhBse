' ---------------------------------------------------------------------------
' Campari Software
'
' FileDownloadStatusChangedEventArgs.cs
'
' Provides the delegate and associated derived EventArgs class used to provide
' the calling code a way to specify the callback to be used when the
' SpursStatusChanged event.
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
Imports System.Globalization

Namespace UpdaterCore
#Region "class FileDownloadStatusChangedEventArgs"
    ''' <summary>
    ''' Provides data for the <see cref="FileDownloader.DownloadStatusChanged"/> event.
    ''' </summary>
    Public Class FileDownloadStatusChangedEventArgs
        Inherits EventArgs
#Region "events"

#End Region

#Region "class-wide fields"
        Private m_message As String

#End Region

#Region "private and internal properties and methods"

#Region "properties"

#End Region

#Region "methods"

#Region "constructor"
        Friend Sub New(ByVal message As String)
            MyBase.New()
            Me.m_message = message
        End Sub
#End Region

#End Region

#End Region

#Region "public properties and methods"

#Region "properties"

#Region "Message"
        ''' <summary>
        ''' Gets the status message.
        ''' </summary>
        ''' <value>A <see cref="String"/> value indicating the status message.</value>
        Public ReadOnly Property Message() As String
            Get
                Return m_message
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
