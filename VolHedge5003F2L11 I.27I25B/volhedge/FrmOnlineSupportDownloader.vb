Imports System.Threading.Tasks

Public Class FrmOnlineSupportDownloader

    Dim mDnMgrKey As String = "DnUltraViewer"
    Dim mDnMgr As CDownloadManager

    Protected Overrides Sub OnLoad(e As EventArgs)
        mDnMgr = clsGlobal.mDownloadMgr

        txtUrl.Text = Convert.ToString("https://www.ultraviewer.net/en/UltraViewer_setup_6.6_en.exe")
        txtUrl.ReadOnly = True
        lblProgress.Text = ""
        MyBase.OnLoad(e)
    End Sub

    Dim mFolderSelectionDlg As FolderBrowserDialog
    Dim flg As Boolean
    Private Async Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Await ProcessDn()
    End Sub

    Private Async Function ProcessDn() As Task
        If mFolderSelectionDlg Is Nothing Then
            mFolderSelectionDlg = New FolderBrowserDialog()
        End If
        If mFolderSelectionDlg.ShowDialog() = DialogResult.OK Then
            Dim exePath As String = mFolderSelectionDlg.SelectedPath + "\ultraviewer.exe"

            If Not mDnMgr.CheckAndDeleteFile(exePath) Then
                Return
            End If

            mDnMgr.EnqueueDownload(mDnMgrKey, txtUrl.Text, exePath, lblProgress)
            Dim res As DownloadResult = New DownloadResult()
            While Not mDnMgr.TryGetResult(mDnMgrKey, res)
                Await Task.Delay(500)
            End While

            System.Threading.Thread.Sleep(100)
            If res.Success Then
                System.Diagnostics.Process.Start(exePath)
                lblProgress.Text = "Successs  "
            Else
                lblProgress.Text = "failed to start"
            End If

        End If
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        txtUrl.ReadOnly = CheckBox1.Checked
    End Sub
End Class