Public Class DownloadRangeCalc
    Dim ClsLoginData1 As New ClsLoginData
    Dim objtrading As New trading
    Public Shared chkfindRange1 As Boolean

    Private Sub DownloadRangeCalc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        chkfindRange1 = True
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DownloadRangeCalc_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        chkfindRange1 = False
    End Sub

    Private Sub DownloadRangeCalc_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        chkfindRange1 = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub cmdProcessData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdProcessData.Click
        Try

        
        lblMessage.Text = ""
        Me.Cursor = Cursors.WaitCursor
        Dim dtsql, dtaccess As New DataTable()
        Dim dt_insert, dt_update As New DataTable()
            If RegServerIP.Trim = "" Then
                SetRegServer()
            End If
            dtsql = ClsLoginData1.Select_Range_Data(dtpFromDate.Value, dtpToDate.Value)
            dtaccess = objtrading.Select_tblRangeData()
            dt_update = dtsql.Clone
            dt_insert = dtsql.Clone

            For Each drow As DataRow In dtsql.Select
                If (dtaccess.Select("BDate = '" & drow("Date") & "' And Symbol ='" & drow("Symbol") & "'").Length > 0) Then
                    dt_update.ImportRow(drow)
                Else
                    dt_insert.ImportRow(drow)
                End If
            Next
            If (dt_insert.Rows.Count > 0) Then
                objtrading.Insert_tblRangeData(dt_insert)
            End If
            If (dt_update.Rows.Count > 0) Then
                objtrading.Update_tblRangeData(dt_update)
            End If
            lblMessage.Text = "Data Downloaded Successfully!!"
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

End Class