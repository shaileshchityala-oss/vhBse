Imports System
Imports System.Diagnostics
Imports System.Windows.Forms
Imports System.Collections
Imports System.ComponentModel
Imports System.Configuration.Install
Imports System.Reflection
Imports System.IO

Namespace OffLine.Installer
    ' Taken from:http://msdn2.microsoft.com/en-us/library/
    ' system.configuration.configurationmanager.aspx
    ' Set 'RunInstaller' attribute to true.

    <RunInstaller(True)> _
    Public Class InstallerClass
        Inherits System.Configuration.Install.Installer
        Public Sub New()
            MyBase.New()
            AddHandler Me.Committed, AddressOf MyInstaller_Committed
            ' Attach the 'Committed' event.
            ' Attach the 'Committing' event.
            AddHandler Me.Committing, AddressOf MyInstaller_Committing
        End Sub

        ' Event handler for 'Committing' event.
        Private Sub MyInstaller_Committing(ByVal sender As Object, ByVal e As InstallEventArgs)
            'Console.WriteLine("");
            'Console.WriteLine("Committing Event occurred.");
            'Console.WriteLine("");
        End Sub

        ' Event handler for 'Committed' event.
        Private Sub MyInstaller_Committed(ByVal sender As Object, ByVal e As InstallEventArgs)
            Try
                MsgBox("Software Installed.")
                'If (MsgBox("Do you want to start Software?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes) Then

                '    Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                '    Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\VolHedge.exe")
                'End If
                ' Do nothing... 
            Catch
            End Try
        End Sub

        ' Override the 'Install' method.
        Public Overloads Overrides Sub Install(ByVal savedState As IDictionary)
            MyBase.Install(savedState)
        End Sub

        ' Override the 'Commit' method.
        Public Overloads Overrides Sub Commit(ByVal savedState As IDictionary)
            MyBase.Commit(savedState)
        End Sub

        ' Override the 'Rollback' method.
        Public Overloads Overrides Sub Rollback(ByVal savedState As IDictionary)
            MyBase.Rollback(savedState)
        End Sub

        Private Sub InstallerClass_AfterUninstall(ByVal sender As Object, ByVal e As System.Configuration.Install.InstallEventArgs) Handles Me.AfterUninstall
            'If (MsgBox("Are you sure you want to delete?..............", MsgBoxStyle.YesNo) = MsgBoxResult.Yes) Then
            '    Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            '    Directory.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            'End If
        End Sub
    End Class
End Namespace
