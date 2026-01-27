Imports System.Threading
Imports System.Threading.Tasks

Public Class FrmDnMgr
    Dim ObjIO As ImportData.ImportOperation
    Dim mFoContractNew As CNseImportContractZip
    Dim mSecurityContractNew As CNseImportSecurityZip
    Dim mBhavCopyV2 As CNseImportBhavcopyV2

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        mBhavCopyV2 = New CNseImportBhavcopyV2()
        mBhavCopyV2.mDnMrg = clsGlobal.mDownloadMgr
        Await mBhavCopyV2.DoAll(lbl3)
        System.Threading.Thread.Sleep(100)

        Return
        mFoContractNew = New CNseImportContractZip()
        mFoContractNew.mDnMrg = clsGlobal.mDownloadMgr
        Await mFoContractNew.DoAll(lbl1)
        System.Threading.Thread.Sleep(100)
        mSecurityContractNew = New CNseImportSecurityZip()
        mSecurityContractNew.mDnMrg = clsGlobal.mDownloadMgr
        Await mSecurityContractNew.DoAll(lbl2)
        Return


        'Dim strUrl As String = "https://support.finideas.com/contract/contract.zip"
        ''Dim strUrl As String = "https://support.finideas.com/Bhavcopy/BhavCopy_NSE_FO_0_0_0_20251017_F_0000.csv.zip"

        'mgr.EnqueueDownload("file1", strUrl, "C:\download\big1.zip", lbl1, 60)
        'Dim res As DownloadResult = Nothing
        'While Not mgr.TryGetResult("file1", res)
        '    Await Task.Delay(500)
        'End While

        'mgr.EnqueueDownload("file2", strUrl, "C:\download\big2.zip", lbl2, 60)
        'While Not mgr.TryGetResult("file2", res)
        '    Await Task.Delay(500)
        'End While

        'mgr.EnqueueDownload("file3", strUrl, "C:\download\big3.zip", lbl3, 60)

        'While Not mgr.TryGetResult("file3", res)
        '    Await Task.Delay(500)
        'End While

    End Sub

    'Dim mgr As CDownloadManager
    'Private Sub FrmDnMgr_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    mgr = New CDownloadManager(3)
    'End Sub
End Class