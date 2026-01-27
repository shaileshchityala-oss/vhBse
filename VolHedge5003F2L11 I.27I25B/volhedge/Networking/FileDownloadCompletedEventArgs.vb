' ---------------------------------------------------------------------------
' Campari Software
'
' FileDownloadCompletedEventArgs.cs
'
' Provides the delegate and associated derived EventArgs class used to provide
' the calling code a way to specify the callback to be used when the
' DownloadDataCompleted event.
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
Imports System.ComponentModel

Namespace UpdaterCore
#Region "class FileDownloadCompletedEventArgs"
    ''' <summary>
    ''' Provides data for the <see cref="FileDownloader.DownloadCompleted"/> event.
    ''' </summary>
    Public Class FileDownloadCompletedEventArgs
        Inherits EventArgs
#Region "events"

#End Region

#Region "class-wide fields"
        Private m_canceled As Boolean
        Private m_error As Exception
        Private m_result As String
#End Region

#Region "private and internal properties and methods"

#Region "properties"

#End Region

#Region "methods"

#Region "constructor"
        Friend Sub New(ByVal result As String, ByVal [error] As Exception, ByVal canceled As Boolean)
            MyBase.New()
            Me.m_result = result
            Me.m_error = [error]
            Me.m_canceled = canceled
        End Sub
#End Region

#End Region

#End Region

#Region "public properties and methods"

#Region "properties"

#Region "Canceled"
        ''' <summary>
        ''' Gets a value indicating if the download operation was cancelled.
        ''' </summary>
        ''' <value><see langword="true"/> if the download was cancelled; otherwise,
        ''' <see langword="false"/>.</value>
        Public ReadOnly Property Canceled() As Boolean
            Get
                Return Me.m_canceled
            End Get
        End Property
#End Region

#Region "Error"
        ''' <summary>
        ''' Gets a value indicating which error occurred during a download
        ''' operation.
        ''' </summary>
        ''' <value>The <see cref="Exception"/> that occurred.</value>
        Public ReadOnly Property [Error]() As Exception
            Get
                Return Me.m_error
            End Get
        End Property
#End Region

#Region "Result"
        ''' <summary>
        ''' Gets a value indicating the destination file for the download
        ''' operation.
        ''' </summary>
        ''' <value>A <see cref="String"/> representing the destination file.</value>
        Public ReadOnly Property Result() As String
            Get
                Return Me.m_result
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
