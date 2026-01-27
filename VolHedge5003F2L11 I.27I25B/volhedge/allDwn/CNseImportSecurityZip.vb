Imports System.Windows.Forms
Imports System.IO
Imports System.Threading.Tasks
Public Class CNseImportSecurityZip
    Public mDnMrg As CDownloadManager
    Public mContext As CNetFileDownloadContext
    Public objIO As ImportData.ImportOperation
    Sub New()
        mContext = New CNetFileDownloadContext("", "", "")
        Dim fileName As String = "security.zip"
        mContext.mDownIDKey = "NseSecuityZip"
        mContext.mUrl = Convert.ToString("https://support.finideas.com/contract/") & fileName
        mContext.mDownloadFolder = Application.StartupPath + "\Contract"
        mContext.mDownloadFile = fileName
        objIO = New ImportData.ImportOperation()
    End Sub
    Public Function PreProcess()
        mContext.mIsSuccessPreProcess = False
        mDnMrg.CheckAndDeleteFile(mContext.mDownloadFolder, mContext.mDownloadFile)
        If mDnMrg.CheckRemoteFileExists(mContext.mUrl) Then
            mContext.mIsSuccessPreProcess = True
        End If
    End Function

    Public mLabel As Label
    Public Async Function DownloadProcess(pLbl As Label) As Task(Of Boolean)

        If Not mContext.mIsSuccessPreProcess Then
            MessageBox.Show(mContext.mErrorMessage)
        End If
        mContext.mIsSuccessDownload = False
        mLabel = pLbl
        Try
            Dim fileDest As String = mContext.mDownloadFolder + "\" + mContext.mDownloadFile
            mContext.mDownloadedFilePath = fileDest
            mDnMrg.EnqueueDownload(mContext.mDownIDKey, mContext.mUrl, fileDest, pLbl)
            Dim res As DownloadResult = Nothing
            While Not mDnMrg.TryGetResult(mContext.mDownIDKey, res)
                Await Task.Delay(500)
            End While

            mContext.mIsSuccessDownload = True
            Return True
        Catch ex As Exception
            mContext.mIsSuccessDownload = False
            Return False
        End Try
    End Function
    Public Function PostProcess() As Boolean
        If Not mContext.mIsSuccessPreProcess Then
            MessageBox.Show(mLabel.Text)
            Return False
        End If
        If File.Exists(mContext.mDownloadedFilePath) Then
            CZip.ExtractZipWithOverwrite(mContext.mDownloadedFilePath, mContext.mDownloadFolder)
            mLabel.Text = "Extract Done."

            Dim contractFile As String = mContext.mDownloadFolder + "\security.txt"
            If File.Exists(contractFile) Then
                If import_Data.SecurityImport(contractFile, objIO) = True Then
                    fill_token()
                    mLabel.Text = "File Imported Successfully."
                Else
                    mLabel.Text = "File Not Processed."
                End If
            Else
                mLabel.Text = "Contract File Not Found"
            End If
        End If
        Try
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Async Function DoAll(pLbl As Label) As Task
        mLabel = pLbl
        mLabel.Text = ""
        mLabel.ForeColor = Color.Black
        Me.PreProcess()
        System.Threading.Thread.Sleep(200)
        Await Me.DownloadProcess(pLbl)
        System.Threading.Thread.Sleep(100)
        Me.PostProcess()
        mLabel.ForeColor = Color.Green
        System.Threading.Thread.Sleep(100)

    End Function

End Class

