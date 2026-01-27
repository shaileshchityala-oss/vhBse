Imports System
Imports System.IO
Imports System.IO.Compression
Public Class CZip
    Public Shared Function ZipExtract(pZipFilePath As String, pExtractpath As String) As Boolean

        Dim checkfile As Boolean = File.Exists(pZipFilePath)
        Dim checkfold As Boolean = File.Exists(pExtractpath)

        If checkfile Then
            ZipFile.ExtractToDirectory(pZipFilePath, pExtractpath)
            Return True
        End If
        Return False
    End Function

    Public Shared Sub ExtractZipWithOverwrite(pZipFilePath As String, pExtractPath As String)
        ' Ensure extract folder exists
        If Not Directory.Exists(pExtractPath) Then
            Directory.CreateDirectory(pExtractPath)
        End If

        Using archive As ZipArchive = ZipFile.OpenRead(pZipFilePath)
            For Each entry As ZipArchiveEntry In archive.Entries
                Dim destinationPath As String = Path.Combine(pExtractPath, entry.FullName)

                ' Ensure directory exists for this entry
                Dim destDir As String = Path.GetDirectoryName(destinationPath)
                If Not Directory.Exists(destDir) Then
                    Directory.CreateDirectory(destDir)
                End If

                ' Skip directories
                If Not String.IsNullOrEmpty(entry.Name) Then
                    ' Delete existing file to simulate overwrite
                    If File.Exists(destinationPath) Then
                        File.Delete(destinationPath)
                    End If
                    ' Extract file
                    Using entryStream As Stream = entry.Open()
                        Using fileStream As FileStream = File.Create(destinationPath)
                            entryStream.CopyTo(fileStream)
                        End Using
                    End Using
                End If
            Next
        End Using
    End Sub

End Class