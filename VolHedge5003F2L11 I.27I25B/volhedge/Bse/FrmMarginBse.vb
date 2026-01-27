Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Diagnostics

Public Class FrmMarginBse

    Public mNseSr As CSpanReader
    Public mBseSr As CSpanReader

    Protected Overrides Sub OnLoad(e As EventArgs)

        Me.TopMost = True
        clsGlobal.mFrmExchangeMargin = Me

        If clsGlobal.mNseSr Is Nothing Then
            clsGlobal.mNseSr = New CSpanReader()
        End If

        If clsGlobal.mBseSr Is Nothing Then
            clsGlobal.mBseSr = New CSpanReader()

        End If

        mNseSr = clsGlobal.mNseSr
        mBseSr = clsGlobal.mBseSr

        mDtTrades = maintable
        mNseSr.mCurTable = maintable
        mBseSr.mCurTable = maintable

        MyBase.OnLoad(e)
    End Sub
    Public mDtTrades As DataTable
    Public mSymbol As String


    Public Async Function ProcessBse() As Task

        ' mDtTrades = analysis.currtable
        mDtTrades = maintable

        clsGlobal.mBseSr = New CSpanReader()
        mBseSr = clsGlobal.mBseSr
        mBseSr.mCurTable = CUtils.GetFilteredTable(mDtTrades, "exchange='BSE' AND CP<>'E'")
        mBseSr.mMainTable = mBseSr.mCurTable
        If Not mBseSr.mCurTable.Rows.Count > 0 Then
            mBseSr.mCurTable.Dispose()
            Return
        End If

        Dim spanSrcPath As String = "BSERISK" + Now.Year.ToString() + Now.Month.ToString("00") + Now.Day.ToString("00") + "-00.spn"
        'Dim spanSrcPath As String = "BSERISK" + Now.Year.ToString() + Now.Month.ToString("00") + "24-00.spn"

        ' spanSrcPath = "BSERISK20251209-00.spn"
        mBseSr.mSPAN_path = SPAN_PATH

        mBseSr.mExchaneSpanFilePath = spanSrcPath

        Dim spnFileLoc As String = mBseSr.mSPAN_path + "\" + mBseSr.mExchaneSpanFilePath

        If Not File.Exists(spnFileLoc) Then
            MessageBox.Show("BSE Exchange span file not found" + vbNewLine + "Download to " + SPAN_PATH)
            Return
        End If

        mBseSr.mExchange = "BSE"
        mBseSr.mHtBseComp = Nothing
        btnBSE.Enabled = False
        btnBSE.Text = "Processing"

        Await mBseSr.generate_SPAN_data_BSE(mBseSr.mExchange, mBseSr.mExchaneSpanFilePath)
        mBseSr.cal_exp_Margin(analysis.compname, analysis.TabStrategy.SelectedTab.Text)

        txBseTotal.Text = mBseSr.mTotalValue.ToString("0.00")
        txtBseExp.Text = mBseSr.mDouExpMrg.ToString()
        txtBseInit.Text = mBseSr.mDouIntMrg.ToString()
        btnBSE.Enabled = True
        btnBSE.Text = "BSE"


    End Function

    Public Async Function ProcessNse() As Task

        mDtTrades = maintable

        If mDtTrades Is Nothing Then
            Return
        End If

        mNseSr.mCurTable = CUtils.GetFilteredTable(mDtTrades, "exchange='NSE' AND CP<>'E' ")

        If Not mNseSr.mCurTable.Rows.Count > 0 Then
            mNseSr.mCurTable.Dispose()
            Return
        End If

        mNseSr.mMainTable = mNseSr.mCurTable

        btnNSE.Enabled = False
        btnNSE.Text = "Processing"

        mNseSr.mExchaneSpanFilePath = ""
        mNseSr.mExchange = "NSE"
        mNseSr.generate_SPAN_data_NSE()
        mNseSr.cal_exp_Margin(analysis.compname, analysis.TabStrategy.SelectedTab.Text)
        txtNseTotal.Text = mNseSr.mTotalValue.ToString("0.00")
        txtNseExp.Text = mNseSr.mDouExpMrg.ToString()
        txtNseInit.Text = mNseSr.mDouIntMrg.ToString()

        btnNSE.Enabled = True
        btnNSE.Text = "NSE"

        'txtNseTotal.Text = mNseSr.mTotalValue.ToString("0.00")
    End Function
    Private Async Sub btnNSE_Click(sender As Object, e As EventArgs) Handles btnNSE.Click

        Await ProcessNse()
        analysis.cal_exp_Margin()
        'mDtTrades = analysis.currtable

        'If mDtTrades Is Nothing Then
        '    Return
        'End If

        ''mNseSr.mCurTable = maintable
        ''mNseSr.mMainTable = maintable

        'mNseSr.mCurTable = CUtils.GetFilteredTable(mDtTrades, "exchange='NSE'")
        'mNseSr.mMainTable = mNseSr.mCurTable

        'mNseSr.mExchaneSpanFilePath = ""
        'mNseSr.mExchange = "NSE"
        'mNseSr.generate_SPAN_data_NSE()
        'mNseSr.cal_exp_Margin(analysis.compname, analysis.TabStrategy.SelectedTab.Text)

        ''        mNseSr.cal_exp_Margin("PAGEIND", "NOV", "ALL")
        'txtNseTotal.Text = mNseSr.mTotalValue.ToString("0.00")

    End Sub

    Private Async Sub btnBSE_Click(sender As Object, e As EventArgs) Handles btnBSE.Click

        Await ProcessBse()
        'If analysis.Visible = True Then
        analysis.cal_exp_Margin()
        'End If


        'mDtTrades = analysis.currtable

        'mBseSr = New CSpanReader()
        'mBseSr.mCurTable = CUtils.GetFilteredTable(mDtTrades, "exchange='BSE'")
        'mBseSr.mMainTable = mBseSr.mCurTable
        'If Not mBseSr.mCurTable.Rows.Count > 0 Then
        '    mBseSr.mCurTable.Dispose()
        '    Return
        'End If

        'Dim spanSrcPath As String = "BSERISK" + Now.Year.ToString() + Now.Month.ToString("00") + Now.Day.ToString("00") + "-00.spn"
        ''Dim spanSrcPath As String = "BSERISK" + Now.Year.ToString() + Now.Month.ToString("00") + "24-00.spn"

        '' spanSrcPath = "BSERISK20251209-00.spn"
        'mBseSr.mSPAN_path = SPAN_PATH

        'mBseSr.mExchaneSpanFilePath = spanSrcPath

        'Dim spnFileLoc As String = mBseSr.mSPAN_path + "\" + mBseSr.mExchaneSpanFilePath

        'If Not File.Exists(spnFileLoc) Then
        '    MessageBox.Show("BSE Exchange span file not found" + vbNewLine + "Download to " + SPAN_PATH)
        '    Return
        'End If

        'mBseSr.mExchange = "BSE"
        'mBseSr.mHtBseComp = Nothing
        'btnBSE.Enabled = False
        'btnBSE.Text = "Processing"

        'Await mBseSr.generate_SPAN_data_BSE(mBseSr.mExchange, mBseSr.mExchaneSpanFilePath)
        'mBseSr.cal_exp_Margin(analysis.compname, analysis.TabStrategy.SelectedTab.Text)

        'txBseTotal.Text = mBseSr.mTotalValue.ToString("0.00")
        'txtBseExpMargin.Text = mBseSr.mDouExpMrg.ToString()
        'txtBseInitMargin.Text = mBseSr.mDouIntMrg.ToString()
        'btnBSE.Enabled = True
        'btnBSE.Text = "BSE"
    End Sub

    Private Async Sub btnProcessAll_Click(sender As Object, e As EventArgs) Handles btnProcessAll.Click

        btnBSE.Enabled = False
        btnNSE.Enabled = False
        btnProcessAll.Enabled = False

        Try
            Await ProcessNse()
            Await ProcessBse()

        Catch ex As Exception

        End Try


        btnBSE.Enabled = True
        btnNSE.Enabled = True
        btnProcessAll.Enabled = True

        CalcAllValue()

    End Sub

    Private Sub CalcAllValue()

        Dim dblNseInit As Double = CUtils.StringToDouble(txtNseInit.Text)
        Dim dblNseExp As Double = CUtils.StringToDouble(txtNseExp.Text)

        Dim dblBseInit As Double = CUtils.StringToDouble(txtBseInit.Text)
        Dim dblBseExp As Double = CUtils.StringToDouble(txtBseExp.Text)

        Dim dblAllInit As Double = dblNseInit + dblBseInit
        Dim dblAllExp As Double = dblNseExp + dblBseExp
        Dim dblAllTotal As Double = (dblAllInit + dblAllExp) / 100000

        txtAllInit.Text = dblAllInit.ToString("0.00")
        txtAllExp.Text = dblAllExp.ToString("0.00")
        txtAllTotal.Text = dblAllTotal.ToString("0.00")
    End Sub

    Public Sub btnGetCurComp_Click(sender As Object, e As EventArgs) Handles btnGetCurComp.Click
        CalcSymbol()

    End Sub

    Public Sub CalcSymbol()
        If mNseSr.mCurTable Is Nothing Then
            Return
        End If

        If mBseSr.mCurTable Is Nothing Then
            Return
        End If

        mNseSr.cal_exp_Margin(analysis.compname, analysis.TabStrategy.SelectedTab.Text)
        txtNseTotal.Text = mNseSr.mTotalValue.ToString("0.00")
        txtNseExp.Text = mNseSr.mDouExpMrg.ToString()
        txtNseInit.Text = mNseSr.mDouIntMrg.ToString()


        mBseSr.cal_exp_Margin(analysis.compname, analysis.TabStrategy.SelectedTab.Text)
        txBseTotal.Text = mBseSr.mTotalValue.ToString("0.00")
        txtBseExp.Text = mBseSr.mDouExpMrg.ToString()
        txtBseInit.Text = mBseSr.mDouIntMrg.ToString()

        CalcAllValue()
    End Sub

    Private Sub FrmMarginBse_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        clsGlobal.mFrmExchangeMargin = Nothing
    End Sub

    Private Sub chkOnTop_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnTop.CheckedChanged
        Me.TopMost = chkOnTop.Checked
    End Sub
End Class