Imports System
Imports System.Configuration
Imports System.IO
Imports System.Threading
Imports System.Net
'for export to excel
'Imports Microsoft.Office.Interop
Imports Microsoft.Office
Imports System.Runtime.InteropServices.Marshal
Imports Volhedge.OptionG
''' <summary>
''' analysis Class 
''' </summary>
''' <remarks></remarks>
Public Class analysis



    ''' <summary>
    ''' VarIsCurrency
    ''' </summary>
    ''' <remarks>This variable store whether selected company is Currency or Not</remarks>
    Dim VarIsCurrency As Boolean = False
    ''' <summary>
    ''' VarIsTabAddRemove
    ''' </summary>
    ''' <remarks>This variable check New tab Add process working</remarks>
    Dim VarIsTabAddRemove As Boolean = False
    ''' <summary>
    ''' Thr_CalculateDatatable
    ''' </summary>
    ''' <remarks>This Thread Execute to calculate Summary which assign to Analysis Summary Form grid</remarks>
    Dim Thr_CalculateDatatable As New Thread(AddressOf Cal_DeltaGammaVegaThetaSummary)


    ''' <summary>
    ''' VarDeltaval
    ''' </summary>
    ''' <remarks>This veriable store selected tab Delta value</remarks>
    Dim VarDeltaval As Double
    ''' <summary>
    ''' VarVegaval 
    ''' </summary>
    ''' <remarks>This veriable store selected tab Vega value</remarks>
    Dim VarVegaval As Double
    ''' <summary>
    ''' VarGammaval
    ''' </summary>
    ''' <remarks>This veriable store selected tab Gamma value</remarks>
    Dim VarGammaval As Double
    ''' <summary>
    ''' VarThetaval
    ''' </summary>
    ''' <remarks>This veriable store selected tab Theta value</remarks>
    Dim VarThetaval As Double
    ''' <summary>
    ''' VarVolgaval
    ''' </summary>
    ''' <remarks>This veriable store selected tab Volga value</remarks>
    Dim VarVolgaval As Double
    ''' <summary>
    ''' VarVannaval
    ''' </summary>
    ''' <remarks>This veriable store selected tab Vanna value</remarks>
    Dim VarVannaval As Double
    ''' <summary>
    ''' VarGrossmtm
    ''' </summary>
    ''' <remarks>This veriable store selected tab Gross MtoM value</remarks>
    Dim VarGrossmtm As Double
    ''' <summary>
    ''' VarDeltaRS
    ''' </summary>
    ''' <remarks>This veriable store selected tab Delta Val. in Rs. value</remarks>
    Dim VarDeltaRS As Double
    ''' <summary>
    ''' VarPicCnt
    ''' </summary>
    ''' <remarks>This veriable store Picture counter</remarks>
    Dim VarPicCnt As Integer ''


    REM For Store Margin Variable (For Show in PopUp Window) By Viral
    ''' <summary>
    ''' DouIntMrg
    ''' </summary>
    ''' <remarks>This veriable store Initial Margin</remarks>
    Dim DouIntMrg As Double ''
    ''' <summary>
    ''' DouExpMrg
    ''' </summary>
    ''' <remarks>This veriable store Exp. Margin</remarks>
    Dim DouExpMrg As Double ''
    '''' <summary>
    '''' DouEquity
    '''' </summary>
    '''' <remarks>This veriable store Equity</remarks>
    'Dim DouEquity As Double ''

    ''' <summary>
    ''' iLSize
    ''' By Viral
    ''' </summary>
    ''' <remarks>This veriable store LotSize And Update Value When Tab Change</remarks>
    Dim iDeltaLSize As Integer
    Dim iGammaLSize As Integer
    Dim iVegaLSize As Integer
    Dim iThetaLSize As Integer
    Dim iVolgaLSize As Integer
    Dim iVannaLSize As Integer

    Dim dGVColumnHeader As DGVColumnHeader
    Dim Thr_SpanCalc As Thread
#Region "Capture Screen" 'keval(19-2-10)
    Private Declare Function CreateDC Lib "gdi32" Alias "CreateDCA" (ByVal lpDriverName As String, ByVal lpDeviceName As String, ByVal lpOutput As String, ByVal lpInitData As String) As Integer
    Private Declare Function CreateCompatibleDC Lib "GDI32" (ByVal hDC As Integer) As Integer
    Private Declare Function CreateCompatibleBitmap Lib "GDI32" (ByVal hDC As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer) As Integer
    Private Declare Function GetDeviceCaps Lib "gdi32" Alias "GetDeviceCaps" (ByVal hdc As Integer, ByVal nIndex As Integer) As Integer
    Private Declare Function SelectObject Lib "GDI32" (ByVal hDC As Integer, ByVal hObject As Integer) As Integer
    Private Declare Function BitBlt Lib "GDI32" (ByVal srchDC As Integer, ByVal srcX As Integer, ByVal srcY As Integer, ByVal srcW As Integer, ByVal srcH As Integer, ByVal desthDC As Integer, ByVal destX As Integer, ByVal destY As Integer, ByVal op As Integer) As Integer
    Private Declare Function DeleteDC Lib "GDI32" (ByVal hDC As Integer) As Integer
    Private Declare Function DeleteObject Lib "GDI32" (ByVal hObj As Integer) As Integer
    Const SRCCOPY As Integer = &HCC0020
    Private Background As Bitmap
    Private fw, fh As Integer
#End Region
#Region "Delegate"

    Public Delegate Sub GDelegate_calcMarg(ByVal obj As Double, ByVal obj1 As Double)
    Dim obj_DelSetcalcMarg As New GDelegate_calcMarg(AddressOf calcMarg)

    Private Delegate Sub fut_rate(ByVal futrate As Double)
    Private Delegate Sub fut_rate1(ByVal futrate As Double)
    Private Delegate Sub fut_rate2(ByVal futrate As Double)
    Private Delegate Sub eq_rate(ByVal eqrate As Double)
    Private Delegate Sub dVal()
    Private Delegate Sub funrealize()
    Private Delegate Sub test()
    REM Because of Margin show in PopUp Window By Viral
    Private Delegate Sub intMarg(ByVal eqrate As Double)
    Private Delegate Sub expMarg(ByVal eqrate As Double)
    REM End
    Private Delegate Sub totMarg()

    Dim mval As dVal
    Dim munrealize As funrealize
    Dim mdel As fut_rate
    Dim mdel1 As fut_rate1
    Dim mdel2 As fut_rate2
    Dim meqdel As eq_rate
    Dim mtest As test
    REM Because of Margin show in PopUp Window By Viral
    Dim mintMarg As intMarg
    Dim mexpMarg As expMarg
    REM Because of Margin show in PopUp Window
    Dim mtotMarg As totMarg

    Dim vdel As Double
    Dim vdel1 As Double
    Dim vdel2 As Double
    Dim veqdel As Double


    Public Sub TempChangeTAB()
        If tbcomp.SelectedTab.Name <> compname Then
            tbcomp.SelectTab(compname)
        End If
    End Sub
    Private Sub fut(ByVal futrate As Double)
        vdel = futrate
        If VarIsCurrency = True Then
            txtrate.Text = Format(futrate, "##0.0000")
        Else
            txtrate.Text = Format(futrate, "##0.00")
        End If

    End Sub
    Private Sub fut1(ByVal futrate As Double)
        vdel1 = futrate
        If VarIsCurrency = True Then
            txtrate1.Text = Format(futrate, "##0.0000")
        Else
            txtrate1.Text = Format(futrate, "##0.00")
        End If

    End Sub
    Private Sub fut2(ByVal futrate As Double)
        vdel2 = futrate
        If VarIsCurrency = True Then
            txtrate2.Text = Format(futrate, "##0.0000")
        Else
            txtrate2.Text = Format(futrate, "##0.00")
        End If

    End Sub
    Private Sub eq(ByVal eqrate As Double)
        veqdel = eqrate
        txteqrate.Text = Format(eqrate, "##0.00")
    End Sub

    Private Sub ftest()
        lblmcount.Text = Val(lblmcount.Text) + 1
    End Sub
    Private Sub fintMarg(ByVal mar As Double)
        'DouIntMrg = mar
        txtintmrg.Text = mar
    End Sub
    Private Sub fexpMarg(ByVal mar As Double)
        'DouExpMrg = mar
        txtexpmrg.Text = mar
    End Sub
    Private Sub ftotMarg()
        'txttotmarg.Text = Math.Round((DouIntMrg + DouExpMrg + DouEquity) / 100000, 2)
        txttotmarg.Text = Math.Round((Val(txtintmrg.Text) + Val(txtexpmrg.Text) + Val(txtEquity.Text)) / 100000, 2)
    End Sub
    Dim date1 As Date
    Dim date2 As Date
    Dim date3 As Date
    Dim pan11 As Boolean
    Dim pan21 As Boolean
    Dim pan31 As Boolean
#End Region
#Region "Other variable"
    Public isRefresh As Boolean
    Public Shared chkanalysis As Boolean
    Dim tradeFileType As String
    Public chkpro As Boolean = False
    'Dim mObjData As New DataAnalysis.AnalysisData
    Dim objTrad As trading = New trading
    Dim objAna As New analysisprocess
    Dim objScr As script = New script
    Dim Objsql As SqlDbProcess = New SqlDbProcess

    Dim r As Integer


    Dim currtable As New DataTable
    Dim gcurrtable As New DataTable


    



    'Public multicastListener_focm As Socket

    Dim Mrateofinterast As Double = 0
    'Dim mbuyprice As New Hashtable
    'Dim msaleprice As New Hashtable
    'Dim mltpprice As New Hashtable


    'Dim mfbuyprice As New Hashtable
    'Dim mfsaleprice As New Hashtable
    'Dim mfltpprice As New Hashtable

    'Dim Currebuyprice As New Hashtable
    'Dim Curresaleprice As New Hashtable
    'Dim Curreltpprice As New Hashtable
    'Dim CurrequityVol As New Hashtable


    Dim futarray As New ArrayList
    Dim eqarray As New ArrayList
    Dim cparray As New ArrayList


   
    Dim scripttable As New DataTable

    Dim eqsecurity As New DataTable
    Public Shared comp_ana As New DataTable
    Public Shared compname As String
    Dim tbmo As String


    Dim selectedtab As New TabPage

    'for tcp client -start
    Dim localEndPoint As IPEndPoint

    'for tcp client -end

    

    Dim mdv As DataView
    Dim thrworking As Boolean = True
    Dim thrworking_future_only As Boolean = False
    Dim changeVal1 As Boolean = False

    Dim bSqlValidated As Boolean = True

    'Dim AlertOn As String
    Dim zero_analysis As String
    Dim objAl As New alertentry
    Dim altable As New DataTable
    'Dim malert As New DataTable
    Dim acomp As New DataTable
    Public refreshstarted As Boolean = False

    Dim alertcont As Label
    ' Dim mf5ref As Boolean = True
    Dim firstload As String
    Dim chknifty As Boolean = False

    Public flgSummary As Boolean = False
    Public flgcalsummarythrdstart As Boolean = False
    Shared ind As Integer = 0
    Dim ind1 As Integer = 0
    Dim totgmtm As Double

    Dim objval As Object
    Dim Maturity_Cur_month As Date
    Dim Maturity_first_month As Long
    Dim Maturity_second_month As Long
    Dim Maturity_third_month As Long
    Dim XResolution As Integer
    Dim Save_applied As Boolean = False
    Public Shared noPackage As Integer

    ''divyesh
    'Dim bhavcopy As DataTable
    Dim Objbhavcopy As New bhav_copy

    Dim STime As Date
    Dim ETime As Date

    Dim DtExpDiff As New DataTable

#End Region
#Region "Margin Variable"
    Dim mSPAN_path As String
    Dim mSPANFile_time1 As Date
    Dim mSPANFile_time2 As Date
    Dim mSPANFile_time3 As Date
    Dim mSPANFile_time4 As Date
    Dim mSPANFile_time5 As Date
    Dim mSPANFile_time6 As Date

    Dim mCurrent_SPAN_file As String
    Dim mCurrent_CurSPAN_file As String

    Public exp_latest_spn_file As String
    Public exp_latest_zip_file As String

    Public exp_latest_Curspn_file As String
    Public exp_latest_Curzip_file As String

    Public mTbl_exposure_comp As New DataTable
    Public mTbl_exposure_database As New DataTable
    Public mTbl_SPAN_output As New DataTable
    Public mTbl_span_calc As New DataTable

    Public FlgThr_Span As Boolean = False
#End Region

    ''' <summary>
    ''' analysis_Activated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to set Form ICON</remarks>
    Private Sub analysis_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.Icon = My.Resources.volhedge_icon
        ' searchcompany()

    End Sub
    ''' <summary>
    ''' analysis_FormClosing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to stop broadcast receiving and Save maintable into Analysis table in database</remarks>
    Private Sub analysis_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            objTrad.Uid = GdtSettings.Select("SettingName='LTP_PLUS_GAP'")(0).Item("Uid")
            objTrad.SettingName = "LTP_PLUS_GAP"
            objTrad.SettingKey = txtLtpVal.Text
            objTrad.Update_setting()

            'txtLtpVal1.Text = GdtSettings.Select("SettingName='LTP_MINUS_GAP'")(0).Item("SettingKey").ToString()
            objTrad.Uid = GdtSettings.Select("SettingName='LTP_MINUS_GAP'")(0).Item("Uid")
            objTrad.SettingName = "LTP_MINUS_GAP"
            objTrad.SettingKey = txtLtpVal1.Text
            objTrad.Update_setting()


            'txtVolVal1.Text = GdtSettings.Select("SettingName='VOL_PLUS_GAP'")(0).Item("SettingKey").ToString()
            objTrad.Uid = GdtSettings.Select("SettingName='VOL_PLUS_GAP'")(0).Item("Uid")
            objTrad.SettingName = "VOL_PLUS_GAP"
            objTrad.SettingKey = txtVolVal1.Text
            objTrad.Update_setting()

            'txtVolVal.Text = GdtSettings.Select("SettingName='VOL_MINUS_GAP'")(0).Item("SettingKey").ToString()
            objTrad.Uid = GdtSettings.Select("SettingName='VOL_MINUS_GAP'")(0).Item("Uid")
            objTrad.SettingName = "VOL_MINUS_GAP"
            objTrad.SettingKey = txtVolVal.Text
            objTrad.Update_setting()

            Try
                Try
                    If DGTrading.RowCount > 0 Then
                        Select Case (MsgBox("Do you want to save current Volatility?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question))
                            Case MsgBoxResult.Yes
                                REM SaveColumn Profile
                                Gsub_GridColProfileSave()
                                cmdsave_Click()
                                ReFresh_Maintable()
                                Call save_data(maintable)
                                chkanalysis = False
                                objanalysis = Nothing
                                refreshstarted = False
                                'server.Close()
                                If chkanalysis = False Then
                                    MDI.ToolStripcompanyCombo.Visible = False
                                    MDI.ToolStripMenuSearchComp.Visible = False
                                End If

                                If NetMode = "UDP" Then
                                    'Try
                                    '    'multicastListener_fo.Disconnect(False)
                                    '    If Not multicastListener_fo Is Nothing Then
                                    '        multicastListener_fo.Close()
                                    '        multicastListener_fo = Nothing
                                    '    End If
                                    '    If Not ThreadReceive_fo Is Nothing Then ThreadReceive_fo.Abort()
                                    '    BackgroundWorker1.Dispose()
                                    'Catch ex As Threading.ThreadAbortException
                                    '    Threading.Thread.ResetAbort()
                                    'End Try
                                    'Try
                                    '    'multicastListener_cm.Disconnect(False)
                                    '    If Not multicastListener_cm Is Nothing Then
                                    '        multicastListener_cm.Close()
                                    '        multicastListener_cm = Nothing
                                    '    End If
                                    '    If Not ThreadReceive_cm Is Nothing Then ThreadReceive_cm.Abort()
                                    'Catch ex As Threading.ThreadAbortException
                                    '    Threading.Thread.ResetAbort()
                                    'End Try
                                ElseIf NetMode = "TCP" Then
                                    'BackgroundWorker2.Dispose()
                                    Objsql.DeleteFoToken()
                                    Objsql.DeleteEqToken()
                                    Objsql.DeleteCurToken()
                                ElseIf NetMode = "NET" Then
                                    'BackgroundWorker4.Dispose()
                                    'If Not ThreadReceive_cm Is Nothing Then ThreadReceive_cm.Abort()
                                End If
                                GdtCompanyAnalysis = objTrad.select_company_ana()
                            Case MsgBoxResult.No
                                REM SaveColumn Profile
                                Gsub_GridColProfileSave()
                                chkanalysis = False
                                objanalysis = Nothing
                                refreshstarted = False
                                ReFresh_Maintable()
                                Call save_data(maintable)
                                If chkanalysis = True Then
                                    MDI.ToolStripcompanyCombo.Visible = True
                                    MDI.ToolStripMenuSearchComp.Visible = True
                                End If

                                'server.Close()
                                If NetMode = "UDP" Then
                                    'Try
                                    '    'multicastListener_fo.Disconnect(False)
                                    '    If Not multicastListener_fo Is Nothing Then
                                    '        multicastListener_fo.Close()
                                    '        multicastListener_fo = Nothing
                                    '    End If
                                    '    If Not ThreadReceive_fo Is Nothing Then ThreadReceive_fo.Abort()
                                    'Catch ex As Threading.ThreadAbortException
                                    '    Threading.Thread.ResetAbort()
                                    'End Try
                                    'Try
                                    '    'multicastListener_cm.Disconnect(False)
                                    '    If Not multicastListener_cm Is Nothing Then
                                    '        multicastListener_cm.Close()
                                    '        multicastListener_cm = Nothing
                                    '    End If
                                    '    If Not ThreadReceive_cm Is Nothing Then ThreadReceive_cm.Abort()
                                    'Catch ex As Threading.ThreadAbortException
                                    '    Threading.Thread.ResetAbort()
                                    'End Try
                                ElseIf NetMode = "TCP" Then
                                    'BackgroundWorker2.Dispose()
                                    Objsql.DeleteFoToken()
                                    Objsql.DeleteEqToken()
                                    Objsql.DeleteCurToken()
                                ElseIf NetMode = "NET" Then
                                    'BackgroundWorker4.Dispose()
                                    'If Not ThreadReceive_cm Is Nothing Then ThreadReceive_cm.Abort()
                                End If
                            Case MsgBoxResult.Cancel
                                e.Cancel = True
                                If chkanalysis = True Then
                                    MDI.ToolStripcompanyCombo.Visible = True
                                    MDI.ToolStripMenuSearchComp.Visible = True
                                End If
                        End Select
                    Else 'DGTrading.RowCount = 0
                        Gsub_GridColProfileSave()
                        If maintable.Rows.Count > 0 Then
                            Call save_data(maintable)
                        End If
                    End If
                    MDI.SummaryF9ToolStripMenuItem.Enabled = False
                Catch ex As Threading.ThreadAbortException
                    Threading.Thread.ResetAbort()
                End Try
                chkanalysis = False
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
            'ThreadReceive_fo.Abort()
            'ThreadReceive_cm.Abort()
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    ''' <summary>
    ''' analysis_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to following process done
    ''' 1. Fill scripttable, eqsecurity, prebal, comptable datatable
    ''' 2. Fill hashOrder hashtable using trading Order No. and Entry No.
    ''' 3. Set Apropriate method to Delagate object
    ''' 4. Initialize SPAN datatable
    ''' 5. Initialize Broadcast and start broadcast receiving
    ''' 6. Fill company tab in tab control and if NIFTY tab exist and Select NIFTY table otherwise default tab select
    '''</remarks>
    Private Sub analysis_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        If GdtSettings.Select("SettingName='LTP_PLUS_GAP'").Length = 0 Then
            objTrad.SettingName = "LTP_PLUS_GAP"
            objTrad.SettingKey = "10"
            objTrad.Insert_setting()
            'GdtSettings = objTrad.Settings
        End If
        If GdtSettings.Select("SettingName='LTP_MINUS_GAP'").Length = 0 Then
            objTrad.SettingName = "LTP_MINUS_GAP"
            objTrad.SettingKey = "-10"
            objTrad.Insert_setting()
            'GdtSettings = objTrad.Settings
        End If

        If GdtSettings.Select("SettingName='VOL_PLUS_GAP'").Length = 0 Then
            objTrad.SettingName = "VOL_PLUS_GAP"
            objTrad.SettingKey = "5"
            objTrad.Insert_setting()
            'GdtSettings = objTrad.Settings
        End If
        If GdtSettings.Select("SettingName='VOL_MINUS_GAP'").Length = 0 Then
            objTrad.SettingName = "VOL_MINUS_GAP"
            objTrad.SettingKey = "-5"
            objTrad.Insert_setting()
            'GdtSettings = objTrad.Settings
        End If
        GdtSettings = objTrad.Settings

        txtLtpVal.Text = GdtSettings.Select("SettingName='LTP_PLUS_GAP'")(0).Item("SettingKey").ToString()
        txtLtpVal1.Text = GdtSettings.Select("SettingName='LTP_MINUS_GAP'")(0).Item("SettingKey").ToString()
        txtVolVal1.Text = GdtSettings.Select("SettingName='VOL_PLUS_GAP'")(0).Item("SettingKey").ToString()
        txtVolVal.Text = GdtSettings.Select("SettingName='VOL_MINUS_GAP'")(0).Item("SettingKey").ToString()



        dtBCopy = New DataTable
        dtBCopy.Columns.Add("InstrumentName", GetType(String))
        dtBCopy.Columns.Add("Symbol", GetType(String))
        dtBCopy.Columns.Add("ExpiryDate", GetType(String))
        dtBCopy.Columns.Add("StrikePrice", GetType(String))
        dtBCopy.Columns.Add("OptionType", GetType(String))
        dtBCopy.Columns.Add("CALevel", GetType(String))
        dtBCopy.Columns.Add("MarketType", GetType(String))
        dtBCopy.Columns.Add("OpenPrice", GetType(String))
        dtBCopy.Columns.Add("HighPrice", GetType(String))
        dtBCopy.Columns.Add("LowPrice", GetType(String))
        dtBCopy.Columns.Add("ClosingPrice", GetType(String))
        dtBCopy.Columns.Add("TotalQuantityTraded", GetType(String))
        dtBCopy.Columns.Add("TotalValueTraded", GetType(String))
        dtBCopy.Columns.Add("PreviousClosePrice", GetType(String))
        dtBCopy.Columns.Add("OpenInterest", GetType(String))
        dtBCopy.Columns.Add("ChgOpenInterest", GetType(String))
        dtBCopy.Columns.Add("Indicator", GetType(String))
        dtBCopy.Columns.Add("BCAST", GetType(String))
        dtBCopy.Columns.Add("MsgTyp", GetType(String))

        dtfoBCopy = dtBCopy.Clone
        dtcmBCopy = dtBCopy.Clone
        dtcurBCopy = dtBCopy.Clone



        'CheckExpiryDate()
        REM Check Expiry date againest System date
        If Today >= CDate(G_VarExpiryDate) Then
            MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
            Call clsGlobal.Sub_Get_Version_TextFile()
            Application.Exit()
            End
            Exit Sub
        End If


        'MsgBox(DateDiff(DateInterval.Second, TmpDate, Now))
        If GdtSettings.Select("SettingName='IMPORT_PREVIOUS_POSITION_FLAG'").Length > 0 Then
            If GdtSettings.Select("SettingName='IMPORT_PREVIOUS_POSITION_FLAG'")(0).Item("SettingKey") = "TRUE" Then
                Me.Cursor = Cursors.WaitCursor
                objTrad.Uid = GdtSettings.Select("SettingName='IMPORT_PREVIOUS_POSITION_FLAG'")(0).Item("Uid")
                objTrad.SettingName = "IMPORT_PREVIOUS_POSITION_FLAG"
                objTrad.SettingKey = "FALSE"
                objTrad.Update_setting()
                GdtSettings = objTrad.Settings
                Call fill_equity_dtable()
                objAna.get_ltp(maintable)
                Me.Cursor = Cursors.Default
            End If
        End If
        MDI.ExportAnalysisToolStripMenuItem.Visible = True
        MDI.AddUserDefineTagToolStripMenuItem.Visible = True
        Dim DtGrid As New DataTable
        'MsgBox(tbcomp.TabPages.Count)
        compname = ""
        'objanalysis : used in scenario tocheck if analysis is open or not 
        objanalysis = Me

        'to open only one instance of analysis
        chkanalysis = True 'For open analysis status

        'to draw tabcontrol and tableLayoutpanel 1
        Me.tbcomp.DrawMode = TabDrawMode.OwnerDrawFixed
        TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
        TableLayoutPanel1.RowStyles(2).Height = 1
        TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Absolute
        TableLayoutPanel1.RowStyles(3).Height = 1

        ''fill all contract script into scripttable
        scripttable = New DataTable
        scripttable = cpfmaster

        'fill all equity security in eqsecurity table
        eqsecurity = New DataTable
        eqsecurity = eqmaster

        'select previous balance Expense calculation
        prebal = objTrad.prebal

        'REM For Patch Expense Diff
        'DtExpDiff = New DataTable
        'Dim Pre_Expense As Double
        'Dim Cur_Expense As Double
        'Dim Expense_Diff As Double
        'Dim PatchDate As Date
        'DtExpDiff = objTrad.Select_Patch_Expense
        'If DtExpDiff.Rows.Count > 0 Then
        '    If DtExpDiff.Rows(0)("ExpenseDiffFlg") = True Then
        '        G_DTExpenseData.Rows.Clear()
        '        REM  Calculate Expense  
        '        Call GSub_CalculateExpense(GdtFOTrades, "FO", True)
        '        Call GSub_CalculateExpense(GdtEQTrades, "EQ", True)
        '        Call GSub_CalculateExpense(GdtCurrencyTrades, "CURR", True)
        '        REM End

        '        PatchDate = DtExpDiff.Rows(0)("PatchDate")
        '        Pre_Expense = Val(IIf(IsDBNull(prebal.Compute("Sum(stbal)", "")) = True, 0, prebal.Compute("Sum(stbal)", "")) + IIf(IsDBNull(prebal.Compute("Sum(futbal)", "")) = True, 0, prebal.Compute("Sum(futbal)", "")) + IIf(IsDBNull(prebal.Compute("Sum(optbal)", "")) = True, 0, prebal.Compute("Sum(optbal)", "")))
        '        Cur_Expense = Val(G_DTExpenseData.Compute("sum(Expense)", ""))
        '        Expense_Diff = Pre_Expense - Cur_Expense
        '        objTrad.Update_Patch_Expense(PatchDate, Expense_Diff, False)
        '        GPatchExpDiff = Expense_Diff
        '    Else
        '        GPatchExpDiff = DtExpDiff.Rows(0)("ExpenseDiff")
        '    End If
        'Else
        '    GPatchExpDiff = 0
        'End If
        'REM End

        REM For Patch Expense Diff
        DtExpDiff = New DataTable
        Dim Pre_Expense As Double
        Dim Cur_Expense As Double
        Dim Expense_Diff As Double
        Dim PatchDate As Date
        DtExpDiff = objTrad.Select_Patch_Expense
        If DtExpDiff.Rows.Count > 0 Then
            If DtExpDiff.Rows(0)("ExpenseDiffFlg") = True Then
                G_DTExpenseData.Rows.Clear()
                REM  Calculate Expense  
                Call GSub_CalculateExpense(GdtFOTrades, "FO", True)
                Call GSub_CalculateExpense(GdtEQTrades, "EQ", True)
                Call GSub_CalculateExpense(GdtCurrencyTrades, "CURR", True)
                REM End
                PatchDate = DtExpDiff.Rows(0)("PatchDate")
                For Each dr As DataRow In DtExpDiff.DefaultView.ToTable(True, "Company").Rows
                    'Pre_Expense = Val(IIf(IsDBNull(prebal.Compute("Sum(stbal)", "")) = True, 0, prebal.Compute("Sum(stbal)", "")) + IIf(IsDBNull(prebal.Compute("Sum(futbal)", "")) = True, 0, prebal.Compute("Sum(futbal)", "")) + IIf(IsDBNull(prebal.Compute("Sum(optbal)", "")) = True, 0, prebal.Compute("Sum(optbal)", "")))
                    Pre_Expense = Val(IIf(IsDBNull(prebal.Compute("Sum(stbal)", "Company='" & dr("Company") & "'")) = True, 0, prebal.Compute("Sum(stbal)", "Company='" & dr("Company") & "'")) + IIf(IsDBNull(prebal.Compute("Sum(futbal)", "Company='" & dr("Company") & "'")) = True, 0, prebal.Compute("Sum(futbal)", "Company='" & dr("Company") & "'")) + IIf(IsDBNull(prebal.Compute("Sum(optbal)", "Company='" & dr("Company") & "'")) = True, 0, prebal.Compute("Sum(optbal)", "Company='" & dr("Company") & "'")))
                    Cur_Expense = Val(IIf(IsDBNull(G_DTExpenseData.Compute("sum(Expense)", "Company='" & dr("Company") & "'")) = True, 0, G_DTExpenseData.Compute("sum(Expense)", "Company='" & dr("Company") & "'")))
                    Expense_Diff = Pre_Expense - Cur_Expense
                    objTrad.Update_Patch_Expense(dr("Company"), PatchDate, Expense_Diff, False)
                Next
                'GPatchExpDiff = Expense_Diff
                GPatchExpDiff = Val(IIf(IsDBNull(DtExpDiff.Compute("sum(ExpenseDiff)", "")) = True, 0, DtExpDiff.Compute("sum(ExpenseDiff)", "")))
            Else
                'GPatchExpDiff = DtExpDiff.Rows(0)("ExpenseDiff")
                GPatchExpDiff = Val(IIf(IsDBNull(DtExpDiff.Compute("sum(ExpenseDiff)", "")) = True, 0, DtExpDiff.Compute("sum(ExpenseDiff)", "")))
            End If
        Else
            GPatchExpDiff = 0
        End If

        DtExpDiff = objTrad.Select_Patch_Expense

        'DtExpDiff.AcceptChanges()
        REM End


        'select company
        'comptable = New DataTable
        comptable = objTrad.Comapany
        comptable_Net = objTrad.Comapany_Net


        REM Comented Because Of Trade File Import  Process Change (By Viral 1 Aug-11)
        'plz Comment HasOrder Code till Nse Eq Impliment in Link Table
        'add all 'orderno-entryno' of FO and EQ trades to hashorder table
        hashOrder = New Hashtable
        'add FO trades 'orderno-entryno' to hashorder 
        For Each drow As DataRow In GdtFOTrades.Rows
            If drow("orderno").ToString <> "0" Then
                If hashOrder.Contains(drow("orderno").ToString.Trim & "-" & drow("entryno").ToString.Trim) = False Then
                    hashOrder.Add(drow("orderno").ToString.Trim & "-" & drow("entryno").ToString.Trim, "1")
                End If
            End If
        Next
        'add EQ trades' 'orderno-entryno' to hashorder 
        For Each drow As DataRow In GdtEQTrades.Rows
            If drow("orderno").ToString <> "0" Then
                If hashOrder.Contains(drow("orderno").ToString.Trim & "-" & drow("entryno").ToString.Trim) = False Then
                    hashOrder.Add(drow("orderno").ToString.Trim & "-" & drow("entryno").ToString.Trim, "1")
                End If
            End If
        Next

        Dim noday As Integer
        Dim i As Integer
        Me.Cursor = Cursors.WaitCursor

        'set delegates to functions to update all 3 futures value, _
        'equity, broadcast timing label , margin's value
        mdel = AddressOf fut
        mdel1 = AddressOf fut1
        mdel2 = AddressOf fut2
        meqdel = AddressOf eq
        mval = AddressOf get_value
        mtest = AddressOf ftest
        REM Because of Margin show in PopUp Window By Viral
        'mintMarg = AddressOf fintMarg
        'mexpMarg = AddressOf fexpMarg
        REM END 
        mtotMarg = AddressOf ftotMarg

        'stores setting values
        Mrateofinterast = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Rateofinterest'").ToString)

        noday = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='NoofDay'").ToString)
        zero_analysis = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='zero_qty_analysis'").ToString)
        objval = GdtSettings.Compute("max(SettingKey)", "SettingName='Maturity_Far_month'").ToString
        Dim farDate As Object
        Dim varMMonth As Int16 = Today.Month

        If objval = "" Or objval = "Current Month" Then
            varMMonth = Today.Month + 1
            'Maturity_Far_month = CDate(Today.Month & "/30/" & Today.Year)
        ElseIf objval = "Next Month" Then
            varMMonth = Today.Month + 2
            'Maturity_Far_month = CDate(Today.Month + 1 & "/30/" & Today.Year)
        Else
            varMMonth = Today.Month + 3
            'Dim strdate As String = Today.Month + 2 & "/30/" & Today.Year
            'Maturity_Far_month = CDate(strdate)
        End If
        If varMMonth > 12 Then
            varMMonth = varMMonth Mod 12
            Maturity_Far_month = DateSerial(Today.Year + 1, varMMonth, 1)
        Else
            Maturity_Far_month = DateSerial(Today.Year, varMMonth, 1)
        End If

        scripttable.Select("") ' This Code Because of Speed-Up as Directed By Alpeshbhai (By Viral 4-aug-11)
        farDate = scripttable.Compute("max(expdate1)", "expdate1 < #" & fDate(Maturity_Far_month) & "#")
        If IsDBNull(farDate) = False Then
            Maturity_Far_month = CDate(farDate)
        End If
        'if maturity date is gone then display message to modify date
        If CDate(Format(Now, "MMM/dd/yyyy")) > Maturity_Far_month Then
            MsgBox("Please modify the 'Far Month Maturity' Date from Settings.", MsgBoxStyle.Exclamation)
        End If

        'Noofday calculation
        If noday > 1 Then
            txtnoofday.Text = noday
        ElseIf noday = 1 And UCase(WeekdayName(Weekday(Now))) = "FRIDAY" Then
            txtnoofday.Text = 3
        Else
            txtnoofday.Text = 1
        End If

        'initialize span table
        init_span_tables()

        i = 0
        'if NIFTY company is in comptable then select it otherwise select first one
        For Each drow As DataRow In comptable.Select("", "Company")
            tbcomp.TabPages.Add(drow("company").ToString)
            tbcomp.TabPages.Item(i).Name = drow("company").ToString
            tbcomp.TabPages.Item(i).Tag = drow("company").ToString
            If drow("company").ToString.ToUpper = "NIFTY" Then
                chknifty = True
                ind = i
            End If
            i = i + 1
        Next
        RefreshToolStripMenuItem.Visible = False
        If NetMode = "TCP" Then
            ReFreshToken()
            Me.Text = Me.Text & " - TCP"
        ElseIf NetMode = "UDP" Then
            Me.Text = Me.Text & " - UDP"
        ElseIf NetMode = "NET" Then
            Me.Text = Me.Text & " - InterNet"
            RefreshToolStripMenuItem.Visible = True
        End If

        'select symbol except 'CA','CE','PA','PE'
        mdv = New DataView(scripttable, "option_type not in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows)

        'add all tokens from contract  to futall
        futall = New ArrayList
        futall = futtoken
        'add all tokens from equity  to eqfutall
        eqfutall = New ArrayList
        eqfutall = eqtoken

        REM Fill All Currency Future Token
        Currfutall = New ArrayList
        Currfutall = Currfuttoken
        'Margin ###################################################################
        'select span path
        mSPAN_path = GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_path'").ToString
        'get exposure margine
        mTbl_exposure_database = objTrad.Exposure_margin

        '  fill_equity_dtable()         'changes done by nasima at 10 th july
        fill_tabpages()
        'cal_expense_position()
        'change tab to selected compnaty
        If tbcomp.TabPages.Count > 0 Then
            firstload = "Treaded"
            If chknifty = True Then
                compname = "NIFTY"
                tbcomp.SelectedIndex = ind
            Else
                compname = tbcomp.SelectedTab.Text
            End If
            Call Fill_StrategyTab_MonthWise()
            '  maintable.Rows.Clear()
            'first time change_tab so "traded" is used
            If ThrdLoadBhavCopy.IsAlive Then
                ThrdLoadBhavCopy.Priority = ThreadPriority.Highest
            End If

            change_tab(compname, "Treaded")

            
            'cmdStart_Click(cmdStart, e)
        End If


        REM For Set Grid Column Index And Width From Saved Setting
        DtGrid = objTrad.GFun_SetGridColumnSetting(DGTrading)
        If DtGrid.Rows.Count > 0 Then
            Dim DvGrid As New DataView
            DvGrid = DtGrid.DefaultView
            DvGrid.RowFilter = "FormName='Analysis'"
            DvGrid.Sort = "DisplayIndex"
            Dim ColList() As String = {"ColumnName", "DisplayIndex", "Width", "IsVisible"}
            For Each Dr As DataRow In DvGrid.ToTable(True, ColList).Rows
                'If Dr("ColumnName").ToString = "deltaval" Then MsgBox("A")
                If Dr("DisplayIndex") >= DGTrading.Columns.Count Then
                    DGTrading.Columns(Dr("ColumnName").ToString).DisplayIndex = DGTrading.Columns.Count - 1
                Else
                    DGTrading.Columns(Dr("ColumnName").ToString).DisplayIndex = Dr("DisplayIndex")
                End If
                DGTrading.Columns(Dr("ColumnName").ToString).Width = Dr("width")
                'MsgBox(Dr("ColumnName").ToString & "-" & Dr("DisplayIndex"))
                DGTrading.Columns(Dr("ColumnName").ToString).Visible = CBool(Dr("IsVisible"))

                If Dr("ColumnName").ToString = "liq" Then
                    If CBool(Dr("IsVisible")) Then
                        IlliqToolStripMenuItem.ForeColor = Color.Black
                    Else
                        IlliqToolStripMenuItem.ForeColor = Color.Blue
                    End If
                ElseIf Dr("ColumnName").ToString = "grossmtm" Then
                    If CBool(Dr("IsVisible")) Then
                        GrossMTMToolStripMenuItem.ForeColor = Color.Black
                    Else
                        GrossMTMToolStripMenuItem.ForeColor = Color.Blue
                    End If
                ElseIf Dr("ColumnName").ToString = "remarks" Then
                    If CBool(Dr("IsVisible")) Then
                        RemarksToolStripMenuItem.ForeColor = Color.Black
                    Else
                        RemarksToolStripMenuItem.ForeColor = Color.Blue
                    End If
                ElseIf Dr("ColumnName").ToString.ToUpper = "DELTA" Then
                    If CBool(Dr("IsVisible")) Then
                        HideGreeksToolStripMenuItem.Text = "Hide Greeks"
                    Else
                        HideGreeksToolStripMenuItem.Text = "Show Greeks"
                    End If
                ElseIf Dr("ColumnName").ToString.ToUpper = "DELTAVAL" Then
                    If CBool(Dr("IsVisible")) Then
                        HideGreeksValToolStripMenuItem.Text = "Hide Greeks Val"
                    Else
                        HideGreeksValToolStripMenuItem.Text = "Show Greeks Val"
                    End If
                ElseIf Dr("ColumnName").ToString = "DeltaN" Then
                    If CBool(Dr("IsVisible")) Then
                        HideGreeksNutToolStripMenuItem.Text = "Hide Greeks Neut."
                    Else
                        HideGreeksNutToolStripMenuItem.Text = "Show Greeks Neut."
                    End If
                End If
            Next
        End If

        'today date
        txtdttoday.Text = CStr(Format(Now, "dd/MMM/yyyy"))

        Me.Cursor = Cursors.Default
        'after first time changetab firstload=""
        firstload = ""

        'set calculation interval
        Dim btime As Double
        btime = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Timer_Calculation_Interval'").ToString)
        If btime = 0 Then
            btime = 1
        End If
        Timer_Calculation.Interval = btime * 1000
        Timer_Calculation.Enabled = True

        'set refresh interval
        Dim rtime As Double
        rtime = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Refresh_time'").ToString)
        If rtime = 0 Then
            rtime = 30
        End If
        Timer_refresh.Interval = rtime * 1000
        Timer_refresh.Enabled = True

        'set span interval
        Dim stime As Double
        stime = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='SPAN_TIMER'").ToString)
        If stime = 0 Then
            stime = 30
        End If
        Timer_span.Interval = stime * 1000
        Timer_span.Enabled = True

        'use to check any change in value of call vol,put vol, future textbox
        changeVal1 = False

        If mode = "Offline" Then
            cmdStart_Click(cmdStart, e)
        ElseIf mode = "Future Update Only" Then
            cmdstart_future_update_click(cmdstart_future_update, e)
        End If

        REM Implementing ATM/ITM/OTM  By viral 27-06-11
        If SELECTION_TYPE = Setting_SELECTION_TYPE.ITM_ATM_OTM Then
            lblITM.Text = "ITM"
            lblATM.Text = "ATM"
            lblOTM.Text = "OTM"
        ElseIf SELECTION_TYPE = Setting_SELECTION_TYPE.STRIKE Then
            lblITM.Text = "ATM-"
            lblATM.Text = "ATM"
            lblOTM.Text = "ATM+"
        End If

        Call searchcompany()

        MDI.ToolStripcompanyCombo.Text = compname
        Me.WindowState = FormWindowState.Maximized
        'call timer_calculation at form_load
        ''By Viral 06-08-2011
        'Call Timer_Calculation_Tick(sender, e)


        'Me.Refresh()
        'ToolTip1.SetToolTip(cmdStart, "F12")

        dGVColumnHeader = New DGVColumnHeader
        'DGTrading.ColumnHeadersDefaultCellStyle, DGTrading.AdjustedTopLeftHeaderBorderStyle)

        DGTrading.Columns("IsCalc").HeaderCell = dGVColumnHeader

        alert_comp()
    End Sub

    Private Sub DGTrading_ColumnHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGTrading.ColumnHeaderMouseClick
        If DGTrading.Columns(e.ColumnIndex).Name = "IsCalc" Then
            For i As Integer = 0 To DGTrading.Rows.Count - 1
                'DGTrading.Rows(i).Cells("IsCalc").Value = dGVColumnHeader.CheckAll
                DGTrading.Rows(i).Cells("IsCalc").Value = CType(DGTrading.Columns("IsCalc").HeaderCell, DGVColumnHeader).CheckAll

                Dim script As String
                script = DGTrading.Rows(i).Cells("script").Value

                For Each drow As DataRow In maintable.Select("script='" & script & "' And company='" & compname & "'")
                    drow("IsCalc") = CBool(DGTrading.Rows(i).Cells("Iscalc").Value)
                Next
                currtable.Rows(i)("IsCalc") = CBool(DGTrading.Rows(i).Cells("IsCalc").Value)
                objAna.Update_IsCalc(script, CBool(DGTrading.Rows(i).Cells("IsCalc").Value), compname)

            Next
        End If
    End Sub

    Private Sub DGTrading_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGTrading.CellClick
        'If DGTrading.Columns(e.ColumnIndex).Name = "IsCalc" Then
        '    For i As Integer = 0 To Me.DataGridView1.RowCount - 1
        '        'Escalate Editmode
        '        Me.DataGridView1.EndEdit()
        '        Dim re_value As String = Me.DataGridView1.Rows(i).Cells(0).EditedFormattedValue.ToString()
        '        Me.DataGridView1.Rows(i).Cells(0).Value = "true"
        '    Next
        'End If
    End Sub

    ''' <summary>
    ''' fill_tabpages
    ''' </summary>
    ''' <remarks>This method call to Add tab page into tab control</remarks>
    Private Sub fill_tabpages()
        If zero_analysis = "1" Then 'keep only those companies whose qty=0
            Dim t As Integer = tbcomp.TabPages.Count - 1
            Dim tab As TabPage
            Dim dv As DataView
            While t >= 0
                tab = tbcomp.TabPages(t)
                dv = New DataView(maintable, "company ='" & tab.Name.ToString() & "'and units <> 0", "", DataViewRowState.CurrentRows)
                If dv.ToTable.Rows.Count <= 0 Then
                    If tab.Name.ToUpper() = "NIFTY" Then
                        chknifty = False 'delete nifty
                    End If
                    tbcomp.TabPages.Remove(tab)
                    t = tbcomp.TabPages.Count - 1
                Else
                    t -= 1
                End If
            End While
            If chknifty = True Then 'if nifty company exists
                Dim i As Integer = 0
                For Each tab In tbcomp.TabPages
                    If tab.Name.ToUpper() = "NIFTY" Then
                        ind = i
                        Exit For
                    End If
                    i += 1
                Next
            End If
        End If
    End Sub
    Private Sub Fill_StrategyTab_MonthWise(Optional ByVal IsTabAppand As Boolean = False)
        Try
            VarIsTabAddRemove = True
            If IsTabAppand = False Then
                TabStrategy.TabPages.Clear()
            End If
            If maintable.Rows.Count > 0 Then
                Dim obj_tab As TabPage
                If TabStrategy.TabPages.ContainsKey("TP_All") = False Then
                    obj_tab = New TabPage
                    obj_tab.Text = "All"
                    obj_tab.Name = "TP_All"
                    TabStrategy.TabPages.Add(obj_tab)
                End If
                For Each dr As DataRow In New DataView(maintable, "company='" & compname & "' AND CP <> 'E'", "mdate", DataViewRowState.CurrentRows).ToTable(True, "mdate").Rows
                    If TabStrategy.TabPages.ContainsKey("TP_" & UCase(Format(CDate(dr("mdate")), "MMMyy"))) = False Then
                        obj_tab = New TabPage
                        obj_tab.Text = Format(CDate(dr("mdate")), "MMM").ToUpper()
                        obj_tab.Tag = Format(CDate(dr("mdate")), "dd-MMM-yyyy")
                        obj_tab.Name = "TP_" & Format(CDate(dr("mdate")), "MMMyy").ToUpper()
                        TabStrategy.TabPages.Add(obj_tab)
                    End If
                Next
            Else
                Dim obj_tab As TabPage
                If TabStrategy.TabPages.ContainsKey("TP_All") = False Then
                    obj_tab = New TabPage
                    obj_tab.Text = "All"
                    obj_tab.Name = "TP_All"
                    TabStrategy.TabPages.Add(obj_tab)
                End If
            End If
            VarIsTabAddRemove = False
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
            VarIsTabAddRemove = False
        End Try
    End Sub
    ''' <summary>
    ''' setGridTrad()
    ''' </summary>
    '''<remarks>use for setting of resolution</remarks>
    Private Sub setGridTrad()
        Dim intX As Integer
        intX = Screen.PrimaryScreen.Bounds.Width
        Dim diff As Double = intX - 1024

        'if screen resolution is 1024 then no change
        If diff = 0 Then
            Exit Sub
        ElseIf diff > 0 Then 'if resolution > 1024 then increase column width
            diff = diff / 16
            Dim i As Integer
            For i = 0 To DGTrading.Columns.Count - 1
                DGTrading.Columns(i).Width += diff
            Next
        Else 'if resolution < 1024 then descrease column width
            diff = diff / 16
            Dim i As Integer
            For i = 0 To DGTrading.Columns.Count - 1
                DGTrading.Columns(i).Width += diff
            Next
        End If
    End Sub
    ''' <summary>
    ''' analysis_FormClosed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to hide Search bar from MDI from</remarks>
    Private Sub analysis_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MDI.ToolStripMenuSearchComp.Visible = False
        MDI.ToolStripcompanyCombo.Visible = False
        MDI.ExportAnalysisToolStripMenuItem.Visible = False
        MDI.AddUserDefineTagToolStripMenuItem.Visible = False
        If IsVersionExpire = True Then
            MsgBox("Please Contact Vendor, Version Expired.", MsgBoxStyle.Exclamation)
            Call clsGlobal.Sub_Get_Version_TextFile()
            Application.Exit()
            End
            Exit Sub
        End If
    End Sub

#Region "Data from Server"
    
    

    

    '====================
    
    '====================
    

    ''' <summary>
    ''' cmdStart_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to set Online and Offline mode</remarks>
    Private Sub cmdStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStart.Click
        'set isliq column to read only
        DGTrading.Columns("liq").ReadOnly = True


        DGTrading.Columns("IsCalc").ReadOnly = True
        CType(DGTrading.Columns("IsCalc").HeaderCell, DGVColumnHeader).rdOnly = True
        DGTrading.Columns("lv").ReadOnly = True

        'stop future 'update mode'
        cmdstart_future_update.Text = "Start Future Update"
        thrworking_future_only = False

        'stop timer 
        '  Timer_Calculation.Stop()
        ' thrworking = True
        'call function toggle from start to stop and vice versa
        Call start_stop()
    End Sub
    ''' <summary>
    ''' start_stop
    ''' </summary>
    ''' <remarks>This method call to Illiq and remark column set Readonly=TRUE or Readonly=FALSE</remarks>

    Private Sub start_stop()
        If thrworking = True Then 'if online mode then set offline mode
            'seeting for offline mode
            cmdStart.Text = "Start"
            thrworking = False      'online flag set to false
            'call or put's isliq cell set as readonly = false
            For Each grow As DataGridViewRow In DGTrading.Rows
                If (grow.Cells("CP").Value = "C" Or grow.Cells("CP").Value = "P") Then
                    grow.Cells("liq").ReadOnly = False
                Else
                    grow.Cells("liq").ReadOnly = True
                End If
                grow.Cells("remarks").ReadOnly = False
                grow.Cells("IsCalc").ReadOnly = False  
                grow.Cells("lv").ReadOnly = False 
            Next
            CType(DGTrading.Columns("IsCalc").HeaderCell, DGVColumnHeader).rdOnly = False
            'txtcvol.ReadOnly = False
            'txtpvol.ReadOnly = False
            'txtcvol1.ReadOnly = False
            'txtpvol1.ReadOnly = False
            'txtcvol2.ReadOnly = False
            'txtpvol2.ReadOnly = False
            'txtfut1.ReadOnly = False
            'txtfut2.ReadOnly = False
            'txtfut3.ReadOnly = False

            'stop calculation timer
            Timer_Calculation.Stop()
            txteqrate.ReadOnly = False
            lblStatus.Text = "Offline Update"
            lblStatus.BackColor = Color.Red

        Else   'setting for online mode
            Save_applied = False   'offline mode flag set to false
            'start online mode
            cmdStart.Text = "Stop"
            thrworking = True
            Timer_Calculation.Start()
            lblStatus.Text = "Online Update"
            lblStatus.BackColor = Color.Green
            txteqrate.ReadOnly = True
            'txtcvol.ReadOnly = True
            'txtpvol.ReadOnly = True
            'txtcvol1.ReadOnly = True
            'txtpvol1.ReadOnly = True
            'txtcvol2.ReadOnly = True
            'txtpvol2.ReadOnly = True
            'txtfut1.ReadOnly = True
            'txtfut2.ReadOnly = True
            'txtfut3.ReadOnly = True
            DGTrading.Columns("liq").ReadOnly = True
            DGTrading.Columns("IsCalc").ReadOnly = True
            CType(DGTrading.Columns("IsCalc").HeaderCell, DGVColumnHeader).rdOnly = True
            DGTrading.Columns("remarks").ReadOnly = True
            DGTrading.Columns("lv").ReadOnly = True
        End If
    End Sub
    ''' <summary>
    ''' start_stop_future_only
    ''' </summary>
    ''' <remarks>This method call to Start and Stop future mode</remarks>
    Private Sub start_stop_future_only()
        If thrworking_future_only = True Then   'if future update only mode is running then stop it
            DGTrading.Columns("liq").ReadOnly = True
            DGTrading.Columns("IsCalc").ReadOnly = True
            CType(DGTrading.Columns("IsCalc").HeaderCell, DGVColumnHeader).rdOnly = True
            DGTrading.Columns("remarks").ReadOnly = True
            DGTrading.Columns("lv").ReadOnly = True
            cmdstart_future_update.Text = "Start Future Update"
            thrworking_future_only = False

            'stop the timer to update the data accoridng to future
            Timer_Calculation.Stop()

            'to stop future update mode and start offline mode
            thrworking = True
            Call start_stop()

        Else   'start 'future update mode' 
            cmdstart_future_update.Text = "Stop Future Update"
            thrworking_future_only = True

            'if we start the future update only then we have to stop the regualar updates of broadcast
            'set online mode = flase
            cmdStart.Text = "Start"
            thrworking = False

            ' grdtrad.Columns(26).ReadOnly = False
            For Each grow As DataGridViewRow In DGTrading.Rows
                If (grow.Cells("CP").Value = "C" Or grow.Cells("CP").Value = "P") Then
                    grow.Cells("liq").ReadOnly = False
                Else
                    grow.Cells("liq").ReadOnly = True
                End If
                grow.Cells("remarks").ReadOnly = False
                grow.Cells("IsCalc").ReadOnly = False
                grow.Cells("lv").ReadOnly = False
            Next
            CType(DGTrading.Columns("IsCalc").HeaderCell, DGVColumnHeader).rdOnly = False
            'start the timer to update the data accoridng to future
            Timer_Calculation.Start()
            lblStatus.Text = "Future Update Only"
            lblStatus.BackColor = Color.Orange
        End If
    End Sub
#End Region

#Region "CALL PUT"
    ''' <summary>
    ''' tbcomp_SelectedIndexChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to Tab page navigate than refresh analysis form</remarks>
    Private Sub tbcomp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbcomp.SelectedIndexChanged
        If VarIsTabAddRemove = True Then Exit Sub
        'Dim mStart As String
        Try
            ' Call cmdsave_Click()
            If tbcomp.TabPages.Count > 0 And Not tbcomp.SelectedTab Is Nothing Then
                pan1.Visible = False
                pan2.Visible = False
                pan3.Visible = False
                'first time not called bacoz firstload='traded'
                If firstload = "" And Not compname Is Nothing Then
                    'selectedtab = tbcomp.SelectedTab
                    'mStart = cmdStart.Text
                    compname = tbcomp.SelectedTab.Text
                    Call Fill_StrategyTab_MonthWise()
                    Call change_tab(compname, tbmo)
                    'SetStartStop(mStart)
                    ind1 = tbcomp.TabPages.IndexOf(tbcomp.SelectedTab)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    ''' <summary>
    ''' clear_tab
    ''' </summary>
    ''' <remarks>This method call to reset all text box value</remarks>
    Private Sub clear_tab()
        txtshare.Text = "0"
        txttdelval.Text = "0"
        txttthval.Text = "0"
        txttvgval.Text = "0"
        txttgmval.Text = "0"
        txteqrate.Text = "0"
        txttdelta1.Text = "0"
        txtttheta1.Text = "0"
        txttvega1.Text = "0"
        txttgamma1.Text = "0"

        txttvolgaval.Text = "0"
        txttvolga1.Text = "0"
        txttvannaval.Text = "0"
        txttvanna1.Text = "0"

        'Alpesh 
        'pan11 = False
        'pan21 = False
        'pan31 = False

        'txtfut1.Visible = False
        'lfut1.Visible = False
        'txtfut2.Visible = False
        'lfut2.Visible = False
        'txtfut3.Visible = False
        'lfut3.Visible = False

        txtcvol.Text = 0
        txtpvol.Text = 0
        txtcvol1.Text = 0
        txtpvol1.Text = 0
        txtcvol2.Text = 0
        txtpvol2.Text = 0
        txtfut1.Text = 0
        txtfut2.Text = 0
        txtfut3.Text = 0
        txtprGmtm.Text = 0
        txttGmtm.Text = 0
        txttotGmtm.Text = 0
        txtprexp.Text = 0
        txttexp.Text = 0
        txttotexp.Text = 0
        txtprnetmtm.Text = 0
        txttnetmtm.Text = 0
        txttotnetmtm.Text = 0
        txttotsqexp.Text = 0
        txttotsqmtm.Text = 0
        'txtrate.Text = 0
        'txtrate1.Text = 0
        'txtrate2.Text = 0
    End Sub
    ''' <summary>
    ''' change_tab
    ''' </summary>
    ''' <param name="str"></param>
    ''' <param name="mo"></param>
    ''' <remarks>This method call to refill datagrid and refresh TOP panel. This method call when tab pahe navigate.</remarks>
    Public Sub change_tab(ByVal str As String, ByVal mo As String)
        Dim ApplyVolFlg As Boolean
        Dim PatchExpDiff As Double
        Try
            Dim T1 = System.Environment.TickCount

            FSTimerLogFile.WriteLine("Change_tab Process Start :: " & System.Environment.TickCount - T1)
            T1 = System.Environment.TickCount
            If TabStrategy.SelectedTab.Name = "TP_All" Then
                Objbhavcopy.LoadCFProfit(str)
            Else
                Objbhavcopy.LoadCFProfit("")
            End If


            If GdtCurrencyTrades.Select("Company='" & str & "'").Length > 0 Then
                VarIsCurrency = True
            Else
                VarIsCurrency = False
            End If
            Call clear_tab()
            Call get_AtmClear()
            'ALPESH 23/04/2011
            'TableLayoutPanel1.RowStyles(2).SizeType = SizeType.Absolute
            'TableLayoutPanel1.RowStyles(2).Height = 0
            'TableLayoutPanel1.RowStyles(3).SizeType = SizeType.Absolute
            'TableLayoutPanel1.RowStyles(3).Height = 0

            currtable.Rows.Clear()
            'mCurrtable.Rows.Clear()

            'Dim dv As DataView
            'dv = New DataView(maintable, "company='" & str & "' ", "strikes,cp,company", DataViewRowState.CurrentRows)
            'currtable = dv.ToTable()

            '' ''REM For Patch Expense Diff
            '' ''DtExpDiff = New DataTable
            '' ''Dim Pre_Expense As Double
            '' ''Dim Cur_Expense As Double
            '' ''Dim Expense_Diff As Double
            '' ''Dim PatchDate As Date
            '' ''DtExpDiff = objTrad.Select_Patch_Expense
            '' ''If DtExpDiff.Rows.Count > 0 Then
            '' ''    If DtExpDiff.Rows(0)("ExpenseDiffFlg") = True Then
            '' ''        PatchDate = DtExpDiff.Rows(0)("PatchDate")
            '' ''        Pre_Expense = Val(prebal.Compute("Sum(stbal)", "") + prebal.Compute("Sum(futbal)", "") + prebal.Compute("Sum(optbal)", ""))
            '' ''        Cur_Expense = Val(G_DTExpenseData.Compute("sum(Expense)", ""))
            '' ''        Expense_Diff = Pre_Expense - Cur_Expense
            '' ''        objTrad.Update_Patch_Expense(PatchDate, Expense_Diff, False)
            '' ''        GPatchExpDiff = Expense_Diff
            '' ''    End If
            '' ''End If
            '' ''REM End 

            'Call CalPatchExpenseDiff()

            Dim dv As DataView
            Dim Var_Condition As String = "company='" & str & "'"

            'If Not TabStrategy.SelectedTab.Name = "TP_All" Then
            '    Var_Condition &= " And mdate = #" & TabStrategy.SelectedTab.Tag & "# AND CP <> 'E'  "
            '    If DtExpDiff.Select("PatchDate >= #" & TabStrategy.SelectedTab.Tag & "# ").Length = 0 Then
            '        PatchExpDiff = 0
            '    Else
            '        PatchExpDiff = GPatchExpDiff
            '    End If
            'Else
            '    PatchExpDiff = GPatchExpDiff
            'End If

            If Not TabStrategy.SelectedTab.Name = "TP_All" Then
                Var_Condition &= " And mdate = #" & TabStrategy.SelectedTab.Tag & "# AND CP <> 'E'  "
                If DtExpDiff.Select("PatchDate >= #" & TabStrategy.SelectedTab.Tag & "# ").Length = 0 Then
                    PatchExpDiff = 0
                Else
                    'PatchExpDiff = GPatchExpDiff
                    PatchExpDiff = IIf(IsDBNull(DtExpDiff.Compute("Sum(ExpenseDiff)", "company='" & str & "'")) = True, 0, DtExpDiff.Compute("Sum(ExpenseDiff)", "company='" & str & "'"))
                End If
            Else
                'PatchExpDiff = GPatchExpDiff
                PatchExpDiff = IIf(IsDBNull(DtExpDiff.Compute("Sum(ExpenseDiff)", Var_Condition)) = True, 0, DtExpDiff.Compute("Sum(ExpenseDiff)", Var_Condition))
            End If


            dv = New DataView(maintable, Var_Condition, "strikes,cp,company", DataViewRowState.CurrentRows)
            currtable = dv.ToTable()

            FSTimerLogFile.WriteLine("Analysis Fill table :: " & System.Environment.TickCount - T1)
            T1 = System.Environment.TickCount

            REM (Jignesh) This method call to fill ht_Ana_Position hashtable which use to get position value
            Call Fill_Analysis_Hashtable(currtable, str)

            FSTimerLogFile.WriteLine("Analysis Fill Hashtable :: " & System.Environment.TickCount - T1)
            T1 = System.Environment.TickCount

            ''divyesh :  check datatable value

            If currtable.Rows.Count > 0 Then
                If currtable.Rows(0).Item("script").ToString.Substring(3, 3) = "IDX" Then
                    'if INDEX then 'calculate vol using equity' option will be visible false
                    chkCalVol.Visible = False
                    'chkCalVol.Checked = False
                    txteqrate.Visible = False
                    lbleq.Visible = False
                ElseIf VarIsCurrency = True Then
                    chkCalVol.Visible = False
                    lbleq.Visible = False
                    txteqrate.Visible = False
                Else
                    chkCalVol.Visible = True
                    txteqrate.Visible = True
                    lbleq.Visible = True
                End If
                If currtable.Rows(0).Item("script").ToString.Substring(3, 3) = "IDX" Then
                    chkCalVol.Visible = False
                End If
            End If

            ' mCurrtable = dv.ToTable()

            'If zero_analysis <> 0 Then 'if zero_analysis=1 thern keep compnies whose qty=0
            '    gcurrtable.Rows.Clear()
            '    Dim gdv As New DataView(maintable, "company='" & str & "' and units <> 0 ", "strikes,cp,company", DataViewRowState.CurrentRows)
            '    gcurrtable = gdv.ToTable()
            'Else
            gcurrtable = dv.ToTable()
            'End If
            cparray = New ArrayList
            futarray = New ArrayList
            eqarray = New ArrayList
            Dim valToken As Double
            Dim arr(14) As String
            arr = objAna.company_ltp(str)
            ' txteqrate.Text = arr(12)  'todisplay offline equity value to textbox
            ''=====================================keval(27-2-10)
            'If arr(13) = "True" Then 'if 'chkvol' checkbox = true then display value
            '    chkCalVol.Checked = True
            '    'txteqrate.Visible = True
            '    txteqrate.Text = arr(12)
            '    '  lbleq.Visible = True
            'Else 'else for IDX 'calculate vol using equity' is visible false
            '    chkCalVol.Checked = False
            '    ' txteqrate.Visible = False
            '    ' lbleq.Visible = False
            'End If
            ''=======================================

            While ThrdLoadBhavCopy.IsAlive
                'MsgBox(Now)
            End While

            For Each drow As DataRow In currtable.Rows
                If drow("CP") = "F" Then 'for FUTURE, if LTP exist then add (token,LTP)
                    gcurrtable.Select("tokanno=" & CLng(drow("tokanno")) & "")(0).Item("Strikes") = 0
                    futarray.Add(CLng(drow("tokanno")))
                    valToken = objAna.ltp_token(CLng(drow("tokanno")), drow("script"), str)
                    If VarIsCurrency = True Then  REM Currency Future
                        If valToken <> 0 Then
                            If Not Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                Currfltpprice.Add(CLng(drow("tokanno")), valToken)
                            End If
                        Else
                            If arr(3) = CDate(drow("mdate")) Then
                                If Not Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currfltpprice.Add(CLng(drow("tokanno")), CDbl(arr(2)))
                                End If
                            ElseIf arr(5) = CDate(drow("mdate")) Then
                                If Not Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currfltpprice.Add(CLng(drow("tokanno")), CDbl(arr(4)))
                                End If
                            ElseIf arr(7) = CDate(drow("mdate")) Then
                                If Not Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currfltpprice.Add(CLng(drow("tokanno")), CDbl(arr(6)))
                                End If
                            End If
                        End If
                    Else  ' FO Future
                        If valToken <> 0 Then
                            If Not fltpprice.Contains(CLng(drow("tokanno"))) Then
                                fltpprice.Add(CLng(drow("tokanno")), valToken)
                            End If
                        Else
                            If arr(3) = CDate(drow("mdate")) Then
                                If Not fltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltpprice.Add(CLng(drow("tokanno")), CDbl(arr(2)))
                                End If
                            ElseIf arr(5) = CDate(drow("mdate")) Then
                                If Not fltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltpprice.Add(CLng(drow("tokanno")), CDbl(arr(4)))
                                End If
                            ElseIf arr(7) = CDate(drow("mdate")) Then
                                If Not fltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltpprice.Add(CLng(drow("tokanno")), CDbl(arr(6)))
                                End If
                            End If
                        End If
                    End If

                ElseIf drow("CP") = "E" Then
                    eqarray.Add(CLng(drow("tokanno")))
                    If Not eltpprice.Contains(CLng(drow("tokanno"))) Then
                        eltpprice.Add(CLng(drow("tokanno")), drow("last"))
                    End If
                Else  REM Option Script
                    If IsDBNull(drow("isliq")) = True Then
                        drow("isliq") = False
                    End If
                    If VarIsCurrency = True Then
                        If CBool(drow("isliq")) = True Then
                            cparray.Add(CLng(drow("token1")))
                            cparray.Add(CLng(drow("tokanno")))
                            If Not Currltpprice.Contains(CLng(drow("tokanno"))) Then
                                Currltpprice.Add(CLng(drow("tokanno")), drow("last"))
                            End If
                            If Not Currltpprice.Contains(CLng(drow("token1"))) Then
                                Currltpprice.Add(CLng(drow("token1")), drow("last1"))
                            End If
                        Else
                            cparray.Add(CLng(drow("tokanno")))
                            If Not Currltpprice.Contains(CLng(drow("tokanno"))) Then
                                Currltpprice.Add(CLng(drow("tokanno")), drow("last"))
                            End If
                        End If
                        If Not futarray.Contains(CLng(drow("ftoken"))) Then
                            futarray.Add(CLng(drow("ftoken")))
                        End If

                        If Not Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                            Currfltpprice.Add(CLng(drow("ftoken")), drow("curSpot"))
                        End If
                    Else
                        If CBool(drow("isliq")) = True Then
                            cparray.Add(CLng(drow("token1")))
                            cparray.Add(CLng(drow("tokanno")))
                            If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                                ltpprice.Add(CLng(drow("tokanno")), drow("last"))
                            End If
                            If Not ltpprice.Contains(CLng(drow("token1"))) Then
                                ltpprice.Add(CLng(drow("token1")), drow("last1"))
                            End If

                        Else
                            cparray.Add(CLng(drow("tokanno")))
                            If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                                ltpprice.Add(CLng(drow("tokanno")), drow("last"))
                            End If
                        End If

                        If Not futarray.Contains(CLng(drow("ftoken"))) Then
                            futarray.Add(CLng(drow("ftoken")))
                        End If

                        If Not fltpprice.Contains(CLng(drow("ftoken"))) Then
                            fltpprice.Add(CLng(drow("ftoken")), drow("curSpot"))
                        End If
                    End If


                    'REM1 :keval(04-08-2010)
                    'changes Done by nasima on 10 th aug
                    '=========================================
                    'if perticular options future is not there in hashtable then add it
                    If IsDBNull(drow("fut_mdate")) = True Then
                        If IsDBNull(drow("mdate")) = False Then
                            drow("fut_mdate") = drow("mdate")
                        End If
                    End If
                    'REM 1: END
                    If arr(3) = CDate(drow("fut_mdate")) Then
                        ' If arr(3) = CDate(drow("mdate")) Then
                        If Not fltpprice.Contains(CLng(drow("ftoken"))) Then
                            fltpprice.Add(CLng(drow("ftoken")), CDbl(arr(2)))
                        End If
                    ElseIf arr(5) = CDate(drow("fut_mdate")) Then
                        ' ElseIf arr(5) = CDate(drow("mdate")) Then
                        If Not fltpprice.Contains(CLng(drow("ftoken"))) Then
                            fltpprice.Add(CLng(drow("ftoken")), CDbl(arr(4)))
                        End If
                        '  ElseIf arr(7) = CDate(drow("mdate")) Then
                    ElseIf arr(7) = CDate(drow("fut_mdate")) Then
                        If Not fltpprice.Contains(CLng(drow("ftoken"))) Then
                            fltpprice.Add(CLng(drow("ftoken")), CDbl(arr(6)))
                        End If
                    End If
                    'end of perticular options future is not there in hashtable then add it
                End If
            Next

            REM Calculate Lot Size  By Viral
            Dim fdv As DataView
            If VarIsCurrency = True Then
                fdv = New DataView(Currencymaster, "symbol='" & GetSymbol(str) & "' and option_type not in ('CA','CE','PA','PE') ", "symbol", DataViewRowState.CurrentRows)
                txtlot.Text = IIf(IsDBNull(fdv.ToTable(True, "lotsize", "multiplier").Compute("max(multiplier) ", "")), 0, fdv.ToTable(True, "lotsize", "multiplier").Compute("max(multiplier)", ""))
                'delta
                If CURR_DELTA_BASE = Setting_Greeks_BASE.Lot Then
                    iDeltaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iDeltaLSize = 1
                End If

                'Gamma
                If CURR_GAMMA_BASE = Setting_Greeks_BASE.Lot Then
                    iGammaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iGammaLSize = 1
                End If

                'Vega
                If CURR_VEGA_BASE = Setting_Greeks_BASE.Lot Then
                    iVegaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iVegaLSize = 1
                End If

                'Theta
                If CURR_THETA_BASE = Setting_Greeks_BASE.Lot Then
                    iThetaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iThetaLSize = 1
                End If

                'Volga
                If CURR_VOLGA_BASE = Setting_Greeks_BASE.Lot Then
                    iVolgaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iVolgaLSize = 1
                End If

                'vanna
                If CURR_VANNA_BASE = Setting_Greeks_BASE.Lot Then
                    iVannaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iVannaLSize = 1
                End If

            Else
                fdv = New DataView(scripttable, "symbol='" & GetSymbol(str) & "' and option_type not in ('CA','CE','PA','PE') ", "symbol", DataViewRowState.CurrentRows)
                txtlot.Text = IIf(IsDBNull(fdv.ToTable(True, "lotsize").Compute("max(lotsize)", "")), 0, fdv.ToTable(True, "lotsize").Compute("max(lotsize)", ""))
                'Delta
                If EQ_DELTA_BASE = Setting_Greeks_BASE.Lot Then
                    iDeltaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iDeltaLSize = 1
                End If

                'Gamma
                If EQ_GAMMA_BASE = Setting_Greeks_BASE.Lot Then
                    iGammaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iGammaLSize = 1
                End If

                'Vega
                If EQ_VEGA_BASE = Setting_Greeks_BASE.Lot Then
                    iVegaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iVegaLSize = 1
                End If

                'Theta
                If EQ_THETA_BASE = Setting_Greeks_BASE.Lot Then
                    iThetaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iThetaLSize = 1
                End If

                'Volga
                If EQ_VOLGA_BASE = Setting_Greeks_BASE.Lot Then
                    iVolgaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iVolgaLSize = 1
                End If

                'Vanna
                If EQ_VANNA_BASE = Setting_Greeks_BASE.Lot Then
                    iVannaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                Else
                    iVannaLSize = 1
                End If

            End If

            txtcvol.Text = arr(0)
            txtpvol.Text = arr(1)
            txtcvol1.Text = arr(8)
            txtpvol1.Text = arr(9)
            txtcvol2.Text = arr(10)
            txtpvol2.Text = arr(11)
            txteqrate.Text = arr(12)
            'Alpesh 
            txtfut1.Text = arr(2)
            txtfut2.Text = arr(4)
            txtfut3.Text = arr(6)
            'txtexp.Text = Format(CDate(arr(3)), "MMM yy")
            'txtexp1.Text = Format(CDate(arr(5)), "MMM yy")
            'txtexp2.Text = Format(CDate(arr(7)), "MMM yy")

            Dim i As Integer
            i = 0
            Dim mdate As New ArrayList


            REM coding by mahesh to set the date for the far month expiry

            Dim temp_cur_date_second As Long
            Dim temp_today_date_second As Long
            temp_today_date_second = DateDiff(DateInterval.Second, CDate("1/1/1980"), CDate(Format(Now, "MMM/dd/yyyy")))
            'get current month maturity from the contract

            'temp_cur_mdate_month = (CDate(Format(Now, "MM/"MMM/dd/yyyy"ear * 12) + CDate(Format(Now, "MMM/dd/yyyy")).Month
            If VarIsCurrency = True Then
                temp_cur_date_second = IIf(IsDBNull(Currencymaster.Compute("min(expiry_date)", "symbol='" & GetSymbol(str) & " ' and expiry_date >= " & temp_today_date_second)), 0, Currencymaster.Compute("min(expiry_date)", "symbol='" & GetSymbol(str) & " ' and expiry_date >= " & temp_today_date_second))
            Else
                temp_cur_date_second = IIf(IsDBNull(scripttable.Compute("min(expiry_date)", "symbol='" & GetSymbol(str) & " ' and expiry_date >= " & temp_today_date_second)), 0, scripttable.Compute("min(expiry_date)", "symbol='" & GetSymbol(str) & " ' and expiry_date >= " & temp_today_date_second))
            End If

            Maturity_Cur_month = DateAdd(DateInterval.Second, temp_cur_date_second, CDate("1/1/1980"))

            Maturity_first_month = (Maturity_Cur_month.Year * 12) + Maturity_Cur_month.Month
            Maturity_second_month = (Maturity_Cur_month.Year * 12) + Maturity_Cur_month.Month + 1
            Maturity_third_month = (Maturity_Cur_month.Year * 12) + Maturity_Cur_month.Month + 2
            Dim str1(0) As String
            str1(0) = "fut_mdate"
            ' str1(1) = "cp"
            Dim SelTabName As String
            SelTabName = TabStrategy.SelectedTab.Name
            'end of coding for far month expiry
            'Determine how many panel of expiry date will be visible
            For Each drow As DataRow In dv.ToTable(True, str1).Select("", "fut_mdate") 'get only cp,exipry date
                If Not mdate.Contains(drow("fut_mdate")) Then
                    mdate.Add(drow("fut_mdate")) 'add expiry date to mdate
                    'select first maturity date and visible first panel
                    If i = 0 And CDate(drow("fut_mdate")) >= Today Then  'And ((CDate(drow("fut_mdate")).Year * 12) + CDate(drow("fut_mdate")).Month) <= Maturity_third_month Then
                        If SelTabName = "TP_All" Then
                            dtexp.Value = CDate(drow("fut_mdate"))
                            txtdays.Text = DateDiff(DateInterval.Day, Now(), CDate(dtexp.Value)) + 1 ' DateDiff(DateInterval.Day, CDate(dtexp.Value), Now())
                            pan1.Visible = True
                            pan11 = True
                            pan1.Refresh()
                            txtfut1.Visible = True
                            'TableLayoutPanel1.RowStyles(1).Height = 26
                            lfut1.Visible = True
                            date1 = dtexp.Value.Date
                            txtexp.Text = Format(CDate(drow("fut_mdate")), "MMM yy")
                            If CDate(arr(3)) = CDate(drow("fut_mdate")) Then
                                txtfut1.Text = arr(2)
                            End If
                            i = i + 1
                            'TabControl1.TabPages(1).Visible = True
                            'TabControl1.TabPages(1).ToolTipText = ""
                        Else
                            'If TabControl1.SelectedIndex = 1 Then TabControl1.SelectedIndex = 0
                            'TabControl1.TabPages(1).ToolTipText = "Disable this Tab For Individual Month"
                        End If

                        'Rem Cal For Call1,Call2,Call3,Call4 
                        Dim SelTab As Integer
                        If TabStrategy.SelectedIndex = 0 Then
                            SelTab = 0
                        Else
                            If TabStrategy.SelectedIndex <= 3 Then
                                If TabStrategy.SelectedIndex Mod 3 = 0 Then
                                    SelTab = 3
                                Else
                                    SelTab = TabStrategy.SelectedIndex Mod 3
                                End If
                            Else
                                If GVarMaturity_Far_month = "Current Month" Then
                                    SelTab = 1
                                ElseIf GVarMaturity_Far_month = "Next Month" Then
                                    SelTab = 2
                                ElseIf GVarMaturity_Far_month = "Next Month" Then
                                    SelTab = 3
                                End If
                            End If
                        End If

                        REM  Broadcast is not stop, While swiching from one month tab to another tab
                        If thrworking = False Then
                            If Val(txtcvol.Text) <> 0 Or Val(txtpvol.Text) <> 0 Or Val(txtcvol1.Text) <> 0 Or Val(txtpvol1.Text) <> 0 Or Val(txtcvol2.Text) <> 0 Or Val(txtpvol2.Text) <> 0 Then
                                ApplyVolFlg = True
                            Else
                                ApplyVolFlg = False
                            End If
                        End If
                        '' ''        txtfut1.Text = arr(2)
                        '' ''        txtfut2.Text = arr(4)
                        '' ''        txtfut3.Text = arr(6)
                        Select Case SelTab
                            Case 0    REM For All
                                pan11 = True
                                pan21 = True
                                pan31 = True
                                cmdsave_Click()
                                'UpdateAutomatic(ApplyVolFlg)
                            Case 1
                                pan11 = True
                                pan21 = False
                                pan31 = False
                                cmdsave_Click()
                                'UpdateAutomatic(ApplyVolFlg)
                            Case 2
                                pan11 = False
                                pan21 = True
                                pan31 = False
                                cmdsave_Click()
                                'UpdateAutomatic(ApplyVolFlg)
                            Case 3
                                pan11 = False
                                pan21 = False
                                pan31 = True
                                cmdsave_Click()
                                'UpdateAutomatic(ApplyVolFlg)
                        End Select


                        '' ''Dim sender As Object
                        '' ''Dim e As System.EventArgs
                        '' ''Select Case TabStrategy.SelectedIndex
                        '' ''    Case 0    REM For All
                        '' ''        txtcvol.Text = arr(0)
                        '' ''        txtpvol.Text = arr(1)
                        '' ''        txtcvol1.Text = arr(8)
                        '' ''        txtpvol1.Text = arr(9)
                        '' ''        txtcvol2.Text = arr(10)
                        '' ''        txtpvol2.Text = arr(11)
                        '' ''        txtfut1.Text = arr(2)
                        '' ''        txtfut2.Text = arr(4)
                        '' ''        txtfut3.Text = arr(6)
                        '' ''    Case 1
                        '' ''        save_apply()
                        '' ''        'txtcvol.Text = arr(0)
                        '' ''        'txtpvol.Text = arr(1)
                        '' ''        'txtcvol1.Text = arr(8)
                        '' ''        'txtpvol1.Text = arr(9)
                        '' ''        'txtcvol2.Text = arr(10)
                        '' ''        'txtpvol2.Text = arr(11)
                        '' ''    Case 2
                        '' ''        txtcvol.Text = arr(8)
                        '' ''        txtpvol.Text = arr(9)
                        '' ''        txtfut1.Text = arr(4)
                        '' ''        txtcvol1.Text = arr(8)
                        '' ''        txtpvol1.Text = arr(9)
                        '' ''        txtfut2.Text = arr(4)
                        '' ''        txtcvol2.Text = arr(10)
                        '' ''        txtpvol2.Text = arr(11)
                        '' ''        txtfut3.Text = arr(6)

                        '' ''        save_apply()
                        '' ''        'UpdateAutomatic(True)
                        '' ''        'txtfut1_LostFocus(sender, e)
                        '' ''    Case 3
                        '' ''        txtcvol.Text = arr(10)
                        '' ''        txtpvol.Text = arr(11)
                        '' ''        txtfut1.Text = arr(6)
                        '' ''        txtcvol1.Text = arr(8)
                        '' ''        txtpvol1.Text = arr(9)
                        '' ''        txtfut2.Text = arr(4)
                        '' ''        txtcvol2.Text = arr(10)
                        '' ''        txtpvol2.Text = arr(11)
                        '' ''        txtfut3.Text = arr(6)


                        '' ''        save_apply()
                        '' ''        'UpdateAutomatic(True)
                        '' ''        'txtfut1_LostFocus(sender, e)
                        '' ''End Select

                        'select second maturity date and visible second panel
                    ElseIf i = 1 And CDate(drow("fut_mdate")) >= CDate(Format(Now, "MMM/dd/yyyy")) And ((CDate(drow("fut_mdate")).Year * 12) + CDate(drow("fut_mdate")).Month) <= Maturity_third_month Then
                        dtexp1.Value = CDate(drow("fut_mdate"))
                        txtdays1.Text = DateDiff(DateInterval.Day, Now(), CDate(dtexp1.Value)) + 1 ' DateDiff(DateInterval.Day, CDate(dtexp.Value), Now())
                        pan2.Visible = True
                        pan21 = True
                        pan2.Refresh()
                        txtfut2.Visible = True
                        lfut2.Visible = True
                        TableLayoutPanel1.RowStyles(2).Height = 26
                        date2 = dtexp1.Value.Date
                        txtexp1.Text = Format(CDate(drow("fut_mdate")), "MMM yy")
                        If CDate(arr(5)) = CDate(drow("fut_mdate")) Then
                            txtfut2.Text = arr(4)
                        End If
                        i = i + 1
                        'select third maturity date and visible third panel
                    ElseIf i = 2 And CDate(drow("fut_mdate")) >= CDate(Format(Now, "MMM/dd/yyyy")) And ((CDate(drow("fut_mdate")).Year * 12) + CDate(drow("fut_mdate")).Month) <= Maturity_third_month Then
                        dtexp2.Value = CDate(drow("fut_mdate"))
                        txtdays2.Text = DateDiff(DateInterval.Day, Now(), CDate(dtexp2.Value)) + 1 ' DateDiff(DateInterval.Day, CDate(dtexp.Value), Now())
                        pan3.Visible = True
                        pan3.Refresh()
                        TableLayoutPanel1.RowStyles(3).Height = 26
                        pan31 = True
                        txtfut3.Visible = True
                        lfut3.Visible = True
                        date3 = dtexp2.Value.Date
                        txtexp2.Text = Format(CDate(drow("fut_mdate")), "MMM yy")
                        If CDate(arr(7)) = CDate(drow("fut_mdate")) Then
                            txtfut3.Text = arr(6)
                        End If
                        i = i + 1
                        Exit For
                    End If
                End If
            Next
            'Dim DtMMonth As DataTable
            'If VarIsCurrency = True Then
            '    DtMMonth = New DataView(Currencymaster, "Option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1")
            'Else
            '    DtMMonth = New DataView(cpfmaster, "Option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1")
            'End If
            'If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
            '    Maturity_Far_month = DtMMonth.Rows(0).Item("expdate1")
            'ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
            '    Maturity_Far_month = DtMMonth.Rows(1).Item("expdate1")
            'ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
            '    Maturity_Far_month = DtMMonth.Rows(2).Item("expdate1")
            'End If

            'if maturity array does not containt the far month maturity then add it to the first line
            'If Not mdate.Contains(Maturity_Far_month) Then 'add far_maturity date
            '    'mdate.Add(Maturity_Far_month)
            '    'if noone first, second, third panels' flag true then far_month expiry date is considered 
            '    If pan11 = False Then
            '        dtexp.Value = Maturity_Far_month
            '        txtdays.Text = DateDiff(DateInterval.Day, Now(), CDate(dtexp.Value)) + 1 ' DateDiff(DateInterval.Day, CDate(dtexp.Value), Now())
            '        pan1.Visible = True
            '        pan11 = True
            '        pan1.Refresh()
            '        txtfut1.Visible = True
            '        'TableLayoutPanel1.RowStyles(1).Height = 26

            '        lfut1.Visible = True
            '        date1 = dtexp.Value.Date
            '        txtexp.Text = Format(Maturity_Far_month, "MMM yy")
            '        If CDate(arr(3)) = Maturity_Far_month Then
            '            txtfut1.Text = arr(2)
            '        ElseIf CDate(arr(5)) = Maturity_Far_month Then
            '            txtfut1.Text = arr(4)
            '        ElseIf CDate(arr(7)) = Maturity_Far_month Then
            '            txtfut1.Text = arr(6)
            '        End If
            '    ElseIf pan21 = False Then
            '        dtexp1.Value = Maturity_Far_month
            '        txtdays1.Text = DateDiff(DateInterval.Day, Now(), CDate(dtexp1.Value)) + 1 ' DateDiff(DateInterval.Day, CDate(dtexp.Value), Now())
            '        pan2.Visible = True
            '        pan2.Refresh()
            '        pan21 = True
            '        txtfut2.Visible = True
            '        lfut2.Visible = True
            '        TableLayoutPanel1.RowStyles(2).Height = 26
            '        date2 = dtexp1.Value.Date
            '        txtexp1.Text = Format(Maturity_Far_month, "MMM yy")
            '        If CDate(arr(3)) = Maturity_Far_month Then
            '            txtfut2.Text = arr(2)
            '        ElseIf CDate(arr(5)) = Maturity_Far_month Then
            '            txtfut2.Text = arr(4)
            '        ElseIf CDate(arr(7)) = Maturity_Far_month Then
            '            txtfut2.Text = arr(6)
            '        End If
            '    ElseIf pan31 = False Then
            '        dtexp2.Value = Maturity_Far_month
            '        txtdays2.Text = DateDiff(DateInterval.Day, Now(), CDate(dtexp2.Value)) + 1 ' DateDiff(DateInterval.Day, CDate(dtexp.Value), Now())
            '        pan3.Visible = True
            '        pan3.Refresh()
            '        TableLayoutPanel1.RowStyles(3).Height = 26
            '        pan31 = True
            '        txtfut3.Visible = True
            '        lfut3.Visible = True
            '        date3 = dtexp2.Value.Date
            '        txtexp2.Text = Format(Maturity_Far_month, "MMM yy")
            '        If CDate(arr(3)) = Maturity_Far_month Then
            '            txtfut3.Text = arr(2)
            '        ElseIf CDate(arr(5)) = Maturity_Far_month Then
            '            txtfut3.Text = arr(4)
            '        ElseIf CDate(arr(7)) = Maturity_Far_month Then
            '            txtfut3.Text = arr(6)
            '        End If
            '    End If
            'End If

            For Each drow As DataRow In fdv.ToTable.Rows
                If pan1.Visible = True And dtexp.Value = CDate(drow("expdate1")) Then
                    If Not futarray.Contains(CLng(drow("token"))) Then
                        futarray.Add(CLng(drow("token")))
                        valToken = objAna.ltp_token(CLng(drow("token")), drow("script"), str)
                        If valToken <> 0 Then
                            If Not fltpprice.Contains(CLng(drow("token"))) Then
                                fltpprice.Add(CLng(drow("token")), valToken)
                            End If
                        Else
                            If arr(3) = CDate(drow("expdate1")) Then
                                If Not fltpprice.Contains(CLng(drow("token"))) Then
                                    fltpprice.Add(CLng(drow("token")), CDbl(arr(2)))
                                End If
                            Else
                                If Not fltpprice.Contains(CLng(drow("token"))) Then
                                    fltpprice.Add(CLng(drow("token")), 0)
                                End If
                            End If
                        End If
                    End If
                ElseIf pan2.Visible = True And dtexp1.Value = CDate(drow("expdate1")) Then
                    If Not futarray.Contains(CLng(drow("token"))) Then
                        futarray.Add(CLng(drow("token")))
                        valToken = objAna.ltp_token(CLng(drow("token")), drow("script"), str)
                        If valToken <> 0 Then
                            If Not fltpprice.Contains(CLng(drow("token"))) Then
                                fltpprice.Add(CLng(drow("token")), valToken)
                            End If
                        Else
                            If arr(5) = CDate(drow("expdate1")) Then
                                If Not fltpprice.Contains(CLng(drow("token"))) Then
                                    fltpprice.Add(CLng(drow("token")), CDbl(arr(4)))
                                End If

                            End If
                        End If
                    End If
                ElseIf pan3.Visible = True And dtexp2.Value = CDate(drow("expdate1")) Then
                    If Not futarray.Contains(CLng(drow("token"))) Then
                        futarray.Add(CLng(drow("token")))
                        valToken = objAna.ltp_token(CLng(drow("token")), drow("script"), str)
                        If valToken <> 0 Then
                            If Not fltpprice.Contains(CLng(drow("token"))) Then
                                fltpprice.Add(CLng(drow("token")), valToken)
                            End If
                        Else
                            If arr(7) = CDate(drow("expdate1")) Then
                                If Not fltpprice.Contains(CLng(drow("token"))) Then
                                    fltpprice.Add(CLng(drow("token")), CDbl(arr(6)))
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            'Alpesh  20/04/2011
            REM In Analysis window, Previous & Today Expense for All tab & different months tab come as per Tab Selection
            If TabStrategy.SelectedTab.Name = "TP_All" Then
                txtprexp.Text = -Format(Math.Abs(CFexpense) + Math.Abs(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & compname & "' and entry_date < #" & fDate(Today.Date) & "# and exp_date >= #" & fDate(Today) & "#").ToString) + PatchExpDiff), Expensestr)
                txttexp.Text = -Format(Math.Abs(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & compname & "' and entry_date >= #" & fDate(Today.Date) & "# and exp_date >= #" & fDate(Today) & "#").ToString)), Expensestr)
                txttotexp.Text = -(Math.Abs(Val(txtprexp.Text)) + Math.Abs(Val(txttexp.Text)))
            Else
                txtprexp.Text = -Format(Math.Abs(CFexpense) + Math.Abs(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & compname & "' and exp_date=#" & TabStrategy.SelectedTab.Tag & "# and entry_date<#" & fDate(Today.Date) & "# and exp_date >= #" & fDate(Today) & "#").ToString) + PatchExpDiff), Expensestr)
                txttexp.Text = -Format(Math.Abs(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & compname & "'  AND exp_date=#" & TabStrategy.SelectedTab.Tag & "# AND entry_date>= #" & fDate(Today.Date) & "# and exp_date >= #" & fDate(Today) & "#").ToString)), Expensestr)
                txttotexp.Text = -(Math.Abs(Val(txtprexp.Text)) + Math.Abs(Val(txttexp.Text)))
            End If

            '' ''If VarIsCurrency = True Then
            '' ''    txtprexp.Text = -Format(Math.Abs(Val(prebal.Compute("sum(tot)", "company='" & compname & "' and tdate < #" & Format(Today, "dd-MMM-yyyy") & "#").ToString)), CurrencyExpenseStr)
            '' ''    txttexp.Text = -Format(Math.Abs(Val(prebal.Compute("sum(tot)", "company='" & compname & "' and tdate = #" & Format(Today, "dd-MMM-yyyy") & "#").ToString)), CurrencyExpenseStr)
            '' ''    txttotexp.Text = -(Math.Abs(Val(txtprexp.Text)) + Math.Abs(Val(txttexp.Text)))
            '' ''Else
            '' ''    txtprexp.Text = -Format(Math.Abs(Val(prebal.Compute("sum(tot)", "company='" & compname & "' and tdate < #" & Format(Today, "dd-MMM-yyyy") & "#").ToString)), Expensestr)
            '' ''    txttexp.Text = -Format(Math.Abs(Val(prebal.Compute("sum(tot)", "company='" & compname & "' and tdate = #" & Format(Today, "dd-MMM-yyyy") & "#").ToString)), Expensestr)
            '' ''    txttotexp.Text = -(Math.Abs(Val(txtprexp.Text)) + Math.Abs(Val(txttexp.Text)))
            '' ''End If

            'this will keep the old status of start/stop mode or future only mode for trade referesh or new position from open position form
            If thrworking = True And thrworking_future_only = False Then 'online mode
                If Not mo Is Nothing Then
                    calculate_tab(True)
                Else

                    calculate_tab()
                End If
                ' call_function()
                cal_eq()
                cal_future()
                cal_exp_Margin()
                get_value()
            ElseIf thrworking = False And thrworking_future_only = False Then 'offline mode
                If Not mo Is Nothing Then
                    calculate_tab(True)
                Else
                    calculate_tab()
                End If
                ' call_function()
                cal_eq()
                cal_future()
                cal_exp_Margin()
                get_value()
            ElseIf thrworking = False And thrworking_future_only = True Then 'future update only mopde
                thrworking_future_only = False
                If Save_applied = False Then 'previously online mode
                    If Not mo Is Nothing Then
                        calculate_tab(True)
                    Else
                        calculate_tab()
                    End If
                    ' call_function()
                    cal_exp_Margin()
                    get_value()
                Else 'previously offline mode
                    save_apply()
                    cal_exp_Margin()
                    get_value()
                End If
                thrworking_future_only = True

                cal_eq()
                cal_future()

            End If

            'If zero_analysis <> 0 Then
            '    gcurrtable.DefaultView.RowFilter = "units<>0"
            'End If

            ''divyesh
            If zero_analysis <> 0 Then
                currtable.DefaultView.RowFilter = "units<>0"
            End If

            'If VarIsCurrency = False Then
            '    For Each drow As DataRow In gcurrtable.Rows
            '        If drow("cp") = "F" Then
            '            drow("Lots") = Val(drow("units") / cpfmaster.Compute("MAX(lotsize)", "script='" & drow("script") & "'"))
            '        ElseIf drow("cp") = "E" Then
            '            Exit For
            '        End If
            '    Next
            'End If


            ''divyesh
            ''Call AssignBhavcopyLTP()

            REM impliment Reset FixVol While Not Set Vol  By Viral
            If thrworking = False And thrworking_future_only = False Then


                If txtcvol.Visible = True Then
                    If Not (Val(txtcvol.Text) <> 0 And Val(txtpvol.Text) <> 0) Then
                        MDI.ReSetFixVolToolStripMenuItem_Click(MDI.ReSetFixVolToolStripMenuItem, New EventArgs)
                    End If
                End If
                If txtcvol1.Visible = True Then
                    If Not (Val(txtcvol1.Text) <> 0 And Val(txtpvol1.Text) <> 0) Then
                        MDI.ReSetFixVolToolStripMenuItem_Click(MDI.ReSetFixVolToolStripMenuItem, New EventArgs)
                    End If
                End If
                If txtcvol2.Visible = True Then
                    If Not (Val(txtcvol2.Text) <> 0 And Val(txtpvol2.Text) <> 0) Then
                        MDI.ReSetFixVolToolStripMenuItem_Click(MDI.ReSetFixVolToolStripMenuItem, New EventArgs)
                    End If
                End If
            End If
            'If Not (Val(txtcvol.Text) <> 0 And Val(txtpvol.Text) <> 0) Or (Val(txtcvol1.Text) <> 0 And Val(txtpvol1.Text) <> 0) Or (Val(txtcvol2.Text) <> 0 And Val(txtpvol2.Text) <> 0) Then
            '    'Call AssignBhavcopyLTP(True)
            '    MDI.ReSetFixVolToolStripMenuItem_Click(MDI.ReSetFixVolToolStripMenuItem, New EventArgs)
            'End If

            Call AssignLots()
            DGTrading.DataSource = currtable
            'DGTrading.DataSource = gcurrtable
            DGTrading.Refresh()


            Call SetGrid_Rounding()
            'DGTrading.Columns("totExp").DisplayIndex = DGTrading.Columns("grossmtm").DisplayIndex + 1
            DGTrading.Columns("asset_tokan").Visible = False
            DGTrading.Columns("prExp").Visible = False
            DGTrading.Columns("toExp").Visible = False
            DGTrading.Columns("deltavval").Visible = False ' 53 = Deltavval index
            'DGTrading.Columns(53).Visible = False


            REM Use To Hide Previous Delat & Current Delta
            'DGTrading.Columns(54).Visible = False
            'DGTrading.Columns(55).Visible = False
            'DGTrading.Columns(56).Visible = False
            'DGTrading.Columns(57).Visible = False
            'DGTrading.Columns(58).Visible = False
            'DGTrading.Columns(59).Visible = False
            'DGTrading.Columns(60).Visible = False
            'DGTrading.Columns(61).Visible = False
            'DGTrading.Columns(62).Visible = False
            'DGTrading.Columns(63).Visible = False
            'DGTrading.Columns(64).Visible = False

            'DGTrading.Columns(65).Visible = False
            'DGTrading.Columns(66).Visible = False
            'DGTrading.Columns(67).Visible = False
            'DGTrading.Columns(68).Visible = False

            If chkLtp.Checked = True Or chkVol.Checked = True Then
                Call cal_projMTM_plusMinus("P")
                Call cal_projMTM_plusMinus("M")
            End If


            If Panel1.Visible = False Then TableLayoutPanel1.RowStyles(2).Height = 0
            If Panel3.Visible = False Then TableLayoutPanel1.RowStyles(3).Height = 0
            'display expense

            MDI.ToolStripcompanyCombo.Text = compname

            'Dim ed As System.Windows.Forms.DrawItemEventArgs
            'TabControl1_DrawItem(TabControl1, ed)

            FSTimerLogFile.WriteLine("Change_tab Process End :: " & System.Environment.TickCount - T1)
            T1 = System.Environment.TickCount

        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    ''' <summary>
    ''' AssignBhavcopyLTP
    ''' </summary>
    ''' <remarks>This method call to Assign LTP price from Bhav copy</remarks>
    Public Sub AssignBhavcopyLTP(ByVal isfrombcopyprocess As Boolean)
        If GdtBhavcopy.Rows.Count > 0 Then
            LastBhavcopyDate = (GdtBhavcopy.Compute("MAX(entry_date)", ""))
            For Each drow As DataRow In currtable.Rows
                If drow("last") = 0 Or GVarIsNewBhavcopy = True Or isfrombcopyprocess = True Then
                    If drow("IsCurrency") = False Then
                        If drow("cp") = "F" Or drow("cp") = "C" Or drow("cp") = "P" Then
                            drow("last") = Format(Val(fltpprice(CLng(drow("tokanno")))), "#0.00")

                            GoTo lbl1
                        ElseIf drow("cp") = "E" Then
                            drow("last") = Format(Val(eltpprice(CLng(drow("tokanno")))), "#0.00")
                            GoTo lbl1
                        End If
                    Else
                        drow("last") = Val(Currltpprice(CLng(drow("tokanno"))))
                    End If
                End If
lbl1:
                If GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and script='" & drow("script") & "'").Length > 0 Then
                    If drow("last") = 0 Or drow("last") <> GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and script='" & drow("script") & "'")(0).Item("ltp") Then
                        If GdtBhavcopy.Select("script='" & drow("script") & "'").Length > 0 Then
                            If isfrombcopyprocess = False Then
                                drow("last") = Format(GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and script='" & drow("script") & "'")(0).Item("ltp"), "#0.00")
                            End If
                        End If
                    End If
                End If

                Dim fltppr As Double = Val(fltpprice(CLng(drow("ftoken"))))
                Dim ltppr As Double = Val(drow("last") & "")
                Dim mt As Double = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                mt = mt / 365
                Dim iscall As Boolean = False
                Dim mIsFut As Boolean = False
                If drow("cp") = "C" Then
                    iscall = True
                    mIsFut = False
                ElseIf drow("cp") = "P" Then
                    iscall = False
                    mIsFut = False
                ElseIf drow("cp") = "F" Then
                    mIsFut = True
                    iscall = False
                End If
                Dim Strikeprice As Double = Val(drow("strikes") & "")

                Dim mVolatility As Double
                If mIsFut = False Then
                    Try
                        mVolatility = Greeks.Black_Scholes(fltppr, Strikeprice, Mrateofinterast, 0, ltppr, mt, iscall, mIsFut, 0, 6)
                    Catch ex As Exception
                        REM 'While Discount Vol
                        mVolatility = 0.01
                    End Try
                    drow("lv") = mVolatility * 100
                End If
            Next
        Else
            For Each drow As DataRow In currtable.Rows
                If drow("last") = 0 Then
                    If drow("IsCurrency") = False Then
                        If drow("cp") = "F" Or drow("cp") = "C" Or drow("cp") = "P" Then
                            drow("last") = Format(Val(fltpprice(CLng(drow("tokanno")))), "#0.00")
                        ElseIf drow("cp") = "E" Then
                            drow("last") = Format(Val(eltpprice(CLng(drow("tokanno")))), "#0.00")
                        End If
                    Else
                        drow("last") = Val(Currltpprice(CLng(drow("tokanno"))))
                    End If
                End If
            Next
        End If

        '        For Each drow As DataRow In gcurrtable.Rows
        '            If drow("last") = 0 Then
        '                If drow("IsCurrency") = False Then
        '                    If drow("cp") = "F" Or drow("cp") = "C" Or drow("cp") = "P" Then
        '                        drow("last") = Format(Val(fltpprice(CLng(drow("tokanno")))), "#0.00")
        '                        GoTo lbl1
        '                    ElseIf drow("cp") = "E" Then
        '                        drow("last") = Format(Val(eltpprice(CLng(drow("tokanno")))), "#0.00")
        '                        GoTo lbl1
        '                    End If
        '                Else
        '                    drow("last") = Val(Currltpprice(CLng(drow("tokanno"))))
        '                End If
        '            End If
        'lbl1:
        '            If drow("last") = 0 Or drow("last") <> GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and script='" & drow("script") & "'")(0).Item("ltp") Then
        '                If GdtBhavcopy.Select("script='" & drow("script") & "'").Length > 0 Then
        '                    drow("last") = Format(GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and script='" & drow("script") & "'")(0).Item("ltp"), "#0.00")
        '                Else
        '                    drow("last") = 0
        '                End If
        '                IsBhavcopyImported = False
        '            End If
        '        Next
    End Sub

    ' ''    Public Sub AssignBhavcopyLTP()
    ' ''        If GdtBhavcopy.Rows.Count > 0 Then
    ' ''            LastBhavcopyDate = (GdtBhavcopy.Compute("MAX(entry_date)", ""))
    ' ''            For Each drow As DataRow In currtable.Rows
    ' ''                If drow("last") = 0 Or GVarIsNewBhavcopy = True Then
    ' ''                    If drow("IsCurrency") = False Then
    ' ''                        If drow("cp") = "F" Or drow("cp") = "C" Or drow("cp") = "P" Then
    ' ''                            drow("last") = Format(Val(fltpprice(CLng(drow("tokanno")))), "#0.00")
    ' ''                            GoTo lbl1
    ' ''                        ElseIf drow("cp") = "E" Then
    ' ''                            drow("last") = Format(Val(eltpprice(CLng(drow("tokanno")))), "#0.00")
    ' ''                            GoTo lbl1
    ' ''                        End If
    ' ''                    Else
    ' ''                        drow("last") = Val(Currltpprice(CLng(drow("tokanno"))))
    ' ''                    End If
    ' ''                End If
    ' ''lbl1:
    ' ''                If GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and script='" & drow("script") & "'").Length > 0 Then
    ' ''                    If drow("last") = 0 Or drow("last") <> GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and script='" & drow("script") & "'")(0).Item("ltp") Then
    ' ''                        If GdtBhavcopy.Select("script='" & drow("script") & "'").Length > 0 Then
    ' ''                            drow("last") = Format(GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and script='" & drow("script") & "'")(0).Item("ltp"), "#0.00")
    ' ''                        End If
    ' ''                    End If
    ' ''                End If
    ' ''            Next
    ' ''        Else
    ' ''            For Each drow As DataRow In currtable.Rows
    ' ''                If drow("last") = 0 Then
    ' ''                    If drow("IsCurrency") = False Then
    ' ''                        If drow("cp") = "F" Or drow("cp") = "C" Or drow("cp") = "P" Then
    ' ''                            drow("last") = Format(Val(fltpprice(CLng(drow("tokanno")))), "#0.00")
    ' ''                        ElseIf drow("cp") = "E" Then
    ' ''                            drow("last") = Format(Val(eltpprice(CLng(drow("tokanno")))), "#0.00")
    ' ''                        End If
    ' ''                    Else
    ' ''                        drow("last") = Val(Currltpprice(CLng(drow("tokanno"))))
    ' ''                    End If
    ' ''                End If
    ' ''            Next
    ' ''        End If

    ' ''        '        For Each drow As DataRow In gcurrtable.Rows
    ' ''        '            If drow("last") = 0 Then
    ' ''        '                If drow("IsCurrency") = False Then
    ' ''        '                    If drow("cp") = "F" Or drow("cp") = "C" Or drow("cp") = "P" Then
    ' ''        '                        drow("last") = Format(Val(fltpprice(CLng(drow("tokanno")))), "#0.00")
    ' ''        '                        GoTo lbl1
    ' ''        '                    ElseIf drow("cp") = "E" Then
    ' ''        '                        drow("last") = Format(Val(eltpprice(CLng(drow("tokanno")))), "#0.00")
    ' ''        '                        GoTo lbl1
    ' ''        '                    End If
    ' ''        '                Else
    ' ''        '                    drow("last") = Val(Currltpprice(CLng(drow("tokanno"))))
    ' ''        '                End If
    ' ''        '            End If
    ' ''        'lbl1:
    ' ''        '            If drow("last") = 0 Or drow("last") <> GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and script='" & drow("script") & "'")(0).Item("ltp") Then
    ' ''        '                If GdtBhavcopy.Select("script='" & drow("script") & "'").Length > 0 Then
    ' ''        '                    drow("last") = Format(GdtBhavcopy.Select("entry_date='" & LastBhavcopyDate & "' and script='" & drow("script") & "'")(0).Item("ltp"), "#0.00")
    ' ''        '                Else
    ' ''        '                    drow("last") = 0
    ' ''        '                End If
    ' ''        '                IsBhavcopyImported = False
    ' ''        '            End If
    ' ''        '        Next
    ' ''    End Sub
    ''' <summary>
    ''' AssignLots
    ''' </summary>
    ''' <remarks>This method call to Assign lot to script</remarks>
    Private Sub AssignLots()
        REM After importing old Contract/Security master (i.e. March month files) and then on clicking Analysis sub-menu item of File menu, an Unhandled Exception is not Occurred and Analysis window is  functioning
        If VarIsCurrency = False Then
            For Each drow As DataRow In currtable.Select("cp<>'E'")
                drow("Lots") = IIf(IsDBNull(drow("Lots")) = True, 0, drow("Lots"))
                If drow("Lots") = 0 Then
                    'drow("Lots") = Val(drow("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                    'Val(drow("units")) / IIf(IsDBNull(cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")) = True, 1, cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'"))
                    REM  For cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'") Dbnull  Alpesh 
                    If IsDBNull(cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")) = True Then
                        drow("Lots") = 0
                    Else
                        drow("Lots") = Val(drow("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                    End If
                    'ElseIf drow("cp") = "E" Then
                    '    drow("Lots") = Val(drow("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                End If
            Next

            For Each drow As DataRow In currtable.Select("cp='E'")
                drow("Lots") = IIf(IsDBNull(drow("Lots")) = True, 0, drow("Lots"))
                If drow("Lots") = 0 Then
                    'drow("Lots") = Val(drow("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                    'Val(drow("units")) / IIf(IsDBNull(cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")) = True, 1, cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'"))
                    REM  For cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'") Dbnull  Alpesh 


                    If IsDBNull(scripttable.Compute("MAX(lotsize)", "Symbol = '" & drow("company") & "' and option_type not in ('CA','CE','PA','PE') ")) = True Then
                        drow("Lots") = 0
                    Else
                        drow("Lots") = Val(drow("units")) / scripttable.Compute("MAX(lotsize)", "Symbol = '" & drow("company") & "'and option_type not in ('CA','CE','PA','PE') ")
                    End If
                    'ElseIf drow("cp") = "E" Then
                    '    drow("Lots") = Val(drow("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                End If
            Next

        Else
            For Each drow As DataRow In currtable.Select("")
                drow("Lots") = IIf(IsDBNull(drow("Lots")) = True, 0, drow("Lots"))
                If drow("Lots") = 0 Then
                    drow("Lots") = Val(drow("units")) / Currencymaster.Compute("MAX(multiplier)", "script = '" & drow("script") & "'")
                    'ElseIf drow("cp") = "E" Then
                    '    drow("Lots") = Val(drow("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & drow("script") & "'")
                End If
            Next
        End If
    End Sub
    ''' <summary>
    ''' calculate_tabMt
    ''' </summary>
    ''' <param name="status"></param>
    ''' <remarks>This method when Tab page change Update Maintable</remarks>
    Private Sub calculate_tabMt(ByVal CmpName As String, Optional ByVal status As Boolean = False)
        Try
            If (ltpprice.Count > 0 Or status = True) Then
                Dim ltppr As Double = 0
                Dim ltppr1 As Double = 0
                Dim fltppr As Double = 0
                Dim mt As Double = 0
                Dim mmt As Double = 0
                Dim isfut As Boolean = False
                Dim iscall As Boolean = False

                Dim token As Long
                Dim token1 As Long
                Dim dt As Date

                For Each drow As DataRow In maintable.Select("company='" & CmpName & "'", "strikes,cp,company")
                    If drow("cp") = "E" Then
                        drow("last") = Val(eltpprice(CLng(drow("tokanno"))))
                        REM Fut and Eq Delat Neytralize calc
                        Try
                            If (drow("cp") = "F" Or drow("cp") = "E") Then
                                If drow("delta") <> 0 Then
                                    drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                End If
                            End If
                        Catch ex As Exception

                        End Try

                        If iDeltaLSize > 1 Then
                            drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                        End If

                    ElseIf drow("CP") = "F" Then
                        drow("last") = Val(fltpprice(CLng(drow("tokanno"))))
                        If pan11 = True Then
                            If date1 = CDate(drow("mdate")).Date Then
                                If VarIsCurrency = True Then

                                End If
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            End If
                        End If
                        If pan21 = True Then
                            If date2 = CDate(drow("mdate")).Date Then
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            End If
                        End If
                        If pan31 = True Then
                            If date3 = CDate(drow("mdate")).Date Then
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True
                            End If
                        End If

                        REM Fut and Eq Delat Neytralize calc
                        Try

                        
                            If (drow("cp") = "F" Or drow("cp") = "E") Then
                                If drow("delta") <> 0 Then
                                    drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                End If
                            End If
                        Catch ex As Exception

                        End Try

                        If iDeltaLSize > 1 Then
                            drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                        End If
                    Else
                        If pan11 = True Then
                            'If date1 = CDate(drow("fut_mdate")).Date Then
                            If date1 = CDate(drow("mdate")).Date Then
                                If IsDBNull(drow("ftoken")) = True Then
                                    drow("ftoken") = Val(maintable.Compute("MAX(tokanno)", "company='" & drow("company") & "' AND (cp='F' or cp='X') AND mdate='" & drow("mdate") & "'").ToString)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            End If
                        End If
                        If pan21 = True Then
                            'If date2 = CDate(drow("fut_mdate")).Date Then
                            If date2 = CDate(drow("mdate")).Date Then
                                fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            End If
                        End If
                        If pan31 = True Then
                            'If date3 = CDate(drow("fut_mdate")).Date Then
                            If date3 = CDate(drow("mdate")).Date Then
                                fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True
                            End If
                        End If
                        'For Each dr As DataRow In currtable.Select("company='" & compname & "' and cp not in ('F','E') and mdate='" & dt & "' and tokanno='" & CLng(drow("tokanno")) & "'")
                        If CBool(drow("isliq")) = True Then
                            token = CLng(drow("tokanno"))
                            token1 = CLng(drow("token1"))
                        Else
                            token = CLng(drow("tokanno"))
                            'If token = 57564 Then
                            '    MsgBox(token)
                            'End If
                            token1 = 0
                        End If

                        Dim index As Integer = -1 ' currtable.Rows.IndexOf(drow)

                        mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                        mmt = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date, CDate(drow("mdate")).Date)
                        If Now.Date = CDate(drow("mdate")).Date Then
                            mt += 0.5
                        End If
                        If CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date = CDate(drow("mdate")).Date Then
                            mmt += 0.5
                        End If
                        If drow("cp") = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        If ltpprice.Contains(token) Then
                            If token1 > 0 Then
                                ltppr = Val(ltpprice(token))
                                ltppr1 = Val(ltpprice(token1))
                            Else
                                ltppr = Val(ltpprice(token))
                                ltppr1 = 0
                            End If

                            If fltppr <> 0 Then
                                If status = True Then
                                    ' If Val(drow("lv1")) <> 0 Or Val(drow("lv")) <> 0 Then
                                    If CBool(drow("isliq")) = True Then
                                        CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, Val(drow("lv1").ToString), drow("lv"), True)
                                    Else
                                        If iscall = True Then
                                            CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, drow("lv"), Val(drow("lv1").ToString), True)
                                        Else
                                            CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, Val(drow("lv1").ToString), drow("lv"), True)
                                        End If
                                    End If
                                    'End If
                                Else
                                    'CalData(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname)
                                    'Alpesh 21/04/2011 
                                    'If cmdStart.Text = "Stop" Then
                                    If thrworking = True Then
                                        CalData(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname)
                                    Else
                                        'CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, drow("lv"), Val(drow("lv1").ToString), True)
                                        If iscall = True Then
                                            CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, drow("lv"), Val(drow("lv1").ToString), True)
                                        Else
                                            CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, Val(drow("lv1").ToString), drow("lv"), True)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        ' Next
                    End If
                Next
            End If
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try

    End Sub
    ''' <summary>
    ''' calculate_tab
    ''' </summary>
    ''' <param name="status"></param>
    ''' <remarks>This method when Tab page change</remarks>
    Private Sub calculate_tab(Optional ByVal status As Boolean = False)
        Try
            If (ltpprice.Count > 0 Or status = True) Then
                Dim ltppr As Double = 0
                Dim ltppr1 As Double = 0
                Dim fltppr As Double = 0
                Dim mt As Double = 0
                Dim mmt As Double = 0
                Dim isfut As Boolean = False
                Dim iscall As Boolean = False

                Dim token As Long
                Dim token1 As Long
                Dim dt As Date

                For Each drow As DataRow In currtable.Select("isCalc = True") '.Select("company='" & compname & "' ")
                    If drow("cp") = "E" Then
                        drow("last") = Val(eltpprice(CLng(drow("tokanno"))))

                        If iDeltaLSize > 1 Then
                            drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                        End If

                        For Each grow As DataRow In gcurrtable.Select("tokanno='" & drow("tokanno") & "'")
                            grow("last") = drow("last")
                        Next

                    ElseIf drow("CP") = "F" Then
                        drow("last") = Val(fltpprice(CLng(drow("tokanno"))))

                        If iDeltaLSize > 1 Then
                            drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                        End If

                        For Each grow As DataRow In gcurrtable.Select("tokanno='" & drow("tokanno") & "'")
                            grow("last") = drow("last")
                        Next

                        If pan11 = True Then
                            If date1 = CDate(drow("mdate")).Date Then
                                If VarIsCurrency = True Then

                                End If
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            End If
                        End If
                        If pan21 = True Then
                            If date2 = CDate(drow("mdate")).Date Then
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            End If
                        End If
                        If pan31 = True Then
                            If date3 = CDate(drow("mdate")).Date Then
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True
                            End If
                        End If
                        Else
                            If pan11 = True Then

                                'If date1 = CDate(drow("fut_mdate")).Date Then
                                If date1 = CDate(drow("mdate")).Date Then
                                    If IsDBNull(drow("ftoken")) = True Then
                                        drow("ftoken") = Val(currtable.Compute("MAX(tokanno)", "company='" & drow("company") & "' AND (cp='F' or cp='X') AND mdate='" & drow("mdate") & "'").ToString)
                                    End If
                                    fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                    txtrate.Invoke(mdel, fltppr)
                                    dt = date1
                                    isfut = True
                                End If
                            End If
                            If pan21 = True Then

                                'If date2 = CDate(drow("fut_mdate")).Date Then

                                If date2 = CDate(drow("mdate")).Date Then
                                    fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                    dt = date2
                                    txtrate1.Invoke(mdel1, fltppr)
                                    isfut = True
                                End If
                            End If
                            If pan31 = True Then
                                'If date3 = CDate(drow("fut_mdate")).Date Then
                                If date3 = CDate(drow("mdate")).Date Then
                                    fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                    dt = date3
                                    txtrate2.Invoke(mdel2, fltppr)
                                    isfut = True
                                End If
                            End If
                            'For Each dr As DataRow In currtable.Select("company='" & compname & "' and cp not in ('F','E') and mdate='" & dt & "' and tokanno='" & CLng(drow("tokanno")) & "'")

                            If CBool(drow("isliq")) = True Then
                                token = CLng(drow("tokanno"))
                                token1 = CLng(drow("token1"))
                            Else
                                token = CLng(drow("tokanno"))
                                'If token = 57564 Then
                                '    MsgBox(token)
                                'End If
                                token1 = 0
                        End If

                        REM Fut and Eq Delat Neytralize calc
                        Try

                        
                            If (drow("cp") = "F" Or drow("cp") = "E") Then
                                If drow("delta") <> 0 Then
                                    drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                        If iDeltaLSize > 1 Then
                            drow("deltaval") = (Math.Round((Val(drow("delta") & "") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            drow("deltaval1") = (Math.Round((Val(drow("delta") & "") * drow("units")) / iDeltaLSize, roundDelta_Val))
                        End If

                        Dim index As Integer = -1 ' currtable.Rows.IndexOf(drow)
                        For Each grow As DataRow In gcurrtable.Select("tokanno=" & token & "")
                            index = gcurrtable.Rows.IndexOf(grow)
                        Next

                        mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                        mmt = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date, CDate(drow("mdate")).Date)
                        If Now.Date = CDate(drow("mdate")).Date Then
                            mt += 0.5
                        End If
                        If CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date = CDate(drow("mdate")).Date Then
                            mmt += 0.5
                        End If
                        If drow("cp") = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        If ltpprice.Contains(token) Then

                            If token1 > 0 Then
                                ltppr = Val(ltpprice(token))
                                ltppr1 = Val(ltpprice(token1))
                            Else
                                ltppr = Val(ltpprice(token))
                                ltppr1 = 0
                            End If

                            If fltppr <> 0 Then
                                If status = True Then
                                    ' If Val(drow("lv1")) <> 0 Or Val(drow("lv")) <> 0 Then
                                    If CBool(drow("isliq")) = True Then
                                        CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, Val(drow("lv1").ToString), drow("lv"), True)
                                    Else
                                        If iscall = True Then
                                            CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, drow("lv"), Val(drow("lv1").ToString), True)
                                        Else
                                            CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, Val(drow("lv1").ToString), drow("lv"), True)
                                        End If
                                    End If
                                    'End If
                                Else
                                    'CalData(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname)
                                    'Alpesh 21/04/2011 
                                    'If cmdStart.Text = "Stop" Then
                                    If thrworking = True Then
                                        CalData(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname)
                                    Else
                                        'CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, drow("lv"), Val(drow("lv1").ToString), True)
                                        If iscall = True Then
                                            CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, drow("lv"), Val(drow("lv1").ToString), True)
                                        Else
                                            CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, Val(drow("lv1").ToString), drow("lv"), True)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        ' Next
                    End If
                Next
            End If
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try

    End Sub
    ''' <summary>
    ''' CalData_start
    ''' </summary>
    ''' <param name="futval">Future LTP price</param>
    ''' <param name="stkprice">Strike price</param>
    ''' <param name="cpprice">Option LTP price</param>
    ''' <param name="cpprice1">Opposite Option LTP price</param>
    ''' <param name="mT">Maturity Time</param>
    ''' <param name="mmT">Maturity Time - No. Of Days</param>
    ''' <param name="mIsCall">Is Call then True otherwise FALSE</param>
    ''' <param name="mIsFut">Is Future </param>
    ''' <param name="drow">Datarow</param>
    ''' <param name="qty">Qty</param>
    ''' <param name="index">Row Index</param>
    ''' <param name="cname">Company Name</param>
    ''' <param name="cvol">Call Vol.</param>
    ''' <param name="pvol">Put Vol.</param>
    ''' <param name="store_ltp"></param>
    ''' <remarks>This method call to calculate Greek value using Black_Scholes function and that Greek value store into datarow</remarks>
    Private Sub CalData_start(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal cpprice1 As Double, ByVal mT As Integer, ByVal mmT As Integer, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal drow As DataRow, ByVal qty As Double, ByVal index As Integer, ByVal cname As String, ByVal cvol As Double, ByVal pvol As Double, Optional ByVal store_ltp As Boolean = False)
        Try
            Dim mVDelta As Double
            Dim mDelta As Double
            Dim mGama As Double
            Dim mVega As Double
            Dim mThita As Double
            Dim mVolga As Double
            Dim mVanna As Double
            ' Dim mRah As Double

            Dim mD1 As Double
            Dim mD2 As Double

            Dim mDelta1 As Double
            Dim mGama1 As Double
            Dim mVega1 As Double
            Dim mThita1 As Double
            Dim mVolga1 As Double
            Dim mVanna1 As Double
            ' Dim mRah1 As Double

            Dim mD11 As Double
            Dim mD21 As Double

            Dim mVolatility As Double
            'Dim mVolatility1 As Double
            Dim tmpcpprice As Double = 0
            Dim tmpcpprice1 As Double = 0
            tmpcpprice = cpprice
            tmpcpprice1 = cpprice1

            Dim tcname As String
            tcname = cname
            'Dim mIsCal As Boolean
            'Dim mIsPut As Boolean
            'Dim mIsFut As Boolean

            mVDelta = 0
            mDelta = 0
            mGama = 0
            mVega = 0
            mThita = 0
            mVolga = 0
            mVanna = 0
            ' mRah = 0
            Dim _mt, _mt1 As Double
            If cvol = 0 Then cvol = 0.1
            If pvol = 0 Then pvol = 0.1
            If mT = 0 Then
                _mt = 0.00001
                _mt1 = 0.00001
            Else
                _mt = (mT) / 365
                '_mt1 = ((mT + 1) - CInt(txtnoofday.Text)) / 365
                _mt1 = (mmT) / 365

            End If
            'If tmpcpprice1 = 0 Then
            '    mVolatility = mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
            'Else
            '    'mVolatility1 = mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, mIsCall, mIsFut, 0, 6)
            '    If mIsCall = True Then
            '        mVolatility = mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, False, mIsFut, 0, 6)
            '    Else
            '        mVolatility = mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, True, mIsFut, 0, 6)
            '    End If
            'End If
            If chkCalVol.Checked = True Then mIsFut = False
            If CBool(IIf(IsDBNull(drow("IsVolFix")), False, drow("IsVolFix"))) = False Then
                If tmpcpprice1 = 0 Then 'whose isliq=false
                    If mIsCall = True Then
                        mVolatility = cvol / 100
                    Else
                        mVolatility = pvol / 100
                    End If
                    cpprice = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0))
                    cpprice1 = 0 'if isliq=false then cpprice1=0

                Else 'if isliq=true, calculate both cpprice and cpprice1

                    If mIsCall = True Then
                        mVolatility = pvol / 100
                        cpprice = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, Val(cvol / 100), _mt, mIsCall, mIsFut, 0, 0))
                        cpprice1 = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, Val(pvol / 100), _mt, False, mIsFut, 0, 0))
                        'mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, False, mIsFut, 0, 6)
                    Else
                        mVolatility = cvol / 100
                        cpprice = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, Val(pvol / 100), _mt, mIsCall, mIsFut, 0, 0))
                        cpprice1 = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, Val(cvol / 100), _mt, True, mIsFut, 0, 0))
                        ' mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, True, mIsFut, 0, 6)
                    End If

                End If
            Else
                'drow("IsVolFix") = True
                If tmpcpprice1 = 0 Then 'whose isliq=false
                    mVolatility = Val(drow("lv").ToString & "") / 100
                    cpprice = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0))
                    cpprice1 = 0 'if isliq=false then cpprice1=0
                Else 'if isliq=true, calculate both cpprice and cpprice1
                    mVolatility = Val(drow("lv").ToString & "") / 100
                    cpprice = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0))
                    cpprice1 = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, True, mIsFut, 0, 0))
                    ' mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, True, mIsFut, 0, 6)
                End If

            End If

            mVDelta = mVDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, gVOLATITLIY, _mt, mIsCall, mIsFut, 0, 1))

            mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))

            mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))

            mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))

            mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))


            '' ''Calculation For Volga And Vanna 

            '' ''mD1 = (((Math.Log(futval / stkprice) + ((Mrateofinterast + (mVolatility) ^ 2) / 2) * (mT / 365))) / (mVolatility * Math.Sqrt(mT / 365)))
            '' ''mD2 = ((Math.Log(futval / stkprice) + ((Mrateofinterast - (mVolatility) ^ 2) / 2) * (mT / 365))) / (mVolatility * Math.Sqrt(mT / 365))

            ' ''mD1 = mD1 + (((Math.Log(futval / stkprice) + ((Mrateofinterast + (mVolatility) ^ 2) / 2) * _mt)) / (mVolatility * Math.Sqrt(_mt)))
            ' ''mD2 = mD2 + ((Math.Log(futval / stkprice) + ((Mrateofinterast - (mVolatility) ^ 2) / 2) * _mt)) / (mVolatility * Math.Sqrt(_mt))

            ' ''mVolga = mVolga + (mVega * mD1 * mD2 / mVolatility / 100)
            ' ''mVanna = mVanna + (mVega / futval * (1 - (mD1 / (mVolatility * Math.Sqrt(_mt)))))

            'Using Function 
            mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
            mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

            mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
            mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)

            ' mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))


            mDelta1 = mDelta1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 1))

            mGama1 = mGama1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 2))

            mVega1 = mVega1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 3))

            mThita1 = mThita1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 4))

            '' '' ''Calculation For Volga1 And Vanna1

            '' '' ''mD11 = (((Math.Log(futval / stkprice) + ((Mrateofinterast + (mVolatility) ^ 2) / 2) * (mmT / 365))) / (mVolatility * Math.Sqrt(mmT / 365)))
            '' '' ''mD21 = ((Math.Log(futval / stkprice) + ((Mrateofinterast - (mVolatility) ^ 2) / 2) * (mmT / 365))) / (mVolatility * Math.Sqrt(mmT / 365))

            ' '' ''mD11 = mD11 + (((Math.Log(futval / stkprice) + ((Mrateofinterast + (mVolatility) ^ 2) / 2) * _mt1)) / (mVolatility * Math.Sqrt(_mt1)))
            ' '' ''mD21 = mD21 + ((Math.Log(futval / stkprice) + ((Mrateofinterast - (mVolatility) ^ 2) / 2) * _mt1)) / (mVolatility * Math.Sqrt(_mt1))

            ' '' ''mVolga1 = mVolga1 + (mVega1 * mD11 * mD21 / mVolatility / 100)
            ' '' ''mVanna1 = mVanna1 + (mVega1 / futval * (1 - (mD11 / (mVolatility * Math.Sqrt(_mt1)))))

            'Using Function 
            mD11 = mD11 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt1)
            mD21 = mD21 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt1)

            mVolga1 = mVolga1 + CalVolga(mVega1, mD11, mD21, mVolatility)
            mVanna1 = mVanna1 + CalVanna(futval, mVega, mD11, mD21, mVolatility, _mt1)


            ' mRah1 = mRah1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 5))

            If tcname = compname Then 'if company name = select company name

                drow("last") = cpprice
                drow("last1") = cpprice1
                'If tmpcpprice1 = 0 Then
                drow("lv") = mVolatility * 100 'Math.Round(mVolatility * 100, 2)
                'Else
                ' drow("lv") = Math.Round(mVolatility1 * 100, 2)
                'End If

                'Delta
                drow("delta") = Math.Round(mDelta, roundDelta)
                If iDeltaLSize <= 1 Then
                    drow("deltaval") = Math.Round(mDelta * qty, roundDelta_Val)
                    drow("deltaval1") = Math.Round(mDelta1 * qty, roundDelta_Val)
                Else
                    drow("deltaval") = Math.Round((mDelta * qty) / iDeltaLSize, roundDelta_Val)
                    drow("deltaval1") = Math.Round((mDelta1 * qty) / iDeltaLSize, roundDelta_Val)
                End If

                'Gamma
                drow("gamma") = Math.Round(mGama, roundGamma)
                If iGammaLSize <= 1 Then
                    drow("gmval") = Math.Round(mGama * qty, roundGamma_Val)
                    drow("gmval1") = Math.Round(mGama1 * qty, roundGamma_Val)
                    Else
                    drow("gmval") = Math.Round((mGama * qty) / iGammaLSize, roundGamma_Val)
                    drow("gmval1") = Math.Round((mGama1 * qty) / iGammaLSize, roundGamma_Val)
                    End If

                'Vega
                drow("vega") = Math.Round(mVega, roundVega)
                If iVegaLSize <= 1 Then
                    drow("vgval") = Math.Round(mVega * qty, roundVega_Val)
                    drow("vgval1") = Math.Round(mVega1 * qty, roundVega_Val)
                Else
                    drow("vgval") = Math.Round((mVega * qty) / iVegaLSize, roundVega_Val)
                    drow("vgval1") = Math.Round((mVega1 * qty) / iVegaLSize, roundVega_Val)
                End If

                'Theta
                drow("theta") = Math.Round(mThita, roundTheta)
                If iThetaLSize <= 1 Then
                drow("thetaval") = Math.Round(mThita * qty, roundTheta_Val)
                    drow("thetaval1") = Math.Round(mThita1 * qty, roundTheta_Val)
                Else
                    drow("thetaval") = Math.Round((mThita * qty) / iThetaLSize, roundTheta_Val)
                    drow("thetaval1") = Math.Round((mThita1 * qty) / iThetaLSize, roundTheta_Val)
                End If

                If mThita < 0 Then
                    If Math.Abs(mThita) > cpprice Then
                        drow("theta") = -Math.Round(cpprice, roundTheta)
                        If iThetaLSize <= 1 Then
                        drow("thetaval") = Math.Round(Val(drow("theta")) * qty, roundTheta_Val)
                        Else
                            drow("thetaval") = Math.Round((Val(drow("theta")) * qty) / iThetaLSize, roundTheta_Val)
                        End If
                    End If
                Else
                    If Math.Abs(mThita) > cpprice Then
                        drow("theta") = Math.Round(cpprice, roundTheta)
                        If iThetaLSize <= 1 Then
                        drow("thetaval") = Math.Round(Val(drow("theta")) * qty, roundTheta_Val)
                        Else
                            drow("thetaval") = Math.Round((Val(drow("theta")) * qty) / iThetaLSize, roundTheta_Val)
                    End If
                End If
                End If
                    If mThita1 < 0 Then
                        If Math.Abs(mThita1) > cpprice Then
                        If iThetaLSize <= 1 Then
                            drow("thetaval1") = Math.Round(-Math.Round(cpprice, roundTheta) * qty, roundTheta_Val)
                        Else
                            drow("thetaval1") = Math.Round((-Math.Round(cpprice, roundTheta) * qty) / iThetaLSize, roundTheta_Val)
                        End If

                    End If
                    Else
                        If Math.Abs(mThita) > cpprice Then
                        If iThetaLSize <= 1 Then
                            drow("thetaval1") = Math.Round(Math.Round(cpprice, roundTheta) * qty, roundTheta_Val)
                        Else
                            drow("thetaval1") = Math.Round((Math.Round(cpprice, roundTheta) * qty) / iThetaLSize, roundTheta_Val)
                        End If
                    End If
                End If

                'Volga
                drow("volga") = Math.Round(mVolga, roundVolga)
                If iVolgaLSize <= 1 Then
                    drow("volgaval") = Math.Round(mVolga * qty, roundVolga_Val)
                    drow("volgaval1") = Math.Round(mVolga1 * qty, roundVolga_Val)
                Else
                    drow("volgaval") = Math.Round((mVolga * qty) / iVolgaLSize, roundVolga_Val)
                    drow("volgaval1") = Math.Round((mVolga1 * qty) / iVolgaLSize, roundVolga_Val)
                End If
                

                'Vanna
                drow("vanna") = Math.Round(mVanna, roundVanna)
                If iVannaLSize <= 1 Then
                    drow("vannaval") = Math.Round(mVanna * qty, roundVanna_Val)
                    drow("vannaval1") = Math.Round(mVanna1 * qty, roundVanna_Val)
                Else
                    drow("vannaval") = Math.Round((mVanna * qty) / iVannaLSize, roundVanna_Val)
                    drow("vannaval1") = Math.Round((mVanna1 * qty) / iVannaLSize, roundVanna_Val)
                End If
                
                REM Calculation For UpDown Analysis By Viral
                If SELECTION_TYPE = Setting_SELECTION_TYPE.ITM_ATM_OTM Then
                    If drow("CP") = "P" Then
                        drow("deltavval") = Math.Round(Math.Abs(mVDelta), roundDelta) 'Math.Round(1 - mVDelta, roundDelta)
                    Else
                        drow("deltavval") = Math.Round(mVDelta, roundDelta)
                    End If
                Else
                    drow("deltavval") = Math.Round(mVDelta, roundDelta)
                End If


                'Greeks Neutralize by viral
                If mDelta <> 0 Then
                    drow("DeltaN") = (Val(txttdelval.Text) / mDelta).ToString(NeutralizeStr) * -1
                Else
                    drow("DeltaN") = 0
                End If
                If mGama <> 0 Then
                    drow("GammaN") = (Val(txttgmval.Text) / mGama).ToString(NeutralizeStr) * -1
                Else
                    drow("GammaN") = 0
                End If
                If mVega <> 0 Then
                    drow("VegaN") = (Val(txttvgval.Text) / mVega).ToString(NeutralizeStr) * -1
                Else
                    drow("VegaN") = 0
                End If
                If mThita <> 0 Then
                    drow("ThetaN") = (Val(txttthval.Text) / mThita).ToString(NeutralizeStr) * -1
                Else
                    drow("ThetaN") = 0
                End If
                If mVolga <> 0 Then
                    drow("VolgaN") = (Val(txttvolgaval.Text) / mVolga).ToString(NeutralizeStr) * -1
                Else
                    drow("VolgaN") = 0
                End If
                If mVanna <> 0 Then
                    drow("VannaN") = (Val(txttvannaval.Text) / mVanna).ToString(NeutralizeStr) * -1
                Else
                    drow("VannaN") = 0
                End If


                    If drow("mdate") = Today.Date Then
                        drow("deltaval1") = 0
                        drow("thetaval1") = 0
                        drow("vgval1") = 0
                        drow("gmval1") = 0
                    End If
                If index > -1 Then
                    gcurrtable.Rows(index)("last") = cpprice
                    gcurrtable.Rows(index)("last1") = cpprice1
                    gcurrtable.Rows(index)("lv") = mVolatility * 100 'Math.Round(mVolatility * 100, 2)
                    gcurrtable.Rows(index)("delta") = Math.Round(mDelta, roundDelta)

                    'gcurrtable.Rows(index)("deltaval") = Math.Round(mDelta * qty, roundDelta_Val)

                    'Delta
                    If iDeltaLSize <= 1 Then
                        gcurrtable.Rows(index)("deltaval") = Math.Round(mDelta * qty, roundDelta_Val)
                        gcurrtable.Rows(index)("deltaval1") = Math.Round(mDelta1 * qty, roundDelta_Val)
                    Else
                        gcurrtable.Rows(index)("deltaval") = Math.Round((mDelta * qty) / iDeltaLSize, roundDelta_Val)
                        gcurrtable.Rows(index)("deltaval1") = Math.Round((mDelta1 * qty) / iDeltaLSize, roundDelta_Val)
                    End If

                    'Gamma
                    gcurrtable.Rows(index)("gamma") = Math.Round(mGama, roundGamma)
                    If iGammaLSize <= 1 Then
                        gcurrtable.Rows(index)("gmval") = Math.Round(mGama * qty, roundGamma_Val)
                        Else
                        gcurrtable.Rows(index)("gmval") = Math.Round((mGama * qty) / iGammaLSize, roundGamma_Val)
                        End If

                    'Vega
                    gcurrtable.Rows(index)("vega") = Math.Round(mVega, roundVega)
                    If iVegaLSize <= 1 Then
                        gcurrtable.Rows(index)("vgval") = Math.Round(mVega * qty, roundVega_Val)
                    Else
                        gcurrtable.Rows(index)("vgval") = Math.Round((mVega * qty) / iVegaLSize, roundVega_Val)
                        End If


                    'Theta
                    gcurrtable.Rows(index)("theta") = Math.Round(mThita, roundTheta)
                    If mThita < 0 Then
                        If Math.Abs(mThita) > cpprice Then
                            gcurrtable.Rows(index)("theta") = -Math.Round(cpprice, roundTheta)
                        End If
                    Else
                        If Math.Abs(mThita) > cpprice Then
                            gcurrtable.Rows(index)("theta") = Math.Round(cpprice, roundTheta)
                        End If
                    End If
                    If iThetaLSize <= 1 Then
                    gcurrtable.Rows(index)("thetaval") = Math.Round(Val(gcurrtable.Rows(index)("theta")) * qty, roundTheta_Val)
                    Else
                        gcurrtable.Rows(index)("thetaval") = Math.Round((Val(gcurrtable.Rows(index)("theta")) * qty) / iThetaLSize, roundTheta_Val)
                    End If


                    'Volga
                    gcurrtable.Rows(index)("volga") = Math.Round(mVolga, roundVolga)
                    If iVolgaLSize <= 1 Then
                    gcurrtable.Rows(index)("volgaval") = Math.Round(mVolga * qty, roundVolga_Val)
                    Else
                        gcurrtable.Rows(index)("volgaval") = Math.Round((mVolga * qty) / iVolgaLSize, roundVolga_Val)
                    End If

                    'Vanna
                    gcurrtable.Rows(index)("vanna") = Math.Round(mVanna, roundVanna)
                    If iVannaLSize <= 1 Then
                    gcurrtable.Rows(index)("vannaval") = Math.Round(mVanna * qty, roundVanna_Val)
                    Else
                        gcurrtable.Rows(index)("vannaval") = Math.Round((mVanna * qty) / iVannaLSize, roundVanna_Val)
                End If


                    REM Calculation For UpDown Analysis By Viral
                    If SELECTION_TYPE = Setting_SELECTION_TYPE.ITM_ATM_OTM Then
                        If gcurrtable.Rows(index)("CP") = "P" Then
                            gcurrtable.Rows(index)("deltavval") = Math.Round(Math.Abs(mVDelta), roundDelta) 'Math.Round(1 - mVDelta, roundDelta)
                        Else
                            gcurrtable.Rows(index)("deltavval") = Math.Round(mVDelta, roundDelta)
                        End If
                    Else
                        If gcurrtable.Rows(index)("CP") = "P" Then
                            gcurrtable.Rows(index)("deltavval") = Math.Round(mVDelta + 1, roundDelta)
                        Else
                            gcurrtable.Rows(index)("deltavval") = Math.Round(mVDelta, roundDelta)
                        End If
                    End If

                End If

                    '    If mCurrtable.Rows.Count >= index Then
                    '        mCurrtable.Rows(index)("last") = cpprice
                    '        'If tmpcpprice1 = 0 Then
                    '        mCurrtable.Rows(index)("lv") = Math.Round(mVolatility * 100, 2)
                    '        'Else
                    '        '   mCurrtable.Rows(index)("lv") = Math.Round(mVolatility1 * 100, 2)
                    '        ' End If

                    '        mCurrtable.Rows(index)("delta") = Math.Round(mDelta1, roundDelta)
                    '        mCurrtable.Rows(index)("deltaval") = Math.Round(mDelta1 * qty, roundDelta_Val)
                    '        mCurrtable.Rows(index)("theta") = Math.Round(mThita1, roundTheta)
                    '        If mThita < 0 Then
                    '            If Math.Abs(mThita) > cpprice Then
                    '                mCurrtable.Rows(index)("theta") = -Math.Round(cpprice, roundTheta)
                    '            End If
                    '        Else
                    '            If Math.Abs(mThita) > cpprice Then
                    '                mCurrtable.Rows(index)("theta") = Math.Round(cpprice, roundTheta)
                    '            End If
                    '        End If
                    '        mCurrtable.Rows(index)("thetaval") = Math.Round(mThita1 * qty, roundTheta_Val)
                    '        mCurrtable.Rows(index)("vega") = Math.Round(mVega1, roundVega)
                    '        mCurrtable.Rows(index)("vgval") = Math.Round(mVega1 * qty, roundVega_Val)
                    '        mCurrtable.Rows(index)("gamma") = Math.Round(mGama1, roundGamma)
                    '        mCurrtable.Rows(index)("gmval") = Math.Round(mGama1 * qty, roundGamma_Val)
                    '    End If


                    'if ltp price of call / put is not there in token hashtable then add it
                    If store_ltp = True Then 'update LTP value to ltpprice hashtable
                        If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                            ltpprice.Add(CLng(drow("tokanno")), cpprice)
                        Else
                            ltpprice(CLng(drow("tokanno"))) = cpprice
                        End If
                    End If
                    'end of ltp price stroate
                End If

        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    '' ''Private Function CalD1(ByVal mfutval As Double, ByVal mstkprice As Double, ByVal _mVolatility As Double, ByVal mDte As Double) As Double
    '' ''    Try
    '' ''        CalD1 = (((Math.Log(mfutval / mstkprice) + ((Mrateofinterast + (_mVolatility) ^ 2) / 2) * mDte)) / (_mVolatility * Math.Sqrt(mDte)))
    '' ''    Catch ex As Exception

    '' ''    End Try
    '' ''End Function
    '' ''Private Function CalD2(ByVal mfutval As Double, ByVal mstkprice As Double, ByVal _mVolatility As Double, ByVal mDte As Double) As Double
    '' ''    Try
    '' ''        CalD2 = ((Math.Log(mfutval / mstkprice) + ((Mrateofinterast - (_mVolatility) ^ 2) / 2) * mDte)) / (_mVolatility * Math.Sqrt(mDte))
    '' ''    Catch ex As Exception

    '' ''    End Try
    '' ''End Function
    '' ''Private Function CalVolga(ByVal mVega As Double, ByVal mD1 As Double, ByVal mD2 As Double, ByVal _mVolatility As Double) As Double
    '' ''    Try
    '' ''        CalVolga = (mVega * mD1 * mD2 / _mVolatility / 100)
    '' ''    Catch ex As Exception

    '' ''    End Try

    '' ''End Function
    '' ''Private Function CalVanna(ByVal mfutval As Double, ByVal mVega As Double, ByVal mD1 As Double, ByVal mD2 As Double, ByVal _mVolatility As Double, ByVal mDte As Double) As Double
    '' ''    Try
    '' ''        CalVanna = (mVega / mfutval * (1 - (mD1 / (_mVolatility * Math.Sqrt(mDte)))))
    '' ''    Catch ex As Exception

    '' ''    End Try
    '' ''End Function

    ''' <summary>
    ''' CalData
    ''' </summary>
    ''' <param name="futval"></param>
    ''' <param name="stkprice"></param>
    ''' <param name="cpprice"></param>
    ''' <param name="cpprice1"></param>
    ''' <param name="mT"></param>
    ''' <param name="mmT"></param>
    ''' <param name="mIsCall"></param>
    ''' <param name="mIsFut"></param>
    ''' <param name="drow"></param>
    ''' <param name="qty"></param>
    ''' <param name="index"></param>
    ''' <param name="cname"></param>
    ''' <remarks>This method call to calculate Greek value using Black_Scholes function and that Greek value store into datarow</remarks>
    Private Sub CalData(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal cpprice1 As Double, ByVal mT As Double, ByVal mmT As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal drow As DataRow, ByVal qty As Double, ByVal index As Integer, ByVal cname As String)
        Try
            Dim mDelta As Double
            Dim mVDelta As Double
            Dim mGama As Double
            Dim mVega As Double
            Dim mThita As Double
            Dim mVolga As Double
            Dim mVanna As Double
            Dim mRah As Double
            Dim mDelta1 As Double
            Dim mGama1 As Double
            Dim mVega1 As Double
            Dim mThita1 As Double
            Dim mVolga1 As Double
            Dim mVanna1 As Double
            Dim mRah1 As Double
            Dim a As Double = 0
            Dim b As Double = 0
            Dim mVolatility As Double
            Dim mVolatility1 As Double
            Dim tmpcpprice As Double = 0
            Dim tmpcpprice1 As Double = 0
            tmpcpprice = cpprice
            tmpcpprice1 = cpprice1
            Dim tcname As String
            tcname = cname

            Dim mD1 As Double
            Dim mD2 As Double

            Dim mD11 As Double
            Dim mD21 As Double

            'Dim mIsCal As Boolean
            'Dim mIsPut As Boolean
            'Dim mIsFut As Boolean
            mDelta = 0
            mVDelta = 0
            mGama = 0
            mVega = 0
            mThita = 0
            mVolga = 0
            mVanna = 0
            mRah = 0

            Dim _mt, _mt1 As Double

            If mT = 0 Then
                _mt = 0.00001
                _mt1 = 0.00001
            Else
                _mt = (mT) / 365
                '_mt1 = ((mT + 1) - CInt(txtnoofday.Text)) / 365
                _mt1 = (mmT) / 365
            End If

            drow("IsVolFix") = CBool(IIf(IsDBNull(drow("IsVolFix")), False, drow("IsVolFix")))

            REM: 'calculate vol using equity' is clicked then set mIsFut=false
            If chkCalVol.Checked = True Then mIsFut = False 'keval(23-2-10)
            If CBool(IIf(IsDBNull(drow("IsVolFix")), False, drow("IsVolFix"))) = False Then
                If thrworking_future_only = False Then 'if offline mode or online mode
                    If tmpcpprice1 = 0 Then 'if token1's LTP is zero when isliq=false
                        If drow("IsCurrency") = False Then
                            mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
                        Else
                            mVolatility = Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
                        End If
                        mVolatility1 = 0
                    Else 'if token1's LTP<>0 when isliq=True
                        'mVolatility1 = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, mIsCall, mIsFut, 0, 6)
                        If drow("IsCurrency") = False Then
                            If mIsCall = True Then
                                mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, False, mIsFut, 0, 6)
                                mVolatility1 = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, True, mIsFut, 0, 6)
                            Else
                                mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, True, mIsFut, 0, 6)
                                mVolatility1 = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, False, mIsFut, 0, 6)
                            End If
                        Else
                            If mIsCall = True Then
                                mVolatility = Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, tmpcpprice1, _mt, False, mIsFut, 0, 6)
                                mVolatility1 = Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, tmpcpprice, _mt, True, mIsFut, 0, 6)
                            Else
                                mVolatility = Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, tmpcpprice1, _mt, True, mIsFut, 0, 6)
                                mVolatility1 = Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, tmpcpprice, _mt, False, mIsFut, 0, 6)
                            End If
                        End If
                    End If
                Else 'future update mode
                    'take volatility from the grid data and calculat teh ltp
                    mVolatility = drow("lv") / 100
                End If
            Else
                mVolatility = drow("lv") / 100
            End If
            If drow("IsCurrency") = False Then
                'If Not mIsFut Then
                If mVolatility <> 0 Then
                    mVDelta = mVDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, gVOLATITLIY, _mt, mIsCall, mIsFut, 0, 1))
                    mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))
                    mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))
                    mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))
                    mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))
                    'Cal Volga and Vanna Alpesh
                    mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
                    mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

                    mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
                    mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)

                    mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))
                    mDelta1 = mDelta1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 1))
                    mGama1 = mGama1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 2))
                    mVega1 = mVega1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 3))
                    mThita1 = mThita1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 4))
                    mRah1 = mRah1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 5))

                    mD11 = mD11 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt1)
                    mD21 = mD21 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt1)

                    mVolga1 = mVolga1 + CalVolga(mVega1, mD11, mD21, mVolatility)
                    mVanna1 = mVanna1 + CalVanna(futval, mVega, mD11, mD21, mVolatility, _mt1)

                End If
            Else
                'If Not mIsFut Then
                If mVolatility <> 0 Then
                    mVDelta = mVDelta + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, gVOLATITLIY, _mt, mIsCall, mIsFut, 0, 1))
                    mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))
                    mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))
                    mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))
                    mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))

                    'Cal Volga and Vanna Alpesh
                    mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
                    mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

                    mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
                    mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)


                    mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))
                    mDelta1 = mDelta1 + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 1))
                    mGama1 = mGama1 + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 2))
                    mVega1 = mVega1 + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 3))
                    mThita1 = mThita1 + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 4))
                    mRah1 = mRah1 + (Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 5))

                    mD11 = mD11 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt1)
                    mD21 = mD21 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt1)

                    mVolga1 = mVolga1 + CalVolga(mVega1, mD11, mD21, mVolatility)
                    mVanna1 = mVanna1 + CalVanna(futval, mVega, mD11, mD21, mVolatility, _mt1)

                End If
            End If

            'Else
            ''mDelta = mDelta + (1 * drow("Qty"))
            'End If


            ' If tcname = compname Then 
            If mIsCall = True Then
                a = Math.Max(futval - stkprice, 0)
                b = Math.Max(cpprice - a, 0) 'use for theta calcution
            Else
                a = Math.Max(stkprice - futval, 0)
                b = Math.Max(cpprice - a, 0)
            End If

            If CBool(IIf(IsDBNull(drow("IsVolFix")), False, drow("IsVolFix"))) = False Then
                If thrworking_future_only = False Then 'for online or offline mode
                    drow("last") = cpprice
                    drow("last1") = cpprice1
                    drow("lv") = mVolatility * 100 'Math.Round(mVolatility * 100, 2)
                    drow("lv1") = mVolatility1 * 100 'Math.Round(mVolatility1 * 100, 2)
                Else  'calculate ltp for the future update only
                    If drow("IsCurrency") = False Then
                        If mVolatility <> 0 Then
                            cpprice = Math.Round(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0), 2)
                        End If
                    Else
                        If mVolatility <> 0 Then
                            cpprice = Math.Round(Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0), 2)
                        End If
                    End If
                    'drow("last1") = Math.Round(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 6), 2)
                    drow("last") = cpprice
                End If
            Else
                If drow("IsCurrency") = False Then
                    If mVolatility <> 0 Then
                        cpprice = Math.Round(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0), 2)
                    End If
                Else
                    If mVolatility <> 0 Then
                        cpprice = Math.Round(Greeks.Black_Scholes(futval, stkprice, CurrencyRateOfInterest, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0), 2)
                    End If
                End If
                drow("last") = cpprice

                For Each row As DataRow In maintable.Select("script='" & drow("script") & "' And company='" & drow("company") & "'")
                    row("last") = drow("last")
                    row("lv") = drow("lv")
                Next
            End If
            'If tmpcpprice1 = 0 Then

            'Else
            ' drow("lv") = Math.Round(mVolatility1 * 100, 2)
            'End If


            ' drow("delta") = Format(mDelta, Deltastr)
            REM Calculation For UpDown Analysis By Viral
            If SELECTION_TYPE = Setting_SELECTION_TYPE.ITM_ATM_OTM Then
                If drow("CP") = "P" Then
                    drow("deltavval") = Math.Round(Math.Abs(mVDelta), roundDelta) 'Math.Round(1 - mVDelta, roundDelta)
                Else
                    drow("deltavval") = Math.Round(mVDelta, roundDelta)
                End If
            Else
                If drow("CP") = "P" Then
                    drow("deltavval") = Math.Round(mVDelta + 1, roundDelta)
                Else
                    drow("deltavval") = Math.Round(mVDelta, roundDelta)
                End If

            End If

            drow("delta") = Math.Round(mDelta, roundDelta)

            REM implimenting of Dalta Calc Base On Lot or Unit Setting  By viral 8-07-11
            'drow("deltaval") = Math.Round(mDelta * qty, roundDelta_Val)
            If iDeltaLSize <= 1 Then
                drow("deltaval") = Math.Round(mDelta * qty, roundDelta_Val)
                drow("deltaval1") = Math.Round(mDelta1 * qty, roundDelta_Val)
            Else
                drow("deltaval") = (Math.Round((mDelta * qty) / iDeltaLSize, roundDelta_Val))
                drow("deltaval1") = (Math.Round((mDelta1 * qty) / iDeltaLSize, roundDelta_Val))
            End If


            drow("theta") = Math.Round(mThita, roundTheta)
            If iThetaLSize <= 1 Then
            drow("thetaval") = Math.Round(mThita * qty, roundTheta_Val)
            Else
                drow("thetaval") = Math.Round((mThita * qty) / iThetaLSize, roundTheta_Val)
            End If

            '#########################################keval(29-06-2010)
            'If mThita < 0 Then
            '    If Math.Abs(mThita) > b Then
            '        drow("theta") = -Math.Round(b, roundTheta)
            '        drow("thetaval") = Math.Round(Val(-b) * qty, roundTheta_Val)
            '    End If
            'Else
            '    If Math.Abs(mThita) > b Then
            '        drow("theta") = Math.Round(b, roundTheta)
            '        drow("thetaval") = Math.Round(Val(b) * qty, roundTheta_Val)
            '    End If
            'End If
            '######################################End
            If mThita < 0 Then
                If Math.Abs(mThita) > cpprice Then
                    drow("theta") = -Math.Round(cpprice, roundTheta)
                    If iThetaLSize <= 1 Then
                    drow("thetaval") = Math.Round(Val(drow("theta")) * qty, roundTheta_Val)
                    Else
                        drow("thetaval") = Math.Round((Val(drow("theta")) * qty) / iThetaLSize, roundTheta_Val)
                    End If
                End If
            Else
                If Math.Abs(mThita) > cpprice Then
                    drow("theta") = Math.Round(cpprice, roundTheta)
                    If iThetaLSize <= 1 Then
                    drow("thetaval") = Math.Round(Val(drow("theta")) * qty, roundTheta_Val)
                    Else
                        drow("thetaval") = Math.Round((Val(drow("theta")) * qty) / iThetaLSize, roundTheta_Val)
                End If
            End If
            End If

            If iThetaLSize <= 1 Then
            drow("thetaval1") = Math.Round(mThita1 * qty, roundTheta_Val)
            Else
                drow("thetaval1") = Math.Round((mThita1 * qty) / iThetaLSize, roundTheta_Val)
            End If

            If mThita1 < 0 Then
                If Math.Abs(mThita1) > b Then
                    If iThetaLSize <= 1 Then
                    drow("thetaval1") = Math.Round(-Math.Round(b, roundTheta) * qty, roundTheta_Val)
                    Else
                        drow("thetaval1") = Math.Round((-Math.Round(b, roundTheta) * qty) / iThetaLSize, roundTheta_Val)
                End If
                End If
            Else
                If Math.Abs(mThita) > b Then
                    If iThetaLSize <= 1 Then
                    drow("thetaval1") = Math.Round(Math.Round(b, roundTheta) * qty, roundTheta_Val)
                    Else
                        drow("thetaval1") = Math.Round((Math.Round(b, roundTheta) * qty) / iThetaLSize, roundTheta_Val)
                End If
            End If
            End If

            'Gamma
            drow("gamma") = Math.Round(mGama, roundGamma)
            If iGammaLSize <= 1 Then
                drow("gmval") = Math.Round(mGama * qty, roundGamma_Val)
                drow("gmval1") = Math.Round(mGama1 * qty, roundGamma_Val)
            Else
                drow("gmval") = Math.Round((mGama * qty) / iGammaLSize, roundGamma_Val)
                drow("gmval1") = Math.Round((mGama1 * qty) / iGammaLSize, roundGamma_Val)
            End If
            
            'Vega
            drow("vega") = Math.Round(mVega, roundVega)
            If iVegaLSize <= 1 Then
            drow("vgval") = Math.Round(mVega * qty, roundVega_Val)
            drow("vgval1") = Math.Round(mVega1 * qty, roundVega_Val)
            Else
                drow("vgval") = Math.Round((mVega * qty) / iVegaLSize, roundVega_Val)
                drow("vgval1") = Math.Round((mVega1 * qty) / iVegaLSize, roundVega_Val)
            End If
            

            'Volga
            drow("volga") = Math.Round(mVolga, roundVolga)
            If iVolgaLSize <= 1 Then
            drow("volgaval") = Math.Round(mVolga * qty, roundVolga_Val)
            drow("volgaval1") = Math.Round(mVolga1 * qty, roundVolga_Val)
            Else
                drow("volgaval") = Math.Round((mVolga * qty) / iVolgaLSize, roundVolga_Val)
                drow("volgaval1") = Math.Round((mVolga1 * qty) / iVolgaLSize, roundVolga_Val)
            End If
            

            'Vanna
            drow("vanna") = Math.Round(mVanna, roundVanna)
            If iVannaLSize <= 1 Then
            drow("vannaval") = Math.Round(mVanna * qty, roundVanna_Val)
            drow("vannaval1") = Math.Round(mVanna1 * qty, roundVanna_Val)
            Else
                drow("vannaval") = Math.Round((mVanna * qty) / iVannaLSize, roundVanna_Val)
                drow("vannaval1") = Math.Round((mVanna1 * qty) / iVannaLSize, roundVanna_Val)
            End If

            If mDelta <> 0 Then
                drow("DeltaN") = (Val(txttdelval.Text) / mDelta).ToString(NeutralizeStr) * -1
            Else
                drow("DeltaN") = 0
            End If
            If mGama <> 0 Then
                drow("GammaN") = (Val(txttgmval.Text) / mGama).ToString(NeutralizeStr) * -1
            Else
                drow("GammaN") = 0
            End If
            If mVega <> 0 Then
                drow("VegaN") = (Val(txttvgval.Text) / mVega).ToString(NeutralizeStr) * -1
            Else
                drow("VegaN") = 0
            End If
            If mThita <> 0 Then
                drow("ThetaN") = (Val(txttthval.Text) / mThita).ToString(NeutralizeStr) * -1
            Else
                drow("ThetaN") = 0
            End If
            If mVolga <> 0 Then
                drow("VolgaN") = (Val(txttvolgaval.Text) / mVolga).ToString(NeutralizeStr) * -1
            Else
                drow("VolgaN") = 0
            End If
            If mVanna <> 0 Then
                drow("VannaN") = (Val(txttvannaval.Text) / mVanna).ToString(NeutralizeStr) * -1
            Else
                drow("VannaN") = 0
            End If


            'if mdate is today then sel all greek value to zero
            If drow("mdate") = Today.Date Then
                drow("deltaval1") = 0
                drow("thetaval1") = 0
                drow("vgval1") = 0
                drow("gmval1") = 0
            End If
            ' For Each grow As DataRow In gcurrtable.Select("tokanno='" & CLng(drow("tokanno")) & "'")
            'update all value to gcurrtable
            If index > -1 Then

                gcurrtable.Rows(index)("last") = cpprice
                gcurrtable.Rows(index)("last1") = cpprice1
                gcurrtable.Rows(index)("lv") = mVolatility * 100 'Math.Round(mVolatility * 100, 2)
                gcurrtable.Rows(index)("lv1") = mVolatility1 * 100 'Math.Round(mVolatility1 * 100, 2)
                gcurrtable.Rows(index)("delta") = Math.Round(mDelta, roundDelta)

                'gcurrtable.Rows(index)("deltaval") = Math.Round(mDelta * qty, roundDelta_Val)
                If iDeltaLSize <= 1 Then
                    gcurrtable.Rows(index)("deltaval") = Math.Round(mDelta * qty, roundDelta_Val)
                    gcurrtable.Rows(index)("deltaval1") = Math.Round(mDelta1 * qty, roundDelta_Val)
                Else
                    gcurrtable.Rows(index)("deltaval") = (Math.Round((mDelta * qty) / iDeltaLSize, roundDelta_Val))
                    gcurrtable.Rows(index)("deltaval1") = (Math.Round((mDelta1 * qty) / iDeltaLSize, roundDelta_Val))
                End If

                'theta
                gcurrtable.Rows(index)("theta") = Math.Round(mThita, roundTheta)
                If iThetaLSize <= 1 Then
                gcurrtable.Rows(index)("thetaval") = Math.Round(mThita * qty, roundTheta_Val)
                Else
                    gcurrtable.Rows(index)("thetaval") = Math.Round((mThita * qty) / iThetaLSize, roundTheta_Val)
                End If
                '###################################keval(29-06-2010)
                'If mThita < 0 Then
                '    If Math.Abs(mThita) > b Then
                '        gcurrtable.Rows(index)("theta") = -Math.Round(b, roundTheta)
                '        gcurrtable.Rows(index)("thetaval") = Math.Round(-b * qty, roundTheta_Val)
                '    End If
                'Else
                '    If Math.Abs(mThita) > b Then
                '        gcurrtable.Rows(index)("theta") = Math.Round(b, roundTheta)
                '        gcurrtable.Rows(index)("thetaval") = Math.Round(b * qty, roundTheta_Val)
                '    End If
                'End If
                '###################################End
                '-----------------------------------------------
                If mThita < 0 Then
                    If Math.Abs(mThita) > cpprice Then
                        gcurrtable.Rows(index)("theta") = -Math.Round(cpprice, roundTheta)
                    End If
                Else
                    If Math.Abs(mThita) > cpprice Then
                        gcurrtable.Rows(index)("theta") = Math.Round(cpprice, roundTheta)
                    End If
                End If

                If iThetaLSize <= 1 Then
                gcurrtable.Rows(index)("thetaval") = Math.Round(Val(gcurrtable.Rows(index)("theta")) * qty, roundTheta_Val)
                Else
                    gcurrtable.Rows(index)("thetaval") = Math.Round((Val(gcurrtable.Rows(index)("theta")) * qty) / iThetaLSize, roundTheta_Val)
                End If
                '-------------------------------------------------

                'gamma
                gcurrtable.Rows(index)("gamma") = Math.Round(mGama, roundGamma)
                If iGammaLSize <= 1 Then
                    gcurrtable.Rows(index)("gmval") = Math.Round(mGama * qty, roundGamma_Val)
                Else
                    gcurrtable.Rows(index)("gmval") = Math.Round((mGama * qty) / iGammaLSize, roundGamma_Val)
                End If

                'Vega
                gcurrtable.Rows(index)("vega") = Math.Round(mVega, roundVega)
                If iVegaLSize <= 1 Then
                gcurrtable.Rows(index)("vgval") = Math.Round(mVega * qty, roundVega_Val)
                Else
                    gcurrtable.Rows(index)("vgval") = Math.Round((mVega * qty) / iVegaLSize, roundVega_Val)
                End If

                'Volga
                gcurrtable.Rows(index)("volga") = Math.Round(mVolga, roundVolga)
                If iVolgaLSize <= 1 Then
                gcurrtable.Rows(index)("Volgaval") = Math.Round(mVolga * qty, roundVolga_Val)
                Else
                    gcurrtable.Rows(index)("Volgaval") = Math.Round((mVolga * qty) / iVolgaLSize, roundVolga_Val)
                End If


                'Vanna
                gcurrtable.Rows(index)("vanna") = Math.Round(mVanna, roundVanna)
                If iVannaLSize <= 1 Then
                gcurrtable.Rows(index)("vannaVal") = Math.Round(mVanna * qty, roundVanna_Val)
                Else
                    gcurrtable.Rows(index)("vannaVal") = Math.Round((mVanna * qty) / iVannaLSize, roundVanna_Val)
                End If


                REM Calculation For UpDown Analysis By Viral
                If SELECTION_TYPE = Setting_SELECTION_TYPE.ITM_ATM_OTM Then
                    If gcurrtable.Rows(index)("CP") = "P" Then
                        gcurrtable.Rows(index)("deltavval") = Math.Round(Math.Abs(mVDelta), roundDelta) 'Math.Round(1 - mVDelta, roundDelta)
                    Else
                        gcurrtable.Rows(index)("deltavval") = Math.Round(mVDelta, roundDelta)
                    End If
                Else
                    If gcurrtable.Rows(index)("CP") = "P" Then
                        gcurrtable.Rows(index)("deltavval") = Math.Round(mVDelta + 1, roundDelta)
                    Else
                        gcurrtable.Rows(index)("deltavval") = Math.Round(mVDelta, roundDelta)
                    End If
                End If

            End If
            ' End If
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub

    ''' <summary>
    ''' cal_futureMt
    ''' </summary>
    ''' <remarks>This method upadate LTP and Greek value for Future and Option position For Update Maintable</remarks>
    Private Sub cal_futureMt(ByVal CmpName As String)
        Try
            If (fltpprice.Count > 0 Or Currfltpprice.Count > 0) Then 'if fitpprice hashtable contains token of FO then calculate
                Dim ltppr As Double = 0
                Dim ltppr1 As Double = 0
                Dim fltppr As Double = 0
                Dim eltppr As Double = 0
                Dim mt As Double = 0
                Dim mmt As Double = 0
                Dim isfut As Boolean = False
                Dim iscall As Boolean = False
                Dim dt As Date
                'IsCalc Field For  Selected Rows Calculation By Viral 22nd June 11
                For Each drow As DataRow In maintable.Select("company='" & CmpName & "'")
                    If drow("IsCurrency") = False Then
                        If drow("cp") = "E" Then
                            'if equity then do nothing
                            REM Fut and Eq Delat Neytralize calc
                            Try
                                If (drow("cp") = "F" Or drow("cp") = "E") Then
                                    If drow("delta") <> 0 Then
                                        drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                    End If
                                End If
                            Catch ex As Exception

                            End Try
                            If iDeltaLSize > 1 Then
                                drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                                drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            End If
                        ElseIf drow("CP") = "F" Then 'for future
                            If pan11 = True Then   'if panel1 is visible
                                If date1 = CDate(drow("fut_mdate")).Date Then 'check if expdate is equal ot date1
                                    If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(fltpprice(CLng(drow("tokanno"))))  'get LTP of current future token
                                        For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' and tokanno=" & CLng(drow("tokanno")) & " and fut_mdate='" & date1 & "'")  'update LTP to currtable
                                            fdrow("last") = fltppr
                                        Next
                                        txtrate.Invoke(mdel, fltppr) 'display LTP to txtrate
                                        isfut = True 'set future flag
                                        dt = date1 'store first date
                                    End If
                                End If
                            End If
                            If pan21 = True Then 'if panel2 is visible
                                If date2 = CDate(drow("fut_mdate")).Date Then  'check if exp date is equal to date2
                                    If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(fltpprice(CLng(drow("tokanno")))) 'select LTP of token
                                        'txtrate1.Text = fltppr
                                        txtrate1.Invoke(mdel1, fltppr) 'display LTP to txtrate1 textbox
                                        For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' And tokanno=" & CLng(drow("tokanno")) & " and fut_mdate='" & date2 & "'")
                                            fdrow("last") = fltppr  'update LTP to currtable
                                        Next
                                        isfut = True
                                        dt = date2
                                    End If
                                End If
                            End If
                            If pan31 = True Then 'if pane3 is visible
                                If date3 = CDate(drow("fut_mdate")).Date Then 'check if exp date is equal to date3
                                    If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                        'txtrate2.Text = fltppr
                                        For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' And tokanno=" & CLng(drow("tokanno")) & " and fut_mdate='" & date3 & "'")
                                            fdrow("last") = fltppr 'update LTP to currtable
                                        Next
                                        txtrate2.Invoke(mdel2, fltppr) 'display LTP to txtrate2
                                        isfut = True
                                        dt = date3
                                    End If
                                End If
                            End If
                            REM Fut and Eq Delat Neytralize calc
                            Try

                            
                                If (drow("cp") = "F" Or drow("cp") = "E") Then
                                    If drow("delta") <> 0 Then
                                        drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                    End If
                                End If
                            Catch ex As Exception

                            End Try
                            If iDeltaLSize > 1 Then
                                drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                                drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            End If
                        Else  'calculate for OPTION
                            If pan11 = True Then  'if panel1 is visible
                                If date1 = CDate(drow("fut_mdate")).Date Then  'if expiry date = date1
                                    If fltpprice.Contains(CLng(drow("ftoken"))) Then   'select LTP from fltpprice hashtable
                                        fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                        ' check which ltp is required: future or equity
                                        If chkCalVol.Checked = True Then
                                            'calculate vol using equity is clicked then select LTP of asset_token
                                            REM Unhandled exception has occurred after "Calculate Vol using Equity" checkbox is checked 
                                            If IsDBNull(drow("asset_tokan")) = False Then
                                                eltppr = Val(eltpprice(CLng(drow("asset_tokan"))))
                                            Else
                                                eltppr = 0
                                            End If
                                            txteqrate.Text = eltppr
                                        End If

                                        'if selected company is INDEX  then set quityrate = 0
                                        ' If currtable.Rows(0).Item("script").ToString.Substring(3, 3) <> "IDX" Then
                                        'Else
                                        '   txteqrate.Text = 0
                                        'End If

                                        For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' And tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date1 & "'")
                                            fdrow("last") = fltppr  'update LTP to currtable
                                        Next

                                        txtrate.Invoke(mdel, fltppr) 'display LTP to txtrate
                                        isfut = True
                                        dt = date1
                                    End If
                                End If
                            End If
                            If pan21 = True Then 'if panel2 is visible
                                If date2 = CDate(drow("fut_mdate")).Date Then 'chech if expdate =date2
                                    If fltpprice.Contains(CLng(drow("ftoken"))) Then
                                        'select LTP from fltpprice hashtable
                                        fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                        ' check which ltp is required future or equity
                                        If chkCalVol.Checked Then
                                            'calculate vol using equity is clicked then select LTP of asset_token
                                            eltppr = Val(eltpprice(CLng(Val(drow("asset_tokan") & ""))))
                                            txteqrate.Text = eltppr
                                        End If
                                        'If currtable.Rows(0).Item("script").ToString.Substring(3, 3) <> "IDX" Then
                                        'Else
                                        '   txteqrate.Text = 0
                                        'End If

                                        txtrate1.Invoke(mdel1, fltppr)
                                        For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' And tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date2 & "'")
                                            fdrow("last") = fltppr 'update LTP to currtable
                                        Next
                                        isfut = True
                                        dt = date2
                                    End If
                                End If
                            End If
                            If pan31 = True Then 'if panel3 is visible
                                If date3 = CDate(drow("fut_mdate")).Date Then 'if expitydate = date3
                                    If fltpprice.Contains(CLng(drow("ftoken"))) Then
                                        'select LTP of ftoken
                                        fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                        For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' And tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date3 & "'")
                                            fdrow("last") = fltppr 'update LTP to currtable
                                        Next
                                        ' check which ltp is required future or equity
                                        If chkCalVol.Checked Or thrworking = False Then
                                            'calculate vol using equity is clicked then select LTP of asset_token
                                            eltppr = Val(eltpprice(CLng(Val(drow("asset_tokan").ToString))))
                                            'If drow("asset_tokan").ToString Then
                                            '    eltppr = Val(eltpprice(CLng(drow("asset_tokan"))))
                                            'End If
                                            txteqrate.Text = eltppr
                                        End If
                                        'if selected company is INDEX then set equityrate=0
                                        'If currtable.Rows(0).Item("script").ToString.Substring(3, 3) <> "IDX" Then

                                        'Else
                                        '    txteqrate.Text = 0
                                        '  End If


                                        txtrate2.Invoke(mdel2, fltppr)
                                        isfut = True
                                        dt = date3
                                    End If
                                End If
                            End If

                            'if ltpprice contains tokenno or token1  then calculate greek values
                            If ltpprice.Contains(CLng(drow("tokanno"))) Or ltpprice.Contains(CLng(drow("token1"))) Then
                                Dim index As Integer = -1
                                'select index of position
                                'For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("tokanno")) & "")
                                '    index = gcurrtable.Rows.IndexOf(grow)
                                'Next
                                'if isliq is true then ltppr1 is LTP of token1
                                If CBool(drow("isliq")) = True Then
                                    ltppr = Val(ltpprice(CLng(drow("tokanno"))))
                                    ltppr1 = Val(ltpprice(CLng(drow("token1"))))
                                Else
                                    ltppr = Val(ltpprice(CLng(drow("tokanno"))))
                                    ltppr1 = 0
                                End If

                                'set iscall flag
                                If drow("cp") = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                                mmt = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, CInt(Val(txtnoofday.Text)), Now.Date), CDate(drow("mdate")).Date)
                                If Now.Date = CDate(drow("mdate")).Date Then
                                    mt += 0.5
                                End If
                                If CDate(DateAdd(DateInterval.Day, CInt(Val(txtnoofday.Text)), Now())).Date = CDate(drow("mdate")).Date Then
                                    mmt += 0.5
                                End If

                                'If fltppr <> 0 Then 'if LTP<>0 then calculate greek values
                                If chkCalVol.Checked = True Then 'if chkcalvol is clicked then LTP of asset_token is passed to function
                                    If eltppr <> 0 Then CalData(eltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, CmpName)
                                Else
                                    'If fltppr <> 0 Then CalData(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname)
                                    'Alpesh  Setting for Call value and put value Cal
                                    If fltppr <> 0 Then
                                        If thrworking = True Then
                                            CalData(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, CmpName)
                                        Else
                                            If iscall = True Then
                                                CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, CmpName, drow("lv"), Val(drow("lv1").ToString), True)
                                            Else
                                                CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, CmpName, Val(drow("lv1").ToString), drow("lv"), True)
                                            End If
                                        End If
                                    End If
                                End If
                                'End If
                            End If
                            ' Next
                        End If
                    ElseIf drow("ISCurrency") = True Then '' if Currency Script Value change
                        If drow("cp") = "E" Then
                            'if equity then do nothing
                            REM Fut and Eq Delat Neytralize calc
                            Try

                            
                                If (drow("cp") = "F" Or drow("cp") = "E") Then
                                    If drow("delta") <> 0 Then
                                        drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                    End If
                                End If
                            Catch ex As Exception

                            End Try
                            If iDeltaLSize > 1 Then
                                drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                                drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            End If
                        ElseIf drow("CP") = "F" Then 'for future
                            If pan11 = True Then   'if panel1 is visible
                                If date1 = CDate(drow("fut_mdate")).Date Then
                                    If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))  'get LTP of current future token
                                        For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' And Script='" & drow("Script") & "'")  'update LTP to currtable
                                            fdrow("last") = fltppr
                                        Next
                                        txtrate.Invoke(mdel, fltppr) 'display LTP to txtrate
                                        isfut = True 'set future flag
                                        dt = date1 'store first date
                                    End If
                                End If
                            End If
                            If pan21 = True Then 'if panel2 is visible
                                If date2 = CDate(drow("fut_mdate")).Date Then
                                    If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(Currfltpprice(CLng(drow("tokanno")))) 'select LTP of token
                                        'txtrate1.Text = fltppr
                                        txtrate1.Invoke(mdel1, fltppr) 'display LTP to txtrate1 textbox
                                        For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' And Script='" & drow("Script") & "'")
                                            fdrow("last") = fltppr  'update LTP to currtable
                                        Next
                                        isfut = True
                                        dt = date2
                                    End If
                                End If
                            End If
                            If pan31 = True Then 'if pane3 is visible
                                If date3 = CDate(drow("fut_mdate")).Date Then
                                    If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))
                                        'txtrate2.Text = fltppr
                                        For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' And Script='" & drow("Script") & "'")
                                            fdrow("last") = fltppr 'update LTP to currtable
                                        Next
                                        txtrate2.Invoke(mdel2, fltppr) 'display LTP to txtrate2
                                        isfut = True
                                        dt = date3
                                    End If
                                End If
                            End If

                            REM Fut and Eq Delat Neytralize calc
                            Try

                            
                                If (drow("cp") = "F" Or drow("cp") = "E") Then
                                    If drow("delta") <> 0 Then
                                        drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                    End If
                                End If
                            Catch ex As Exception

                            End Try
                            If iDeltaLSize > 1 Then
                                drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                                drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            End If
                        Else  'calculate for OPTION From Currency Contract
                            If pan11 = True Then  'if panel1 is visible
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then   'select LTP from fltpprice hashtable

                                    ' check which ltp is required: future or equity
                                    If chkCalVol.Checked = True Then
                                        'calculate vol using equity is clicked then select LTP of asset_token
                                        'eltppr = Val(Curreltpprice(CLng(drow("asset_tokan"))))
                                        'txteqrate.Text = eltppr
                                    End If
                                    Dim LTPPRice As Double
                                    LTPPRice = Val(Currltpprice(CLng(drow("TokanNo"))))
                                    'For Each fdrow As DataRow In currtable.Select("company='" & compname & "' AND tokanno=" & CLng(drow("ftoken")) & "")
                                    '    fdrow("last") = fltppr  'update LTP to currtable
                                    'Next
                                    'For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' AND tokanno=" & CLng(drow("ftoken")) & "")
                                    '    grow("last") = fltppr 'update LTP to gcurrtable
                                    'Next

                                    If date1 = CDate(drow("fut_mdate")).Date Then
                                        fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                        txtrate.Invoke(mdel, fltppr) 'display LTP to txtrate
                                    End If
                                    isfut = True
                                    dt = date1
                                End If
                            End If
                            If pan21 = True Then 'if panel2 is visible
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                                    ' check which ltp is required future or equity
                                    If chkCalVol.Checked Then
                                        'calculate vol using equity is clicked then select LTP of asset_token
                                        'eltppr = Val(Curreltpprice(CLng(drow("asset_tokan"))))
                                        'txteqrate.Text = eltppr
                                    End If
                                    For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' And tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date2 & "'")
                                        fdrow("last") = fltppr 'update LTP to currtable
                                    Next

                                    If date2 = CDate(drow("fut_mdate")).Date Then
                                        'select LTP from fltpprice hashtable
                                        fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                        txtrate1.Invoke(mdel1, fltppr)
                                    End If

                                    isfut = True
                                    dt = date2
                                End If
                            End If
                            If pan31 = True Then 'if panel3 is visible
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                                    For Each fdrow As DataRow In maintable.Select("company='" & CmpName & "' And tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date3 & "'")
                                        fdrow("last") = fltppr 'update LTP to currtable
                                    Next
                                    ' check which ltp is required future or equity
                                    If chkCalVol.Checked Or thrworking = False Then
                                        'calculate vol using equity is clicked then select LTP of asset_token
                                        'eltppr = Val(Curreltpprice(CLng(drow("asset_tokan"))))
                                        'txteqrate.Text = eltppr
                                    End If

                                    If date3 = CDate(drow("fut_mdate")).Date Then
                                        'select LTP of ftoken
                                        fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                        txtrate2.Invoke(mdel2, fltppr)
                                    End If
                                    isfut = True
                                    dt = date3
                                End If
                            End If
                            'if ltpprice contains tokenno or token1  then calculate greek values
                            If Currltpprice.Contains(CLng(drow("tokanno"))) Or Currltpprice.Contains(CLng(drow("token1"))) Then
                                Dim index As Integer = -1
                                'select index of position
                                'For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("tokanno")) & "")
                                '    index = gcurrtable.Rows.IndexOf(grow)
                                'Next
                                'if isliq is true then ltppr1 is LTP of token1
                                If CBool(drow("isliq")) = True Then
                                    ltppr = Val(Currltpprice(CLng(drow("tokanno"))))
                                    ltppr1 = Val(Currltpprice(CLng(drow("token1"))))
                                Else
                                    ltppr = Val(Currltpprice(CLng(drow("tokanno"))))
                                    ltppr1 = 0
                                End If

                                'set iscall flag
                                If drow("cp") = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If



                                mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                                mmt = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now.Date), CDate(drow("mdate")).Date)
                                If Now.Date = CDate(drow("mdate")).Date Then
                                    mt += 0.5
                                End If
                                If CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date = CDate(drow("mdate")).Date Then
                                    mmt += 0.5
                                End If

                                If chkCalVol.Checked = True Then 'if chkcalvol is clicked then LTP of asset_token is passed to function
                                    If eltppr <> 0 Then CalData(eltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, CmpName)
                                Else
                                    If fltppr <> 0 Then CalData(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, CmpName)
                                End If
                            End If
                            ' Next
                        End If
                    End If
                Next
                'If Me.InvokeRequired Then
                'Me.Invoke(munrealize)
                ' Me.Invoke(mval)
                'End If

            End If
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub

    Private Function isNextOfFar(ByVal dt As Date) As Boolean
        If DateDiff(DateInterval.Month, Today.Date, dt) >= 3 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' cal_future
    ''' </summary>
    ''' <remarks>This method upadate LTP and Greek value for Future and Option position</remarks>
    Private Sub cal_future()
        Try
            If (fltpprice.Count > 0 Or Currfltpprice.Count > 0) Then 'if fitpprice hashtable contains token of FO then calculate
                Dim ltppr As Double = 0
                Dim ltppr1 As Double = 0
                Dim fltppr As Double = 0
                Dim eltppr As Double = 0
                Dim mt As Double = 0
                Dim mmt As Double = 0
                Dim isfut As Boolean = False
                Dim iscall As Boolean = False
                Dim dt As Date
                'IsCalc Field For  Selected Rows Calculation By Viral 22nd June 11
                For Each drow As DataRow In currtable.Select("company='" & compname & "' And Iscalc=True")
                    If drow("IsCurrency") = False Then
                        If drow("cp") = "E" Then
                            'if equity then do nothing
                            REM Fut and Eq Delat Neytralize calc
                            Try
                                If (drow("cp") = "F" Or drow("cp") = "E") Then
                                    If drow("delta") <> 0 Then
                                        drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                    End If
                                End If
                            Catch ex As Exception

                            End Try
                            If iDeltaLSize > 1 Then
                                drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                                drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            End If
                        ElseIf drow("CP") = "F" Then 'for future
                            If pan11 = True Then   'if panel1 is visible
                                If date1 = CDate(drow("fut_mdate")).Date Then 'check if expdate is equal ot date1
                                    If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(fltpprice(CLng(drow("tokanno"))))  'get LTP of current future token
                                        For Each fdrow As DataRow In currtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("tokanno")) & " and fut_mdate='" & date1 & "'")  'update LTP to currtable
                                            fdrow("last") = fltppr
                                        Next
                                        For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("tokanno")) & " and fut_mdate='" & date1 & "'")
                                            grow("last") = fltppr 'update LTP to gcurrtable
                                        Next
                                        txtrate.Invoke(mdel, fltppr) 'display LTP to txtrate
                                        isfut = True 'set future flag
                                        dt = date1 'store first date

                                    End If
                                End If
                            End If
                            If pan21 = True Then 'if panel2 is visible
                                If date2 = CDate(drow("fut_mdate")).Date Then  'check if exp date is equal to date2
                                    If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(fltpprice(CLng(drow("tokanno")))) 'select LTP of token
                                        'txtrate1.Text = fltppr
                                        txtrate1.Invoke(mdel1, fltppr) 'display LTP to txtrate1 textbox
                                        For Each fdrow As DataRow In currtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("tokanno")) & " and fut_mdate='" & date2 & "'")
                                            fdrow("last") = fltppr  'update LTP to currtable
                                        Next
                                        For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("tokanno")) & " and fut_mdate='" & date2 & "'")
                                            grow("last") = fltppr 'update LTP to gcurrtable
                                        Next
                                        isfut = True
                                        dt = date2
                                    End If
                                End If
                            End If
                            If pan31 = True Then 'if pane3 is visible
                                If date3 = CDate(drow("fut_mdate")).Date Then 'check if exp date is equal to date3
                                    If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                        'txtrate2.Text = fltppr
                                        For Each fdrow As DataRow In currtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("tokanno")) & " and fut_mdate='" & date3 & "'")
                                            fdrow("last") = fltppr 'update LTP to currtable
                                        Next
                                        For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("tokanno")) & " and fut_mdate='" & date3 & "'")
                                            grow("last") = fltppr 'update LTP to gcurrtable
                                        Next
                                        txtrate2.Invoke(mdel2, fltppr) 'display LTP to txtrate2
                                        isfut = True
                                        dt = date3
                                    End If
                                End If
                            End If
                            REM Fut and Eq Delat Neytralize calc
                            Try

                                If (drow("cp") = "F" Or drow("cp") = "E") Then
                                    If drow("delta") <> 0 Then
                                        drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                    End If
                                End If

                            Catch ex As Exception

                            End Try


                            If iDeltaLSize > 1 Then
                                drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                                drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            End If
 
                        Else  'calculate for OPTION
                            If pan11 = True Then  'if panel1 is visible
                                If date1 = CDate(drow("fut_mdate")).Date Then  'if expiry date = date1
                                    If fltpprice.Contains(CLng(drow("ftoken"))) Then   'select LTP from fltpprice hashtable

                                        fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                        ' check which ltp is required: future or equity
                                        If chkCalVol.Checked = True Then

                                            'calculate vol using equity is clicked then select LTP of asset_token
                                            REM Unhandled exception has occurred after "Calculate Vol using Equity" checkbox is checked 
                                            If IsDBNull(drow("asset_tokan")) = False Then
                                                eltppr = Val(eltpprice(CLng(drow("asset_tokan"))))
                                            Else
                                                eltppr = 0
                                            End If
                                            txteqrate.Text = eltppr
                                        End If

                                        'if selected company is INDEX  then set quityrate = 0
                                        ' If currtable.Rows(0).Item("script").ToString.Substring(3, 3) <> "IDX" Then
                                        'Else
                                        '   txteqrate.Text = 0
                                        'End If


                                        For Each fdrow As DataRow In currtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date1 & "'")
                                            fdrow("last") = fltppr  'update LTP to currtable
                                        Next
                                        For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date1 & "'")
                                            grow("last") = fltppr 'update LTP to gcurrtable
                                        Next

                                        txtrate.Invoke(mdel, fltppr) 'display LTP to txtrate
                                        isfut = True
                                        dt = date1
                                    End If
                                End If
                            End If
                            If pan21 = True Then 'if panel2 is visible
                                If date2 = CDate(drow("fut_mdate")).Date Then 'chech if expdate =date2
                                    If fltpprice.Contains(CLng(drow("ftoken"))) Then
                                        'select LTP from fltpprice hashtable
                                        fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                        ' check which ltp is required future or equity
                                        If chkCalVol.Checked Then
                                            'calculate vol using equity is clicked then select LTP of asset_token
                                            eltppr = Val(eltpprice(CLng(Val(drow("asset_tokan") & ""))))
                                            txteqrate.Text = eltppr
                                        End If
                                        'If currtable.Rows(0).Item("script").ToString.Substring(3, 3) <> "IDX" Then
                                        'Else
                                        '   txteqrate.Text = 0
                                        'End If

                                        txtrate1.Invoke(mdel1, fltppr)
                                        For Each fdrow As DataRow In currtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date2 & "'")
                                            fdrow("last") = fltppr 'update LTP to currtable
                                        Next
                                        For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date2 & "'")
                                            grow("last") = fltppr 'update LTP to currtable
                                        Next
                                        isfut = True
                                        dt = date2
                                    End If
                                End If
                            End If
                            If pan31 = True Then 'if panel3 is visible
                                If date3 = CDate(drow("fut_mdate")).Date Then 'if expitydate = date3
                                    If fltpprice.Contains(CLng(drow("ftoken"))) Then
                                        'select LTP of ftoken
                                        fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                        For Each fdrow As DataRow In currtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date3 & "'")
                                            fdrow("last") = fltppr 'update LTP to currtable
                                        Next
                                        For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date3 & "'")
                                            grow("last") = fltppr 'update LTP to gcurrtable
                                        Next
                                        ' check which ltp is required future or equity
                                        If chkCalVol.Checked Or thrworking = False Then
                                            'calculate vol using equity is clicked then select LTP of asset_token
                                            eltppr = Val(eltpprice(CLng(Val(drow("asset_tokan").ToString))))
                                            'If drow("asset_tokan").ToString Then
                                            '    eltppr = Val(eltpprice(CLng(drow("asset_tokan"))))
                                            'End If
                                            txteqrate.Text = eltppr
                                        End If
                                        'if selected company is INDEX then set equityrate=0
                                        'If currtable.Rows(0).Item("script").ToString.Substring(3, 3) <> "IDX" Then

                                        'Else
                                        '    txteqrate.Text = 0
                                        '  End If


                                        txtrate2.Invoke(mdel2, fltppr)
                                        isfut = True
                                        dt = date3
                                    End If
                                End If
                            End If

                            'if ltpprice contains tokenno or token1  then calculate greek values
                            If ltpprice.Contains(CLng(drow("tokanno"))) Or ltpprice.Contains(CLng(drow("token1"))) Then
                                Dim index As Integer = -1
                                'select index of position
                                For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("tokanno")) & "")
                                    index = gcurrtable.Rows.IndexOf(grow)
                                Next
                                'if isliq is true then ltppr1 is LTP of token1
                                If CBool(drow("isliq")) = True Then
                                    ltppr = Val(ltpprice(CLng(drow("tokanno"))))
                                    ltppr1 = Val(ltpprice(CLng(drow("token1"))))
                                Else
                                    ltppr = Val(ltpprice(CLng(drow("tokanno"))))
                                    ltppr1 = 0
                                End If
                                'set iscall flag
                                If drow("cp") = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                                mmt = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, CInt(Val(txtnoofday.Text)), Now.Date), CDate(drow("mdate")).Date)
                                If Now.Date = CDate(drow("mdate")).Date Then
                                    mt += 0.5
                                End If
                                If CDate(DateAdd(DateInterval.Day, CInt(Val(txtnoofday.Text)), Now())).Date = CDate(drow("mdate")).Date Then
                                    mmt += 0.5
                                End If

                                'If fltppr <> 0 Then 'if LTP<>0 then calculate greek values
                                If chkCalVol.Checked = True Then 'if chkcalvol is clicked then LTP of asset_token is passed to function
                                    If eltppr <> 0 Then CalData(eltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname)
                                Else
                                    'If fltppr <> 0 Then CalData(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname)
                                    'Alpesh  Setting for Call value and put value Cal
                                    If fltppr <> 0 Then
                                        If thrworking = True Then
                                            CalData(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname)
                                        Else
                                            If iscall = True Then
                                                CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, drow("lv"), Val(drow("lv1").ToString), True)
                                            Else
                                                CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, Val(drow("lv1").ToString), drow("lv"), True)
                                            End If
                                        End If
                                    End If
                                End If
                                'End If
                            End If
                            ' Next
                        End If
                    ElseIf drow("ISCurrency") = True Then '' if Currency Script Value change
                        If drow("cp") = "E" Then
                            'if equity then do nothing
                            REM Fut and Eq Delat Neytralize calc
                            Try


                                If (drow("cp") = "F" Or drow("cp") = "E") Then
                                    If drow("delta") <> 0 Then
                                        drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                    End If
                                End If
                            Catch ex As Exception

                            End Try
                            If iDeltaLSize > 1 Then
                                drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                                drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            End If
                        ElseIf drow("CP") = "F" Then 'for future
                            If pan11 = True Then   'if panel1 is visible
                                If date1 = CDate(drow("fut_mdate")).Date Then
                                    If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))  'get LTP of current future token
                                        For Each fdrow As DataRow In currtable.Select("Script='" & drow("Script") & "'")  'update LTP to currtable
                                            fdrow("last") = fltppr
                                        Next
                                        For Each grow As DataRow In gcurrtable.Select("Script='" & drow("Script") & "'")
                                            grow("last") = fltppr 'update LTP to gcurrtable
                                        Next
                                        txtrate.Invoke(mdel, fltppr) 'display LTP to txtrate
                                        isfut = True 'set future flag
                                        dt = date1 'store first date
                                    End If
                                End If
                            End If
                            If pan21 = True Then 'if panel2 is visible
                                If date2 = CDate(drow("fut_mdate")).Date Then
                                    If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(Currfltpprice(CLng(drow("tokanno")))) 'select LTP of token
                                        'txtrate1.Text = fltppr
                                        txtrate1.Invoke(mdel1, fltppr) 'display LTP to txtrate1 textbox
                                        For Each fdrow As DataRow In currtable.Select("Script='" & drow("Script") & "'")
                                            fdrow("last") = fltppr  'update LTP to currtable
                                        Next
                                        For Each grow As DataRow In gcurrtable.Select("Script='" & drow("Script") & "'")
                                            grow("last") = fltppr 'update LTP to gcurrtable
                                        Next
                                        isfut = True
                                        dt = date2
                                    End If
                                End If
                            End If
                            If pan31 = True Then 'if pane3 is visible
                                If date3 = CDate(drow("fut_mdate")).Date Then
                                    If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))
                                        'txtrate2.Text = fltppr
                                        For Each fdrow As DataRow In currtable.Select("Script='" & drow("Script") & "'")
                                            fdrow("last") = fltppr 'update LTP to currtable
                                        Next
                                        For Each grow As DataRow In gcurrtable.Select("Script='" & drow("Script") & "'")
                                            grow("last") = fltppr 'update LTP to gcurrtable
                                        Next
                                        txtrate2.Invoke(mdel2, fltppr) 'display LTP to txtrate2
                                        isfut = True
                                        dt = date3
                                    End If
                                End If
                            End If
                            If isNextOfFar(CDate(drow("fut_mdate"))) Then
                                REM B'Cose of LTp Not Come in Nex To far Month in Currency  For H6G
                                If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))  'get LTP of current future token
                                    For Each fdrow As DataRow In currtable.Select("Script='" & drow("Script") & "'")  'update LTP to currtable
                                        fdrow("last") = fltppr
                                        isfut = True 'set future flag
                                        'dt = date1 'store first date
                                        dt = CDate(drow("fut_mdate")).Date
                                    Next
                                    For Each grow As DataRow In gcurrtable.Select("Script='" & drow("Script") & "'")
                                        grow("last") = fltppr 'update LTP to gcurrtable
                                    Next
                                    'txtrate.Invoke(mdel, fltppr) 'display LTP to txtrate
                                    'isfut = True 'set future flag
                                    ''dt = date1 'store first date
                                    'dt = CDate(drow("fut_mdate")).Date
                                End If
                            End If
                            REM Fut and Eq Delat Neytralize calc
                            Try


                                If (drow("cp") = "F" Or drow("cp") = "E") Then
                                    If drow("delta") <> 0 Then
                                        drow("DeltaN") = (Val(txttdelval.Text) / Val(drow("delta") & "")).ToString(NeutralizeStr) * -1
                                    End If
                                End If
                            Catch ex As Exception

                            End Try

                            If iDeltaLSize > 1 Then
                                drow("deltaval") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                                drow("deltaval1") = (Math.Round((drow("delta") * drow("units")) / iDeltaLSize, roundDelta_Val))
                            End If

                        Else  'calculate for OPTION From Currency Contract
                            If pan11 = True Then  'if panel1 is visible
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then   'select LTP from fltpprice hashtable

                                    ' check which ltp is required: future or equity
                                    If chkCalVol.Checked = True Then
                                        'calculate vol using equity is clicked then select LTP of asset_token
                                        'eltppr = Val(Curreltpprice(CLng(drow("asset_tokan"))))
                                        'txteqrate.Text = eltppr
                                    End If
                                    Dim LTPPRice As Double
                                    LTPPRice = Val(Currltpprice(CLng(drow("TokanNo"))))
                                    'For Each fdrow As DataRow In currtable.Select("company='" & compname & "' AND tokanno=" & CLng(drow("ftoken")) & "")
                                    '    fdrow("last") = fltppr  'update LTP to currtable
                                    'Next
                                    'For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' AND tokanno=" & CLng(drow("ftoken")) & "")
                                    '    grow("last") = fltppr 'update LTP to gcurrtable
                                    'Next

                                    If date1 = CDate(drow("fut_mdate")).Date Then
                                        fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                        txtrate.Invoke(mdel, fltppr) 'display LTP to txtrate
                                    End If
                                    isfut = True
                                    dt = date1
                                End If
                            End If
                            If pan21 = True Then 'if panel2 is visible
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                                    REM Change by Alpesh For Currency Problem 
                                    fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                    ' check which ltp is required future or equity
                                    If chkCalVol.Checked Then
                                        'calculate vol using equity is clicked then select LTP of asset_token
                                        'eltppr = Val(Curreltpprice(CLng(drow("asset_tokan"))))
                                        'txteqrate.Text = eltppr
                                    End If
                                    For Each fdrow As DataRow In currtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date2 & "'")
                                        fdrow("last") = fltppr 'update LTP to currtable
                                    Next
                                    For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date2 & "'")
                                        grow("last") = fltppr 'update LTP to currtable
                                    Next

                                    If date2 = CDate(drow("fut_mdate")).Date Then
                                        'select LTP from fltpprice hashtable
                                        fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                        txtrate1.Invoke(mdel1, fltppr)
                                    End If

                                    isfut = True
                                    dt = date2
                                End If
                            End If
                            If pan31 = True Then 'if panel3 is visible
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                                    REM Change by Alpesh For Currency Problem 
                                    fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                    For Each fdrow As DataRow In currtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date3 & "'")
                                        fdrow("last") = fltppr 'update LTP to currtable
                                    Next
                                    For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("ftoken")) & " and fut_mdate='" & date3 & "'")
                                        grow("last") = fltppr 'update LTP to gcurrtable
                                    Next
                                    ' check which ltp is required future or equity
                                    If chkCalVol.Checked Or thrworking = False Then
                                        'calculate vol using equity is clicked then select LTP of asset_token
                                        'eltppr = Val(Curreltpprice(CLng(drow("asset_tokan"))))
                                        'txteqrate.Text = eltppr
                                    End If

                                    If date3 = CDate(drow("fut_mdate")).Date Then
                                        'select LTP of ftoken
                                        fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                        txtrate2.Invoke(mdel2, fltppr)
                                    End If
                                    isfut = True
                                    dt = date3
                                End If
                            End If
                            'if ltpprice contains tokenno or token1  then calculate greek values
                            If Currltpprice.Contains(CLng(drow("tokanno"))) Or Currltpprice.Contains(CLng(drow("token1"))) Then
                                Dim index As Integer = -1
                                'select index of position
                                For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' And Iscalc=True and tokanno=" & CLng(drow("tokanno")) & "")
                                    index = gcurrtable.Rows.IndexOf(grow)
                                Next
                                'if isliq is true then ltppr1 is LTP of token1
                                If CBool(drow("isliq")) = True Then
                                    ltppr = Val(Currltpprice(CLng(drow("tokanno"))))
                                    ltppr1 = Val(Currltpprice(CLng(drow("token1"))))
                                Else
                                    ltppr = Val(Currltpprice(CLng(drow("tokanno"))))
                                    ltppr1 = 0
                                End If
                                'set iscall flag
                                If drow("cp") = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                                mmt = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now.Date), CDate(drow("mdate")).Date)
                                If Now.Date = CDate(drow("mdate")).Date Then
                                    mt += 0.5
                                End If
                                If CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date = CDate(drow("mdate")).Date Then
                                    mmt += 0.5
                                End If

                                If chkCalVol.Checked = True Then 'if chkcalvol is clicked then LTP of asset_token is passed to function
                                    If eltppr <> 0 Then CalData(eltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname)
                                Else
                                    If fltppr <> 0 Then CalData(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname)
                                End If
                            End If
                            ' Next
                        End If
                    End If
                Next
                'If Me.InvokeRequired Then
                'Me.Invoke(munrealize)
                ' Me.Invoke(mval)
                'End If



            End If
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    ''' <summary>
    ''' cal_eqMt
    ''' </summary>
    ''' <remarks>This method call to update Equity Positoin For MainTable</remarks>
    Private Sub cal_eqMt(ByVal CmpName As String)
        Try
            'if equity pposition is exists
            If (eltpprice.Count > 0) Then
                ' If eltpprice.Contains(token) Then
                For Each drow As DataRow In maintable.Select("company='" & CmpName & "' And cp='E'")
                    'update LTP 
                    If thrworking = True Then
                        txteqrate.Invoke(meqdel, Val(eltpprice(CLng(drow("tokanno").ToString))))
                    End If
                    drow("last") = txteqrate.Text
                Next
            End If

            REM: if only equity position's then add future of currentmonth  to display futurevalue in txtrate
            Dim fdv As DataView
            'select future from contract of selected company
            fdv = New DataView(scripttable, "symbol='" & CmpName & "' and option_type not in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows)
            If fdv.ToTable.Rows.Count > 0 Then
                Try
                    txtrate.Invoke(mdel, fltpprice.Item(CLng(fdv(0)("token").ToString)))
                    REM  Calculation Of rate1,rate2 Alpesh
                    REM Next month Future Rate is  displayed in Far month tab 
                    txtrate1.Invoke(mdel1, fltpprice.Item(CLng(fdv(1)("token").ToString)))
                    txtrate2.Invoke(mdel2, fltpprice.Item(CLng(fdv(2)("token").ToString)))
                Catch ex As Exception
                End Try
            Else
                txtrate.Invoke(mdel, 0)
            End If
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub

    ''' <summary>
    ''' cal_eq
    ''' </summary>
    ''' <remarks>This method call to update Equity Positoin</remarks>
    Private Sub cal_eq()
        Try
            'if equity pposition is exists
            If (eltpprice.Count > 0) Then
                ' If eltpprice.Contains(token) Then

                REM: IsCalc Field For  Selected Rows Calculation By Viral 22nd June 11
                For Each drow As DataRow In currtable.Select("cp='E' And Iscalc=True")
                    'update LTP 
                    If thrworking = True Then
                        txteqrate.Invoke(meqdel, Val(eltpprice(CLng(drow("tokanno").ToString))))
                    End If
                    drow("last") = txteqrate.Text
                Next
                For Each grow As DataRow In gcurrtable.Select("cp='E' And Iscalc=True")
                    grow("last") = txteqrate.Text
                Next
            End If

            REM: if only equity position's then add future of currentmonth  to display futurevalue in txtrate
            Dim fdv As DataView
            'select future from contract of selected company

            fdv = New DataView(scripttable, "symbol='" & compname & "' and option_type not in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows)
            If fdv.ToTable.Rows.Count > 0 Then
                Try
                    txtrate.Invoke(mdel, fltpprice.Item(CLng(fdv(0)("token").ToString)))
                    REM  Calculation Of rate1,rate2 Alpesh
                    REM Next month Future Rate is  displayed in Far month tab 
                    txtrate1.Invoke(mdel1, fltpprice.Item(CLng(fdv(1)("token").ToString)))
                    txtrate2.Invoke(mdel2, fltpprice.Item(CLng(fdv(2)("token").ToString)))
                Catch ex As Exception
                End Try
            Else
                txtrate.Invoke(mdel, 0)
            End If
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub
    ''' <summary>
    ''' get_value
    ''' </summary>
    ''' <remarks>This method call to update bottom panel</remarks>
    Private Sub get_value()

        Try
            REM: IsCalc Field For  Selected Rows Calculation By Viral 22nd June 11
            Dim dtCurTlb As DataTable
            Dim Dv As DataView
            Dv = New DataView(currtable, "Iscalc = True", "company", DataViewRowState.CurrentRows)
            dtCurTlb = Dv.ToTable()
            dtCurTlb.Select("") ' For Speedup as Directed By Alpeshbhai(By Viral)
            REM End
            'calculate greek values
            If dtCurTlb.Rows.Count > 0 Then
                txttdelval.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", "").ToString), Deltastr_Val)
                txttthval.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", "").ToString), Thetastr_Val)
                txttvgval.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", "").ToString), Vegastr_Val)
                txttgmval.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", "").ToString), Gammastr_Val)
                txtshare.Text = Format(-Val(txttdelval.Text), NeutralizeStr)

                txttvolgaval.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", "").ToString), Volgastr_Val)
                txttvannaval.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", "").ToString), Vannastr_Val)

                txttdelta1.Text = Format(Val(dtCurTlb.Compute("sum(deltaval1)", "").ToString), Deltastr_Val)
                txtttheta1.Text = Format(Val(dtCurTlb.Compute("sum(thetaval1)", "").ToString), Thetastr_Val)
                txttvega1.Text = Format(Val(dtCurTlb.Compute("sum(vgval1)", "").ToString), Vegastr_Val)
                txttgamma1.Text = Format(Val(dtCurTlb.Compute("sum(gmval1)", "").ToString), Gammastr_Val)

                txttvolga1.Text = Format(Val(dtCurTlb.Compute("sum(volgaval1)", "").ToString), Volgastr_Val)
                txttvanna1.Text = Format(Val(dtCurTlb.Compute("sum(vannaval1)", "").ToString), Vannastr_Val)

                'for normal delta, wega, theta, gamma, volga, vanna


                'delta
                txtdelval_1.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Deltastr_Val)
                txtdelval_2.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Deltastr_Val)
                txtdelval_3.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Deltastr_Val)
                txtdelval_4.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Deltastr_Val)

                'gamma

                txtgmval_1.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Gammastr_Val)
                txtgmval_2.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Gammastr_Val)
                txtgmval_3.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Gammastr_Val)
                txtgmval_4.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Gammastr_Val)

                'theta
                txtthetaval_1.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Thetastr_Val)
                txtthetaval_2.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Thetastr_Val)
                txtthetaval_3.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Thetastr_Val)
                txtthetaval_4.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Thetastr_Val)

                'wega

                txtwegaval_1.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Vegastr_Val)
                txtwegaval_2.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Vegastr_Val)
                txtwegaval_3.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Vegastr_Val)
                txtwegaval_4.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Vegastr_Val)

                'shares
                txtshare1.Text = Format(-Val(txtdelval_1.Text), NeutralizeStr)
                txtshare2.Text = Format(-Val(txtdelval_2.Text), NeutralizeStr)
                txtshare3.Text = Format(-Val(txtdelval_3.Text), NeutralizeStr)
                txtshare4.Text = Format(-Val(txtdelval_4.Text), NeutralizeStr)

                'volga
                txtvolgaval_1.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Volgastr_Val)
                txtvolgaval_2.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Volgastr_Val)
                txtvolgaval_3.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Volgastr_Val)
                txtvolgaval_4.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Volgastr_Val)

                'vanna
                txtvannaval_1.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Vannastr_Val)
                txtvannaval_2.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Vannastr_Val)
                txtvannaval_3.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Vannastr_Val)
                txtvannaval_4.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Vannastr_Val)


                'for t-1 delta, wega, theta, gamma, volga, vanna
                'delta
                txttdelval_1.Text = Format(Val(dtCurTlb.Compute("sum(deltaval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Deltastr_Val)
                txttdelval_2.Text = Format(Val(dtCurTlb.Compute("sum(deltaval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Deltastr_Val)
                txttdelval_3.Text = Format(Val(dtCurTlb.Compute("sum(deltaval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Deltastr_Val)
                txttdelval_4.Text = Format(Val(dtCurTlb.Compute("sum(deltaval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Deltastr_Val)

                'gamma
                txttgmval_1.Text = Format(Val(dtCurTlb.Compute("sum(gmval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Gammastr_Val)
                txttgmval_2.Text = Format(Val(dtCurTlb.Compute("sum(gmval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Gammastr_Val)
                txttgmval_3.Text = Format(Val(dtCurTlb.Compute("sum(gmval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Gammastr_Val)
                txttgmval_4.Text = Format(Val(dtCurTlb.Compute("sum(gmval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Gammastr_Val)

                'theta
                txttthetaval_1.Text = Format(Val(dtCurTlb.Compute("sum(thetaval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Thetastr_Val)
                txttthetaval_2.Text = Format(Val(dtCurTlb.Compute("sum(thetaval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Thetastr_Val)
                txttthetaval_3.Text = Format(Val(dtCurTlb.Compute("sum(thetaval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Thetastr_Val)
                txttthetaval_4.Text = Format(Val(dtCurTlb.Compute("sum(thetaval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Thetastr_Val)

                'wega
                txttwegaval_1.Text = Format(Val(dtCurTlb.Compute("sum(vgval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Vegastr_Val)
                txttwegaval_2.Text = Format(Val(dtCurTlb.Compute("sum(vgval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Vegastr_Val)
                txttwegaval_3.Text = Format(Val(dtCurTlb.Compute("sum(vgval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Vegastr_Val)
                txttwegaval_4.Text = Format(Val(dtCurTlb.Compute("sum(vgval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Vegastr_Val)

                'Volga
                txttvolgaval_1.Text = Format(Val(dtCurTlb.Compute("sum(volgaval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Volgastr_Val)
                txttvolgaval_2.Text = Format(Val(dtCurTlb.Compute("sum(volgaval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Volgastr_Val)
                txttvolgaval_3.Text = Format(Val(dtCurTlb.Compute("sum(volgaval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Volgastr_Val)
                txttvolgaval_4.Text = Format(Val(dtCurTlb.Compute("sum(volgaval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Volgastr_Val)

                'Vanna
                txttvannaval_1.Text = Format(Val(dtCurTlb.Compute("sum(Vannaval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Vannastr_Val)
                txttvannaval_2.Text = Format(Val(dtCurTlb.Compute("sum(Vannaval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Vannastr_Val)
                txttvannaval_3.Text = Format(Val(dtCurTlb.Compute("sum(Vannaval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Vannastr_Val)
                txttvannaval_4.Text = Format(Val(dtCurTlb.Compute("sum(Vannaval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Vannastr_Val)


                'All call's D.G.V.T.Vo.Va.
                lblCallUnits.Text = Format(Val(dtCurTlb.Compute("sum(units)", "CP='C'").ToString), "0")
                lblCallDelta.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", "CP='C'").ToString), Deltastr_Val)
                lblCallGamma.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", "CP='C'").ToString), Gammastr_Val)
                lblCallVega.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", "CP='C'").ToString), Vegastr_Val)
                lblCallTheta.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", "CP='C'").ToString), Thetastr_Val)
                lblCallVolga.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", "CP='C'").ToString), Volgastr_Val)
                lblCallVanna.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", "CP='C'").ToString), Vannastr_Val)

                'All Put's D.G.V.T.Vo.Va.
                lblPutUnits.Text = Format(Val(dtCurTlb.Compute("sum(units)", "CP='P'").ToString), "0")
                lblPutDelta.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", "CP='P'").ToString), Deltastr_Val)
                lblPutGamma.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", "CP='P'").ToString), Gammastr_Val)
                lblPutvega.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", "CP='P'").ToString), Vegastr_Val)
                lblPutTheta.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", "CP='P'").ToString), Thetastr_Val)
                lblPutVolga.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", "CP='P'").ToString), Volgastr_Val)
                lblPutVanna.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", "CP='P'").ToString), Vannastr_Val)

                lblFutUnits.Text = Format(Val(dtCurTlb.Compute("sum(units)", "CP='F' or CP='X' or CP=''").ToString), "0")
                'Futur's Delta
                lblFutDelta.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", "CP='F' or CP='X' or CP=''").ToString), Deltastr_Val)

                lblEqUnits.Text = Format(Val(dtCurTlb.Compute("sum(units)", "CP='E'").ToString), "0")
                'Equity's delta
                lblEqDelta.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", "CP='E'").ToString), Deltastr_Val)

                'total of D.G.V.T
                lblTotUnits.Text = Format(Val(lblCallUnits.Text) + Val(lblPutUnits.Text) + Val(lblFutUnits.Text) + Val(lblEqUnits.Text), "0")
                lblTotDelta.Text = Format(Val(lblCallDelta.Text) + Val(lblPutDelta.Text) + Val(lblFutDelta.Text) + Val(lblEqDelta.Text), "#0.00")
                lblTotGamma.Text = Format(Val(lblCallGamma.Text) + Val(lblPutGamma.Text), "#0.00")
                lblTotVega.Text = Format(Val(lblCallVega.Text) + Val(lblPutvega.Text), "#0.00")
                lblTotTheta.Text = Format(Val(lblCallTheta.Text) + Val(lblPutTheta.Text), "#0.00")
                lblTotVolga.Text = Format(Val(lblCallVolga.Text) + Val(lblPutVolga.Text), "#0.00")
                lblTotVanna.Text = Format(Val(lblCallVanna.Text) + Val(lblPutVanna.Text), "#0.00")

                Call cal_grossmtm()    'comment for testing
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' get_value_Clear
    ''' </summary>
    ''' <remarks>This method call to update bottom panel</remarks>
    Private Sub get_value_Clear()
        Try
            'calculate greek values
            txttdelval.Text = Format(Val(currtable.Compute("sum(deltaval)", "").ToString), Deltastr_Val)
            txttthval.Text = Format(Val(currtable.Compute("sum(thetaval)", "").ToString), Thetastr_Val)
            txttvgval.Text = Format(Val(currtable.Compute("sum(vgval)", "").ToString), Vegastr_Val)
            txttgmval.Text = Format(Val(currtable.Compute("sum(gmval)", "").ToString), Gammastr_Val)
            txtshare.Text = Format(-Val(txttdelval.Text), NeutralizeStr)
            txttvolgaval.Text = Format(Val(currtable.Compute("sum(volgaval)", "").ToString), Volgastr_Val)
            txttvannaval.Text = Format(Val(currtable.Compute("sum(vannaval)", "").ToString), Vannastr_Val)

            txttdelta1.Text = Format(Val(currtable.Compute("sum(deltaval1)", "").ToString), Deltastr_Val)
            txtttheta1.Text = Format(Val(currtable.Compute("sum(thetaval1)", "").ToString), Thetastr_Val)
            txttvega1.Text = Format(Val(currtable.Compute("sum(vgval1)", "").ToString), Vegastr_Val)
            txttgamma1.Text = Format(Val(currtable.Compute("sum(gmval1)", "").ToString), Gammastr_Val)
            txttvolga1.Text = Format(Val(currtable.Compute("sum(volgaval1)", "").ToString), Volgastr_Val)
            txttvanna1.Text = Format(Val(currtable.Compute("sum(vannaval1)", "").ToString), Vannastr_Val)

            'for normal delta, wega, theta, gamma

            'delta
            txtdelval_1.Text = Format(Val(currtable.Compute("sum(deltaval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Deltastr_Val)
            txtdelval_2.Text = Format(Val(currtable.Compute("sum(deltaval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Deltastr_Val)
            txtdelval_3.Text = Format(Val(currtable.Compute("sum(deltaval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Deltastr_Val)
            txtdelval_4.Text = Format(Val(currtable.Compute("sum(deltaval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Deltastr_Val)
            
            'gamma

            txtgmval_1.Text = Format(Val(currtable.Compute("sum(gmval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Gammastr_Val)
            txtgmval_2.Text = Format(Val(currtable.Compute("sum(gmval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Gammastr_Val)
            txtgmval_3.Text = Format(Val(currtable.Compute("sum(gmval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Gammastr_Val)
            txtgmval_4.Text = Format(Val(currtable.Compute("sum(gmval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Gammastr_Val)

            'theta
            txtthetaval_1.Text = Format(Val(currtable.Compute("sum(thetaval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Thetastr_Val)
            txtthetaval_2.Text = Format(Val(currtable.Compute("sum(thetaval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Thetastr_Val)
            txtthetaval_3.Text = Format(Val(currtable.Compute("sum(thetaval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Thetastr_Val)
            txtthetaval_4.Text = Format(Val(currtable.Compute("sum(thetaval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Thetastr_Val)

            'wega
            txtwegaval_1.Text = Format(Val(currtable.Compute("sum(vgval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Vegastr_Val)
            txtwegaval_2.Text = Format(Val(currtable.Compute("sum(vgval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Vegastr_Val)
            txtwegaval_3.Text = Format(Val(currtable.Compute("sum(vgval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Vegastr_Val)
            txtwegaval_4.Text = Format(Val(currtable.Compute("sum(vgval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Vegastr_Val)

            'shares
            txtshare1.Text = Format(-Val(txtdelval_1.Text), NeutralizeStr)
            txtshare2.Text = Format(-Val(txtdelval_2.Text), NeutralizeStr)
            txtshare3.Text = Format(-Val(txtdelval_3.Text), NeutralizeStr)
            txtshare4.Text = Format(-Val(txtdelval_4.Text), NeutralizeStr)

            'Volga
            txtvolgaval_1.Text = Format(Val(currtable.Compute("sum(volgaval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Volgastr_Val)
            txtvolgaval_2.Text = Format(Val(currtable.Compute("sum(volgaval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Volgastr_Val)
            txtvolgaval_3.Text = Format(Val(currtable.Compute("sum(volgaval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Volgastr_Val)
            txtvolgaval_4.Text = Format(Val(currtable.Compute("sum(volgaval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Volgastr_Val)

            'Vanna
            txtvannaval_1.Text = Format(Val(currtable.Compute("sum(vannaval)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Vannastr_Val)
            txtvannaval_2.Text = Format(Val(currtable.Compute("sum(vannaval)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Vannastr_Val)
            txtvannaval_3.Text = Format(Val(currtable.Compute("sum(vannaval)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Vannastr_Val)
            txtvannaval_4.Text = Format(Val(currtable.Compute("sum(vannaval)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Vannastr_Val)

            'for t-1 delta, wega, theta, gamma
            'delta
            txttdelval_1.Text = Format(Val(currtable.Compute("sum(deltaval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Deltastr_Val)
            txttdelval_2.Text = Format(Val(currtable.Compute("sum(deltaval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Deltastr_Val)
            txttdelval_3.Text = Format(Val(currtable.Compute("sum(deltaval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Deltastr_Val)
            txttdelval_4.Text = Format(Val(currtable.Compute("sum(deltaval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Deltastr_Val)

            'gamma
            txttgmval_1.Text = Format(Val(currtable.Compute("sum(gmval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Gammastr_Val)
            txttgmval_2.Text = Format(Val(currtable.Compute("sum(gmval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Gammastr_Val)
            txttgmval_3.Text = Format(Val(currtable.Compute("sum(gmval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Gammastr_Val)
            txttgmval_4.Text = Format(Val(currtable.Compute("sum(gmval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Gammastr_Val)

            'theta
            txttthetaval_1.Text = Format(Val(currtable.Compute("sum(thetaval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Thetastr_Val)
            txttthetaval_2.Text = Format(Val(currtable.Compute("sum(thetaval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Thetastr_Val)
            txttthetaval_3.Text = Format(Val(currtable.Compute("sum(thetaval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Thetastr_Val)
            txttthetaval_4.Text = Format(Val(currtable.Compute("sum(thetaval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Thetastr_Val)

            'wega
            txttwegaval_1.Text = Format(Val(currtable.Compute("sum(vgval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Vegastr_Val)
            txttwegaval_2.Text = Format(Val(currtable.Compute("sum(vgval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Vegastr_Val)
            txttwegaval_3.Text = Format(Val(currtable.Compute("sum(vgval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Vegastr_Val)
            txttwegaval_4.Text = Format(Val(currtable.Compute("sum(vgval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Vegastr_Val)

            'volga
            txttvolgaval_1.Text = Format(Val(currtable.Compute("sum(volgaval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Volgastr_Val)
            txttvolgaval_2.Text = Format(Val(currtable.Compute("sum(volgaval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Volgastr_Val)
            txttvolgaval_3.Text = Format(Val(currtable.Compute("sum(volgaval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Volgastr_Val)
            txttvolgaval_4.Text = Format(Val(currtable.Compute("sum(volgaval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Volgastr_Val)

            'vanna
            txttvannaval_1.Text = Format(Val(currtable.Compute("sum(vannaval1)", "mdate_months=" & Maturity_first_month & " or cp='E'").ToString), Vannastr_Val)
            txttvannaval_2.Text = Format(Val(currtable.Compute("sum(vannaval1)", "mdate_months=" & Maturity_second_month & " or cp='E'").ToString), Vannastr_Val)
            txttvannaval_3.Text = Format(Val(currtable.Compute("sum(vannaval1)", "mdate_months=" & Maturity_third_month & " or cp='E'").ToString), Vannastr_Val)
            txttvannaval_4.Text = Format(Val(currtable.Compute("sum(vannaval1)", "mdate_months>" & Maturity_third_month & " or cp='E'").ToString), Vannastr_Val)



            'All call's D.G.V.T.
            lblCallUnits.Text = Format(Val(currtable.Compute("sum(units)", "CP='C'").ToString), "0")
            lblCallDelta.Text = Format(Val(currtable.Compute("sum(deltaval)", "CP='C'").ToString), Deltastr_Val)
            lblCallGamma.Text = Format(Val(currtable.Compute("sum(gmval)", "CP='C'").ToString), Gammastr_Val)
            lblCallVega.Text = Format(Val(currtable.Compute("sum(vgval)", "CP='C'").ToString), Vegastr_Val)
            lblCallTheta.Text = Format(Val(currtable.Compute("sum(thetaval)", "CP='C'").ToString), Thetastr_Val)
            lblCallVolga.Text = Format(Val(currtable.Compute("sum(volgaval)", "CP='C'").ToString), Volgastr_Val)
            lblCallVanna.Text = Format(Val(currtable.Compute("sum(vannaval)", "CP='C'").ToString), Vannastr_Val)

            'All Put's D.G.V.T.
            lblPutUnits.Text = Format(Val(currtable.Compute("sum(units)", "CP='P'").ToString), "0")
            lblPutDelta.Text = Format(Val(currtable.Compute("sum(deltaval)", "CP='P'").ToString), Deltastr_Val)
            lblPutGamma.Text = Format(Val(currtable.Compute("sum(gmval)", "CP='P'").ToString), Gammastr_Val)
            lblPutvega.Text = Format(Val(currtable.Compute("sum(vgval)", "CP='P'").ToString), Vegastr_Val)
            lblPutTheta.Text = Format(Val(currtable.Compute("sum(thetaval)", "CP='P'").ToString), Thetastr_Val)
            lblPutVolga.Text = Format(Val(currtable.Compute("sum(volgaval)", "CP='P'").ToString), Volgastr_Val)
            lblPutVanna.Text = Format(Val(currtable.Compute("sum(vannaval)", "CP='P'").ToString), Vannastr_Val)

            lblFutUnits.Text = Format(Val(currtable.Compute("sum(units)", "CP='F' or CP='X' or CP=''").ToString), "0")
            'Futur's Delta
            lblFutDelta.Text = Format(Val(currtable.Compute("sum(deltaval)", "CP='F' or CP='X' or CP=''").ToString), Deltastr_Val)

            lblEqUnits.Text = Format(Val(currtable.Compute("sum(units)", "CP='E'").ToString), "0")
            'Equity's delta
            lblEqDelta.Text = Format(Val(currtable.Compute("sum(deltaval)", "CP='E'").ToString), Deltastr_Val)

            'total of D.G.V.T
            lblTotUnits.Text = Format(Val(lblCallUnits.Text) + Val(lblPutUnits.Text) + Val(lblFutDelta.Text) + Val(lblEqUnits.Text), "0")
            lblTotDelta.Text = Format(Val(lblCallDelta.Text) + Val(lblPutDelta.Text) + Val(lblFutDelta.Text) + Val(lblEqDelta.Text), "#0.00")
            lblTotGamma.Text = Format(Val(lblCallGamma.Text) + Val(lblPutGamma.Text), "#0.00")
            lblTotVega.Text = Format(Val(lblCallVega.Text) + Val(lblPutvega.Text), "#0.00")
            lblTotTheta.Text = Format(Val(lblCallTheta.Text) + Val(lblPutTheta.Text), "#0.00")
            lblTotVolga.Text = Format(Val(lblCallVolga.Text) + Val(lblPutVolga.Text), "#0.00")
            lblTotVanna.Text = Format(Val(lblCallVanna.Text) + Val(lblPutVanna.Text), "#0.00")

            'cal_grossmtm()    'comment for testing
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub


    ''' <summary>
    ''' cal_exp_Margin
    ''' </summary>
    ''' <remarks>This method call to refresh Exposure Margin Textbox</remarks>
    Private Sub cal_exp_Margin()
        'calulcate grossMTM and expense
        DouExpMrg = 0
        DouIntMrg = 0
        'DouEquity = 0
        'txtexpmrg.Text = 0
        'txtintmrg.Text = 0
        txtEquity.Text = 0
        'Exposure Magin ##############################################
        If mTbl_SPAN_output.Rows.Count > 0 Then
            For Each drow As DataRow In mTbl_SPAN_output.Select("ClientCode='" & compname & "'")
                DouExpMrg = Format(DouExpMrg + Val(drow("exposure_margin").ToString), exmargstr)
                DouIntMrg = Format(DouIntMrg + Val(Val(drow("spanreq").ToString) - Val(drow("anov").ToString)), inmargstr)

                'txtexpmrg.Text = Format(Val(txtexpmrg.Text) + Val(drow("exposure_margin").ToString), exmargstr)
                'txtintmrg.Text = Format(Val(txtintmrg.Text) + Val(Val(drow("spanreq").ToString) - Val(drow("anov").ToString)), inmargstr)
            Next
        End If


        'equity ###########################################################
        Dim equity As Double = 0
        For Each drow As DataRow In currtable.Select("cp='E'")
            equity = equity + Val(Val(drow("units")) * Val(drow("traded")))
        Next

        'txtequity.Text = Math.Round(equity, Roundequity)
        'DouEquity = Format(equity, equitystr)
        'txttotmarg.Text = Math.Round((DouIntMrg + DouExpMrg + DouEquity) / 100000, 2)

        txtEquity.Text = Format(equity, equitystr)
        'txttotmarg.Text = Math.Round((Val(txtintmrg.Text) + Val(txtexpmrg.Text) + Val(txtEquity.Text)) / 100000, 2)
        txttotmarg.Text = Math.Round((DouIntMrg + DouExpMrg + Val(txtEquity.Text)) / 100000, 2)

    End Sub

    ''' <summary>
    ''' cal_grossmtm
    ''' </summary>
    ''' <remarks>This method call to calculate Gross MtoM, Net MtoM and Sq.Of Expense</remarks>
    Private Sub cal_grossmtm()
        REM Calculation Of Previous GROSSMToM and Today GrossMtom

        Dim prgrossmtm As Double = 0
        Dim togrossmtm As Double = 0
        Dim totgross As Double = 0
        Dim VarTodayGrossMTM As Double = 0

        REM: IsCalc Field For  Selected Rows Calculation By Viral 22nd June 11

        'Dim Dv As DataView
        'Dv = New DataView(currtable, "Iscalc = True", "company", DataViewRowState.CurrentRows)
        'dtCurTlb = Dv.ToTable()
        REM End
        prgrossmtm = 0
        togrossmtm = 0
        totgross = 0

        'For Each mrow As DataRow In gcurrtable.Rows
        '    '     mrow("totExp") = -(Val(mrow("prExp").ToString) + Val(mrow("toExp").ToString))
        '    '   calculate grossmtm positionwise
        '    If Val(mrow("units")) <> 0 Then
        '        mrow("grossmtm") = Val(mrow("units")) * (Val(mrow("last")) - Val(mrow("traded")))
        '    Else
        '        mrow("grossmtm") = -Val(mrow("traded").ToString)
        '    End If
        '    mrow("netmtm") = Math.Round(Val(mrow("grossmtm").ToString) + Val(mrow("totExp").ToString), 0)
        '    'caluclate today grossmtm
        '    'we can't get entry date of each trade so need to refer trading table 
        '    For Each drow1 As DataRow In GdtFOTrades.Select("script='" & mrow("script") & "' AND entry_date=#" & Now.Date & "#")
        '        togrossmtm += Val(drow1("qty")) * (Val(mrow("last")) - Val(drow1("rate")))
        '        ' Debug.WriteLine((val(drow1("qty")) * (val(drow("last")) - val(drow1("rate")))))
        '    Next
        '    For Each drow1 As DataRow In GdtEQTrades.Select("script='" & mrow("script") & "' and entry_date=#" & Now.Date & "#")
        '        togrossmtm += Val(drow1("qty")) * (Val(mrow("last")) - Val(drow1("rate")))
        '    Next
        '    For Each drow1 As DataRow In GdtCurrencyTrades.Select("script='" & mrow("script") & "' and entry_date=#" & Now.Date & "#")
        '        togrossmtm += Val(drow1("qty")) * (Val(mrow("last")) - Val(drow1("rate")))
        '    Next

        'Next
        'prgrossmtm = gcurrtable.Compute("sum(grossmtm)", "")

        ''divyesh
        ''currently using curratable changes 
        For Each mrow As DataRow In currtable.Select("Iscalc = True")
            '     mrow("totExp") = -(Val(mrow("prExp").ToString) + Val(mrow("toExp").ToString))
            '   calculate grossmtm positionwise
            If Val(mrow("units")) <> 0 Then
                mrow("grossmtm") = Val(mrow("units")) * (Val(mrow("last")) - Val(mrow("traded")))
            Else
                mrow("grossmtm") = -Val(mrow("traded").ToString)
            End If
            mrow("netmtm") = Math.Round(Val(mrow("grossmtm").ToString) + Val(mrow("totExp").ToString), 0)

            'caluclate today grossmtm
            'we can't get entry date of each trade so need to refer trading table 

            'From 50051 
            Dim Obj_Struct_Pos As Struct_Analysis_Position = CType(ht_Ana_Position(CLng(mrow("tokanno"))), Struct_Analysis_Position)

            Dim VarTodayqty As Integer = Obj_Struct_Pos.VarTodayUnits
            Dim VarTodayrate As Double = Obj_Struct_Pos.VarTodayValue
            If VarTodayqty <> 0 Then
                VarTodayrate = VarTodayrate / VarTodayqty
                'VarTodayGrossMtoM = VarTodayNewQty * (VarLTP - VarTodayNewPrice)
                VarTodayGrossMTM = VarTodayqty * (Val(mrow("last")) - VarTodayrate)
            Else
                'VarTodayGrossMTM = 0
                VarTodayGrossMTM = -Val(VarTodayrate)
            End If

            togrossmtm += VarTodayGrossMTM

            'Old
            'For Each drow1 As DataRow In GdtFOTrades.Select("script='" & mrow("script") & "' AND (entrydate >= #" & Format(Today, "dd-MMM-yyyy") & "# AND entrydate < #" & Format(Today.AddDays(1), "dd-MMM-yyyy") & "#)")
            '    togrossmtm += Val(drow1("qty")) * (Val(mrow("last")) - Val(drow1("rate")))
            '    ' Debug.WriteLine((val(drow1("qty")) * (val(drow("last")) - val(drow1("rate")))))
            'Next
            'For Each drow1 As DataRow In GdtEQTrades.Select("script='" & mrow("script") & "' and (entrydate>=#" & Format(Today, "dd-MMM-yyyy") & "# AND entrydate<#" & Format(Today.AddDays(1), "dd-MMM-yyyy") & "#)")
            '    togrossmtm += Val(drow1("qty")) * (Val(mrow("last")) - Val(drow1("rate")))
            'Next
            'For Each drow1 As DataRow In GdtCurrencyTrades.Select("script='" & mrow("script") & "' and (entrydate>=#" & Format(Today, "dd-MMM-yyyy") & "# AND entrydate<#" & Format(Today.AddDays(1), "dd-MMM-yyyy") & "#)")
            '    togrossmtm += Val(drow1("qty")) * (Val(mrow("last")) - Val(drow1("rate")))
            'Next

        Next
        prgrossmtm = currtable.Compute("sum(grossmtm)", "Iscalc = True")

        'For Each drow As DataRow In currtable.Rows
        '    For Each drow1 As DataRow In dtFOTrades.Select("script='" & drow("script") & "'  and entry_date=#" & Now.Date & "# ")
        '        togrossmtm += Val(drow1("qty")) * (Val(drow("last")) - Val(drow1("rate")))
        '        ' Debug.WriteLine((val(drow1("qty")) * (val(drow("last")) - val(drow1("rate")))))
        '    Next
        '    For Each drow1 As DataRow In dtEQTrades.Select("script='" & drow("script") & "' and entry_date=#" & Now.Date & "# ")
        '        togrossmtm += Val(drow1("qty")) * (Val(drow("last")) - Val(drow1("rate")))
        '    Next
        '    If Val(drow("units")) <> 0 Then
        '        prgrossmtm += Val(drow("units")) * (Val(drow("last")) - Val(drow("traded")))
        '    Else
        '        prgrossmtm += (-Val(drow("traded")))
        '    End If
        'Next
        txttGmtm.Text = Format(togrossmtm, GrossMTMstr)
        txttotGmtm.Text = Format(CFgprofit + prgrossmtm, GrossMTMstr)
        txtprGmtm.Text = Format((CFgprofit + prgrossmtm) - togrossmtm, GrossMTMstr)

        cal_net_mtm()
        cal_sqmtm()

    End Sub


    Private Sub cal_grossmtmMt(ByVal CmpName As String)
        REM Calculation Of Previous GROSSMToM and Today GrossMtom
        Dim prgrossmtm As Double = 0
        Dim togrossmtm As Double = 0
        Dim totgross As Double = 0
        Dim VarTodayGrossMTM As Double = 0

        For Each mrow As DataRow In maintable.Select("company ='" & CmpName & "'")
            '   calculate grossmtm positionwise
            If Val(mrow("units")) <> 0 Then
                mrow("grossmtm") = Val(mrow("units")) * (Val(mrow("last")) - Val(mrow("traded")))
                mrow("curGrossMTM") = Val(mrow("units")) * (Val(mrow("last")) - Val(mrow("traded")))
            Else
                mrow("curGrossMTM") = -Val(mrow("traded").ToString)
            End If
            mrow("netmtm") = Math.Round(Val(mrow("grossmtm").ToString) + Val(mrow("totExp").ToString), 0)

            'caluclate today grossmtm
            'we can't get entry date of each trade so need to refer trading table 

            'From 50051 
            Dim Obj_Struct_Pos As Struct_Analysis_Position = CType(ht_Ana_Position(CLng(mrow("tokanno"))), Struct_Analysis_Position)

            Dim VarTodayqty As Integer = Obj_Struct_Pos.VarTodayUnits
            Dim VarTodayrate As Double = Obj_Struct_Pos.VarTodayValue
            If VarTodayqty <> 0 Then
                VarTodayrate = VarTodayrate / VarTodayqty
                'VarTodayGrossMtoM = VarTodayNewQty * (VarLTP - VarTodayNewPrice)
                VarTodayGrossMTM = VarTodayqty * (Val(mrow("last")) - VarTodayrate)
            Else
                'VarTodayGrossMTM = 0
                VarTodayGrossMTM = -Val(VarTodayrate)
            End If

            mrow("curTotalMTM") = VarTodayGrossMTM
        Next


        'txttGmtm.Text = Format(togrossmtm, GrossMTMstr)
        'txttotGmtm.Text = Format(prgrossmtm, GrossMTMstr)
        'txtprGmtm.Text = Format(prgrossmtm - togrossmtm, GrossMTMstr)


    End Sub

    ''' <summary>
    ''' cal_net_mtm
    ''' </summary>
    ''' <remarks>This method call to refresh Net MtoM lebel</remarks>
    Private Sub cal_net_mtm()
        txtprnetmtm.Text = Format(Val(txtprGmtm.Text) + Val(txtprexp.Text), NetMTMstr)
        txttnetmtm.Text = Format(Val(txttGmtm.Text) + Val(txttexp.Text), NetMTMstr)
        'CFnprofit +
        txttotnetmtm.Text = Format(Val(txttotGmtm.Text) + Val(txttotexp.Text), NetMTMstr)
    End Sub
    ''' <summary>
    ''' cal_sqmtm
    ''' </summary>
    ''' <remarks>This method call to calculate SQ. of MtoM</remarks>
    Private Sub cal_sqmtm()
        Dim prexp As Double = 0
        Dim texp As Double = 0
        prexp = 0
        texp = 0

        REM: IsCalc Field For  Selected Rows Calculation By Viral 22nd June 11    
        Dim dtCurTlb As DataTable
        Dim Dv As DataView
        Dv = New DataView(currtable, "Iscalc = True", "company", DataViewRowState.CurrentRows)
        dtCurTlb = Dv.ToTable()
        REM End
        For Each drow As DataRow In dtCurTlb.Select("units <> 0")
            If drow("cp") = "E" Then
                If Format(drow("entrydate"), "MMM/dd/yyyy") = Today.Date Then
                    If Val(drow("units")) > 0 Then
                        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * ndbs) / ndbsp)
                    Else
                        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * ndbl) / ndblp)
                    End If
                Else
                    If Val(drow("units")) > 0 Then
                        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * dbs) / dbsp)
                    Else
                        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * dbl) / dblp)
                    End If
                End If

                'tp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entrydate = #" & Format(Today, "dd-MMM-yyyy") & "# and qty > 0").ToString) * Val(drow("last")) * ndbs / ndbsp)
                'tp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entrydate = #" & Format(Today, "dd-MMM-yyyy") & "# and qty < 0").ToString) * Val(drow("last")) * ndbl / ndblp)
                'tp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entrydate <> #" & Format(Today, "dd-MMM-yyyy") & "# and qty > 0").ToString) * Val(drow("last")) * dbs / dbsp)
                'tp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entrydate <> #" & Format(Today, "dd-MMM-yyyy") & "# and qty < 0").ToString) * Val(drow("last")) * dbl / dblp)

            ElseIf drow("cp") = "F" Then
                If drow("IsCurrency") = True Then
                    If Val(drow("units")) > 0 Then
                        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currfuts) / currfutsp)
                    Else
                        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currfutl) / currfutlp)
                    End If
                Else
                'prexp = prexp + val(((val(Math.Abs(drow("prqty"))) * val(drow("last"))) * exptable.Compute("max(futs)", "")) / exptable.Compute("max(futsp)", ""))
                If Val(drow("units")) > 0 Then
                    texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * futs) / futsp)
                Else
                    texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * futl) / futlp)
                End If
                End If
            Else
                If drow("IsCurrency") = True Then
                    If Val(exptable.Compute("max(currspl)", "")) <> 0 Then
                        'If val(drow("prqty")) > 0 Then
                        '    prexp = prexp + val(((val(drow("prqty")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", ""))
                        'Else
                        '    prexp = prexp + Math.Abs(val(((val(drow("prqty")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", "")))
                        'End If
                        If Val(drow("units")) > 0 Then
                            'texp = texp + val(((val(drow("units")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", ""))
                            texp = texp + (Val((Math.Abs(Val(drow("units"))) * (Val(drow("strikes")) + Val(drow("last")))) * exptable.Compute("max(currsps)", "")) / exptable.Compute("max(currspsp)", ""))
                        Else
                            'texp = texp + val(Math.Abs(((val(drow("units")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", "")))
                            texp = texp + (Val((Math.Abs(Val(drow("units"))) * (Val(drow("strikes")) + Val(drow("last")))) * exptable.Compute("max(currspl)", "")) / exptable.Compute("max(currsplp)", ""))

                        End If

                    Else
                        'If val(drow("prqty")) > 0 Then
                        '    prexp = prexp + val(((val(drow("prqty")) * (val(drow("last")))) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", ""))
                        'Else
                        '    prexp = prexp + Math.Abs(val(((val(drow("prqty")) * (val(drow("last")))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", "")))
                        ''End If
                        If Val(drow("units")) > 0 Then
                            'texp = texp + val(((val(drow("units")) * (val(drow("last")))) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", ""))
                            texp = texp + (Val((Math.Abs(Val(drow("units"))) * Val(drow("last"))) * exptable.Compute("max(currpres)", "")) / exptable.Compute("max(currpresp)", ""))
                        Else
                            ' texp = texp + val(Math.Abs(((val(drow("units")) * (val(drow("last")))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", "")))
                            texp = texp + (Val((Math.Abs(Val(drow("units"))) * Val(drow("last"))) * exptable.Compute("max(currprel)", "")) / exptable.Compute("max(currprelp)", ""))
                        End If
                        'texp = texp + (val((Math.Abs(val(drow("units"))) * val(drow("last"))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", ""))

                    End If
                Else
                If Val(exptable.Compute("max(spl)", "")) <> 0 Then
                    'If val(drow("prqty")) > 0 Then
                    '    prexp = prexp + val(((val(drow("prqty")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", ""))
                    'Else
                    '    prexp = prexp + Math.Abs(val(((val(drow("prqty")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", "")))
                    'End If
                    If Val(drow("units")) > 0 Then
                        'texp = texp + val(((val(drow("units")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", ""))
                        texp = texp + (Val((Math.Abs(Val(drow("units"))) * (Val(drow("strikes")) + Val(drow("last")))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", ""))
                    Else
                        'texp = texp + val(Math.Abs(((val(drow("units")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", "")))
                        texp = texp + (Val((Math.Abs(Val(drow("units"))) * (Val(drow("strikes")) + Val(drow("last")))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", ""))

                    End If

                Else
                    'If val(drow("prqty")) > 0 Then
                    '    prexp = prexp + val(((val(drow("prqty")) * (val(drow("last")))) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", ""))
                    'Else
                    '    prexp = prexp + Math.Abs(val(((val(drow("prqty")) * (val(drow("last")))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", "")))
                    ''End If
                    If Val(drow("units")) > 0 Then
                        'texp = texp + val(((val(drow("units")) * (val(drow("last")))) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", ""))
                        texp = texp + (Val((Math.Abs(Val(drow("units"))) * Val(drow("last"))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", ""))
                    Else
                        ' texp = texp + val(Math.Abs(((val(drow("units")) * (val(drow("last")))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", "")))
                        texp = texp + (Val((Math.Abs(Val(drow("units"))) * Val(drow("last"))) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", ""))
                    End If
                    'texp = texp + (val((Math.Abs(val(drow("units"))) * val(drow("last"))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", ""))


                End If
            End If
                
            End If
        Next
        txttotsqexp.Text = -Format(texp, Expensestr)        'txtprsqmtm.Text = Math.Round(val(txtprnetmtm.Text) - prexp, RoundSquareMTM)
        txtprsqmtm.Text = Format(Val(txtprnetmtm.Text), SquareMTMstr)
        txttosqmtm.Text = Format(Val(txttnetmtm.Text), SquareMTMstr)
        txttotsqmtm.Text = Format(Val(txtprsqmtm.Text) + Val(txttosqmtm.Text) - texp, SquareMTMstr)
    End Sub

    ''''''''' <summary>
    ''''''''' cal_sqmtm
    ''''''''' </summary>
    ''''''''' <remarks>This method call to calculate SQ. of MtoM</remarks>
    '' '' ''Private Sub cal_sqmtm()
    '' '' ''    Dim prexp As Double = 0
    '' '' ''    Dim texp As Double = 0
    '' '' ''    prexp = 0
    '' '' ''    texp = 0
    '' '' ''    For Each drow As DataRow In currtable.Select("units <> 0")
    '' '' ''        If drow("cp") = "E" Then
    '' '' ''            'If Format(drow("entrydate"), "MMM/dd/yyyy") = Today.Date Then
    '' '' ''            '    If Val(drow("units")) > 0 Then
    '' '' ''            '        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * ndbs) / ndbsp)
    '' '' ''            '    Else
    '' '' ''            '        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * ndbl) / ndblp)
    '' '' ''            '    End If
    '' '' ''            'Else
    '' '' ''            '    If Val(drow("units")) > 0 Then
    '' '' ''            '        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * dbs) / dbsp)
    '' '' ''            '    Else
    '' '' ''            '        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * dbl) / dblp)
    '' '' ''            '    End If
    '' '' ''            'End If

    '' '' ''            texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entrydate = #" & Format(Today, "dd-MMM-yyyy") & "# and qty > 0").ToString) * Val(drow("last")) * ndbs / ndbsp)
    '' '' ''            texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entrydate = #" & Format(Today, "dd-MMM-yyyy") & "# and qty < 0").ToString) * Val(drow("last")) * ndbl / ndblp)
    '' '' ''            texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entrydate <> #" & Format(Today, "dd-MMM-yyyy") & "# and qty > 0").ToString) * Val(drow("last")) * dbs / dbsp)
    '' '' ''            texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entrydate <> #" & Format(Today, "dd-MMM-yyyy") & "# and qty < 0").ToString) * Val(drow("last")) * dbl / dblp)

    '' '' ''        ElseIf drow("cp") = "F" Then
    '' '' ''            If drow("Iscurrency") = True Then
    '' '' ''                texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currfuts) / currfutsp)
    '' '' ''                'If Val(drow("units")) > 0 Then
    '' '' ''                '    texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currfuts) / currfutsp)
    '' '' ''                'Else
    '' '' ''                '    texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currfuts) / currfutsp)
    '' '' ''                'End If
    '' '' ''                'Else
    '' '' ''                '    If Val(currspl) <> 0 Then
    '' '' ''                '        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currsps) / currspsp)
    '' '' ''                '    Else
    '' '' ''                '        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currpres) / currpresp)
    '' '' ''                '    End If
    '' '' ''                'End If
    '' '' ''        Else
    '' '' ''            'prexp = prexp + val(((val(Math.Abs(drow("prqty"))) * val(drow("last"))) * exptable.Compute("max(futs)", "")) / exptable.Compute("max(futsp)", ""))
    '' '' ''            If Val(drow("units")) > 0 Then
    '' '' ''                texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * futs) / futsp)
    '' '' ''            Else
    '' '' ''                texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * futl) / futlp)
    '' '' ''            End If
    '' '' ''        End If

    '' '' ''        Else
    '' '' ''            'If drow("Iscurrency") = True Then
    '' '' ''            '    If Val(currspl) <> 0 Then
    '' '' ''            '        texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currsps) / currspsp)
    '' '' ''            '    Else
    '' '' ''            '        If Val(drow("units")) > 0 Then
    '' '' ''            '            texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currpres) / currpresp)
    '' '' ''            '        Else
    '' '' ''            '            texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currprel) / currpresp)
    '' '' ''            '        End If
    '' '' ''            '        'texp = texp + Val(((Val(Math.Abs(drow("units"))) * Val(drow("last"))) * currprel) / currpresp)
    '' '' ''            '    End If
    '' '' ''            'Else
    '' '' ''            If Val(exptable.Compute("max(spl)", "")) <> 0 Then
    '' '' ''                'If val(drow("prqty")) > 0 Then
    '' '' ''                '    prexp = prexp + val(((val(drow("prqty")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", ""))
    '' '' ''                'Else
    '' '' ''                '    prexp = prexp + Math.Abs(val(((val(drow("prqty")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", "")))
    '' '' ''                'End If
    '' '' ''                If Val(drow("units")) > 0 Then
    '' '' ''                    'texp = texp + val(((val(drow("units")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", ""))
    '' '' ''                    texp = texp + (Val((Math.Abs(Val(drow("units"))) * (Val(drow("strikes")) + Val(drow("last")))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", ""))
    '' '' ''                Else
    '' '' ''                    'texp = texp + val(Math.Abs(((val(drow("units")) * (val(drow("strikes")) + val(drow("last")))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", "")))
    '' '' ''                    texp = texp + (Val((Math.Abs(Val(drow("units"))) * (Val(drow("strikes")) + Val(drow("last")))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", ""))

    '' '' ''                End If

    '' '' ''            Else
    '' '' ''                'If val(drow("prqty")) > 0 Then
    '' '' ''                '    prexp = prexp + val(((val(drow("prqty")) * (val(drow("last")))) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", ""))
    '' '' ''                'Else
    '' '' ''                '    prexp = prexp + Math.Abs(val(((val(drow("prqty")) * (val(drow("last")))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", "")))
    '' '' ''                ''End If
    '' '' ''                If Val(drow("units")) > 0 Then
    '' '' ''                    'texp = texp + val(((val(drow("units")) * (val(drow("last")))) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", ""))
    '' '' ''                    texp = texp + (Val((Math.Abs(Val(drow("units"))) * Val(drow("last"))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", ""))
    '' '' ''                Else
    '' '' ''                    ' texp = texp + val(Math.Abs(((val(drow("units")) * (val(drow("last")))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", "")))
    '' '' ''                    texp = texp + (Val((Math.Abs(Val(drow("units"))) * Val(drow("last"))) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", ""))
    '' '' ''                End If
    '' '' ''                'texp = texp + (val((Math.Abs(val(drow("units"))) * val(drow("last"))) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", ""))
    '' '' ''            End If
    '' '' ''            'End If
    '' '' ''        End If
    '' '' ''    Next
    '' '' ''    txttotsqexp.Text = -Format(texp, Expensestr)        'txtprsqmtm.Text = Math.Round(val(txtprnetmtm.Text) - prexp, RoundSquareMTM)
    '' '' ''    txtprsqmtm.Text = Format(Val(txtprnetmtm.Text), SquareMTMstr)
    '' '' ''    txttosqmtm.Text = Format(Val(txttnetmtm.Text), SquareMTMstr)
    '' '' ''    txttotsqmtm.Text = Format(Val(txtprsqmtm.Text) + Val(txttosqmtm.Text) - texp, SquareMTMstr)
    '' '' ''End Sub

    'Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
    '    Me.Close()
    'End Sub
#End Region
#Region "Grid Event"
    ''' <summary>
    ''' tbcomp_DrawItem
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to Redraw Tab item of tab control</remarks>
    Private Sub tbcomp_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tbcomp.DrawItem
        'use to draw tabs with backgroung = black
        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = tbcomp.TabPages(e.Index)
        Dim br As System.Drawing.Brush
        Dim sf As New StringFormat
        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width + 2, e.Bounds.Height - 2)
        sf.Alignment = StringAlignment.Near
        Dim strTitle As String = tp.Text
        If tbcomp.SelectedIndex = e.Index Then
            Dim f As Font = New Font(tbcomp.Font.Name, tbcomp.Font.Size, FontStyle.Regular, tbcomp.Font.Unit)
            br = New SolidBrush(Color.Black)
            g.FillRectangle(br, e.Bounds)

            br = New SolidBrush(Color.White)
            g.DrawString(strTitle, f, br, r, sf)


        Else
            Dim f As Font = New Font(tbcomp.Font.Name, tbcomp.Font.Size, FontStyle.Regular, tbcomp.Font.Unit)
            br = New SolidBrush(Color.WhiteSmoke)
            g.FillRectangle(br, e.Bounds)

            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, f, br, r, sf)

        End If
        tp.Refresh()
        'Call TabStrategy_DrawItem(sender, e)
    End Sub

    ''' <summary>
    ''' TabStrategy_DrawItem
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to Redraw Tab item of tab control</remarks>
    Private Sub TabStrategy_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles TabStrategy.DrawItem
        'use to draw tabs with backgroung = black
        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = TabStrategy.TabPages(e.Index)
        Dim br As System.Drawing.Brush
        Dim sf As New StringFormat
        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width + 2, e.Bounds.Height - 2)
        sf.Alignment = StringAlignment.Near
        Dim strTitle As String = tp.Text
        If TabStrategy.SelectedIndex = e.Index Then
            Dim f As Font = New Font(TabStrategy.Font.Name, TabStrategy.Font.Size, FontStyle.Regular, TabStrategy.Font.Unit)
            br = New SolidBrush(Color.Black)
            'br = New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Teal, Color.Black, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal)
            g.FillRectangle(br, e.Bounds)

            br = New SolidBrush(Color.White)
            g.DrawString(strTitle, f, br, r, sf)


        Else
            Dim f As Font = New Font(TabStrategy.Font.Name, TabStrategy.Font.Size, FontStyle.Regular, TabStrategy.Font.Unit)
            br = New SolidBrush(Color.WhiteSmoke)
            g.FillRectangle(br, e.Bounds)

            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, f, br, r, sf)

        End If
        tp.Refresh()
    End Sub



    ''' <summary>
    ''' grdtrad_CellDoubleClick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to duble click on Datagrid to display Display Trading Form</remarks>
    Private Sub grdtrad_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGTrading.CellDoubleClick
        If (e.ColumnIndex <> DGTrading.Columns("liq").Index Or e.ColumnIndex <> DGTrading.Columns("Remarks").Index) And e.RowIndex >= 0 Then ' if not isliq and remarks columns then open
            Call f1_grddoubl()
        End If
    End Sub
    ''' <summary>
    ''' f1_grddoubl
    ''' </summary>
    ''' <remarks>This method call to open Display Trading form</remarks>
    Private Sub f1_grddoubl()
        Try
            If currtable.Rows.Count > 0 Then       'if trades exists then
                Dim mStart As String
                mStart = cmdStart.Text
                Dim display As New displaytrans
                Dim token As Long
                Dim script As String
                script = ""
                Dim CP As String = ""
                'If currtable.Rows.Count > 0 Then
                token = CLng(DGTrading.CurrentRow.Cells("TokanNo").Value)
                'select current clicked postion's token no and assign its values to dispTrans' variables
                'only one position of each token displayed in analysis so for loop is not required
                For Each drow As DataRow In currtable.Select("tokanno=" & token & "")
                    display.token = CLng(drow("tokanno"))
                    script = CStr(drow("script"))
                    display.script = CStr(drow("script"))
                    display.cpfe = CStr(CStr(drow("cp")))
                    display.company = CStr(drow("company"))
                    If CStr(CStr(drow("cp"))) <> "E" Then
                        display.token1 = CLng(drow("token1"))
                        display.instrumentname = scripttable.Compute("max(instrumentname)", "script='" & CStr(drow("script")) & "'").ToString
                        display.mdate = CDate(drow("mdate"))
                        display.isliq = CBool(drow("isliq"))
                        display.strikerate = Val(drow("strikes"))
                        display.obj = Me
                        display.VarScriptType = "FO"
                    Else
                        display.instrumentname = eqsecurity.Compute("max(series)", "script='" & CStr(drow("script")) & "'").ToString
                        display.obj = Me
                        display.VarScriptType = "EQ"
                    End If
                Next
                If VarIsCurrency = True Then
                    display.VarScriptType = "CURRENCY"
                End If
                'stop calculation timer
                Timer_Calculation.Stop()
                display.ShowDialog()

                'if any insertion or deletion in trades then refill trading tables

                If chkpro = True Then
                    Me.Cursor = Cursors.WaitCursor
                    System.Threading.Thread.Sleep(30)

                    REM 1: comment by keval(20-05) bocoz in displayTrans never new company added

                    'If tbcomp.TabPages.Count <= 0 Then
                    '    Dim i As Integer
                    '    i = 0
                    '    For Each drow As DataRow In objTrad.Comapany.Rows
                    '        tbcomp.TabPages.Add(drow("company"))
                    '        tbcomp.TabPages.Item(i).Name = drow("company")
                    '        If UCase(drow("company")) = "NIFTY" Then
                    '            chknifty = True
                    '            ind = i
                    '        End If
                    '        i += 1
                    '    Next
                    'ElseIf objTrad.Comapany.Rows.Count > tbcomp.TabCount Then
                    '    tbcomp.TabPages.Clear()
                    '    Dim i As Integer
                    '    i = 0
                    '    For Each drow As DataRow In objTrad.Comapany.Rows
                    '        tbcomp.TabPages.Add(drow("company"))
                    '        tbcomp.TabPages.Item(i).Name = drow("company")
                    '        If UCase(drow("company")) = "NIFTY" Then
                    '            chknifty = True
                    '            ind = i
                    '        End If
                    '        i += 1
                    '    Next
                    'End If

                    'recalculate and refill maintable,currtable and gcurrtable
                    REM JIGNESH
                    'Call fill_equity_dtable()

                    If tbcomp.TabPages.Count > 0 Then 'if any company's postion exists
                        'if selected company's trades are exists
                        If maintable.Compute("count(company)", "company='" & compname & "'") > 0 Then
                            'if trades are  there , select that company
                            Call Fill_StrategyTab_MonthWise()
                            change_tab(compname, tbmo)
                        Else 'if selected company's all trades are deleted, delete that company's tab
                            tbcomp.TabPages.RemoveByKey(compname)
                            compname = ""
                            'select first company from list
                            If tbcomp.TabPages.Count > 0 Then
                                tbcomp.SelectedIndex = 0
                                compname = tbcomp.SelectedTab.Name.ToString
                                Call Fill_StrategyTab_MonthWise()
                            End If
                            change_tab(compname, tbmo)
                        End If
                    End If

                    Me.Cursor = Cursors.Default
                    chkpro = False
                End If
                If gcurrtable.Rows.Count = 0 Then
                    Call Fill_StrategyTab_MonthWise()
                    Call get_value_Clear()
                End If
                Timer_Calculation.Start()

                'SetStartStop(mStart)
            End If
            '  End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' DGTrading_CellEndEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to update Illiq and Remark into Maintable</remarks>
    Private Sub DGTrading_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGTrading.CellEndEdit
        'only in offline mode change in isliq then change its token1
        ' if call's script then pass put's token to token1 and vice versa
        On Error Resume Next
        If e.ColumnIndex = DGTrading.Columns("liq").Index And thrworking = False And (DGTrading.Rows(e.RowIndex).Cells("CP").Value = "C" Or DGTrading.Rows(e.RowIndex).Cells("CP").Value = "P") Then
            Dim token1, token As Long
            Dim script, script1 As String
            Dim a, a1 As String
            'Dim tk As Long
            script = DGTrading.Rows(e.RowIndex).Cells("script").Value
            token1 = CLng(DGTrading.Rows(e.RowIndex).Cells("token1").Value)
            token = CLng(DGTrading.Rows(e.RowIndex).Cells("tokanno").Value)
            If token1 = 0 Then
                a = Mid(script, Len(script) - 1, 1)
                a1 = Mid(script, Len(script), 1)
                If a = "C" Then 'selected script's type is call
                    script1 = Mid(script, 1, Len(script) - 2) & "  P" & a1  'create put script
                Else ' selected script type is put
                    script1 = Mid(script, 1, Len(script) - 2) & "  C" & a1 'create call script

                End If
                '  MsgBox(script1)

                token1 = CLng(IIf(IsDBNull(scripttable.Compute("max(token)", "script='" & script1 & "'")), 0, scripttable.Compute("max(token)", "script='" & script1 & "'")))
                DGTrading.Rows(e.RowIndex).Cells("token1").Value = token1
                DGTrading.EndEdit()
            End If
            'update isliq flag value to database
            Dim chk As Boolean
            chk = CBool(DGTrading.Rows(e.RowIndex).Cells("liq").Value)
            'objTrad.Update_Liq(script, token1, CBool(DGTrading.Rows(e.RowIndex).Cells("liq").Value))
            objAna.Update_isLiq(script, token1, CBool(DGTrading.Rows(e.RowIndex).Cells("liq").Value), compname)

            'Dim buffer_temp(4) As Byte
            'Dim buffer(5) As Byte

            'add token1 to cparray and change isliq status
            If CBool(DGTrading.Rows(e.RowIndex).Cells("liq").Value) = True Then
                If cparray.Contains(token) Then
                    '  cparray.Remove(token)
                    cparray.Add(token1)
                    If Not ltpprice.ContainsKey(token1) Then
                        ltpprice.Add(ltpprice, 0)
                    End If
                    For Each drow As DataRow In maintable.Select("script='" & script & "' and tokanno=" & token & " And company='" & compname & "'")
                        drow("isliq") = True
                    Next
                    currtable.Rows(e.RowIndex)("isliq") = True
                End If
            Else
                If cparray.Contains(token1) Then
                    For Each drow As DataRow In maintable.Select("script='" & script & "' and tokanno=" & token & " And company='" & compname & "'")
                        drow("isliq") = False
                    Next
                    currtable.Rows(e.RowIndex)("isliq") = False
                End If
            End If
        ElseIf e.ColumnIndex = DGTrading.Columns("remarks").Index And thrworking = False Then 'save remarks column
            Dim script As String
            script = DGTrading.Rows(e.RowIndex).Cells("script").Value
            For Each drow As DataRow In maintable.Select("script='" & script & "' And company='" & compname & "'")
                drow("remarks") = DGTrading.Rows(e.RowIndex).Cells("remarks").Value.ToString
            Next
            currtable.Rows(e.RowIndex)("remarks") = DGTrading.Rows(e.RowIndex).Cells("remarks").Value.ToString
            objAna.Update_Remarks(script, DGTrading.Rows(e.RowIndex).Cells("remarks").Value.ToString, compname)

        ElseIf e.ColumnIndex = DGTrading.Columns("IsCalc").Index And thrworking = False Then 'save IsCalc column
            REM: IsCalc Field For  Selected Rows Calculation By Viral 22nd June 11
            Dim script As String
            script = DGTrading.Rows(e.RowIndex).Cells("script").Value
            For Each drow As DataRow In maintable.Select("script='" & script & "' And company='" & compname & "'")
                drow("IsCalc") = CBool(DGTrading.Rows(e.RowIndex).Cells("Iscalc").Value)
            Next
            currtable.Rows(e.RowIndex)("IsCalc") = CBool(DGTrading.Rows(e.RowIndex).Cells("IsCalc").Value)
            objAna.Update_IsCalc(script, CBool(DGTrading.Rows(e.RowIndex).Cells("IsCalc").Value), compname)
		ElseIf e.ColumnIndex = DGTrading.Columns("lv").Index Then 'save remarks column
            'If DGTrading.Rows(e.RowIndex).Cells("cp").Value <> "F" Then 'And DGTrading.Rows(e.RowIndex).Cells("cp").Value <> "E" Then
            If DGTrading.Rows(e.RowIndex).Cells("lv").Value > 0 Then
                DGTrading.Rows(e.RowIndex).Cells("IsVolFix").Value = True
            Else
                DGTrading.Rows(e.RowIndex).Cells("IsVolFix").Value = False
            End If
            Dim script As String
            script = DGTrading.Rows(e.RowIndex).Cells("script").Value
            For Each drow As DataRow In maintable.Select("script='" & script & "' And company='" & compname & "'")
                drow("IsVolFix") = DGTrading.Rows(e.RowIndex).Cells("IsVolFix").Value.ToString
                drow("lv") = DGTrading.Rows(e.RowIndex).Cells("lv").Value.ToString
            Next
            currtable.Rows(e.RowIndex)("IsVolFix") = DGTrading.Rows(e.RowIndex).Cells("IsVolFix").Value.ToString
            objAna.Update_IsVolFix(script, CBool(DGTrading.Rows(e.RowIndex).Cells("IsVolFix").Value), compname)
            'Else
            '    DGTrading.CancelEdit()
            'End If
        End If

    End Sub
    Private Sub grdtrad_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGTrading.CellFormatting
        Dim VarColor As Color
        'Dim BaKColor As Color
        If IIf(IsDBNull(DGTrading.Rows(e.RowIndex).Cells("IsVolFix").Value), False, DGTrading.Rows(e.RowIndex).Cells("IsVolFix").Value) = True Then
            'BaKColor = Color.Tan
            DGTrading.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Indigo
        Else
            'BaKColor = Color.Black
            DGTrading.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Color.Black
        End If
        If DGTrading.Rows(e.RowIndex).Cells("CP").Value = "C" Then
            VarColor = Color.Yellow
        ElseIf DGTrading.Rows(e.RowIndex).Cells("CP").Value = "P" Then
            VarColor = Color.LimeGreen
        ElseIf DGTrading.Rows(e.RowIndex).Cells("CP").Value = "F" Then
            VarColor = Color.Orange
        ElseIf DGTrading.Rows(e.RowIndex).Cells("CP").Value = "E" Then
            VarColor = Color.LightPink
        End If
        DGTrading.Rows(e.RowIndex).Cells("Maturity").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("Strikes").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("CP").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("traded").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("last").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("lv").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("Delta").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("DeltaVal").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("DeltaN").Style.ForeColor = VarColor

        DGTrading.Rows(e.RowIndex).Cells("Gamma").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("Gmval").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("GammaN").Style.ForeColor = VarColor

        DGTrading.Rows(e.RowIndex).Cells("Vega").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("VgVal").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("VegaN").Style.ForeColor = VarColor

        DGTrading.Rows(e.RowIndex).Cells("Theta").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("ThetaVal").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("ThetaN").Style.ForeColor = VarColor

        DGTrading.Rows(e.RowIndex).Cells("Volga").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("VolgaVal").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("VolgaN").Style.ForeColor = VarColor

        DGTrading.Rows(e.RowIndex).Cells("Vanna").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("VannaVal").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("VannaN").Style.ForeColor = VarColor

        DGTrading.Rows(e.RowIndex).Cells("grossmtm").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("totExp").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("netmtm").Style.ForeColor = VarColor
        DGTrading.Rows(e.RowIndex).Cells("remarks").Style.ForeColor = VarColor

        If Val(DGTrading.Rows(e.RowIndex).Cells("units").Value) < 0 Then
            DGTrading.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.Red
            DGTrading.Rows(e.RowIndex).Cells("Lots").Style.ForeColor = Color.Red
        ElseIf Val(DGTrading.Rows(e.RowIndex).Cells("units").Value) > 0 Then
            DGTrading.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.SkyBlue
            DGTrading.Rows(e.RowIndex).Cells("Lots").Style.ForeColor = Color.SkyBlue
        Else
            DGTrading.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.White
            DGTrading.Rows(e.RowIndex).Cells("Lots").Style.ForeColor = Color.White
        End If

    End Sub
    ''' <summary>
    ''' SetGrid_Rounding
    ''' </summary>
    ''' <remarks>This method call to display rounding decimal datagrid</remarks>
    Private Sub SetGrid_Rounding()
        If VarIsCurrency = True Then
            DGTrading.Columns("Strikes").DefaultCellStyle.Format = CurrencyStrikesStr
            DGTrading.Columns("Traded").DefaultCellStyle.Format = CurrencyNetPriceStr
            DGTrading.Columns("Last").DefaultCellStyle.Format = CurrencyLTPStr
            DGTrading.Columns("totExp").DefaultCellStyle.Format = CurrencyExpenseStr
        Else
            DGTrading.Columns("Strikes").DefaultCellStyle.Format = ""
            DGTrading.Columns("Traded").DefaultCellStyle.Format = "#0.00"
            DGTrading.Columns("Last").DefaultCellStyle.Format = "#0.00"
            DGTrading.Columns("totExp").DefaultCellStyle.Format = Expensestr
        End If
        DGTrading.Columns("Delta").DefaultCellStyle.Format = Deltastr
        DGTrading.Columns("DeltaVal").DefaultCellStyle.Format = Deltastr_Val
        DGTrading.Columns("DeltaN").DefaultCellStyle.Format = NeutralizeStr

        DGTrading.Columns("Gamma").DefaultCellStyle.Format = Gammastr
        DGTrading.Columns("GmVal").DefaultCellStyle.Format = Gammastr_Val
        DGTrading.Columns("GammaN").DefaultCellStyle.Format = NeutralizeStr

        DGTrading.Columns("Vega").DefaultCellStyle.Format = Vegastr
        DGTrading.Columns("vgVal").DefaultCellStyle.Format = Vegastr_Val
        DGTrading.Columns("VegaN").DefaultCellStyle.Format = NeutralizeStr

        DGTrading.Columns("Theta").DefaultCellStyle.Format = Thetastr
        DGTrading.Columns("ThetaVal").DefaultCellStyle.Format = Thetastr_Val
        DGTrading.Columns("ThetaN").DefaultCellStyle.Format = NeutralizeStr

        DGTrading.Columns("Volga").DefaultCellStyle.Format = Volgastr
        DGTrading.Columns("VolgaVal").DefaultCellStyle.Format = Volgastr_Val
        DGTrading.Columns("VolgaN").DefaultCellStyle.Format = NeutralizeStr

        DGTrading.Columns("Vanna").DefaultCellStyle.Format = Vannastr
        DGTrading.Columns("VannaVal").DefaultCellStyle.Format = Vannastr_Val
        DGTrading.Columns("VannaN").DefaultCellStyle.Format = NeutralizeStr

    End Sub
    ''' <summary>
    ''' grdtrad_KeyDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call Key Stroke fire on Datagrid</remarks>
    Private Sub grdtrad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGTrading.KeyDown
        If e.KeyCode = Keys.F1 Then
            f1_grddoubl() 'open dispTrades form
        ElseIf e.KeyCode = Keys.PageDown Then
            If (tbcomp.SelectedIndex <= tbcomp.TabPages.Count) Then
                tbcomp.SelectedIndex = tbcomp.SelectedIndex + 1
            End If

        ElseIf e.KeyCode = Keys.PageUp Then
            If (tbcomp.SelectedIndex >= 1 And tbcomp.TabPages.Count > 0) Then
                tbcomp.SelectedIndex = tbcomp.SelectedIndex - 1
            End If
        End If

    End Sub
#End Region

#Region "Text Eevent"
    Private Sub txttdelval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttdelval.TextChanged
        If Val(txttdelval.Text) > 0 Then
            txttdelval.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttdelval.Text) < 0 Then
            txttdelval.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttdelval.BackColor = Color.Black
        End If

    End Sub
    Private Sub txttgmval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttgmval.TextChanged
        If Val(txttgmval.Text) > 0 Then
            txttgmval.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttgmval.Text) < 0 Then
            txttgmval.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttgmval.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttvgval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttvgval.TextChanged
        If Val(txttvgval.Text) > 0 Then
            txttvgval.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttvgval.Text) < 0 Then
            txttvgval.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttvgval.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttthval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttthval.TextChanged
        If Val(txttthval.Text) > 0 Then
            txttthval.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttthval.Text) < 0 Then
            txttthval.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttthval.BackColor = Color.Black
        End If
    End Sub
    Private Sub txtshare_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtshare.TextChanged
        If Val(txtshare.Text) > 0 Then
            txtshare.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txtshare.Text) < 0 Then
            txtshare.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txtshare.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttdelta1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttdelta1.TextChanged
        If Val(txttdelta1.Text) > 0 Then
            txttdelta1.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttdelta1.Text) < 0 Then
            txttdelta1.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttdelta1.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttgamma1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttgamma1.TextChanged
        If Val(txttgamma1.Text) > 0 Then
            txttgamma1.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttgamma1.Text) < 0 Then
            txttgamma1.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttgamma1.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttvega1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttvega1.TextChanged
        If Val(txttvega1.Text) > 0 Then
            txttvega1.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttvega1.Text) < 0 Then
            txttvega1.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttvega1.BackColor = Color.Black
        End If
    End Sub
    Private Sub txtttheta1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtttheta1.TextChanged
        If Val(txtttheta1.Text) > 0 Then
            txtttheta1.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txtttheta1.Text) < 0 Then
            txtttheta1.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txtttheta1.BackColor = Color.Black
        End If
    End Sub

    Private Sub txtnoofday_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtnoofday.KeyDown
        If e.KeyCode = Keys.Enter Then
            Call UpdateAutomatic(False)
        End If
    End Sub
    Private Sub txtnoofday_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtnoofday.KeyPress
        numonly(e)
    End Sub
    Private Sub txtnoofday_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'lblt1.Text = "T-" & txtnoofday.Text
    End Sub
    Private Sub txtprGmtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprGmtm.TextChanged
        If Val(txtprGmtm.Text) > 0 Then
            txtprGmtm.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txtprGmtm.Text) < 0 Then
            txtprGmtm.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txtprGmtm.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttGmtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttGmtm.TextChanged
        If Val(txttGmtm.Text) > 0 Then
            txttGmtm.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttGmtm.Text) < 0 Then
            txttGmtm.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttGmtm.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttotGmtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttotGmtm.TextChanged
        If Val(txttotGmtm.Text) > 0 Then
            txttotGmtm.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttotGmtm.Text) < 0 Then
            txttotGmtm.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttotGmtm.BackColor = Color.Black
        End If
    End Sub
    ''' <summary>
    ''' Timer_Calculation_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to Timer fire to refresh datagrid, Top Panel and button panel</remarks>
    Private Sub Timer_Calculation_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Calculation.Tick
        If DGTrading.Rows.Count <= 0 Then Exit Sub
        noPackage = 0
        If (refreshstarted = False And thrworking = True) Or thrworking_future_only = True Then
            Call cal_eq()
            Call cal_future()
            Call get_AtmCalc()

            'Try
            '    dtBCopy.Rows.Clear()
            '    dtBCopy.AcceptChanges()
            '    dtBCopy.Merge(dtfoBCopy.Copy)
            '    dtBCopy.AcceptChanges()
            '    dtBCopy.Merge(dtcmBCopy.Copy)
            '    dtBCopy.AcceptChanges()
            '    dtBCopy.Merge(dtcurBCopy.Copy)
            '    dtBCopy.AcceptChanges()
            '    dgBcopy.DataSource = dtBCopy.Copy
            'Catch ex As Exception

            'End Try

            'MDI.Text = MDI.VarMDITitle & "  [" & Format(DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")), "dd-MMM-yyyy hh:mm ss") & " FO:" & dtfoBCopy.Rows.Count & bIsfoBcopyComplete.ToString & " EQ:" & dtcmBCopy.Rows.Count & bIscmBcopyComplete.ToString & " Cur:" & dtcurBCopy.Rows.Count & bIscurBcopyComplete.ToString & "|" & dtfoBCopy.Rows.Count + dtcmBCopy.Rows.Count + dtcurBCopy.Rows.Count & "|] "

            If bIsfoBcopyComplete = True And bIscmBcopyComplete = True And bIscurBcopyComplete = True Then
                BtnSaveBhavCopy_Click()
            End If

            Me.Invoke(mval)

            If flgSummary = True Then
                VarDeltaval = Val(txttdelval.Text)
                VarGammaval = Val(txttgmval.Text)
                VarThetaval = Val(txttthval.Text)
                VarVegaval = Val(txttvgval.Text)
                VarVolgaval = Val(txttvolgaval.Text)
                VarVannaval = Val(txttvannaval.Text)
                VarGrossmtm = Val(txttotGmtm.Text)
                VarDeltaRS = Val(txtrate.Text)

                If flgcalsummarythrdstart = False Then
                    Timer_Calculation.Stop()
                    'txtVarexpense = txttotexp.Text
                    'txtVarcurrent = txttotnetmtm.Text
                    'txtVarProjectMTM = txttotsqmtm.Text
                    'Thr_CalculateDatatable.Abort()
                    flgcalsummarythrdstart = True
                    Thr_CalculateDatatable = New Thread(AddressOf Cal_DeltaGammaVegaThetaSummary)
                    'Thr_CalculateDatatable = New Thread(AddressOf cal_comp)
                    Thr_CalculateDatatable.Start()
                    Timer_Calculation.Start()
                End If
            End If
            'lblcount.Text = CInt(lblcount.Text) + 1
        End If
        obj_DelSetcalcMarg(douexpmrg, douintmrg)
    End Sub
    Private Sub txtprexp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprexp.TextChanged
        If Val(txtprexp.Text) > 0 Then
            txtprexp.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txtprexp.Text) < 0 Then
            txtprexp.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txtprexp.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttexp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttexp.TextChanged
        If Val(txttexp.Text) > 0 Then
            txttexp.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttexp.Text) < 0 Then
            txttexp.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttexp.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttotexp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttotexp.TextChanged
        If Val(txttotexp.Text) > 0 Then
            txttotexp.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttotexp.Text) < 0 Then
            txttotexp.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttotexp.BackColor = Color.Black
        End If
    End Sub
    Private Sub txtprnetmtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprnetmtm.TextChanged
        If Val(txtprnetmtm.Text) > 0 Then
            txtprnetmtm.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txtprnetmtm.Text) < 0 Then
            txtprnetmtm.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txtprnetmtm.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttnetmtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttnetmtm.TextChanged
        If Val(txttnetmtm.Text) > 0 Then
            txttnetmtm.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttnetmtm.Text) < 0 Then
            txttnetmtm.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttnetmtm.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttotnetmtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttotnetmtm.TextChanged
        If Val(txttotnetmtm.Text) > 0 Then
            txttotnetmtm.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttotnetmtm.Text) < 0 Then
            txttotnetmtm.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttotnetmtm.BackColor = Color.Black
        End If
    End Sub
    Private Sub txtprsqmtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprsqmtm.TextChanged
        If Val(txtprsqmtm.Text) > 0 Then
            txtprsqmtm.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txtprsqmtm.Text) < 0 Then
            txtprsqmtm.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txtprsqmtm.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttosqmtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttosqmtm.TextChanged
        If Val(txttosqmtm.Text) > 0 Then
            txttosqmtm.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttosqmtm.Text) < 0 Then
            txttosqmtm.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttosqmtm.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttotsqmtm_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttotsqmtm.TextChanged
        If Val(txttotsqmtm.Text) > 0 Then
            txttotsqmtm.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttotsqmtm.Text) < 0 Then
            txttotsqmtm.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttotsqmtm.BackColor = Color.Black
        End If
    End Sub


    Private Sub deltaAlert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles deltaAlert.Click
        If Not alertcont Is Nothing Then
            'If alertcont.Text <> "0" Then
            Select Case alertcont.Name
                Case "txttdelval"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Delta"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()
                    cmdalert.Text = "Stop Alert"
                Case "txttgmval"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Gamma"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()
                    cmdalert.Text = "Stop Alert"
                Case "txttvgval"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Vega"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()
                    cmdalert.Text = "Stop Alert"
                Case "txttthval"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Theta"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()

                    cmdalert.Text = "Stop Alert"
                    REM "Set Alert" right click option is  working for Volga/Vanna
                Case "txttvolgaval"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Volga"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()

                    cmdalert.Text = "Stop Alert"
                Case "txttvannaval"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Vanna"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()

                    cmdalert.Text = "Stop Alert"
                Case "txttdelta1"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Delta"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()
                    cmdalert.Text = "Stop Alert"
                Case "txttgamma1"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Gamma"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()
                    cmdalert.Text = "Stop Alert"
                Case "txttvega1"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Vega"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()
                    cmdalert.Text = "Stop Alert"
                Case "txtttheta1"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Theta"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()
                    cmdalert.Text = "Stop Alert"
                Case "txttvolga1"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Volga"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()
                    cmdalert.Text = "Stop Alert"
                Case "txttvanna1"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "Vanna"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()
                    cmdalert.Text = "Stop Alert"
                Case "txttotGmtm"
                    timer1.Stop()
                    cmdalert.Text = "Start Alert"

                    Dim alert As New alert_entry
                    alert.compname = compname
                    alert.fieldname = "GrossMTM"
                    alert.val1 = Val(alertcont.Text)
                    alert.ShowDialog()
                    alert_comp()
                    timer1.Start()

                    cmdalert.Text = "Stop Alert"
            End Select
            'End If
        End If
    End Sub
    Private Sub txttdelval_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttdelval.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttdelval
        End If
    End Sub
    Private Sub txttgmval_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttgmval.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttgmval
        End If
    End Sub
    Private Sub txttvgval_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttvgval.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttvgval
        End If
    End Sub
    Private Sub txttthval_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttthval.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttthval
        End If
    End Sub
    REM  "Set Alert" right click option is  working for Volga/Vanna
    Private Sub txttvolgaval_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttvolgaval.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttvolgaval
        End If
    End Sub
    Private Sub txttvannaval_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttvannaval.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttvannaval
        End If
    End Sub
    Private Sub txttdelta1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttdelta1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttdelta1
        End If
    End Sub
    Private Sub txttgamma1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttgamma1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttgamma1
        End If
    End Sub
    Private Sub txttvega1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttvega1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttvega1
        End If
    End Sub
    Private Sub txtttheta1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txtttheta1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txtttheta1
        End If
    End Sub
    Private Sub txttvolga1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttvolga1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttvolga1
        End If
    End Sub
    Private Sub txttvanna1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttvanna1.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttvanna1
        End If
    End Sub
    Private Sub txttotGmtm_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles txttotGmtm.MouseDown
        If e.Button = Windows.Forms.MouseButtons.Right Then
            alertcont = txttotGmtm
        End If
    End Sub


    'Private Sub cmdscen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdscen.Click
    ' 'MDI.ToolStripMenuSearchComp.Visible = False
    ''MDI.ToolStripcompanyCombo.Visible = False
    '    Call scenario()
    'End Sub

    Private Sub txtcvol_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcvol.GotFocus, txtcvol1.GotFocus, txtcvol2.GotFocus, txtpvol.GotFocus, txtpvol1.GotFocus, txtpvol2.GotFocus, txtfut1.GotFocus, txtfut2.GotFocus, txtfut3.GotFocus, txteqrate.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub

    Private Sub txtcvol_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcvol.Validated
        If txtcvol.Text.Trim = "" Then
            txtcvol.Text = 0
        End If
    End Sub
    Private Sub txtpvol_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpvol.Validated
        If txtpvol.Text.Trim = "" Then
            txtpvol.Text = 0
        End If
    End Sub

    Private Sub txtfut1_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfut1.Validated
        If Val(txtfut1.Text.Trim) = 0 Then
            txtfut1.Text = 0
        End If
    End Sub
    Private Sub txtfut2_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfut2.Validated
        If txtfut2.Text.Trim = "" Then
            txtfut2.Text = 0
        End If
    End Sub
    Private Sub txtfut3_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfut3.Validated
        If txtfut3.Text.Trim = "" Then
            txtfut3.Text = 0
        End If
    End Sub
    Private Sub txtprsqexp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtprsqexp.TextChanged
        If Val(txtprsqexp.Text) > 0 Then
            txtprsqexp.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txtprsqexp.Text) < 0 Then
            txtprsqexp.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txtprsqexp.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttosqexp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttosqexp.TextChanged
        If Val(txttosqexp.Text) > 0 Then
            txttosqexp.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttosqexp.Text) < 0 Then
            txttosqexp.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttosqexp.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttotsqexp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txttotsqexp.TextChanged
        If Val(txttotsqexp.Text) > 0 Then
            txttotsqexp.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttotsqexp.Text) < 0 Then
            txttotsqexp.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttotsqexp.BackColor = Color.Black
        End If
    End Sub





    Private Sub cmdsave_Click()
        If thrworking = False Then

            Save_applied = True

            ''If GdtCompanyAnalysis.Rows.Count > 0 Then
            ''    txtcvol.Text = GdtCompanyAnalysis.Rows(0)(2)
            ''    txtpvol.Text = GdtCompanyAnalysis.Rows(0)(3)
            ''    txtcvol1.Text = GdtCompanyAnalysis.Rows(0)(4)
            ''    txtpvol1.Text = GdtCompanyAnalysis.Rows(0)(5)
            ''    txtcvol2.Text = GdtCompanyAnalysis.Rows(0)(6)
            ''    txtpvol2.Text = GdtCompanyAnalysis.Rows(0)(7)
            ''    txtfut1.Text = GdtCompanyAnalysis.Rows(0)(8)
            ''    txtfut2.Text = GdtCompanyAnalysis.Rows(0)(9)
            ''    txtfut3.Text = GdtCompanyAnalysis.Rows(0)(10)
            ''End If
            comp_ana = GdtCompanyAnalysis 'objTrad.select_company_ana
            Dim exp1 As Date
            Dim exp2 As Date
            Dim exp3 As Date
            If pan1.Visible = True Then
                exp1 = dtexp.Value.Date
            Else
                exp1 = "01/01/1905"
            End If
            If pan2.Visible = True Then
                exp2 = dtexp1.Value.Date
            Else
                exp2 = "01/01/1905"
            End If
            If pan3.Visible = True Then
                exp3 = dtexp2.Value.Date
            Else
                exp3 = "01/01/1905"
            End If

            If comp_ana.Compute("count(company)", "company='" & compname & "'") <= 0 Then
                objTrad.Insert_company_ana(compname, Val(txtcvol.Text), Val(txtpvol.Text), Val(txtcvol1.Text), Val(txtpvol1.Text), Val(txtcvol2.Text), Val(txtpvol2.Text), Val(txtfut1.Text), Val(txtfut2.Text), Val(txtfut3.Text), Now.Date, exp1, exp2, exp3, txteqrate.Text, chkCalVol.Checked)
            Else
                objTrad.Update_company_ana(compname, Val(txtcvol.Text), Val(txtpvol.Text), Val(txtcvol1.Text), Val(txtpvol1.Text), Val(txtcvol2.Text), Val(txtpvol2.Text), Val(txtfut1.Text), Val(txtfut2.Text), Val(txtfut3.Text), Now.Date, exp1, exp2, exp3, txteqrate.Text, chkCalVol.Checked)
            End If
            GdtCompanyAnalysis = objTrad.select_company_ana()

            If (Val(txtcvol.Text) <> 0 And Val(txtpvol.Text) <> 0) Or (Val(txtcvol1.Text) <> 0 And Val(txtpvol1.Text) <> 0) Or (Val(txtcvol2.Text) <> 0 And Val(txtpvol2.Text) <> 0) Then
                save_apply()
                If DGTrading.Rows.Count > 0 Then
                    cal_exp_Margin()
                    get_value()
                End If

            End If

        End If
    End Sub


#End Region
#Region "Margin"
    Private Sub init_span_tables()
        With mTbl_exposure_comp
            .Columns.Add("CompName", GetType(String))
            .Columns.Add("mat_month", GetType(Integer))
            .Columns.Add("p", GetType(Double))
            .Columns.Add("fut_opt", GetType(String))
        End With
        With mTbl_SPAN_output
            .Columns.Add("ClientCode", GetType(String)) ' company name
            .Columns.Add("lfv", GetType(Double))
            .Columns.Add("sfv", GetType(Double))
            .Columns.Add("lov", GetType(Double))
            .Columns.Add("sov", GetType(Double))
            .Columns.Add("spanreq", GetType(Double)) ' for initial margin  spanreq-anvo
            .Columns.Add("anov", GetType(Double))
            .Columns.Add("exposure_margin", GetType(Double))
        End With
        With mTbl_span_calc
            .Columns.Add("description", GetType(String))
            .Columns.Add("compname", GetType(String))
            .Columns.Add("exp_date", GetType(String))
            .Columns.Add("cal_put_fut", GetType(String))
            .Columns.Add("strike_price", GetType(String))
        End With
    End Sub
    'Private Function generate_SPAN_data(ByVal dtable As DataTable) As Boolean
    '    Try
    'Dim client_code As String
    'Dim temp_comp_name As String
    'Dim comp_name As String
    'Dim option_type As String 'OOP or FUT
    'Dim mat_date As String 'yyyymmdd
    'Dim CAL_PUT As String 'C or P
    'Dim strike_price As String
    'Dim qty As String

    'Dim ht_comp As New Hashtable
    'Dim drow_position As DataRow



    ''create file stream
    'Dim fs_spn As New FileStream(mSPAN_path & "\span.spn", FileMode.Create)
    'Dim fs_Curspn As New FileStream(mSPAN_path & "\curspan.spn", FileMode.Create)
    'Dim fs_input As New FileStream(mSPAN_path & "\input.txt", FileMode.Create)
    'Dim fs_curinput As New FileStream(mSPAN_path & "\curinput.txt", FileMode.Create)
    'Dim fs_batchfile As New FileStream(mSPAN_path & "\generate.bat", FileMode.Create)
    'Dim fs_curbatchfile As New FileStream(mSPAN_path & "\curgenerate.bat", FileMode.Create)

    'Dim sw As StreamWriter
    '        sw = New StreamWriter(fs_spn)
    'Dim cursw As StreamWriter
    '        cursw = New StreamWriter(fs_Curspn)

    ''CType(Me.MdiParent, mdiMain).StatusBar1.Panels(2).Text = ""
    '        If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
    '            MsgBox("Enter Correct Path for span in setting.")
    '            Exit Function
    '        End If
    ''get latest span file
    '        mCurrent_SPAN_file = get_latest_spn_file(mSPAN_path, "FO")
    '        mCurrent_CurSPAN_file = get_latest_spn_file(mSPAN_path, "CUR")
    '        get_expected_latest_spn_file(mCurrent_SPAN_file, "FO")
    '        get_expected_latest_spn_file(mCurrent_SPAN_file, "CUR")

    ''CType(Me.MdiParent, mdiMain).StatusBar1.Panels(2).Text = mCurrent_SPAN_file

    '        sw.WriteLine("LOAD " & mSPAN_path & "\" & mCurrent_SPAN_file)
    '        sw.WriteLine("Load " & mSPAN_path & "\input.txt" & ",USEXTLAYOUT")
    '        sw.WriteLine("Calc")
    '        sw.WriteLine("Save " & mSPAN_path & "\output.xml")
    '        sw.Close()
    '        fs_spn.Close()

    '        cursw.WriteLine("LOAD " & mSPAN_path & "\" & mCurrent_CurSPAN_file)
    '        cursw.WriteLine("Load " & mSPAN_path & "\curinput.txt" & ",USEXTLAYOUT")
    '        cursw.WriteLine("Calc")
    '        cursw.WriteLine("Save " & mSPAN_path & "\curoutput.xml")
    '        cursw.Close()
    '        fs_Curspn.Close()

    '        sw = New StreamWriter(fs_batchfile)
    '        sw.WriteLine(mSPAN_path & "\spanit " & mSPAN_path & "\span.spn")
    '        sw.Close()
    '        fs_batchfile.Close()

    '        sw = New StreamWriter(fs_curbatchfile)
    '        sw.WriteLine(mSPAN_path & "\spanit " & mSPAN_path & "\curspan.spn")
    '        sw.Close()
    '        fs_curbatchfile.Close()

    '' Dim temp As Integer



    ''add analysis companies to hashtable
    '        For Each drow As DataRow In dtable.DefaultView.ToTable(True, ("company,IsCurrency").Split(",")).Select("IsCurrency = False")
    '            If ht_comp.ContainsKey(drow("company")) = False Then
    '                ht_comp.Add(drow("company"), 1)
    '            End If
    '        Next

    ''analysis companies copy to string array
    'Dim ar_comp(ht_comp.Count - 1) As String
    '        ht_comp.Keys.CopyTo(ar_comp, 0)



    '        sw = New StreamWriter(fs_input)
    '        sw.WriteLine("<?xml version=""" & "1.0""" & "?>")
    '        sw.WriteLine("<posFile>")
    '        sw.WriteLine("<fileFormat>4.00</fileFormat>")
    '        sw.WriteLine("<created>" & Format(Today.Year, "####") & Format(Today.Month, "##") & Format(Today.Day, "##") & "</created>")
    '        sw.WriteLine("<pointInTime>")
    '        sw.WriteLine("<date></date>")
    '        sw.WriteLine("<isSetl>0</isSetl>")
    '        sw.WriteLine("<time>:::::</time>")
    '        sw.WriteLine("<run>0</run>")
    '        sw.WriteLine("<pointInTime>")
    '        sw.WriteLine("<date></date>")
    '        sw.WriteLine("<isSetl>0</isSetl>")
    '        sw.WriteLine("<time>:::::</time>")
    '        sw.WriteLine("<run>0</run>")

    ''loop for each client

    ''Debug.WriteLine(cur_position_client_list)



    ''For Each drow_client In mTbl_ledger.Select(cur_position_client_list)
    '        For i As Integer = 0 To ar_comp.Length - 1
    '            client_code = ar_comp(i)
    '            temp_comp_name = ""

    '            sw.WriteLine("<portfolio>")
    '            sw.WriteLine("<firm>" & client_code & "</firm>")
    '            sw.WriteLine("<acctId>" & client_code & "</acctId>")
    '            sw.WriteLine("<acctType>S</acctType>")
    '            sw.WriteLine("<isCust>1</isCust>")
    '            sw.WriteLine("<seg>N/A</seg>")
    '            sw.WriteLine("<isNew>1</isNew>")
    '            sw.WriteLine("<pclient>0</pclient>")
    '            sw.WriteLine("<currency>INR</currency>")
    '            sw.WriteLine("<ledgerBal>0.00</ledgerBal>")
    '            sw.WriteLine("<ote>0.00</ote>")
    '            sw.WriteLine("<securities>0.00</securities>")

    '            sw.WriteLine("<ecPort>")
    '            sw.WriteLine("<ec>NSCCL</ec>")

    ' ''loop for each position
    '            For Each drow_position In dtable.Select("company = '" & client_code & "' and units <> 0", "company,strikes")
    '                comp_name = drow_position("company")
    '                If InStr(comp_name, "&") > 0 Then
    '                    comp_name = Replace(comp_name, "&", "&amp;")
    '                End If

    '                If UCase(drow_position("cp")) = "F" Then
    '                    option_type = "FUT"
    '                    CAL_PUT = ""
    '                Else
    '                    option_type = "OOP"
    '                    CAL_PUT = Mid(UCase(drow_position("cp")), 1, 1)
    '                End If
    '                mat_date = Format(drow_position("mdate"), "yyyyMMdd")
    '                strike_price = FormatNumber(drow_position("strikes"), 2, TriState.False, TriState.False, TriState.False)
    '                qty = drow_position("units")

    '                If temp_comp_name <> comp_name Then
    '                    If temp_comp_name <> "" Then
    '                        sw.WriteLine("</ccPort>")
    '                    End If
    '                    sw.WriteLine("<ccPort>")
    '                    sw.WriteLine("<cc>" & GetSymbol(comp_name) & "</cc>")
    '                    sw.WriteLine("<r>1</r>")
    '                    sw.WriteLine("<currency>INR</currency>")
    '                    sw.WriteLine("<pss>0</pss>")
    '                End If

    '                sw.WriteLine("<np>")
    '                sw.WriteLine("<exch>NSE</exch>")
    '                sw.WriteLine("<pfCode>" & GetSymbol(comp_name) & "</pfCode>")
    '                sw.WriteLine("<pfType>" & option_type & "</pfType>")
    '                sw.WriteLine("<pe>" & mat_date & "</pe>")
    '                If option_type = "OOP" Then
    '                    sw.WriteLine("<undPe>000000</undPe>")
    '                    sw.WriteLine("<o>" & CAL_PUT & "</o>")
    '                    sw.WriteLine("<k>" & strike_price & "</k>")
    '                End If
    '                sw.WriteLine("<net>" & qty & "</net>")
    '                sw.WriteLine("</np>")

    '                temp_comp_name = comp_name
    '            Next
    '            sw.WriteLine("</ccPort>")
    ''end of loop for each position


    '            sw.WriteLine("</ecPort>")
    '            sw.WriteLine("</portfolio>")
    '        Next
    '        sw.WriteLine("</pointInTime>")
    '        sw.WriteLine("</pointInTime>")
    '        sw.WriteLine("</posFile>")
    '        sw.Close()
    '        fs_input.Close()



    ''Currency......................................
    ''add analysis companies to hashtable
    '        For Each drow As DataRow In dtable.DefaultView.ToTable(True, ("company,IsCurrency").Split(",")).Select("IsCurrency = True")
    '            If ht_comp.ContainsKey(drow("company")) = False Then
    '                ht_comp.Add(drow("company"), 1)
    '            End If
    '        Next

    ''analysis companies copy to string array
    '        ReDim ar_comp(ht_comp.Count - 1)
    '        ht_comp.Keys.CopyTo(ar_comp, 0)



    '        sw = New StreamWriter(fs_curinput)
    '        sw.WriteLine("<?xml version=""" & "1.0""" & "?>")
    '        sw.WriteLine("<posFile>")
    '        sw.WriteLine("<fileFormat>4.00</fileFormat>")
    '        sw.WriteLine("<created>" & Format(Today.Year, "####") & Format(Today.Month, "##") & Format(Today.Day, "##") & "</created>")
    '        sw.WriteLine("<pointInTime>")
    '        sw.WriteLine("<date></date>")
    '        sw.WriteLine("<isSetl>0</isSetl>")
    '        sw.WriteLine("<time>:::::</time>")
    '        sw.WriteLine("<run>0</run>")
    '        sw.WriteLine("<pointInTime>")
    '        sw.WriteLine("<date></date>")
    '        sw.WriteLine("<isSetl>0</isSetl>")
    '        sw.WriteLine("<time>:::::</time>")
    '        sw.WriteLine("<run>0</run>")

    ''loop for each client

    ''Debug.WriteLine(cur_position_client_list)



    ''For Each drow_client In mTbl_ledger.Select(cur_position_client_list)
    '        For i As Integer = 0 To ar_comp.Length - 1
    '            client_code = ar_comp(i)
    '            temp_comp_name = ""

    '            sw.WriteLine("<portfolio>")
    '            sw.WriteLine("<firm>" & client_code & "</firm>")
    '            sw.WriteLine("<acctId>" & client_code & "</acctId>")
    '            sw.WriteLine("<acctType>S</acctType>")
    '            sw.WriteLine("<isCust>1</isCust>")
    '            sw.WriteLine("<seg>N/A</seg>")
    '            sw.WriteLine("<isNew>1</isNew>")
    '            sw.WriteLine("<pclient>0</pclient>")
    '            sw.WriteLine("<currency>INR</currency>")
    '            sw.WriteLine("<ledgerBal>0.00</ledgerBal>")
    '            sw.WriteLine("<ote>0.00</ote>")
    '            sw.WriteLine("<securities>0.00</securities>")

    '            sw.WriteLine("<ecPort>")
    '            sw.WriteLine("<ec>NSCCL</ec>")

    ' ''loop for each position
    '            For Each drow_position In dtable.Select("company = '" & client_code & "' and units <> 0", "company,strikes")
    '                comp_name = drow_position("company")
    '                If InStr(comp_name, "&") > 0 Then
    '                    comp_name = Replace(comp_name, "&", "&amp;")
    '                End If

    '                If UCase(drow_position("cp")) = "F" Then
    '                    option_type = "FUT"
    '                    CAL_PUT = ""
    '                Else
    '                    option_type = "OOP"
    '                    CAL_PUT = Mid(UCase(drow_position("cp")), 1, 1)
    '                End If
    '                mat_date = Format(drow_position("mdate"), "yyyyMMdd")
    '                strike_price = FormatNumber(drow_position("strikes"), 2, TriState.False, TriState.False, TriState.False)
    '                qty = Val(drow_position("Lots") & "")

    '                If temp_comp_name <> comp_name Then
    '                    If temp_comp_name <> "" Then
    '                        sw.WriteLine("</ccPort>")
    '                    End If
    '                    sw.WriteLine("<ccPort>")
    '                    sw.WriteLine("<cc>" & GetSymbol(comp_name) & "</cc>")
    '                    sw.WriteLine("<r>1</r>")
    '                    sw.WriteLine("<currency>INR</currency>")
    '                    sw.WriteLine("<pss>0</pss>")
    '                End If

    '                sw.WriteLine("<np>")
    '                sw.WriteLine("<exch>NSE</exch>")
    '                sw.WriteLine("<pfCode>" & GetSymbol(comp_name) & "</pfCode>")
    '                sw.WriteLine("<pfType>" & option_type & "</pfType>")
    '                sw.WriteLine("<pe>" & mat_date & "</pe>")
    '                If option_type = "OOP" Then
    '                    sw.WriteLine("<undPe>000000</undPe>")
    '                    sw.WriteLine("<o>" & CAL_PUT & "</o>")
    '                    sw.WriteLine("<k>" & strike_price & "</k>")
    '                End If
    '                sw.WriteLine("<net>" & qty & "</net>")
    '                sw.WriteLine("</np>")

    '                temp_comp_name = comp_name
    '            Next
    '            sw.WriteLine("</ccPort>")
    ''end of loop for each position


    '            sw.WriteLine("</ecPort>")
    '            sw.WriteLine("</portfolio>")
    '        Next
    '        sw.WriteLine("</pointInTime>")
    '        sw.WriteLine("</pointInTime>")
    '        sw.WriteLine("</posFile>")
    '        sw.Close()
    '        fs_curinput.Close()
    ''============================

    '        If System.IO.File.Exists(mSPAN_path & "\output.xml") = True Then
    '            Try
    '                System.IO.File.Delete(mSPAN_path & "\output.xml")
    '            Catch ex As Exception
    '                MsgBox("Software cannot access SPAN Output file.", MsgBoxStyle.Exclamation)
    '                Return False
    '                Exit Function
    '            End Try
    '        End If

    '        If System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = True Then
    '            Try
    '                System.IO.File.Delete(mSPAN_path & "\curoutput.xml")
    '            Catch ex As Exception
    '                MsgBox("Software cannot access SPAN Currency Output file.", MsgBoxStyle.Exclamation)
    '                Return False
    '                Exit Function
    '            End Try
    '        End If

    ''Shell(mSPAN_path & "\generate.bat", AppWinStyle.MinimizedNoFocus)
    'Dim worker As New System.Threading.Thread(AddressOf execute_batch_file)
    '        worker.Start()
    '        worker.Join()
    ''execute_batch_file()
    ''Process.Start(mSPAN_path & "\spanit", mSPAN_path & "\span.spn")
    ''MsgBox("job_completed")

    ''If extract_span_req() = False Then
    ''    Return False
    ''    Exit Function
    ''End If

    '        If extract_exposure_margin() = False Then
    '            Return False
    '            Exit Function
    '        End If

    'Dim drow_span As DataRow
    'Dim fut_opt As String
    'Dim mRet_Obj As Object
    'Dim mDatabase_margin As Double
    '        For Each drow As DataRow In dtable.Select("(cp='F') or (cp<>'F' and units < 0)")


    '            If LCase(drow("cp")) = "F" Then
    '                fut_opt = "FUT"

    '                For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "' and mat_month = " & CDate(drow("mdate")).Month)

    '                    For Each drow_span In mTbl_SPAN_output.Select("clientcode='" & drow("company") & "'")

    '                        mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(drow("company")) & "'")
    '                        If Not IsDBNull(mRet_Obj) Then
    '                            mDatabase_margin = Val(mRet_Obj) / 100
    '                        Else
    '                            mDatabase_margin = 0
    '                        End If
    '                        If drow("units") < 0 Then
    '                            drow_span("exposure_margin") += ((drow1("p") * (-drow("units")) * mDatabase_margin))
    '                        Else
    '                            drow_span("exposure_margin") += ((drow1("p") * drow("units") * mDatabase_margin))
    '                        End If

    '                    Next
    '                Next
    '            Else
    '                fut_opt = "OPT"

    '                For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "'")

    '                    For Each drow_span In mTbl_SPAN_output.Select("clientcode='" & drow("company") & "'")

    '                        mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(drow("company")) & "'")
    '                        If Not IsDBNull(mRet_Obj) Then
    '                            mDatabase_margin = Val(mRet_Obj) / 100
    '                        Else
    '                            mDatabase_margin = 0
    '                        End If
    '                        If drow("units") < 0 Then
    '                            drow_span("exposure_margin") += ((drow1("p") * (-drow("units")) * mDatabase_margin))
    '                        Else
    '                            drow_span("exposure_margin") += ((drow1("p") * drow("units") * mDatabase_margin))
    '                        End If

    '                    Next
    '                Next
    '            End If
    '        Next

    '        set_exact_exposure_margin()
    ''DataGridView1.DataSource = mTbl_SPAN_output
    ''MsgBox("exposure margin: - " & mTbl_SPAN_output.Rows(0)("exposure_margin"))
    ''MsgBox("exposure margin: - " & mTbl_SPAN_output.Rows(0)("spanreq") - mTbl_SPAN_output.Rows(0)("anov"))
    ''DataGridView1.DataSource = mTbl_exposure_comp
    ''DataGridView2.DataSource = mTbl_SPAN_output
    '        Return True
    '    Catch ex As Exception
    '        MsgBox(ex.ToString)
    '        Return False
    '    End Try


    'End Function

    Private Sub generate_SPAN_data() 'As Boolean
        Try
            Dim dtable As New DataTable
            dtable = maintable.Copy()

            mTbl_exposure_comp.Clear()
            mTbl_SPAN_output.Clear()
            mTbl_span_calc.Clear()

            Dim client_code As String
            Dim temp_comp_name As String
            Dim comp_name As String
            Dim option_type As String 'OOP or FUT
            Dim mat_date As String 'yyyymmdd
            Dim CAL_PUT As String 'C or P
            Dim strike_price As String
            Dim qty As String

            Dim ht_comp As New Hashtable
            Dim drow_position As DataRow

            'add analysis companies to hashtable
            For Each drow As DataRow In dtable.DefaultView.ToTable(True, "company").Rows
                If ht_comp.ContainsKey(drow("company")) = False Then
                    ht_comp.Add(drow("company"), 1)
                End If
            Next

            'analysis companies copy to string array
            Dim ar_comp(ht_comp.Count - 1) As String
            ht_comp.Keys.CopyTo(ar_comp, 0)

            Try


                System.IO.File.Delete(mSPAN_path & "\output.xml")
                System.IO.File.Delete(mSPAN_path & "\curoutput.xml")
                System.IO.File.Delete(mSPAN_path & "\span.spn")
                System.IO.File.Delete(mSPAN_path & "\curspan.spn")
                System.IO.File.Delete(mSPAN_path & "\input.txt")
                System.IO.File.Delete(mSPAN_path & "\curinput.txt")
                System.IO.File.Delete(mSPAN_path & "\generate.bat")
                System.IO.File.Delete(mSPAN_path & "\curgenerate.bat")
            Catch ex As Exception
                MsgBox("Error")
                FlgThr_Span = False
            End Try

            wait(3000)

            'create file stream
            Dim fs_spn As New FileStream(mSPAN_path & "\span.spn", FileMode.Create)
            Dim fs_Curspn As New FileStream(mSPAN_path & "\curspan.spn", FileMode.Create)

            Dim fs_input As New FileStream(mSPAN_path & "\input.txt", FileMode.Create)
            Dim fs_Curinput As New FileStream(mSPAN_path & "\curinput.txt", FileMode.Create)

            Dim fs_batchfile As New FileStream(mSPAN_path & "\generate.bat", FileMode.Create)
            Dim fs_Curbatchfile As New FileStream(mSPAN_path & "\curgenerate.bat", FileMode.Create)


            Dim sw As StreamWriter
            sw = New StreamWriter(fs_spn)
            Dim cursw As StreamWriter
            cursw = New StreamWriter(fs_Curspn)

            'CType(Me.MdiParent, mdiMain).StatusBar1.Panels(2).Text = ""
            If Not Directory.Exists(mSPAN_path) Then 'if not correct span software path
                MsgBox("Enter Correct Path for span in setting.")
                Exit Sub
            End If
            'get latest span file
            mCurrent_SPAN_file = get_latest_spn_file(mSPAN_path, "FO")
            mCurrent_CurSPAN_file = get_latest_spn_file(mSPAN_path, "CUR")
            get_expected_latest_spn_file(mCurrent_SPAN_file, "FO")
            get_expected_latest_spn_file(mCurrent_CurSPAN_file, "CUR")

            'CType(Me.MdiParent, mdiMain).StatusBar1.Panels(2).Text = mCurrent_SPAN_file
            If mCurrent_SPAN_file = "" And mCurrent_CurSPAN_file = "" Then
                MsgBox("Invalid Span File..!")
                FlgThr_Span = False
                Exit Sub
            End If


            sw.WriteLine("LOAD " & mSPAN_path & "\" & mCurrent_SPAN_file)
            sw.WriteLine("Load " & mSPAN_path & "\input.txt" & ",USEXTLAYOUT")
            sw.WriteLine("Calc")
            sw.WriteLine("Save " & mSPAN_path & "\output.xml")
            sw.Close()
            fs_spn.Close()

            cursw.WriteLine("LOAD " & mSPAN_path & "\" & mCurrent_CurSPAN_file)
            cursw.WriteLine("Load " & mSPAN_path & "\curinput.txt" & ",USEXTLAYOUT")
            cursw.WriteLine("Calc")
            cursw.WriteLine("Save " & mSPAN_path & "\curoutput.xml")
            cursw.Close()
            fs_Curspn.Close()


            sw = New StreamWriter(fs_batchfile)
            sw.WriteLine(mSPAN_path & "\spanit " & mSPAN_path & "\span.spn")
            sw.Close()
            fs_batchfile.Close()

            sw = New StreamWriter(fs_Curbatchfile)
            sw.WriteLine(mSPAN_path & "\spanit " & mSPAN_path & "\curspan.spn")
            sw.Close()
            fs_Curbatchfile.Close()



            ' Dim temp As Integer

            sw = New StreamWriter(fs_input)
            sw.WriteLine("<?xml version=""" & "1.0""" & "?>")
            sw.WriteLine("<posFile>")
            sw.WriteLine("<fileFormat>4.00</fileFormat>")
            sw.WriteLine("<created>" & Format(Today.Year, "####") & Format(Today.Month, "##") & Format(Today.Day, "##") & "</created>")
            sw.WriteLine("<pointInTime>")
            sw.WriteLine("<date></date>")
            sw.WriteLine("<isSetl>0</isSetl>")
            sw.WriteLine("<time>:::::</time>")
            sw.WriteLine("<run>0</run>")
            sw.WriteLine("<pointInTime>")
            sw.WriteLine("<date></date>")
            sw.WriteLine("<isSetl>0</isSetl>")
            sw.WriteLine("<time>:::::</time>")
            sw.WriteLine("<run>0</run>")

            'loop for each client

            'Debug.WriteLine(cur_position_client_list)


            'For Each drow_client In mTbl_ledger.Select(cur_position_client_list)
            For i As Integer = 0 To ar_comp.Length - 1
                client_code = ar_comp(i)
                temp_comp_name = ""

                sw.WriteLine("<portfolio>")
                sw.WriteLine("<firm>" & client_code & "</firm>")
                sw.WriteLine("<acctId>" & GetSymbol(client_code) & "</acctId>")
                sw.WriteLine("<acctType>S</acctType>")
                sw.WriteLine("<isCust>1</isCust>")
                sw.WriteLine("<seg>N/A</seg>")
                sw.WriteLine("<isNew>1</isNew>")
                sw.WriteLine("<pclient>0</pclient>")
                sw.WriteLine("<currency>INR</currency>")
                sw.WriteLine("<ledgerBal>0.00</ledgerBal>")
                sw.WriteLine("<ote>0.00</ote>")
                sw.WriteLine("<securities>0.00</securities>")

                sw.WriteLine("<ecPort>")
                sw.WriteLine("<ec>NSCCL</ec>")

                ''loop for each position
                For Each drow_position In dtable.Select("company = '" & client_code & "' and units <> 0", "company,strikes")
                    comp_name = drow_position("company")
                    If InStr(comp_name, "&") > 0 Then
                        comp_name = Replace(comp_name, "&", "&amp;")
                    End If

                    If UCase(drow_position("cp")) = "F" Then
                        option_type = "FUT"
                        CAL_PUT = ""
                    Else
                        option_type = "OOP"
                        CAL_PUT = Mid(UCase(drow_position("cp")), 1, 1)
                    End If
                    If comp_name.Contains("INR") Then
                        mat_date = Format(drow_position("mdate"), "yyyyMM")
                    Else
                        mat_date = Format(drow_position("mdate"), "yyyyMMdd")
                    End If

                    strike_price = FormatNumber(drow_position("strikes"), 2, TriState.False, TriState.False, TriState.False)
                    If CBool(drow_position("IsCurrency")) = True Then
                        qty = 0 'drow_position("Lots")
                    Else
                        qty = drow_position("units")
                    End If


                    If temp_comp_name <> comp_name Then
                        If temp_comp_name <> "" Then
                            sw.WriteLine("</ccPort>")
                        End If
                        sw.WriteLine("<ccPort>")
                        sw.WriteLine("<cc>" & GetSymbol(comp_name) & "</cc>")
                        sw.WriteLine("<r>1</r>")
                        sw.WriteLine("<currency>INR</currency>")
                        sw.WriteLine("<pss>0</pss>")
                    End If

                    sw.WriteLine("<np>")
                    sw.WriteLine("<exch>NSE</exch>")
                    sw.WriteLine("<pfCode>" & GetSymbol(comp_name) & "</pfCode>")
                    sw.WriteLine("<pfType>" & option_type & "</pfType>")
                    sw.WriteLine("<pe>" & mat_date & "</pe>")
                    If option_type = "OOP" Then
                        If comp_name.Contains("INR") Then
                            sw.WriteLine("<undPe>000000</undPe>")
                        Else
                            sw.WriteLine("<undPe>00000000</undPe>")
                        End If

                        sw.WriteLine("<o>" & CAL_PUT & "</o>")
                        sw.WriteLine("<k>" & strike_price & "</k>")
                    End If
                    sw.WriteLine("<net>" & qty & "</net>")
                    sw.WriteLine("</np>")

                    temp_comp_name = comp_name
                Next
                sw.WriteLine("</ccPort>")
                'end of loop for each position


                sw.WriteLine("</ecPort>")
                sw.WriteLine("</portfolio>")
            Next
            sw.WriteLine("</pointInTime>")
            sw.WriteLine("</pointInTime>")
            sw.WriteLine("</posFile>")
            sw.Close()
            fs_input.Close()


            '''''Currinpitfile
            sw = New StreamWriter(fs_Curinput)
            sw.WriteLine("<?xml version=""" & "1.0""" & "?>")
            sw.WriteLine("<posFile>")
            sw.WriteLine("<fileFormat>4.00</fileFormat>")
            sw.WriteLine("<created>" & Format(Today.Year, "####") & Format(Today.Month, "##") & Format(Today.Day, "##") & "</created>")
            sw.WriteLine("<pointInTime>")
            sw.WriteLine("<date></date>")
            sw.WriteLine("<isSetl>0</isSetl>")
            sw.WriteLine("<time>:::::</time>")
            sw.WriteLine("<run>0</run>")
            sw.WriteLine("<pointInTime>")
            sw.WriteLine("<date></date>")
            sw.WriteLine("<isSetl>0</isSetl>")
            sw.WriteLine("<time>:::::</time>")
            sw.WriteLine("<run>0</run>")

            'loop for each client

            'Debug.WriteLine(cur_position_client_list)


            'For Each drow_client In mTbl_ledger.Select(cur_position_client_list)
            For i As Integer = 0 To ar_comp.Length - 1
                client_code = ar_comp(i)
                temp_comp_name = ""

                sw.WriteLine("<portfolio>")
                sw.WriteLine("<firm>" & client_code & "</firm>")
                sw.WriteLine("<acctId>" & GetSymbol(client_code) & "</acctId>")
                sw.WriteLine("<acctType>S</acctType>")
                sw.WriteLine("<isCust>1</isCust>")
                sw.WriteLine("<seg>N/A</seg>")
                sw.WriteLine("<isNew>1</isNew>")
                sw.WriteLine("<pclient>0</pclient>")
                sw.WriteLine("<currency>INR</currency>")
                sw.WriteLine("<ledgerBal>0.00</ledgerBal>")
                sw.WriteLine("<ote>0.00</ote>")
                sw.WriteLine("<securities>0.00</securities>")

                sw.WriteLine("<ecPort>")
                sw.WriteLine("<ec>NSCCL</ec>")

                ''loop for each position
                For Each drow_position In dtable.Select("company = '" & client_code & "' and units <> 0", "company,strikes")
                    comp_name = drow_position("company")
                    If InStr(comp_name, "&") > 0 Then
                        comp_name = Replace(comp_name, "&", "&amp;")
                    End If

                    If UCase(drow_position("cp")) = "F" Then
                        option_type = "FUT"
                        CAL_PUT = ""
                    Else
                        option_type = "OOP"
                        CAL_PUT = Mid(UCase(drow_position("cp")), 1, 1)
                    End If
                    If client_code.Contains("INR") Then
                        mat_date = Format(drow_position("mdate"), "yyyyMM")
                    Else
                        mat_date = Format(drow_position("mdate"), "yyyyMMdd")
                    End If

                    strike_price = FormatNumber(drow_position("strikes"), 2, TriState.False, TriState.False, TriState.False)
                    If CBool(drow_position("IsCurrency")) = True Then
                        qty = drow_position("Lots")
                    Else
                        qty = 0 'drow_position("units")
                    End If


                    If temp_comp_name <> comp_name Then
                        If temp_comp_name <> "" Then
                            sw.WriteLine("</ccPort>")
                        End If
                        sw.WriteLine("<ccPort>")
                        sw.WriteLine("<cc>" & GetSymbol(comp_name) & "</cc>")
                        sw.WriteLine("<r>1</r>")
                        sw.WriteLine("<currency>INR</currency>")
                        sw.WriteLine("<pss>0</pss>")
                    End If

                    sw.WriteLine("<np>")
                    sw.WriteLine("<exch>NSE</exch>")
                    sw.WriteLine("<pfCode>" & GetSymbol(comp_name) & "</pfCode>") '//Viral
                    sw.WriteLine("<pfType>" & option_type & "</pfType>")
                    sw.WriteLine("<pe>" & mat_date & "</pe>")
                    If option_type = "OOP" Then
                        If client_code.Contains("INR") Then
                            sw.WriteLine("<undPe>000000</undPe>")
                        Else
                            sw.WriteLine("<undPe>00000000</undPe>")
                        End If

                        sw.WriteLine("<o>" & CAL_PUT & "</o>")
                        sw.WriteLine("<k>" & strike_price & "</k>")
                    End If
                    sw.WriteLine("<net>" & qty & "</net>")
                    sw.WriteLine("</np>")

                    temp_comp_name = comp_name
                Next
                sw.WriteLine("</ccPort>")
                'end of loop for each position


                sw.WriteLine("</ecPort>")
                sw.WriteLine("</portfolio>")
            Next
            sw.WriteLine("</pointInTime>")
            sw.WriteLine("</pointInTime>")
            sw.WriteLine("</posFile>")
            sw.Close()
            fs_Curinput.Close()




            If System.IO.File.Exists(mSPAN_path & "\output.xml") = True Then
                Try
                    System.IO.File.Delete(mSPAN_path & "\output.xml")
                Catch ex As Exception
                    MsgBox("Software cannot access SPAN Output file.", MsgBoxStyle.Exclamation)
                    'Return False
                    FlgThr_Span = False
                    Exit Sub
                End Try
            End If

            If System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = True Then
                Try
                    System.IO.File.Delete(mSPAN_path & "\curoutput.xml")
                Catch ex As Exception
                    MsgBox("Software cannot access SPAN Currency Output file.", MsgBoxStyle.Exclamation)
                    'Return False
                    FlgThr_Span = False
                    Exit Sub
                End Try
            End If

            'Shell(mSPAN_path & "\generate.bat", AppWinStyle.MinimizedNoFocus)

            'Dim worker As New System.Threading.Thread(AddressOf execute_batch_file)
            'worker.Start()
            'worker.Join()

            If mCurrent_SPAN_file <> "" Then
                Dim worker As New System.Threading.Thread(AddressOf execute_FO_batch_file)
                worker.Start()
                worker.Join()
            End If

            If mCurrent_CurSPAN_file <> "" Then
                Dim worker2 As New System.Threading.Thread(AddressOf execute_Cur_batch_file)
                worker2.Start()
                worker2.Join()
            End If


            'execute_batch_file()
            'Process.Start(mSPAN_path & "\spanit", mSPAN_path & "\span.spn")
            'MsgBox("job_completed")

            'If extract_span_req() = False Then
            '    Return False
            '    Exit Function
            'End If

            If extract_exposure_margin() = False Then
                'Return False
                FlgThr_Span = False
                Exit Sub
            End If

            Dim drow_span As DataRow
            Dim fut_opt As String
            Dim mRet_Obj As Object
            Dim mDatabase_margin As Double


            Dim Dv As DataView
            Dv = New DataView(mTbl_exposure_comp.Copy)
            mTbl_exposure_comp = Dv.ToTable(True, "CompName", "mat_MOnth", "P", "fut_opt")

            Dim tblSpn As New DataTable
            Dim column As DataColumn

            column = New DataColumn()
            column.DataType = GetType(String)
            column.ColumnName = "ClientCode"
            column.DefaultValue = ""
            tblSpn.Columns.Add(column) ' company name

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "lfv"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "sfv"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "lov"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "sov"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "spanreq"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column) ' for initial margin  spanreq-anvo

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "anov"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)

            column = New DataColumn()
            column.DataType = GetType(Double)
            column.ColumnName = "exposure_margin"
            column.DefaultValue = 0
            tblSpn.Columns.Add(column)




            Dv = New DataView(mTbl_SPAN_output.Copy)
            Dim row As DataRow
            For Each Drow As DataRow In Dv.ToTable(True, "ClientCode").Rows
                row = tblSpn.NewRow
                row("ClientCode") = Drow("ClientCode")
                row("lfv") = mTbl_SPAN_output.Compute("Max(lfv)", "ClientCode='" & Drow("ClientCode") & "'")
                row("sfv") = mTbl_SPAN_output.Compute("Max(sfv)", "ClientCode='" & Drow("ClientCode") & "'")
                row("lov") = mTbl_SPAN_output.Compute("Max(lov)", "ClientCode='" & Drow("ClientCode") & "'")
                row("sov") = mTbl_SPAN_output.Compute("Max(sov)", "ClientCode='" & Drow("ClientCode") & "'")
                row("spanreq") = mTbl_SPAN_output.Compute("Max(spanreq)", "ClientCode='" & Drow("ClientCode") & "'")
                row("anov") = mTbl_SPAN_output.Compute("Max(anov)", "ClientCode='" & Drow("ClientCode") & "'")
                row("exposure_margin") = mTbl_SPAN_output.Compute("Max(exposure_margin)", "ClientCode='" & Drow("ClientCode") & "'")
                tblSpn.Rows.Add(row)
                tblSpn.AcceptChanges()
            Next




            mTbl_SPAN_output = tblSpn


            For Each drow As DataRow In dtable.Select("(cp='F') or (cp<>'F' and units < 0)")


                If LCase(drow("cp")) = "F" Then
                    fut_opt = "FUT"




                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "' and mat_month = " & CDate(drow("mdate")).Month)

                        For Each drow_span In mTbl_SPAN_output.Select("clientcode='" & drow("company") & "'")

                            mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(drow("company")) & "'")
                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                mDatabase_margin = 0
                            End If
                            'If CBool(drow("IsCurrency")) = True Then
                            '    If drow("lots") < 0 Then
                            '        drow_span("exposure_margin") += ((drow1("p") * (-drow("lots")) * mDatabase_margin))
                            '    Else
                            '        drow_span("exposure_margin") += ((drow1("p") * drow("lots") * mDatabase_margin))
                            '    End If
                            'Else
                            If drow("units") < 0 Then
                                drow_span("exposure_margin") += ((drow1("p") * (-drow("units")) * mDatabase_margin))
                            Else
                                drow_span("exposure_margin") += ((drow1("p") * drow("units") * mDatabase_margin))
                            End If
                            'End If


                        Next
                    Next
                Else
                    fut_opt = "OPT"

                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(drow("company")) & "' and fut_opt='" & fut_opt & "'")

                        For Each drow_span In mTbl_SPAN_output.Select("clientcode='" & drow("company") & "'")

                            mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(drow("company")) & "'")
                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                mDatabase_margin = 0
                            End If
                            'If CBool(drow("IsCurrency")) = True Then
                            '    If drow("lots") < 0 Then
                            '        drow_span("exposure_margin") += ((drow1("p") * (-drow("lots")) * mDatabase_margin))
                            '    Else
                            '        drow_span("exposure_margin") += ((drow1("p") * drow("lots") * mDatabase_margin))
                            '    End If
                            'Else
                            If drow("units") < 0 Then
                                drow_span("exposure_margin") += ((drow1("p") * (-drow("units")) * mDatabase_margin))
                            Else
                                drow_span("exposure_margin") += ((drow1("p") * drow("units") * mDatabase_margin))
                            End If
                            'End If
                        Next
                    Next
                End If
            Next

            set_exact_exposure_margin()
            'DataGridView1.DataSource = mTbl_SPAN_output
            'MsgBox("exposure margin: - " & mTbl_SPAN_output.Rows(0)("exposure_margin"))
            'MsgBox("exposure margin: - " & mTbl_SPAN_output.Rows(0)("spanreq") - mTbl_SPAN_output.Rows(0)("anov"))
            'DataGridView1.DataSource = mTbl_exposure_comp
            'DataGridView2.DataSource = mTbl_SPAN_output
            'Return True
        Catch ex As Exception
            MsgBox(ex.ToString)
            'Return False
            FlgThr_Span = False
        End Try


    End Sub
    ''' <summary>
    ''' get_latest_spn_file this function is use to get latest span File of either fo or Currency
    ''' </summary>
    ''' <param name="path"></param>
    ''' <param name="sTyp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function get_latest_spn_file(ByVal path As String, ByVal sTyp As String) As String
        Dim di As New IO.DirectoryInfo(path)
        Dim fi As IO.FileInfo
        Dim max As Date
        Dim current As Date
        Dim max_file As String = ""

        REM 2:span filename format: nsccl.20100503.i01.spn (nsccl_X.yyyymmdd.i01.spn) For Currency SpanFile
        REM 1:span filename format: nsccl.20100503.i01.spn (nsccl.yyyymmdd.i01.spn)
        'length of spanfile name: 20chars

        If sTyp = "FO" Then
            Dim aryFi As IO.FileInfo() = di.GetFiles("nsccl*.spn")
            For Each fi In aryFi 'check for available span file
                Debug.WriteLine(fi.Name)
                If Mid(fi.Name(), 16, 1) = "s" Then
                    'If fi.Name.Length > 20 Then
                    '    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " 23:59:59")
                    'Else
                    If fi.Name.Length = 20 Then
                        current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " 22:59:59")
                    End If
                    'End If
                Else 'filename format:nsccl.yyyymmdd.i01.spn 
                    'If fi.Name.Length > 22 Then
                    '    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " " & Mid(fi.Name, 18, 1) & ":" & Mid(fi.Name, 20, 1) & ":00")
                    'Else
                    If fi.Name.Length = 22 Then
                        current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " " & Mid(fi.Name, 18, 1) & ":00:00")
                    End If
                    'End If
                End If

                If current > max Then 'if find span file
                    max = current
                    max_file = fi.Name() 'return span file name
                End If
            Next
        Else
            Dim aryFi As IO.FileInfo() = di.GetFiles("nsccl_x*.spn")
            For Each fi In aryFi 'check for available span file
                Debug.WriteLine(fi.Name)
                If Mid(fi.Name(), 18, 1) = "s" Then
                    'If fi.Name.Length > 20 Then
                    '    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " 23:59:59")
                    'Else
                    If fi.Name.Length = 22 Then
                        current = CDate(Mid(fi.Name(), 9, 4) & "/" & Mid(fi.Name(), 13, 2) & "/" & Mid(fi.Name(), 15, 2) & " 22:59:59")
                    End If
                    'End If
                Else 'filename format:nsccl.yyyymmdd.i01.spn 
                    'If fi.Name.Length > 22 Then
                    '    current = CDate(Mid(fi.Name(), 7, 4) & "/" & Mid(fi.Name(), 11, 2) & "/" & Mid(fi.Name(), 13, 2) & " " & Mid(fi.Name, 18, 1) & ":" & Mid(fi.Name, 20, 1) & ":00")
                    'Else
                    If fi.Name.Length = 24 Then
                        current = CDate(Mid(fi.Name(), 9, 4) & "/" & Mid(fi.Name(), 13, 2) & "/" & Mid(fi.Name(), 15, 2) & " " & Mid(fi.Name, 20, 1) & ":00:00")
                    End If
                    'End If
                End If
                If current > max Then 'if find span file
                    max = current
                    max_file = fi.Name() 'return span file name
                End If
            Next
        End If
        Return max_file
    End Function
    Private Sub set_exact_exposure_margin()
        Dim client_code_list As String = ""
        Dim dv As DataView
        dv = dtable.DefaultView
        dv.RowFilter = "cp='F'" 'select only FUTURE positions
        dv.Sort = "company,mdate"
        Dim i As Integer = 0
        Dim drow As DataRowView
        Dim mRet_Obj As Object
        Dim mDatabase_margin As Double

        ' Dim prv_clientcode As String
        Dim prv_comp As String
        Dim qty1 As Integer
        Dim set1 As Integer
        Dim rem1 As Integer
        Dim mon1 As Integer

        Dim qty2 As Integer
        Dim set2 As Integer
        Dim rem2 As Integer
        Dim mon2 As Integer

        Dim qty3 As Integer
        Dim set3 As Integer
        Dim rem3 As Integer
        Dim mon3 As Integer

        Dim temp_rem As Integer
        Dim mat_date As Boolean = False

        While i < dv.Count
            drow = dv.Item(i)
            i += 1

a1:
            qty1 = 0
            set1 = 0
            rem1 = 0
            mon1 = 0

            qty2 = 0
            set2 = 0
            rem2 = 0
            mon2 = 0

            qty3 = 0
            set3 = 0
            rem3 = 0
            mon3 = 0

            temp_rem = 0
            mat_date = False

            prv_comp = GetSymbol(drow("company"))
            qty1 = drow("units")
            mon1 = CDate(drow("mdate")).Month

            If DateDiff(DateInterval.Day, Today.Date, CDate(drow("mdate"))) <= 2 Then
                mat_date = True
            End If
            set1 = 0
            rem1 = drow("units")


            If i = dv.Count Then
                Exit While
            End If
            drow = dv.Item(i)
            i += 1

            If prv_comp <> GetSymbol(drow("company")) Then
                GoTo a1
            Else
                prv_comp = GetSymbol(drow("company"))
                qty2 = drow("units")
                mon2 = CDate(drow("mdate")).Month

                If (rem1 < 0 And qty2 < 0) Or (rem1 > 0 And qty2 > 0) Or mat_date = True Then
                    set2 = 0
                    rem2 = qty2
                Else
                    set2 = Math.Min(Math.Abs(rem1), Math.Abs(qty2))
                    If rem1 < 0 Then
                        rem1 = rem1 + set2
                    Else
                        rem1 = rem1 - set2
                    End If
                    If qty2 < 0 Then
                        rem2 = qty2 + set2
                    Else
                        rem2 = qty2 - set2
                    End If
                End If


                If i = dv.Count Then
                    GoTo a2
                End If
                drow = dv.Item(i)
                i += 1

                If prv_comp <> GetSymbol(drow("company")) Then
                    GoTo a2
                Else
                    prv_comp = GetSymbol(drow("company"))
                    qty3 = drow("units")
                    mon3 = CDate(drow("mdate")).Month

                    If (rem2 < 0 And qty3 < 0) Or (rem2 > 0 And qty3 > 0) Then
                        set3 = 0
                        rem3 = qty3
                    Else
                        If mat_date = False Then
                            set3 = Math.Min(Math.Abs(rem1 + rem2), Math.Abs(qty3))
                        Else
                            set3 = Math.Min(Math.Abs(rem2), Math.Abs(qty3))
                        End If

                        If rem1 <> 0 And mat_date = False Then
                            If Math.Abs(rem1) > set3 Then
                                If rem1 < 0 Then
                                    rem1 = rem1 + set3
                                Else
                                    rem1 = rem1 - set3
                                End If
                            Else
                                rem1 = 0
                            End If
                            temp_rem = Math.Abs(set3 - Math.Abs(rem1))

                            If temp_rem > 0 Then
                                If rem2 < 0 Then
                                    rem2 = rem2 + temp_rem
                                Else
                                    rem2 = rem2 - temp_rem
                                End If
                            End If
                        Else
                            If rem2 < 0 Then
                                rem2 = rem2 + set3
                            Else
                                rem2 = rem2 - set3
                            End If
                        End If


                        If qty3 < 0 Then
                            rem3 = qty3 + set3
                        Else
                            rem3 = qty3 - set3
                        End If
                    End If

                    If i = dv.Count Then
                        GoTo a2
                    End If
                    drow = dv.Item(i)
                    i += 1

                End If

a2:
                If qty1 <> rem1 Then
                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(prv_comp) & "' and fut_opt='fut' and mat_month = " & mon1)

                        For Each drow_span As DataRow In mTbl_SPAN_output.Select("clientcode='" & drow("company") & "'")

                            mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(prv_comp) & "'")
                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                mDatabase_margin = 0
                            End If

                            drow_span("exposure_margin") -= (drow1("p") * Math.Abs(qty1) * mDatabase_margin)

                            drow_span("exposure_margin") += (drow1("p") * Math.Abs(rem1) * mDatabase_margin)
                        Next
                    Next
                End If

                If (qty2 <> rem2) Or set2 <> 0 Then
                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(prv_comp) & "' and fut_opt='fut' and mat_month = " & mon2)

                        For Each drow_span As DataRow In mTbl_SPAN_output.Select("clientcode='" & drow("company") & "'")

                            mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(prv_comp) & "'")
                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                mDatabase_margin = 0
                            End If

                            drow_span("exposure_margin") -= (drow1("p") * Math.Abs(qty2) * mDatabase_margin)

                            drow_span("exposure_margin") += (drow1("p") * Math.Abs(rem2) * mDatabase_margin)
                            drow_span("exposure_margin") += (1 / 3) * (drow1("p") * Math.Abs(set2) * mDatabase_margin)
                        Next
                    Next
                End If

                If (qty3 <> rem3) Or set3 <> 0 Then
                    For Each drow1 As DataRow In mTbl_exposure_comp.Select("CompName='" & GetSymbol(prv_comp) & "' and fut_opt='fut' and mat_month = " & mon3)

                        For Each drow_span As DataRow In mTbl_SPAN_output.Select("clientcode='" & drow("company") & "'")

                            mRet_Obj = mTbl_exposure_database.Compute("sum(exposure_margin)", "compname='" & GetSymbol(prv_comp) & "'")
                            If Not IsDBNull(mRet_Obj) Then
                                mDatabase_margin = Val(mRet_Obj) / 100
                            Else
                                mDatabase_margin = 0
                            End If

                            drow_span("exposure_margin") -= (drow1("p") * Math.Abs(qty3) * mDatabase_margin)

                            drow_span("exposure_margin") += (drow1("p") * Math.Abs(rem3) * mDatabase_margin)
                            drow_span("exposure_margin") += (1 / 3) * (drow1("p") * Math.Abs(set3) * mDatabase_margin)
                        Next
                    Next
                End If

                GoTo a1
            End If

        End While


    End Sub
    Public Function get_expected_latest_spn_file(ByVal current_span_file As String, ByVal sType As String)
        If sType = "FO" Then
            If Today.Now < mSPANFile_time1 Or Today.Now > mSPANFile_time6 Then ''previous day last file
                If Today.DayOfWeek() = DayOfWeek.Saturday Then
                    exp_latest_spn_file = "nsccl." & Format(DateAdd(DateInterval.Day, -1, Today), "yyyyMMdd") & ".s.spn"
                    exp_latest_zip_file = "nsccl." & Format(DateAdd(DateInterval.Day, -1, Today), "yyyyMMdd") & ".s.zip"
                ElseIf Today.DayOfWeek() = DayOfWeek.Sunday Then
                    exp_latest_spn_file = "nsccl." & Format(DateAdd(DateInterval.Day, -2, Today), "yyyyMMdd") & ".s.spn"
                    exp_latest_zip_file = "nsccl." & Format(DateAdd(DateInterval.Day, -2, Today), "yyyyMMdd") & ".s.zip"
                Else
                    exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".s.spn" 'nsccl.20070302.s_1.spn
                    exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".s.zip"
                End If
            End If

            If Date.Now >= mSPANFile_time1 And Date.Now < mSPANFile_time2 Then
                exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i01.spn" 'nsccl.20070316.i01_1.spn
                exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i1.zip"
            ElseIf Date.Now >= mSPANFile_time2 And Date.Now < mSPANFile_time3 Then
                exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i02.spn"
                exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i2.zip"
            ElseIf Date.Now >= mSPANFile_time3 And Date.Now < mSPANFile_time4 Then
                exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i03.spn"
                exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i3.zip"
            ElseIf Date.Now >= mSPANFile_time4 And Date.Now < mSPANFile_time5 Then
                exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i04.spn"
                exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i4.zip"
            ElseIf Date.Now >= mSPANFile_time5 And Date.Now < mSPANFile_time6 Then
                exp_latest_spn_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i05.spn"
                exp_latest_zip_file = "nsccl." & Format(Today, "yyyyMMdd") & ".i5.zip"
            End If
        Else
            If Today.Now < mSPANFile_time1 Or Today.Now > mSPANFile_time6 Then ''previous day last file
                If Today.DayOfWeek() = DayOfWeek.Saturday Then
                    exp_latest_Curspn_file = "nsccl_x." & Format(DateAdd(DateInterval.Day, -1, Today), "yyyyMMdd") & ".s.spn"
                    exp_latest_Curzip_file = "nsccl_x." & Format(DateAdd(DateInterval.Day, -1, Today), "yyyyMMdd") & ".s.zip"
                ElseIf Today.DayOfWeek() = DayOfWeek.Sunday Then
                    exp_latest_Curspn_file = "nsccl_x." & Format(DateAdd(DateInterval.Day, -2, Today), "yyyyMMdd") & ".s.spn"
                    exp_latest_Curzip_file = "nsccl_x." & Format(DateAdd(DateInterval.Day, -2, Today), "yyyyMMdd") & ".s.zip"
                Else
                    exp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".s.spn" 'nsccl.20070302.s_1.spn
                    exp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".s.zip"
                End If
            End If

            If Date.Now >= mSPANFile_time1 And Date.Now < mSPANFile_time2 Then
                exp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i01.spn" 'nsccl.20070316.i01_1.spn
                exp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i1.zip"
            ElseIf Date.Now >= mSPANFile_time2 And Date.Now < mSPANFile_time3 Then
                exp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i02.spn"
                exp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i2.zip"
            ElseIf Date.Now >= mSPANFile_time3 And Date.Now < mSPANFile_time4 Then
                exp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i03.spn"
                exp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i3.zip"
            ElseIf Date.Now >= mSPANFile_time4 And Date.Now < mSPANFile_time5 Then
                exp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i04.spn"
                exp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i4.zip"
            ElseIf Date.Now >= mSPANFile_time5 And Date.Now < mSPANFile_time6 Then
                exp_latest_Curspn_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i05.spn"
                exp_latest_Curzip_file = "nsccl_x." & Format(Today, "yyyyMMdd") & ".i5.zip"
            End If
        End If

        'If current_span_file <> exp_latest_spn_file And mDownload_inprogess = False Then
        '    Dim worker As New System.Threading.Thread(AddressOf download_file_from_nse)
        '    worker.Start()
        'End If
    End Function
    Private Sub execute_FO_batch_file()
        'run the batch file
        Try


            Shell(mSPAN_path & "\generate.bat", AppWinStyle.Hide)

            'Process.Start(mSPAN_path & "\generate.bat")
            'MsgBox("job_going_on")

            'wait until generate output.xml file
            While System.IO.File.Exists(mSPAN_path & "\output.xml") = False 'And System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = False
                System.Threading.Thread.Sleep(1000)
            End While
        Catch ex As Exception

        End Try
    End Sub
    Private Sub execute_Cur_batch_file()
        'run the batch file
        Try

        
            Shell(mSPAN_path & "\curgenerate.bat", AppWinStyle.Hide)

            'Process.Start(mSPAN_path & "\generate.bat")
            'MsgBox("job_going_on")

            'wait until generate output.xml file
            While System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = False
                System.Threading.Thread.Sleep(1000)
            End While
        Catch ex As Exception

        End Try
    End Sub

    Private Sub execute_batch_file()
        'run the batch file
        Shell(mSPAN_path & "\generate.bat", AppWinStyle.Hide, True)
        Shell(mSPAN_path & "\curgenerate.bat", AppWinStyle.Hide, True)
        'Process.Start(mSPAN_path & "\generate.bat")
        'MsgBox("job_going_on")

        'wait until generate output.xml file
        While System.IO.File.Exists(mSPAN_path & "\output.xml") = False And System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = False
            System.Threading.Thread.Sleep(1000)
        End While





    End Sub
    Private Function extract_exposure_margin() As Boolean
        Dim sr As StreamReader
        Dim drow_output As DataRow
        Dim temp_data As String

        Dim comp_name As String
        Dim exp_date As String
        Dim strike_price As String
        Dim cal_put_fut As String

        Dim fut_comp_name As String

        Try

            mTbl_exposure_comp.Rows.Clear()
            mTbl_SPAN_output.Rows.Clear()
            mTbl_span_calc.Rows.Clear()
            'check if output.xml exists
            If System.IO.File.Exists(mSPAN_path & "\output.xml") = True Then
                Try
Read_spn_output:
                    sr = New IO.StreamReader(mSPAN_path & "\output.xml")
                Catch ex As Exception
                    System.Threading.Thread.Sleep(100)
                    GoTo Read_spn_output
                End Try

                Dim s As New Xml.XmlTextReader(sr)
                Dim got_span_req As Boolean = False
                's.WhitespaceHandling = Xml.WhitespaceHandling.None

                While s.Read
                    If s.Name = "phyPf" Then
                        Exit While
                    End If
                End While

                While True
                    Select Case s.Name
                        Case "phyPf" 'within <phyPf> contains Equity's LTP
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString
                            drow_output = mTbl_exposure_comp.NewRow
                            drow_output("CompName") = s.ReadElementString("pfCode").ToString
                            drow_output("fut_opt") = "OPT"


                            While True
                                s.Read()
                                Select Case s.Name
                                    Case "phy" 'Equity
                                        s.Read()

                                        temp_data = s.ReadElementString("cId").ToString
                                        temp_data = s.ReadElementString("pe").ToString
                                        drow_output("p") = Val(s.ReadElementString("p"))
                                        mTbl_exposure_comp.Rows.Add(drow_output)

                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "phy"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "phyPf"
                                        Exit While
                                End Select
                            End While
                        Case "futPf" 'for future
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString

                            'got_span_req = False
                            fut_comp_name = s.ReadElementString("pfCode").ToString

                            While True
                                s.Read()

                                Select Case s.Name
                                    Case "fut" 'future's LTP
                                        s.Read()

                                        'If got_span_req = False Then
                                        drow_output = mTbl_exposure_comp.NewRow
                                        drow_output("CompName") = fut_comp_name
                                        drow_output("fut_opt") = "FUT"

                                        temp_data = s.ReadElementString("cId").ToString
                                        temp_data = s.ReadElementString("pe").ToString
                                        drow_output("mat_month") = CInt(Mid(temp_data, 5, 2))
                                        drow_output("p") = Val(s.ReadElementString("p"))
                                        mTbl_exposure_comp.Rows.Add(drow_output)
                                        'got_span_req = True
                                        'End If
                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "fut"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "futPf"
                                        Exit While
                                End Select
                            End While
                        Case "oopPf" 'options
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString
                            comp_name = s.ReadElementString("pfCode").ToString
                            While True
                                s.Read()

                                Select Case s.Name
                                    Case "series"
                                        s.Read()

                                        temp_data = s.ReadElementString("pe").ToString
                                        temp_data = s.ReadElementString("v").ToString
                                        temp_data = s.ReadElementString("volSrc").ToString
                                        temp_data = s.ReadElementString("setlDate").ToString

                                        exp_date = Mid(temp_data, 7, 2) & Format(CDate(Mid(temp_data, 5, 2) & "/01/2000"), "MMM") & Mid(temp_data, 1, 4)
                                        cal_put_fut = "FUT"
                                        strike_price = ""

                                        drow_output = mTbl_span_calc.NewRow
                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                        drow_output("compname") = comp_name
                                        drow_output("cal_put_fut") = cal_put_fut
                                        drow_output("strike_price") = strike_price
                                        drow_output("exp_date") = exp_date
                                        mTbl_span_calc.Rows.Add(drow_output)

                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "opt"
                                                    s.Read()

                                                    temp_data = s.ReadElementString("cId").ToString
                                                    temp_data = s.ReadElementString("o").ToString
                                                    If temp_data = "C" Then
                                                        strike_price = s.ReadElementString("k").ToString

                                                        cal_put_fut = "CAL"
                                                        drow_output = mTbl_span_calc.NewRow
                                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                        drow_output("compname") = comp_name
                                                        drow_output("cal_put_fut") = cal_put_fut
                                                        drow_output("strike_price") = strike_price
                                                        drow_output("exp_date") = exp_date
                                                        mTbl_span_calc.Rows.Add(drow_output)

                                                        cal_put_fut = "PUT"
                                                        drow_output = mTbl_span_calc.NewRow
                                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                        drow_output("compname") = comp_name
                                                        drow_output("cal_put_fut") = cal_put_fut
                                                        drow_output("strike_price") = strike_price
                                                        drow_output("exp_date") = exp_date
                                                        mTbl_span_calc.Rows.Add(drow_output)
                                                    End If

                                                    While True
                                                        s.Read()

                                                        Select Case s.Name
                                                            Case "opt"
                                                                Exit While
                                                        End Select
                                                    End While
                                                Case "series"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "oopPf"
                                        Exit While
                                End Select
                            End While
                        Case "portfolio"

                            While True
                                Select Case s.Name
                                    Case "portfolio"
                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "firm"
                                                    got_span_req = False
                                                    drow_output = mTbl_SPAN_output.NewRow
                                                    temp_data = s.ReadElementString("firm")
                                                    drow_output("ClientCode") = temp_data '//Viral  instade of accid
                                                    temp_data = s.ReadElementString("acctId").ToString()
                                                    temp_data = s.ReadElementString("acctType")
                                                    temp_data = s.ReadElementString("isCust")
                                                    temp_data = s.ReadElementString("seg")
                                                    temp_data = s.ReadElementString("isNew")
                                                    temp_data = s.ReadElementString("pclient")
                                                    temp_data = s.ReadElementString("currency")
                                                    temp_data = s.ReadElementString("ledgerBal")
                                                    temp_data = s.ReadElementString("ote")
                                                    temp_data = s.ReadElementString("securities")
                                                    drow_output("lfv") = Val(s.ReadElementString("lfv"))
                                                    drow_output("sfv") = Val(s.ReadElementString("sfv"))
                                                    drow_output("lov") = Val(s.ReadElementString("lov"))
                                                    drow_output("sov") = Val(s.ReadElementString("sov"))
                                                Case "spanReq"
                                                    If got_span_req = False Then
                                                        drow_output("spanreq") = Val(s.ReadElementString("spanReq"))
                                                        drow_output("anov") = Val(s.ReadElementString("anov"))
                                                        If (drow_output("spanreq") - drow_output("anov")) <= 0 Then
                                                            drow_output("spanreq") = 0
                                                            drow_output("anov") = 0
                                                        End If
                                                        got_span_req = True
                                                    End If
                                                Case "portfolio"
                                                    drow_output("exposure_margin") = 0
                                                    mTbl_SPAN_output.Rows.Add(drow_output)
                                                    Exit While
                                            End Select
                                        End While
                                    Case "spanFile"
                                        Exit While
                                End Select
                                s.Read()

                            End While

                            Exit While
                    End Select
                    s.Read()

                End While

            End If
            If Not IsNothing(sr) Then sr.Close()
            REM Currency By Viral
            '==============================================================
            If System.IO.File.Exists(mSPAN_path & "\curoutput.xml") = True Then
                Try
Read_curspn_output:
                    sr = New IO.StreamReader(mSPAN_path & "\curoutput.xml")
                Catch ex As Exception
                    System.Threading.Thread.Sleep(100)
                    GoTo Read_curspn_output
                End Try

                Dim s As New Xml.XmlTextReader(sr)
                Dim got_span_req As Boolean = False
                's.WhitespaceHandling = Xml.WhitespaceHandling.None

                While s.Read
                    If s.Name = "phyPf" Then
                        Exit While
                    End If
                End While

                While True
                    Select Case s.Name
                        Case "phyPf" 'within <phyPf> contains Equity's LTP
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString
                            drow_output = mTbl_exposure_comp.NewRow
                            drow_output("CompName") = s.ReadElementString("pfCode").ToString
                            drow_output("fut_opt") = "OPT"


                            While True
                                s.Read()
                                Select Case s.Name
                                    Case "phy" 'Equity
                                        s.Read()

                                        temp_data = s.ReadElementString("cId").ToString
                                        temp_data = s.ReadElementString("pe").ToString
                                        drow_output("p") = Val(s.ReadElementString("p"))
                                        mTbl_exposure_comp.Rows.Add(drow_output)

                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "phy"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "phyPf"
                                        Exit While
                                End Select
                            End While
                        Case "futPf" 'for future
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString

                            'got_span_req = False
                            fut_comp_name = s.ReadElementString("pfCode").ToString

                            While True
                                s.Read()

                                Select Case s.Name
                                    Case "fut" 'future's LTP
                                        s.Read()

                                        'If got_span_req = False Then
                                        drow_output = mTbl_exposure_comp.NewRow
                                        drow_output("CompName") = fut_comp_name
                                        drow_output("fut_opt") = "FUT"

                                        temp_data = s.ReadElementString("cId").ToString
                                        temp_data = s.ReadElementString("pe").ToString
                                        drow_output("mat_month") = CInt(Mid(temp_data, 5, 2))
                                        drow_output("p") = Val(s.ReadElementString("p"))
                                        mTbl_exposure_comp.Rows.Add(drow_output)
                                        'got_span_req = True
                                        'End If
                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "fut"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "futPf"
                                        Exit While
                                End Select
                            End While
                        Case "oopPf" 'options
                            s.Read()
                            temp_data = s.ReadElementString("pfId").ToString
                            comp_name = s.ReadElementString("pfCode").ToString
                            While True
                                s.Read()

                                Select Case s.Name
                                    Case "series"
                                        s.Read()

                                        temp_data = s.ReadElementString("pe").ToString
                                        temp_data = s.ReadElementString("v").ToString
                                        temp_data = s.ReadElementString("volSrc").ToString
                                        temp_data = s.ReadElementString("setlDate").ToString

                                        exp_date = Mid(temp_data, 7, 2) & Format(CDate(Mid(temp_data, 5, 2) & "/01/2000"), "MMM") & Mid(temp_data, 1, 4)
                                        cal_put_fut = "FUT"
                                        strike_price = ""

                                        drow_output = mTbl_span_calc.NewRow
                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                        drow_output("compname") = comp_name
                                        drow_output("cal_put_fut") = cal_put_fut
                                        drow_output("strike_price") = strike_price
                                        drow_output("exp_date") = exp_date
                                        mTbl_span_calc.Rows.Add(drow_output)

                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "opt"
                                                    s.Read()

                                                    temp_data = s.ReadElementString("cId").ToString
                                                    temp_data = s.ReadElementString("o").ToString
                                                    If temp_data = "C" Then
                                                        strike_price = s.ReadElementString("k").ToString

                                                        cal_put_fut = "CAL"
                                                        drow_output = mTbl_span_calc.NewRow
                                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                        drow_output("compname") = comp_name
                                                        drow_output("cal_put_fut") = cal_put_fut
                                                        drow_output("strike_price") = strike_price
                                                        drow_output("exp_date") = exp_date
                                                        mTbl_span_calc.Rows.Add(drow_output)

                                                        cal_put_fut = "PUT"
                                                        drow_output = mTbl_span_calc.NewRow
                                                        drow_output("description") = concat_scrip(comp_name, cal_put_fut, exp_date, strike_price)
                                                        drow_output("compname") = comp_name
                                                        drow_output("cal_put_fut") = cal_put_fut
                                                        drow_output("strike_price") = strike_price
                                                        drow_output("exp_date") = exp_date
                                                        mTbl_span_calc.Rows.Add(drow_output)
                                                    End If

                                                    While True
                                                        s.Read()

                                                        Select Case s.Name
                                                            Case "opt"
                                                                Exit While
                                                        End Select
                                                    End While
                                                Case "series"
                                                    Exit While
                                            End Select
                                        End While
                                    Case "oopPf"
                                        Exit While
                                End Select
                            End While
                        Case "portfolio"

                            While True
                                Select Case s.Name
                                    Case "portfolio"
                                        While True
                                            s.Read()

                                            Select Case s.Name
                                                Case "firm"
                                                    got_span_req = False
                                                    drow_output = mTbl_SPAN_output.NewRow
                                                    temp_data = s.ReadElementString("firm")
                                                    drow_output("ClientCode") = temp_data '//Viral Instade of acctId
                                                    temp_data = s.ReadElementString("acctId").ToString()
                                                    temp_data = s.ReadElementString("acctType")
                                                    temp_data = s.ReadElementString("isCust")
                                                    temp_data = s.ReadElementString("seg")
                                                    temp_data = s.ReadElementString("isNew")
                                                    temp_data = s.ReadElementString("pclient")
                                                    temp_data = s.ReadElementString("currency")
                                                    temp_data = s.ReadElementString("ledgerBal")
                                                    temp_data = s.ReadElementString("ote")
                                                    temp_data = s.ReadElementString("securities")
                                                    drow_output("lfv") = Val(s.ReadElementString("lfv"))
                                                    drow_output("sfv") = Val(s.ReadElementString("sfv"))
                                                    drow_output("lov") = Val(s.ReadElementString("lov"))
                                                    drow_output("sov") = Val(s.ReadElementString("sov"))
                                                Case "spanReq"
                                                    If got_span_req = False Then
                                                        drow_output("spanreq") = Val(s.ReadElementString("spanReq"))
                                                        drow_output("anov") = Val(s.ReadElementString("anov"))
                                                        If (drow_output("spanreq") - drow_output("anov")) <= 0 Then
                                                            drow_output("spanreq") = 0
                                                            drow_output("anov") = 0
                                                        End If
                                                        got_span_req = True
                                                    End If
                                                Case "portfolio"
                                                    drow_output("exposure_margin") = 0
                                                    mTbl_SPAN_output.Rows.Add(drow_output)
                                                    Exit While
                                            End Select
                                        End While
                                    Case "spanFile"
                                        Exit While
                                End Select
                                s.Read()

                            End While

                            Exit While
                    End Select
                    s.Read()

                End While

            End If

            Return True
        Catch ex As Exception
            MsgBox("Error in Function 'extract_exposure_margin' ::" & ex.Message)
            Return False
        End Try
    End Function
    Private Function concat_scrip(ByVal comp_name As String, ByVal cal_put_fut As String, ByVal exp_date As String, ByVal strike_price As String) As String
        Dim index_name As String
        Dim a_e As String

        If cal_put_fut = "FUT" Then
            If UCase(comp_name) = "NIFTY" Or UCase(comp_name) = "BANKNIFTY" Or UCase(comp_name) = "CNXIT" Or UCase(comp_name) = "MINIFTY" Or UCase(comp_name) = "CNX100" Or UCase(comp_name) = "JUNIOR" Or UCase(comp_name) = "NFTYMCAP50" Then
                index_name = "FUTIDX"
            Else
                index_name = "FUTSTK"
            End If
        Else
            If UCase(comp_name) = "NIFTY" Or UCase(comp_name) = "BANKNIFTY" Or UCase(comp_name) = "CNXIT" Or UCase(comp_name) = "MINIFTY" Or UCase(comp_name) = "CNX100" Or UCase(comp_name) = "JUNIOR" Or UCase(comp_name) = "NFTYMCAP50" Then
                index_name = "OPTIDX"
                a_e = "E"
            Else
                index_name = "OPTSTK"
                'a_e = "A"
                a_e = "E"
            End If
        End If
        'generate script
        If cal_put_fut = "FUT" Then
            Return index_name & Space(2) & comp_name & Space(2) & exp_date & "  "
        Else
            Return index_name & Space(2) & comp_name & Space(2) & exp_date & "  " & strike_price & "  " & UCase(Mid(cal_put_fut, 1, 1)) & UCase(Mid(a_e, 1, 1))
        End If

    End Function
#End Region
#Region "total & avg of opt,fut & stk"
    Private Sub cmdrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdrefresh.Click

        If AppLicMode = "SERLIC" Then
            If bool_IsServerConnected = False Then Exit Sub
        End If

        'Dim mStart As String
        Try
            If isRefresh = False Then
                'mStart = cmdStart.Text
                isRefresh = True
                Dim t1 As Long = System.Environment.TickCount
                'Dim dStart As Date = Now
                Call proc_data(False)
                'MsgBox("Start:" & dStart & vbCrLf & "End :" & Now)
                Call searchcompany(comptable)
                FSTimerLogFile.WriteLine("Refresh Completed(MS) :" & System.Environment.TickCount - t1)
                'grdtrad.ResumeLayout()
                isRefresh = False
                'Call SetStartStop(mStart)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub

    Private Sub analysis_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim TmpObj = New Object

        If AppLicMode = "SERLIC" Then
            If bool_IsServerConnected = False Then Exit Sub
        End If

        If e.KeyCode = Keys.F5 And e.Control = False Then
            Try
                Call cmdrefresh_Click(TmpObj, e)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        ElseIf e.KeyCode = Keys.F3 Then
            scenario()
        ElseIf e.KeyCode = Keys.F9 Then
            flgSummary = True
            alertmsg = False
            summary()
        ElseIf e.KeyCode = Keys.F12 Then
            start_stop()
        ElseIf e.KeyCode = Keys.F7 Then
            alertentry()
        ElseIf e.KeyCode = Keys.F8 Then
            openposition()
            Call searchcompany()
            MDI.ToolStripcompanyCombo.Text = compname
            '=======================keval chakalasiya(15-02-2010)
        ElseIf e.KeyCode = Keys.F11 Then
            start_stop_future_only()
        ElseIf (e.KeyCode = Keys.F10) Then
            cmdExport_Click(New Object, e)
        ElseIf (e.KeyCode = Keys.Add) And e.Control = True Then
            AddUserDefCompany("")
        ElseIf (e.KeyCode = Keys.A) And e.Control = True Then
            AddUserDefCompany("")
        ElseIf (e.KeyCode = Keys.Left) And e.Control = True Then
            If Not (tbcomp.SelectedIndex <= 0) Then
                tbcomp.SelectedTab = tbcomp.TabPages(tbcomp.SelectedIndex - 1)
            End If
        ElseIf (e.KeyCode = Keys.Right) And e.Control = True Then
            Try
                tbcomp.SelectedTab = tbcomp.TabPages(tbcomp.SelectedIndex + 1)
            Catch ex As Exception
            End Try
        ElseIf (Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.T) Then
            TabControl1.SelectedIndex = 0
        ElseIf (Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.M) Then
            TabControl1.SelectedIndex = 1
        ElseIf (Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.C) Then
            txtcvol.Focus()
            txtcvol.Select(0, txtcvol.Text.Length)
        ElseIf (Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.P) Then
            txtpvol.Focus()
            txtpvol.Select(0, txtpvol.Text.Length)

        ElseIf (Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.F) Then
            txtfut1.Focus()
            txtfut1.Select(0, txtfut1.Text.Length)
        ElseIf (Control.ModifierKeys = Keys.Control And e.KeyCode = Keys.E) Then
            txteqrate.Focus()
            txteqrate.Select(0, txteqrate.Text.Length)
            'ElseIf e.KeyCode = Keys.PageDown Then
            '    If (tbcomp.SelectedIndex <= tbcomp.TabPages.Count) Then
            '        tbcomp.SelectedIndex = tbcomp.SelectedIndex + 1
            '    End If

            'ElseIf e.KeyCode = Keys.PageUp Then
            '    If (tbcomp.SelectedIndex >= 1 And tbcomp.TabPages.Count > 0) Then
            '        tbcomp.SelectedIndex = tbcomp.SelectedIndex - 1
            '    End If
        ElseIf (Control.ModifierKeys = Keys.Alt And e.KeyCode = Keys.C) Then
            Call BtnCalc_Click(sender, e)

        End If


        '================================================
    End Sub

#End Region
#Region "Alert"
    Private Sub alert_comp()
        Dim dv As DataView = New DataView(maintable, "", "company", DataViewRowState.CurrentRows)

        Dim arow As DataRow
        Dim row As DataRow
        allcomp.Rows.Clear()

        altable.Rows.Clear()
        alerttable.Rows.Clear()

        dv = New DataView(GdtFOTrades)
        objAl.Status = 1
        altable = objAl.select_data
        If altable.Rows.Count > 0 Then
            For Each drow As DataRow In altable.Select("entrydate=#" & fDate(Now.Date) & "#")
                For Each row In dv.ToTable(True, "company").Select("company='" & drow("comp_script") & "' ")
                    arow = alerttable.NewRow
                    arow("comp_script") = drow("comp_script")
                    arow("opt") = drow("opt")
                    arow("field") = drow("field")
                    arow("value1") = drow("value1")
                    arow("value2") = drow("value2")
                    arow("status") = 0
                    arow("status1") = 1  'securitywise
                    arow("delta") = 0
                    arow("theta") = 0
                    arow("vega") = 0
                    arow("gamma") = 0
                    arow("volga") = 0
                    arow("vanna") = 0
                    arow("token") = 0
                    arow("ftoken") = 0
                    arow("strike") = 0
                    arow("mdate") = 0
                    arow("cp") = 0
                    arow("units") = 0
                    arow("entrydate") = drow("entrydate")
                    arow("uid") = drow("uid")
                    alerttable.Rows.Add(arow)
                Next
            Next
        End If
        objAl.Status = 2
        altable = objAl.select_data
        If altable.Rows.Count > 0 Then
            For Each drow As DataRow In altable.Select("entrydate=#" & fDate(Now.Date) & "#")
                arow = alerttable.NewRow
                arow("comp_script") = drow("comp_script")
                arow("opt") = drow("opt")
                arow("field") = drow("field")
                arow("value1") = drow("value1")
                arow("value2") = drow("value2")
                arow("status") = 0
                arow("status1") = 2  'for scriptwise
                arow("delta") = 0
                arow("theta") = 0
                arow("vega") = 0
                arow("gamma") = 0
                arow("volga") = 0
                arow("vanna") = 0
                arow("token") = 0
                arow("ftoken") = 0
                arow("strike") = 0
                arow("mdate") = ""
                arow("cp") = 0
                arow("units") = 0
                arow("entrydate") = drow("entrydate")
                arow("uid") = drow("uid")
                arow("units") = (drow("units"))
                For Each row In scripttable.Select("script='" & drow("comp_script") & "' ")
                    arow("token") = CLng(row("token"))
                    arow("strike") = CLng(row("strike_price"))
                    arow("mdate") = CDate(row("expdate1"))
                    arow("cp") = (row("option_type"))

                    For Each frow As DataRow In mdv.ToTable().Select("Symbol='" & row("Symbol") & "' and expdate1=#" & Format(row("expdate1"), "dd-MMM-yyyy") & "#")
                        arow("ftoken") = CLng(frow("token"))
                    Next
                    alerttable.Rows.Add(arow)
                Next
            Next
        End If
        'End If
    End Sub
    Private Sub timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer1.Tick
        alertprocess()
    End Sub
    Private Sub alertprocess()
        Try
            'If AlertOn = 0 Then
            For Each mrow As DataRow In alerttable.Select("status='0' and entrydate=#" & fDate(Now.Date) & "#")
                If mrow("status1") = 1 Then
                    If (ltpprice.Count > 0) Then

                        Dim ltppr As Double = 0
                        Dim ltppr1 As Double = 0

                        Dim fltppr As Double = 0
                        Dim mt As Double = 0
                        Dim isfut As Boolean = False
                        Dim iscall As Boolean = False

                        Dim token As Long
                        Dim token1 As Long
                        mrow("delta") = 0
                        mrow("theta") = 0
                        mrow("vega") = 0
                        mrow("gamma") = 0
                        mrow("volga") = 0
                        mrow("vanna") = 0
                        mrow("grossmtm") = 0
                        Dim alltp As Double = 0
                        For Each drow As DataRow In maintable.Select("company='" & mrow("comp_script") & "'")
                            If CBool(drow("isliq")) = True Then
                                token = CLng(drow("tokanno"))
                                token1 = CLng(drow("token1"))
                            Else
                                token = CLng(drow("tokanno"))
                                token1 = 0
                            End If

                            If ltpprice.Contains(token) Then
                                fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                isfut = True
                                If token1 > 0 Then
                                    ltppr = Val(ltpprice(token))
                                    ltppr1 = Val(ltpprice(token1))
                                Else
                                    ltppr = Val(ltpprice(token))
                                    ltppr1 = 0
                                End If
                                mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                                If Now.Date = CDate(drow("mdate")).Date Then
                                    mt = 0.5
                                End If
                                If drow("cp") = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If

                                If fltppr > 0 Then
                                    CalDataalert(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, iscall, isfut, mrow, Val(drow("units")), Val(drow("traded")))
                                Else
                                    If veqdel > 0 Then
                                        CalDataalert(veqdel, Val(drow("strikes")), ltppr, ltppr1, mt, iscall, isfut, mrow, Val(drow("units")), Val(drow("traded")))
                                    End If
                                End If
                                If Val(drow("units")) = 0 Then
                                    mrow("grossmtm") += Math.Round(-Val(drow("traded")), 2)
                                Else
                                    mrow("grossmtm") += Math.Round((Val(drow("units")) * (Val(ltppr) - Val(drow("traded")))), 2)
                                End If

                            End If
                            If drow("cp") = "F" Then
                                alltp = Val(fltpprice(CLng(drow("tokanno"))))
                                If Val(drow("units")) = 0 Then
                                    mrow("grossmtm") += Math.Round(-Val(drow("traded")), 2)
                                Else
                                    mrow("grossmtm") += Math.Round((Val(drow("units")) * (Val(alltp) - Val(drow("traded")))), 2)
                                End If
                            ElseIf drow("cp") = "E" Then
                                alltp = Val(eltpprice(CLng(drow("tokanno"))))
                                If Val(drow("units")) = 0 Then
                                    mrow("grossmtm") += Math.Round(-Val(drow("traded")), 2)
                                Else
                                    mrow("grossmtm") += Math.Round((Val(drow("units")) * (Val(alltp) - Val(drow("traded")))), 2)
                                End If

                            End If
                            If drow("cp") = "F" Or drow("cp") = "E" Then
                                mrow("delta") = Val(mrow("delta")) + (Val(Math.Round(drow("deltaval"), roundDelta)))
                                mrow("theta") = Val(mrow("theta")) + (Val(Math.Round(drow("thetaval"), roundTheta)))
                                mrow("vega") = Val(mrow("vega")) + (Val(Math.Round(drow("vgval"), roundVega)))
                                mrow("gamma") = Val(mrow("gamma")) + (Val(Math.Round(drow("gmval"), roundGamma)))
                                mrow("volga") = Val(mrow("volga")) + (Val(Math.Round(drow("volgaval"), roundVolga)))
                                mrow("vanna") = Val(mrow("vanna")) + (Val(Math.Round(drow("vannaval"), roundVanna)))
                            End If
                        Next

                    End If
                    alertshow(mrow)
                Else
                    altemp.Rows.Clear()
                    If (ltpprice.Count > 0) Then

                        Dim ltppr As Double = 0

                        Dim fltppr As Double = 0
                        Dim mt As Double = 0
                        Dim isfut As Boolean = False
                        Dim iscall As Boolean = False

                        Dim token As Integer
                        mrow("delta") = 0
                        mrow("theta") = 0
                        mrow("vega") = 0
                        mrow("gamma") = 0
                        mrow("vanna") = 0
                        mrow("gamma") = 0
                        mrow("grossmtm") = 0

                        token = CLng(mrow("token"))

                        If ltpprice.Contains(CLng(token)) Then
                            fltppr = Val(fltpprice(CLng(mrow("ftoken"))))
                            isfut = True
                            ltppr = Val(ltpprice(CLng(token)))
                            mt = DateDiff(DateInterval.Day, Now.Date, CDate(mrow("mdate")).Date)
                            If Now.Date = CDate(mrow("mdate")).Date Then
                                mt = 0.5
                            End If
                            If Mid(mrow("cp"), 1, 1) = "C" Then
                                iscall = True
                            Else
                                iscall = False
                            End If

                            If fltppr > 0 Then
                                CalDataalert(fltppr, Val(mrow("strike")), ltppr, 0, mt, iscall, isfut, mrow, 1, 0)
                            Else
                                If veqdel > 0 Then
                                    CalDataalert(veqdel, Val(mrow("strike")), ltppr, 0, mt, iscall, isfut, mrow, 1, 0)
                                End If
                            End If
                            'If val(mdrow("units")) = 0 Then
                            '    mrow("grossmtm") += Math.Round(val(drow("traded")), 2)
                            'Else
                            '    mrow("grossmtm") += Math.Round((val(drow("units")) * (val(ltppr) - val(drow("traded")))), 2)
                            'End If

                        End If

                    End If
                    alertshow(mrow)
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Sub CalDataalert(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal cpprice1 As Double, ByVal mT As Integer, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal drow As DataRow, ByVal qty As Double, ByVal traded As Double)


        Dim mDelta As Double
        Dim mGama As Double
        Dim mVega As Double
        Dim mThita As Double
        Dim mVolga As Double
        Dim mVanna As Double
        Dim mRah As Double
        Dim mD1 As Double
        Dim mD2 As Double




        Dim mVolatility As Double
        'Dim mVolatility1 As Double
        Dim tmpcpprice As Double = 0
        Dim tmpcpprice1 As Double = 0
        tmpcpprice1 = cpprice1
        tmpcpprice = cpprice
        'Dim mIsCal As Boolean
        'Dim mIsPut As Boolean
        'Dim mIsFut As Boolean


        mDelta = 0
        mGama = 0
        mVega = 0
        mThita = 0
        mVolga = 0
        mVanna = 0
        mRah = 0
        mD1 = 0
        mD2 = 0

        Dim _mt As Double
        'IF MATURITY IS 0 THEN _MT = 0.00001 
        If mT = 0 Then
            _mt = 0.00001

        Else
            _mt = (mT) / 365


        End If
        If tmpcpprice1 = 0 Then
            mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
        Else
            If mIsCall = True Then
                mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, False, mIsFut, 0, 6)
            Else
                mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, True, mIsFut, 0, 6)

            End If
            'mVolatility1 = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, mIsCall, mIsFut, 0, 6)
        End If
        Try

            mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))

            mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))

            mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))

            mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))

            mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))


            mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
            mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

            mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
            mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)



            drow("delta") = Val(drow("delta")) + (Val(Math.Round(mDelta * qty, roundDelta_Val)))
            drow("theta") = Val(drow("theta")) + (Val(Math.Round(mThita * qty, roundTheta_Val)))
            drow("vega") = Val(drow("vega")) + (Val(Math.Round(mVega * qty, roundVega_Val)))
            drow("gamma") = Val(drow("gamma")) + (Val(Math.Round(mGama * qty, roundGamma_Val)))
            drow("volga") = Val(drow("volga")) + (Val(Math.Round(mVolga * qty, roundVolga_Val)))
            drow("vanna") = Val(drow("vanna")) + (Val(Math.Round(mVanna * qty, roundVanna_Val)))

            'drow("grossmtm") = val(drow("grossmtm")) + val(val(qty) * (val(cpprice) - val(traded)))
            'get_value()
        Catch ex As Exception
        End Try
        ''MsgBox(mDelta)
    End Sub
    Private Sub alertshow(ByVal mdrow As DataRow)
        Dim arow As DataRow
        altemp.Rows.Clear()
        If mdrow("opt").ToString.Trim = "Between" Then
            If UCase(mdrow("field")) = UCase("delta") Then
                ' If val(mdrow("delta")) > val(mdrow("value1")) And val(mdrow("delta")) < val(mdrow("value2")) And val(mdrow("delta")) <> 0 Then
                If Val(mdrow("delta")) > Val(mdrow("value1")) And Val(mdrow("delta")) < Val(mdrow("value2")) Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("delta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("theta") Then
                If Val(mdrow("theta")) > Val(mdrow("value1")) And Val(mdrow("theta")) < Val(mdrow("value2")) Then
                    ' If val(mdrow("theta")) > val(mdrow("value1")) And val(mdrow("theta")) < val(mdrow("value2")) And val(mdrow("theta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("theta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vega") Then
                If Val(mdrow("vega")) > Val(mdrow("value1")) And Val(mdrow("vega")) < Val(mdrow("value2")) Then
                    ' If val(mdrow("vega")) > val(mdrow("value1")) And val(mdrow("vega")) < val(mdrow("value2")) And val(mdrow("vega")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vega")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("gamma") Then
                If Val(mdrow("gamma")) > Val(mdrow("value1")) And Val(mdrow("gamma")) < Val(mdrow("value2")) Then
                    'If val(mdrow("gamma")) > val(mdrow("value1")) And val(mdrow("gamma")) < val(mdrow("value2")) And val(mdrow("gamma")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("gamma")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("volga") Then
                If Val(mdrow("volga")) > Val(mdrow("value1")) And Val(mdrow("volga")) < Val(mdrow("value2")) Then
                    'If val(mdrow("volga")) > val(mdrow("value1")) And val(mdrow("volga")) < val(mdrow("value2")) And val(mdrow("volga")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("volga")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vanna") Then
                If Val(mdrow("vanna")) > Val(mdrow("value1")) And Val(mdrow("vanna")) < Val(mdrow("value2")) Then
                    'If val(mdrow("vanna")) > val(mdrow("value1")) And val(mdrow("vanna")) < val(mdrow("value2")) And val(mdrow("vanna")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vanna")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("grossmtm") Then
                If Val(mdrow("grossmtm")) > Val(mdrow("value1")) And Val(mdrow("grossmtm")) < Val(mdrow("value2")) Then
                    'If val(mdrow("grossmtm")) > val(mdrow("value1")) And val(mdrow("grossmtm")) < val(mdrow("value2")) And val(mdrow("gamma")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("grossmtm")
                    altemp.Rows.Add(arow)
                End If
            End If

        ElseIf mdrow("opt").ToString.Trim = "Equal to" Then

            If UCase(mdrow("field")) = UCase("delta") Then
                If Val(mdrow("delta")) = Val(mdrow("value1")) Then
                    'If val(mdrow("delta")) = val(mdrow("value1")) And val(mdrow("delta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("delta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("theta") Then
                If Val(mdrow("theta")) = Val(mdrow("value1")) Then
                    'If val(mdrow("theta")) = val(mdrow("value1")) And val(mdrow("theta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("theta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vega") Then
                If Val(mdrow("vega")) = Val(mdrow("value1")) Then
                    'If val(mdrow("vega")) = val(mdrow("value1")) And val(mdrow("vega")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vega")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("gamma") Then
                If Val(mdrow("gamma")) = Val(mdrow("value1")) Then
                    'If val(mdrow("gamma")) = val(mdrow("value1")) And val(mdrow("gamma")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("gamma")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("volga") Then
                If Val(mdrow("volga")) = Val(mdrow("value1")) Then
                    'If val(mdrow("volga")) = val(mdrow("value1")) And val(mdrow("volga")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("volga")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vanna") Then
                If Val(mdrow("vanna")) = Val(mdrow("value1")) Then
                    'If val(mdrow("vanna")) = val(mdrow("value1")) And val(mdrow("vanna")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vanna")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("grossmtm") Then
                If Val(mdrow("grossmtm")) = Val(mdrow("value1")) Then
                    'If val(mdrow("grossmtm")) = val(mdrow("value1")) And val(mdrow("grossmtm")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("grossmtm")
                    altemp.Rows.Add(arow)
                End If
            End If

        ElseIf mdrow("opt").ToString.Trim = "Greater than" Then

            If UCase(mdrow("field")) = UCase("delta") Then
                If Val(mdrow("delta")) > Val(mdrow("value1")) Then
                    'If val(mdrow("delta")) > val(mdrow("value1")) And val(mdrow("delta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("delta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("theta") Then
                If Val(mdrow("theta")) > Val(mdrow("value1")) Then
                    'If val(mdrow("theta")) > val(mdrow("value1")) And val(mdrow("theta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("theta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vega") Then
                If Val(mdrow("vega")) > Val(mdrow("value1")) Then
                    'If val(mdrow("vega")) > val(mdrow("value1")) And val(mdrow("vega")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vega")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("gamma") Then
                If Val(mdrow("gamma")) > Val(mdrow("value1")) Then
                    'If val(mdrow("gamma")) > val(mdrow("value1")) And val(mdrow("gamma")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("gamma")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("volga") Then
                If Val(mdrow("volga")) > Val(mdrow("value1")) Then
                    'If val(mdrow("volga")) > val(mdrow("value1")) And val(mdrow("volga")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("volga")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vanna") Then
                If Val(mdrow("vanna")) > Val(mdrow("value1")) Then
                    'If val(mdrow("vanna")) > val(mdrow("value1")) And val(mdrow("vanna")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vanna")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("grossmtm") Then
                If Val(mdrow("grossmtm")) > Val(mdrow("value1")) Then
                    'If val(mdrow("grossmtm")) > val(mdrow("value1")) And val(mdrow("grossmtm")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("grossmtm")
                    altemp.Rows.Add(arow)
                End If
            End If

        ElseIf mdrow("opt").ToString.Trim = "Less than" Then

            If UCase(mdrow("field")) = UCase("delta") Then
                If Val(mdrow("delta")) < Val(mdrow("value1")) Then
                    'If val(mdrow("delta")) < val(mdrow("value1")) And val(mdrow("delta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("delta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("theta") Then
                If Val(mdrow("theta")) < Val(mdrow("value1")) Then
                    'If val(mdrow("theta")) < val(mdrow("value1")) And val(mdrow("theta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("theta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vega") Then
                If Val(mdrow("vega")) < Val(mdrow("value1")) Then
                    'If val(mdrow("vega")) < val(mdrow("value1")) And val(mdrow("vega")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vega")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("gamma") Then
                If Val(mdrow("gamma")) < Val(mdrow("value1")) Then
                    'If val(mdrow("gamma")) < val(mdrow("value1")) And val(mdrow("gamma")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("gamma")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("volga") Then
                If Val(mdrow("volga")) < Val(mdrow("value1")) Then
                    'If val(mdrow("volga")) < val(mdrow("value1")) And val(mdrow("volga")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("volga")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vanna") Then
                If Val(mdrow("vanna")) < Val(mdrow("value1")) Then
                    'If val(mdrow("vanna")) < val(mdrow("value1")) And val(mdrow("vanna")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vanna")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("grossmtm") Then
                If Val(mdrow("grossmtm")) < Val(mdrow("value1")) Then
                    'If val(mdrow("grossmtm")) < val(mdrow("value1")) And val(mdrow("grossmtm")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("grossmtm")
                    altemp.Rows.Add(arow)
                End If
            End If


        ElseIf mdrow("opt").ToString.Trim = "Greater than or equal to" Then

            If UCase(mdrow("field")) = UCase("delta") Then
                If Val(mdrow("delta")) >= Val(mdrow("value1")) Then
                    '  If val(mdrow("delta")) >= val(mdrow("value1")) And val(mdrow("delta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("delta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("theta") Then
                If Val(mdrow("theta")) >= Val(mdrow("value1")) Then
                    'If val(mdrow("theta")) >= val(mdrow("value1")) And val(mdrow("theta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("theta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vega") Then
                If Val(mdrow("vega")) >= Val(mdrow("value1")) Then
                    'If val(mdrow("vega")) >= val(mdrow("value1")) And val(mdrow("vega")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vega")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("gamma") Then
                If Val(mdrow("gamma")) >= Val(mdrow("value1")) Then
                    'If val(mdrow("gamma")) >= val(mdrow("value1")) And val(mdrow("gamma")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("gamma")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("volga") Then
                If Val(mdrow("volga")) >= Val(mdrow("value1")) Then
                    'If val(mdrow("volga")) >= val(mdrow("value1")) And val(mdrow("volga")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("volga")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vanna") Then
                If Val(mdrow("vanna")) >= Val(mdrow("value1")) Then
                    'If val(mdrow("vanna")) >= val(mdrow("value1")) And val(mdrow("vanna")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vanna")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("grossmtm") Then
                If Val(mdrow("grossmtm")) >= Val(mdrow("value1")) Then
                    'If val(mdrow("grossmtm")) >= val(mdrow("value1")) And val(mdrow("grossmtm")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("grossmtm")
                    altemp.Rows.Add(arow)
                End If
            End If

        ElseIf mdrow("opt").ToString.Trim = "Less than or equal to" Then

            If UCase(mdrow("field")) = UCase("delta") Then
                If Val(mdrow("delta")) <= Val(mdrow("value1")) Then
                    'If val(mdrow("delta")) <= val(mdrow("value1")) And val(mdrow("delta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("delta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("theta") Then
                If Val(mdrow("theta")) <= Val(mdrow("value1")) Then
                    'If val(mdrow("theta")) <= val(mdrow("value1")) And val(mdrow("theta")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("theta")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vega") Then
                If Val(mdrow("vega")) <= Val(mdrow("value1")) Then
                    'If val(mdrow("vega")) <= val(mdrow("value1")) And val(mdrow("vega")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vega")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("gamma") Then
                If Val(mdrow("gamma")) <= Val(mdrow("value1")) Then
                    'If val(mdrow("gamma")) <= val(mdrow("value1")) And val(mdrow("gamma")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("gamma")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("volga") Then
                If Val(mdrow("volga")) <= Val(mdrow("value1")) Then
                    'If val(mdrow("volga")) <= val(mdrow("value1")) And val(mdrow("volga")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("volga")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("vanna") Then
                If Val(mdrow("vanna")) <= Val(mdrow("value1")) Then
                    'If val(mdrow("vanna")) <= val(mdrow("value1")) And val(mdrow("vanna")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("vanna")
                    altemp.Rows.Add(arow)
                End If
            ElseIf UCase(mdrow("field")) = UCase("grossmtm") Then
                If Val(mdrow("grossmtm")) <= Val(mdrow("value1")) Then
                    'If val(mdrow("grossmtm")) <= val(mdrow("value1")) And val(mdrow("grossmtm")) <> 0 Then
                    arow = altemp.NewRow
                    arow("comp_script") = mdrow("comp_script")
                    arow("opt") = mdrow("opt")
                    arow("field") = mdrow("field")
                    arow("value1") = mdrow("value1")
                    arow("value2") = mdrow("value2")
                    arow("current") = mdrow("grossmtm")
                    altemp.Rows.Add(arow)
                End If
            End If
        End If

        If altemp.Rows.Count > 0 Then
            mdrow("status") = "1"
            Dim alert As New alert
            alert.temptable = altemp.Copy
            'alert.temptable = altemp
            alert.comp = mdrow("comp_script")
            alert.fi = mdrow("field")
            alert.uid = mdrow("uid")
            'If mdrow("status") = "0" Then
            '    alert.Show()
            'End If
            alert.BringToFront()
            alert.Show()

        End If
        'For Each drow As DataRow In alerttable.Select("comp_script='" & comp & "' and field ='" & field & "'")
        '    drow("status") = 0
        'Next
    End Sub
    Private Sub cmdalert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdalert.Click
        'If UCase(cmdalert.Text) = "START ALERT" Then
        '    alert_comp()
        '    timer1.Start()
        '    cmdalert.Text = "Stop Alert"
        'Else
        '    timer1.Stop()
        '    cmdalert.Text = "Start Alert"
        'End If
        If AppLicMode = "SERLIC" Then
            If bool_IsServerConnected = False Then Exit Sub
        End If
        Call openposition()
        Call searchcompany()
        MDI.ToolStripcompanyCombo.Text = compname
    End Sub
    Private Sub alertentry()
        timer1.Stop()
        'cmdalert.Text = "Start Alert"

        Dim alert As New alert_entry
        alert.ShowDialog()
        alert_comp()
        timer1.Start()
        'cmdalert.Text = "Stop Alert"
    End Sub
    REM Show Or Set Alert
    Private Sub cmdalertentry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        alertentry()
    End Sub
#End Region
#Region "All Summary"
    ''' <summary>
    ''' allcompany
    ''' </summary>
    ''' <remarks>This method call to refill Gtbl_Summary_Analysis table which use to bind with Analysis Summary datagrid</remarks>
    Private Sub allcompany()
        REM Add Volga,Vanna Greeks in Summary(F9) Button Click show in grid

        Dim dt As New DataTable
        Dim dv As DataView
        dt = objTrad.Comapany_summary
        'If zero_analysis <> 0 Then
        'dv = New DataView(maintable, "units <> 0", "company", DataViewRowState.CurrentRows)
        'Else
        dv = New DataView(maintable, "", "company", DataViewRowState.CurrentRows)
        'End If
        acomp = New DataTable
        acomp = dv.ToTable
        acomp.Columns.Add("deltaRs", GetType(Double))
        acomp.AcceptChanges()
        Dim row As DataRow
        Gtbl_Summary_Analysis.Rows.Clear()
        row = Gtbl_Summary_Analysis.NewRow
        row("company") = "Total"
        row("delta") = 0
        row("theta") = 0
        row("gamma") = 0
        row("vega") = 0
        row("volga") = 0
        row("vanna") = 0
        row("deltaRs") = 0
        row("grossmtm") = 0
        Gtbl_Summary_Analysis.Rows.Add(row)
        For Each drow As DataRow In dt.Rows
            Dim trow() As DataRow = acomp.Select("company='" & drow("company") & "'")
            If trow.Length > 0 Then
                row = Gtbl_Summary_Analysis.NewRow
                row("company") = drow("company")
                row("delta") = 0
                row("theta") = 0
                row("gamma") = 0
                row("vega") = 0
                row("volga") = 0
                row("vanna") = 0
                row("deltaRs") = 0
                row("grossmtm") = 0
                Gtbl_Summary_Analysis.Rows.Add(row)
            End If
        Next
        Gtbl_Summary_Analysis.AcceptChanges()
    End Sub
    ''' <summary>
    ''' Cal_DeltaGammaVegaThetaSummary
    ''' </summary>
    ''' <remarks>This method call to calculate Greeks value for Analysis Summary Form</remarks>
    Private Sub Cal_DeltaGammaVegaThetaSummary()
        REM Add Volga,Vanna in Alert Screen for give alert for volga ,vanna changes for given criteria
        Dim ltppr As Double = 0
        Dim ltppr1 As Double = 0
        Dim fltppr As Double = 0
        Dim mt As Double = 0
        Dim isfut As Boolean = False
        Dim iscall As Boolean = False
        Dim alltp As Double = 0
        Dim token As Long
        Dim token1 As Long
        Dim togrossmtm As Double = 0
        Dim prgrossmtm As Double = 0
        Dim prExp As Double = 0
        Dim toExp As Double = 0
        Dim texp As Double = 0
        For Each mrow As DataRow In Gtbl_Summary_Analysis.Select("company <> 'Total'")
            If mrow("Company") = compname Then
                'mrow("Company") = compname
                mrow.BeginEdit()
                mrow("delta") = VarDeltaval
                mrow("theta") = VarThetaval
                mrow("gamma") = VarGammaval
                mrow("vega") = VarVegaval
                mrow("volga") = VarVolgaval
                mrow("vanna") = VarVannaval
                mrow("deltaRS") = VarDeltaval * VarDeltaRS
                mrow("grossmtm") = VarGrossmtm
                mrow.EndEdit()
                Continue For
            Else
                Dim DTTmp As DataTable = New DataView(acomp, "company='" & mrow("company") & "' AND mdate Is Not Null", "mdate", DataViewRowState.CurrentRows).ToTable(True, "ftoken")
                Dim VarCompFToken As Long
                If DTTmp.Rows.Count > 0 Then
                    VarCompFToken = DTTmp.Rows(0)("ftoken")
                End If
                For Each drow As DataRow In acomp.Select("company='" & mrow("company") & "'")

                    If CBool(drow("isliq")) = True Then
                        token = CLng(drow("tokanno"))
                        token1 = CLng(drow("token1"))
                    Else
                        token = CLng(drow("tokanno"))
                        token1 = 0
                    End If
                    If drow("iscurrency") = False Then
                        fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                    Else
                        fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                    End If

                    If ltpprice.Contains(token) Then

                        If token1 > 0 Then
                            ltppr = Val(ltpprice(token))
                            ltppr1 = Val(ltpprice(token1))
                        Else
                            ltppr = Val(ltpprice(token))
                            ltppr1 = 0
                        End If
                        mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                        If Now.Date = CDate(drow("mdate")).Date Then
                            mt = 0.5
                        End If
                        If drow("cp") = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        If Val(drow("units")) <> 0 And (drow("cp") = "C" Or drow("cp") = "P") Then
                            If fltppr > 0 Then
                                CalDataalert1(fltppr, Val(drow("strikes").ToString), ltppr, ltppr1, mt, iscall, isfut, drow, Val(drow("units").ToString))
                            Else
                                If veqdel > 0 Then
                                    CalDataalert1(veqdel, Val(drow("strikes").ToString), ltppr, ltppr1, mt, iscall, isfut, drow, Val(drow("units").ToString))
                                    '  drow("grossmtm") = Math.Round((val(drow("units")) * (val(drow("last")) - val(drow("traded")))), 2)
                                End If
                            End If
                        End If
                    End If

                    REM Change BY Alpesh For Currency
                    If Currltpprice.Contains(token) Then
                        'If token = 1026 Then
                        '    MsgBox("A")
                        'End If
                        If token1 > 0 Then
                            ltppr = Val(Currltpprice(token))
                            ltppr1 = Val(Currltpprice(token1))
                        Else
                            ltppr = Val(Currltpprice(token))
                            ltppr1 = 0
                        End If
                        mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                        If Now.Date = CDate(drow("mdate")).Date Then
                            mt = 0.5
                        End If
                        If drow("cp") = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        If Val(drow("units")) <> 0 And (drow("cp") = "C" Or drow("cp") = "P") Then
                            If fltppr > 0 Then
                                'If drow("Script").ToString.Contains("Eurinr") = True Then MsgBox("a")
                                'Debug.Print(drow("Script"))
                                CalDataalert1(fltppr, Val(drow("strikes").ToString), ltppr, ltppr1, mt, iscall, isfut, drow, Val(drow("units").ToString))
                            Else
                                If veqdel > 0 Then
                                    'Debug.Print(drow("Script"))
                                    CalDataalert1(veqdel, Val(drow("strikes").ToString), ltppr, ltppr1, mt, iscall, isfut, drow, Val(drow("units").ToString))
                                    '  drow("grossmtm") = Math.Round((val(drow("units")) * (val(drow("last")) - val(drow("traded")))), 2)
                                End If
                            End If
                        End If
                    End If


                    Dim VarCompFLTP As Double = 0
                    If drow("iscurrency") = False Then
                        VarCompFLTP = Val(fltpprice(VarCompFToken))

                    Else
                        VarCompFLTP = Val(Currfltpprice(VarCompFToken))
                    End If
                    If IsDBNull(drow("deltaval")) Then
                        drow("deltaval") = 0
                        acomp.AcceptChanges()
                    End If

                    'drow("deltaRS") = Val(drow("deltaval") * VarCompFLTP)
                    drow("deltaRS") = Val(IIf(IsDBNull(drow("deltaval")) = True, 0, drow("deltaval")) * VarCompFLTP)
                    'fltppr = Val(Ght_fltpprice(CInt(DRowScript("ftokan"))))
                    'If DRowScript("fltp") <> fltppr Then
                    '    DRowScript("fltp") = fltppr
                    '    VarFLTPChange = True
                    'Else
                    '    VarFLTPChange = False
                    'End If
                    '  drow("deltaRS") = Val(drow("units")) * Val(drow("traded"))
                    If drow("cp") = "F" Then
                        If drow("iscurrency") = False Then
                            alltp = Val(fltpprice(CLng(drow("tokanno"))))
                        Else
                            alltp = Val(Currfltpprice(CLng(drow("tokanno"))))
                        End If
                        drow("last") = alltp
                        If Val(drow("units")) = 0 Then
                            drow("grossmtm") = -Val(drow("traded"))
                        Else
                            drow("grossmtm") = (Val(drow("units")) * (Val(alltp) - Val(drow("traded"))))
                        End If
                    ElseIf drow("cp") = "E" Then
                        alltp = Val(eltpprice(CLng(drow("tokanno"))))
                        drow("last") = alltp
                        If Val(drow("units")) = 0 Then
                            drow("grossmtm") = -Val(drow("traded"))
                        Else
                            drow("grossmtm") = (Val(drow("units")) * (Val(alltp) - Val(drow("traded"))))
                        End If
                    Else
                        If Val(drow("units")) = 0 Then
                            drow("grossmtm") = -Val(drow("traded"))
                        Else
                            drow("grossmtm") = (Val(drow("units")) * (Val(drow("last")) - Val(drow("traded"))))
                        End If
                    End If
                Next
            End If

            'mrow("delta") = 0
            'mrow("theta") = 0
            'mrow("vega") = 0
            'mrow("gamma") = 0
            'mrow("grossmtm") = 0
            isfut = True


            If mrow("company") = "Total" Then Continue For
            mrow.BeginEdit()
            mrow("delta") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(deltaval)", "company='" & mrow("company") & "'")) = True, 0, acomp.Compute("sum(deltaval)", "company='" & mrow("company") & "'")), Deltastr_Val))
            mrow("deltaRS") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(deltaRS)", "company='" & mrow("company") & "'")) = True, 0, acomp.Compute("sum(deltaRS)", "company='" & mrow("company") & "'")), Deltastr_Val))
            mrow("theta") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(thetaval)", "company='" & mrow("company") & "'")) = True, 0, acomp.Compute("sum(thetaval)", "company='" & mrow("company") & "'")), Thetastr_Val))
            mrow("vega") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(vgval)", "company='" & mrow("company") & "'")) = True, 0, acomp.Compute("sum(vgval)", "company='" & mrow("company") & "'")), Vegastr_Val))
            mrow("gamma") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(gmval)", "company='" & mrow("company") & "'")) = True, 0, acomp.Compute("sum(gmval)", "company='" & mrow("company") & "'")), Gammastr_Val))
            mrow("volga") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(volgaval)", "company='" & mrow("company") & "'")) = True, 0, acomp.Compute("sum(volgaval)", "company='" & mrow("company") & "'")), Volgastr_Val))
            mrow("vanna") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(vannaval)", "company='" & mrow("company") & "'")) = True, 0, acomp.Compute("sum(vannaval)", "company='" & mrow("company") & "'")), Vannastr_Val))
            mrow("grossmtm") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(grossmtm)", "company='" & mrow("company") & "'")) = True, 0, acomp.Compute("sum(grossmtm)", "company='" & mrow("company") & "'")), GrossMTMstr))
            mrow.EndEdit()
        Next

        REM  Show Total of Delta is displayed
        If Gtbl_Summary_Analysis.Rows.Count > 0 Then
            With Gtbl_Summary_Analysis.Rows(0)
                .Item("deltaRS") = Math.Round(Val(Gtbl_Summary_Analysis.Compute("sum(deltaRS)", "company<>'Total'").ToString), 2)
                .Item("delta") = Math.Round(Val(Gtbl_Summary_Analysis.Compute("sum(delta)", "company<>'Total'").ToString), 2)
                .Item("theta") = Math.Round(Val(Gtbl_Summary_Analysis.Compute("sum(theta)", "company<>'Total'").ToString), 2)
                .Item("vega") = Math.Round(Val(Gtbl_Summary_Analysis.Compute("sum(vega)", "company<>'Total'").ToString), 2)
                .Item("gamma") = Math.Round(Val(Gtbl_Summary_Analysis.Compute("sum(gamma)", "company<>'Total'").ToString), 2)
                .Item("volga") = Math.Round(Val(Gtbl_Summary_Analysis.Compute("sum(volga)", "company<>'Total'").ToString), 2)
                .Item("vanna") = Math.Round(Val(Gtbl_Summary_Analysis.Compute("sum(vanna)", "company<>'Total'").ToString), 2)
                .Item("grossmtm") = Math.Round(Val(Gtbl_Summary_Analysis.Compute("sum(grossmtm)", "company<>'Total'").ToString), 2)
            End With
        End If
        'Gtbl_Summary_Analysis.AcceptChanges()
        flgcalsummarythrdstart = False
    End Sub
    ''' <summary>
    ''' cal_comp
    ''' </summary>
    ''' <remarks>This method call to Calcu. Company Summary </remarks>
    Private Sub cal_comp()
        For Each mrow As DataRow In Gtbl_Summary_Analysis.Select("company <> 'Total'")
            If mrow("company") = compname Then
                mrow("company") = compname
                mrow("delta") = Val(VarDeltaval)
                For Each drow As DataRow In currtable.Select("company='" & compname & "' and cp = 'F' or cp='XX' ")
                    If drow("cp") = "F" OrElse drow("cp") = "XX" Then 'equity
                        mrow("deltaRS") = (VarDeltaval) * drow("last")
                    End If
                Next
                mrow("gamma") = Val(VarGammaval)
                mrow("vega") = Val(VarVegaval)
                mrow("theta") = Val(VarThetaval)
                mrow("grossmtm") = Val(VarGrossmtm)

            Else
                ' If (ltpprice.Count > 0) Then
                Dim ltppr As Double = 0
                Dim ltppr1 As Double = 0
                Dim fltppr As Double = 0
                Dim mt As Double = 0
                Dim isfut As Boolean = False
                Dim iscall As Boolean = False
                Dim alltp As Double = 0
                Dim token As Long
                Dim token1 As Long
                Dim togrossmtm As Double = 0
                Dim prgrossmtm As Double = 0
                Dim prExp As Double = 0
                Dim toExp As Double = 0
                Dim texp As Double = 0
                'mrow("delta") = 0
                'mrow("theta") = 0
                'mrow("vega") = 0
                'mrow("gamma") = 0
                'mrow("grossmtm") = 0
                'mrow("expense") = 0
                'mrow("current") = 0
                'mrow("projMTM") = 0
                isfut = True

                For Each drow As DataRow In acomp.Select("company='" & mrow("company") & "'")
                    drow("grossmtm") = 0
                    If CBool(drow("isliq")) = True Then
                        token = CLng(drow("tokanno"))
                        token1 = CLng(drow("token1"))
                    Else
                        token = CLng(drow("tokanno"))
                        token1 = 0
                    End If
                    If ltpprice.Contains(token) Then
                        fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                        If token1 > 0 Then
                            ltppr = Val(ltpprice(token))
                            ltppr1 = Val(ltpprice(token1))
                        Else
                            ltppr = Val(ltpprice(token))
                            ltppr1 = 0
                        End If
                        mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                        If Now.Date = CDate(drow("mdate")).Date Then
                            mt = 0.5
                        End If
                        If drow("cp") = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        If Val(drow("units")) <> 0 And drow("cp") <> "E" Then
                            If fltppr > 0 Then
                                CalDataalert1(fltppr, Val(drow("strikes").ToString), ltppr, ltppr1, mt, iscall, isfut, drow, Val(drow("units").ToString))
                            Else
                                If veqdel > 0 Then
                                    CalDataalert1(veqdel, Val(drow("strikes").ToString), ltppr, ltppr1, mt, iscall, isfut, drow, Val(drow("units").ToString))
                                    '  drow("grossmtm") = Math.Round((val(drow("units")) * (val(drow("last")) - val(drow("traded")))), 2)
                                End If
                            End If
                        End If
                    End If
                    If drow("cp") = "F" Then
                        alltp = Val(fltpprice(CLng(drow("tokanno"))))
                        drow("last") = alltp
                        If Val(drow("units")) = 0 Then
                            drow("grossmtm") = -Val(drow("traded"))
                        Else
                            drow("grossmtm") = (Val(drow("units")) * (Val(alltp) - Val(drow("traded"))))
                        End If
                    ElseIf drow("cp") = "E" Then
                        alltp = Val(eltpprice(CLng(drow("tokanno"))))
                        drow("last") = alltp
                        If Val(drow("units")) = 0 Then
                            drow("grossmtm") = -Val(drow("traded"))
                        Else
                            drow("grossmtm") = (Val(drow("units")) * (Val(alltp) - Val(drow("traded"))))
                        End If
                    Else
                        If Val(drow("units")) = 0 Then
                            drow("grossmtm") = -Val(drow("traded"))
                        Else
                            drow("grossmtm") = (Val(drow("units")) * (Val(drow("last")) - Val(drow("traded"))))
                        End If
                    End If
                    'mrow("grossmtm") = drow("grossmtm")
                    drow("deltaRS") = Val(drow("units")) * Val(drow("traded"))
                    'mrow("deltaRS") = drow("deltaRS")
                    'mrow("current") = (mrow("grossmtm") + mrow("expense"))
                    If flgSummary = True Then
                        ' Call summary_Exp(drow("company"), prExp, toExp)

                        mrow("expense") = -Format(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & drow("company") & "'").ToString), Expensestr)

                        'mrow("expense") = -Format(Val(prebal.Compute("sum(tot)", "company='" & drow("company") & "'").ToString), Expensestr)

                        '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@222
                        REM 2:proj.MTM
                        For Each drow1 As DataRow In GdtFOTrades.Select("script='" & drow("script") & "'  and entry_date='" & Now.Date & "' And company='" & compname & "'")
                            togrossmtm += (Val(drow1("qty")) * (Val(drow("last")) - Val(drow1("rate"))))
                        Next
                        For Each drow1 As DataRow In GdtEQTrades.Select("script='" & drow("script") & "' and entry_date='" & Now.Date & "' And company='" & compname & "'")
                            togrossmtm += (Val(drow1("qty")) * (Val(drow("last")) - Val(drow1("rate"))))
                        Next
                        If Val(drow("units")) <> 0 Then
                            prgrossmtm += (Val(drow("units")) * (Val(drow("last")) - Val(drow("traded"))))
                        Else
                            prgrossmtm += (-Val(drow("traded")))
                        End If

                        If drow("cp") = "E" Then
                            ' texp = texp + Val(Val(Math.Abs(drow("units"))) * Val(drow("last")) * ndbs / ndbsp)
                            texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entry_date = #" & fDate(Now.Date) & "# and qty > 0").ToString) * Val(drow("last")) * ndbs / ndbsp)
                            texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entry_date = #" & fDate(Now.Date) & "# and qty < 0").ToString) * Val(drow("last")) * ndbl / ndblp)
                            texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entry_date <> #" & fDate(Now.Date) & "# and qty > 0").ToString) * Val(drow("last")) * dbs / dbsp)
                            texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entry_date <> #" & fDate(Now.Date) & "# and qty < 0").ToString) * Val(drow("last")) * dbl / dblp)

                        ElseIf drow("cp") = "F" Then
                            If Val(drow("units")) > 0 Then
                                texp = texp + Val(Val(Math.Abs(drow("units"))) * Val(drow("last")) * futs / futsp)
                            Else
                                texp = texp + Val(Val(Math.Abs(drow("units"))) * Val(drow("last")) * futl / futlp)
                            End If
                        Else
                            If Val(spl) <> 0 Then
                                If Val(drow("units")) > 0 Then
                                    texp = texp + (Val((Math.Abs(Val(drow("units"))) * (Val(drow("strikes")) + Val(drow("last")))) * sps) / spsp)
                                Else
                                    texp = texp + (Val((Math.Abs(Val(drow("units"))) * (Val(drow("strikes")) + Val(drow("last")))) * spl) / splp)
                                End If
                            Else
                                If Val(drow("units")) > 0 Then
                                    texp = texp + (Val((Math.Abs(Val(drow("units"))) * Val(drow("last"))) * pres) / presp)
                                Else
                                    texp = texp + (Val((Math.Abs(Val(drow("units"))) * Val(drow("last"))) * prel) / prelp)
                                End If
                            End If
                        End If
                        Dim txtprsqmtm1 As Double = prgrossmtm - togrossmtm + prExp
                        Dim txttosqmtm1 As Double = togrossmtm + toExp

                        mrow("projMTM") = Format(txtprsqmtm1 + txttosqmtm1 - texp, SquareMTMstr)

                    End If

                Next
                'If mrow("company") = "Total" Then Continue For
                '  Try
                mrow("delta") = Val(Format(acomp.Compute("sum(deltaval)", "company='" & mrow("company") & "'"), Deltastr_Val))
                mrow("deltaRS") = Val(Format(acomp.Compute("sum(deltaRS)", "company='" & mrow("company") & "'"), Deltastr_Val))

                mrow("theta") = Val(Format(acomp.Compute("sum(thetaval)", "company='" & mrow("company") & "'"), Thetastr_Val))
                mrow("vega") = Val(Format(acomp.Compute("sum(vgval)", "company='" & mrow("company") & "'"), Vegastr_Val))
                mrow("gamma") = Val(Format(acomp.Compute("sum(gmval)", "company='" & mrow("company") & "'"), Gammastr_Val))
                mrow("volga") = Val(Format(acomp.Compute("sum(volgaval)", "company='" & mrow("company") & "'"), Volgastr_Val))
                mrow("vanna") = Val(Format(acomp.Compute("sum(vannaval)", "company='" & mrow("company") & "'"), Vannastr_Val))
                mrow("grossmtm") = Val(Format(acomp.Compute("sum(grossmtm)", "company='" & mrow("company") & "'"), GrossMTMstr))
                mrow("current") = Val(Format(mrow("grossmtm") + mrow("expense"), NetMTMstr))
                '   Catch ex As Exception
                ' MsgBox(ex.Message)
                '  End Try
            End If
        Next

        With Gtbl_Summary_Analysis.Rows(0)
            .Item("deltaRS") = Math.Round(Gtbl_Summary_Analysis.Compute("sum(deltaRS)", "company<>'Total'"), 2)
            .Item("theta") = Math.Round(Gtbl_Summary_Analysis.Compute("sum(theta)", "company<>'Total'"), 2)
            .Item("vega") = Math.Round(Gtbl_Summary_Analysis.Compute("sum(vega)", "company<>'Total'"), 2)
            .Item("gamma") = Math.Round(Gtbl_Summary_Analysis.Compute("sum(gamma)", "company<>'Total'"), 2)
            .Item("volga") = Math.Round(Gtbl_Summary_Analysis.Compute("sum(volga)", "company<>'Total'"), 2)
            .Item("vanna") = Math.Round(Gtbl_Summary_Analysis.Compute("sum(vanna)", "company<>'Total'"), 2)
            .Item("grossmtm") = Math.Round(Gtbl_Summary_Analysis.Compute("sum(grossmtm)", "company<>'Total'"), 2)
            .Item("current") = Math.Round(Gtbl_Summary_Analysis.Compute("sum(current)", "company<>'Total'"), 2)
            .Item("expense") = Math.Round(Gtbl_Summary_Analysis.Compute("sum(expense)", "company<>'Total'"), 2)
            .Item("projMTM") = Math.Round(Gtbl_Summary_Analysis.Compute("sum(projMTM)", "company<>'Total'"), 2)
        End With
        totgmtm = Val(IIf(IsDBNull(Gtbl_Summary_Analysis.Compute("sum(grossmtm)", "")), 0, Gtbl_Summary_Analysis.Compute("sum(grossmtm)", "")))
        flgcalsummarythrdstart = False
    End Sub

    ''' <summary>
    ''' CalDataalert1
    ''' </summary>
    ''' <param name="futval"></param>
    ''' <param name="stkprice"></param>
    ''' <param name="cpprice"></param>
    ''' <param name="cpprice1"></param>
    ''' <param name="mT"></param>
    ''' <param name="mIsCall"></param>
    ''' <param name="mIsFut"></param>
    ''' <param name="drow"></param>
    ''' <param name="qty"></param>
    ''' <remarks>This method call to calculate Alert and display Alert popup</remarks>
    Private Sub CalDataalert1(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal cpprice1 As Double, ByVal mT As Integer, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal drow As DataRow, ByVal qty As Double)
        Try
            Dim mDelta As Double
            Dim mGama As Double
            Dim mVega As Double
            Dim mThita As Double
            Dim mVolga As Double
            Dim mVanna As Double
            Dim mRah As Double


            Dim mVolatility As Double
            Dim tmpcpprice As Double = 0
            Dim tmpcpprice1 As Double = 0
            Dim mD1 As Double
            Dim mD2 As Double

            tmpcpprice = cpprice
            tmpcpprice1 = cpprice1

            mDelta = 0
            mGama = 0
            mVega = 0
            mThita = 0
            mVolga = 0
            mVanna = 0
            mRah = 0
            mVolga = 0
            mVanna = 0
            mD1 = 0
            mD2 = 0
            Dim _mt As Double

            If mT = 0 Then
                _mt = 0.00001
            Else
                _mt = (mT) / 365
            End If
            If stkprice = 0 Then  'future volatility =0
                mVolatility = 0
            ElseIf tmpcpprice1 = 0 Then
                mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
            Else
                If mIsCall = True Then
                    mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, False, mIsFut, 0, 6)
                Else
                    mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, True, mIsFut, 0, 6)
                End If
            End If

            ' Try
            If mVolatility = 0 Then
                mVolatility = 0.1
                mDelta = 1
            Else
                'mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))
                mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))
            End If



            mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))

            mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))

            mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))

            mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))

            'Using Function 
            mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
            mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

            mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
            mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)


            'drow("delta") = Math.Round(val(drow("delta")) + (val(Math.Round(mDelta, roundDelta)) * qty), roundDelta)
            'drow("theta") = Math.Round(val(drow("theta")) + (val(Math.Round(mThita, roundTheta)) * qty), roundTheta)
            'drow("vega") = Math.Round(val(drow("vega")) + (val(Math.Round(mVega, roundVega)) * qty), roundVega)
            'drow("gamma") = Math.Round(val(drow("gamma")) + (val(Math.Round(mGama, roundGamma)) * qty), roundGamma)
            drow("last") = cpprice
            drow("deltaval") = Val(Math.Round(mDelta * qty, roundDelta_Val))
            drow("vgval") = Val(Math.Round(mVega * qty, roundVega_Val))
            drow("gmval") = Val(Math.Round(mGama * qty, roundGamma_Val))

            drow("volgaval") = Val(Math.Round(mVolga * qty, roundVolga_Val))
            drow("vannaval") = Val(Math.Round(mVanna * qty, roundVanna_Val))

            If mThita < 0 Then
                If Math.Abs(mThita) > cpprice Then
                    mThita = -Math.Round(cpprice, roundTheta)
                    'drow("thetaval") = Math.Round(mThita* qty, roundTheta_Val)
                End If
            Else
                If Math.Abs(mThita) > cpprice Then
                    mThita = Math.Round(cpprice, roundTheta)
                    'drow("thetaval") = Math.Round(mThita * qty, roundTheta_Val)
                End If
            End If
            drow("thetaval") = Val(Math.Round(mThita * qty, roundTheta_Val))


            'Catch ex As Exception
            '    MsgBox(ex.ToString)
            ' End Try

        Catch ex As Exception
            MsgBox("futval = " & futval & vbCrLf _
             & "stkprice = " & stkprice & vbCrLf _
             & "ltppr = " & cpprice & vbCrLf _
             & "ltppr1 = " & cpprice1 & vbCrLf _
             & "qty = " & qty & vbCrLf _
             & "script = " & drow("script") & vbCrLf & ex.ToString)
        End Try


    End Sub
    ''' <summary>
    ''' cmbSummary
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to display Analysis Summary Form</remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        flgSummary = True
        alertmsg = False
        '  Timer_Calculation_Tick(sender, e)
        Call summary()
        If tbcomp.TabPages.Count = 0 Then Exit Sub
        If tbcomp.SelectedTab.Name <> compname Then
            tbcomp.SelectTab(compname)
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If alertmsg = True Then
            'cal_comp()
        Else
            Timer2.Stop()
        End If
    End Sub
    ''' <summary>
    ''' summary
    ''' </summary>
    ''' <remarks>This method call to display Analysis Summary Form</remarks>
    Public Sub summary()
        REM Add Volga,Vanna Greeks in Summary(F9) Button Click show in grid
        Try
            If alertmsg = False Then
                VolHedge.allcompany.Close()
                flgSummary = True
                Call Init_Gtbl_Summary_Analysis()
                allcompany()
                If thrworking = False OrElse thrworking_future_only = False Then
                    VarDeltaval = Val(txttdelval.Text)
                    VarGammaval = Val(txttgmval.Text)
                    VarThetaval = Val(txttthval.Text)
                    VarVegaval = Val(txttvgval.Text)
                    VarVolgaval = Val(txttvolgaval.Text)
                    VarVannaval = Val(txttvannaval.Text)
                    VarGrossmtm = Val(txttotGmtm.Text)
                    VarDeltaRS = Val(txtrate.Text)
                    REM This Function Commented By Viral 14-July-2011 
                    'Because Of When Calculate Currency Fo&Op  Thread Function And here Defined Code Both Conflict 
                    Call Cal_DeltaGammaVegaThetaSummary()
                End If
                ' cal_comp()
                '  ac.temptable = allcomp
                'ac.totmtm = totgmtm 'val(IIf(IsDBNull(allcomp.Compute("sum(grossmtm)", "")), 0, allcomp.Compute("sum(grossmtm)", "")))
                If flgcalsummarythrdstart = False Then
                    'Godj_Frm_Analysis_Summary = New allcompany
                    'ac.Show()
                    VolHedge.allcompany.Show()
                Else
                    Exit Try
                End If
                Timer2.Start()
                alertmsg = True
            Else
                flgSummary = False
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Private Function summary_Exp(ByVal compname As String, ByRef prExp As Double, ByRef toExp As Double)
        'Expense @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        REM 1: Calculate Expense


        Dim interast As Double = 0
        If UCase(fifo_avg) = "FALSE" Then
            Dim dv As DataView = New DataView(GdtFOTrades)
            Dim stexp, stexp1, ndst, dst, expto As Double
            'calculate previous balance

            prExp = -Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & compname & "'").ToString)
            'for today expense$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

            'Equity ##################################################################
            stexp = 0
            stexp1 = 0
            dst = 0
            ndst = 0
            expto = 0
            stexp = Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "company='" & compname & "' and qty > 0 and entry_date = #" & fDate(dttoday.Value.Date) & "#").ToString), 2)
            stexp1 = Math.Abs(Math.Round(Val(GdtEQTrades.Compute("sum(tot)", "company='" & compname & "' and qty < 0 and entry_date =  #" & fDate(dttoday.Value.Date) & "#").ToString), 2))
            dst = stexp - stexp1
            If dst > 0 Then
                ndst = stexp1
                expto += dst * ndbl / ndblp
                expto += stexp1 * ndbs / ndbsp
                expto += stexp1 * ndbl / ndblp
            Else
                ndst = stexp
                dst = -dst
                expto += dst * dbs / dbsp
                expto += stexp * ndbl / ndblp
                expto += stexp * ndbs / ndbsp
            End If
            'Futre #################################################################

            stexp = 0
            stexp1 = 0

            stexp = Val(GdtFOTrades.Compute("sum(tot)", "cp not in ('C','P','E') and company='" & compname & "' and qty > 0 and entry_date =  #" & fDate(dttoday.Value.Date) & "#").ToString)
            stexp1 = Math.Abs(Val(GdtFOTrades.Compute("sum(tot)", "cp not in ('C','P','E')  and company='" & compname & "' and qty < 0 and entry_date =  #" & fDate(dttoday.Value.Date) & "#").ToString))

            expto += stexp * futl / futlp
            expto += stexp1 * futs / futsp

            'Option ####################################################################

            If Val(spl) <> 0 Then
                stexp = 0
                stexp1 = 0
                stexp = Val(GdtFOTrades.Compute("sum(tot2)", "cp  in ('C','P') and company='" & compname & "' and qty > 0 and entry_date =  #" & fDate(dttoday.Value.Date) & "#").ToString)
                stexp1 = Math.Abs(Val(GdtFOTrades.Compute("sum(tot2)", "cp  in ('C','P') and company='" & compname & "' and qty < 0 and entry_date =  #" & fDate(dttoday.Value.Date) & "#").ToString))

                expto += stexp * spl / splp
                expto += stexp1 * sps / spsp
            Else
                stexp = 0
                stexp1 = 0
                stexp = Val(GdtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "' and qty > 0 and entry_date =  #" & fDate(dttoday.Value.Date) & "#").ToString)
                stexp1 = Math.Abs(Val(GdtFOTrades.Compute("sum(tot)", "cp  in ('C','P') and company='" & compname & "' and qty < 0 and entry_date =  #" & fDate(dttoday.Value.Date) & "#").ToString))

                expto += stexp * prel / prelp
                expto += stexp1 * pres / presp
            End If
            toExp = -expto
        End If

        ' REM 1:End
        ' @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

    End Function
#End Region

    ''' <summary>
    ''' save_applyMt
    ''' </summary>
    ''' <remarks>This method call to Update Grid according to Offline value enter For MainTable</remarks>
    Private Sub save_applyMt(ByVal CmpName As String)
        Try
            Dim ltppr As Double = 0
            Dim ltppr1 As Double = 0
            Dim fltppr As Double = 0
            Dim mt As Double = 0
            Dim mmt As Double = 0
            Dim isfut As Boolean = False
            Dim iscall As Boolean = False

            Dim token As Long
            Dim token1 As Long
            Dim dt As Date

            Dim cal_vol As Double = 0
            Dim put_vol As Double = 0

            For Each drow As DataRow In maintable.Select("company='" & CmpName & "'", "strikes,cp,company")
                If drow("cp") = "E" Then 'equity
                    If thrworking = True Then
                        drow("last") = Val(eltpprice(CLng(drow("tokanno")))) 'get LTP of equity
                    Else
                        drow("last") = Val(txteqrate.Text)
                    End If
                ElseIf drow("CP") = "F" Then 'Future
                    If pan11 = True Then 'for first expiry date
                        If date1 = CDate(drow("mdate")).Date And Val(txtfut1.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currfltpprice(CLng(drow("tokanno"))) = txtfut1.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("tokanno")), txtfut1.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltpprice(CLng(drow("tokanno"))) = txtfut1.Text
                                Else
                                    fltpprice.Add(CLng(drow("tokanno")), txtfut1.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            End If
                        End If
                    End If
                    If pan21 = True Then
                        If date2 = CDate(drow("mdate")).Date And Val(txtfut2.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currfltpprice(CLng(drow("tokanno"))) = txtfut2.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("tokanno")), txtfut2.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltpprice(CLng(drow("tokanno"))) = txtfut2.Text
                                Else
                                    fltpprice.Add(CLng(drow("tokanno")), txtfut2.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            End If

                        End If
                    End If
                    If pan31 = True Then
                        If date3 = CDate(drow("mdate")).Date And Val(txtfut3.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currfltpprice(CLng(drow("tokanno"))) = txtfut3.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("tokanno")), txtfut3.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltpprice(CLng(drow("tokanno"))) = txtfut3.Text
                                Else
                                    fltpprice.Add(CLng(drow("tokanno")), txtfut3.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True

                            End If
                        End If
                    End If
                    If VarIsCurrency = True Then
                        drow("last") = Val(Currfltpprice(CLng(drow("tokanno"))))
                    Else
                        drow("last") = Val(fltpprice(CLng(drow("tokanno"))))
                    End If

                Else
                    If pan11 = True Then
                        If date1 = CDate(drow("fut_mdate")).Date And Val(txtfut1.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                                    Currfltpprice(CLng(drow("ftoken"))) = txtfut1.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("ftoken")), txtfut1.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("ftoken"))) Then
                                    fltpprice(CLng(drow("ftoken"))) = txtfut1.Text
                                Else
                                    fltpprice.Add(CLng(drow("ftoken")), txtfut1.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            End If
                        End If
                    End If
                    If pan21 = True Then
                        If date2 = CDate(drow("fut_mdate")).Date And Val(txtfut2.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                                    Currfltpprice(CLng(drow("ftoken"))) = txtfut2.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("ftoken")), txtfut2.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("ftoken"))) Then
                                    fltpprice(CLng(drow("ftoken"))) = txtfut2.Text
                                Else
                                    fltpprice.Add(CLng(drow("ftoken")), txtfut2.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            End If

                        End If
                    End If
                    If pan31 = True Then
                        If date3 = CDate(drow("fut_mdate")).Date And Val(txtfut3.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                                    Currfltpprice(CLng(drow("ftoken"))) = txtfut3.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("ftoken")), txtfut3.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("ftoken"))) Then
                                    fltpprice(CLng(drow("ftoken"))) = txtfut3.Text
                                Else
                                    fltpprice.Add(CLng(drow("ftoken")), txtfut3.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True
                            End If
                        End If
                    End If

                    Debug.Print(txtrate.Text)
                    Debug.Print(txtrate1.Text)
                    Debug.Print(txtrate2.Text)


                    If CBool(drow("isliq")) = True Then
                        token = CLng(drow("tokanno"))
                        token1 = CLng(drow("token1"))
                    Else
                        token = CLng(drow("tokanno"))
                        token1 = 0
                    End If
                    Dim index As Integer = -1 ' currtable.Rows.IndexOf(drow)
                    'For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' and tokanno=" & token & "")
                    '    index = gcurrtable.Rows.IndexOf(grow)
                    'Next

                    mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                    mmt = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date, CDate(drow("mdate")).Date)
                    If drow("cp") = "C" Then
                        iscall = True
                    Else
                        iscall = False
                    End If
                    If VarIsCurrency = True Then REM Opetion from CurrencyContract
                        If Currltpprice.Contains(token) Then
                            If token1 > 0 Then
                                ltppr = Val(Currltpprice(token))
                                ltppr1 = Val(Currltpprice(token1))
                            Else
                                ltppr = Val(Currltpprice(token))
                                ltppr1 = 0
                            End If
                            If chkCalVol.Checked = True And thrworking = False Then fltppr = Val(txteqrate.Text)
                            If fltppr <> 0 Then
                                cal_vol = 0
                                put_vol = 0
                                If pan11 = True Then
                                    If date1 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol.Text
                                        put_vol = txtpvol.Text
                                    End If
                                End If
                                If pan21 = True Then
                                    If date2 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol1.Text
                                        put_vol = txtpvol1.Text
                                    End If
                                End If
                                If pan31 = True Then
                                    If date3 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol2.Text
                                        put_vol = txtpvol2.Text
                                    End If
                                End If
                                Call CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, CmpName, cal_vol, put_vol)
                                If CBool(drow("isliq")) = True Then
                                    If Not Currltpprice.Contains(CLng(drow("tokanno"))) Then
                                        Currltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                    Else
                                        Currltpprice(CLng(drow("tokanno"))) = drow("last")
                                    End If
                                    If Not Currltpprice.Contains(CLng(drow("token1"))) Then
                                        Currltpprice.Add(CLng(drow("token1")), drow("last1"))
                                    Else
                                        Currltpprice(CLng(drow("token1"))) = drow("last1")
                                    End If
                                Else
                                    If Not Currltpprice.Contains(CLng(drow("tokanno"))) Then
                                        Currltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                    Else
                                        Currltpprice(CLng(drow("tokanno"))) = drow("last")
                                    End If
                                End If
                            End If
                        End If
                    Else   REM Opetion from Contract
                        If ltpprice.Contains(token) Then
                            If token1 > 0 Then
                                ltppr = Val(ltpprice(token))
                                ltppr1 = Val(ltpprice(token1))
                            Else
                                ltppr = Val(ltpprice(token))
                                ltppr1 = 0
                            End If
                            If chkCalVol.Checked = True And thrworking = False Then fltppr = Val(txteqrate.Text)
                            If fltppr <> 0 Then
                                cal_vol = 0
                                put_vol = 0
                                If pan11 = True Then
                                    If date1 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol.Text
                                        put_vol = txtpvol.Text
                                    End If
                                    If cal_vol <> 0 Or put_vol <> 0 Then
                                        Call CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, CmpName, cal_vol, put_vol)
                                    End If
                                End If
                                If pan21 = True Then
                                    If date2 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol1.Text
                                        put_vol = txtpvol1.Text
                                    End If
                                    If cal_vol <> 0 Or put_vol <> 0 Then
                                        Call CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, CmpName, cal_vol, put_vol)
                                    End If
                                End If
                                If pan31 = True Then
                                    If date3 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol2.Text
                                        put_vol = txtpvol2.Text
                                    End If
                                    If cal_vol <> 0 Or put_vol <> 0 Then
                                        Call CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, CmpName, cal_vol, put_vol)
                                    End If

                                End If
                                If CBool(drow("isliq")) = True Then
                                    If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                                        ltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                    Else
                                        ltpprice(CLng(drow("tokanno"))) = drow("last")

                                    End If
                                    If Not ltpprice.Contains(CLng(drow("token1"))) Then
                                        ltpprice.Add(CLng(drow("token1")), drow("last1"))
                                    Else
                                        ltpprice(CLng(drow("token1"))) = drow("last1")

                                    End If
                                Else
                                    If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                                        ltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                    Else
                                        ltpprice(CLng(drow("tokanno"))) = drow("last")
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next

        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub


    ''' <summary>
    ''' save_apply
    ''' </summary>
    ''' <remarks>This method call to Update Grid according to Offline value enter</remarks>
    Private Sub save_apply()
        Try
            Dim ltppr As Double = 0
            Dim ltppr1 As Double = 0
            Dim fltppr As Double = 0
            Dim mt As Double = 0
            Dim mmt As Double = 0
            Dim isfut As Boolean = False
            Dim iscall As Boolean = False

            Dim token As Long
            Dim token1 As Long
            Dim dt As Date

            Dim cal_vol As Double = 0
            Dim put_vol As Double = 0

            For Each drow As DataRow In currtable.Rows 'Select("company='" & compname & "' ")
                If drow("cp") = "E" Then 'equity
                    If thrworking = True Then
                        drow("last") = Val(eltpprice(CLng(drow("tokanno")))) 'get LTP of equity
                    Else
                        drow("last") = Val(txteqrate.Text)
                    End If

                    For Each grow As DataRow In gcurrtable.Select("tokanno='" & drow("tokanno") & "'")
                        grow("last") = drow("last")
                    Next

                ElseIf drow("CP") = "F" Then 'Future
                    If pan11 = True Then 'for first expiry date
                        If date1 = CDate(drow("mdate")).Date And Val(txtfut1.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currfltpprice(CLng(drow("tokanno"))) = txtfut1.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("tokanno")), txtfut1.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltpprice(CLng(drow("tokanno"))) = txtfut1.Text
                                Else
                                    fltpprice.Add(CLng(drow("tokanno")), txtfut1.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            End If
                        End If
                    End If
                    If pan21 = True Then
                        If date2 = CDate(drow("mdate")).Date And Val(txtfut2.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currfltpprice(CLng(drow("tokanno"))) = txtfut2.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("tokanno")), txtfut2.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltpprice(CLng(drow("tokanno"))) = txtfut2.Text
                                Else
                                    fltpprice.Add(CLng(drow("tokanno")), txtfut2.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            End If

                        End If
                    End If
                    If pan31 = True Then
                        If date3 = CDate(drow("mdate")).Date And Val(txtfut3.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currfltpprice(CLng(drow("tokanno"))) = txtfut3.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("tokanno")), txtfut3.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("tokanno"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltpprice(CLng(drow("tokanno"))) = txtfut3.Text
                                Else
                                    fltpprice.Add(CLng(drow("tokanno")), txtfut3.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("tokanno"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True

                            End If
                        End If
                    End If
                    If VarIsCurrency = True Then
                        drow("last") = Val(Currfltpprice(CLng(drow("tokanno"))))
                    Else
                        drow("last") = Val(fltpprice(CLng(drow("tokanno"))))
                    End If

                    For Each grow As DataRow In gcurrtable.Select("tokanno='" & drow("tokanno") & "'")
                        grow("last") = drow("last")
                    Next

                    'Start Apply Volatality Of Call and Put To 'F'
                    '------------------------------------------------------------------------
                    '' '' ''If CBool(drow("isliq")) = True Then
                    '' '' ''    token = CLng(drow("tokanno"))
                    '' '' ''    token1 = CLng(drow("token1"))
                    '' '' ''Else
                    '' '' ''    token = CLng(drow("tokanno"))
                    '' '' ''    token1 = 0
                    '' '' ''End If

                    '' '' ''Dim index As Integer = -1 ' currtable.Rows.IndexOf(drow)
                    '' '' ''For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' and tokanno=" & token & "")
                    '' '' ''    index = gcurrtable.Rows.IndexOf(grow)
                    '' '' ''Next

                    '' '' ''mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                    '' '' ''mmt = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date, CDate(drow("mdate")).Date)
                    '' '' ''If drow("cp") = "C" Then
                    '' '' ''    iscall = True
                    '' '' ''Else
                    '' '' ''    iscall = False
                    '' '' ''End If

                    ' '' '' ''If ltpprice.Contains(token) Then
                    '' '' ''If token1 > 0 Then
                    '' '' ''    fltppr = Val(fltpprice(token))
                    '' '' ''    ltppr1 = Val(fltpprice(token1))
                    '' '' ''Else
                    '' '' ''    fltppr = Val(fltpprice(token))
                    '' '' ''    ltppr1 = 0
                    '' '' ''End If
                    '' '' ''If chkCalVol.Checked = True And thrworking = False Then fltppr = Val(txteqrate.Text)
                    '' '' ''If fltppr <> 0 Then
                    '' '' ''    cal_vol = 0
                    '' '' ''    put_vol = 0
                    '' '' ''    If pan11 = True Then
                    '' '' ''        If date1 = CDate(drow("fut_mdate")).Date Then
                    '' '' ''            cal_vol = txtcvol.Text
                    '' '' ''            put_vol = txtpvol.Text
                    '' '' ''        End If
                    '' '' ''        If cal_vol <> 0 Or put_vol <> 0 Then
                    '' '' ''            Call CalData_start(fltppr, Val(drow("strikes")), fltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, cal_vol, put_vol)
                    '' '' ''        End If
                    '' '' ''    End If
                    '' '' ''    If pan21 = True Then
                    '' '' ''        If date2 = CDate(drow("fut_mdate")).Date Then
                    '' '' ''            cal_vol = txtcvol1.Text
                    '' '' ''            put_vol = txtpvol1.Text
                    '' '' ''        End If
                    '' '' ''        If cal_vol <> 0 Or put_vol <> 0 Then
                    '' '' ''            Call CalData_start(fltppr, Val(drow("strikes")), fltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, cal_vol, put_vol)
                    '' '' ''        End If
                    '' '' ''    End If
                    '' '' ''    If pan31 = True Then
                    '' '' ''        If date3 = CDate(drow("fut_mdate")).Date Then
                    '' '' ''            cal_vol = txtcvol2.Text
                    '' '' ''            put_vol = txtpvol2.Text
                    '' '' ''        End If
                    '' '' ''        If cal_vol <> 0 Or put_vol <> 0 Then
                    '' '' ''            Call CalData_start(fltppr, Val(drow("strikes")), fltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, cal_vol, put_vol)
                    '' '' ''        End If

                    '' '' ''    End If
                    '' '' ''    If CBool(drow("isliq")) = True Then
                    '' '' ''        If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                    '' '' ''            ltpprice.Add(CLng(drow("tokanno")), drow("last"))
                    '' '' ''        Else
                    '' '' ''            ltpprice(CLng(drow("tokanno"))) = drow("last")

                    '' '' ''        End If
                    '' '' ''        If Not ltpprice.Contains(CLng(drow("token1"))) Then
                    '' '' ''            ltpprice.Add(CLng(drow("token1")), drow("last1"))
                    '' '' ''        Else
                    '' '' ''            ltpprice(CLng(drow("token1"))) = drow("last1")

                    '' '' ''        End If
                    '' '' ''    Else
                    '' '' ''        If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                    '' '' ''            ltpprice.Add(CLng(drow("tokanno")), drow("last"))
                    '' '' ''        Else
                    '' '' ''            ltpprice(CLng(drow("tokanno"))) = drow("last")
                    '' '' ''        End If
                    '' '' ''    End If
                    '' '' ''End If

                    '---------------------------------------------------

                    '' ''If VarIsCurrency = True Then REM Opetion from CurrencyContract
                    '' ''    If Currltpprice.Contains(token) Then
                    '' ''        If token1 > 0 Then
                    '' ''            ltppr = Val(Currltpprice(token))
                    '' ''            ltppr1 = Val(Currltpprice(token1))
                    '' ''        Else
                    '' ''            ltppr = Val(Currltpprice(token))
                    '' ''            ltppr1 = 0
                    '' ''        End If
                    '' ''        If chkCalVol.Checked = True And thrworking = False Then fltppr = Val(txteqrate.Text)
                    '' ''        If fltppr <> 0 Then
                    '' ''            cal_vol = 0
                    '' ''            put_vol = 0
                    '' ''            If pan11 = True Then
                    '' ''                If date1 = CDate(drow("fut_mdate")).Date Then
                    '' ''                    cal_vol = txtcvol.Text
                    '' ''                    put_vol = txtpvol.Text
                    '' ''                End If
                    '' ''            End If
                    '' ''            If pan21 = True Then
                    '' ''                If date2 = CDate(drow("fut_mdate")).Date Then
                    '' ''                    cal_vol = txtcvol1.Text
                    '' ''                    put_vol = txtpvol1.Text
                    '' ''                End If
                    '' ''            End If
                    '' ''            If pan31 = True Then
                    '' ''                If date3 = CDate(drow("fut_mdate")).Date Then
                    '' ''                    cal_vol = txtcvol2.Text
                    '' ''                    put_vol = txtpvol2.Text
                    '' ''                End If
                    '' ''            End If
                    '' ''            Call CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, cal_vol, put_vol)
                    '' ''            If CBool(drow("isliq")) = True Then
                    '' ''                If Not Currltpprice.Contains(CLng(drow("tokanno"))) Then
                    '' ''                    Currltpprice.Add(CLng(drow("tokanno")), drow("last"))
                    '' ''                Else
                    '' ''                    Currltpprice(CLng(drow("tokanno"))) = drow("last")
                    '' ''                End If
                    '' ''                If Not Currltpprice.Contains(CLng(drow("token1"))) Then
                    '' ''                    Currltpprice.Add(CLng(drow("token1")), drow("last1"))
                    '' ''                Else
                    '' ''                    Currltpprice(CLng(drow("token1"))) = drow("last1")
                    '' ''                End If
                    '' ''            Else
                    '' ''                If Not Currltpprice.Contains(CLng(drow("tokanno"))) Then
                    '' ''                    Currltpprice.Add(CLng(drow("tokanno")), drow("last"))
                    '' ''                Else
                    '' ''                    Currltpprice(CLng(drow("tokanno"))) = drow("last")
                    '' ''                End If
                    '' ''            End If
                    '' ''        End If
                    '' ''    End If
                    '' ''End If
                    'End  of Apply Volatality Of Call and Put
                Else
                    If pan11 = True Then
                        If date1 = CDate(drow("fut_mdate")).Date And Val(txtfut1.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                                    Currfltpprice(CLng(drow("ftoken"))) = txtfut1.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("ftoken")), txtfut1.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("ftoken"))) Then
                                    fltpprice(CLng(drow("ftoken"))) = txtfut1.Text
                                Else
                                    fltpprice.Add(CLng(drow("ftoken")), txtfut1.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                txtrate.Invoke(mdel, fltppr)
                                dt = date1
                                isfut = True
                            End If
                        End If
                    End If
                    If pan21 = True Then
                        If date2 = CDate(drow("fut_mdate")).Date And Val(txtfut2.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                                    Currfltpprice(CLng(drow("ftoken"))) = txtfut2.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("ftoken")), txtfut2.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("ftoken"))) Then
                                    fltpprice(CLng(drow("ftoken"))) = txtfut2.Text
                                Else
                                    fltpprice.Add(CLng(drow("ftoken")), txtfut2.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                dt = date2
                                txtrate1.Invoke(mdel1, fltppr)
                                isfut = True
                            End If

                        End If
                    End If
                    If pan31 = True Then
                        If date3 = CDate(drow("fut_mdate")).Date And Val(txtfut3.Text) <> 0 Then
                            If VarIsCurrency = True Then
                                If Currfltpprice.Contains(CLng(drow("ftoken"))) Then
                                    Currfltpprice(CLng(drow("ftoken"))) = txtfut3.Text
                                Else
                                    Currfltpprice.Add(CLng(drow("ftoken")), txtfut3.Text)
                                End If
                                fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True
                            Else
                                If fltpprice.Contains(CLng(drow("ftoken"))) Then
                                    fltpprice(CLng(drow("ftoken"))) = txtfut3.Text
                                Else
                                    fltpprice.Add(CLng(drow("ftoken")), txtfut3.Text)
                                End If
                                fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                                dt = date3
                                txtrate2.Invoke(mdel2, fltppr)
                                isfut = True
                            End If
                        End If
                    End If

                    'Debug.Print(txtrate.Text)
                    'Debug.Print(txtrate1.Text)
                    'Debug.Print(txtrate2.Text)


                    If CBool(drow("isliq")) = True Then
                        token = CLng(drow("tokanno"))
                        token1 = CLng(drow("token1"))
                    Else
                        token = CLng(drow("tokanno"))
                        token1 = 0
                    End If
                    Dim index As Integer = -1 ' currtable.Rows.IndexOf(drow)
                    For Each grow As DataRow In gcurrtable.Select("company='" & compname & "' and tokanno=" & token & "")
                        index = gcurrtable.Rows.IndexOf(grow)
                    Next

                    mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                    mmt = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date, CDate(drow("mdate")).Date)
                    If drow("cp") = "C" Then
                        iscall = True
                    Else
                        iscall = False
                    End If
                    If VarIsCurrency = True Then REM Opetion from CurrencyContract
                        If Currltpprice.Contains(token) Then
                            If token1 > 0 Then
                                ltppr = Val(Currltpprice(token))
                                ltppr1 = Val(Currltpprice(token1))
                            Else
                                ltppr = Val(Currltpprice(token))
                                ltppr1 = 0
                            End If
                            If chkCalVol.Checked = True And thrworking = False Then fltppr = Val(txteqrate.Text)
                            If fltppr <> 0 Then
                                cal_vol = 0
                                put_vol = 0
                                If pan11 = True Then
                                    If date1 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol.Text
                                        put_vol = txtpvol.Text
                                    End If
                                End If
                                If pan21 = True Then
                                    If date2 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol1.Text
                                        put_vol = txtpvol1.Text
                                    End If
                                End If
                                If pan31 = True Then
                                    If date3 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol2.Text
                                        put_vol = txtpvol2.Text
                                    End If
                                End If
                                Call CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, cal_vol, put_vol)
                                If CBool(drow("isliq")) = True Then
                                    If Not Currltpprice.Contains(CLng(drow("tokanno"))) Then
                                        Currltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                    Else
                                        Currltpprice(CLng(drow("tokanno"))) = drow("last")
                                    End If
                                    If Not Currltpprice.Contains(CLng(drow("token1"))) Then
                                        Currltpprice.Add(CLng(drow("token1")), drow("last1"))
                                    Else
                                        Currltpprice(CLng(drow("token1"))) = drow("last1")
                                    End If
                                Else
                                    If Not Currltpprice.Contains(CLng(drow("tokanno"))) Then
                                        Currltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                    Else
                                        Currltpprice(CLng(drow("tokanno"))) = drow("last")
                                    End If
                                End If
                            End If
                        End If
                    Else   REM Opetion from Contract
                        If ltpprice.Contains(token) Then
                            If token1 > 0 Then
                                ltppr = Val(ltpprice(token))
                                ltppr1 = Val(ltpprice(token1))
                            Else
                                ltppr = Val(ltpprice(token))
                                ltppr1 = 0
                            End If
                            If chkCalVol.Checked = True And thrworking = False Then fltppr = Val(txteqrate.Text)
                            If fltppr <> 0 Then
                                cal_vol = 0
                                put_vol = 0
                                If pan11 = True Then
                                    If date1 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol.Text
                                        put_vol = txtpvol.Text
                                    End If
                                    If cal_vol <> 0 Or put_vol <> 0 Then
                                        Call CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, cal_vol, put_vol)
                                    End If
                                End If
                                If pan21 = True Then
                                    If date2 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol1.Text
                                        put_vol = txtpvol1.Text
                                    End If
                                    If cal_vol <> 0 Or put_vol <> 0 Then
                                        Call CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, cal_vol, put_vol)
                                    End If
                                End If
                                If pan31 = True Then
                                    If date3 = CDate(drow("fut_mdate")).Date Then
                                        cal_vol = txtcvol2.Text
                                        put_vol = txtpvol2.Text
                                    End If
                                    If cal_vol <> 0 Or put_vol <> 0 Then
                                        Call CalData_start(fltppr, Val(drow("strikes")), ltppr, ltppr1, mt, mmt, iscall, isfut, drow, drow("units"), index, compname, cal_vol, put_vol)
                                    End If

                                End If
                                If CBool(drow("isliq")) = True Then
                                    If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                                        ltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                    Else
                                        ltpprice(CLng(drow("tokanno"))) = drow("last")

                                    End If
                                    If Not ltpprice.Contains(CLng(drow("token1"))) Then
                                        ltpprice.Add(CLng(drow("token1")), drow("last1"))
                                    Else
                                        ltpprice(CLng(drow("token1"))) = drow("last1")

                                    End If
                                Else
                                    If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                                        ltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                    Else
                                        ltpprice(CLng(drow("tokanno"))) = drow("last")
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next
            save_applyMt(compname)
        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub

    ''' <summary>
    ''' ReFresh_Maintable
    ''' </summary>
    ''' <remarks>This method call to Update MainTable enter</remarks>
    Private Sub ReFresh_Maintable()
        Dim ApplyVolFlg As Boolean
        Dim CmpName As String
        Try

            For Each cmpRow As DataRow In comptable.Rows
                CmpName = cmpRow("Company")
                If GdtCurrencyTrades.Select("Company='" & CmpName & "'").Length > 0 Then
                    VarIsCurrency = True
                Else
                    VarIsCurrency = False
                End If
                ApplyVolFlg = False
                cparray = New ArrayList
                futarray = New ArrayList
                eqarray = New ArrayList
                Dim valToken As Double
                Dim arr(14) As String
                arr = objAna.company_ltp(CmpName)

                Call Fill_Analysis_Hashtable(maintable, CmpName)

                For Each drow As DataRow In maintable.Select("company = '" & CmpName & "'", "strikes,cp,company")
                    If drow("CP") = "F" Then 'for FUTURE, if LTP exist then add (token,LTP)
                        futarray.Add(CLng(drow("tokanno")))
                        valToken = objAna.ltp_token(CLng(drow("tokanno")), drow("script"), CmpName)
                        If VarIsCurrency = True Then  REM Currency Future
                            If valToken <> 0 Then
                                If Not Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currfltpprice.Add(CLng(drow("tokanno")), valToken)
                                End If
                            Else
                                If arr(3) = CDate(drow("mdate")) Then
                                    If Not Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                        Currfltpprice.Add(CLng(drow("tokanno")), CDbl(arr(2)))
                                    End If
                                ElseIf arr(5) = CDate(drow("mdate")) Then
                                    If Not Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                        Currfltpprice.Add(CLng(drow("tokanno")), CDbl(arr(4)))
                                    End If
                                ElseIf arr(7) = CDate(drow("mdate")) Then
                                    If Not Currfltpprice.Contains(CLng(drow("tokanno"))) Then
                                        Currfltpprice.Add(CLng(drow("tokanno")), CDbl(arr(6)))
                                    End If
                                End If
                            End If
                        Else  ' FO Future
                            If valToken <> 0 Then
                                If Not fltpprice.Contains(CLng(drow("tokanno"))) Then
                                    fltpprice.Add(CLng(drow("tokanno")), valToken)
                                End If
                            Else
                                If arr(3) = CDate(drow("mdate")) Then
                                    If Not fltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltpprice.Add(CLng(drow("tokanno")), CDbl(arr(2)))
                                    End If
                                ElseIf arr(5) = CDate(drow("mdate")) Then
                                    If Not fltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltpprice.Add(CLng(drow("tokanno")), CDbl(arr(4)))
                                    End If
                                ElseIf arr(7) = CDate(drow("mdate")) Then
                                    If Not fltpprice.Contains(CLng(drow("tokanno"))) Then
                                        fltpprice.Add(CLng(drow("tokanno")), CDbl(arr(6)))
                                    End If
                                End If
                            End If
                        End If
                    ElseIf drow("CP") = "E" Then
                        eqarray.Add(CLng(drow("tokanno")))
                        If Not eltpprice.Contains(CLng(drow("tokanno"))) Then
                            eltpprice.Add(CLng(drow("tokanno")), drow("last"))
                        End If
                    Else  REM Option Script
                        If IsDBNull(drow("isliq")) = True Then
                            drow("isliq") = False
                        End If
                        If VarIsCurrency = True Then
                            If CBool(drow("isliq")) = True Then
                                cparray.Add(CLng(drow("token1")))
                                cparray.Add(CLng(drow("tokanno")))
                                If Not Currltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                End If
                                If Not Currltpprice.Contains(CLng(drow("token1"))) Then
                                    Currltpprice.Add(CLng(drow("token1")), drow("last1"))
                                End If
                            Else
                                cparray.Add(CLng(drow("tokanno")))
                                If Not Currltpprice.Contains(CLng(drow("tokanno"))) Then
                                    Currltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                End If
                            End If
                        Else
                            If CBool(drow("isliq")) = True Then
                                cparray.Add(CLng(drow("token1")))
                                cparray.Add(CLng(drow("tokanno")))
                                If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                                    ltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                End If
                                If Not ltpprice.Contains(CLng(drow("token1"))) Then
                                    ltpprice.Add(CLng(drow("token1")), drow("last1"))
                                End If
                            Else
                                cparray.Add(CLng(drow("tokanno")))
                                If Not ltpprice.Contains(CLng(drow("tokanno"))) Then
                                    ltpprice.Add(CLng(drow("tokanno")), drow("last"))
                                End If
                            End If
                        End If

                        'REM1 :keval(04-08-2010)
                        'changes Done by nasima on 10 th aug
                        '=========================================
                        'if perticular options future is not there in hashtable then add it
                        If IsDBNull(drow("fut_mdate")) = True Then
                            If IsDBNull(drow("mdate")) = False Then
                                drow("fut_mdate") = drow("mdate")
                            End If
                        End If
                        'REM 1: END
                        If arr(3) = CDate(drow("fut_mdate")) Then
                            ' If arr(3) = CDate(drow("mdate")) Then
                            If Not fltpprice.Contains(CLng(drow("ftoken"))) Then
                                fltpprice.Add(CLng(drow("ftoken")), CDbl(arr(2)))
                            End If
                        ElseIf arr(5) = CDate(drow("fut_mdate")) Then
                            ' ElseIf arr(5) = CDate(drow("mdate")) Then
                            If Not fltpprice.Contains(CLng(drow("ftoken"))) Then
                                fltpprice.Add(CLng(drow("ftoken")), CDbl(arr(4)))
                            End If
                            '  ElseIf arr(7) = CDate(drow("mdate")) Then
                        ElseIf arr(7) = CDate(drow("fut_mdate")) Then
                            If Not fltpprice.Contains(CLng(drow("ftoken"))) Then
                                fltpprice.Add(CLng(drow("ftoken")), CDbl(arr(6)))
                            End If
                        End If
                        'end of perticular options future is not there in hashtable then add it
                    End If
                Next

                Dim fdv As DataView
                If VarIsCurrency = True Then
                    fdv = New DataView(Currencymaster, "symbol='" & CmpName & "' and option_type not in ('CA','CE','PA','PE') ", "symbol", DataViewRowState.CurrentRows)
                    txtlot.Text = IIf(IsDBNull(fdv.ToTable(True, "lotsize", "multiplier").Compute("max(multiplier)", "")), 0, fdv.ToTable(True, "lotsize", "multiplier").Compute("max(multiplier)", ""))
                    If CURR_DELTA_BASE = Setting_Greeks_BASE.Lot Then
                        iDeltaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iDeltaLSize = 1
                    End If

                    'Gamma
                    If CURR_GAMMA_BASE = Setting_Greeks_BASE.Lot Then
                        iGammaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iGammaLSize = 1
                    End If

                    'Vega
                    If CURR_VEGA_BASE = Setting_Greeks_BASE.Lot Then
                        iVegaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iVegaLSize = 1
                    End If

                    'theta
                    If CURR_THETA_BASE = Setting_Greeks_BASE.Lot Then
                        iThetaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iThetaLSize = 1
                    End If

                    'Volga
                    If CURR_VOLGA_BASE = Setting_Greeks_BASE.Lot Then
                        iVolgaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iVolgaLSize = 1
                    End If

                    'Vanna
                    If CURR_VANNA_BASE = Setting_Greeks_BASE.Lot Then
                        iVannaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iVannaLSize = 1
                    End If

                Else
                    fdv = New DataView(scripttable, "symbol='" & CmpName & "' and option_type not in ('CA','CE','PA','PE') ", "symbol", DataViewRowState.CurrentRows)
                    txtlot.Text = IIf(IsDBNull(fdv.ToTable(True, "lotsize").Compute("max(lotsize)", "")), 0, fdv.ToTable(True, "lotsize").Compute("max(lotsize)", ""))
                    If EQ_DELTA_BASE = Setting_Greeks_BASE.Lot Then
                        iDeltaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iDeltaLSize = 1
                    End If

                    'Gamma
                    If EQ_GAMMA_BASE = Setting_Greeks_BASE.Lot Then
                        iGammaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iGammaLSize = 1
                    End If

                    'Vega
                    If EQ_VEGA_BASE = Setting_Greeks_BASE.Lot Then
                        iVegaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iVegaLSize = 1
                    End If

                    'theta
                    If EQ_THETA_BASE = Setting_Greeks_BASE.Lot Then
                        iThetaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iThetaLSize = 1
                    End If

                    'volga
                    If EQ_VOLGA_BASE = Setting_Greeks_BASE.Lot Then
                        iVolgaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iVolgaLSize = 1
                    End If

                    'vanna
                    If EQ_VANNA_BASE = Setting_Greeks_BASE.Lot Then
                        iVannaLSize = Val(IIf((Val(txtlot.Text) = 0), 1, Val(txtlot.Text)))
                    Else
                        iVannaLSize = 1
                    End If
                End If
                txtcvol.Text = arr(0)
                txtpvol.Text = arr(1)
                txtcvol1.Text = arr(8)
                txtpvol1.Text = arr(9)
                txtcvol2.Text = arr(10)
                txtpvol2.Text = arr(11)
                txteqrate.Text = arr(12)
                'Alpesh 
                txtfut1.Text = arr(2)
                txtfut2.Text = arr(4)
                txtfut3.Text = arr(6)
                'txtexp.Text = Format(CDate(arr(3)), "MMM yy")
                'txtexp1.Text = Format(CDate(arr(5)), "MMM yy")
                'txtexp2.Text = Format(CDate(arr(7)), "MMM yy")

                Dim i As Integer
                i = 0
                Dim mdate As New ArrayList

                REM coding by mahesh to set the date for the far month expiry

                Dim temp_cur_date_second As Long
                Dim temp_today_date_second As Long
                temp_today_date_second = DateDiff(DateInterval.Second, CDate("1/1/1980"), CDate(Format(Now, "MMM/dd/yyyy")))
                'get current month maturity from the contract

                'temp_cur_mdate_month = (CDate(Format(Now, "MM/"MMM/dd/yyyy"ear * 12) + CDate(Format(Now, "MMM/dd/yyyy")).Month
                If VarIsCurrency = True Then
                    temp_cur_date_second = IIf(IsDBNull(Currencymaster.Compute("min(expiry_date)", "symbol='" & CmpName & "' and expiry_date >= " & temp_today_date_second)), 0, Currencymaster.Compute("min(expiry_date)", "symbol='" & CmpName & "' and expiry_date >= " & temp_today_date_second))
                Else
                    temp_cur_date_second = IIf(IsDBNull(scripttable.Compute("min(expiry_date)", "symbol='" & CmpName & "' and expiry_date >= " & temp_today_date_second)), 0, scripttable.Compute("min(expiry_date)", "symbol='" & CmpName & "' and expiry_date >= " & temp_today_date_second))
                End If

                Maturity_Cur_month = DateAdd(DateInterval.Second, temp_cur_date_second, CDate("1/1/1980"))

                Maturity_first_month = (Maturity_Cur_month.Year * 12) + Maturity_Cur_month.Month
                Maturity_second_month = (Maturity_Cur_month.Year * 12) + Maturity_Cur_month.Month + 1
                Maturity_third_month = (Maturity_Cur_month.Year * 12) + Maturity_Cur_month.Month + 2
                Dim str1(0) As String
                str1(0) = "fut_mdate"
                ' str1(1) = "cp"
                Dim SelTabName As String
                SelTabName = TabStrategy.SelectedTab.Name
                'end of coding for far month expiry
                'Determine how many panel of expiry date will be visible
                Dim dv As DataView
                dv = New DataView(maintable, "Company= '" & CmpName & "'", "strikes,cp,company", DataViewRowState.CurrentRows)

                For Each drow As DataRow In dv.ToTable(True, str1).Select("", "fut_mdate") 'get only cp,exipry date
                    If Not mdate.Contains(drow("fut_mdate")) Then
                        mdate.Add(drow("fut_mdate")) 'add expiry date to mdate
                        'select first maturity date and visible first panel
                        If i = 0 And CDate(drow("fut_mdate")) >= Today Then  'And ((CDate(drow("fut_mdate")).Year * 12) + CDate(drow("fut_mdate")).Month) <= Maturity_third_month Then
                            dtexp.Value = CDate(drow("fut_mdate"))
                            txtdays.Text = DateDiff(DateInterval.Day, Now(), CDate(dtexp.Value)) + 1 ' DateDiff(DateInterval.Day, CDate(dtexp.Value), Now())
                            pan1.Visible = True
                            pan11 = True
                            pan1.Refresh()
                            txtfut1.Visible = True
                            'TableLayoutPanel1.RowStyles(1).Height = 26
                            lfut1.Visible = True
                            date1 = dtexp.Value.Date
                            txtexp.Text = Format(CDate(drow("fut_mdate")), "MMM yy")
                            If CDate(arr(3)) = CDate(drow("fut_mdate")) Then
                                txtfut1.Text = arr(2)
                            End If
                            i = i + 1
                            'TabControl1.TabPages(1).Visible = True
                            'TabControl1.TabPages(1).ToolTipText = ""


                            'Rem Cal For Call1,Call2,Call3,Call4 
                            Dim SelTab As Integer
                            SelTab = 0
                            REM  Broadcast is not stop, While swiching from one month tab to another tab
                            If thrworking = False Then
                                If Val(txtcvol.Text) <> 0 Or Val(txtpvol.Text) <> 0 Or Val(txtcvol1.Text) <> 0 Or Val(txtpvol1.Text) <> 0 Or Val(txtcvol2.Text) <> 0 Or Val(txtpvol2.Text) <> 0 Then
                                    ApplyVolFlg = True
                                Else
                                    ApplyVolFlg = False
                                End If
                            End If
                            '' ''        txtfut1.Text = arr(2)
                            '' ''        txtfut2.Text = arr(4)
                            '' ''        txtfut3.Text = arr(6)
                            REM For All
                            pan11 = True
                            pan21 = True
                            pan31 = True
                            UpdateAutomatic(ApplyVolFlg)

                            'select second maturity date and visible second panel
                        ElseIf i = 1 And CDate(drow("fut_mdate")) >= CDate(Format(Now, "MMM/dd/yyyy")) And ((CDate(drow("fut_mdate")).Year * 12) + CDate(drow("fut_mdate")).Month) <= Maturity_third_month Then
                            dtexp1.Value = CDate(drow("fut_mdate"))
                            txtdays1.Text = DateDiff(DateInterval.Day, Now(), CDate(dtexp1.Value)) + 1 ' DateDiff(DateInterval.Day, CDate(dtexp.Value), Now())
                            pan2.Visible = True
                            pan21 = True
                            pan2.Refresh()
                            txtfut2.Visible = True
                            lfut2.Visible = True
                            TableLayoutPanel1.RowStyles(2).Height = 26
                            date2 = dtexp1.Value.Date
                            txtexp1.Text = Format(CDate(drow("fut_mdate")), "MMM yy")
                            If CDate(arr(5)) = CDate(drow("fut_mdate")) Then
                                txtfut2.Text = arr(4)
                            End If
                            i = i + 1
                            'select third maturity date and visible third panel
                        ElseIf i = 2 And CDate(drow("fut_mdate")) >= CDate(Format(Now, "MMM/dd/yyyy")) And ((CDate(drow("fut_mdate")).Year * 12) + CDate(drow("fut_mdate")).Month) <= Maturity_third_month Then
                            dtexp2.Value = CDate(drow("fut_mdate"))
                            txtdays2.Text = DateDiff(DateInterval.Day, Now(), CDate(dtexp2.Value)) + 1 ' DateDiff(DateInterval.Day, CDate(dtexp.Value), Now())
                            pan3.Visible = True
                            pan3.Refresh()
                            TableLayoutPanel1.RowStyles(3).Height = 26
                            pan31 = True
                            txtfut3.Visible = True
                            lfut3.Visible = True
                            date3 = dtexp2.Value.Date
                            txtexp2.Text = Format(CDate(drow("fut_mdate")), "MMM yy")
                            If CDate(arr(7)) = CDate(drow("fut_mdate")) Then
                                txtfut3.Text = arr(6)
                            End If
                            i = i + 1
                            Exit For
                        End If
                    End If
                Next

                For Each drow As DataRow In fdv.ToTable.Rows
                    If pan1.Visible = True And dtexp.Value = CDate(drow("expdate1")) Then
                        If Not futarray.Contains(CLng(drow("token"))) Then
                            futarray.Add(CLng(drow("token")))
                            valToken = objAna.ltp_token(CLng(drow("token")), drow("script"), CmpName)
                            If valToken <> 0 Then
                                If Not fltpprice.Contains(CLng(drow("token"))) Then
                                    fltpprice.Add(CLng(drow("token")), valToken)
                                End If
                            Else
                                If arr(3) = CDate(drow("expdate1")) Then
                                    If Not fltpprice.Contains(CLng(drow("token"))) Then
                                        fltpprice.Add(CLng(drow("token")), CDbl(arr(2)))
                                    End If
                                Else
                                    If Not fltpprice.Contains(CLng(drow("token"))) Then
                                        fltpprice.Add(CLng(drow("token")), 0)
                                    End If
                                End If
                            End If
                        End If
                    ElseIf pan2.Visible = True And dtexp1.Value = CDate(drow("expdate1")) Then
                        If Not futarray.Contains(CLng(drow("token"))) Then
                            futarray.Add(CLng(drow("token")))
                            valToken = objAna.ltp_token(CLng(drow("token")), drow("script"), CmpName)
                            If valToken <> 0 Then
                                If Not fltpprice.Contains(CLng(drow("token"))) Then
                                    fltpprice.Add(CLng(drow("token")), valToken)
                                End If
                            Else
                                If arr(5) = CDate(drow("expdate1")) Then
                                    If Not fltpprice.Contains(CLng(drow("token"))) Then
                                        fltpprice.Add(CLng(drow("token")), CDbl(arr(4)))
                                    End If

                                End If
                            End If
                        End If
                    ElseIf pan3.Visible = True And dtexp2.Value = CDate(drow("expdate1")) Then
                        If Not futarray.Contains(CLng(drow("token"))) Then
                            futarray.Add(CLng(drow("token")))
                            valToken = objAna.ltp_token(CLng(drow("token")), drow("script"), CmpName)
                            If valToken <> 0 Then
                                If Not fltpprice.Contains(CLng(drow("token"))) Then
                                    fltpprice.Add(CLng(drow("token")), valToken)
                                End If
                            Else
                                If arr(7) = CDate(drow("expdate1")) Then
                                    If Not fltpprice.Contains(CLng(drow("token"))) Then
                                        fltpprice.Add(CLng(drow("token")), CDbl(arr(6)))
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next
                'Alpesh  20/04/2011
                REM In Analysis window, Previous & Today Expense for All tab & different months tab come as per Tab Selection
                'For TP_All
                txtprexp.Text = -Format(Math.Abs(CFexpense) + Math.Abs(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & compname & "' and entry_date < #" & fDate(Today) & "#").ToString)), Expensestr)
                txttexp.Text = -Format(Math.Abs(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & compname & "' and entry_date >= #" & fDate(Today) & "#").ToString)), Expensestr)
                txttotexp.Text = -(Math.Abs(Val(txtprexp.Text)) + Math.Abs(Val(txttexp.Text)))

                'this will keep the old status of start/stop mode or future only mode for trade referesh or new position from open position form
                If thrworking = True And thrworking_future_only = False Then 'online mode
                    'If Not mo Is Nothing Then
                    'calculate_tab(True)
                    'Else
                    calculate_tabMt(CmpName)
                    'End If
                    ' call_function()
                    cal_eqMt(CmpName)
                    cal_futureMt(CmpName)

                ElseIf thrworking = False And thrworking_future_only = False Then 'offline mode
                    'If Not mo Is Nothing Then
                    'calculate_tab(True)
                    'Else
                    calculate_tabMt(CmpName)
                    'End If
                    ' call_function()
                    cal_eqMt(CmpName)
                    cal_futureMt(CmpName)

                ElseIf thrworking = False And thrworking_future_only = True Then 'future update only mopde
                    thrworking_future_only = False
                    If Save_applied = False Then 'previously online mode
                        'If Not mo Is Nothing Then
                        'calculate_tab(True)
                        'Else
                        calculate_tabMt(CmpName)
                        'End If
                        ' call_function()

                    Else 'previously offline mode
                        save_applyMt(CmpName)
                    End If
                    thrworking_future_only = True

                    cal_eqMt(CmpName)
                    cal_futureMt(CmpName)
                End If

                cal_grossmtmMt(CmpName)
                maintable.AcceptChanges()
            Next


        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
        End Try
    End Sub

    ''' <summary>
    ''' save_data
    ''' </summary>
    ''' <param name="table"></param>
    ''' <remarks>This method call to save analysis data into database</remarks>
    Private Sub save_data(ByVal table As DataTable)
        If maintable.Rows.Count > 0 Then
            Dim mt As Double
            Dim futval As Double
            Dim iscall As Boolean
            Dim drow As DataRow
            'Dim dr As DataRow
            'Dim Dif As Integer
            For Each drow In table.Rows

                'drow("curSpot") = table.Compute("max(last)", "cp='F' and company = '" & drow("company") & "' And month ='" & drow("month") & "'")
                'curVol = lv
                'curDelVal = deltaval
                'curVegVal = vgval
                'curTheVal = thetaval
                drow("status") = 1
                drow("entrydate") = Now.Date



                If drow("CP") = "F" Then
                    If CBool(drow("isCurrency")) = True Then
                        drow("last") = IIf((IsDBNull(Currfltpprice(CLng(drow("tokanno")))) Or Currfltpprice(CLng(drow("tokanno"))) Is Nothing), 0, Currfltpprice(CLng(drow("tokanno"))))
                    Else
                        drow("last") = IIf((IsDBNull(fltpprice(CLng(drow("tokanno")))) Or fltpprice(CLng(drow("tokanno"))) Is Nothing), 0, fltpprice(CLng(drow("tokanno"))))
                    End If

                    drow("lv") = 0
                    drow("lv1") = 0
                    drow("last1") = 0
                ElseIf drow("cp") = "E" Then
                    drow("last") = IIf((IsDBNull(eltpprice(CLng(drow("tokanno")))) Or eltpprice(CLng(drow("tokanno"))) Is Nothing), 0, eltpprice(CLng(drow("tokanno"))))
                    drow("lv") = 0
                    drow("lv1") = 0
                    drow("last1") = 0
                Else
                    futval = 0
                    If CBool(drow("isCurrency")) = True Then
                        futval = IIf(IsNothing(Currfltpprice(CLng(drow("ftoken")))), 0, Currfltpprice(CLng(drow("ftoken"))))
                        If drow("cp") = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                        If mt = 0 Then
                            mt = 0.0001
                        Else
                            mt = (mt) / 365
                        End If
                        If CBool(IIf(IsDBNull(drow("IsVolFix")), False, drow("IsVolFix"))) = False Then
                            If drow("isliq") = True Then
                                drow("last") = IIf((IsDBNull(Currltpprice(CLng(drow("tokanno")))) Or Currltpprice(CLng(drow("tokanno"))) Is Nothing), 0, Currltpprice(CLng(drow("tokanno"))))
                                drow("last1") = IIf((IsDBNull(Currltpprice(CLng(drow("token1")))) Or Currltpprice(CLng(drow("tokanno"))) Is Nothing), 0, Currltpprice(CLng(drow("token1"))))
                                If futval <> 0 Then
                                    If iscall = True Then
                                        drow("lv") = objAna.Vol(futval, Val(drow("strikes")), Val(drow("last1")), mt, False, True) * 100
                                        drow("lv1") = objAna.Vol(futval, Val(drow("strikes")), Val(drow("last")), mt, True, True) * 100
                                    Else
                                        drow("lv1") = objAna.Vol(futval, Val(drow("strikes")), Val(drow("last1")), mt, False, True) * 100
                                        drow("lv") = objAna.Vol(futval, Val(drow("strikes")), Val(drow("last")), mt, True, True) * 100
                                    End If
                                End If
                            Else
                                If futval <> 0 Then
                                    drow("last") = IIf((IsDBNull(Currltpprice(CLng(drow("tokanno")))) Or Currltpprice(CLng(drow("tokanno"))) Is Nothing), 0, Currltpprice(CLng(drow("tokanno"))))
                                    drow("last1") = 0
                                    drow("lv") = objAna.Vol(futval, Val(drow("strikes")), Val(drow("last")), mt, iscall, True) * 100
                                    drow("lv1") = 0
                                End If
                            End If
                        End If
                    Else

                        futval = IIf(IsNothing(fltpprice(CLng(drow("ftoken")))), 0, fltpprice(CLng(drow("ftoken"))))
                        If drow("cp") = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                        If mt = 0 Then
                            mt = 0.0001
                        Else
                            mt = (mt) / 365
                        End If
                        If CBool(IIf(IsDBNull(drow("IsVolFix")), False, drow("IsVolFix"))) = False Then
                            If drow("isliq") = True Then
                                drow("last") = IIf((IsDBNull(ltpprice(CLng(drow("tokanno")))) Or ltpprice(CLng(drow("tokanno"))) Is Nothing), 0, ltpprice(CLng(drow("tokanno"))))
                                drow("last1") = IIf((IsDBNull(ltpprice(CLng(drow("token1")))) Or ltpprice(CLng(drow("tokanno"))) Is Nothing), 0, ltpprice(CLng(drow("token1"))))
                                If futval <> 0 Then
                                    If iscall = True Then
                                        drow("lv") = objAna.Vol(futval, Val(drow("strikes")), Val(drow("last1")), mt, False, True) * 100
                                        drow("lv1") = objAna.Vol(futval, Val(drow("strikes")), Val(drow("last")), mt, True, True) * 100
                                    Else
                                        drow("lv1") = objAna.Vol(futval, Val(drow("strikes")), Val(drow("last1")), mt, False, True) * 100
                                        drow("lv") = objAna.Vol(futval, Val(drow("strikes")), Val(drow("last")), mt, True, True) * 100
                                    End If
                                End If
                            Else
                                If futval <> 0 Then
                                    drow("last") = IIf((IsDBNull(ltpprice(CLng(drow("tokanno")))) Or ltpprice(CLng(drow("tokanno"))) Is Nothing), 0, ltpprice(CLng(drow("tokanno"))))
                                    drow("last1") = 0
                                    drow("lv") = objAna.Vol(futval, Val(drow("strikes")), Val(drow("last")), mt, iscall, True) * 100
                                    drow("lv1") = 0
                                End If
                            End If
                        End If
                    End If
                End If
                table.AcceptChanges()

                If IsDate(drow("preDate")) = False Then
                    'Put EntDate  With Curr Data
                    drow("preDate") = drow("entrydate")
                    If CBool(drow("isCurrency")) = True Then
                        drow("curSpot") = IIf(IsNothing(Currfltpprice(CLng(drow("ftoken")))), 0, Currfltpprice(CLng(drow("ftoken")))) 'Val(table.Compute("max(last)", "cp='F' and company = '" & drow("company") & "' And month ='" & drow("month") & "'").ToString)
                    Else
                        drow("curSpot") = IIf(IsNothing(fltpprice(CLng(drow("ftoken")))), 0, fltpprice(CLng(drow("ftoken")))) 'Val(table.Compute("max(last)", "cp='F' and company = '" & drow("company") & "' And month ='" & drow("month") & "'").ToString)
                    End If


                    drow("curVol") = drow("lv")
                    drow("curDelVal") = drow("deltaval")
                    drow("curVegVal") = drow("vgval")
                    drow("curTheVal") = drow("thetaval")

                Else
                    If drow("preDate") = drow("entrydate") Then
                        If CBool(drow("isCurrency")) = True Then
                            drow("curSpot") = IIf(IsNothing(Currfltpprice(CLng(drow("ftoken")))), 0, Currfltpprice(CLng(drow("ftoken")))) 'Val(table.Compute("max(last)", "cp='F' and company = '" & drow("company") & "' And month ='" & drow("month") & "'").ToString)
                        Else
                            drow("curSpot") = IIf(IsNothing(fltpprice(CLng(drow("ftoken")))), 0, fltpprice(CLng(drow("ftoken")))) 'Val(table.Compute("max(last)", "cp='F' and company = '" & drow("company") & "' And month ='" & drow("month") & "'").ToString)
                        End If



                        drow("curVol") = drow("lv")
                        drow("curDelVal") = drow("deltaval")
                        drow("curVegVal") = drow("vgval")
                        drow("curTheVal") = drow("thetaval")

                        'drow("curVol") = drow("lv")
                        'drow("curDelVal") = drow("deltaval")
                        'drow("curVegVal") = drow("vgval")
                        'drow("curTheVal") = drow("thetaval")
                    Else
                        If CBool(drow("isCurrency")) = True Then
                            drow("curSpot") = IIf(IsNothing(Currfltpprice(CLng(drow("ftoken")))), 0, Currfltpprice(CLng(drow("ftoken")))) 'Val(table.Compute("max(last)", "cp='F' and company = '" & drow("company") & "' And month ='" & drow("month") & "'").ToString)
                        Else
                            drow("curSpot") = IIf(IsNothing(fltpprice(CLng(drow("ftoken")))), 0, fltpprice(CLng(drow("ftoken")))) 'Val(table.Compute("max(last)", "cp='F' and company = '" & drow("company") & "' And month ='" & drow("month") & "'").ToString)
                        End If


                        'drow("curVol") = drow("lv")
                        'drow("curDelVal") = drow("deltaval")
                        'drow("curVegVal") = drow("vgval")
                        'drow("curTheVal") = drow("thetaval")
                        drow("curVol") = drow("lv")
                        drow("curDelVal") = drow("deltaval")
                        drow("curVegVal") = drow("vgval")
                        drow("curTheVal") = drow("thetaval")
                    End If
                End If
                table.AcceptChanges()
            Next

            objAna.delete_analysis() 'delete previous analysis data
            objAna.insert_analysis(table) 'insert data of maintable

        End If
    End Sub
    Private Function IsNothing(ByVal obj As Object) As Boolean
        Dim result As Boolean
        If obj Is Nothing Then
            result = True
        Else
            result = False
        End If
        Return result
    End Function
    ''' <summary>
    ''' cmdExport_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to Export Analysis into Excel</remarks>
    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '--------------------------------
        'date:13-2-2010 developed by keval chkalasiya
        Dim pageNo As Integer
        VarPicCnt = 0
        If tbcomp.TabPages.Count > 0 Then
            Dim ThisApplication As New Excel.Application
            Dim ThisWorkbook As Excel.Workbook
            Dim mWorkSheet As Excel.Worksheet
            Dim mShoDilog As New OpenFileDialog
            Dim mPath As String

            If FolderBrowserDialog1.ShowDialog() <> Windows.Forms.DialogResult.Cancel Then
                mPath = FolderBrowserDialog1.SelectedPath
                If mPath <> "" Then
                    Try
                        If Mid(mPath.Trim, mPath.Trim.Length, 1) = "\" Then 'if in filename path, last '/' is there ,ignore '/' from filename 
                            mPath = mPath & "Analysis " & Format(Now, "dd-MMM-yyyy hh mm ss tt") & ".xls"
                        Else
                            mPath = mPath & "\Analysis " & Format(Now, "dd-MMM-yyyy hh mm ss tt") & ".xls"
                        End If

                        ThisWorkbook = ThisApplication.Workbooks.Add
                        For pageNo = tbcomp.TabPages.Count - 1 To 0 Step -1
                            tbcomp.SelectedTab = tbcomp.TabPages(pageNo)
                            mWorkSheet = ThisWorkbook.Worksheets.Add
                            ThisWorkbook.Activate()
                            mWorkSheet = ThisWorkbook.ActiveSheet
                            Call formatExcelSheet(mWorkSheet)
                        Next
                        ThisApplication.ActiveWorkbook.SaveAs(mPath)

                        'mWorkbook.Close(True)

                        MsgBox("Export Completed Successfully.", MsgBoxStyle.Information)
                    Catch ex As Exception
                        MsgBox(ex.Message, MsgBoxStyle.Exclamation)
                    Finally
                        ThisWorkbook.Close()
                        ThisApplication.Quit()
                        releaseObject(ThisWorkbook)
                        releaseObject(ThisApplication)
                        ThisWorkbook = Nothing
                        ThisApplication = Nothing

                        'Dim proc As System.Diagnostics.Process
                        'For Each proc In System.Diagnostics.Process.GetProcessesByName("EXCEL")
                        '    proc.Kill()
                        'Next
                    End Try
                End If
            End If

        End If 'end of if tabpages.count>0
        '--------------------------------
        'end of date:13-2-2010 devseloped by keval chkalasiya
    End Sub
    ''' <summary>
    ''' releaseObject
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <remarks>This method call to releaseobject object as Passed</remarks>
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing

        Catch ex As Exception
            obj = Nothing
            MessageBox.Show("Exception Occured while releasing object " + ex.ToString())
        Finally
            GC.Collect()
        End Try
    End Sub
    ''' <summary>
    ''' CaptureScreen
    ''' </summary>
    ''' <remarks>This method call to capture and store into Application folder</remarks>
    Protected Sub CaptureScreen()
        Dim hsdc, hmdc As Integer
        Dim hbmp, hbmpold As Integer
        Dim r As Integer
        hsdc = CreateDC("DISPLAY", "", "", "")
        hmdc = CreateCompatibleDC(hsdc)
        'fw = GetDeviceCaps(hsdc, 8)
        'fh = GetDeviceCaps(hsdc, 10)
        fw = Me.Width
        fh = Me.Height
        hbmp = CreateCompatibleBitmap(hsdc, fw, fh)
        hbmpold = SelectObject(hmdc, hbmp)
        r = BitBlt(hmdc, 0, 0, fw, fh, hsdc, 0, 0, 13369376)

        hbmp = SelectObject(hmdc, hbmpold)
        r = DeleteDC(hsdc)
        r = DeleteDC(hmdc)
        Background = Image.FromHbitmap(New IntPtr(hbmp))
        DeleteObject(hbmp)
    End Sub
    Private Sub wait(ByVal interval As Integer)
        Dim sw As New Stopwatch
        sw.Start()
        Do While sw.ElapsedMilliseconds < interval
            ' Allows UI to remain responsive
            ' Application.DoEvents()
            SendKeys.Flush()
        Loop
        sw.Stop()
    End Sub
    ''' <summary>
    ''' formatExcelSheet
    ''' </summary>
    ''' <param name="mWorkSheet"></param>
    ''' <remarks>This method call to Create Excel File object fill all data</remarks>
    Private Sub formatExcelSheet(ByVal mWorkSheet As Excel.Worksheet)
        REM Theta value,Volga ,Vanna Export in Excel

        Dim i As Integer
        Dim j As Integer
        Dim start As Integer
        Dim row As Integer
        row = 2
        Dim cellRange As Excel.Range
        With mWorkSheet.Cells
            'scrip Name
            cellRange = mWorkSheet.Range("a" & row, "s2")
            cellRange.Merge()
            cellRange.HorizontalAlignment = Excel.Constants.xlCenter
            'company name: heading 
            mWorkSheet.Name = tbcomp.SelectedTab.Text.Replace("/", "-")
            'If tbcomp.SelectedTab.Text = "ORIENTBANK" Then
            '    MsgBox(tbcomp.SelectedTab.Text)
            'End If
            .Cells.Item(row, 1) = tbcomp.SelectedTab.Text
            .Range("a" & row).Font.Bold = True
            .Range("a" & row).Interior.ColorIndex = 35  'row background color

            row += 1
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1row:3
            'Date Row : formatting
            .Item(row, 1) = "Date"
            .Item(row, 3) = "Expiry/Days/Rate"
            .Item(row, 7) = "Col Val"
            .Item(row, 9) = "%"
            .Item(row, 10) = "Put Val"
            .Item(row, 12) = "%"
            .Item(row, 13) = "Future"
            .Range("a" & row).Font.Bold = True
            .Range("c" & row).Font.Bold = True
            .Range("g" & row).Font.Bold = True
            .Range("j" & row).Font.Bold = True
            .Range("m" & row).Font.Bold = True
            'value
            .Cells.Item(row, 2) = txtdttoday.Text
            'fill expiry/days/rate-1/call/put/future
            .Cells.Item(row, 4) = txtexp.Text
            .Cells.Item(row, 5) = txtdays.Text
            .Cells.Item(row, 6) = txtrate.Text
            .Cells.Item(row, 8) = txtcvol.Text
            .Cells.Item(row, 11) = txtpvol.Text
            .Cells.Item(row, 14) = txtfut1.Text
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1row:4
            If pan2.Visible = True Then
                row += 1
                .Item(row, 3) = "Expiry/Days/Rate"
                .Item(row, 7) = "Col Val"
                .Item(row, 9) = "%"
                .Item(row, 10) = "Put Val"
                .Item(row, 12) = "%"
                .Item(row, 13) = "Future"
                .Range("a" & row).Font.Bold = True
                .Range("c" & row).Font.Bold = True
                .Range("g" & row).Font.Bold = True
                .Range("j" & row).Font.Bold = True
                .Range("m" & row).Font.Bold = True
                .Cells.Item(row, 4) = txtexp1.Text
                .Cells.Item(row, 5) = txtdays1.Text
                .Cells.Item(row, 6) = txtrate1.Text
                .Cells.Item(row, 8) = txtcvol1.Text
                .Cells.Item(row, 11) = txtpvol1.Text
                .Cells.Item(row, 14) = txtfut2.Text
            End If
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1row:5
            If pan3.Visible = True Then
                row += 1
                .Item(row, 3) = "Expiry/Days/Rate"
                .Item(row, 7) = "Col Val"
                .Item(row, 9) = "%"
                .Item(row, 10) = "Put Val"
                .Item(row, 12) = "%"
                .Item(row, 13) = "Future"
                .Range("a" & row).Font.Bold = True
                .Range("c" & row).Font.Bold = True
                .Range("g" & row).Font.Bold = True
                .Range("j" & row).Font.Bold = True
                .Range("m" & row).Font.Bold = True
                'fill expiry/days/rate-3/call/put/future
                .Cells.Item(row, 4) = txtexp2.Text
                .Cells.Item(row, 5) = txtdays2.Text
                .Cells.Item(row, 6) = txtrate2.Text
                .Cells.Item(row, 8) = txtcvol2.Text
                .Cells.Item(row, 11) = txtpvol2.Text
                .Cells.Item(row, 14) = txtfut3.Text
            End If
            row = row + 1
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!1row:6
            'next row 
            .Item(row, 1) = "Maturity"
            .Item(row, 2) = "Strike"
            .Item(row, 3) = "Type"
            .Item(row, 4) = "Units"
            .Item(row, 5) = "Lots"
            .Item(row, 6) = "Net Price"
            .Item(row, 7) = "LTP"
            .Item(row, 8) = "Vol(%)"
            .Item(row, 9) = "Delta"
            .Item(row, 10) = "D.V."
            .Item(row, 11) = "Gamma"
            .Item(row, 12) = "G.V."
            .Item(row, 13) = "Vega"
            .Item(row, 14) = "V.V."
            .Item(row, 15) = "Theta"
            .Item(row, 16) = "T.V."
            .Item(row, 17) = "Volga"
            .Item(row, 18) = "Vo.V."
            .Item(row, 19) = "Vanna"
            .Item(row, 20) = "Va.V."
            .Item(row, 21) = "l lliq."
            .Item(row, 22) = "GrossMTM"
            .Item(row, 23) = "Remarks"
            .Range("a" & row & ":w" & row).Font.Bold = True
            .Range("a" & row & ":w" & row).Interior.ColorIndex = 35
            .Range("a" & row & ":w" & row).HorizontalAlignment = Excel.Constants.xlCenter
            start = row
            row = row + 1
            'fill values from grdtrad grid to excel cell
            For i = 0 To DGTrading.Rows.Count - 1
                .Cells.Item(row + i, 1) = DGTrading.Rows(i).Cells(0).Value
                .Cells.Item(row + i, 2) = DGTrading.Rows(i).Cells(1).Value
                .Cells.Item(row + i, 3) = DGTrading.Rows(i).Cells(2).Value
                For j = 5 To 17
                    .Cells.Item(row + i, j - 1) = Math.Round(Val(DGTrading.Rows(i).Cells(j).Value.ToString), 2)
                Next j
                REM For Volga To Va.V.
                For j = 18 To 21
                    .Cells.Item(row + i, j - 1) = Math.Round(Val(DGTrading.Rows(i).Cells(j).Value.ToString), 5)
                Next j
                '.Cells.Item(row + i, 17) = DGTrading.Rows(i).Cells(24).Value   'l lliq.
                '.Cells.Item(row + i, 18) = DGTrading.Rows(i).Cells(41).Value   'GrossMTM
                '.Cells.Item(row + i, 19) = DGTrading.Rows(i).Cells(43).Value   'Remarks
                .Cells.Item(row + i, 21) = DGTrading.Rows(i).Cells(28).Value   'l lliq.
                .Cells.Item(row + i, 22) = DGTrading.Rows(i).Cells(47).Value   'GrossMTM
                .Cells.Item(row + i, 23) = DGTrading.Rows(i).Cells(49).Value   'Remarks
            Next i

            row += i + 3
            .Range("c" & start & ":c" & row).HorizontalAlignment = Excel.Constants.xlCenter
            mWorkSheet.Range("a" & start & ":w" & row - 4).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium)
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            .Item(row, 5) = "Delta Value"
            .Item(row, 6) = "Gamma Value"
            .Item(row, 7) = "Vega Value"
            .Item(row, 8) = "Theta Value"
            .Item(row, 9) = "Volga Value"
            .Item(row, 10) = "Vanna Value"
            .Range("e" & row & ":j" & row).Font.Bold = True
            .Range("e" & row & ":j" & row).Interior.ColorIndex = 35
            mWorkSheet.Range("c" & row & ":j" & row + 8).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium)
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            REM Export Volgaval and  Vannaval in Excel
            row += 1
            .Range("c" & row - 1 & ":d" & row + 7).Interior.ColorIndex = 35
            cellRange = mWorkSheet.Range("c" & row, "c" & row + 1)
            cellRange.Merge()
            .Item(row, 3) = "Current"
            .Item(row, 4) = "Today"
            .Range("c" & row & ":d" & row).Font.Bold = True
            .Cells.Item(row, 5) = txtdelval_1.Text
            .Cells.Item(row, 6) = txtgmval_1.Text
            .Cells.Item(row, 7) = txtwegaval_1.Text
            .Cells.Item(row, 8) = txtthetaval_1.Text
            .Cells.Item(row, 9) = txtvolgaval_1.Text
            .Cells.Item(row, 10) = txtvannaval_1.Text

            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            .Item(row, 4) = "Next Day"
            .Cells.Item(row, 5) = txttdelval_1.Text
            .Cells.Item(row, 6) = txttgmval_1.Text
            .Cells.Item(row, 7) = txttwegaval_1.Text
            .Cells.Item(row, 8) = txttthetaval_1.Text
            .Cells.Item(row, 9) = txttvolgaval_1.Text
            .Cells.Item(row, 10) = txttvannaval_1.Text

            .Range("d" & row).Font.Bold = True
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            cellRange = mWorkSheet.Range("c" & row, "c" & row + 1)
            cellRange.Merge()
            .Item(row, 3) = "Current + 1"
            .Item(row, 4) = "Today"
            .Range("c" & row & ":d" & row).Font.Bold = True
            .Cells.Item(row, 5) = txtdelval_2.Text
            .Cells.Item(row, 6) = txtgmval_2.Text
            .Cells.Item(row, 7) = txtwegaval_2.Text
            .Cells.Item(row, 8) = txtthetaval_2.Text
            .Cells.Item(row, 9) = txtvolgaval_2.Text
            .Cells.Item(row, 10) = txtvannaval_2.Text

            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            .Item(row, 4) = "Next Day"
            .Range("d" & row).Font.Bold = True
            .Cells.Item(row, 5) = txttdelval_2.Text
            .Cells.Item(row, 6) = txttgmval_2.Text
            .Cells.Item(row, 7) = txttwegaval_2.Text
            .Cells.Item(row, 8) = txttthetaval_2.Text
            .Cells.Item(row, 9) = txttvolgaval_2.Text
            .Cells.Item(row, 10) = txttvannaval_2.Text

            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            cellRange = mWorkSheet.Range("c" & row, "c" & row + 1)
            cellRange.Merge()
            .Item(row, 3) = "Current + 2"
            .Item(row, 4) = "Today"
            .Range("c" & row & ":d" & row).Font.Bold = True
            .Cells.Item(row, 5) = txtdelval_3.Text
            .Cells.Item(row, 6) = txtgmval_3.Text
            .Cells.Item(row, 7) = txtwegaval_3.Text
            .Cells.Item(row, 8) = txtthetaval_3.Text
            .Cells.Item(row, 9) = txtvolgaval_3.Text
            .Cells.Item(row, 10) = txtvannaval_3.Text

            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            .Item(row, 4) = "Next Day"
            .Range("d" & row).Font.Bold = True
            .Cells.Item(row, 5) = txttdelval_3.Text
            .Cells.Item(row, 6) = txttgmval_3.Text
            .Cells.Item(row, 7) = txttwegaval_3.Text
            .Cells.Item(row, 8) = txttthetaval_3.Text
            .Cells.Item(row, 9) = txttvolgaval_3.Text
            .Cells.Item(row, 10) = txttvannaval_3.Text

            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            cellRange = mWorkSheet.Range("c" & row, "c" & row + 1)
            cellRange.Merge()
            .Item(row, 3) = "Far Month"
            .Item(row, 4) = "Today"
            .Range("c" & row & ":d" & row).Font.Bold = True
            .Cells.Item(row, 5) = txtdelval_4.Text
            .Cells.Item(row, 6) = txtgmval_4.Text
            .Cells.Item(row, 7) = txtwegaval_4.Text
            .Cells.Item(row, 8) = txtthetaval_4.Text
            .Cells.Item(row, 9) = txtvolgaval_4.Text
            .Cells.Item(row, 10) = txtvannaval_4.Text

            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            .Item(row, 4) = "Next Day"
            .Range("d" & row).Font.Bold = True
            .Cells.Item(row, 5) = txttdelval_4.Text
            .Cells.Item(row, 6) = txttgmval_4.Text
            .Cells.Item(row, 7) = txttwegaval_4.Text
            .Cells.Item(row, 8) = txttthetaval_4.Text
            .Cells.Item(row, 9) = txttvolgaval_4.Text
            .Cells.Item(row, 10) = txttvannaval_4.Text

            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 2
            .Range("c" & row & ":d" & row + 5).Interior.ColorIndex = 35
            mWorkSheet.Range("c" & row & ":d" & row + 5).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium)
            .Range("g" & row & ":h" & row + 3).Interior.ColorIndex = 35
            mWorkSheet.Range("g" & row & ":h" & row + 3).BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium)
            .Item(row, 4) = "Total"
            .Item(row, 7) = "Margin Detail"
            .Range("g" & row & ":h" & row).Merge()
            .Range("d" & row).Font.Bold = True
            .Range("g" & row).Font.Bold = True
            .Range("g" & row).HorizontalAlignment = Excel.Constants.xlCenter
            .Range("d" & row).HorizontalAlignment = Excel.Constants.xlCenter
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            .Item(row, 3) = "Gross MTM"
            .Range("c" & row).Font.Bold = True
            .Range("g" & row).Font.Bold = True
            .Cells.Item(row, 4) = txttotGmtm.Text
            .Item(row, 7) = "Intial Mar"
            .Cells.Item(row, 8) = txtintmrg.Text 'DouIntMrg

            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            .Item(row, 3) = "Expense"
            .Cells.Item(row, 4) = txttotexp.Text
            .Item(row, 7) = "Expo.Mar"
            .Cells.Item(row, 8) = txtexpmrg.Text 'DouExpMrg
            .Range("c" & row).Font.Bold = True
            .Range("g" & row).Font.Bold = True
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            .Item(row, 3) = "Current MTM"
            .Cells.Item(row, 4) = Val(txttotGmtm.Text) + Val(txttotexp.Text) 'txttotnetmtm.Text
            .Item(row, 7) = "Equity"
            .Cells.Item(row, 8) = txtEquity.Text 'DouEquity
            .Range("c" & row).Font.Bold = True
            .Range("g" & row).Font.Bold = True
            '!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            row += 1
            'sq of exp, proj. MTM
            .Item(row, 3) = "Proj. MTM"
            .Range("c" & row).Font.Bold = True
            .Item(row, 4) = txttotsqmtm.Text

            row += 1
            'sq of exp, proj. MTM
            .Item(row, 3) = "Sq. Off Exp."
            .Range("c" & row).Font.Bold = True
            .Item(row, 4) = txttotsqexp.Text
        End With
        mWorkSheet.Columns("c:j").columnWidth = 15
        mWorkSheet.Columns("v:w").columnWidth = 15
        mWorkSheet.Rows("2:" & row).rowHeight = 15
        wait(1000)
        ' System.Threading.Thread.Sleep(5000)
        'take screen shot and save to excel sheet
        'first screen shot will be saved in current directory byname Capture.jpg
        CaptureScreen()
        VarPicCnt += 1
        Dim PictureBox1 As New PictureBox
        PictureBox1.Image = Background
        If File.Exists(Application.StartupPath & "\Capture" & VarPicCnt & ".jpg") Then
            File.Delete(Application.StartupPath & "\Capture" & VarPicCnt & ".jpg")
        End If
        PictureBox1.Image.Save(Application.StartupPath & "\Capture" & VarPicCnt & ".jpg")
        Dim strImage As String = Application.StartupPath & "\Capture" & VarPicCnt & ".jpg"
        mWorkSheet.Shapes.AddPicture(strImage, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 5, row * 16, 800, 600)
    End Sub
    ''' <summary>
    ''' scenario
    ''' </summary>
    ''' <remarks>This method call to display Scenario form according to select company</remarks>
    Private Sub scenario()
        'REM: want to transfer GrossMTM to scenario
        Dim grossMTM As Double
        If MsgBox("Want to transfer Gross MTM to scenario?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
            grossMTM = Val(txttotGmtm.Text)
        Else
            grossMTM = 0
        End If

        REM: if no position of FO then give message and exit
        If gcurrtable.Rows.Count <= 0 Then
            MsgBox("No Call, Put or Fut. data in selected tab!!", MsgBoxStyle.Exclamation)
            Dim scenario As New scenario1

            If scenario.chkscenario = False Then 'if scenario's any instance is not opened
                scenario.MdiParent = Me.MdiParent
                scenario.ShowForm(True)
                Call searchcompany()
            Else
                MDI.ToolStripMenuSearchComp.Visible = True
                MDI.ToolStripcompanyCombo.Visible = True
                scenario.Dispose()
            End If
            Exit Sub
        End If
        REM :initialize scenario table
        init_scenario()

        Dim drow As DataRow
        Dim i As Integer
        i = 1
        REM: add FO position to scenario table whose quantity<>0
        For Each row As DataRow In currtable.Select("units <> 0", "cp,mdate") 'Select(" cp <> 'E' and units <> 0", "CP")
            drow = scenariotable.NewRow
            drow("status") = True
            drow("time1") = dttoday.Value.Date
            If row("cp") = "E" Then
                drow("time2") = dtexp.Value.Date
            Else
                drow("time2") = CDate(row("mdate")).Date
            End If
            'drow("time2") = CDate(row("mdate")).Date
            drow("cpf") = row("cp")
            drow("spot") = 0
            'If drow("cpf") = "F" Or drow("cpf") = "X" Then
            If Not drow("cpf") = "E" Then
                If pan1.Visible = True Then
                    If CDate(row("mdate")).Date = dtexp.Value.Date Then
                        drow("spot") = Val(txtrate.Text)
                    End If
                End If
                If pan2.Visible = True Then
                    If CDate(row("mdate")).Date = dtexp1.Value.Date Then
                        drow("spot") = Val(txtrate1.Text)
                    End If
                End If
                If pan3.Visible = True Then
                    If CDate(row("mdate")).Date = dtexp2.Value.Date Then
                        drow("spot") = Val(txtrate2.Text)
                    End If
                End If
                If Val(drow("spot") & "") = 0 Then
                    'If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                    '    drow("spot") = Val(txtrate.Text)
                    'ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                    '    drow("spot") = Val(txtrate1.Text)
                    'ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                    '    drow("spot") = Val(txtrate2.Text)
                    'End If
                    Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(row("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                    Dim ftoken As Long
                    If DtFMonthDate.Rows.Count > 0 Then
                        If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                            ftoken = DtFMonthDate.Rows(0)("token")
                            'dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                        ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                            ftoken = DtFMonthDate.Rows(1)("token")
                            'dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                        ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                            ftoken = DtFMonthDate.Rows(2)("token")
                            'dr("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                        End If
                    End If
                    drow("spot") = Val(fltpprice(ftoken))

                End If
            Else
                drow("spot") = txteqrate.Text
            End If
            drow("strikes") = Val(row("Strikes"))
            drow("qty") = Val(row("units"))

            Dim VarLTPPrice As Double = gcurrtable.Compute("Max(last)", "script='" & row("script") & "'")
            drow("rate") = VarLTPPrice 'Val(row("last"))
            drow("ltp") = VarLTPPrice 'Val(row("last"))
            'drow("rate") = Val(row("last"))
            'drow("ltp") = Val(row("last"))
            If Val(row("lv")) = 0 Then
                If row("CP").ToString <> "F" Then
                    drow("vol") = 1
                Else
                    drow("vol") = Val(row("lv"))
                End If
            Else
                drow("vol") = Val(row("lv"))
            End If

            drow("delta") = Val(row("delta").ToString)
            drow("deltaval") = Val(row("deltaval") & "")
            drow("theta") = Val(row("theta"))
            drow("thetaval") = Val(row("thetaval"))
            drow("vega") = Val(row("vega"))
            drow("vgval") = Val(row("vgval"))
            drow("gamma") = Val(row("gamma"))
            drow("gmval") = Val(row("gmval"))
            drow("volga") = Val(row("volga"))
            drow("volgaval") = Val(row("volgaval"))
            drow("vanna") = Val(row("vanna"))
            drow("vannaval") = Val(row("vannaval"))

            ''divyesh
            '' assign deltaval1,thetaval1,vgval1,gmval1 and ltp1 value
            drow("deltaval1") = Val(row("deltaval1") & "")
            drow("thetaval1") = Val(row("thetaval1").ToString)
            drow("vgval1") = Val(row("vgval1"))
            drow("gmval1") = Val(row("gmval1"))
            drow("volgaval1") = Val(row("volgaval1"))
            drow("vannaval1") = Val(row("vannaval1"))

            drow("ltp1") = Val(row("last"))

            drow("uid") = i
            drow("grossMTM") = grossMTM
            drow("DifFactor") = 0
            scenariotable.Rows.Add(drow)
            i += 1
        Next

        If scenariotable.Rows.Count > 0 Then
            Dim scenario As New scenario1
            scenario.MdiParent = Me.MdiParent
            scenario.trscen = True 'used to check scenario is opened from analysis or not
            scenario.time1 = dttoday.Value.Date
            scenario.time2 = CDate(scenariotable.Compute("max(time2)", "")).Date 'dtexp.Value.Date
            REM: if startdate = expirydate then set scenario.time2= expirydate
            REM Checking In Scenario window, Days value is displayed for Apr 11 expiry where as CMP value is displayed for Mar 11 expiry. 
            If dttoday.Value.Date = dtexp.Value.Date Then
                Dim dv As DataView = New DataView(scripttable, "mo = '" & Format(CDate(DateAdd(DateInterval.Month, 1, dtexp.Value.Date)), "MM/yyyy") & "' and symbol='" & compname & "'", "", DataViewRowState.CurrentRows)
                If dv.ToTable.Rows.Count > 0 Then
                    For Each drow1 As DataRow In dv.ToTable.Rows
                        scenario.time2 = CDate(drow1("expdate1")).Date
                        Exit For
                    Next
                End If
            End If

            scenario.mvalue = Val(txtrate.Text)
            'scenario.mAllCV = ""
            'refreshstarted = False
            scenario.scname = tbcomp.SelectedTab.Text
            scenario.ShowForm(True)
        Else
            MsgBox("No Call, Put or Fut. data in selected tab!!!", MsgBoxStyle.Exclamation)
            'Me.Hide()
        End If
    End Sub
    ''' <summary>
    ''' openposition
    ''' </summary>
    ''' <remarks>This method call to display Open Position Form</remarks>
    Private Sub openposition()
        '  Dim opp As New scriptgenrate
        'Dim mStart As String
        Dim opp As New OpenPosition
        Timer_Calculation.Stop()
        opp.openposition = True 'used for at a time only one openposition form is open
        'opp.ShowDialog() 'open openposition form
        If tbcomp.TabPages.Count <= 0 Then
            opp.ShowForm("", "")
        Else
            opp.ShowForm(tbcomp.SelectedTab.Name, tbcomp.SelectedTab.Name)
        End If

        If opp.openposyes = False Then
            Timer_Calculation.Start()
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        System.Threading.Thread.Sleep(500) ' sleep thread to stop work

        '*********************************************************
        REM: Add company of new added positions to tabs
        '*********************************************************
        comptable = objTrad.Comapany
        comptable_Net = objTrad.Comapany_Net
        'mStart = cmdStart.Text

        If LastOpenPosition = "" Then
            Call AddRemoveTab(comptable)
            If tbcomp.TabPages.ContainsKey("NIFTY") = True Then
                compname = "NIFTY"
                Call Fill_StrategyTab_MonthWise()
                change_tab(compname, tbmo)
                tbcomp.SelectedTab = tbcomp.TabPages(compname)
            End If
            'If tbcomp.TabPages.Count <= 0 Then 'if previously no company to tabs
            '    Dim i As Integer
            '    i = 0
            '    'Add company to tabs and if NIFTY is present then select it
            '    For Each drow As DataRow In comptable.Rows 'noone company's position exists
            '        tbcomp.TabPages.Add(drow("company"))
            '        tbcomp.TabPages.Item(i).Name = drow("company")
            '        If UCase(drow("company")) = "NIFTY" Then
            '            chknifty = True
            '            compname = "NIFTY"
            '            ind = i
            '        End If
            '        i += 1
            '    Next
            'End If
        Else
            Call AddRemoveTab(comptable)
            compname = LastOpenPosition
            Call Fill_StrategyTab_MonthWise(True)
            change_tab(compname, tbmo)
            tbcomp.SelectedTab = tbcomp.TabPages(compname)
            LastOpenPosition = ""
        End If

        'SetStartStop(mStart)

        ''If tbcomp.TabPages.Count <= 0 Then 'if previously no company to tabs
        ''    Dim i As Integer
        ''    i = 0
        ''    Add company to tabs and if NIFTY is present then select it
        ''    For Each drow As DataRow In comptable.Rows 'noone company's position exists
        ''        tbcomp.TabPages.Add(drow("company"))
        ''        tbcomp.TabPages.Item(i).Name = drow("company")
        ''        If UCase(drow("company")) = "NIFTY" Then
        ''            chknifty = True
        ''            compname = "NIFTY"
        ''            ind = i
        ''        End If
        ''        i += 1
        ''    Next
        ''ElseIf comptable.Rows.Count > tbcomp.TabCount Then 'if new company's position is added
        ''    clear the tabs
        ''    tbcomp.TabPages.Clear()
        ''    Dim i As Integer
        ''    i = 0
        ''    Add company to tabs and if NIFTY is present then select it
        ''    For Each drow As DataRow In comptable.Rows
        ''        tbcomp.TabPages.Add(drow("company"))
        ''        tbcomp.TabPages.Item(i).Name = drow("company")
        ''        If UCase(drow("company")) = "NIFTY" Then
        ''            chknifty = True
        ''            compname = "NIFTY"
        ''            ind = i
        ''        End If
        ''        i += 1
        ''    Next
        ''End If

        ''If LastOpenPosition <> "" Then
        ''    tbcomp.SelectedTab = tbcomp.TabPages(LastOpenPosition)
        ''    change_tab(compname, tbmo)
        ''    LastOpenPosition = ""
        ''End If

        'refill maintable,currtable,gcurrtable
        'changes Done by Nasima on 10th Aug
        'fill_equity_dtable()


        REM: if NIFTY present then changetab to NIFTY
        'If tbcomp.TabPages.Count > 0 Then
        '    If maintable.Compute("count(company)", "company='" & compname & "'") > 0 Then
        '        tbcomp.SelectedIndex = ind
        '        change_tab(compname, tbmo)
        '    Else 'select first company from companylist
        '        compname = ""
        '        If tbcomp.TabPages.Count > 0 Then
        '            'tbcomp.TabPages.RemoveAt(ind1)
        '            tbcomp.SelectedIndex = 0
        '            compname = tbcomp.SelectedTab.Name.ToString
        '        End If
        '        change_tab(compname, tbmo)
        '        'tbcomp.SelectedTab = tbcomp.TabPages(LastOpenPosition)
        '    End If
        'End If


        Me.Cursor = Cursors.Default
        Timer_Calculation.Start()
    End Sub
    ''' <summary>
    ''' AddRemoveTab
    ''' </summary>
    ''' <param name="DTComp"></param>
    ''' <remarks>This method call to Fill company Tab Control</remarks>
    Private Sub AddRemoveTab(ByVal DTComp As DataTable)
        VarIsTabAddRemove = True
        For Each dr As DataRow In DTComp.Rows
            If tbcomp.TabPages.ContainsKey(dr("company")) = False Then
                tbcomp.TabPages.Add(dr("company"), dr("company"))
                tbcomp.TabPages(dr("company").ToString).Name = dr("company")
                tbcomp.TabPages(dr("company").ToString).Tag = dr("company")
            End If
        Next
        VarIsTabAddRemove = False
        If NetMode = "TCP" Then
            Call ReFreshToken()
        End If
    End Sub
    Private Sub ReFreshToken()
        If bSqlValidated = True Then
            Dim dv As DataView
            Objsql.DeleteFoToken()
            Objsql.DeleteEqToken()
            Objsql.DeleteCurToken()
            Dim FoTokens As New Hashtable
            Dim EqTokens As New Hashtable
            Dim CurTokens As New Hashtable

            'Dim FoTokenArr() As String

            Dim strFoTokens As String = ""
            Dim strEqTokens As String = ""
            Dim strCurTokens As String = ""

            dv = New DataView(maintable, "", "", DataViewRowState.CurrentRows)
            For Each dr As DataRow In dv.ToTable().Rows
                If dr("ISCurrency") = True Then
                    If Not CurTokens.Contains(Val(dr("Tokanno") & "")) Then
                        CurTokens.Add(Val(dr("Tokanno") & ""), Val(dr("Tokanno") & ""))
                    End If
                    If Not CurTokens.Contains(Val(dr("fToken") & "")) Then
                        CurTokens.Add(Val(dr("fToken") & ""), Val(dr("fToken") & ""))
                    End If
                Else
                    If dr("cp") = "E" Then
                        If Not EqTokens.Contains(Val(dr("Tokanno") & "")) Then
                            EqTokens.Add(Val(dr("Tokanno") & ""), Val(dr("Tokanno") & ""))
                        End If
                        If Not EqTokens.Contains(Val(dr("fToken") & "")) Then
                            EqTokens.Add(Val(dr("fToken") & ""), Val(dr("fToken") & ""))
                        End If
                    Else
                        If Not FoTokens.Contains(Val(dr("Tokanno") & "")) Then
                            FoTokens.Add(Val(dr("Tokanno") & ""), Val(dr("Tokanno") & ""))
                        End If
                        If Not FoTokens.Contains(Val(dr("fToken") & "")) Then
                            FoTokens.Add(Val(dr("fToken") & ""), Val(dr("fToken") & ""))
                        End If
                    End If
                End If
            Next


            For Each item As DictionaryEntry In FoTokens
                strFoTokens = strFoTokens & IIf(strFoTokens.Length > 0, ",", "") & item.Value.ToString()
            Next
            For Each item As DictionaryEntry In EqTokens
                strEqTokens = strEqTokens & IIf(strEqTokens.Length > 0, ",", "") & item.Value.ToString()
            Next
            For Each item As DictionaryEntry In CurTokens
                strCurTokens = strCurTokens & IIf(strCurTokens.Length > 0, ",", "") & item.Value.ToString()
            Next

            Objsql.AppendCurTokens(IIf(strCurTokens.Length = 0, "0", strCurTokens))
            Objsql.AppendEQTokens(IIf(strEqTokens.Length = 0, "0", strEqTokens))
            Objsql.AppendFoTokens(IIf(strFoTokens.Length = 0, "0", strFoTokens))
        End If
    End Sub
    '''''' <summary>
    '''''' connect_server
    '''''' </summary>
    '''''' <remarks>This method call to Init. FO broadcast</remarks>
    ' ''Private Sub connect_server()
    ' ''    Try
    ' ''        If multicastListener_fo Is Nothing Then
    ' ''            initialize_fo_broadcast()
    ' ''        End If
    ' ''    Catch ex As Exception
    ' ''    End Try
    ' ''End Sub
    '''''' <summary>
    '''''' disconnect_server
    '''''' </summary>
    '''''' <remarks>This method call to stop FO broadcast</remarks>
    ' ''Private Sub disconnect_server()
    ' ''    Try
    ' ''        'ThreadReceive_fo.Suspend()
    ' ''        ThreadReceive_fo.Abort()
    ' ''        If Not multicastListener_fo Is Nothing Then
    ' ''            multicastListener_fo.Close()
    ' ''            multicastListener_fo = Nothing
    ' ''        End If
    ' ''    Catch ex As Exception

    ' ''    End Try
    ' ''End Sub

    ''''''''' <summary>
    ''''''''' cmdudpstrart_Click
    ''''''''' </summary>
    ''''''''' <param name="sender"></param>
    ''''''''' <param name="e"></param>
    ''''''''' <remarks>This method call to start FO broadcast</remarks>

    '' '' ''Private Sub cmdudpstrart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '' '' ''    Call connect_server()
    '' '' ''End Sub
    ''''''''' <summary>
    ''''''''' cmdstop_Click
    ''''''''' </summary>
    ''''''''' <param name="sender"></param>
    ''''''''' <param name="e"></param>
    ''''''''' <remarks>This method call to Stop FO broadcast</remarks>
    '' '' ''Private Sub cmdstop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdstop.Click
    '' '' ''    Call disconnect_server()
    '' '' ''End Sub

    ''' <summary>
    ''' Timer_refresh_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to import refresh timer fire then refresh import source</remarks>
    Private Sub Timer_refresh_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_refresh.Tick
        REM Broadcast receiving Not stopped - trade addition from trade file using Auto Refresh
        'Dim mStart As String
        Try
            If isRefresh = False Then 'refresh process is not running
                'mStart = cmdStart.Text
                isRefresh = True 'start refresh
                Call proc_data(True)
                isRefresh = False 'end of refresh
                'Call SetStartStop(mStart)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    '''''''' <summary>
    '''''''' chkRefresh_CheckedChanged
    '''''''' </summary>
    '''''''' <param name="sender"></param>
    '''''''' <param name="e"></param>
    '''''''' <remarks></remarks>
    ' '' ''Private Sub chkRefresh_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ' '' ''    Dim rtime As Double
    ' '' ''    If chkRefresh.Checked = True Then
    ' '' ''        chkRefresh.Text = "Refresh Start"
    ' '' ''        Timer_refresh.Stop() 'stop refresh timer
    ' '' ''    Else
    ' '' ''        chkRefresh.Text = "Refresh Stop"
    ' '' ''        'set interval of refresh timer
    ' '' ''        rtime = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='Refresh_time'").ToString)
    ' '' ''        If rtime = 0 Then
    ' '' ''            rtime = 30
    ' '' ''        End If
    ' '' ''        Timer_refresh.Interval = rtime * 1000
    ' '' ''        Timer_refresh.Start()
    ' '' ''    End If
    ' '' ''End Sub
    ''' <summary>
    ''' cmdstart_future_update_click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to update start and stop future mode</remarks>
    Private Sub cmdstart_future_update_click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdstart_future_update.Click
        start_stop_future_only()
    End Sub

    Private Sub chkCalVol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCalVol.CheckedChanged
        If currtable.Rows.Count = 0 Then Exit Sub
        REM: if INDEX then no need to check cal vol
        'for INDEX no option for 'calculate vol using equity'
        If currtable.Rows.Count > 0 Then
            If currtable.Rows(0).Item("script").ToString.Substring(3, 3) = "IDX" Then
                Exit Sub
            End If
        End If
        '***************************************************************
        'equityVol Hashtable contains(company,checkvol status)
        'used during changetab.
        'check if company and its chkcalvol's value is stored or not
        '***************************************************************
        If equityVol.Contains(compname) = False Then
            equityVol.Add(compname, chkCalVol.Checked)
        Else
            equityVol(compname) = chkCalVol.Checked
        End If

        Call cal_future()
        'If chkCalVol.Checked = True Then
        '    txteqrate.Visible = True
        '    lbleq.Visible = True
        'Else
        '    txteqrate.Text = "0" 
        '    txteqrate.Visible = False
        '    lbleq.Visible = False

        'End If
        If thrworking = True Then
            thrworking = False
            Call cmdsave_Click()
            thrworking = True
        Else
            Call cmdsave_Click()
        End If
    End Sub
    ''' <summary>
    ''' UpdateAutomatic
    ''' </summary>
    ''' <remarks>This method Set Offline value</remarks>
    Private Sub UpdateAutomatic(ByVal Flg As Boolean)
        txtcvol.Text = Val(txtcvol.Text)
        txtcvol1.Text = Val(txtcvol1.Text)
        txtcvol2.Text = Val(txtcvol2.Text)

        txtpvol.Text = Val(txtpvol.Text)
        txtpvol1.Text = Val(txtpvol1.Text)
        txtpvol2.Text = Val(txtpvol2.Text)

        txtfut1.Text = Val(txtfut1.Text)
        txtfut2.Text = Val(txtfut2.Text)
        txtfut3.Text = Val(txtfut3.Text)

        If (Val(txtcvol.Text) <> 0 And Val(txtpvol.Text) = 0) Then
            txtpvol.Text = txtcvol.Text
        End If
        If (Val(txtcvol.Text) = 0 And Val(txtpvol.Text) <> 0) Then
            txtcvol.Text = txtpvol.Text
        End If

        If (Val(txtfut1.Text) <> 0 And Val(txteqrate.Text) = 0) Then
            txteqrate.Text = txtfut1.Text
        End If

        'If (Val(txtfut1.Text) = 0 And Val(txteqrate.Text) <> 0) Then
        '    txtfut1.Text = txteqrate.Text
        'End If

        If (Val(txtfut1.Text) = 0 And Val(txteqrate.Text) <> 0 And txteqrate.Visible = True) Then
            txtfut1.Text = txteqrate.Text
            'Else
            'txtfut1.Text = 0
        End If

        If pan2.Visible = True Then
            If (Val(txtcvol1.Text) <> 0 And Val(txtpvol1.Text) = 0) Then
                txtpvol1.Text = txtcvol1.Text
            End If
            If (Val(txtcvol1.Text) = 0 And Val(txtpvol1.Text) <> 0) Then
                txtcvol1.Text = txtpvol1.Text
            End If
        End If

        If pan3.Visible = True Then
            If (txtcvol2.Text <> 0 And txtpvol2.Text = 0) Then
                txtpvol2.Text = txtcvol2.Text
            End If
            If (txtcvol2.Text = 0 And txtpvol2.Text <> 0) Then
                txtcvol2.Text = txtpvol2.Text
            End If
        End If

        If pan3.Visible = True Then
            'If ((Val(txtfut3.Text) <> 0 And Val(txtcvol2.Text) <> 0) Or (Val(txtfut3.Text) = 0 And Val(txtcvol2.Text) = 0)) And ((Val(txtfut2.Text) <> 0 And Val(txtcvol1.Text) <> 0) Or (Val(txtfut2.Text) = 0 And Val(txtcvol1.Text) = 0)) And ((Val(txtfut1.Text) <> 0 And Val(txtcvol.Text) <> 0) Or (Val(txtfut1.Text) = 0 And Val(txtcvol.Text) = 0)) Then
            'If ((Val(txtfut3.Text) <> 0 And Val(txtcvol2.Text) <> 0) Or (Val(txtfut3.Text) = 0 And Val(txtcvol2.Text) = 0)) Then
            If ((Val(txtfut3.Text) <> 0 And Val(txtcvol2.Text) <> 0)) Then 'Or (Val(txtfut3.Text) = 0 And Val(txtcvol2.Text) = 0)
                If thrworking_future_only = True Then
                    start_stop_future_only()
                Else
                    thrworking = True
                    If Flg = True Then
                        Call start_stop()
                    End If
                End If
                Call cmdsave_Click()
            End If
        ElseIf pan2.Visible = True Then
            'If ((Val(txtfut2.Text) <> 0 And Val(txtcvol1.Text) <> 0) Or (Val(txtfut2.Text) = 0 And Val(txtcvol1.Text) = 0)) And ((Val(txtfut1.Text) <> 0 And Val(txtcvol.Text) <> 0) Or (Val(txtfut1.Text) = 0 And Val(txtcvol.Text) = 0)) Then
            'If ((Val(txtfut2.Text) <> 0 And Val(txtcvol1.Text) <> 0) Or (Val(txtfut2.Text) = 0 And Val(txtcvol1.Text) = 0)) Then
            If ((Val(txtfut2.Text) <> 0 And Val(txtcvol1.Text) <> 0)) Then 'Or (Val(txtfut2.Text) = 0 And Val(txtcvol1.Text) = 0)
                If thrworking_future_only = True Then
                    start_stop_future_only()
                Else
                    thrworking = True
                    If Flg = True Then
                        Call start_stop()
                    End If
                End If
                Call cmdsave_Click()
            End If
        Else
            If Val(txtcvol.Text) <> 0 And Val(txtfut1.Text) <> 0 Then  REM Jignesh And changeVal1 = True Then
                If thrworking_future_only = True Then
                    start_stop_future_only()
                Else
                    thrworking = True
                    If Flg = True Then
                        Call start_stop()
                    End If
                End If
                Call cmdsave_Click()
            End If
        End If
        changeVal1 = False
    End Sub

    Private Sub txtcvol_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcvol.KeyPress, txtcvol1.KeyPress, txtcvol2.KeyPress, txtpvol.KeyPress, txtfut1.KeyPress, txtfut2.KeyPress, txtfut3.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Call UpdateAutomatic(True)
            SendKeys.Send("{HOME}+{END}")
        ElseIf e.KeyChar < "0" Or e.KeyChar > "9" Then
            If e.KeyChar <> "." And Asc(e.KeyChar) <> 8 Then
                e.KeyChar = ""
            End If
        Else
            changeVal1 = True
        End If
    End Sub

    Private Sub txtcvol_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcvol.LostFocus
        If changeVal1 = True Then UpdateAutomatic(True)
    End Sub

    Private Sub chkLtp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVol.CheckedChanged, chkLtp.CheckedChanged
        If chkLtp.Checked = True Or chkVol.Checked = True Then
            Call cal_projMTM_plusMinus("P")
            Call cal_projMTM_plusMinus("M")
        End If
    End Sub
    ''' <summary>
    ''' cal_projMTM_plusMinus
    ''' </summary>
    ''' <remarks>This method call to calculate Plus Minus Proj. MtoM</remarks>
    Private Sub cal_projMTM_plusMinus(ByVal sTyp As String)
        Try
            Dim ht_TMPLTPPrice As New Hashtable

            Dim ltppr As Double = 0
            Dim ltppr1 As Double = 0

            Dim fltppr As Double = 0
            Dim mt As Double = 0
            Dim mmt As Double = 0
            Dim isfut As Boolean = False
            Dim iscall As Boolean = False

            Dim token As Long
            Dim token1 As Long
            ' Dim dt As Date
            Dim cnt As Integer
            'Dim type = "plus"
            Dim pLtp, nLtp, pVol, nVol As Double
            If chkVol.Checked = True Then
                pVol = Val(txtVolVal.Text)
                nVol = Val(txtVolVal1.Text)
            Else
                pVol = 0
                nVol = 0
            End If

            If chkLtp.Checked = True Then
                pLtp = Val(txtLtpVal.Text)
                nLtp = Val(txtLtpVal1.Text)
            Else
                pLtp = 0
                nLtp = 0
            End If


            'For cnt = 0 To 1

            For Each drow As DataRow In currtable.Select("isCalc = true")  'Select("company='" & compname & "' ")
                If ht_TMPLTPPrice.Contains(drow("tokanno")) = False Then ht_TMPLTPPrice.Add(drow("tokanno"), drow("last"))
                If drow("CP") = "F" Then
                    'drow("last") = Val(fltpprice(CLng(drow("tokanno"))))
                    For Each grow As DataRow In gcurrtable.Select("tokanno='" & drow("tokanno") & "'")
                        grow("last") = drow("last")
                    Next
                    If pan11 = True Then
                        If date1 = CDate(drow("mdate")).Date Then
                            ' fltppr = val(fltpprice(CLng(drow("tokanno"))))
                            fltppr = txtrate.Text
                            isfut = True
                        End If
                    End If

                Else
                    If pan11 = True Then
                        If date1 = CDate(drow("fut_mdate")).Date Then
                            'fltppr = val(fltpprice(CLng(drow("ftoken"))))
                            'txtrate.Invoke(mdel, fltppr)
                            fltppr = txtrate.Text
                            'dt = date1
                            isfut = True
                        End If
                    End If

                    If CBool(drow("isliq")) = True Then
                        token = CLng(drow("tokanno"))
                        token1 = CLng(drow("token1"))
                    Else
                        token = CLng(drow("tokanno"))
                        token1 = 0
                    End If


                    mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                    mmt = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date, CDate(drow("mdate")).Date)
                    If Now.Date = CDate(drow("mdate")).Date Then
                        mt += 0.5
                    End If
                    If CDate(DateAdd(DateInterval.Day, CInt(txtnoofday.Text), Now())).Date = CDate(drow("mdate")).Date Then
                        mmt += 0.5
                    End If
                    If drow("cp") = "C" Then
                        iscall = True
                    Else
                        iscall = False
                    End If
                End If

                If fltppr <> 0 Then
                    'If cnt = 0 Then   'for +ve change
                    If sTyp = "P" Then
                        'Calc Vol mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
                        cal_projMtm_vol(fltppr + fltppr * pLtp / 100, Val(drow("strikes")), mt, mmt, iscall, isfut, drow, Val(drow("lv")) + pVol)
                    Else                     'for -ve change
                        cal_projMtm_vol(fltppr + fltppr * nLtp / 100, Val(drow("strikes")), mt, mmt, iscall, isfut, drow, Val(drow("lv")) + nVol)
                    End If
                End If

            Next
            cal_projMTM_LTP_VOL(sTyp)
            'type = "minus"
            For Each Drow As DataRow In currtable.Rows
                Drow("last") = ht_TMPLTPPrice(Drow("tokanno"))
                gcurrtable.Select("tokanno=" & Drow("tokanno") & "")(0).Item("last") = ht_TMPLTPPrice(Drow("tokanno"))
            Next
            'Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
    ''' <summary>
    ''' cal_projMtm_vol
    ''' </summary>
    ''' <param name="futval"></param>
    ''' <param name="stkprice"></param>
    ''' <param name="mT"></param>
    ''' <param name="mmT"></param>
    ''' <param name="mIsCall"></param>
    ''' <param name="mIsFut"></param>
    ''' <param name="drow"></param>
    ''' <param name="vol"></param>
    ''' <remarks>This method call to calc. Proj. MtoM Vol. LTP. % change</remarks>
    Private Sub cal_projMtm_vol(ByVal futval As Double, ByVal stkprice As Double, ByVal mT As Integer, ByVal mmT As Integer, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal drow As DataRow, ByVal vol As Double)

        Dim _mt, _mt1 As Double
        Dim ltp As Double

        If mT = 0 Then
            _mt = 0.00001
            _mt1 = 0.00001
        Else
            _mt = (mT) / 365
            '_mt1 = ((mT + 1) - CInt(txtnoofday.Text)) / 365
            _mt1 = (mmT) / 365
        End If


        If stkprice > 0 Then
            If vol <> 0 Then

                ltp = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, Val(vol / 100), _mt, mIsCall, False, 0, 0))
                drow("last") = ltp
            End If
        Else
            drow("last") = futval
            'drow("last") = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, vol, _mt, False, True, 0, 0))
        End If

    End Sub
    ''' <summary>
    ''' cal_projMTM_LTP_VOL
    ''' </summary>
    ''' <param name="type"></param>
    ''' <remarks>This method call to calculate Proj. MtoM on LTP. and Vol. change</remarks>
    Private Sub cal_projMTM_LTP_VOL(ByVal sType As String)
        If currtable.Rows.Count = 0 Then Exit Sub
        Dim prgrossmtm As Double = 0
        Dim togrossmtm As Double = 0
        Dim totgross As Double = 0
        Dim prexp As Double = 0
        Dim texp As Double = 0

        For Each drow As DataRow In currtable.Rows
            For Each drow1 As DataRow In GdtFOTrades.Select("script='" & drow("script") & "'  and entry_date=#" & fDate(Now.Date) & "#  And company='" & compname & "'")
                togrossmtm += (Val(drow1("qty")) * (Val(drow("last")) - Val(drow1("rate"))))
            Next
            For Each drow1 As DataRow In GdtEQTrades.Select("script='" & drow("script") & "' and entry_date=#" & fDate(Now.Date) & "#  And company='" & compname & "'")
                togrossmtm += (Val(drow1("qty")) * (Val(drow("last")) - Val(drow1("rate"))))
            Next
            If Val(drow("units")) <> 0 Then
                prgrossmtm += (Val(drow("units")) * (Val(drow("last")) - Val(drow("traded"))))
            Else
                prgrossmtm += (-Val(drow("traded")))
            End If
        Next
        For Each drow As DataRow In currtable.Select("units <> 0")
            If drow("cp") = "E" Then
                '  texp = texp + Val(Val(Math.Abs(drow("units"))) * Val(drow("last")) * ndbs / ndbsp)
                texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entry_date = #" & fDate(Now.Date) & "# and qty > 0").ToString) * Val(drow("last")) * ndbs / ndbsp)
                texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entry_date = #" & fDate(Now.Date) & "# and qty < 0").ToString) * Val(drow("last")) * ndbl / ndblp)
                texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entry_date <> #" & fDate(Now.Date) & "# and qty > 0").ToString) * Val(drow("last")) * dbs / dbsp)
                texp += (Val(GdtEQTrades.Compute("sum(qty)", "company='" & compname & "' and entry_date <> #" & fDate(Now.Date) & "# and qty < 0").ToString) * Val(drow("last")) * dbl / dblp)

            ElseIf drow("cp") = "F" Then
                If Val(drow("units")) > 0 Then
                    texp = texp + Val(Val(Math.Abs(drow("units"))) * Val(drow("last")) * futs / futsp)
                Else
                    texp = texp + Val(Val(Math.Abs(drow("units"))) * Val(drow("last")) * futl / futlp)
                End If
            Else
                If Val(exptable.Compute("max(spl)", "")) <> 0 Then
                    If Val(drow("units")) > 0 Then
                        texp = texp + (Val((Math.Abs(Val(drow("units"))) * (Val(drow("strikes")) + Val(drow("last")))) * sps) / spsp)
                    Else
                        texp = texp + (Val((Math.Abs(Val(drow("units"))) * (Val(drow("strikes")) + Val(drow("last")))) * spl) / splp)
                    End If
                Else
                    If Val(drow("units")) > 0 Then
                        texp = texp + (Val((Math.Abs(Val(drow("units"))) * Val(drow("last"))) * pres) / presp)
                    Else
                        texp = texp + (Val((Math.Abs(Val(drow("units"))) * Val(drow("last"))) * prel) / prelp)
                    End If
                End If
            End If
        Next
        texp = Math.Round(texp, 2)
        Dim txtprsqmtm1 As Double = prgrossmtm - togrossmtm + Val(txtprexp.Text)
        Dim txttosqmtm1 As Double = togrossmtm + Val(txttexp.Text)
        If sType = "P" Then 'Plus
            lblpVal.Text = Format(txtprsqmtm1 + txttosqmtm1 - texp, SquareMTMstr)
        ElseIf sType = "M" Then 'Minus
            lblnVal.Text = Format(txtprsqmtm1 + txttosqmtm1 - texp, SquareMTMstr)
        End If
    End Sub


    ''' <summary>
    ''' analysis_Resize
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call when resize Analysis form</remarks>
    Private Sub analysis_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Not XResolution = Screen.PrimaryScreen.Bounds.Width Then
            XResolution = Screen.PrimaryScreen.Bounds.Width
            Call setGridTrad()
        End If
    End Sub

    Private Sub txtLtpVal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLtpVal.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub

    Private Sub txtVolVal_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVolVal.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub

    Private Sub txtLtpVal1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLtpVal1.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub

    Private Sub txtVolVal1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVolVal1.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub
    ''' <summary>
    ''' txtLtpVal_KeyDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to calculate GAP opening Analysis value</remarks>
    Private Sub txtLtpVal_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtVolVal1.KeyDown, txtVolVal.KeyDown, txtLtpVal1.KeyDown, txtLtpVal.KeyDown
        If e.KeyCode = Keys.Return Or e.KeyCode = Keys.Tab Then
            If Val(txtLtpVal1.Text) < -99 Then
                MsgBox("Value can't be less than -99.")
                Exit Sub
            End If

            If chkLtp.Checked = True Or chkVol.Checked = True Then
                Call cal_projMTM_plusMinus("P")
                Call cal_projMTM_plusMinus("M")
            End If
        End If
    End Sub

    Private Sub txtLtpVal_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVolVal1.KeyPress, txtVolVal.KeyPress, txtLtpVal1.KeyPress, txtLtpVal.KeyPress
        If e.KeyChar < "0" Or e.KeyChar > "9" Then
            If e.KeyChar <> "." And Asc(e.KeyChar) <> 8 And e.KeyChar <> "-" Then
                e.KeyChar = ""
            End If
        End If
    End Sub
    Private Sub TradGridToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TradGridToolStripMenuItem.Click
        If DGTrading.Visible = True Then
            DGTrading.Visible = False
            TableLayoutPanel1.RowStyles(4).Height = 26
            TableLayoutPanel1.RowStyles(5).Height += 100
            'TradGridToolStripMenuItem.Image = Image.FromFile(Windows.Forms.Application.StartupPath() & "\icon1.png")
            TradGridToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Visible = True
            TableLayoutPanel1.RowStyles(5).Height -= 100
            TradGridToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Pan3ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Pan3ToolStripMenuItem.Click
        If TabControl1.Visible = True Then
            TabControl1.Visible = False
            TableLayoutPanel1.RowStyles(4).Height += 200
            Pan3ToolStripMenuItem.ForeColor = Color.Blue
        Else
            TabControl1.Visible = True
            TableLayoutPanel1.RowStyles(4).Height -= 200
            Pan3ToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub ButtonsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonsToolStripMenuItem.Click
        If TableLayoutPanel13.Visible = True Then
            TableLayoutPanel13.Visible = False
            TableLayoutPanel3.ColumnStyles(2).Width = 0
            ButtonsToolStripMenuItem.ForeColor = Color.Blue
        Else
            TableLayoutPanel13.Visible = True
            TableLayoutPanel3.ColumnStyles(2).Width = 35 '50
            ButtonsToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Panel1ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel1ToolStripMenuItem.Click
        If pan11 = True Then
            If panel2.Visible = True Then
                panel2.Visible = False
                TableLayoutPanel1.RowStyles(1).Height = 0
                'TableLayoutPanel1.RowStyles(5).Height += 100
                Panel1ToolStripMenuItem.ForeColor = Color.Blue
            Else
                panel2.Visible = True
                TableLayoutPanel1.RowStyles(1).Height = 26
                Panel1ToolStripMenuItem.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub Panel2ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel2ToolStripMenuItem.Click
        If pan21 = True Then
            If Panel1.Visible = True Then
                Panel1.Visible = False
                TableLayoutPanel1.RowStyles(2).Height = 0
                'TableLayoutPanel1.RowStyles(5).Height += 100
                Panel2ToolStripMenuItem.ForeColor = Color.Blue
            Else
                Panel1.Visible = True
                TableLayoutPanel1.RowStyles(2).Height = 26
                Panel2ToolStripMenuItem.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub Panel3ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel3ToolStripMenuItem.Click
        If pan31 = True Then
            If Panel3.Visible = True Then
                Panel3.Visible = False
                TableLayoutPanel1.RowStyles(3).Height = 0
                Panel3ToolStripMenuItem.ForeColor = Color.Blue
                'TableLayoutPanel1.RowStyles(5).Height += 100

            Else
                Panel3.Visible = True
                TableLayoutPanel1.RowStyles(3).Height = 26
                Panel3ToolStripMenuItem.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub DeltaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeltaToolStripMenuItem.Click
        If DGTrading.Columns("Delta").Visible = True Then
            DGTrading.Columns("Delta").Visible = False
            DeltaToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("Delta").Visible = True
            DeltaToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub DVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DVToolStripMenuItem.Click
        If DGTrading.Columns("deltaval").Visible = True Then
            DGTrading.Columns("deltaval").Visible = False
            DVToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("Deltaval").Visible = True
            DVToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub GammaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GammaToolStripMenuItem.Click
        If DGTrading.Columns("gamma").Visible = True Then
            DGTrading.Columns("gamma").Visible = False
            GammaToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("gamma").Visible = True
            GammaToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub GVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GVToolStripMenuItem.Click
        If DGTrading.Columns("gmval").Visible = True Then
            DGTrading.Columns("gmval").Visible = False
            GVToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("gmval").Visible = True
            GVToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TheToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TheToolStripMenuItem.Click
        If DGTrading.Columns("theta").Visible = True Then
            DGTrading.Columns("theta").Visible = False
            TheToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("theta").Visible = True
            TheToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TVToolStripMenuItem.Click
        If DGTrading.Columns("thetaval").Visible = True Then
            DGTrading.Columns("thetaval").Visible = False
            TVToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("thetaval").Visible = True
            TVToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub VVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VVToolStripMenuItem.Click
        If DGTrading.Columns("vgval").Visible = True Then
            DGTrading.Columns("vgval").Visible = False
            VVToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("vgval").Visible = True
            VVToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub VegaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VegaToolStripMenuItem.Click
        If DGTrading.Columns("vega").Visible = True Then
            DGTrading.Columns("vega").Visible = False
            VegaToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("vega").Visible = True
            VegaToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub
    Private Sub VolgaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VolgaToolStripMenuItem.Click
        If DGTrading.Columns("volga").Visible = True Then
            DGTrading.Columns("volga").Visible = False
            VolgaToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("volga").Visible = True
            VolgaToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub
    Private Sub VovToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VoVToolStripMenuItem.Click
        If DGTrading.Columns("volgaval").Visible = True Then
            DGTrading.Columns("volgaval").Visible = False
            VoVToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("volgaval").Visible = True
            VoVToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub
    Private Sub VannaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VannaToolStripMenuItem.Click
        If DGTrading.Columns("vanna").Visible = True Then
            DGTrading.Columns("vanna").Visible = False
            VannaToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("vanna").Visible = True
            VannaToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub
    Private Sub VaVToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VaVToolStripMenuItem.Click
        If DGTrading.Columns("vannaval").Visible = True Then
            DGTrading.Columns("vannaval").Visible = False
            VaVToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("vannaval").Visible = True
            VaVToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub
    Private Sub IlliqToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IlliqToolStripMenuItem.Click
        If DGTrading.Columns("liq").Visible = True Then
            DGTrading.Columns("liq").Visible = False
            IlliqToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("liq").Visible = True
            IlliqToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub
    Private Sub lblnVal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblnVal.TextChanged
        If Val(lblnVal.Text) > 0 Then
            lblnVal.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(lblnVal.Text) < 0 Then
            lblnVal.BackColor = Color.FromArgb(64, 0, 0)
        Else
            lblnVal.BackColor = Color.Black
        End If
    End Sub

    Private Sub lblpVal_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblpVal.TextChanged
        If Val(lblpVal.Text) > 0 Then
            lblpVal.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(lblpVal.Text) < 0 Then
            lblpVal.BackColor = Color.FromArgb(64, 0, 0)
        Else
            lblpVal.BackColor = Color.Black
        End If
    End Sub

#Region "Import from diffrent file functions"
    ''' <summary>
    ''' DTTMPManualFOImportDate 
    ''' </summary>
    ''' <remarks>This datatable marge all FO trade</remarks>
    Private DTTMPManualFOImportDate As DataTable
    ''' <summary>
    ''' DTTMPManualEQImportDate 
    ''' </summary>
    ''' <remarks>This datatable marge all Equity trade</remarks>
    Private DTTMPManualEQImportDate As DataTable
    ''' <summary>
    ''' DTTMPManualCurrencyDate
    ''' </summary>
    ''' <remarks>This datatable marge all Currency trade</remarks>
    Private DTTMPManualCurrencyDate As DataTable
    ''' <summary>
    ''' G_DTImportSetting 
    ''' </summary>
    ''' <remarks>This datatable store Import setting data</remarks>
    Private G_DTImportSetting As DataTable
    Private ImportStatus As Boolean = False

    ''' <summary>
    ''' proc_data
    ''' </summary>
    ''' <param name="istimer"></param>
    ''' <remarks>This method call to start import process from several type of import source according to setting apply</remarks>
    Private Sub proc_data(ByVal istimer As Boolean)
        Try


            Dim ObjImpData As New import_Data
            Dim ObjIO As New ImportData.ImportOperation
            'for checking if any new trades are inserted
            'check the length of the order hash table
            VarImportInserted = False
            Dim len As Integer = hashOrder.Keys.Count
            refreshstarted = False
            Me.Cursor = Cursors.WaitCursor
            'Dim chk1 As Boolean
            DTAllTableMerge.Rows.Clear()
            ''divyesh
            Dim objTradManual As New trading
            DTTMPManualFOImportDate = New DataTable
            DTTMPManualEQImportDate = New DataTable
            DTTMPManualCurrencyDate = New DataTable
            Dim StrImportFlag As String = ""
            G_DTImportSetting = objTrad.Select_Import_Setting()
            'Dim dStart As Date = Now
            If istimer = True Then
                Dim DtImportSetting As DataTable = New DataView(G_DTImportSetting, "Auto_Import=TRUE", "", DataViewRowState.CurrentRows).ToTable
                For Each dr As DataRow In DtImportSetting.Rows
                    Dim VarFileNameFormat As String
                    VarFileNameFormat = dr("FileName_Format").Split(".")(0)
                    Dim VarFilePath As String
                    Select Case dr("Text_Type").ToString
                        Case "NEAT FO Trade File"
                            VarFilePath = dr("File_Path") & "\Trade" & dr("File_Code") & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromNeaFOTEXT(True, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "NEAT CURRENCY Trade File"
                            VarFilePath = dr("File_Path") & "\Trade" & dr("File_Code") & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromNeaCurrTEXT(True, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                            '==========================================
                        Case "FIST FO Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromFistFOTEXT(True, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "FIST FO ADMIN Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromFadmFOTEXT(True, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                            '==========================================
                        Case "GETS FO Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromGetsFOTEXT(True, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "GETS CURRENCY Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromGetsCurrTEXT(True, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                        Case "GETS EQ Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromGetsEQTEXT(True, DTTMPManualEQImportDate, VarFilePath, "", ObjIO)
                            '==========================================
                        Case "ODIN FO Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromOdinFOTEXT(True, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "ODIN EQ Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromOdinEQTEXT(True, DTTMPManualEQImportDate, VarFilePath, "", ObjIO)
                        Case "ODIN CURRENCY Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromOdinCurrTEXT(True, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                            '=========================================
                        Case "NOW FO Trade File"
                            Dim VarStr1 As String = VarFileNameFormat.Substring(0, VarFileNameFormat.Length - 14)
                            Dim VarStr2 As String = VarFileNameFormat.Substring(VarFileNameFormat.Length - 14)
                            VarFilePath = dr("File_Path") & "\" & VarStr1 & Format(Today, VarStr2) & ".txt"
                            Call ObjImpData.FromNowFOTEXT(True, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "NOW EQ Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromNowEQTEXT(True, DTTMPManualEQImportDate, VarFilePath, "", ObjIO)
                        Case "NOW CURRENCY Trade File"
                            Dim VarStr1 As String = VarFileNameFormat.Substring(0, VarFileNameFormat.Length - 14)
                            Dim VarStr2 As String = VarFileNameFormat.Substring(VarFileNameFormat.Length - 14)
                            VarFilePath = dr("File_Path") & "\" & VarStr1 & Format(Today, VarStr2) & ".txt"
                            Call ObjImpData.FromNowCurrTEXT(True, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                            '=========================================
                        Case "NOTICE FO Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call ObjImpData.FromNotisFOTEXT(True, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "NOTICE CURRENCY Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call ObjImpData.FromNotisCurrTEXT(True, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                        Case "NOTICE EQ Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call ObjImpData.FromNotisEQTEXT(True, DTTMPManualEQImportDate, VarFilePath, "", ObjIO)
                            '===============================
                        Case "NSE FO Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call ObjImpData.FromNseFOTEXT(True, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "NSE EQ Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call proc_data_FromNSEEqTEXT(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                        Case "NSE CURRENCY Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call ObjImpData.FromNseCurrTEXT(True, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                            '================================
                        Case "" ' SQL Server Database Type Selected Then
                            Select Case dr("Server_Type").ToString
                                Case "GETS SQL Server Database"
                                    Call proc_data_FromGETSdb(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                                Case "ODIN SQL Server Database"
                                    Call proc_data_FromODINdb(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                                Case "NOTIS FO SQL Server Database"
                                    Call proc_data_FromNotisFOdb(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                                Case "NOTIS EQ SQL Server Database"
                                    Call proc_data_FromNotisEQdb(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                            End Select
                    End Select
                Next
            Else
                Dim DtImportSetting As DataTable = New DataView(G_DTImportSetting, "Manual_Import=TRUE", "", DataViewRowState.CurrentRows).ToTable
                For Each dr As DataRow In DtImportSetting.Rows
                    Dim VarFileNameFormat As String
                    VarFileNameFormat = dr("FileName_Format").Split(".")(0)
                    Dim VarFilePath As String
                    Select Case dr("Text_Type").ToString
                        Case "NEAT FO Trade File"
                            VarFilePath = dr("File_Path") & "\Trade" & dr("File_Code") & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromNeaFOTEXT(False, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "NEAT CURRENCY Trade File"
                            VarFilePath = dr("File_Path") & "\Trade" & dr("File_Code") & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromNeaCurrTEXT(False, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                            '--------------------------------------------
                        Case "FIST FO Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromFistFOTEXT(False, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "FIST FO ADMIN Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromFadmFOTEXT(False, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                            '============================================
                        Case "GETS FO Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromGetsFOTEXT(False, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "GETS CURRENCY Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromGetsCurrTEXT(False, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                        Case "GETS EQ Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromGetsEQTEXT(False, DTTMPManualEQImportDate, VarFilePath, "", ObjIO)
                            '==============================================
                        Case "ODIN FO Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromOdinFOTEXT(False, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "ODIN EQ Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromOdinEQTEXT(False, DTTMPManualEQImportDate, VarFilePath, "", ObjIO)
                        Case "ODIN CURRENCY Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromOdinCurrTEXT(False, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                            '==============================================
                        Case "NOW FO Trade File"
                            Dim VarStr1 As String = VarFileNameFormat.Substring(0, VarFileNameFormat.Length - 14)
                            Dim VarStr2 As String = VarFileNameFormat.Substring(VarFileNameFormat.Length - 14)
                            VarFilePath = dr("File_Path") & "\" & VarStr1 & Format(Today, VarStr2) & ".txt"
                            Call ObjImpData.FromNowFOTEXT(False, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "NOW EQ Trade File"
                            VarFilePath = dr("File_Path") & "\" & Format(Today, VarFileNameFormat) & ".txt"
                            Call ObjImpData.FromNowEQTEXT(False, DTTMPManualEQImportDate, VarFilePath, "", ObjIO)
                        Case "NOW CURRENCY Trade File"
                            Dim VarStr1 As String = VarFileNameFormat.Substring(0, VarFileNameFormat.Length - 14)
                            Dim VarStr2 As String = VarFileNameFormat.Substring(VarFileNameFormat.Length - 14)
                            VarFilePath = dr("File_Path") & "\" & VarStr1 & Format(Today, VarStr2) & ".txt"
                            Call ObjImpData.FromNowCurrTEXT(False, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                            '==============================================
                        Case "NOTICE FO Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call ObjImpData.FromNotisFOTEXT(False, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "NOTICE CURRENCY Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call ObjImpData.FromNotisCurrTEXT(False, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                        Case "NOTICE EQ Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call ObjImpData.FromNotisEQTEXT(False, DTTMPManualEQImportDate, VarFilePath, "", ObjIO)
                            '==================================
                        Case "NSE FO Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call ObjImpData.FromNseFOTEXT(False, DTTMPManualFOImportDate, VarFilePath, "", ObjIO)
                        Case "NSE EQ Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call proc_data_FromNSEEqTEXT(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, DTTMPManualCurrencyDate, objTradManual, GVar_DealerCode, VarFilePath, "")
                        Case "NSE CURRENCY Trade File"
                            VarFilePath = dr("File_Path") & "\" & VarFileNameFormat & ".txt"
                            Call ObjImpData.FromNseCurrTEXT(False, DTTMPManualCurrencyDate, VarFilePath, "", ObjIO)
                            '===================================
                        Case "" ' SQL Server Database Type Selected Then
                            Select Case dr("Server_Type").ToString
                                Case "GETS SQL Server Database"
                                    Call proc_data_FromGETSdb(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                                Case "ODIN SQL Server Database"
                                    Call proc_data_FromODINdb(False, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                                Case "NOTIS FO SQL Server Database"
                                    Call proc_data_FromNotisFOdb(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                                Case "NOTIS EQ SQL Server Database"
                                    Call proc_data_FromNotisEQdb(True, DTTMPManualFOImportDate, DTTMPManualEQImportDate, objTradManual, dr("Server_Name").ToString, dr("Database_Name").ToString, dr("User_Name").ToString, dr("Pwd").ToString, dr("Table_Name").ToString)
                            End Select
                    End Select
                Next
            End If
            ObjImpData = Nothing
            ObjIO = Nothing

            'MsgBox("Start:" & dStart & vbCrLf & "End :" & Now)

        Catch ex As Exception
            MsgBox("Error in Procedure : Proc_data [Importing Trade]" & vbCrLf & ex.Message)
            Me.Cursor = Cursors.Default
        End Try


        Try
            If DTTMPManualFOImportDate.Rows.Count = 0 And DTTMPManualEQImportDate.Rows.Count = 0 And DTTMPManualCurrencyDate.Rows.Count = 0 Then
                Me.Cursor = Cursors.Default
                Exit Sub
            Else
                Dim sort As DataGridViewColumn = DGTrading.SortedColumn
                Dim sortindex As Integer
                If Not IsNothing(sort) Then
                    sortindex = sort.Index
                Else
                    sortindex = 0
                End If
                Dim sortorder As SortOrder
                If Not sort Is Nothing Then
                    sortorder = DGTrading.SortedColumn.HeaderCell.SortGlyphDirection
                End If

                If DTTMPManualFOImportDate.Rows.Count > 0 Then
                    'REM Calc FO Expences 
                    Call GSub_CalculateExpense(DTTMPManualFOImportDate, "FO", True)
                    DTAllTableMerge.Merge(DTTMPManualFOImportDate)
                End If
                If DTTMPManualEQImportDate.Rows.Count > 0 Then
                    'REM Calc Eq Expences By Viral
                    Call GSub_CalculateExpense(DTTMPManualEQImportDate, "EQ", True)
                    DTAllTableMerge.Merge(DTTMPManualEQImportDate)
                End If
                If DTTMPManualCurrencyDate.Rows.Count > 0 Then
                    'REM Calc Currency Expences By Viral
                    Call GSub_CalculateExpense(DTTMPManualCurrencyDate, "CURR", True)
                    DTAllTableMerge.Merge(DTTMPManualCurrencyDate)
                End If

                DTAllTableMerge.AcceptChanges()
                FSTimerLogFile.WriteLine("No. of Trades :" & DTAllTableMerge.Rows.Count)

                comptable = objTrad.Comapany
                comptable_Net = objTrad.Comapany_Net
                Dim VarDTDate As Date = DTAllTableMerge.Rows(0)("entrydate")

                'Dim ArrTMPDate As New ArrayList
                'For Each Dr As DataRow In DTAllTableMerge.Rows
                '    If ArrTMPDate.Contains(Format(Dr("entrydate"), "dd-MMM-yyyy")) = False Then
                '        ArrTMPDate.Add(Format(Dr("entrydate"), "dd-MMM-yyyy"))
                '    End If
                'Next

                ''For i1 As Integer = 0 To ArrTMPDate.Count - 1
                ''    objTrad.Delete_prBal(CDate(ArrTMPDate(i1)))
                ''    Call cal_prebal(CDate(ArrTMPDate(i1)))
                ''Next

                REM PreVious balance Calculation Now Not Need Because Of Expences Calc is implimented as instructe Alpeshbhai Commented By Viral 10-Aug-2011
                '----------------------------------------------------
                'REM Delete previous balance on company name and date. 19-11-2010 by Divyesh
                'Dim ti2 As Long = System.Environment.TickCount
                'Dim companyname As DataTable
                'companyname = DTAllTableMerge.DefaultView.ToTable(True, "company")

                'Dim ArrTMPDate As New ArrayList
                'For Each Dr As DataRow In DTAllTableMerge.Rows
                '    If ArrTMPDate.Contains(Format(Dr("entrydate"), "dd-MMM-yyyy")) = False Then
                '        ArrTMPDate.Add(Format(Dr("entrydate"), "dd-MMM-yyyy"))
                '    End If
                'Next

                'For Each dr As DataRow In companyname.Rows
                '    REM Add Company row into Company Table
                '    If comptable.Select("Company='" & dr("company") & "'").Length = 0 Then
                '        comptable.Rows.Add(dr("company"))
                '    End If
                '    REM End
                '    For i1 As Integer = 0 To ArrTMPDate.Count - 1
                '        objTrad.Delete_prBal(CDate(ArrTMPDate(i1)), dr("company"))
                '    Next
                'Next

                ''STime = Date.Now
                ''Calc oF Previous Balance
                'For i1 As Integer = 0 To ArrTMPDate.Count - 1
                '    Call cal_prebal(CDate(ArrTMPDate(i1)), companyname)
                'Next
                ''ETime = Date.Now
                ''MsgBox("cal_prebal Time:" & DateDiff(DateInterval.Second, STime, ETime))

                ''prebal = objTrad.prebal
                'FSTimerLogFile.WriteLine("Previous Balance Delete and Recalulate :" & System.Environment.TickCount - ti2)
                '-----------------------------------------------------------------
                'compname = ""

                'Call fill_equity_dtable() REM maintable Fill From Database
                'If istimer = False Then
                '    If tbcomp.TabPages.Count = comptable.Rows.Count Then
                '        Me.Cursor = Cursors.Default
                '        Exit Sub
                '    End If
                'End If

                'Call GSub_Fill_GDt_AllTrades()
                ''Call fill_equity_dtable()
                'Call GSub_CalculateExpense(DTAllTableMerge)

                Dim selectab As String = compname
                Dim selectindex As Integer = ind1
                Dim i As Integer
                i = 0

                REM This Process Not Using because maintable updation process
                '' done in fill_table(table),fill_equity(temtable) and fill_Currency(table) 19-11-2010 by Divyesh
                ''Call objAna.process_data()

                Call AddRemoveTab(comptable)

                VarIsTabAddRemove = True
                If tbcomp.TabPages.ContainsKey("NIFTY") = True And compname = "" Then
                    compname = "NIFTY"
                    chknifty = True
                    tbcomp.SelectedTab = tbcomp.TabPages(compname)
                Else
                    If tbcomp.TabPages.ContainsKey(compname) = True Then
                        tbcomp.SelectedTab = tbcomp.TabPages(compname)
                    Else
                        compname = comptable.Rows(0).Item("company")
                        tbcomp.SelectedTab = tbcomp.TabPages(compname)
                    End If
                    'If (comptable.Rows.Count <> 0) Then
                    '    compname = comptable.Rows(0).Item("company")
                    '    tbcomp.SelectedTab = tbcomp.TabPages(compname)
                    'End If
                End If

                Dim VarMonthTabName As String = ""
                If TabStrategy.TabPages.Count > 0 Then
                    VarMonthTabName = TabStrategy.SelectedTab.Name
                End If
                Call Fill_StrategyTab_MonthWise(True)
                If VarMonthTabName <> "" Then
                    TabStrategy.SelectedTab = TabStrategy.TabPages(VarMonthTabName)
                End If


                Dim ti1 As Long = System.Environment.TickCount
                If tbmo Is Nothing Then
                    tbmo = ""
                End If
                If compname Is Nothing Then
                    compname = ""
                End If


                Call change_tab(compname, tbmo)


                FSTimerLogFile.WriteLine("Change Tab Process(MS) :" & System.Environment.TickCount - ti1)

                VarIsTabAddRemove = False

                'End If
                If Not sort Is Nothing Then
                    If sortorder.ToString = "Ascending" Then
                        DGTrading.Sort(DGTrading.Columns(sortindex), ComponentModel.ListSortDirection.Ascending)
                    Else
                        DGTrading.Sort(DGTrading.Columns(sortindex), ComponentModel.ListSortDirection.Descending)
                    End If
                End If
                Me.Cursor = Cursors.Default
                refreshstarted = False
            End If
        Catch ex As Exception
            MsgBox("Error in Procedure : Proc_data [After Importing Trade]" & vbCrLf & ex.Message)
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region



    ''' <summary>
    ''' Label24_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to click Margin lable to caclulate Exposure Margin</remarks>
    Private Sub Label24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label24.Click
        If FlgThr_Span = True Then
            Exit Sub
        Else
            Timer_span.Enabled = False
            Try

                'by viral 24-11-11

                'calculate exposure marging menual
                Me.Cursor = Cursors.WaitCursor
                If maintable.Rows.Count > 0 Then
                    If Directory.Exists(mSPAN_path) Then
                        'Dim thrssss As New Threading.Thread(AddressOf generate_SPAN_data)
                        'thrssss.Start()
                        Call generate_SPAN_data()
                    End If
                End If
                DouExpMrg = 0
                DouIntMrg = 0
                'txtexpmrg.Text = 0
                'txtintmrg.Text = 0
                If mTbl_SPAN_output.Rows.Count > 0 Then
                    For Each drow As DataRow In mTbl_SPAN_output.Select("ClientCode='" & compname & "'")
                        DouExpMrg = Format(DouExpMrg + Val(drow("exposure_margin").ToString), exmargstr)
                        DouIntMrg = Format(DouIntMrg + Val(drow("spanreq").ToString) - Val(drow("anov").ToString), inmargstr)
                        txttotmarg.Text = Math.Round((DouIntMrg + DouExpMrg + Val(txtEquity.Text)) / 100000, 2)

                        'txtexpmrg.Text = Format(Val(txtexpmrg.Text) + Val(drow("exposure_margin").ToString), exmargstr)
                        'txtintmrg.Text = Format(Val(txtintmrg.Text) + Val(drow("spanreq").ToString) - Val(drow("anov").ToString), inmargstr)
                        'txttotmarg.Text = Math.Round((Val(txtintmrg.Text) + Val(txtexpmrg.Text) + Val(txtEquity.Text)) / 100000, 2)

                    Next

                End If
                Me.Cursor = Cursors.Default
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
            Timer_span.Enabled = True
        End If
    End Sub
    ''' <summary>
    ''' Timer_span_Tick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to Exposure Margin timer fire to calculate export margin</remarks>
    Private Sub Timer_span_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_span.Tick
        If DGTrading.Rows.Count = 0 Then Exit Sub
        mTbl_exposure_database = objTrad.Exposure_margin
        'MsgBox("timer span" & FlgThr_Span.ToString)
        If FlgThr_Span = False Then
            Thr_SpanCalc = New Thread(AddressOf Thr_Span)
            Thr_SpanCalc.Start()
        End If

    End Sub
    ''' <summary>
    ''' BackgroundWorker1_DoWork
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call calc. exposure margin and dispaly </remarks>
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

    End Sub
    Private Sub Thr_Span()
        Try
            FlgThr_Span = True
            If maintable.Rows.Count > 0 Then
                If Directory.Exists(mSPAN_path) Then
                    generate_SPAN_data() '(maintable)
                End If
            End If
            'txtexpmrg.Text = 0
            'txtintmrg.Text = 0
            REM Because of Margin show in PopUp Window By Viral

            'txtexpmrg.Invoke(mexpMarg, 0)
            'txtintmrg.Invoke(mintMarg, 0)

            DouExpMrg = 0
            DouIntMrg = 0
            REM End

            If mTbl_SPAN_output.Rows.Count > 0 Then
                For Each drow As DataRow In mTbl_SPAN_output.Select("ClientCode='" & compname & "'")
                    REM Because of Margin show in PopUp Window By Viral
                    'txtexpmrg.Invoke(mexpMarg, CDbl(Format(drow("exposure_margin"), exmargstr)))
                    'txtintmrg.Invoke(mintMarg, CDbl(Format(Val(txtintmrg.Text) + Val(drow("spanreq").ToString) - Val(drow("anov").ToString), inmargstr)))

                    'txtexpmrg.Text = CDbl(Format(drow("exposure_margin"), exmargstr))
                    'txtintmrg.Text = CDbl(Format(Val(txtintmrg.Text) + Val(drow("spanreq").ToString) - Val(drow("anov").ToString), inmargstr))


                    DouExpMrg = CDbl(Format(drow("exposure_margin"), exmargstr))
                    DouIntMrg = CDbl(Format(DouIntMrg + Val(drow("spanreq").ToString) - Val(drow("anov").ToString), inmargstr))
                    REM End
                    'txttotmarg.Invoke(mtotMarg)
                    'txttotmarg.Text = Math.Round((Val(txtintmrg.Text) + Val(txtexpmrg.Text) + Val(txtEquity.Text)) / 100000, 2)
                Next

            End If

        Catch ex As Exception
            ' MsgBox("Span Calculation error: " & ex.ToString)
            FlgThr_Span = False
        End Try
        FlgThr_Span = False
    End Sub

    Private Sub calcMarg(ByVal ouExpMrg As Double, ByVal ouIntMrg As Double)
        txtexpmrg.Text = CDbl(Format(ouExpMrg, exmargstr))
        txtintmrg.Text = CDbl(Format(ouIntMrg, inmargstr))
        txttotmarg.Text = Math.Round((Val(txtintmrg.Text) + Val(txtexpmrg.Text) + Val(txtEquity.Text)) / 100000, 2)
        'txtexpmrg.Invoke(mexpMarg, 500)
        'txtintmrg.Invoke(mintMarg, 600)
        'txttotmarg.Invoke(mtotMarg)
    End Sub
    ''' <summary>
    ''' txteqrate_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to Update offline Equity price</remarks>
    Private Sub txteqrate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txteqrate.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If changeVal1 = True Then
                UpdateAutomatic(True) 'cal_equity_offline()
            End If
            changeVal1 = False
            SendKeys.Send("{HOME}+{END}")
        ElseIf e.KeyChar < "0" Or e.KeyChar > "9" Then
            If e.KeyChar <> "." And Asc(e.KeyChar) <> 8 Then
                e.KeyChar = ""
            End If
        Else
            changeVal1 = True
        End If
    End Sub

    Private Sub txteqrate_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txteqrate.Leave
        Call UpdateAutomatic(True) 'cal_equity_offline()
    End Sub

    Private Sub ExpenseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpenseToolStripMenuItem.Click
        If DGTrading.Columns("totExp").Visible = True Then
            DGTrading.Columns("totExp").Visible = False
            ExpenseToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("totExp").Visible = True
            ExpenseToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub GrossMTMToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrossMTMToolStripMenuItem.Click
        If DGTrading.Columns("grossmtm").Visible = True Then
            DGTrading.Columns("grossmtm").Visible = False
            GrossMTMToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("grossmtm").Visible = True
            GrossMTMToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub

    Private Sub RemarksToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemarksToolStripMenuItem.Click
        If DGTrading.Columns("remarks").Visible = True Then
            DGTrading.Columns("remarks").Visible = False
            RemarksToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("remarks").Visible = True
            RemarksToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub
    Private Sub NetMTMToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NetMTMToolStripMenuItem.Click
        If DGTrading.Columns("netmtm").Visible = True Then
            DGTrading.Columns("netmtm").Visible = False
            GrossMTMToolStripMenuItem.ForeColor = Color.Blue
        Else
            DGTrading.Columns("netmtm").Visible = True
            GrossMTMToolStripMenuItem.ForeColor = Color.Black
        End If
    End Sub



    ''' <summary>
    ''' SortingCompanyToolStripMenuItem_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>This method call to Sort all Company Tab in Alphbatical order</remarks>
    Private Sub SortingCompanyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SortingCompanyToolStripMenuItem.Click
        If (tbcomp.TabCount <> 0) Then
            If (SortingCompanyToolStripMenuItem.Checked = False) Then
                If (tbcomp.TabCount <> 1) Then
                    Dim i As Integer
                    i = 0
                    VarIsTabAddRemove = True
                    Dim t As Integer = tbcomp.TabPages.Count - 1
                    Dim tab As TabPage
                    While t >= 0
                        tab = tbcomp.TabPages(t)
                        If UCase(tab.Name.ToString()) = "NIFTY" Then
                            chknifty = False 'delete nifty
                        End If
                        tbcomp.TabPages.Remove(tab)
                        t = tbcomp.TabPages.Count - 1
                    End While
                    comptable = objTrad.Comapany
                    comptable_Net = objTrad.Comapany_Net
                    For Each drow As DataRow In comptable.Select("", "Company")
                        tbcomp.TabPages.Add(drow("company"))
                        tbcomp.TabPages.Item(i).Name = drow("company")
                        tbcomp.TabPages.Item(i).Tag = drow("company")
                        If UCase(drow("company")) = "NIFTY" Then
                            compname = "NIFTY"
                            chknifty = True
                            ind = i
                        End If
                        i = i + 1
                    Next
                    'If chknifty = False Then
                    compname = tbcomp.TabPages.Item(0).Name
                    'End If
                    VarIsTabAddRemove = False
                    Call change_tab(compname, tbmo)
                    'SortingCompanyToolStripMenuItem.Checked = True
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' searchcompany
    ''' </summary>
    ''' <param name="DtCompany"></param>
    ''' <remarks>This method call to refill Company Seach option</remarks>
    Public Sub searchcompany(Optional ByVal DtCompany As DataTable = Nothing)
        If chkanalysis = True Then
            If tbcomp.TabCount > 1 OrElse comptable.Rows.Count > 1 Then
                MDI.ToolStripcompanyCombo.Visible = True
                MDI.ToolStripMenuSearchComp.Visible = True

                If DtCompany Is Nothing Then
                    comptable = objTrad.Comapany
                    DtCompany = comptable.Copy
                    comptable_Net = objTrad.Comapany_Net
                End If
                'Dim dv As DataView = New DataView(comptable, "", "Company", DataViewRowState.CurrentRows)
                MDI.ToolStripcompanyCombo.ComboBox.DataSource = DtCompany
                MDI.ToolStripcompanyCombo.ComboBox.DisplayMember = "Company"
                MDI.ToolStripcompanyCombo.ComboBox.ValueMember = "Company"
            End If
        End If
        'If MDI.ToolStripcompanyCombo.Visible = True Then
        '    SearchCompToolStripMenuItem.Checked = True
        'Else
        '    SearchCompToolStripMenuItem.Checked = False
        'End If
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        'set the drawmode and subscribe to the DrawItem event
        'Me.TabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed
        'AddHandler Me.TabControl1.DrawItem, AddressOf Me.TabControl1_DrawItem
        'Application.EnableVisualStyles()
        'Application.VisualStyleState = VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled
        Application.VisualStyleState = VisualStyles.VisualStyleState.NonClientAreaEnabled
        Application.DoEvents()



    End Sub

    Private Sub ContextMenuStrip1_Opening(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If comptable.Rows.Count = 0 OrElse comptable.Rows.Count = 1 Then
            SortingCompanyToolStripMenuItem.Visible = False
            ' SearchCompToolStripMenuItem.Visible = False
        Else
            SortingCompanyToolStripMenuItem.Visible = True
            'SearchCompToolStripMenuItem.Visible = True
        End If
    End Sub
    Private Sub txtfut1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfut1.LostFocus
        Call UpdateAutomatic(True)
    End Sub

    Private Sub txtfut2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfut2.LostFocus
        Call UpdateAutomatic(True)
    End Sub

    Private Sub txtfut3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtfut3.LostFocus
        Call UpdateAutomatic(True)
    End Sub

    Private Sub TabStrategy_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabStrategy.SelectedIndexChanged
        'Dim earg As System.Windows.Forms.DrawItemEventArgs
        'TabStrategy_DrawItem(sender, earg)
        If VarIsTabAddRemove = True Then Exit Sub
        'Dim mStart As String
        If TabStrategy.TabPages.Count > 0 Then
            'mStart = cmdStart.Text
            Call change_tab(compname, tbmo)
            'Dim ea As System.Windows.Forms.TabControlCancelEventArgs
            TabControl1_SelectedIndexChanged(sender, e)
            'SetStartStop(mStart)
            'TabControl1_Selecting(sender, ea)
        End If
    End Sub
    Private Sub SetStartStop(ByVal mStart As String)
        REM Broadcast Do not stop while switching from one security tab to another security tab 
        If mStart = "Start" Then
            thrworking = True
        Else
            thrworking = False
        End If
        Call start_stop()
    End Sub

    '''' <summary>
    '''' ht_Ana_FToken
    '''' </summary>
    '''' <remarks>This Hashtable use to update Future Text box LTP Price</remarks>
    'Dim ht_Ana_FToken As New Hashtable
    '''' <summary>
    '''' Arr_Ana_Ftoken 
    '''' </summary>
    '''' <remarks>This Arraylist use to Future TokenNo</remarks>
    'Dim Arr_Ana_Ftoken As New ArrayList
    '''' <summary>
    '''' ht_Ana_EQToken
    '''' </summary>
    '''' <remarks>This Hashtable use to update Equity Text box LTP Price</remarks>
    'Dim ht_Ana_EQToken As New Hashtable
    ''' <summary>
    ''' ht_Ana_Position
    ''' </summary>
    ''' <remarks>This Hashtable use to store [TokenNo] as Key and Struct_Analysis_Position as Value  </remarks>
    Dim ht_Ana_Position As New Hashtable


    Private Sub Fill_Analysis_Hashtable(ByRef dtTable As DataTable, ByVal CmpName As String)
        ''Jignesh

        REM Update Analysis From Hashtable 
        'ht_Ana_FToken.Clear()
        'ht_Ana_EQToken.Clear()
        ht_Ana_Position.Clear()
        'Arr_Ana_Ftoken.Clear()
        For Each Dr As DataRow In dtTable.Select("company = '" & CmpName & "'", "fut_mdate")
            Dim Struct_Pos As New Struct_Analysis_Position
            'If Dr("CP") = "E" Then
            '    If ht_Ana_EQToken.Contains(CLng(Dr("TokanNo"))) = False Then
            '        ht_Ana_EQToken.Add(CLng(Dr("TokanNo")), 0)
            '    End If
            '    Dim DrFOContract() As DataRow = scripttable.Select("Symbol='" & Dr("Company") & "' AND option_type='XX' AND expdate1>=#" & Today & "#", "expdate1")
            '    If DrFOContract.Length > 0 Then
            '        If ht_Ana_FToken.Contains(CLng(DrFOContract(0)("Token"))) = False Then
            '            ht_Ana_FToken.Add(CLng(DrFOContract(0)("Token")), 0)
            '            Arr_Ana_Ftoken.Add(CLng(Dr("FToken")))
            '        End If
            '    End If
            'Else
            '    If Dr("script").ToString.Substring(3, 3) <> "IDX" Then
            '        If ht_Ana_EQToken.Contains(CLng(Dr("Asset_Tokan"))) = False Then
            '            ht_Ana_EQToken.Add(CLng(Dr("Asset_Tokan")), 0)
            '        End If
            '    End If
            'End If
            'If ht_Ana_FToken.Contains(CLng(Dr("FToken"))) = False And Dr("FToken") > 0 Then
            '    ht_Ana_FToken.Add(CLng(Dr("FToken")), 0)
            '    Arr_Ana_Ftoken.Add(CLng(Dr("FToken")))
            'End If

            If Dr("CP") = "E" Then
                'Struct_Pos.VarPrevLongUnits = Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty > 0").ToString)
                'Struct_Pos.VarPrevShortUnits = Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty < 0").ToString)
                'Struct_Pos.VarTodayLongUnits = Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty > 0").ToString)
                'Struct_Pos.VarTodayShortUnits = Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty < 0").ToString)
                '
                'Struct_Pos.VarPrevUnits = Struct_Pos.VarPrevLongUnits + Struct_Pos.VarPrevShortUnits 'Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate<#" & Today & "#").ToString)
                'Struct_Pos.VarTodayUnits = Struct_Pos.VarTodayLongUnits + Struct_Pos.VarTodayShortUnits 'Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate>=#" & Today & "#").ToString)


                'GdtEQTrades.Select("script='" & Dr("script") & "'")
                'Struct_Pos.VarPrevLongUnits = Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty > 0").ToString)
                'Struct_Pos.VarPrevShortUnits = Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty < 0").ToString)
                'Struct_Pos.VarTodayLongUnits = Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty > 0").ToString)
                'Struct_Pos.VarTodayShortUnits = Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty < 0").ToString)


                'Struct_Pos.VarPrevUnits = Struct_Pos.VarPrevLongUnits + Struct_Pos.VarPrevShortUnits 'Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate<#" & Today & "#").ToString)
                'Struct_Pos.VarTodayUnits = Struct_Pos.VarTodayLongUnits + Struct_Pos.VarTodayShortUnits 'Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate>=#" & Today & "#").ToString)

                Struct_Pos.VarPrevUnits = Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate<#" & fDate(Today) & "# And company='" & CmpName & "'").ToString)
                Struct_Pos.VarTodayUnits = Val(GdtEQTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate>=#" & fDate(Today) & "# And company='" & CmpName & "'").ToString)

                Struct_Pos.VarPrevValue = 0 'Val(GdtEQTrades.Compute("SUM(tot)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "#").ToString)
                Struct_Pos.VarTodayValue = Val(GdtEQTrades.Compute("SUM(tot)", "Script='" & Dr("Script") & "' AND entrydate >= #" & fDate(Today) & "# And company='" & CmpName & "'").ToString)
            Else
                If VarIsCurrency = False Then
                    'Struct_Pos.VarPrevLongUnits = Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty > 0").ToString)
                    'Struct_Pos.VarPrevShortUnits = Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty < 0").ToString)
                    'Struct_Pos.VarTodayLongUnits = Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty > 0").ToString)
                    'Struct_Pos.VarTodayShortUnits = Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty < 0").ToString)
                    '
                    'Struct_Pos.VarPrevUnits = Struct_Pos.VarPrevLongUnits + Struct_Pos.VarPrevShortUnits  'Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < # " & Today & "#").ToString)
                    'Struct_Pos.VarTodayUnits = Struct_Pos.VarTodayLongUnits + Struct_Pos.VarTodayShortUnits   'Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "#").ToString)
                    'GdtFOTrades.Select("script='" & Dr("script") & "'")
                    'Struct_Pos.VarPrevLongUnits = Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty > 0").ToString)
                    'Struct_Pos.VarPrevShortUnits = Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty < 0").ToString)
                    'Struct_Pos.VarTodayLongUnits = Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty > 0").ToString)
                    'Struct_Pos.VarTodayShortUnits = Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty < 0").ToString)

                    'Struct_Pos.VarPrevUnits = Struct_Pos.VarPrevLongUnits + Struct_Pos.VarPrevShortUnits  'Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < # " & Today & "#").ToString)
                    'Struct_Pos.VarTodayUnits = Struct_Pos.VarTodayLongUnits + Struct_Pos.VarTodayShortUnits   'Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "#").ToString)

                    Struct_Pos.VarPrevUnits = Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & fDate(Today) & "# And company='" & CmpName & "'").ToString)
                    Struct_Pos.VarTodayUnits = Val(GdtFOTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & fDate(Today) & "# And company='" & CmpName & "'").ToString)


                    Struct_Pos.VarPrevValue = 0 'Val(GdtFOTrades.Compute("SUM(tot)", "Script='" & Dr("Script") & "' AND entrydate < #" & fdate(Today) & "#").ToString)
                    Struct_Pos.VarTodayValue = Val(GdtFOTrades.Compute("SUM(tot)", "Script='" & Dr("Script") & "' AND entrydate >= #" & fDate(Today) & "# And company='" & CmpName & "'").ToString)
                Else
                    'Struct_Pos.VarPrevLongUnits = Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty > 0").ToString)
                    'Struct_Pos.VarPrevShortUnits = Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty < 0").ToString)
                    'Struct_Pos.VarTodayLongUnits = Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty > 0").ToString)
                    'Struct_Pos.VarTodayShortUnits = Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty < 0").ToString)
                    '
                    'Struct_Pos.VarPrevUnits = Struct_Pos.VarPrevLongUnits + Struct_Pos.VarPrevShortUnits 'Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "#").ToString)
                    'Struct_Pos.VarTodayUnits = Struct_Pos.VarTodayLongUnits + Struct_Pos.VarTodayShortUnits 'Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "#").ToString)
                    'GdtCurrencyTrades.Select("script='" & Dr("script") & "'")
                    'Struct_Pos.VarPrevLongUnits = Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty > 0").ToString)
                    'Struct_Pos.VarPrevShortUnits = Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "# AND Qty < 0").ToString)
                    'Struct_Pos.VarTodayLongUnits = Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty > 0").ToString)
                    'Struct_Pos.VarTodayShortUnits = Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "# AND Qty < 0").ToString)

                    'Struct_Pos.VarPrevUnits = Struct_Pos.VarPrevLongUnits + Struct_Pos.VarPrevShortUnits 'Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "#").ToString)
                    'Struct_Pos.VarTodayUnits = Struct_Pos.VarTodayLongUnits + Struct_Pos.VarTodayShortUnits 'Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & Today & "#").ToString)

                    Struct_Pos.VarPrevUnits = Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate < #" & fDate(Today) & "# And company='" & CmpName & "'").ToString)
                    Struct_Pos.VarTodayUnits = Val(GdtCurrencyTrades.Compute("SUM(Qty)", "Script='" & Dr("Script") & "' AND entrydate >= #" & fDate(Today) & "#  And company='" & CmpName & "'").ToString)

                    Struct_Pos.VarPrevValue = 0 'Val(GdtCurrencyTrades.Compute("SUM(tot)", "Script='" & Dr("Script") & "' AND entrydate < #" & Today & "#").ToString)
                    Struct_Pos.VarTodayValue = Val(GdtCurrencyTrades.Compute("SUM(tot)", "Script='" & Dr("Script") & "' AND entrydate >= #" & fDate(Today) & "#  And company='" & CmpName & "'").ToString)
                End If
            End If
            If ht_Ana_Position.Contains(CLng(Dr("TokanNo"))) = False Then
                ht_Ana_Position.Add(CLng(Dr("TokanNo")), Struct_Pos)
            End If
        Next
        REM End
    End Sub
    Private Sub txttVolgaval_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttvolgaval.TextChanged
        If Val(txttvolgaval.Text) > 0 Then
            txttvolgaval.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttvolgaval.Text) < 0 Then
            txttvolgaval.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttvolgaval.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttvannaval_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttvannaval.TextChanged
        If Val(txttvannaval.Text) > 0 Then
            txttvannaval.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttvannaval.Text) < 0 Then
            txttvannaval.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttvannaval.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttvolga1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttvolga1.TextChanged
        If Val(txttvolga1.Text) > 0 Then
            txttvolga1.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttvolga1.Text) < 0 Then
            txttvolga1.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttvolga1.BackColor = Color.Black
        End If
    End Sub
    Private Sub txttvanna1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttvanna1.TextChanged
        If Val(txttvanna1.Text) > 0 Then
            txttvanna1.BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(txttvanna1.Text) < 0 Then
            txttvanna1.BackColor = Color.FromArgb(64, 0, 0)
        Else
            txttvanna1.BackColor = Color.Black
        End If
    End Sub

    '''''' <summary>
    '''''' Txt_TextChanged
    '''''' </summary>
    '''''' <param name="sender"></param>
    '''''' <param name="e"></param>
    '''''' <remarks>This method call to Set BackColor </remarks>
    ' ''Private Sub Txt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt.TextChanged
    ' ''    If Val(sender.Text) > 0 Then
    ' ''        sender.BackColor = Color.FromArgb(0, 64, 0)
    ' ''    ElseIf Val(txttvanna1.Text) < 0 Then
    ' ''        sender.BackColor = Color.FromArgb(64, 0, 0)
    ' ''    Else
    ' ''        sender.BackColor = Color.Black
    ' ''    End If
    ' ''End Sub

    Private Sub cmdsum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdsum.Click

        If AppLicMode = "SERLIC" Then
            If bool_IsServerConnected = False Then Exit Sub
        End If


        flgSummary = True
        alertmsg = False
        '  Timer_Calculation_Tick(sender, e)
        Call summary()
        If tbcomp.TabPages.Count = 0 Then Exit Sub
        If tbcomp.SelectedTab.Name <> compname Then
            tbcomp.SelectTab(compname)
        End If
    End Sub
    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        REM For individual Monthtab  Monthwise Calcultion tab not Showing
        Try
            If TabStrategy.SelectedTab.Name <> "TP_All" Then
                Select Case TabControl1.SelectedIndex
                    Case 1
                        Panel6.Visible = True
                        TableLayoutPanel6.Visible = False
                End Select
            Else
                Panel6.Visible = False
                TableLayoutPanel6.Visible = True
            End If
            'Dim TmpTb As TabPage
            'TmpTb = TabControl1.TabPages(1)
            'If TabStrategy.SelectedTab.Name <> "TP_All" Then
            '    TabControl1.TabPages.Remove(TmpTb)
            'Else
            '    TabControl1.TabPages.Insert(1, TmpTb)
            'End If

            'If TabStrategy.SelectedTab.Name <> "TP_All" Then
            '    TabControl1.TabPages(1).Visible = False
            '    TabControl1.TabPages(1).Enabled = False
            '    'TabControl1.TabPages(1).Hide()
            '    'If TabControl1.TabPages.Count >= 3 Then
            '    'TabControl1.TabPages.Remove(TabControl1.TabPages(1))
            '    'End If
            'Else
            '    Select Case TabControl1.SelectedIndex
            '        Case 1
            '            TabControl1.TabPages(1).Visible = True
            '            TabControl1.TabPages(1).Enabled = True
            '    End Select
            '    'If TabControl1.TabPages.Count < 3 Then
            '    'TabControl1.TabPages.Add(TabControl1.TabPages(1))
            '    'End If
            'End If
        Catch ex As Exception

        End Try

    End Sub

    ''' <summary>
    ''' Gsub_GroupGridColProfileSave
    ''' </summary>
    ''' <remarks>This method call to Save column profile for Grouping Grid into Database according to passing tablename.</remarks>
    Private Sub Gsub_GridColProfileSave()
        Dim Dt As New DataTable
        'Dim I As Integer
        'Dim Str As String
        Dt.Columns.Add("FormName")
        Dt.Columns.Add("ColumnName")
        Dt.Columns.Add("DisplayIndex")
        Dt.Columns.Add("DisplayText")
        Dt.Columns.Add("Width")
        Dt.Columns.Add("IsVisible")
        For cnt As Integer = 0 To DGTrading.ColumnCount - 1
            'If DGTrading.Columns(cnt).Visible = True Then
            'Str = "Insert Into DataGrid_Column_Setting Values('Analysis','" & DGTrading.Columns(cnt).DataPropertyName & " '," & cnt & ", '" & DGTrading.Columns(cnt).HeaderText & " ', " & DGTrading.Columns(cnt).Width & ",1)"
            Dt.Rows.Add("Analysis", DGTrading.Columns(cnt).Name, DGTrading.Columns(cnt).DisplayIndex, DGTrading.Columns(cnt).HeaderText, DGTrading.Columns(cnt).Width, DGTrading.Columns(cnt).Visible)
            'End If
        Next
        Dt.AcceptChanges()
        objTrad.Update_DataGrid_Column_Setting_OnWidthIndex(Dt)
        Dt.Dispose()
    End Sub

    ''Private Sub TabControl1_Selecting(ByVal sender As Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles TabControl1.Selecting
    ''    Try
    ''        If TabStrategy.SelectedTab.Name <> "TP_All" Then
    ''            Select Case TabControl1.SelectedIndex
    ''                Case 1
    ''                    Panel6.Visible = True
    ''                    'TabControl1.TabPages(1).ForeColor = Color.Gray
    ''                    'e.Cancel = True
    ''            End Select
    ''        Else
    ''            Panel6.Visible = False
    ''        End If
    ''    Catch ex As Exception

    ''    End Try
    ''End Sub

    ' '' ''Private Sub TabControl1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
    ' '' ''    Dim f As Font
    ' '' ''    Dim backBrush As Brush
    ' '' ''    Dim foreBrush As Brush

    ' '' ''    If e.Index = Me.TabControl1.SelectedIndex Then
    ' '' ''        f = New Font(e.Font, FontStyle.Italic Or FontStyle.Bold)
    ' '' ''        backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Blue, Color.Black, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal)
    ' '' ''        'foreBrush = Brushes.PowderBlue
    ' '' ''        foreBrush = Brushes.White
    ' '' ''    Else
    ' '' ''        'f = New Font(e.Font, FontStyle.Italic Or FontStyle.Strikeout)
    ' '' ''        'backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Gray, Color.Gray, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal)
    ' '' ''        'foreBrush = New SolidBrush(Color.Black)
    ' '' ''        f = e.Font
    ' '' ''        backBrush = New SolidBrush(e.BackColor)
    ' '' ''        foreBrush = New SolidBrush(e.ForeColor)
    ' '' ''    End If


    ' '' ''    Dim tabName As String = Me.TabControl1.TabPages(e.Index).Text
    ' '' ''    Dim tName As String = Me.TabControl1.TabPages(e.Index).Name
    ' '' ''    If TabStrategy.SelectedTab.Name <> "TP_All" Then
    ' '' ''        If tName = "MonthWise" Then
    ' '' ''            f = New Font(e.Font, FontStyle.Italic Or FontStyle.Regular)
    ' '' ''            backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Gray, Color.Gray, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal)
    ' '' ''            foreBrush = New SolidBrush(Color.Black)
    ' '' ''        End If
    ' '' ''    Else
    ' '' ''        'f = New Font(e.Font, FontStyle.Italic Or FontStyle.Regular)
    ' '' ''        'backBrush = New System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, Color.Aqua, Color.Aqua, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal)
    ' '' ''        'foreBrush = New SolidBrush(Color.Brown)
    ' '' ''    End If
    ' '' ''    Dim sf As New StringFormat()
    ' '' ''    sf.Alignment = StringAlignment.Center
    ' '' ''    e.Graphics.FillRectangle(backBrush, e.Bounds)
    ' '' ''    Dim r As RectangleF = New RectangleF(e.Bounds.X, e.Bounds.Y + 4, e.Bounds.Width, e.Bounds.Height - 4)
    ' '' ''    e.Graphics.DrawString(tabName, f, foreBrush, r, sf)

    ' '' ''    sf.Dispose()
    ' '' ''    If e.Index = Me.TabControl1.SelectedIndex Then
    ' '' ''        f.Dispose()
    ' '' ''        backBrush.Dispose()
    ' '' ''    Else
    ' '' ''        backBrush.Dispose()
    ' '' ''        foreBrush.Dispose()
    ' '' ''    End If
    ' '' ''End Sub 'tabControl1_DrawItem


    Private Sub txttotmarg_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txttotmarg.MouseEnter
        'If MDI.ObjMargin Is Nothing Then
        '    MDI.ObjMargin = New FrmMargin
        '    Dim MLeft As Long = 10 'txttotmarg.Left 
        '    Dim MTop As Long = My.Computer.Screen.Bounds.Height - 140 '630 'txttotmarg.Top 
        '    MDI.ObjMargin.ShowForm(DouIntMrg, DouExpMrg, DouEquity, txttotmarg.Text, MLeft, MTop, Me)
        'End If
    End Sub

    Private Sub CmdScenario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdScenario.Click
        If AppLicMode = "SERLIC" Then
            If bool_IsServerConnected = False Then Exit Sub
        End If
        Call scenario()
    End Sub

    Private Sub CmdAtmCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call get_AtmCalc()
    End Sub
    ''' <summary>
    ''' get_AtmClear
    ''' To Clear ATM Panel
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub get_AtmClear()
        txtDeITM.Text = Format(0, Deltastr_Val)
        txtGmITM.Text = Format(0, Gammastr_Val)
        txtVeITM.Text = Format(0, Vegastr_Val)
        txtThITM.Text = Format(0, Thetastr_Val)
        'txtVoITM.Text = Format(0, Volgastr_Val)
        'txtVaITM.Text = Format(0, Vannastr_Val)

        txtDeATM.Text = Format(0, Deltastr_Val)
        txtGmATM.Text = Format(0, Gammastr_Val)
        txtVeATM.Text = Format(0, Vegastr_Val)
        txtThATM.Text = Format(0, Thetastr_Val)
        'txtVoATM.Text = Format(0, Volgastr_Val)
        'txtVaATM.Text = Format(0, Vannastr_Val)

        txtDeOTM.Text = Format(0, Deltastr_Val)
        txtGmOTM.Text = Format(0, Gammastr_Val)
        txtVeOTM.Text = Format(0, Vegastr_Val)
        txtThOTM.Text = Format(0, Thetastr_Val)
        'txtVoOTM.Text = Format(0, Volgastr_Val)
        'txtVaOTM.Text = Format(0, Vannastr_Val)


        txtDeTot.Text = Format(0, Deltastr_Val)
        txtGmTot.Text = Format(0, Gammastr_Val)
        txtVeTot.Text = Format(0, Vegastr_Val)
        txtThTot.Text = Format(0, Thetastr_Val)
        'txtVoTot.Text = Format(0, Volgastr_Val)
        'txtVaTot.Text = Format(0, Vannastr_Val)
    End Sub
    ''' <summary>
    ''' get_AtmCalc
    ''' </summary>
    ''' <remarks>This method call to Calculate Atm Panel By Viral 27-06-11</remarks>
    Private Sub get_AtmCalc()
        If currtable.Rows.Count <= 0 Then Exit Sub
        get_AtmClear()
        Dim OptTyp As String = ""
        Dim StrFilter As String = ""
        Dim StrITMFilter As String = ""
        Dim StrATMFilter As String = ""
        Dim StrOTMFilter As String = ""
        Dim dtCurTlb As DataTable
        Dim Dv As DataView
        Try
            If OptAll.Checked = True Then
                OptTyp = ""
            ElseIf OptCall.Checked = True Then
                OptTyp = "C"
            ElseIf OptPut.Checked = True Then
                OptTyp = "P"
            End If

            'StrITMFilter = "Case When CP='P' then (1-deltavval) Else deltavval End < " & ATMDELTA_MIN & ""
            StrITMFilter = "deltavval > " & ATMDELTA_MAX & ""
            StrATMFilter = "deltavval >= " & ATMDELTA_MIN & " And deltavval <= " & ATMDELTA_MAX & ""
            StrOTMFilter = "deltavval < " & ATMDELTA_MIN & ""


            StrFilter = "Iscalc = True"
            If OptTyp.Length > 0 Then
                StrFilter = StrFilter + " And CP ='" & OptTyp & "'"
            End If
            StrFilter = StrFilter + " And CP <>'F'"


            'Rem Put Delta = 1- Delta

            Dv = New DataView(currtable, StrFilter, "company", DataViewRowState.CurrentRows)
            dtCurTlb = Dv.ToTable()
            REM End
            'calculate greek values
            If dtCurTlb.Rows.Count > 0 Then
                txtDeITM.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", StrITMFilter).ToString), Deltastr_Val)
                txtGmITM.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", StrITMFilter).ToString), Gammastr_Val)
                txtVeITM.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", StrITMFilter).ToString), Vegastr_Val)
                txtThITM.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", StrITMFilter).ToString), Thetastr_Val)
                'txtVoITM.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", StrITMFilter).ToString), Volgastr_Val)
                'txtVaITM.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", StrITMFilter).ToString), Vannastr_Val)

                txtDeATM.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", StrATMFilter).ToString), Deltastr_Val)
                txtGmATM.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", StrATMFilter).ToString), Gammastr_Val)
                txtVeATM.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", StrATMFilter).ToString), Vegastr_Val)
                txtThATM.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", StrATMFilter).ToString), Thetastr_Val)
                'txtVoATM.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", StrATMFilter).ToString), Volgastr_Val)
                'txtVaATM.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", StrATMFilter).ToString), Vannastr_Val)

                txtDeOTM.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", StrOTMFilter).ToString), Deltastr_Val)
                txtGmOTM.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", StrOTMFilter).ToString), Gammastr_Val)
                txtVeOTM.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", StrOTMFilter).ToString), Vegastr_Val)
                txtThOTM.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", StrOTMFilter).ToString), Thetastr_Val)
                'txtVoOTM.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", StrOTMFilter).ToString), Volgastr_Val)
                'txtVaOTM.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", StrOTMFilter).ToString), Vannastr_Val)

                txtDeTot.Text = Format(Val(txtDeITM.Text) + Val(txtDeATM.Text) + Val(txtDeOTM.Text), Deltastr_Val)
                txtGmTot.Text = Format(Val(txtGmITM.Text) + Val(txtGmATM.Text) + Val(txtGmOTM.Text), Gammastr_Val)
                txtVeTot.Text = Format(Val(txtVeITM.Text) + Val(txtVeATM.Text) + Val(txtVeOTM.Text), Vegastr_Val)
                txtThTot.Text = Format(Val(txtThITM.Text) + Val(txtThATM.Text) + Val(txtThOTM.Text), Thetastr_Val)

                'txtDeTot.Text = Format(Val(dtCurTlb.Compute("sum(deltaval)", "").ToString), Deltastr_Val)
                'txtGmTot.Text = Format(Val(dtCurTlb.Compute("sum(gmval)", "").ToString), Gammastr_Val)
                'txtVeTot.Text = Format(Val(dtCurTlb.Compute("sum(vgval)", "").ToString), Vegastr_Val)
                'txtThTot.Text = Format(Val(dtCurTlb.Compute("sum(thetaval)", "").ToString), Thetastr_Val)
                ''txtVoTot.Text = Format(Val(dtCurTlb.Compute("sum(volgaval)", "").ToString), Volgastr_Val)
                ''txtVaTot.Text = Format(Val(dtCurTlb.Compute("sum(vannaval)", "").ToString), Vannastr_Val)
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub BtnCalc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCalc.Click
        If chkLtp.Checked = True Or chkVol.Checked = True Then
            Call cal_projMTM_plusMinus("P")
            Call cal_projMTM_plusMinus("M")
        End If
    End Sub

    Private Sub OptPut_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptPut.CheckedChanged, OptCall.CheckedChanged, OptAll.CheckedChanged
        Call get_AtmCalc()
    End Sub

    Private Sub txtdelval_1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdelval_1.TextChanged, txttdelval_1.TextChanged, txtdelval_2.TextChanged, txttdelval_2.TextChanged, txtdelval_3.TextChanged, txttdelval_3.TextChanged, txtdelval_4.TextChanged, txttdelval_4.TextChanged, lblCallDelta.TextChanged, lblPutDelta.TextChanged, lblFutDelta.TextChanged, lblEqDelta.TextChanged, lblTotDelta.TextChanged, txtDeITM.TextChanged, txtDeATM.TextChanged, txtDeOTM.TextChanged, txtDeTot.TextChanged
        If Val(CType(sender, Label).Text) > 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(CType(sender, Label).Text) < 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(64, 0, 0)
        Else
            CType(sender, Label).BackColor = Color.Black
        End If
    End Sub

    Private Sub txtgmval_1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtgmval_1.TextChanged, txttgmval_1.TextChanged, txtgmval_2.TextChanged, txttgmval_2.TextChanged, txtgmval_3.TextChanged, txttgmval_3.TextChanged, txtgmval_4.TextChanged, txttgmval_4.TextChanged, lblCallGamma.TextChanged, lblPutGamma.TextChanged, lblTotGamma.TextChanged, txtGmITM.TextChanged, txtGmATM.TextChanged, txtGmOTM.TextChanged, txtGmTot.TextChanged
        If Val(CType(sender, Label).Text) > 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(CType(sender, Label).Text) < 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(64, 0, 0)
        Else
            CType(sender, Label).BackColor = Color.Black
        End If
    End Sub

    Private Sub txtwegaval_1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtwegaval_1.TextChanged, txttwegaval_1.TextChanged, txtwegaval_2.TextChanged, txttwegaval_2.TextChanged, txtwegaval_3.TextChanged, txttwegaval_3.TextChanged, txtwegaval_4.TextChanged, txttwegaval_4.TextChanged, lblCallVega.TextChanged, lblPutvega.TextChanged, lblTotVega.TextChanged, txtVeITM.TextChanged, txtVeATM.TextChanged, txtVeOTM.TextChanged, txtVeTot.TextChanged
        If Val(CType(sender, Label).Text) > 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(CType(sender, Label).Text) < 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(64, 0, 0)
        Else
            CType(sender, Label).BackColor = Color.Black
        End If
    End Sub

    Private Sub txtthetaval_1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtthetaval_1.TextChanged, txttthetaval_1.TextChanged, txtthetaval_2.TextChanged, txttthetaval_2.TextChanged, txtthetaval_3.TextChanged, txttthetaval_3.TextChanged, txtthetaval_4.TextChanged, txttthetaval_4.TextChanged, lblCallTheta.TextChanged, lblPutTheta.TextChanged, lblTotTheta.TextChanged, txtThITM.TextChanged, txtThATM.TextChanged, txtThOTM.TextChanged, txtThTot.TextChanged
        If Val(CType(sender, Label).Text) > 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(CType(sender, Label).Text) < 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(64, 0, 0)
        Else
            CType(sender, Label).BackColor = Color.Black
        End If
    End Sub

    Private Sub txtvolgaval_1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvolgaval_1.TextChanged, txttvolgaval_1.TextChanged, txtvolgaval_2.TextChanged, txttvolgaval_2.TextChanged, txtvolgaval_3.TextChanged, txttvolgaval_3.TextChanged, txtvolgaval_4.TextChanged, txttvolgaval_4.TextChanged, lblCallVolga.TextChanged, lblPutVolga.TextChanged, lblTotVolga.TextChanged
        If Val(CType(sender, Label).Text) > 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(CType(sender, Label).Text) < 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(64, 0, 0)
        Else
            CType(sender, Label).BackColor = Color.Black
        End If
    End Sub

    Private Sub txtvannaval_1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvannaval_1.TextChanged, txttvannaval_1.TextChanged, txtvannaval_2.TextChanged, txttvannaval_2.TextChanged, txtvannaval_3.TextChanged, txttvannaval_3.TextChanged, txtvannaval_4.TextChanged, txttvannaval_4.TextChanged, lblCallVanna.TextChanged, lblPutVanna.TextChanged, lblTotVanna.TextChanged
        If Val(CType(sender, Label).Text) > 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(CType(sender, Label).Text) < 0 Then
            CType(sender, Label).BackColor = Color.FromArgb(64, 0, 0)
        Else
            CType(sender, Label).BackColor = Color.Black
        End If
    End Sub

    Private Sub txtshare1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtshare1.TextChanged, txtshare2.TextChanged, txtshare3.TextChanged, txtshare4.TextChanged
        If Val(CType(sender, TextBox).Text) > 0 Then
            CType(sender, TextBox).BackColor = Color.FromArgb(0, 64, 0)
        ElseIf Val(CType(sender, TextBox).Text) < 0 Then
            CType(sender, TextBox).BackColor = Color.FromArgb(64, 0, 0)
        Else
            CType(sender, TextBox).BackColor = Color.Black
        End If
    End Sub

    Private Sub HideGreeksToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideGreeksToolStripMenuItem.Click
        If HideGreeksToolStripMenuItem.Text = "Hide Greeks" Then
            DGTrading.Columns("Delta").Visible = False
            DGTrading.Columns("Gamma").Visible = False
            DGTrading.Columns("Vega").Visible = False
            DGTrading.Columns("Theta").Visible = False
            DGTrading.Columns("Volga").Visible = False
            DGTrading.Columns("Vanna").Visible = False


            HideGreeksToolStripMenuItem.Text = "Show Greeks"
        ElseIf HideGreeksToolStripMenuItem.Text = "Show Greeks" Then
            DGTrading.Columns("Delta").Visible = True
            DGTrading.Columns("Gamma").Visible = True
            DGTrading.Columns("Vega").Visible = True
            DGTrading.Columns("Theta").Visible = True
            DGTrading.Columns("Volga").Visible = True
            DGTrading.Columns("Vanna").Visible = True

            HideGreeksToolStripMenuItem.Text = "Hide Greeks"
        End If
    End Sub

    Private Sub HideGreeksValToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideGreeksValToolStripMenuItem.Click
        If HideGreeksValToolStripMenuItem.Text = "Hide Greeks Val" Then
            DGTrading.Columns("Deltaval").Visible = False
            DGTrading.Columns("gmval").Visible = False
            DGTrading.Columns("vgval").Visible = False
            DGTrading.Columns("Thetaval").Visible = False
            DGTrading.Columns("Volgaval").Visible = False
            DGTrading.Columns("Vannaval").Visible = False


            HideGreeksValToolStripMenuItem.Text = "Show Greeks Val"
        ElseIf HideGreeksValToolStripMenuItem.Text = "Show Greeks Val" Then
            DGTrading.Columns("Deltaval").Visible = True
            DGTrading.Columns("gmval").Visible = True
            DGTrading.Columns("vgval").Visible = True
            DGTrading.Columns("Thetaval").Visible = True
            DGTrading.Columns("Volgaval").Visible = True
            DGTrading.Columns("Vannaval").Visible = True

            HideGreeksValToolStripMenuItem.Text = "Hide Greeks Val"
        End If
    End Sub

    Private Sub HideGreeksNutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideGreeksNutToolStripMenuItem.Click
        If HideGreeksNutToolStripMenuItem.Text = "Hide Greeks Neut." Then
            DGTrading.Columns("DeltaN").Visible = False
            DGTrading.Columns("GammaN").Visible = False
            DGTrading.Columns("VegaN").Visible = False
            DGTrading.Columns("ThetaN").Visible = False
            DGTrading.Columns("VolgaN").Visible = False
            DGTrading.Columns("VannaN").Visible = False


            HideGreeksNutToolStripMenuItem.Text = "Show Greeks Neut."
        ElseIf HideGreeksNutToolStripMenuItem.Text = "Show Greeks Neut." Then
            DGTrading.Columns("DeltaN").Visible = True
            DGTrading.Columns("GammaN").Visible = True
            DGTrading.Columns("VegaN").Visible = True
            DGTrading.Columns("ThetaN").Visible = True
            DGTrading.Columns("VolgaN").Visible = True
            DGTrading.Columns("VannaN").Visible = True

            HideGreeksNutToolStripMenuItem.Text = "Hide Greeks Neut."
        End If
    End Sub
    Public Sub UpdateDT_Symbol(ByRef DT As DataTable, ByVal oldCompany As String, ByVal newCompany As String)
        For Each dr As DataRow In DT.Select("company='" & oldCompany & "'")
            dr("company") = newCompany
        Next
    End Sub

    Public Sub AddUserDefCompany(ByVal sName As String)
        Dim tab As New TabPage
        tab.Text = sName
        tab.Name = sName
        tab.Tag = GetSymbol(sName)
        tab.ToolTipText = GetSymbol(sName)
        tab.ImageKey = "Icon1.ico"

        Dim ObjUserdefTab As New UserDefTag
        ObjUserdefTab = FrmUserDefTag.ShowForm(sName)
        If ObjUserdefTab.bIsValid = True Then
            'If tbcomp.TabPages.Contains(tab) Then
            If tbcomp.TabPages.Count <= 0 Then GoTo Add
            If tbcomp.SelectedTab.Text = tab.Text Then
                If tbcomp.SelectedTab.Text <> ObjUserdefTab.sTagName Then
                    'Update in 
                    'analysis()
                    'Trading/Currency_Trading/EqTrading
                    'Expense_Data()
                    ObjUserdefTab.Update_Symbol(tbcomp.SelectedTab.Text, ObjUserdefTab.sTagName)
                    UpdateDT_Symbol(maintable, tbcomp.SelectedTab.Text, ObjUserdefTab.sTagName)
                    UpdateDT_Symbol(currtable, tbcomp.SelectedTab.Text, ObjUserdefTab.sTagName)
                    UpdateDT_Symbol(gcurrtable, tbcomp.SelectedTab.Text, ObjUserdefTab.sTagName)

                    UpdateDT_Symbol(GdtFOTrades, tbcomp.SelectedTab.Text, ObjUserdefTab.sTagName)
                    UpdateDT_Symbol(GdtEQTrades, tbcomp.SelectedTab.Text, ObjUserdefTab.sTagName)
                    UpdateDT_Symbol(GdtCurrencyTrades, tbcomp.SelectedTab.Text, ObjUserdefTab.sTagName)

                    UpdateDT_Symbol(G_DTExpenseData, tbcomp.SelectedTab.Text, ObjUserdefTab.sTagName)
                    UpdateDT_Symbol(Gtbl_Summary_Analysis, tbcomp.SelectedTab.Text, ObjUserdefTab.sTagName)
                    UpdateDT_Symbol(GdtCompanyAnalysis, tbcomp.SelectedTab.Text, ObjUserdefTab.sTagName)

                End If
                tbcomp.SelectedTab.Text = ObjUserdefTab.sTagName
                tbcomp.SelectedTab.Name = ObjUserdefTab.sTagName
                tbcomp.SelectedTab.Tag = GetSymbol(ObjUserdefTab.sTagName)
                tbcomp.SelectedTab.ToolTipText = GetSymbol(ObjUserdefTab.sTagName)
                tbcomp.SelectedTab.ImageKey = "Icon1.ico"

            Else
Add:


                tab = New TabPage
                tab.Text = ObjUserdefTab.sTagName
                tab.Name = ObjUserdefTab.sTagName
                tab.Tag = GetSymbol(ObjUserdefTab.sTagName)
                tab.ToolTipText = GetSymbol(ObjUserdefTab.sTagName)
                tab.ImageKey = "Icon1.ico"

                If tbcomp.TabPages.Count > 0 Then
                    If tbcomp.TabPages.Contains(tab) Then
                        MsgBox("Tag Allready Exist.", MsgBoxStyle.Information)
                        Exit Sub
                    End If
                End If

                tbcomp.TabPages.Add(tab)
                compname = tab.Name
                tbcomp.SelectedTab = tbcomp.TabPages(compname)
            End If
        End If
    End Sub




    Private Sub RenameTagToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameTagToolStripMenuItem.Click
        AddUserDefCompany(tbcomp.SelectedTab.Name)
    End Sub
    Private Sub BackgroundWorker3_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker3.DoWork
        Try
            Objbhavcopy.ProcessBCastBCopy(dtBCopy)
            '''''
            BackgroundWorker3.Dispose()
        Catch ex As Exception
            ' MsgBox("Span Calculation error: " & ex.ToString)
            BackgroundWorker3.Dispose()
        End Try
    End Sub

    Private Sub BtnSaveBhavCopy_Click()
        Try
            dtBCopy.Rows.Clear()
            dtBCopy.AcceptChanges()
            dtBCopy.Merge(dtfoBCopy.Copy)
            dtBCopy.AcceptChanges()
            dtBCopy.Merge(dtcmBCopy.Copy)
            dtBCopy.AcceptChanges()
            dtBCopy.Merge(dtcurBCopy.Copy)
            dtBCopy.AcceptChanges()
            'dgBcopy.DataSource = dtBCopy.Copy
        Catch ex As Exception

        End Try

        'If bIsfoBcopyComplete = True And bIscmBcopyComplete = True And bIscurBcopyComplete = True Then
        bIsfoBcopyComplete = False
        bIscmBcopyComplete = False
        bIscurBcopyComplete = False
        Try
            'If DGTrading.Rows.Count = 0 Then Exit Sub
            If BackgroundWorker3.IsBusy = False Then
                BackgroundWorker3.RunWorkerAsync()
            End If
        Catch ex As Exception
            'MsgBox("Error")
        End Try
        'End If

    End Sub

    Public Sub ThrInterNetData()
        Try

            bool_Thr_GetInterNetDat = True
            Label57.BackColor = Color.Red
            G_BCastNetFoIsOn = True

            For Each dt As DataTable In comptable_Net.Tables
                resetEvents = New ManualResetEvent(dt.Rows.Count - 1) {}
                Dim i As Integer = 0
                For Each drow As DataRow In dt.Select("", "company")
                    'Call GetInternetData(drow("company").ToString)
                    resetEvents(i) = New ManualResetEvent(False)
                    ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf GetInternetData), DirectCast(i & "," & drow("company").ToString, Object))
                    i = i + 1
                    If i = 64 Then Exit For
                Next
                WaitHandle.WaitAll(resetEvents)
            Next

            G_BCastNetFoIsOn = False
            Label57.BackColor = Color.Black
            bool_Thr_GetInterNetDat = False

        Catch ex As Threading.ThreadAbortException
            Threading.Thread.ResetAbort()
            'Label57.BackColor = Color.Blue
        Catch ex As Exception
            'MsgBox("ThrInterNetData" & vbCrLf & ex.ToString)
            'Threading.Thread.ResetAbort()
            Label57.BackColor = Color.BlueViolet
            'bool_Thr_GetInterNetDat = False
            'MDI.Thr_GetInterNetDat = New Thread(AddressOf ThrInterNetData)
            'MDI.Thr_GetInterNetDat.Start()

        End Try







        '        Dim obj_AsyncResult(3) As IAsyncResult
        '        obj_AsyncResult(0) = obj_Del_Ref_Pos_Dealer.BeginInvoke(obj_Enum_calc_Type, dtTemp_DealerScript, dtTemp_DealerCompany, dtTemp_Dealerwise, Nothing, Nothing)
        '        obj_AsyncResult(1) = obj_Del_Ref_Pos_Company.BeginInvoke(obj_Enum_calc_Type, dtTemp_DealerScript, dtTemp_DealerCompany, dtTemp_Companywise, Nothing, Nothing)
        '        obj_AsyncResult(2) = obj_Del_Ref_Pos_Group_Dealer.BeginInvoke(obj_Enum_calc_Type, dtTemp_DealerScript, dtTemp_DealerCompany, dtTemp_Dealerwise, dtTemp_Groupwise, Nothing, Nothing)
        '        obj_AsyncResult(3) = obj_Del_Ref_Pos_Group_Script.BeginInvoke(obj_Enum_calc_Type, dtTemp_GroupScript, dtTemp_GroupCompany, dtTemp_Groupwise, Nothing, Nothing)

        '        REM Fill Chart Display Datatable to Run Thread
        '        If GVarIsFillChartData = True And GVarIsFillChartData_Working = False Then
        '            DelChartDtFill.BeginInvoke(dtTemp_DealerScript, Nothing, Nothing)
        '        End If
        '        REM End

        '        REM Alert calculation start
        '        If GVarIsAlertCalcStart = True Then
        '            Call DelCheckAlertCalc.BeginInvoke(Gtbl_script.Copy, dtTemp_DealerCompany, dtTemp_Companywise, dtTemp_Dealerwise, dtTemp_Groupwise, dtTemp_GroupCompany, Nothing, Nothing)
        '        End If
        '        REM End

        '        REM Wait for Grid Updation option not complated
        '        obj_AsyncResult(0).AsyncWaitHandle.WaitOne(20000, True)
        '        obj_AsyncResult(1).AsyncWaitHandle.WaitOne(20000, True)
        '        obj_AsyncResult(2).AsyncWaitHandle.WaitOne(20000, True)
        '        obj_AsyncResult(3).AsyncWaitHandle.WaitOne(20000, True)
        '        REM End
        'WaitHandle.WaitAll(



    End Sub


    Private Sub DGTrading_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGTrading.CellContentClick

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click


        If bool_Thr_GetInterNetDat = False Then
            MDI.Timer_Net.Stop()
            'MDI.Timer_Net_Tick(Timer_Net, New EventArgs)


            'Call HtmlPars.GetFoInterNetData(tbcomp.SelectedTab.Name)

            Try
                If bool_IsTelNet = False Then
                    Dim Thr_Telnet As New System.Threading.Thread(AddressOf CheckTelNet_Connection)
                    Thr_Telnet.Start()
                    Exit Sub
                End If

                If AppLicMode = "NETLIC" Then
                    If ObjLoginData.GetUserStatus = "out" Then
                        End
                    End If
                End If

                Try
                    bool_Thr_GetInterNetDat = True
                    Label57.BackColor = Color.Red
                    G_BCastNetFoIsOn = True

                    resetEvents = New ManualResetEvent(0) {}
                    Dim i As Integer = 0
                    'For Each drow As DataRow In dt.Select("", "company")
                    'Call GetInternetData(drow("company").ToString)
                    resetEvents(i) = New ManualResetEvent(False)
                    ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf GetInternetData), DirectCast(i & "," & tbcomp.SelectedTab.Name.ToString, Object))
                    i = i + 1
                    'If i = 64 Then Exit For
                    'Next
                    WaitHandle.WaitAll(resetEvents)


                    G_BCastNetFoIsOn = False
                    Label57.BackColor = Color.Black
                    bool_Thr_GetInterNetDat = False

                Catch ex As Threading.ThreadAbortException
                    Threading.Thread.ResetAbort()
                    'Label57.BackColor = Color.Blue
                Catch ex As Exception
                    MsgBox("ThrInterNetData" & vbCrLf & ex.ToString)
                    Label57.BackColor = Color.BlueViolet
                End Try


            Catch ex As Threading.ThreadAbortException
                Threading.Thread.ResetAbort()
            Catch ex As Exception
                MsgBox("Refresh" & vbCrLf & ex.ToString)
            End Try


            MDI.Timer_Net.Start()

        End If



    End Sub

    Private Sub Label19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label19.Click

    End Sub

    Private Sub CopyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click

        Dim DtCur As New DataTable
        DtCur = currtable.Copy

        Dim fotrd As New DataTable
        Dim cmtrd As New DataTable
        Dim curtrd As New DataTable
        Dim dv As DataView
        dv = New DataView(DtCur, " CP<>'E' And IsCurrency = False and units <> 0", "", DataViewRowState.CurrentRows)
        fotrd = dv.ToTable().Copy

        dv = New DataView(DtCur, " CP='E' And IsCurrency = False  and units <> 0", "", DataViewRowState.CurrentRows)
        cmtrd = dv.ToTable().Copy

        dv = New DataView(DtCur, " CP<>'E' And IsCurrency = True  and units <> 0", "", DataViewRowState.CurrentRows)
        curtrd = dv.ToTable().Copy

        Dim strTagName As String = InputBox("Insert tag name.", "Volhedge", "tag" & tbcomp.TabCount + 1)

        Dim objScript As script = New script

        Timer_Calculation.Stop()


        ''fo
        ''==================
        For Each drow As DataRow In fotrd.Rows
            'Dim tkk As Long = CLng(Val(objTrad.Trading.Compute("max(token)", "script='" & drow("script") & "'").ToString))
            'If tkk > 0 Then
            '    MsgBox(txtscript.Text & " script already exist in Traded")
            '    Exit Sub
            'End If
            Dim a, a1, script1 As String
            Dim tk As Long
            GVarMAXFOTradingOrderNo = GVarMAXFOTradingOrderNo + 1
            'insert trade to Trading table
            objScript.InstrumentName = drow("script").ToString.Substring(0, 6)

            objScript.Company = drow("company") & "/" & strTagName
            objScript.Mdate = CDate(drow("mdate"))
            objScript.StrikeRate = Val(drow("Strikes") & "")
            objScript.CP = UCase(Mid(drow("CP"), 1, 1))
            objScript.Script = drow("script")
            objScript.Units = Val(drow("units") & "")
            objScript.Rate = Val(drow("traded") & "")
            objScript.EntryDate = Date.Now

            If UCase(Mid(drow("CP"), 1, 1)) = "C" Or UCase(Mid(drow("CP"), 1, 1)) = "P" Then
                a = Mid(drow("script"), Len(drow("script")) - 1, 1)
                a1 = Mid(drow("script"), Len(drow("script")), 1)
                If a = "C" Then
                    script1 = Mid(drow("script"), 1, Len(drow("script")) - 2) & "P" & a1
                Else
                    script1 = Mid(drow("script"), 1, Len(drow("script")) - 2) & "C" & a1
                End If
                script1 = script1.ToUpper
                tk = CLng(cpfmaster.Compute("max(token)", "script='" & script1 & "'").ToString)
                objScript.Token = tk
                For Each row As DataRow In GdtFOTrades.Select("script='" & drow("script") & "' And company='" & objScript.Company & "'")
                    If row("isliq") = True Then
                        objScript.Isliq = True ' "Yes"
                    Else
                        objScript.Isliq = False '"No"
                    End If
                    Exit For
                Next
            Else
                objScript.Token = 0
                objScript.asset_tokan = 0
                objScript.Isliq = False ' "No"
            End If
            objScript.orderno = GVarMAXFOTradingOrderNo
            Dim tk1 As Long = CLng(Val(cpfmaster.Compute("max(token)", "script='" & drow("script") & "'").ToString))
            If tk1 = 0 Then
                MsgBox(drow("script") & " does not exist in contract")
            End If
            'save trade to database trade table
            Dim DTDuplicate As DataTable
            DTDuplicate = New DataTable
            DTDuplicate = objScript.Insert()
            If DTDuplicate.Rows.Count - 1 > 0 Then
                MsgBox("Contract Exist.")
                Exit Sub
            End If
            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            'get uid from trading table
            Dim dtUid As DataTable = objScript.select_trading_uid
            'Add datarow to analysis's dtFOTrading table
            Dim DtTempFO_trad As New DataTable
            DtTempFO_trad = GdtFOTrades.Clone

            Dim tprow As DataRow
            tprow = DtTempFO_trad.NewRow
            tprow("uid") = dtUid.Rows(0)("uid")
            tprow("token") = 0
            tprow("mo") = Format(drow("mdate"), "MM/yyyy")
            tprow("instrumentname") = drow("script").ToString.Substring(0, 6)

            tprow("company") = drow("company") & "/" & strTagName

            tprow("mdate") = drow("mdate")
            tprow("Strikerate") = drow("Strikes")
            tprow("CP") = drow("CP")
            tprow("script") = drow("script")
            tprow("qty") = drow("units")
            tprow("Rate") = drow("traded")
            tprow("EntryDate") = drow("EntryDate")
            tprow("entry_date") = drow("entrydate")
            tprow("isliq") = objScript.Isliq
            tprow("token1") = tk1
            tprow("orderno") = GVarMAXFOTradingOrderNo
            tprow("entryno") = 0
            tprow("tot") = Val(drow("units")) * Val(drow("traded"))
            tprow("issummary") = True
            tprow("isdisplay") = True
            DtTempFO_trad.Rows.Add(tprow)

            Call insert_FOTradeToGlobalTable(DtTempFO_trad)
            GdtFOTrades.Rows(GdtFOTrades.Rows.Count - 1).Item("Uid") = tprow("uid")
            Call GSub_CalculateExpense(DtTempFO_trad, "FO", True)

            'calculate expense of inserted position
            Dim prExp, toExp As Double
            'cal_prebal(dtent.Value.Date, CmbComp.Text.Trim, UCase(Mid(cmbcp.SelectedValue, 1, 1)), CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp)
            'insert position to database's analysis table also
            Dim dtAna As New DataTable
            'dtAna = objAna.fill_table_process(txtscript.Text.Trim, CInt(txtunit.Text), Val(txtrate.Text), prExp, toExp, dtent.Value.Date)
            'insert FO trade to analysis table


            dtAna = objAna.fill_table_process(drow("script"), CInt(drow("units")), Val(drow("traded")), prExp, toExp, Date.Today.Date, drow("company") & "/" & strTagName)
            'insert FO trade to analysis table
            objScript.insert_FOTrade_in_maintable(drow("script"), dtAna, prExp, toExp, Date.Today.Date, drow("company") & "/" & strTagName)

            'MsgBox("Script saved Successfully.", MsgBoxStyle.Information)

            LastOpenPosition = drow("company") & "/" & strTagName

        Next


        ''==================

        ''eq
        '=======================
        For Each drow As DataRow In cmtrd.Rows
            GVarMAXEQTradingOrderNo = GVarMAXEQTradingOrderNo + 1
            objScript.Company = drow("company") & "/" & strTagName


            objScript.Script = drow("script")
            objScript.CP = "EQ"
            objScript.Units = Val(drow("units") & "")
            objScript.Rate = Val(drow("traded") & "")
            objScript.EntryDate = Date.Now
            objScript.orderno = GVarMAXEQTradingOrderNo
            objScript.insert_equity()

            'get uid from equity_trading
            Dim dtUid As DataTable = objScript.select_equity_uid

            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            'Save to analysis's tptable (equity trading) table
            Dim DtTempEQ_trad As New DataTable
            DtTempEQ_trad = GdtEQTrades.Clone

            Dim tprow As DataRow
            tprow = DtTempEQ_trad.NewRow
            tprow("uid") = dtUid.Rows(0)("uid")
            tprow("company") = drow("company") & "/" & strTagName
            tprow("eq") = "EQ"
            tprow("script") = drow("script")
            tprow("qty") = drow("units")
            tprow("Rate") = drow("traded")

            tprow("EntryDate") = Format(drow("EntryDate"), "MMM/dd/yyyy")
            tprow("entry_date") = Format(drow("entrydate"), "MMM/dd/yyyy")
            tprow("orderno") = GVarMAXEQTradingOrderNo
            tprow("entryno") = 0
            tprow("issummary") = True
            tprow("isdisplay") = True
            tprow("tot") = Val(drow("units")) * Val(drow("traded"))
            DtTempEQ_trad.Rows.Add(tprow)

            Call insert_EQTradeToGlobalTable(DtTempEQ_trad)
            GdtEQTrades.Rows(GdtEQTrades.Rows.Count - 1).Item("Uid") = tprow("uid")
            Call GSub_CalculateExpense(DtTempEQ_trad, "EQ", True)

            ' If dteqent.Value.Date < Now.Date Then
            Dim prExp, toExp As Double
            'caluclate expense of inserted position
            'cal_prebal(dteqent.Value.Date, cmbeqcomp.Text.Trim, "E", CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp)

            'insert position to database's analysis table also
            Dim dtAna As New DataTable
            'dtAna = objAna.fill_equity_process(UCase(txteqscript.Text.Trim), CInt(txtequnit.Text), Val(txteqrate.Text), prExp, toExp, dteqent.Value.Date)

            '***********************************************************************************
            'insert FO trade to analysis table
            Dim dtEntdate As Date = Date.Today.Date

            dtAna = objAna.fill_equity_process(drow("script"), CInt(drow("units")), Val(drow("traded")), prExp, toExp, dtEntdate, drow("company") & "/" & strTagName)
            'insert FO trade to analysis table													   
            objScript.insert_EQTrade_in_maintable(drow("script"), dtAna, prExp, toExp, dtEntdate, drow("company") & "/" & strTagName)
            'MsgBox("Script saved Successfully.", MsgBoxStyle.Information)
            LastOpenPosition = drow("company") & "/" & strTagName
        Next
        '====================---

        ''cur
        '===================
        For Each drow As DataRow In curtrd.Rows
            Dim Varmultiplier As Double = Currencymaster.Compute("MAX(multiplier)", "Script='" & drow("script") & "'")
            Dim tkk As Long = CLng(Val(objTrad.select_Currency_Trading.Compute("MAX(token)", "script='" & drow("script") & "'").ToString))
            If tkk > 0 Then
                MsgBox(drow("script") & " script already exist in Traded")
                Exit Sub
            End If
            Dim a, a1, script1 As String
            Dim tk As Long
            GVarMAXCURRTradingOrderNo = GVarMAXCURRTradingOrderNo + 1
            'insert trade to Trading table
            objScript.InstrumentName = drow("script").ToString.Substring(0, 6)
            objScript.Company = drow("company") & "/" & strTagName

            objScript.Mdate = CDate(drow("mdate"))
            objScript.StrikeRate = Val(drow("Strikes") & "")
            'objScript.CP = UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1))
            REM Change By Alpesh For Set CP='F' In Currency Trade Table 
            objScript.CP = UCase(Mid(drow("CP"), 1, 1))
            objScript.Script = drow("script")
            objScript.Units = Val(drow("units") & "") 'Val(txtCurrencyunit.Text) * Varmultiplier
            objScript.Rate = Val(drow("traded") & "")
            objScript.EntryDate = Date.Now
            If UCase(Mid(drow("CP"), 1, 1)) = "C" Or UCase(Mid(drow("CP"), 1, 1)) = "P" Then
                a = Mid(drow("script"), Len(drow("script")) - 1, 1)
                a1 = Mid(drow("script"), Len(drow("script")), 1)
                If a = "C" Then
                    script1 = Mid(drow("script"), 1, Len(drow("script")) - 2) & "P" & a1
                Else
                    script1 = Mid(drow("script"), 1, Len(drow("script")) - 2) & "C" & a1
                End If

                script1 = script1.ToUpper
                tk = CLng(Currencymaster.Compute("max(token)", "script='" & script1 & "'").ToString)
                objScript.Token = tk

                For Each row As DataRow In GdtCurrencyTrades.Select("script='" & drow("script") & "' And company='" & objScript.Company & "'")
                    If row("isliq") = True Then
                        objScript.Isliq = True ' "Yes"
                    Else
                        objScript.Isliq = False '"No"
                    End If
                    Exit For
                Next
            Else
                objScript.Token = 0
                objScript.asset_tokan = 0
                objScript.Isliq = False ' "No"
            End If
            objScript.orderno = GVarMAXCURRTradingOrderNo
            Dim tk1 As Long = CLng(Val(Currencymaster.Compute("max(token)", "script='" & drow("script") & "'").ToString))
            If tk1 = 0 Then
                MsgBox(drow("script") & " does not exist in contract")
            End If
            objScript.Units = Val(drow("units") & "") 'Val(txtCurrencyunit.Text) * Currencymaster.Compute("max(multiplier)", "script='" & txtCurrencyscript.Text & "'")

            'save trade to database trade table
            objScript.Insert_Currency_Trading()

            '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            'get uid from trading table
            Dim dtUid As DataTable = objScript.select_Currency_trading_uid

            'Save to analysis's dtFOTrading table

            Dim DtTempCurr_trad As New DataTable
            DtTempCurr_trad = GdtCurrencyTrades.Clone

            Dim tprow As DataRow
            tprow = DtTempCurr_trad.NewRow
            tprow("uid") = dtUid.Rows(0)("uid")
            tprow("token") = 0
            tprow("mo") = Format(drow("mdate"), "MM/yyyy")
            tprow("instrumentname") = drow("script").ToString.Substring(0, 6)
            tprow("company") = drow("company") & "/" & strTagName

            tprow("mdate") = drow("mdate")
            tprow("Strikerate") = Val(drow("Strikes") & "")
            tprow("CP") = drow("CP")
            tprow("script") = drow("script")
            tprow("qty") = drow("units") 'Val(txtCurrencyunit.Text) * Varmultiplier
            tprow("Rate") = Val(drow("traded") & "")
            tprow("EntryDate") = Format(drow("EntryDate"), "MMM/dd/yyyy")
            tprow("entry_date") = Format(drow("entrydate"), "MMM/dd/yyyy")
            tprow("isliq") = objScript.Isliq
            tprow("token1") = tk1
            tprow("orderno") = GVarMAXCURRTradingOrderNo
            tprow("entryno") = 0
            tprow("tot") = Val(drow("units")) * Val(drow("traded")) 'Val(txtCurrencyunit.Text) * Val(txtCurrencyrate.Text) * Varmultiplier
            tprow("issummary") = True
            tprow("isdisplay") = True
            DtTempCurr_trad.Rows.Add(tprow)

            Call insert_CurrencyTradeToGlobalTable(DtTempCurr_trad)
            GdtCurrencyTrades.Rows(GdtCurrencyTrades.Rows.Count - 1).Item("Uid") = tprow("uid")
            Call GSub_CalculateExpense(DtTempCurr_trad, "CURR", True)


            'calculate expense of inserted position
            Dim prExp, toExp As Double
            'Dim optype As String = "C" & UCase(Mid(cmbCurrencyCP.SelectedValue, 1, 1))
            'Call cal_prebal(DTPCurrencyEntryDate.Value.Date, cmbCurrencyComp.Text.Trim, optype, CInt(txtCurrencyunit.Text) * Varmultiplier, Val(txtCurrencyrate.Text), prExp, toExp)
            'insert position to database's analysis table also
            Dim dtAna As New DataTable
            'dtAna = objAna.fill_table_process(txtCurrencyscript.Text.Trim, CInt(txtCurrencyunit.Text) * Varmultiplier, Val(txtCurrencyrate.Text), prExp, toExp, DTPCurrencyEntryDate.Value.Date)

            'insert Currency trade to analysis table
            Dim dtEntdate As Date = Date.Today.Date

            dtAna = objAna.fill_table_process(drow("script"), CInt(drow("units")), Val(drow("traded")), prExp, toExp, dtEntdate, drow("company") & "/" & strTagName)
            'insert Currency trade to analysis table
            objScript.insert_CurrencyTrade_in_maintable(drow("script"), dtAna, prExp, toExp, dtEntdate, drow("company") & "/" & strTagName)

            'MsgBox("Script saved Successfully.", MsgBoxStyle.Information)

            LastOpenPosition = drow("company") & "/" & strTagName

        Next
        '===================


        System.Threading.Thread.Sleep(500)


        comptable = objTrad.Comapany
        comptable_Net = objTrad.Comapany_Net

        Call AddRemoveTab(comptable)
        compname = LastOpenPosition
        Call Fill_StrategyTab_MonthWise(True)
        change_tab(compname, tbmo)
        tbcomp.SelectedTab = tbcomp.TabPages(compname)
        LastOpenPosition = ""
        Timer_Calculation.Start()

        'Call searchcompany()
        'MDI.ToolStripcompanyCombo.Text = compname
    End Sub

    Private Sub analysis_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    End Sub
End Class

Structure Struct_Analysis_Position
    Dim VarPrevLongUnits As Integer
    Dim VarPrevShortUnits As Integer
    Dim VarTodayLongUnits As Integer
    Dim VarTodayShortUnits As Integer

    Dim VarPrevUnits As Integer
    Dim VarTodayUnits As Integer

    Dim VarPrevValue As Double
    Dim VarTodayValue As Double
End Structure

