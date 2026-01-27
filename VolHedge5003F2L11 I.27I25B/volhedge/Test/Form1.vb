' ---------------------------------------------------------------------------
' Campari Software
'
' Form1.cs
'
' Summary description for Form1.cs
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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Net
Imports UpdaterCore
Imports System.Diagnostics
Imports System.IO
Namespace Tester
	#Region "namespace references"

	#End Region

	#Region "Form1 class"
	Public Partial Class Form1
		Inherits Form
		#Region "events"

		#End Region

		#Region "class-wide fields"
		Private downloader As FileDownloader

		#End Region

		#Region "private and internal properties and methods"

		#Region "properties"

		#End Region

		#Region "methods"

		#Region "button1_Click"
		Private Sub button1_Click(sender As Object, e As EventArgs)
			Dim strOldExeName As [String] = "FinMarketWatch" & DateTime.Today.[Date].ToString("yyyyMMdd") & ".exe"
			'''///////=====================
			'http://www.finideas.com/FinIdeasBackUp/FinMarketWatch.exe

			'Process[] runningProcesses = Process.GetProcesses();
			'foreach (Process process in runningProcesses)
			'{
			'    // now check the modules of the process
			'    foreach (ProcessModule module in process.Modules)
			'    {
			'        if (module.FileName.Equals("FinMarketWatch.exe"))
			'        {
			'            process.Kill();
			'        }
			'    }
			'}


			Dim localByName As Process() = Process.GetProcessesByName("FinMarketWatch")
			For Each p As Process In localByName
				p.Kill()
			Next

			'''//////======================

			'if (Directory.Exists("OLDExe") == false)
			'{
			'    //MessageBox.Show("Not Exist");
			'    Directory.CreateDirectory("OLDExe");

			'}

			If File.Exists("FinMarketWatch.exe") = True Then
				'MessageBox.Show("Exist");
				File.Move("FinMarketWatch.exe", strOldExeName)
			End If

			Dim response As String = [String].Empty
			Try
				textBox6.Text = [String].Empty
				textBox6.Refresh()

				downloader = New FileDownloader()

				downloader.DownloadCompleted += New EventHandler(Of FileDownloadCompletedEventArgs)(AddressOf downloader_DownloadCompleted)
				downloader.DownloadProgressChanged += New EventHandler(Of FileDownloadProgressChangedEventArgs)(AddressOf downloader_DownloadProgressChanged)
				downloader.DownloadStatusChanged += New EventHandler(Of FileDownloadStatusChangedEventArgs)(AddressOf downloader_DownloadStatusChanged)

				downloader.Download(textBox1.Text)
			Catch ex As Exception
				textBox6.Text += ex.Message & System.Environment.NewLine
					'strOldExeName
				File.Move(strOldExeName, "FinMarketWatch.exe")
			End Try
		End Sub
		#End Region

		#Region "button2_Click"
		Private Sub button2_Click(sender As Object, e As EventArgs)
			Dim response As String = [String].Empty
			Try
				textBox6.Text = [String].Empty
				textBox6.Refresh()

				downloader = New FileDownloader()

				downloader.DownloadCompleted += New EventHandler(Of FileDownloadCompletedEventArgs)(AddressOf downloader_DownloadCompleted)
				downloader.DownloadProgressChanged += New EventHandler(Of FileDownloadProgressChangedEventArgs)(AddressOf downloader_DownloadProgressChanged)
				downloader.DownloadStatusChanged += New EventHandler(Of FileDownloadStatusChangedEventArgs)(AddressOf downloader_DownloadStatusChanged)

				downloader.DownloadAsync(textBox1.Text)
			Catch ex As Exception
				textBox6.Text += ex.Message & System.Environment.NewLine
			End Try
		End Sub
		#End Region

		#Region "button3_Click"
		Private Sub button3_Click(sender As Object, e As EventArgs)
			downloader.DownloadAsyncCancel()
		End Sub
		#End Region

		#End Region

		#Region "downloader_DownloadStatusChanged"
		Private Sub downloader_DownloadStatusChanged(sender As Object, e As FileDownloadStatusChangedEventArgs)
			textBox6.Text += Convert.ToString(e.Message) & System.Environment.NewLine
			textBox6.Refresh()
		End Sub
		#End Region

		#Region "downloader_DownloadCompleted"
		Private Sub downloader_DownloadCompleted(sender As Object, e As FileDownloadCompletedEventArgs)
			Me.progressBar1.Value = 0
			If e.Canceled Then
				textBox6.Text += "Download cancelled." & System.Environment.NewLine
			Else
				textBox6.Text += "Download complete." & System.Environment.NewLine
				textBox6.Text += "Downloaded to: " & Convert.ToString(e.Result) & System.Environment.NewLine
			End If

			If e.[Error] IsNot Nothing Then
				textBox6.Text += Convert.ToString(e.[Error].Message) & System.Environment.NewLine
			End If
			textBox6.Refresh()
		End Sub
		#End Region

		#Region "downloader_DownloadProgressChanged"
		Private Sub downloader_DownloadProgressChanged(sender As Object, e As FileDownloadProgressChangedEventArgs)
			Me.progressBar1.Value = e.ProgressPercentage
		End Sub
		#End Region

		#End Region

		#End Region

		#Region "public properties and methods"

		#Region "properties"

		#End Region

		#Region "methods"

		#Region "constructor"
		''' <summary>
		''' Default constructor for Form1
		''' </summary>
		Public Sub New()
			InitializeComponent()
		End Sub
		#End Region

		Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs)
			Try
				Process.Start("FinMarketWatch.exe")
			Catch generatedExceptionName As Exception
					'throw;
				MessageBox.Show("Now You Can Start  Marketwatch.")
			End Try

		End Sub

		#End Region
	End Class
	#End Region
End Namespace
