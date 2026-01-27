'Imports System.Windows.Forms.DataVisualization.Charting
Imports MSChart20Lib
Imports System.io
Imports System.Data
Imports System.Data.OleDb
Imports OptionG
Public Class scenario1

#Region "Variable"
    Dim tleft As Integer
    Dim tTop As Integer
    Dim FrmMove As Boolean
    Dim DragX As Integer
    Dim DragY As Integer

    Public Shared chkscenario As Boolean
    Public VarIsCurrency As Boolean = False

    Public time1, time2 As Date
    Public mvalue As Double
    Public scname As String
    Public trscen As Boolean = False
    Public isVolCal As Boolean = False

    Public isStartEndDateChange As Boolean = True

    Public Shared chartName As String
    Public Shared ValLBLprofit As String
    Public Shared ValLBLgamma As String
    Public Shared ValLBLtheta As String
    Public Shared ValLBLvega As String
    Public Shared ValLBLdelta As String
    Public Shared ValLBLvolga As String
    Public Shared ValLBLVanna As String

    Public Shared cellno As Integer

    Public objProfitLossChart As New frmprofitLossChart
    Public Shared DecimalSetting As New FrmScenarioSetting

    REM local
    Dim XResolution As Integer
    Dim ShowPanel1 As Boolean = True 'Panel Default True
    Dim ShowPanel2 As Boolean
    Dim KeyF4Togal As Boolean = False
    Dim KeyF3Togal As Boolean = True 'Panel Default True
    Dim bCheckAll As Boolean = False
    Dim VarIsFirstLoad As Boolean = False
    Dim VarIsFrmLoad As Boolean = False
    Dim FirstDgt As Integer

    Dim contMenu() As ContextMenuStrip
    Dim contMenuExpiry() As ContextMenuStrip

    Dim objScenarioDetail As New scenarioDetail
    Dim objExp As New expenses
    Dim objTrad As trading = New trading


    Dim grossmtm As Double

    Dim interval As Double
    Dim txtmid As Double
    Dim mAllCV As String = ""
    Dim Mrateofinterast As Double = 0
    Dim TmPnl1 As Boolean = True
    Dim TmPnl2 As Boolean = True
    Dim frmFlg As Boolean = False
    Dim flgUnderline As Boolean = False
    Dim checkall As Boolean = True
    Dim gcheck As Boolean = False

    Dim involflag As Boolean = False
    Dim decvolflag As Boolean = False

    Dim ColorPremium As Color = Color.FromArgb(209, 255, 209)
    Dim ColorDelta As Color = Color.FromArgb(255, 204, 153)
    Dim ColorGamma As Color = Color.FromArgb(51, 204, 204)
    Dim ColorVega As Color = Color.FromArgb(255, 153, 204)
    Dim ColorTheta As Color = Color.FromArgb(204, 153, 255)
    Dim ColorVolga As Color = Color.FromArgb(0, 255, 0)
    Dim ColorVanna As Color = Color.FromArgb(51, 255, 255)

    Dim ColorSpot As Color = Color.FromArgb(161, 75, 10)
    Dim ColorMidSpot As Color = Color.FromArgb(254, 127, 0)


    Dim profitarr As New ArrayList
    Dim thetaarr As New ArrayList
    Dim deltaarr As New ArrayList
    Dim gammaarr As New ArrayList
    Dim vegaarr As New ArrayList
    Dim volgaarr As New ArrayList
    Dim vannaarr As New ArrayList

    Public Shared profit As New DataTable
    Public Shared deltatable As New DataTable
    Public Shared gammatable As New DataTable
    Public Shared vegatable As New DataTable
    Public Shared thetatable As New DataTable
    Public Shared volgatable As New DataTable
    Public Shared vannatable As New DataTable

    Dim static_Grid As New DataTable
    
    Public Shared rtable As DataTable


#End Region

#Region "Functions"
    Private Function cal_profitLoss() As Double
        Dim profit As Double = 0
        For Each grow As DataGridViewRow In grdtrad.Rows
            If grow.Cells("Active").Value = False Then Continue For
            If grow.Cells("Active").Value = True And Val(grow.Cells("units").Value) <> 0 Then profit += (Val(grow.Cells("units").Value)) * (Val(grow.Cells("last").Value) - Val(grow.Cells("ltp").Value))
        Next
        profit += grossmtm
        Return profit
    End Function
    Private Function cal_FutprofitLoss() As Double
        Dim profit As Double = 0
        For Each grow As DataGridViewRow In grdtrad.Rows
            If grow.Cells("Active").Value = False Then Continue For
            If grow.Cells("Active").Value = True And Val(grow.Cells("units").Value) <> 0 Then profit += (Val(grow.Cells("units").Value)) * (Val(grow.Cells("last").Value) - Val(grow.Cells("ltp1").Value))
        Next
        profit += grossmtm
        Return profit
    End Function

    Private Sub cal_summary()
        txtdelval.Text = 0
        txtthval.Text = 0
        txtvgval.Text = 0
        txtgmval.Text = 0
        txtVolgaVal.Text = 0
        TxtVannaVal.Text = 0

        txtdelval1.Text = 0
        txtthval1.Text = 0
        txtvgval1.Text = 0
        txtgmval1.Text = 0
        TxtVolgaVal1.Text = 0
        TxtVannaVal1.Text = 0

        'SetZeroVal()

        txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
        txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)

        For Each grow As DataGridViewRow In grdtrad.Rows
            If grow.Cells("Active").Value = True And Val(grow.Cells("units").Value) <> 0 Then
                txtdelval.Text = Format(Val(txtdelval.Text) + Val(grow.Cells("deltaval").Value), DecimalSetting.sDeltaval)
                txtthval.Text = Format(Val(txtthval.Text) + Val(grow.Cells("thetaval").Value), DecimalSetting.sThetaval)
                txtvgval.Text = Format(Val(txtvgval.Text) + Val(grow.Cells("vgval").Value), DecimalSetting.sVegaval)
                txtgmval.Text = Format(Val(txtgmval.Text) + Val(grow.Cells("gmval").Value), DecimalSetting.sGammaval)
                txtVolgaVal.Text = Format(Val(txtVolgaVal.Text) + Val(grow.Cells("volgaval").Value), DecimalSetting.sVolgaval)
                TxtVannaVal.Text = Format(Val(TxtVannaVal.Text) + Val(grow.Cells("vannaval").Value), DecimalSetting.sVannaval)

                'txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(grow.Cells("deltaval1").Value), DecimalSetting.sDeltaval)
                'txtthval1.Text = Format(Val(txtthval1.Text) + Val(grow.Cells("thetaval1").Value), DecimalSetting.sThetaval)
                'txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(grow.Cells("vgval1").Value), DecimalSetting.sVegaval)
                'txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(grow.Cells("gmval1").Value), DecimalSetting.sGammaval)
                'TxtVolgaVal1.Text = Format(Val(TxtVolgaVal1.Text) + Val(grow.Cells("volgaval1").Value), DecimalSetting.sVolgaval)
                'TxtVannaVal1.Text = Format(Val(TxtVannaVal1.Text) + Val(grow.Cells("vannaval1").Value), DecimalSetting.sVannaval)
            End If
        Next
        'result(False)
    End Sub

    Private Sub export()
        Dim savedi As New SaveFileDialog
        savedi.Filter = "Files(*.xls)|*.xls"
        If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim grd(7) As DataGridView
            grd(0) = grdtrad
            grd(1) = grdprofit
            grd(2) = grddelta
            grd(3) = grdtheta
            grd(4) = grdvega
            grd(5) = grdgamma
            grd(6) = grdvolga
            grd(7) = grdvanna
            Dim sname(7) As String
            sname(0) = "Scenario"
            sname(1) = "Profit & Loss"
            sname(2) = "Delta"
            sname(3) = "Theta"
            sname(4) = "Vega"
            sname(5) = "Gamma"
            sname(6) = "Volga"
            sname(7) = "Vanna"
            exporttoexcel(grd, savedi.FileName, sname, "scenario")
            'exporttoexcel(grdprofit)
            'Dim str As String
            'str = Mid(savedi.FileName, 1, Len(savedi.FileName) - 4)
            'exportExcel(grdtrad, str & "_scenario.csv")
            'exportExcel(grdprofit, str & "_ProfitLoss.csv")
            'exportExcel(grddelta, str & "_Delta.csv")
            'exportExcel(grdtheta, str & "_Theta.csv")
            'exportExcel(grdvega, str & "_Vega.csv")
            'exportExcel(grdgamma, str & "_Gamma.csv")
            'MsgBox("Export Complete.", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub import()
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog
        opfile.Filter = "Files(*.xls)|*.xls"
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            read_file(opfile.FileName)
        End If
    End Sub
    Private Sub read_file(ByVal fpath As String)
        Try
            If fpath <> "" Then
                Dim fi As New FileInfo(fpath)
                'Dim dv As DataView
                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
                'Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                Dim drow As DataRow
                Dim objConn As New OleDbConnection(sConnectionString)

                objConn.Open()

                'Dim objCmdSelect As New OleDbCommand("SELECT Staus,'Time I','Time II',C/P/F,Spot,Strike,Qty,Rate,'Vol(%)',Delta,'Del. Val',Theta,Th.Val,Vega,'Vg. Val',Gamma,'Gam. Val',uid FROM " & fi.Name, objConn)
                'Dim objCmdSelect As New OleDbCommand("SELECT * FROM " & fi.Name, objConn)
                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [Scenario$]", objConn)

                Dim objAdapter1 As New OleDbDataAdapter

                objAdapter1.SelectCommand = objCmdSelect
                Dim tempdata As New DataTable
                tempdata = New DataTable
                objAdapter1.Fill(tempdata)
                objConn.Close()
                grdtrad.Rows.Clear()
                txtunits.Text = 0
                txtdelval.Text = 0
                txtthval.Text = 0
                txtvgval.Text = 0
                txtgmval.Text = 0
                txtVolgaVal.Text = 0
                TxtVannaVal.Text = 0
                Dim grow As New DataGridViewRow
                For Each drow In tempdata.Rows
                    grow = New DataGridViewRow
                    grdtrad.Rows.Add(grow)
                Next
                Dim i As Integer = 0
                For Each drow In tempdata.Rows

                    grdtrad.Rows(i).Cells("Active").Value = True
                    grdtrad.Rows(i).Cells("TimeI").Value = CDate(drow("Date"))
                    grdtrad.Rows(i).Cells("TimeII").Value = CDate(drow("Expiry"))
                    grdtrad.Rows(i).Cells("CPF").Value = CStr(drow("C/P/F"))
                    grdtrad.Rows(i).Cells("spval").Value = CDbl(drow("Underlying"))
                    grdtrad.Rows(i).Cells("Strike").Value = CDbl(drow("Strike"))
                    grdtrad.Rows(i).Cells("units").Value = Val(drow("Qty"))
                    grdtrad.Rows(i).Cells("ltp").Value = Val(drow("ltp"))
                    grdtrad.Rows(i).Cells("last").Value = CDbl(drow("Rate"))
                    grdtrad.Rows(i).Cells("lv").Value = Val(drow("Vol(%)"))
                    grdtrad.Rows(i).Cells("delta").Value = Val(drow("Delta"))
                    grdtrad.Rows(i).Cells("deltaval").Value = Val(drow("Del Val"))
                    grdtrad.Rows(i).Cells("theta").Value = Val(drow("Theta"))
                    grdtrad.Rows(i).Cells("thetaval").Value = Val(drow("Th val"))
                    grdtrad.Rows(i).Cells("vega").Value = Val(drow("Vega"))
                    grdtrad.Rows(i).Cells("vgval").Value = Val(drow("Vg val"))
                    grdtrad.Rows(i).Cells("gamma").Value = Val(drow("Gamma"))
                    grdtrad.Rows(i).Cells("gmval").Value = Val(drow("Gam val"))

                    grdtrad.Rows(i).Cells("volga").Value = Val(drow("Volga"))
                    grdtrad.Rows(i).Cells("volgaval").Value = Val(drow("Vo val"))
                    grdtrad.Rows(i).Cells("vanna").Value = Val(drow("Vanna"))
                    grdtrad.Rows(i).Cells("vannaval").Value = Val(drow("Va val"))


                    grdtrad.Rows(i).Cells("uid").Value = Val(drow("uid"))



                    '  txtunits.Text = Val(txtunits.Text) + Val(grdtrad.Rows(i).Cells(6).Value)
                    txtdelval.Text = Val(txtdelval.Text) + Val(grdtrad.Rows(i).Cells("delta").Value)
                    txtthval.Text = Val(txtthval.Text) + Val(grdtrad.Rows(i).Cells("thetaval").Value)
                    txtvgval.Text = Val(txtvgval.Text) + Val(grdtrad.Rows(i).Cells("vega").Value)
                    txtgmval.Text = Val(txtgmval.Text) + Val(grdtrad.Rows(i).Cells("gamma").Value)
                    txtVolgaVal.Text = Val(txtVolgaVal.Text) + Val(grdtrad.Rows(i).Cells("volga").Value)
                    TxtVannaVal.Text = Val(TxtVannaVal.Text) + Val(grdtrad.Rows(i).Cells("vanna").Value)


                    i += 1
                Next


            End If
        Catch ex As Exception
            MsgBox("File Not Processed.")
            ' MsgBox(ex.ToString)
        End Try




    End Sub
    Private Sub ChkVol()
        Dim isVol As Boolean = True
        For Each grow As DataGridViewRow In grdtrad.Rows
            If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 And grow.Cells("CPF").Value <> "F" Then
                If CDbl(grow.Cells("lv").Value) = 0 Then
                    isVol = False
                    Exit For
                End If
            End If
        Next
        If isVol = False Then
            create_active(True)
        End If
    End Sub

    Private Sub result(ByVal ChangeFlg As Boolean)
        'If val(txtllimit.Text) <= 0 Then
        '    MsgBox("Enter Upper Limit")
        '    txtllimit.Focus()
        '    Exit Sub
        'End If

        If Val(txtllimit.Text) <= 0 Then
            MsgBox("Enter No of Strike +/-  ")
            txtllimit.Focus()
            Exit Sub
        End If
        If Val(interval) <= 0 Then
            MsgBox("Enter Interval.")
            txtinterval.Focus()
            Exit Sub
        End If

        'If val(txtmid) <= 0 Then
        '    MsgBox("Enter Mid Value")
        '    txtmid.Focus()
        '    Exit Sub
        'End If

        If (txtmid - (Val(interval * (Val(txtllimit.Text) - 0)))) <= 0 Then Exit Sub

        REM For First Record Show Charts
        If Val(txtmid) <= 0 Then
            CalInterval()
        End If

        If Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then
            Call chkVol()
            init_table()
            init_Statictable()
            If cal_profit() = True Then
                'If ChangeFlg = True Then
                '    If tbcon.SelectedTab.Name = "tbprofitchart" Then
                '        SetResultButtonText(cmdresult, chtprofit, grdprofit, lblprofit, True)
                '    End If
                'Else
                '    If chtprofit.Visible = False Then chtprofit.Visible = True
                'End If
            Else
                Exit Sub
            End If
            create_chart_profit()
            create_chart_profit_Full()

            If cal_delta() = True Then
                'If ChangeFlg = True Then
                '    If tbcon.SelectedTab.Name = "tbchdelta" Then
                '        SetResultButtonText(cmdresult, chtdelta, grddelta, lbldelta, True)
                '    End If
                'End If
            Else
                Exit Sub
            End If
            create_chart_delta()
            create_chart_delta_Full()
            If cal_gamma() = True Then
                'If ChangeFlg = True Then
                '    If tbcon.SelectedTab.Name = "tbchgamma" Then
                '        SetResultButtonText(cmdresult, chtgamma, grdgamma, lblgamma, True)
                '    End If
                'End If
            Else
                Exit Sub
            End If
            create_chart_gamma()
            Call create_chart_gamma_Full()

            If cal_vega() = True Then
                'If ChangeFlg = True Then
                '    If tbcon.SelectedTab.Name = "tbchvega" Then
                '        SetResultButtonText(cmdresult, chtvega, grdvega, lblvega, True)
                '    End If
                'End If
            Else
                Exit Sub
            End If
            create_chart_vega()
            create_chart_vega_Full()

            If cal_theta() = True Then
                'If ChangeFlg = True Then
                '    If tbcon.SelectedTab.Name = "tbchtheta" Then
                '        SetResultButtonText(cmdresult, chttheta, grdtheta, lbltheta, True)
                '    End If
                'End If
            Else
                Exit Sub
            End If
            create_chart_theta()
            create_chart_theta_Full()

            If cal_volga() = True Then
                'If ChangeFlg = True Then
                '    If tbcon.SelectedTab.Name = "tbchvolga" Then
                '        SetResultButtonText(cmdresult, chtvolga, grdvolga, lblvolga, True)
                '    End If
                'End If
            Else
                Exit Sub
            End If
            create_chart_volga()
            create_chart_volga_Full()


            If cal_vanna() = True Then
                'If ChangeFlg = True Then
                '    If tbcon.SelectedTab.Name = "tbchvanna" Then
                '        SetResultButtonText(cmdresult, chtvanna, grdvanna, lblvanna, True)
                '    End If
                'End If
            Else
                Exit Sub
            End If
            create_chart_vanna()
            create_chart_vanna_Full()

            'If mAllCV Is Not Nothing Then
            If mAllCV.Trim = "" Then
                If CmdAllCV.Text = "Show &All Charts(F8)" Then
                    CmdAllCV.Text = "Show &All Values(F8)"
                    mAllCV = "Charts"
                    cmdresult.Text = "&Value(F1)"
                Else
                    CmdAllCV.Text = "Show &All Charts(F8)"
                    mAllCV = "Values"
                    cmdresult.Text = "&Chart(F1)"
                End If
            Else
                CmdAllCV.Text = "Show &All Values(F8)"
                mAllCV = "Charts"
                cmdresult.Text = "&Value(F1)"
            End If
            'End If
            'grdprofit.DataSource = profit
            'grdprofit.Refresh()
            'grddelta.DataSource = deltatable
            'grddelta.Refresh()
            'grdgamma.DataSource = gammatable
            'grdgamma.Refresh()
            'grdvega.DataSource = vegatable
            'grdvega.Refresh()
            'grdtheta.DataSource = thetatable
            'grdtheta.Refresh()
        End If
    End Sub
    Private Sub SetResultButtonText(ByVal Cmd As Button, ByVal cht As AxMSChart20Lib.AxMSChart, ByVal grd As DataGridView, ByVal Lbl As System.Windows.Forms.Label, ByVal SetVisiblity As Boolean)
        If Cmd.Text <> "&Chart(F1)" Then
            If SetVisiblity = True Then
                Cmd.Text = "&Chart(F1)"
                'cht.Visible = False
                Lbl.Visible = False
                'grd.Visible = True
            Else
                Cmd.Text = "&Value(F1)"
            End If
        Else
            If SetVisiblity = True Then
                Cmd.Text = "&Value(F1)"
                'cht.Visible = True
                Lbl.Visible = True
                'grd.Visible = False
            Else
                Cmd.Text = "&Chart(F1)"
            End If
        End If
    End Sub
    Private Sub SetChartGridForAll(ByVal cht As AxMSChart20Lib.AxMSChart, ByVal grd As DataGridView, ByVal lbl As System.Windows.Forms.Label, ByVal Flg As Boolean)
        'cht.Visible = Flg
        lbl.Visible = Flg
        'grd.Visible = Not Flg
    End Sub
    Private Sub setGridTrad()
        Dim intX As Integer
        intX = Screen.PrimaryScreen.Bounds.Width
        Dim diff As Double = intX - 1024

        If diff = 0 Then
            Exit Sub
        ElseIf diff > 0 Then
            diff = diff / 16
            Dim i As Integer
            For i = 0 To grdtrad.Columns.Count - 1
                grdtrad.Columns(i).Width += diff + 2
            Next
        Else
            diff = diff / 16
            Dim i As Integer
            For i = 0 To grdtrad.Columns.Count - 1
                grdtrad.Columns(i).Width += diff
            Next
        End If
    End Sub
    Public Function MaxValOfIntArray(ByRef TheArray As Object) As Double
        'This function gives max value of int array without sorting an array
        Dim i As Integer
        Dim MaxIntegersIndex As Integer
        MaxIntegersIndex = 0

        For i = 1 To UBound(TheArray)
            If Val(TheArray(i)) > Val(TheArray(MaxIntegersIndex)) Then
                MaxIntegersIndex = i
            End If
        Next
        'index of max value is MaxValOfIntArray
        MaxValOfIntArray = Val(TheArray(MaxIntegersIndex))
    End Function
    Public Function MinValOfIntArray(ByRef TheArray As Object) As Double
        'This function gives max value of int array without sorting an array
        Dim i As Integer
        Dim MaxIntegersIndex As Integer
        MaxIntegersIndex = 0

        For i = 1 To UBound(TheArray)
            If Val(TheArray(i)) < Val(TheArray(MaxIntegersIndex)) Then
                MaxIntegersIndex = i
            End If
        Next
        'index of max value is MaxValOfIntArray
        MinValOfIntArray = Val(TheArray(MaxIntegersIndex))
    End Function
    REM On change of cmp , if interval is 0, it calculate interval
    Private Sub CalInterval()
        Dim MidStrike As Double = Val(TxtMStrike.Text)
        If Val(MidStrike) > 0 Then
            txtmid = MidStrike
            If Val(txtmid) < 100 Then
                txtinterval.Text = 1
            ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
                txtinterval.Text = 5
            ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
                txtinterval.Text = 10
            ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
                txtinterval.Text = 100
            ElseIf Val(txtmid) > 10000 Then
                txtinterval.Text = 500
            End If
            interval = txtinterval.Text
        End If
    End Sub

#End Region

#Region "Calculation"
    Private Sub CalDatastkprice(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal mT As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal grow As DataGridViewRow, ByVal qty As Double, ByVal lv As Double, ByVal DiffFactor As Double)
        Try
            futval = futval + DiffFactor
            Dim mDelta As Double
            Dim mGama As Double
            Dim mVega As Double
            Dim mThita As Double
            Dim mVolga As Double
            Dim mVanna As Double
            Dim mRah As Double
            Dim mVolatility As Double
            Dim tmpcpprice As Double = 0
            tmpcpprice = cpprice

            Dim mD1 As Double
            Dim mD2 As Double

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
                _mt = 0.0001
            Else
                _mt = (mT) / 365

            End If
            mVolatility = Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6) & "")
            'mVolatility = lv
            Try

                'If Not mIsFut Then
                mDelta = mDelta + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1) & "")

                mGama = mGama + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2) & "")

                mVega = mVega + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3) & "")

                mThita = mThita + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4) & "")

                mRah = mRah + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5) & "")

                mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
                mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

                mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
                mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)

                'Else
                ''mDelta = mDelta + (1 * drow("Qty"))
                'End If



                grow.Cells("lv").Value = mVolatility * 100
                grow.Cells("delta").Value = Math.Round(mDelta, DecimalSetting.iRDelta)

                grow.Cells("deltaval").Value = Math.Round(mDelta * qty, DecimalSetting.iRDeltaval)

                grow.Cells("gamma").Value = Math.Round(mGama, DecimalSetting.iRGamma)
                grow.Cells("gmval").Value = Math.Round(mGama * qty, DecimalSetting.iRGammaval)
                grow.Cells("vega").Value = Math.Round(mVega, DecimalSetting.iRVega)
                grow.Cells("vgval").Value = Math.Round(mVega * qty, DecimalSetting.iRVegaval)
                grow.Cells("theta").Value = Math.Round(mThita, DecimalSetting.iRTheta)
                grow.Cells("thetaval").Value = Math.Round(mThita * qty, DecimalSetting.iRThetaval)
                grow.Cells("volga").Value = Math.Round(mVolga, DecimalSetting.iRVolga)
                grow.Cells("volgaval").Value = Math.Round(mVolga * qty, DecimalSetting.iRVolgaval)
                grow.Cells("vanna").Value = Math.Round(mVanna, DecimalSetting.iRVanna)
                grow.Cells("vannaval").Value = Math.Round(mVanna * qty, DecimalSetting.iRVannaval)

                'grow.Cells(8).Value = Math.Round(mVolatility * 100, 2)
                'grow.cells(10).value = Format(mDelta, Deltastr)
                'grow.cells(11).value = Format(mDelta * qty, DecimalSetting.sDeltaval)
                'grow.cells(12).value = Format(mGama, DecimalSetting.sGamma)
                'grow.cells(13).value = Format(mGama * qty, DecimalSetting.sGammaval)
                'grow.cells(14).value = Format(mVega, DecimalSetting.sVega)
                'grow.cells(15).value = Format(mVega * qty, DecimalSetting.sVegaval)
                'grow.cells(16).value = Format(mThita, DecimalSetting.sTheta)
                'grow.cells(17).value = Format(mThita * qty, DecimalSetting.sThetaval)
                grdtrad.EndEdit()
            Catch ex As Exception

            End Try
        Catch ex As Exception
            ' MsgBox(ex.ToString)
        End Try


        ''MsgBox(mDelta)

    End Sub
    Private Sub CalData(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal mT As Double, ByVal mmT As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal grow As DataGridViewRow, ByVal qty As Double, ByVal lv As Double, ByVal DifFactor As Double)
        futval = futval + DifFactor
        Dim mDelta As Double
        Dim mGama As Double
        Dim mVega As Double
        Dim mThita As Double
        Dim mVolga As Double
        Dim mVanna As Double
        Dim mRah As Double

        Dim mD1 As Double
        Dim mD2 As Double
        Dim mD11 As Double
        Dim mD21 As Double

        ''divyesh
        Dim mDelta1 As Double
        Dim mGama1 As Double
        Dim mVega1 As Double
        Dim mThita1 As Double
        Dim mVolga1 As Double
        Dim mVanna1 As Double
        Dim mRah1 As Double

        Dim mVolatility As Double
        Dim tmpcpprice As Double = 0
        Dim tmpcpprice1 As Double = 0

        Dim cpprice1 As Double = 0

        tmpcpprice = cpprice

        tmpcpprice1 = cpprice1

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

        mDelta1 = 0
        mGama1 = 0
        mVega1 = 0
        mThita1 = 0
        mVolga1 = 0
        mVanna1 = 0
        mRah1 = 0

        Dim _mt, _mt1 As Double
        'IF MATURITY IS 0 THEN _MT = 0.00001 
        If mT = 0 Then
            _mt = 0.0001
            _mt1 = 0.00001
        Else
            _mt = (mT) / 365
            _mt1 = (mmT) / 365
        End If
        'mVolatility = mObjData.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
        mVolatility = lv
        'mVolatility1 = lv
        Try
            'If Not mIsFut Then
            tmpcpprice = Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0) & "")
            mDelta = mDelta + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1) & "")
            mGama = mGama + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2) & "")
            mVega = mVega + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3) & "")
            mThita = mThita + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4) & "")
            mRah = mRah + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5) & "")

            mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
            mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

            mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
            mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)


            tmpcpprice1 = Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 0) & "")
            mDelta1 = mDelta1 + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 1) & "")
            mGama1 = mGama1 + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 2) & "")
            mVega1 = mVega1 + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 3) & "")
            mThita1 = mThita1 + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 4) & "")
            mRah1 = mRah1 + Val(Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 5) & "")


            mD11 = mD11 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt1)
            mD21 = mD21 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt1)

            mVolga1 = mVolga1 + CalVolga(mVega1, mD11, mD21, mVolatility)
            mVanna1 = mVanna1 + CalVanna(futval, mVega, mD11, mD21, mVolatility, _mt1)


            'Else
            ''mDelta = mDelta + (1 * drow("Qty"))
            'End If

            'grow.Cells(9).Value = Math.Round(tmpcpprice, round)
            'grow.cells(10).value = Math.Round(mDelta, round)
            'grow.cells(11).value = Math.Round(mDelta * qty, round)
            'grow.cells(12).value = Math.Round(mThita, round)
            'grow.cells(13).value = Math.Round(mThita * qty, round)
            'grow.cells(14).value = Math.Round(mVega, round)
            'grow.cells(15).value = Math.Round(mVega * qty, round)
            'grow.cells(16).value = Math.Round(mGama, round)
            'grow.cells(17).value = Math.Round(mGama * qty, round)

            grow.Cells("last").Value = Math.Round(tmpcpprice, 2)
            grow.Cells("delta").Value = Math.Round(mDelta, DecimalSetting.iRDelta)
            grow.Cells("deltaval").Value = Math.Round(mDelta * qty, DecimalSetting.iRDeltaval)
            grow.Cells("gamma").Value = Math.Round(mGama, DecimalSetting.iRGamma)
            grow.Cells("gmval").Value = Math.Round(mGama * qty, DecimalSetting.iRGammaval)
            grow.Cells("vega").Value = Math.Round(mVega, DecimalSetting.iRVega)
            grow.Cells("vgval").Value = Math.Round(mVega * qty, DecimalSetting.iRVegaval)
            grow.Cells("theta").Value = Math.Round(mThita, DecimalSetting.iRTheta)
            grow.Cells("thetaval").Value = Math.Round(mThita * qty, DecimalSetting.iRThetaval)

            grow.Cells("volga").Value = Math.Round(mVolga, DecimalSetting.iRVolga)
            grow.Cells("volgaval").Value = Math.Round(mVolga * qty, DecimalSetting.iRVolgaval)
            grow.Cells("vanna").Value = Math.Round(mVanna, DecimalSetting.iRTheta)
            grow.Cells("vannaval").Value = Math.Round(mVanna * qty, DecimalSetting.iRVannaval)


            grow.Cells("ltp1").Value = Math.Round(tmpcpprice1, 2)
            grow.Cells("delta1").Value = Math.Round(mDelta1, DecimalSetting.iRDelta)
            grow.Cells("deltaval1").Value = Math.Round(mDelta1 * qty, DecimalSetting.iRDeltaval)
            grow.Cells("gamma1").Value = Math.Round(mGama1, DecimalSetting.iRDelta)
            grow.Cells("gmval1").Value = Math.Round(mGama1 * qty, DecimalSetting.iRGammaval)
            grow.Cells("vega1").Value = Math.Round(mVega1, DecimalSetting.iRDelta)
            grow.Cells("vgval1").Value = Math.Round(mVega1 * qty, DecimalSetting.iRVegaval)
            grow.Cells("theta1").Value = Math.Round(mThita1, DecimalSetting.iRDelta)
            grow.Cells("thetaval1").Value = Math.Round(mThita1 * qty, DecimalSetting.iRThetaval)

            grow.Cells("volga1").Value = Math.Round(mVolga1, DecimalSetting.iRVolga)
            grow.Cells("volgaval1").Value = Math.Round(mVolga1 * qty, DecimalSetting.iRVolgaval)
            grow.Cells("vanna1").Value = Math.Round(mVanna1, DecimalSetting.iRVanna)
            grow.Cells("vannaval1").Value = Math.Round(mVanna1 * qty, DecimalSetting.iRVannaval)


            'grow.Cells(9).Value = Math.Round(tmpcpprice, 2)
            'grow.cells(10).value = Format(mDelta, Deltastr)
            'grow.cells(11).value = Format(mDelta * qty, DeltastrVal)
            'grow.cells(12).value = Format(mGama, DecimalSetting.sGamma)
            'grow.cells(13).value = Format(mGama * qty, DecimalSetting.sGammaval)
            'grow.cells(14).value = Format(mVega, DecimalSetting.sVega)
            'grow.cells(15).value = Format(mVega * qty, DecimalSetting.sVegaval)
            'grow.cells(16).value = Format(mThita, DecimalSetting.sTheta)
            'grow.cells(17).value = Format(mThita * qty, DecimalSetting.sThetaval)
            grdtrad.EndEdit()
        Catch ex As Exception

        End Try

        ''MsgBox(mDelta)


    End Sub

    Private Sub cal_exp()
        Dim exptable As New DataTable

        exptable = objExp.Select_Expenses

        Dim texp As Double = 0

        For Each drow As DataGridViewRow In grdtrad.Rows
            If drow.Cells("Active").Value = False Then Continue For
            If Val(drow.Cells("units").Value) > 0 Then
                If drow.Cells("CPF").Value = "F" Then
                    texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(futl)", "")) / exptable.Compute("max(futlp)", "")))
                Else
                    If Val(exptable.Compute("max(spl)", "")) <> 0 Then
                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * (Val(drow.Cells("last").Value) + Val(drow.Cells("last").Value))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", "")))
                    Else
                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", "")))
                    End If
                End If
            Else
                If drow.Cells("CPF").Value = "F" Then
                    texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(futs)", "")) / exptable.Compute("max(futsp)", "")))
                Else
                    If Val(exptable.Compute("max(spl)", "")) <> 0 Then
                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * (Val(drow.Cells("last").Value) + Val(drow.Cells("last").Value))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", "")))
                    Else
                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", "")))
                    End If
                End If
            End If
        Next
        txtexp.Text = Math.Round(texp, 2)
    End Sub
    
    Private Function cal_profit() As Boolean
        grdprofit.DataSource = Nothing
        'grdprofit.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double
        profitarr = New ArrayList
        Dim bval As Double
        Dim bvol As Double

        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean
        'ReDim profitarr(((profit.Rows.Count) * (profit.Columns.Count - 1)))
        For Each dr As DataRow In profit.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In profit.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        Dim cd As Date
                        If (IsDate(col.ColumnName)) Then
                            cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                        Else
                            cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
                        End If
                        If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For
                        If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                            Dim SpotValue As Double = CDbl(dr("SpotValue")) + CDbl(grow.Cells("DifFactor").Value)
                            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                                If CDbl(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Future.")
                                    grdtrad.Focus()
                                    Return False
                                End If
                                If CDbl(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Future.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + ((CDbl(SpotValue) - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value)))
                                'dr(col.ColumnName) = Math.Round(CDbl(dr(col.ColumnName)) + ((CDbl(SpotValue) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), RoundGrossMTM)
                                'dr(col.ColumnName) = Format(CDbl(dr(col.ColumnName)) + ((CDbl(SpotValue) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), GrossMTMstr)

                            Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                'Comment By Alpesh For Ltp 0 is Allow
                                'If CDbl(grow.Cells("ltp").Value) = 0 Then
                                '    MsgBox("Enter Ltp Value for Selected Call or Put")
                                '    grdtrad.Focus()
                                '    Return False
                                'End If

                                If CDbl(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If CDbl(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Exit Function
                                End If

                                If CDbl(grow.Cells("lv").Value) = 0 Then
                                    'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    'grdtrad.Focus()
                                    'Return False

                                End If
                                totcpr = 0
                                'Dim cd As Date
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                Else
                                    cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
                                End If

                                'dcount = DatePart(DateInterval.Day, cd)
                                'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(dtexp.Value.Date))
                                Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                    _mT1 = 0.5
                                End If
                                If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                    If (IsDate(col.ColumnName)) Then
                                        _mT = 0.5
                                    Else
                                        _mT = 0.5
                                    End If

                                End If
                                If _mT = 0 Then
                                    _mT = 0.00001
                                    _mT1 = 0.00001
                                Else
                                    _mT = (_mT) / 365
                                    _mT1 = (_mT1) / 365
                                End If
                                If (IsDate(col.ColumnName)) Then
                                    bval = 0
                                    bvol = 0
                                    'bvol = Greeks.Black_Scholes(CDbl(txtmkt.Text), CDbl(grow.Cells(5).Value), Mrateofinterast, 0, (CDbl(grow.Cells(7).Value)), _mT1, iscall, True, 0, 6)
                                    bval = Greeks.Black_Scholes(spotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value)) / 100, _mT, iscall, True, 0, 0)
                                    totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                    dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
                                    'dr(col.ColumnName) = Math.Round(CDbl(dr(col.ColumnName)) + totcpr, RoundGrossMTM)
                                    ' dr(col.ColumnName) = Format(CDbl(dr(col.ColumnName)) + totcpr,GrossMTMstr)
                                Else
                                    If (iscall = True) Then
                                        If ((CDbl(SpotValue) - CDbl(grow.Cells("Strike").Value)) > 0) Then
                                            bval = (CDbl(SpotValue) - CDbl(grow.Cells("Strike").Value))
                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
                                        Else
                                            bval = 0 ' CDbl(dr(col.ColumnName)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
                                        End If
                                    Else
                                        If ((CDbl(grow.Cells("Strike").Value) - CDbl(SpotValue)) > 0) Then
                                            bval = (CDbl(grow.Cells("Strike").Value) - CDbl(SpotValue))
                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
                                        Else
                                            bval = 0 'CDbl(dr(col.ColumnName)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
                                        End If
                                    End If
                                End If

                            End If

                        End If


                    Next
                    dr(col.ColumnName) += grossmtm
                    dcount = dcount + 1
                End If
                i = i + 1


                profitarr.Add(dr(col.ColumnName))

            Next
        Next
        grdprofit.DataSource = profit

        Try
            grdprofit.ClearSelection()
            For Each grow As DataGridViewRow In grdprofit.Rows
                If grow.Cells(1).Value = 0 Then
                    grow.Selected = True
                    grdprofit.FirstDisplayedScrollingRowIndex = (Val(txtllimit.Text) / 2) 'grow.Index
                    Exit For
                Else
                    'grow.Selected = False
                End If
            Next
        Catch ex As Exception
        End Try

        Return True

        'For Each col As DataColumn In profit.Columns
        '    If UCase(col.ColumnName) <> UCase("spotvalue") Then
        '        For Each drow As DataRow In profit.Rows

        '        Next
        '    End If
        'Next

        'grdprofit.Refresh()
    End Function

    Private Function cal_delta() As Boolean


        grddelta.DataSource = Nothing
        'grddelta.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double
        Dim bval As Double
        Dim bvol As Double


        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean

        For Each dr As DataRow In deltatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In deltatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then


                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        Dim cd As Date
                        If (IsDate(col.ColumnName)) Then
                            cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                        Else
                            cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
                        End If
                        If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For

                        If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                            Dim SpotValue As Double = CDbl(dr("SpotValue")) + CDbl(grow.Cells("DifFactor").Value)
                            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Future.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Future.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + Val(grow.Cells("deltaval").Value), DecimalSetting.iRDeltaval)
                                ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + val(grow.cells(11).value), DecimalSetting.sDeltaval)

                            Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    'grdtrad.Focus()
                                    'Return False

                                End If

                                totcpr = 0
                                'Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0

                                    'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    bvol = Val(grow.Cells("lv").Value) / 100
                                    bval = Greeks.Black_Scholes(SpotValue, Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 1)
                                    totcpr = (Val(bval & "") * Val(grow.Cells("units").Value))
                                    dr(col.ColumnName) = Val(dr(col.ColumnName)) + totcpr
                                    ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sDeltaval)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                deltaarr.Add(dr(col.ColumnName))
            Next
        Next

        grddelta.DataSource = deltatable

        Try
            grddelta.ClearSelection()
            For Each grow As DataGridViewRow In grddelta.Rows
                If grow.Cells(1).Value = 0 Then
                    grow.Selected = True
                    grddelta.FirstDisplayedScrollingRowIndex = (Val(txtllimit.Text) / 2) 'grow.Index
                    Exit For
                Else
                    'grow.Selected = False
                End If
            Next
        Catch ex As Exception
        End Try

        Return True

        'grddelta.Refresh()
    End Function
    Private Function cal_gamma() As Boolean
        grdgamma.DataSource = Nothing
        'grdgamma.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double

        Dim bval As Double
        Dim bvol As Double

        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean

        For Each dr As DataRow In gammatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In gammatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        Dim cd As Date
                        If (IsDate(col.ColumnName)) Then
                            cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                        Else
                            cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
                        End If
                        If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For

                        If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                            Dim SpotValue As Double = CDbl(dr("SpotValue")) + CDbl(grow.Cells("DifFactor").Value)
                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                '    'If val(grow.Cells(2).Value) = 0 Then
                                '    '    MsgBox("Enter Spot Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'If val(grow.Cells(5).Value) = 0 Then
                                '    '    MsgBox("Enter Units Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

                                'Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False


                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    'grdtrad.Focus()
                                    'Return False

                                End If
                                totcpr = 0
                                'Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0
                                    'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    bvol = Val(grow.Cells("lv").Value) / 100
                                    bval = Greeks.Black_Scholes(SpotValue, Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 2)
                                    totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                    dr(col.ColumnName) = Val(dr(col.ColumnName)) + totcpr
                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sGammaval)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                gammaarr.Add(dr(col.ColumnName))
            Next
        Next

        grdgamma.DataSource = gammatable

        Try
            grdgamma.ClearSelection()
            For Each grow As DataGridViewRow In grdgamma.Rows
                If grow.Cells(1).Value = 0 Then
                    grow.Selected = True
                    grdgamma.FirstDisplayedScrollingRowIndex = (Val(txtllimit.Text) / 2) 'grow.Index
                    Exit For
                Else
                    'grow.Selected = False
                End If
            Next
        Catch ex As Exception
        End Try

        Return True

        'grdgamma.Refresh()
    End Function
    Private Function cal_vega() As Boolean

        grdvega.DataSource = Nothing
        'grdvega.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double
        Dim bval As Double
        Dim bvol As Double


        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean

        For Each dr As DataRow In vegatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In vegatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then


                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        Dim cd As Date
                        If (IsDate(col.ColumnName)) Then
                            cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                        Else
                            cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
                        End If
                        If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For

                        If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                            Dim SpotValue As Double = CDbl(dr("SpotValue")) + CDbl(grow.Cells("DifFactor").Value)
                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                '    'If val(grow.Cells(2).Value) = 0 Then
                                '    '    MsgBox("Enter Spot Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'If val(grow.Cells(5).Value) = 0 Then
                                '    '    MsgBox("Enter Units Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

                                'Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    'grdtrad.Focus()
                                    'Return False

                                End If
                                totcpr = 0
                                'Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0
                                    'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    bvol = Val(grow.Cells("lv").Value) / 100
                                    bval = Greeks.Black_Scholes(SpotValue, Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 3)
                                    totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                    dr(col.ColumnName) = Val(dr(col.ColumnName)) + totcpr
                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sVegaval)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                vegaarr.Add(dr(col.ColumnName))
            Next
        Next

        grdvega.DataSource = vegatable
        Try
            grdvega.ClearSelection()
            For Each grow As DataGridViewRow In grdvega.Rows
                If grow.Cells(1).Value = 0 Then
                    grow.Selected = True
                    grdvega.FirstDisplayedScrollingRowIndex = (Val(txtllimit.Text) / 2) 'grow.Index
                    Exit For
                Else
                    'grow.Selected = False
                End If
            Next
        Catch ex As Exception
        End Try
        Return True

        'grdvega.Refresh()

    End Function
    Private Function cal_theta() As Boolean
        grdtheta.DataSource = Nothing
        'grdtheta.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double

        thetaarr = New ArrayList
        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean
        Dim bval As Double
        Dim bvol As Double

        For Each dr As DataRow In thetatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In thetatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        Dim cd As Date
                        If (IsDate(col.ColumnName)) Then
                            cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                        Else
                            cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
                        End If
                        If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For
                        If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                            Dim SpotValue As Double = CDbl(dr("SpotValue")) + CDbl(grow.Cells("DifFactor").Value)
                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                '    'If val(grow.Cells(2).Value) = 0 Then
                                '    '    MsgBox("Enter Spot Value for Selected Future")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'If val(grow.Cells(5).Value) = 0 Then
                                '    '    MsgBox("Enter Units Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

                                'Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    'grdtrad.Focus()
                                    'Return False

                                End If
                                totcpr = 0
                                'Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0
                                    'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    bvol = Val(grow.Cells("lv").Value) / 100
                                    bval = Greeks.Black_Scholes(SpotValue, Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 4)
                                    'totcpr = Math.Round((Math.Round(bval, DecimalSetting.iRTheta) * Val(grow.Cells("units").Value)), DecimalSetting.iRThetaval)
                                    totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                    dr(col.ColumnName) = Val(dr(col.ColumnName)) + totcpr
                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sThetaval)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                thetaarr.Add(dr(col.ColumnName))
            Next
        Next

        grdtheta.DataSource = thetatable
        Try
            grdtheta.ClearSelection()
            For Each grow As DataGridViewRow In grdtheta.Rows
                If grow.Cells(1).Value = 0 Then
                    grow.Selected = True
                    grdtheta.FirstDisplayedScrollingRowIndex = (Val(txtllimit.Text) / 2) 'grow.Index
                    Exit For
                Else
                    'grow.Selected = False
                End If
            Next
        Catch ex As Exception
        End Try
        Return True

        'grdtheta.Refresh()

    End Function

    Private Function cal_volga() As Boolean
        grdvolga.DataSource = Nothing
        'grdvolga.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double

        volgaarr = New ArrayList
        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean
        Dim bval As Double
        Dim bvol As Double
        Dim mD1 As Double
        Dim mD2 As Double
        Dim mVega As Double

        For Each dr As DataRow In volgatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In volgatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        Dim cd As Date
                        If (IsDate(col.ColumnName)) Then
                            cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                        Else
                            cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
                        End If
                        If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For
                        If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                            Dim SpotValue As Double = CDbl(dr("SpotValue")) + CDbl(grow.Cells("DifFactor").Value)
                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                '    'If val(grow.Cells(2).Value) = 0 Then
                                '    '    MsgBox("Enter Spot Value for Selected Future")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'If val(grow.Cells(5).Value) = 0 Then
                                '    '    MsgBox("Enter Units Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

                                'Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    'grdtrad.Focus()
                                    'Return False

                                End If
                                totcpr = 0
                                'Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0

                                    'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    bvol = Val(grow.Cells("lv").Value) / 100
                                    'mVega = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 3)
                                    'mD1 = CalD1(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
                                    'mD2 = CalD2(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
                                    'bval = CalVolga(mVega, mD1, mD2, bvol)
                                    bval = Greeks.Black_Scholes(SpotValue, Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 8)
                                    totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                    dr(col.ColumnName) = Val(dr(col.ColumnName)) + totcpr
                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sVolgaval)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                volgaarr.Add(dr(col.ColumnName))
                'If volgaarr(3) = "NaN" Then MsgBox("A")
            Next
        Next

        grdvolga.DataSource = volgatable
        Try
            grdvolga.ClearSelection()
            For Each grow As DataGridViewRow In grdvolga.Rows
                If grow.Cells(1).Value = 0 Then
                    grow.Selected = True
                    grdvolga.FirstDisplayedScrollingRowIndex = (Val(txtllimit.Text) / 2) 'grow.Index
                    Exit For
                Else
                    'grow.Selected = False
                End If
            Next
        Catch ex As Exception
        End Try
        Return True

        'grdvolga.Refresh()

    End Function
    Private Function cal_vanna() As Boolean
        grdvanna.DataSource = Nothing
        'grdvanna.Refresh()
        Dim totfpr As Double
        Dim totcpr As Double

        vannaarr = New ArrayList
        Dim i As Integer
        i = 0
        Dim dcount As Integer
        dcount = 0
        Dim iscall As Boolean
        Dim bval As Double
        Dim bvol As Double
        Dim mD1 As Double
        Dim mD2 As Double
        Dim mVega As Double

        For Each dr As DataRow In vannatable.Rows
            totfpr = 0
            dcount = 0
            For Each col As DataColumn In vannatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    For Each grow As DataGridViewRow In grdtrad.Rows
                        If grow.Cells("Active").Value = False Then Continue For
                        Dim cd As Date
                        If (IsDate(col.ColumnName)) Then
                            cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                        Else
                            cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
                        End If
                        If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For
                        If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                            Dim SpotValue As Double = CDbl(dr("SpotValue")) + CDbl(grow.Cells("DifFactor").Value)
                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                '    'If val(grow.Cells(2).Value) = 0 Then
                                '    '    MsgBox("Enter Spot Value for Selected Future")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'If val(grow.Cells(5).Value) = 0 Then
                                '    '    MsgBox("Enter Units Value for Selected Futre")
                                '    '    grdtrad.Focus()
                                '    '    Exit Sub
                                '    'End If
                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

                                'Else
                                If grow.Cells("CPF").Value = "C" Then
                                    iscall = True
                                Else
                                    iscall = False
                                End If
                                If Val(grow.Cells("last").Value) = 0 Then
                                    MsgBox("Enter Rate Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("units").Value) = 0 Then
                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If
                                If Val(grow.Cells("spval").Value) = 0 Then
                                    MsgBox("Enter Spot Value for Selected Call or Put.")
                                    grdtrad.Focus()
                                    Return False

                                End If

                                If Val(grow.Cells("lv").Value) = 0 Then
                                    'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                    'grdtrad.Focus()
                                    'Return False

                                End If
                                totcpr = 0
                                'Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                If (IsDate(col.ColumnName)) Then
                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT = 0.5
                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    bval = 0
                                    bvol = 0

                                    'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)

                                    'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                    bvol = Val(grow.Cells("lv").Value) / 100
                                    'mVega = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 3)
                                    'mD1 = CalD1(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
                                    'mD2 = CalD2(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
                                    'bval = CalVanna(Val(dr("SpotValue")), mVega, mD1, mD2, bvol, _mT)
                                    bval = Greeks.Black_Scholes(SpotValue, Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 7)

                                    totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                    dr(col.ColumnName) = Val(dr(col.ColumnName)) + totcpr
                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sVannaval)
                                End If
                            End If
                        End If
                    Next
                    dcount = dcount + 1
                End If
                i = i + 1
                vannaarr.Add(dr(col.ColumnName))
            Next
        Next

        grdvanna.DataSource = vannatable

        Try
            grdvanna.ClearSelection()
            For Each grow As DataGridViewRow In grdvanna.Rows
                If grow.Cells(1).Value = 0 Then
                    grow.Selected = True
                    grdvanna.FirstDisplayedScrollingRowIndex = (Val(txtllimit.Text) / 2) 'grow.Index
                    Exit For
                Else
                    'grow.Selected = False
                End If
            Next
        Catch ex As Exception
        End Try
        Return True

        'grdvanna.Refresh()

    End Function

    Private Sub create_active(Optional ByVal WithOutVol As Boolean = False)

        grdact.Columns.Clear()

        
        Dim i As Integer = 0
        If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        i += 1
        Dim gcol As DataGridViewCheckBoxColumn
        Dim temptable As New DataTable
        temptable = New DataTable
        Dim tdate As Date
        For k As Integer = 0 To i - 1
            tdate = Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MMM/yyyy")
            'If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
            gcol = New DataGridViewCheckBoxColumn
            gcol.HeaderText = tdate.ToString("dd-MMM")
            gcol.DataPropertyName = tdate
            gcol.Width = 70
            grdact.Columns.Add(gcol)
            temptable.Columns.Add(tdate, GetType(Boolean))
            gcol.DefaultCellStyle.NullValue = False
            'End If
        Next

        tdate = Format(dtexp.Value, "dd/MMM/yyyy")
        'If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
        gcol = New DataGridViewCheckBoxColumn
        gcol.HeaderText = "Expiry"
        gcol.DataPropertyName = "Expiry " & tdate
        grdact.Columns.Add(gcol)
        temptable.Columns.Add("Expiry " & tdate, GetType(Boolean))
        gcol.DefaultCellStyle.NullValue = False
        'End If

        If grdact.Columns.Count > 0 Then
            Dim drow As DataRow
            drow = temptable.NewRow
            'For k As Integer = 0 To i - 1
            tdate = Format(DateAdd(DateInterval.Day, 0, dttoday.Value), "dd/MMM/yyyy")
            If (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date)) <> 0 Then
                If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
                    drow(tdate) = True
                Else
                    drow(tdate) = False
                End If
            End If

            Try
                tdate = Format(DateAdd(DateInterval.Day, (i - 1), dttoday.Value), "dd/MMM/yyyy")
                If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
                    'drow(tdate) = True
                    drow("Expiry " & tdate) = True
                Else
                    drow(tdate) = False
                    drow("Expiry " & tdate) = False
                End If
            Catch ex As Exception
                MsgBox("Beginning date can't be greater than expiry date.")
                Exit Sub
            End Try
            Dim t As Integer
            t = Math.Round((Val(grdact.ColumnCount - 1)) / 2, 0)
            tdate = Format(DateAdd(DateInterval.Day, t, dttoday.Value), "dd/MMM/yyyy")
            If (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date)) <> 0 Then
                If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
                    drow(tdate) = True
                Else
                    drow(tdate) = False
                End If
            End If
            ' Next
            temptable.Rows.Add(drow)
            grdact.DataSource = temptable
            'grdact.Refresh()
        End If

        'assign expiry date to selected expiry date
        If frmFlg = True Then 'this flg used bcoz first need not to assign expiry date
            For Each grow As DataGridViewRow In grdtrad.Rows
                If grow.Cells("TimeII").ReadOnly = False Then
                    grow.Cells("TimeII").Value = dtexp.Value
                End If
            Next
        End If
        frmFlg = True
        'If grdtrad.Rows(grdtrad.NewRowIndex).Cells("TimeII").ReadOnly = False Then
        '    grdtrad.Rows(grdtrad.NewRowIndex).Cells("TimeII").Value = dtexp.Value
        'End If

        If grdtrad.Rows.Count > 1 Then
            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells(4).Value = grdtrad.Rows(grdtrad.Rows.Count - 2).Cells(4).Value
        End If

        If WithOutVol = True Or (dttoday.Value = dtexp.Value) Then
            bCheckAll = True
            tdate = Format(dtexp.Value, "dd/MMM/yyyy")
            For Each col As DataGridViewColumn In grdact.Columns
                grdact.Rows(0).Cells(col.Index).Value = False
            Next
            grdact.Rows(0).Cells((grdact.Columns.Count - 1)).Value = True
            'drow("Expiry " & tdate) = False
            bCheckAll = False
        End If
    End Sub

    Private Sub create_chart_profit()
        chtprofit.ColumnCount = 0
        chtprofit.ColumnLabelCount = 0
        chtprofit.ColumnLabelIndex = 0
        chtprofit.AutoIncrement = False
        chtprofit.RowCount = 0
        chtprofit.RowLabelCount = 0
        chtprofit.RowLabelIndex = 0
        chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


        If profit.Columns.Count > 1 Then
            If profit.Columns.Count < 3 Then
                Exit Sub
            End If
            chtprofit.ColumnCount = profit.Columns.Count - 3
            chtprofit.ColumnLabelCount = 1
            chtprofit.ColumnLabelIndex = 1
            chtprofit.AutoIncrement = False
            chtprofit.RowCount = profit.Rows.Count - 1
            chtprofit.RowLabelCount = 1
            chtprofit.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(profitarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(profitarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            'With chtprofit
            '    Dim div As Double
            '    div = (((Math.Abs(maxval) - Math.Abs(minval))) * 40) / 100
            '    chtprofit.chartType = VtChChartType.VtChChartType2dLine
            '    YAxis = .Plot.Axis(VtChAxisId.VtChAxisIdY, 1)
            '    'Set Axis Scale Min and Max values      
            '    YAxis.ValueScale.Auto = False
            '    YAxis.CategoryScale.Auto = False
            '    YAxis.ValueScale.Minimum = minval
            '    YAxis.ValueScale.Maximum = maxval
            '    YAxis.ValueScale.MajorDivision = Format(((Math.Abs(maxval) - Math.Abs(minval)) \ div), "##0")
            '    YAxis.ValueScale.MinorDivision = 2
            'End With
            'YAxis = Nothing


            chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(profit.Rows.Count - 1, profit.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In profit.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    r = 0
                    For Each drow As DataRow In profit.Rows
                        cha(r, c) = Val(drow(col.ColumnName)) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblprofit.Text = "Rs.in ( " & str & ")"
            Else
                lblprofit.Text = "-"
            End If
            chtprofit.ChartData = cha

            chtprofit.ShowLegend = False
            c = 1
            Dim i As Integer = 0





            For Each col As DataColumn In profit.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    chtprofit.Column = c
                    chtprofit.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")

                    i += 1
                    c += 1
                End If
                'If c >= grdprofit.Columns.Count Then
                '    Exit For
                'End If
            Next
            r = 1

            For Each drow As DataRow In profit.Rows
                chtprofit.Row = r
                chtprofit.RowLabel = drow("spotvalue")

                r += 1
            Next
            'MsgBox(chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).AxisGrid.MajorPen.)
            'chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 0).AxisGrid.MajorPen.Style = VtPenStyle.VtPenStyleDotted
            'chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).AxisGrid.MinorPen.Style = VtPenStyle.VtPenStyleDitted
            '  chtprofit.Plot.Axis(0).Labels(0).Format
            'MsgBox(chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).Labels(0))

        End If
    End Sub
    Private Sub create_chart_profit_Full()
        objProfitLossChart.chtprofit.ColumnCount = 0
        objProfitLossChart.chtprofit.ColumnLabelCount = 0
        objProfitLossChart.chtprofit.ColumnLabelIndex = 0
        objProfitLossChart.chtprofit.AutoIncrement = False
        objProfitLossChart.chtprofit.RowCount = 0
        objProfitLossChart.chtprofit.RowLabelCount = 0
        objProfitLossChart.chtprofit.RowLabelIndex = 0
        objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


        If profit.Columns.Count > 2 Then
            objProfitLossChart.chtprofit.ColumnCount = profit.Columns.Count - 3
            objProfitLossChart.chtprofit.ColumnLabelCount = 1
            objProfitLossChart.chtprofit.ColumnLabelIndex = 1
            objProfitLossChart.chtprofit.AutoIncrement = False
            objProfitLossChart.chtprofit.RowCount = profit.Rows.Count - 1
            objProfitLossChart.chtprofit.RowLabelCount = 1
            objProfitLossChart.chtprofit.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(profitarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(profitarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            'With objProfitLossChart.chtprofit
            '    Dim div As Double
            '    div = (((Math.Abs(maxval) - Math.Abs(minval))) * 40) / 100
            '    objProfitLossChart.chtprofit.chartType = VtChChartType.VtChChartType2dLine
            '    YAxis = .Plot.Axis(VtChAxisId.VtChAxisIdY, 1)
            '    'Set Axis Scale Min and Max values      
            '    YAxis.ValueScale.Auto = False
            '    YAxis.CategoryScale.Auto = False
            '    YAxis.ValueScale.Minimum = minval
            '    YAxis.ValueScale.Maximum = maxval
            '    YAxis.ValueScale.MajorDivision = Format(((Math.Abs(maxval) - Math.Abs(minval)) \ div), "##0")
            '    YAxis.ValueScale.MinorDivision = 2
            'End With
            'YAxis = Nothing


            objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(profit.Rows.Count - 1, profit.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In profit.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    r = 0
                    For Each drow As DataRow In profit.Rows
                        cha(r, c) = Val(drow(col.ColumnName)) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblprofit.Text = "Rs.in ( " & str & ")"
            Else
                lblprofit.Text = "-"
            End If
            objProfitLossChart.chtprofit.ChartData = cha

            objProfitLossChart.chtprofit.ShowLegend = False
            c = 1
            Dim i As Integer = 0





            For Each col As DataColumn In profit.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
                    objProfitLossChart.chtprofit.Column = c
                    objProfitLossChart.chtprofit.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")

                    i += 1
                    c += 1
                End If
                'If c >= grdprofit.Columns.Count Then
                '    Exit For
                'End If
            Next
            r = 1

            For Each drow As DataRow In profit.Rows
                objProfitLossChart.chtprofit.Row = r
                objProfitLossChart.chtprofit.RowLabel = drow("spotvalue")

                r += 1
            Next
            'MsgBox(objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).AxisGrid.MajorPen.)
            'objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 0).AxisGrid.MajorPen.Style = VtPenStyle.VtPenStyleDotted
            'objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).AxisGrid.MinorPen.Style = VtPenStyle.VtPenStyleDitted
            '  objProfitLossChart.chtprofit.Plot.Axis(0).Labels(0).Format
            'MsgBox(objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).Labels(0))

        End If

    End Sub
    Private Sub create_chart_delta()
        chtdelta.ColumnCount = 0
        chtdelta.ColumnLabelCount = 0
        chtdelta.ColumnLabelIndex = 0
        chtdelta.AutoIncrement = False
        chtdelta.RowCount = deltatable.Rows.Count - 1
        chtdelta.RowLabelCount = 0
        chtdelta.RowLabelIndex = 0
        chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If deltatable.Columns.Count > 2 Then
            chtdelta.ColumnCount = deltatable.Columns.Count - 3
            chtdelta.ColumnLabelCount = 1
            chtdelta.ColumnLabelIndex = 1
            chtdelta.AutoIncrement = False
            chtdelta.RowCount = deltatable.Rows.Count - 1
            chtdelta.RowLabelCount = 1
            chtdelta.RowLabelIndex = 1

            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(deltaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(deltaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(deltatable.Rows.Count - 1, deltatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In deltatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In deltatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            chtdelta.ChartData = cha
            If div > 1 Then
                lbldelta.Text = "Rs.in ( " & str & ")"
            Else
                lbldelta.Text = "-"
            End If
            chtdelta.ShowLegend = False
            c = 1
            Dim i As Integer = 0

            For Each col As DataColumn In deltatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chtdelta.Column = c
                    chtdelta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grddelta.Columns.Count Then
                    '    Exit For
                    'End If
                End If
            Next
            r = 1
            For Each drow As DataRow In deltatable.Rows
                chtdelta.Row = r
                chtdelta.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_delta_Full()
        objProfitLossChart.chtdelta.ColumnCount = 0
        objProfitLossChart.chtdelta.ColumnLabelCount = 0
        objProfitLossChart.chtdelta.ColumnLabelIndex = 0
        objProfitLossChart.chtdelta.AutoIncrement = False
        objProfitLossChart.chtdelta.RowCount = deltatable.Rows.Count - 1

        objProfitLossChart.chtdelta.RowLabelCount = 0
        objProfitLossChart.chtdelta.RowLabelIndex = 0
        objProfitLossChart.chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If deltatable.Columns.Count > 2 Then
            objProfitLossChart.chtdelta.ColumnCount = deltatable.Columns.Count - 3
            objProfitLossChart.chtdelta.ColumnLabelCount = 1
            objProfitLossChart.chtdelta.ColumnLabelIndex = 1
            objProfitLossChart.chtdelta.AutoIncrement = False
            objProfitLossChart.chtdelta.RowCount = deltatable.Rows.Count - 1
            objProfitLossChart.chtdelta.RowLabelCount = 1
            objProfitLossChart.chtdelta.RowLabelIndex = 1

            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(deltaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(deltaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            objProfitLossChart.chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(deltatable.Rows.Count - 1, deltatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In deltatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In deltatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            objProfitLossChart.chtdelta.ChartData = cha
            If div > 1 Then
                lbldelta.Text = "Rs.in ( " & str & ")"
            Else
                lbldelta.Text = "-"
            End If
            objProfitLossChart.chtdelta.ShowLegend = False
            c = 1
            Dim i As Integer = 0

            For Each col As DataColumn In deltatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chtdelta.Column = c
                    objProfitLossChart.chtdelta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grddelta.Columns.Count Then
                    '    Exit For
                    'End If
                End If
            Next
            r = 1
            For Each drow As DataRow In deltatable.Rows
                objProfitLossChart.chtdelta.Row = r
                objProfitLossChart.chtdelta.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_gamma()
        chtgamma.ColumnCount = 0
        chtgamma.ColumnLabelCount = 0
        chtgamma.ColumnLabelIndex = 0
        chtgamma.AutoIncrement = False
        chtgamma.RowCount = 0
        chtgamma.RowLabelCount = 0
        chtgamma.RowLabelIndex = 0
        chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If gammatable.Columns.Count > 2 Then
            chtgamma.ColumnCount = gammatable.Columns.Count - 3
            chtgamma.ColumnLabelCount = 1
            chtgamma.ColumnLabelIndex = 1
            chtgamma.AutoIncrement = False
            chtgamma.RowCount = gammatable.Rows.Count - 1
            chtgamma.RowLabelCount = 1
            chtgamma.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(gammaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(gammaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(gammatable.Rows.Count - 1, gammatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In gammatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In gammatable.Rows
                        cha(r, c) = Math.Round(Val(drow(col.ColumnName).ToString()), DecimalSetting.iRGammaval) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            
            chtgamma.ChartData = cha

            With chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdY).ValueScale
                .Minimum = GetMinVal(gammatable)
                .Maximum = GetMaxVal(gammatable)
            End With

            If div > 1 Then
                lblgamma.Text = "Rs.in ( " & str & ")"
            Else
                lblgamma.Text = "-"
            End If
            chtgamma.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            For Each col As DataColumn In gammatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chtgamma.Column = c
                    chtgamma.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                    'End If
                End If
            Next
            r = 1
            For Each drow As DataRow In gammatable.Rows
                chtgamma.Row = r
                chtgamma.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_gamma_Full()

        objProfitLossChart.chtgamma.ColumnCount = 0
        objProfitLossChart.chtgamma.ColumnLabelCount = 0
        objProfitLossChart.chtgamma.ColumnLabelIndex = 0
        objProfitLossChart.chtgamma.AutoIncrement = False
        objProfitLossChart.chtgamma.RowCount = 0
        objProfitLossChart.chtgamma.RowLabelCount = 0
        objProfitLossChart.chtgamma.RowLabelIndex = 0
        objProfitLossChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If gammatable.Columns.Count > 2 Then
            objProfitLossChart.chtgamma.ColumnCount = gammatable.Columns.Count - 3
            objProfitLossChart.chtgamma.ColumnLabelCount = 1
            objProfitLossChart.chtgamma.ColumnLabelIndex = 1
            objProfitLossChart.chtgamma.AutoIncrement = False
            objProfitLossChart.chtgamma.RowCount = gammatable.Rows.Count - 1
            objProfitLossChart.chtgamma.RowLabelCount = 1
            objProfitLossChart.chtgamma.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(gammaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(gammaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            objProfitLossChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(gammatable.Rows.Count - 1, gammatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In gammatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In gammatable.Rows
                        cha(r, c) = Math.Round(Val(drow(col.ColumnName)), DecimalSetting.iRGammaval) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            objProfitLossChart.chtgamma.ChartData = cha
            If div > 1 Then
                lblgamma.Text = "Rs.in ( " & str & ")"
            Else
                lblgamma.Text = "-"
            End If
            objProfitLossChart.chtgamma.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            For Each col As DataColumn In gammatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chtgamma.Column = c
                    objProfitLossChart.chtgamma.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                    'End If
                End If
            Next
            r = 1
            For Each drow As DataRow In gammatable.Rows
                objProfitLossChart.chtgamma.Row = r
                objProfitLossChart.chtgamma.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub

    Private Sub create_chart_vega()
        chtvega.ColumnCount = 0
        chtvega.ColumnLabelCount = 0
        chtvega.ColumnLabelIndex = 0
        chtvega.AutoIncrement = False
        chtvega.RowCount = 0
        chtvega.RowLabelCount = 0
        chtvega.RowLabelIndex = 0
        chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If vegatable.Columns.Count > 2 Then

            chtvega.ColumnCount = vegatable.Columns.Count - 3
            chtvega.ColumnLabelCount = 1
            chtvega.ColumnLabelIndex = 1
            chtvega.AutoIncrement = False
            chtvega.RowCount = vegatable.Rows.Count - 1
            chtvega.RowLabelCount = 1
            chtvega.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(vegaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(vegaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(vegatable.Rows.Count - 1, vegatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In vegatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In vegatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            chtvega.ChartData = cha
            If div > 1 Then
                lblvega.Text = "Rs.in ( " & str & ")"
            Else
                lblvega.Text = "-"
            End If
            chtvega.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            For Each col As DataColumn In vegatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chtvega.Column = c
                    chtvega.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next


            r = 1
            For Each drow As DataRow In vegatable.Rows
                chtvega.Row = r
                chtvega.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_vega_Full()
        objProfitLossChart.chtvega.ColumnCount = 0
        objProfitLossChart.chtvega.ColumnLabelCount = 0
        objProfitLossChart.chtvega.ColumnLabelIndex = 0
        objProfitLossChart.chtvega.AutoIncrement = False
        objProfitLossChart.chtvega.RowCount = 0
        objProfitLossChart.chtvega.RowLabelCount = 0
        objProfitLossChart.chtvega.RowLabelIndex = 0
        objProfitLossChart.chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If vegatable.Columns.Count > 2 Then

            objProfitLossChart.chtvega.ColumnCount = vegatable.Columns.Count - 3
            objProfitLossChart.chtvega.ColumnLabelCount = 1
            objProfitLossChart.chtvega.ColumnLabelIndex = 1
            objProfitLossChart.chtvega.AutoIncrement = False
            objProfitLossChart.chtvega.RowCount = vegatable.Rows.Count - 1
            objProfitLossChart.chtvega.RowLabelCount = 1
            objProfitLossChart.chtvega.RowLabelIndex = 1
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(vegaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(vegaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            objProfitLossChart.chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim cha(vegatable.Rows.Count - 1, vegatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In vegatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In vegatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            objProfitLossChart.chtvega.ChartData = cha
            If div > 1 Then
                lblvega.Text = "Rs.in ( " & str & ")"
            Else
                lblvega.Text = "-"
            End If
            objProfitLossChart.chtvega.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            For Each col As DataColumn In vegatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chtvega.Column = c
                    objProfitLossChart.chtvega.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1
                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next


            r = 1
            For Each drow As DataRow In vegatable.Rows
                objProfitLossChart.chtvega.Row = r
                objProfitLossChart.chtvega.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_theta()
        chttheta.ColumnCount = 0
        chttheta.ColumnLabelCount = 0
        chttheta.ColumnLabelIndex = 0
        chttheta.AutoIncrement = False
        chttheta.RowCount = 0
        chttheta.RowLabelCount = 0
        chttheta.RowLabelIndex = 0
        chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If thetatable.Columns.Count > 2 Then
            chttheta.ColumnCount = thetatable.Columns.Count - 3
            chttheta.ColumnLabelCount = 1
            chttheta.ColumnLabelIndex = 1
            chttheta.AutoIncrement = False
            chttheta.RowCount = thetatable.Rows.Count - 1
            chttheta.RowLabelCount = 1
            chttheta.RowLabelIndex = 1
            chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(thetaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(thetaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(thetatable.Rows.Count - 1, thetatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In thetatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In thetatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lbltheta.Text = "Rs. in ( " & str & ")"
            Else
                lbltheta.Text = "-"
            End If
            chttheta.ChartData = cha

            chttheta.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In thetatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chttheta.Column = c
                    chttheta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In thetatable.Rows
                chttheta.Row = r
                chttheta.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_theta_Full()
        objProfitLossChart.chttheta.ColumnCount = 0
        objProfitLossChart.chttheta.ColumnLabelCount = 0
        objProfitLossChart.chttheta.ColumnLabelIndex = 0
        objProfitLossChart.chttheta.AutoIncrement = False
        objProfitLossChart.chttheta.RowCount = 0
        objProfitLossChart.chttheta.RowLabelCount = 0
        objProfitLossChart.chttheta.RowLabelIndex = 0
        objProfitLossChart.chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If thetatable.Columns.Count > 2 Then
            objProfitLossChart.chttheta.ColumnCount = thetatable.Columns.Count - 3
            objProfitLossChart.chttheta.ColumnLabelCount = 1
            objProfitLossChart.chttheta.ColumnLabelIndex = 1
            objProfitLossChart.chttheta.AutoIncrement = False
            objProfitLossChart.chttheta.RowCount = thetatable.Rows.Count - 1
            objProfitLossChart.chttheta.RowLabelCount = 1
            objProfitLossChart.chttheta.RowLabelIndex = 1
            objProfitLossChart.chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(thetaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(thetaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(thetatable.Rows.Count - 1, thetatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In thetatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In thetatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lbltheta.Text = "Rs. in ( " & str & ")"
            Else
                lbltheta.Text = "-"
            End If
            objProfitLossChart.chttheta.ChartData = cha

            objProfitLossChart.chttheta.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In thetatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chttheta.Column = c
                    objProfitLossChart.chttheta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In thetatable.Rows
                objProfitLossChart.chttheta.Row = r
                objProfitLossChart.chttheta.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    '------------
    Private Sub create_chart_volga()
        chtvolga.ColumnCount = 0
        chtvolga.ColumnLabelCount = 0
        chtvolga.ColumnLabelIndex = 0
        chtvolga.AutoIncrement = False
        chtvolga.RowCount = 0
        chtvolga.RowLabelCount = 0
        chtvolga.RowLabelIndex = 0
        chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If volgatable.Columns.Count > 2 Then
            chtvolga.ColumnCount = volgatable.Columns.Count - 3
            chtvolga.ColumnLabelCount = 1
            chtvolga.ColumnLabelIndex = 1
            chtvolga.AutoIncrement = False
            chtvolga.RowCount = volgatable.Rows.Count - 1
            chtvolga.RowLabelCount = 1
            chtvolga.RowLabelIndex = 1
            chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(volgaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(volgaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(volgatable.Rows.Count - 1, volgatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In volgatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In volgatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblvolga.Text = "Rs. in ( " & str & ")"
            Else
                lblvolga.Text = "-"
            End If
            chtvolga.ChartData = cha

            chtvolga.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In volgatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chtvolga.Column = c
                    chtvolga.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In volgatable.Rows
                chtvolga.Row = r
                chtvolga.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_volga_Full()
        objProfitLossChart.chtvolga.ColumnCount = 0
        objProfitLossChart.chtvolga.ColumnLabelCount = 0
        objProfitLossChart.chtvolga.ColumnLabelIndex = 0
        objProfitLossChart.chtvolga.AutoIncrement = False
        objProfitLossChart.chtvolga.RowCount = 0
        objProfitLossChart.chtvolga.RowLabelCount = 0
        objProfitLossChart.chtvolga.RowLabelIndex = 0
        objProfitLossChart.chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If volgatable.Columns.Count > 2 Then
            objProfitLossChart.chtvolga.ColumnCount = volgatable.Columns.Count - 3
            objProfitLossChart.chtvolga.ColumnLabelCount = 1
            objProfitLossChart.chtvolga.ColumnLabelIndex = 1
            objProfitLossChart.chtvolga.AutoIncrement = False
            objProfitLossChart.chtvolga.RowCount = volgatable.Rows.Count - 1
            objProfitLossChart.chtvolga.RowLabelCount = 1
            objProfitLossChart.chtvolga.RowLabelIndex = 1
            objProfitLossChart.chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(volgaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(volgaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(volgatable.Rows.Count - 1, volgatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In volgatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In volgatable.Rows
                        cha(r, c) = drow(col.ColumnName) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblvolga.Text = "Rs. in ( " & str & ")"
            Else
                lblvolga.Text = "-"
            End If
            objProfitLossChart.chtvolga.ChartData = cha

            objProfitLossChart.chtvolga.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In volgatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chtvolga.Column = c
                    objProfitLossChart.chtvolga.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In volgatable.Rows
                objProfitLossChart.chtvolga.Row = r
                objProfitLossChart.chtvolga.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    '-------------
    Private Sub create_chart_vanna()
        chtvanna.ColumnCount = 0
        chtvanna.ColumnLabelCount = 0
        chtvanna.ColumnLabelIndex = 0
        chtvanna.AutoIncrement = False
        chtvanna.RowCount = 0
        chtvanna.RowLabelCount = 0
        chtvanna.RowLabelIndex = 0
        chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If vannatable.Columns.Count > 2 Then
            chtvanna.ColumnCount = vannatable.Columns.Count - 3
            chtvanna.ColumnLabelCount = 1
            chtvanna.ColumnLabelIndex = 1
            chtvanna.AutoIncrement = False
            chtvanna.RowCount = vannatable.Rows.Count - 1
            chtvanna.RowLabelCount = 1
            chtvanna.RowLabelIndex = 1
            chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(vannaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(vannaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(vannatable.Rows.Count - 1, vannatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In vannatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In vannatable.Rows
                        cha(r, c) = Math.Round(Val(drow(col.ColumnName)), DecimalSetting.iRVannaval) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblvanna.Text = "Rs. in ( " & str & ")"
            Else
                lblvanna.Text = "-"
            End If
            chtvanna.ChartData = cha

            chtvanna.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In vannatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    chtvanna.Column = c
                    chtvanna.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In vannatable.Rows
                chtvanna.Row = r
                chtvanna.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
    Private Sub create_chart_vanna_Full()
        objProfitLossChart.chtvanna.ColumnCount = 0
        objProfitLossChart.chtvanna.ColumnLabelCount = 0
        objProfitLossChart.chtvanna.ColumnLabelIndex = 0
        objProfitLossChart.chtvanna.AutoIncrement = False
        objProfitLossChart.chtvanna.RowCount = 0
        objProfitLossChart.chtvanna.RowLabelCount = 0
        objProfitLossChart.chtvanna.RowLabelIndex = 0
        objProfitLossChart.chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
        If vannatable.Columns.Count > 2 Then
            objProfitLossChart.chtvanna.ColumnCount = vannatable.Columns.Count - 3
            objProfitLossChart.chtvanna.ColumnLabelCount = 1
            objProfitLossChart.chtvanna.ColumnLabelIndex = 1
            objProfitLossChart.chtvanna.AutoIncrement = False
            objProfitLossChart.chtvanna.RowCount = vannatable.Rows.Count - 1
            objProfitLossChart.chtvanna.RowLabelCount = 1
            objProfitLossChart.chtvanna.RowLabelIndex = 1
            objProfitLossChart.chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
            Dim div As Long = 1
            Dim str As String = ""
            'Dim YAxis As Axis
            Dim minval As Double = Val(MinValOfIntArray(vannaarr.ToArray))
            Dim maxval As Double = Val(MaxValOfIntArray(vannaarr.ToArray))

            If maxval >= 1000000000 Or Math.Abs(minval) >= 1000000000 Then
                div = 10000000
                str = "Crores"
            ElseIf maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
                div = 100000
                str = "Lacs"
            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
                div = 1000
                str = "Thousands"
            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
                div = 100
                str = "Hundreds"
            Else
                div = 1
            End If
            Dim cha(vannatable.Rows.Count - 1, vannatable.Columns.Count - 3) As Object
            Dim c As Integer
            c = 0
            Dim r As Integer
            r = 0
            For Each col As DataColumn In vannatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    r = 0
                    For Each drow As DataRow In vannatable.Rows
                        cha(r, c) = Math.Round(Val(drow(col.ColumnName)), DecimalSetting.iRVannaval) / div
                        r += 1
                    Next
                    c += 1
                End If
            Next
            If div > 1 Then
                lblvanna.Text = "Rs. in ( " & str & ")"
            Else
                lblvanna.Text = "-"
            End If
            objProfitLossChart.chtvanna.ChartData = cha

            objProfitLossChart.chtvanna.ShowLegend = False
            c = 1
            Dim i As Integer = 0
            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
            'End If
            For Each col As DataColumn In vannatable.Columns
                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

                    objProfitLossChart.chtvanna.Column = c
                    objProfitLossChart.chtvanna.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
                    i += 1

                    c += 1
                    'If c >= grdgamma.Columns.Count Then
                    '    Exit For
                End If
            Next
            r = 1
            For Each drow As DataRow In vannatable.Rows
                objProfitLossChart.chtvanna.Row = r
                objProfitLossChart.chtvanna.RowLabel = drow("spotvalue")
                r += 1
            Next
        End If
    End Sub
#End Region

#Region "Event"
    Private Function GetMinVal(ByVal dt As DataTable) As Double
        Dim dMinVal As Double = 100000000
        For Each grow As DataRow In dt.Rows
            For i As Integer = 2 To dt.Columns.Count - 1
                If dMinVal > Math.Round(Val(grow(i)), DecimalSetting.iRGammaval) Then
                    dMinVal = Math.Round(Val(grow(i)), DecimalSetting.iRGammaval)
                End If
            Next
        Next
        If dMinVal = 100000000 Then
            dMinVal = 0
        End If
        Return dMinVal
    End Function

    Private Function GetMaxVal(ByVal dt As DataTable) As Double
        Dim dMaxVal As Double = 0

        For Each grow As DataRow In dt.Rows
            For i As Integer = 2 To dt.Columns.Count - 1
                If dMaxVal < Math.Round(Val(grow(i)), DecimalSetting.iRGammaval) Then
                    dMaxVal = Math.Round(Val(grow(i)), DecimalSetting.iRGammaval)
                End If
            Next
        Next

        If dMaxVal = 0 Then
            dMaxVal = 1
        End If
        Return dMaxVal
    End Function

    Private Sub contMenuOpen(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        'Dim ind As Integer
        'ind = CType(sender, ContextMenuStrip).Items(0).Tag
        If cellno = -1 Then Exit Sub
        If grdtrad.Rows(cellno).Cells("lv").ReadOnly = True Then
            CType(sender, ContextMenuStrip).Items(0).Text = "Unfreeze"
        Else
            CType(sender, ContextMenuStrip).Items(0).Text = "Freeze"
        End If
    End Sub

    Private Sub contMenuOpenExpiry(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        'Dim ind As Integer
        'ind = CType(sender, ContextMenuStrip).Items(0).Tag
        If cellno = -1 Then Exit Sub
        If grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = True Then
            CType(sender, ContextMenuStrip).Items(0).Text = "Unfreeze"
        Else
            CType(sender, ContextMenuStrip).Items(0).Text = "Freeze"
        End If
    End Sub

    Private Sub freezVol(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cellno = -1 Then Exit Sub
        If CType(sender, ToolStripMenuItem).Text = "Unfreeze" Then
            grdtrad.Rows(cellno).Cells("lv").ReadOnly = False
            CType(sender, ToolStripMenuItem).Text = "Freeze"
        Else
            grdtrad.Rows(cellno).Cells("lv").ReadOnly = True
            CType(sender, ToolStripMenuItem).Text = "Unfreeze"
        End If
    End Sub

    Private Sub freezExpiry(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cellno = -1 Then Exit Sub
        If CType(sender, ToolStripMenuItem).Text = "Unfreeze" Then
            grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = False
            CType(sender, ToolStripMenuItem).Text = "Freeze"
        Else
            grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = True
            CType(sender, ToolStripMenuItem).Text = "Unfreeze"
        End If
    End Sub

    Private Sub init_result()
        rtable = New DataTable
        With rtable.Columns

            '.Add("timeI", GetType(Date))
            '.Add("timeII", GetType(Date))
            '.Add("cpf")
            '.Add("spval", GetType(Double))
            '.Add("strike", GetType(Double))
            '.Add("units", GetType(Double))
            '.Add("last", GetType(Double))
            '.Add("lv", GetType(Double))
            '.Add("delta", GetType(Double))
            '.Add("deltaval", GetType(Double))
            '.Add("theta", GetType(Double))
            '.Add("thetaval", GetType(Double))
            '.Add("vega", GetType(Double))
            '.Add("vgval", GetType(Double))
            '.Add("gamma", GetType(Double))
            '.Add("gmval", GetType(Double))

            .Add("timeI")
            .Add("timeII")
            .Add("cpf")
            .Add("spval", GetType(Double))
            .Add("strike", GetType(Double))
            .Add("units", GetType(Double))
            .Add("last", GetType(Double))
            'By Viral
            .Add("Price", GetType(Double))

            .Add("lv", GetType(Double))
            .Add("delta", GetType(Double))
            .Add("deltaval", GetType(Double))
            .Add("theta", GetType(Double))
            .Add("thetaval", GetType(Double))
            .Add("vega", GetType(Double))
            .Add("vgval", GetType(Double))
            .Add("gamma", GetType(Double))
            .Add("gmval", GetType(Double))
            .Add("volga", GetType(Double))
            .Add("volgaval", GetType(Double))
            .Add("vanna", GetType(Double))
            .Add("vannaval", GetType(Double))
            .Add("pl", GetType(Double))


        End With
    End Sub
    Public Sub fill_result(ByVal ind As Integer, ByVal rind As Integer, ByVal cind As Integer)
        REM Grid  Double Click Show Volga and Vanna 
        If rind = -1 Then Exit Sub
        init_result()
        Dim iscall As Boolean = False
        Dim drow As DataRow
        Dim _mT As Double = 0
        Dim mD1 As Double = 0
        Dim mD2 As Double = 0
        Dim mVolatility As Double = 0


        Select Case ind
            Case 0

                'If CDate(profit.Columns(cind).ColumnName) < CDate(dtexp.Text) Then
                '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
                '    'Else
                '    '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
                'End If

                'mt = DateDiff(DateInterval.Day, CDate(drow("mdate")), Now())

                For Each grow As DataGridViewRow In grdtrad.Rows
                    If grow.Cells("Active").Value = False Then Continue For

                    If (IsDate(profit.Columns(cind).ColumnName)) Then
                        If DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName).Date, CDate(Format(CDate(grow.Cells("TimeII").Value), "MMM/dd/yyyy")).Date) < 0 Then Continue For
                    Else
                        If DateDiff(DateInterval.Day, CDate(Mid(profit.Columns(cind).ColumnName, 8, Len(profit.Columns(cind).ColumnName))).Date, CDate(Format(CDate(grow.Cells("TimeII").Value), "MMM/dd/yyyy")).Date) < 0 Then Continue For
                    End If


                    If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                        Dim SpotValue As Double = Val(profit.Rows(rind)(0)) + CDbl(grow.Cells("DifFactor").Value)
                        drow = rtable.NewRow

                        


                        If (IsDate(profit.Columns(cind).ColumnName)) Then
                            drow("timeI") = Format(CDate(profit.Columns(cind).ColumnName), "MMM/dd/yyyy")

                        ElseIf (cind) = profit.Columns.Count - 1 Then
                            drow("timeI") = Format(CDate(Mid(profit.Columns(cind).ColumnName, 7, Len(profit.Columns(cind).ColumnName) - 1)), "MMM/dd/yyyy")
                        Else
                            Exit Sub
                            'drow("timeI") = Format(CDate(Mid(profit.Columns(cind).ColumnName, 1, Len(profit.Columns(cind).ColumnName) - 4)), "MMM/dd/yyyy")
                        End If

                        'drow("timeII") = Format(CDate(dtexp.Text), "MMM/dd/yyyy")
                        'drow("timeI") = Format(CDate(grow.Cells(1).Value), "MMM/dd/yyyy")
                        drow("timeII") = Format(CDate(grow.Cells("TimeII").Value), "MMM/dd/yyyy")
                        drow("cpf") = grow.Cells("CPF").Value
                        drow("spval") = SpotValue
                        drow("strike") = Val(grow.Cells("Strike").Value)
                        drow("units") = Val(grow.Cells("units").Value)
                        drow("last") = Val(grow.Cells("ltp").Value)            ' 9 is used
                        drow("lv") = Val(grow.Cells("lv").Value)
                        If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                            drow("delta") = Val(grow.Cells("delta").Value)
                            drow("deltaval") = Val(grow.Cells("deltaval").Value)
                            drow("theta") = Val(0)
                            drow("thetaval") = Val(0)
                            drow("vega") = Val(0)
                            drow("vgval") = Val(0)
                            drow("gamma") = Val(0)
                            drow("gmval") = Val(0)
                            drow("volga") = Val(0)
                            drow("volgaval") = Val(0)
                            drow("vanna") = Val(0)
                            drow("vannaval") = Val(0)
                            drow("Price") = ((SpotValue - Val(grow.Cells("spval").Value)) + Val(grow.Cells("ltp").Value))
                            drow("pl") = Math.Round(Val(((SpotValue - Val(grow.Cells("ltp").Value)) * Val(grow.Cells("units").Value))), 2)


                        Else
                            _mT = 0
                            If CDate(drow("timeI")) < CDate(drow("timeII")) Then
                                If (IsDate(profit.Columns(cind).ColumnName)) Then
                                    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName).Date, CDate(drow("timeII")).Date)
                                Else
                                    _mT = DateDiff(DateInterval.Day, CDate(Mid(profit.Columns(cind).ColumnName, 1, Len(profit.Columns(cind).ColumnName) - 4)).Date, CDate(drow("timeII")).Date)
                                End If
                            Else
                                '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
                                If (IsDate(profit.Columns(cind).ColumnName)) Then
                                    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName).Date, CDate(drow("timeII")).Date)
                                    'by Viral 12Oct2011
                                    If _mT = 0 Then
                                        _mT = 0.5
                                    End If
                                Else
                                    '_mT = DateDiff(DateInterval.Day, CDate(Mid(profit.Columns(cind).ColumnName, 1, Len(profit.Columns(cind).ColumnName) - 4)).Date, CDate(drow("timeII")).Date)
                                End If
                            End If

                            If grow.Cells("CPF").Value = "C" Then
                                iscall = True
                            Else
                                iscall = False
                            End If

                            If _mT = 0 Then
                                _mT = 0.0001
                            Else
                                _mT = (_mT) / 365
                            End If
                            Dim bval As Double
                            bval = 0
                            If _mT = 0.0001 Then

                                drow("delta") = 0
                                drow("deltaval") = 0
                                drow("theta") = 0
                                drow("thetaval") = 0
                                drow("vega") = 0
                                drow("vgval") = 0
                                drow("gamma") = 0
                                drow("gmval") = 0

                                drow("volga") = 0
                                drow("vanna") = 0

                                drow("volgaval") = 0
                                drow("vannaval") = 0

                                If drow("cpf") = "C" Then
                                    If (Val(drow("spval")) - Val(drow("strike"))) < 0 Then
                                        drow("Price") = 0
                                    Else
                                        drow("Price") = Val(drow("spval")) - Val(drow("strike"))
                                    End If
                                ElseIf drow("cpf") = "P" Then
                                    If (Val(drow("strike")) - Val(drow("spval"))) < 0 Then
                                        drow("Price") = 0
                                    Else
                                        drow("Price") = Val(drow("strike")) - Val(drow("spval"))
                                    End If
                                ElseIf drow("cpf") = "F" Then
                                    drow("Price") = Val(drow("spval"))
                                End If
                                drow("pl") = Math.Round(CDbl(((drow("Price") - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))), 2)  ' 9 is used
                                'bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 0)
                                'drow("pl") = Math.Round(CDbl(((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))), 2)  ' 9 is used


                                'drow("Price") = (bval - CDbl(grow.Cells("ltp").Value))
                            Else
                                bval = Greeks.Black_Scholes(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 1)
                                drow("delta") = Math.Round(CDbl(Val(bval & "")), DecimalSetting.iRDelta)
                                drow("deltaval") = Math.Round(CDbl(CDbl(Val(bval & "")) * CDbl(grow.Cells("units").Value)), DecimalSetting.iRDeltaval)
                                bval = Greeks.Black_Scholes(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 4)
                                drow("theta") = Math.Round(CDbl(Val(bval & "")), DecimalSetting.iRTheta)
                                drow("thetaval") = Math.Round(CDbl(CDbl(Val(bval & "")) * CDbl(grow.Cells("units").Value)), DecimalSetting.iRThetaval)
                                bval = Greeks.Black_Scholes(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 3)
                                drow("vega") = Math.Round(CDbl(Val(bval & "")), DecimalSetting.iRVega)
                                drow("vgval") = Math.Round(CDbl(CDbl(Val(bval & "")) * CDbl(grow.Cells("units").Value)), DecimalSetting.iRVegaval)
                                bval = Greeks.Black_Scholes(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 2)
                                drow("gamma") = Math.Round(CDbl(Val(bval & "")), DecimalSetting.iRGamma)
                                drow("gmval") = Math.Round(CDbl(CDbl(Val(bval & "")) * CDbl(grow.Cells("units").Value)), DecimalSetting.iRGammaval)

                                'Volga , Vanna
                                mVolatility = Greeks.Black_Scholes(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 6)

                                'mD1 = mD1 + CalD1(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, mVolatility, _mT)
                                'mD2 = mD2 + CalD2(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, mVolatility, _mT)

                                'drow("volga") = CalVolga(drow("vega"), mD1, mD2, mVolatility)
                                'drow("vanna") = CalVanna(SpotValue, drow("vega"), mD1, mD2, mVolatility, _mT)
                                bval = Greeks.Black_Scholes(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 7)
                                drow("vanna") = Math.Round(CDbl(Val(bval & "")), DecimalSetting.iRVanna)
                                drow("vannaval") = Math.Round(CDbl(CDbl(Val(bval & "")) * CDbl(grow.Cells("units").Value)), DecimalSetting.iRVannaval)

                                bval = Greeks.Black_Scholes(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 8)
                                drow("volga") = Math.Round(CDbl(Val(bval & "")), DecimalSetting.iRVolga)
                                drow("volgaval") = Math.Round(CDbl(CDbl(Val(bval & "")) * CDbl(grow.Cells("units").Value)), DecimalSetting.iRVolgaval)

                                'drow("volgaval") = Math.Round(CDbl(drow("volga") * CDbl(grow.Cells("units").Value)), 5)
                                'drow("vannaval") = Math.Round(CDbl(drow("vanna") * CDbl(grow.Cells("units").Value)), 5)


                                'bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 2)
                                'drow("gamma") = Math.Round(CDbl(bval), 5)
                                'drow("gmval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 5)


                                bval = Greeks.Black_Scholes(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 0)

                                drow("pl") = Math.Round(CDbl(((Val(bval & "") - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))), 2)  ' 9 is used
                                drow("Price") = bval

                                'drow("Price") = (bval - CDbl(grow.Cells("ltp").Value))



                            End If
                        End If
                        rtable.Rows.Add(drow)
                    End If
                Next
                drow = rtable.NewRow
                drow("timeI") = ""
                drow("timeII") = ""
                drow("cpf") = "Total"
                drow("spval") = 0
                drow("strike") = 0
                drow("units") = Val(rtable.Compute("sum(units)", "") & "")
                drow("last") = 0
                drow("Price") = 0
                drow("lv") = 0
                drow("delta") = 0
                drow("deltaval") = Val(rtable.Compute("sum(deltaval)", "") & "")
                drow("theta") = Val(0)
                drow("thetaval") = Val(rtable.Compute("sum(thetaval)", "") & "")
                drow("vega") = Val(0)
                drow("vgval") = Val(rtable.Compute("sum(vgval)", "") & "")
                drow("gamma") = Val(0)
                drow("gmval") = Val(rtable.Compute("sum(gmval)", "") & "")
                drow("volga") = Val(0)
                drow("volgaval") = Val(rtable.Compute("sum(volgaval)", "") & "")
                drow("vanna") = Val(0)
                drow("vannaval") = Val(rtable.Compute("sum(vannaval)", "") & "")
                drow("pl") = Math.Round(Val(rtable.Compute("sum(pl)", "") & "") + grossmtm)
                rtable.Rows.Add(drow)
        End Select
    End Sub

#End Region

#Region "Init"

    Private Function DateFormat(ByVal cdt As String) As String
        If IsDate(cdt) Then
            Return Format(CDate(cdt), "dd-MMM-yy")
        Else
            Return "Expiry"
        End If
    End Function

    Private Sub init_table()

        If Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then
            profit = New DataTable
            'grdprofit.Columns.Clear()
            deltatable = New DataTable
            gammatable = New DataTable
            vegatable = New DataTable
            thetatable = New DataTable
            volgatable = New DataTable
            vannatable = New DataTable
            Dim i As Integer = 0
            If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
                i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
            End If
            i += 1
            Dim j As Integer
            j = 0
            Dim start As Double
            Dim endd As Double
            start = Val(txtmid) - (Val(txtllimit.Text) * Val(interval))
            endd = Val(txtmid) + (Val(txtllimit.Text) * Val(interval))
            Dim drow As DataRow

            '######################################################################################
            grdprofit.DataSource = Nothing
            'grdprofit.Refresh()
            'grdprofit.Rows.Clear()
            If grdprofit.Columns.Count > 0 Then
                grdprofit.Columns.Clear()
            End If
            Dim style1 As New DataGridViewCellStyle
            style1.Format = "N2"
            Dim acol As DataGridViewTextBoxColumn

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdprofit.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdprofit.Columns.Add(acol)


            Dim grow As DataGridViewRow
            For Each grow In grdact.Rows

                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            acol = New DataGridViewTextBoxColumn
                            acol.HeaderText = DateFormat(gcol.DataPropertyName) 'gcol.HeaderText
                            acol.Name = acol.HeaderText
                            acol.DataPropertyName = gcol.DataPropertyName
                            acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                            'acol.DefaultCellStyle.Format = RoundGrossMTM

                            grdprofit.Columns.Add(acol)
                            'acol.DefaultCellStyle.Format = "N2"


                        End If
                    End If
                Next
            Next


            With profit.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                .Add(c)
                            End If
                        End If
                    Next
                Next
            End With
            'FOR MINUS LIMIT(STRI) FROM MID
            Dim inter As Double = 0
            Dim int As Double = 0
            For j = 1 To Val(txtllimit.Text) + 1
                drow = profit.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                If drow("spotvalue") < 0 Then Continue For
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100) '& " %"
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)

                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                drow(gcol.DataPropertyName) = 0
                            End If
                        End If
                    Next
                Next
                'If drow("spotvalue") > 0 Then
                profit.Rows.Add(drow)
                'End If
            Next
            'FOR PLUS LIMIT(STRI) FROM MID
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = profit.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100 '& " %"
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                drow(gcol.DataPropertyName) = 0
                            End If
                        End If
                    Next
                Next
                profit.Rows.Add(drow)
            Next


            '######################################################################################

            grddelta.DataSource = Nothing
            'grddelta.Refresh()
            'grdprofit.Rows.Clear()
            If grddelta.Columns.Count > 0 Then
                grddelta.Columns.Clear()
            End If

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grddelta.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grddelta.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = DateFormat(gcol.DataPropertyName) 'gcol.HeaderText
                                acol.Name = acol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                grddelta.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next


            '''''''''''''''''' Detla Table Initalise

            With deltatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = deltatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                    'drow("Percent(%)") = Math.Round(((val(drow("SPOTVALUE")) / val(txtmid)) - 1) * 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                deltatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
            'FOR PLUS LIMIT(STRI) FROM MID
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = deltatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                deltatable.Rows.Add(drow)
            Next
            '######################################################################################

            '############# Gamma Table Init
            grdgamma.DataSource = Nothing
            'grdgamma.Refresh()
            'grdprofit.Rows.Clear()
            If grdgamma.Columns.Count > 0 Then
                grdgamma.Columns.Clear()
            End If

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdgamma.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdgamma.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = DateFormat(gcol.DataPropertyName) 'gcol.HeaderText
                                acol.Name = acol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                grdgamma.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next



            With gammatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            'For j = 1 To val(txtllimit.Text)
            '    drow = gammatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = gammatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                gammatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = gammatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = gammatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                gammatable.Rows.Add(drow)
            Next


            ' Vega Table INit ######################################################################################
            grdvega.DataSource = Nothing
            ' grdvega.Refresh()
            'grdprofit.Rows.Clear()
            If grdvega.Columns.Count > 0 Then
                grdvega.Columns.Clear()
            End If


            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdvega.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdvega.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = DateFormat(gcol.DataPropertyName) 'gcol.HeaderText
                                acol.Name = acol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                grdvega.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next


            With vegatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            'For j = 1 To val(txtllimit.Text)
            '    drow = vegatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = vegatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                vegatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = vegatable.NewRow
            'drow("spotvalue") = (val(interval) * j) + val(txtmid)
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = vegatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                vegatable.Rows.Add(drow)
            Next
            '######################################################################################
            grdtheta.DataSource = Nothing
            'grdtheta.Refresh()
            'grdprofit.Rows.Clear()
            If grdtheta.Columns.Count > 0 Then
                grdtheta.Columns.Clear()
            End If

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdtheta.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdtheta.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = DateFormat(gcol.DataPropertyName) 'gcol.HeaderText
                                acol.Name = acol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                grdtheta.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next


            With thetatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            'For j = 1 To val(txtllimit.Text)
            '    drow = thetatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = thetatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                thetatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = thetatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = thetatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0

                                End If
                            End If
                        End If
                    Next
                Next
                thetatable.Rows.Add(drow)
            Next

            '------------------------------------------------------------------------------------------------------
            'For Volga
            grdvolga.DataSource = Nothing
            'grdvolga.Refresh()
            'grdprofit.Rows.Clear()
            If grdvolga.Columns.Count > 0 Then
                grdvolga.Columns.Clear()
            End If

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdvolga.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdvolga.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = DateFormat(gcol.DataPropertyName) 'gcol.HeaderText
                                acol.Name = acol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                grdvolga.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next


            With volgatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            'For j = 1 To val(txtllimit.Text)
            '    drow = volgatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = volgatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                volgatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = volgatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = volgatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0

                                End If
                            End If
                        End If
                    Next
                Next
                volgatable.Rows.Add(drow)
            Next

            '------------------------------------------------------------------------------------------------------
            'For Vanna
            grdvanna.DataSource = Nothing
            'grdVanna.Refresh()
            'grdprofit.Rows.Clear()
            If grdvanna.Columns.Count > 0 Then
                grdvanna.Columns.Clear()
            End If

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "SpotValue"
            acol.DataPropertyName = "SpotValue"
            acol.Frozen = True
            grdvanna.Columns.Add(acol)

            acol = New DataGridViewTextBoxColumn
            acol.HeaderText = "Percent(%)"
            acol.DataPropertyName = "Percent(%)"
            acol.Frozen = True
            grdvanna.Columns.Add(acol)

            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            If (IsDate(gcol.DataPropertyName)) Then
                                acol = New DataGridViewTextBoxColumn
                                acol.HeaderText = DateFormat(gcol.DataPropertyName) 'gcol.HeaderText
                                acol.Name = acol.HeaderText
                                acol.DataPropertyName = gcol.DataPropertyName
                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                grdvanna.Columns.Add(acol)
                            End If
                        End If
                    End If
                Next
            Next


            With vannatable.Columns
                .Add("SpotValue", GetType(Double))
                .Add("Percent(%)", GetType(Double))
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                Dim c As String = gcol.DataPropertyName
                                If (IsDate(gcol.DataPropertyName)) Then
                                    .Add(c)
                                End If
                            End If
                        End If
                    Next
                Next
            End With
            'For j = 1 To val(txtllimit.Text)
            '    drow = Vannatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            inter = 0
            int = 0
            'For j = 1 To val(txtllimit.Text)
            '    drow = deltatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + start
            For j = 1 To Val(txtllimit.Text) + 1
                drow = vannatable.NewRow

                ' drow("spotvalue") = (val(interval) * j) + start
                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
                drow("Percent(%)") = 0
                If chkint.Checked = True Then
                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
                    drow("Percent(%)") = -(inter * 100)
                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                int += 1
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0
                                End If
                            End If
                        End If
                    Next
                Next
                vannatable.Rows.Add(drow)
            Next
            'For j = 1 To val(txtllimit.Text)
            '    drow = Vannatable.NewRow
            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
            inter = 0
            For j = 1 To Val(txtllimit.Text)
                drow = vannatable.NewRow
                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
                drow("Percent(%)") = 0

                If chkint.Checked = True Then
                    inter = (Val(txtinterval.Text) * Val(j)) / 100
                    drow("Percent(%)") = inter * 100
                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
                Else
                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
                End If
                For Each grow In grdact.Rows
                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                            If grow.Cells(gcol.Index).Value = True Then
                                If (IsDate(gcol.DataPropertyName)) Then
                                    drow(gcol.DataPropertyName) = 0

                                End If
                            End If
                        End If
                    Next
                Next
                vannatable.Rows.Add(drow)
            Next

        Else
            MsgBox("enter Value.")
        End If
    End Sub
    Private Sub init_Statictable()
        static_Grid = New DataTable

        Dim i As Integer = 0
        If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
            i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        i += 1
        Dim j As Integer
        j = 0


        grdStaticCalc.DataSource = Nothing

        If grdStaticCalc.Columns.Count > 0 Then
            grdStaticCalc.Columns.Clear()
        End If
        Dim style1 As New DataGridViewCellStyle
        style1.Format = "N2"
        Dim acol As DataGridViewTextBoxColumn

        acol = New DataGridViewTextBoxColumn
        acol.HeaderText = "SpotValue"
        acol.Name = "SpotValue"
        acol.DataPropertyName = "SpotValue"
        acol.Frozen = True
        acol.ReadOnly = False
        grdStaticCalc.Columns.Add(acol)

        acol = New DataGridViewTextBoxColumn
        acol.HeaderText = "Percent(%)"
        acol.Name = "Percent"
        acol.DataPropertyName = "Percent(%)"
        acol.Frozen = True
        acol.ReadOnly = False
        grdStaticCalc.Columns.Add(acol)


        Dim grow As DataGridViewRow
        For Each grow In grdact.Rows
            For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                    If grow.Cells(gcol.Index).Value = True Then
                        acol = New DataGridViewTextBoxColumn
                        acol.HeaderText = DateFormat(gcol.DataPropertyName) 'gcol.HeaderText
                        acol.Name = acol.HeaderText
                        acol.DataPropertyName = gcol.DataPropertyName
                        acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                        'acol.DefaultCellStyle.Format = RoundGrossMTM
                        acol.ReadOnly = True
                        grdStaticCalc.Columns.Add(acol)
                        'acol.DefaultCellStyle.Format = "N2"
                    End If
                End If
            Next
        Next

        With static_Grid.Columns
            .Add("SpotValue", GetType(Double))
            .Add("Percent(%)", GetType(Double))
            For Each grow In grdact.Rows
                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
                        If grow.Cells(gcol.Index).Value = True Then
                            Dim c As String = gcol.DataPropertyName
                            .Add(c)
                        End If
                    End If
                Next
            Next
        End With
        Dim drow As DataRow
        drow = static_Grid.NewRow
        drow("spotvalue") = txtmid
        drow("Percent(%)") = 0 '& " %"
        static_Grid.Rows.Add(drow)

        grdStaticCalc.DataSource = static_Grid
        grdStaticCalc.ColumnHeadersVisible = False
        'grdStaticCalc.Rows.Add(drow)
    End Sub

#End Region

#Region "Grid Event"
    Private Sub cal(ByVal cno As Integer, ByVal currow As DataGridViewRow, Optional ByVal check As Boolean = False)
        Dim iscall As Boolean = False
        Dim zero As Double = 0
        Dim one As Double = 1
        Dim count As Integer
        count = 0
        Dim futval As Double
        Dim brate As Double = 0
        Dim srate As Double = 0
        futval = 0
        Dim sumunit As Double = 0
        Dim mt As Double = 0
        Dim mmt As Double = 0
        Dim grow As DataGridViewRow
        If check = True Then
            grow = currow
        Else
            grow = grdtrad.CurrentRow
        End If
        If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
            grow.Cells("CPF").Value = CStr("C")
        Else
            grow.Cells("CPF").Value = UCase(grow.Cells("CPF").Value)
            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                grow.Cells("delta").Value = one
            Else
                grow.Cells("delta").Value = zero
            End If
        End If

        grdtrad.EndEdit()
        'If IsDBNull(grow.Cells(2).Value) Or Not IsDate(grow.Cells(2).Value) Then
        '    MsgBox("Enter Maturity date")
        '    Exit Sub
        'End If
        If Not IsDBNull(grow.Cells("CPF").Value) And Not grow.Cells("CPF").Value Is Nothing Then
            If UCase(grow.Cells("CPF").Value) = "F" Or UCase(grow.Cells("CPF").Value) = "E" Then
                grow.Cells("deltaval").Value = grow.Cells("units").Value
                grow.Cells("delta").Value = Math.Round(one, 2)
                grow.Cells("deltaval").Value = Math.Round(Val(grow.Cells("deltaval").Value), 2)
                grow.Cells("Strike").Value = Math.Round(zero, 2)
                If Val(grow.Cells("last").Value) = 0 Then
                    If Val(grow.Cells("spval").Value) <> 0 Then
                        grow.Cells("last").Value = Math.Round(CDbl(grow.Cells("spval").Value), 2)
                    Else
                        grow.Cells("last").Value = Math.Round(zero, 2)
                    End If
                End If
                grow.Cells("lv").Value = Math.Round(zero, 2)
                grow.Cells("gamma").Value = Math.Round(zero, 2)
                grow.Cells("gmval").Value = Math.Round(zero, 2)
                grow.Cells("vega").Value = Math.Round(zero, 2)
                grow.Cells("vgval").Value = Math.Round(zero, 2)
                grow.Cells("theta").Value = Math.Round(zero, 2)
                grow.Cells("thetaval").Value = Math.Round(zero, 2)


                grow.Cells("gamma1").Value = Math.Round(zero, 2)
                grow.Cells("gmval1").Value = Math.Round(zero, 2)
                grow.Cells("vega1").Value = Math.Round(zero, 2)
                grow.Cells("vgval1").Value = Math.Round(zero, 2)
                grow.Cells("theta").Value = Math.Round(zero, 2)
                grow.Cells("thetaval1").Value = Math.Round(zero, 2)

                grow.Cells("volga").Value = Math.Round(zero, 2)
                grow.Cells("volgaval1").Value = Math.Round(zero, 2)
                grow.Cells("vanna").Value = Math.Round(zero, 2)
                grow.Cells("vannaval1").Value = Math.Round(zero, 2)

                grdtrad.EndEdit()
            Else
                futval = Val(grow.Cells("spval").Value)
                If IsDBNull(grow.Cells("TimeI").Value) Then
                    grow.Cells("TimeI").Value = dttoday.Value.Date
                    'ElseIf grow.Cells("TimeI").Value.ToString.Trim = "" Then
                    '    grow.Cells("TimeI").Value = dttoday.Value.Date
                ElseIf grow.Cells("TimeI").Value = Nothing Then
                    grow.Cells("TimeI").Value = dttoday.Value.Date
                End If
                If IsDBNull(grow.Cells("TimeII").Value) Then
                    grow.Cells("TimeII").Value = dtexp.Value.Date
                    ' ElseIf grow.Cells("TimeII").Value.ToString.Trim = "" Then
                ElseIf grow.Cells("TimeII").Value = Nothing Then
                    grow.Cells("TimeII").Value = dtexp.Value.Date
                End If
                If CDate(grow.Cells("TimeI").Value) < CDate(grow.Cells("TimeII").Value) Then
                    mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
                    'mmt = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, CInt(txtdays.Text), Now.Date), CDate(grow.Cells("TimeII").Value).Date)
                    'Else
                    '    mt = DateDiff(DateInterval.Day, CDate(grow.Cells(1).Value), CDate(grow.Cells(2).Value))
                End If
                If CDate(grow.Cells("TimeI").Value).Date = CDate(grow.Cells("TimeII").Value).Date Then
                    mt = 0.5
                End If

                'mt = DateDiff(DateInterval.Day, CDate(drow("mdate")), Now())
                If grow.Cells("CPF").Value = "C" Then
                    iscall = True
                Else
                    iscall = False
                End If
                If futval <> 0 Then
                    If cno = 9 Then
                        CalDatastkprice(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, iscall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100), 0)
                        '' CalData(futval, Val(grow.Cells(5).Value), Val(grow.Cells(7).Value), mt, iscall, True, grow, Val(grow.Cells(6).Value), (Val(grow.Cells(8).Value) / 100))
                    Else
                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, iscall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100), 0)
                        isVolCal = True
                    End If
                End If
            End If
        End If
        txtunits.Text = 0
        txtdelval.Text = 0
        txtthval.Text = 0
        txtvgval.Text = 0
        txtgmval.Text = 0
        txtVolgaVal.Text = 0
        TxtVannaVal.Text = 0

        ''divyesh
        txtunits1.Text = 0
        txtdelval1.Text = 0
        txtthval1.Text = 0
        txtvgval1.Text = 0
        txtgmval1.Text = 0
        TxtVolgaVal1.Text = 0
        TxtVannaVal1.Text = 0

        If grdtrad.Rows.Count > 1 Then
            For Each row As DataGridViewRow In grdtrad.Rows
                If row.Cells("Active").Value = True Then
                    txtdelval.Text = Format(Val(txtdelval.Text) + Val(row.Cells("deltaval").Value), DecimalSetting.sDeltaval)
                    txtthval.Text = Format(Val(txtthval.Text) + Val(row.Cells("thetaval").Value), DecimalSetting.sThetaval)
                    txtvgval.Text = Format(Val(txtvgval.Text) + Val(row.Cells("vgval").Value), DecimalSetting.sVegaval)
                    txtgmval.Text = Format(Val(txtgmval.Text) + Val(row.Cells("gmval").Value), DecimalSetting.sGammaval)
                    txtVolgaVal.Text = Format(Val(txtVolgaVal.Text) + Val(row.Cells("volgaval").Value), DecimalSetting.sVolgaval)
                    TxtVannaVal.Text = Format(Val(TxtVannaVal.Text) + Val(row.Cells("vannaval").Value), DecimalSetting.sVannaval)

                    ''divyesh
                    txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(row.Cells("deltaval1").Value), DecimalSetting.sDeltaval)
                    txtthval1.Text = Format(Val(txtthval1.Text) + Val(row.Cells("thetaval1").Value), DecimalSetting.sThetaval)
                    txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(row.Cells("vgval1").Value), DecimalSetting.sVegaval)
                    txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(row.Cells("gmval1").Value), DecimalSetting.sGammaval)
                    TxtVolgaVal1.Text = Format(Val(TxtVolgaVal1.Text) + Val(row.Cells("volgaval1").Value), DecimalSetting.sVolgaval)
                    TxtVannaVal1.Text = Format(Val(TxtVannaVal1.Text) + Val(row.Cells("vannaval1").Value), DecimalSetting.sVannaval)
                End If
            Next

            txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
            ''divyesh
            txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)
        End If
    End Sub
    Private Sub grdtrad_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellEndEdit
        'If (e.ColumnIndex = 5 Or e.ColumnIndex = 6 Or e.ColumnIndex =9) Then
        '    cal()
        'Else
        If VarIsFirstLoad = True Then
            VarIsFirstLoad = False
            Exit Sub
        End If
        Dim zero As Double = 0
        Dim one As Double = 1

        If Not IsDate(grdtrad.Rows(e.RowIndex).Cells("TimeI").Value) Then grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
        If Not IsDate(grdtrad.Rows(e.RowIndex).Cells("TimeII").Value) Then grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dtexp.Value
        'grdtrad.Rows(e.RowIndex).Cells("lv").Value = Val(grdtrad.Rows(e.RowIndex).Cells("lv").Value).ToString("#0.00")
         grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = CDate(grdtrad.Rows(e.RowIndex).Cells("TimeII").Value)

        If grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E" Then
            grdtrad.Rows(e.RowIndex).Cells("Strike").ReadOnly = True
            grdtrad.Rows(e.RowIndex).Cells("lv").ReadOnly = True

            grdtrad.Rows(e.RowIndex).Cells("Strike").Value = 0
            grdtrad.Rows(e.RowIndex).Cells("lv").Value = 0


            grdtrad.Rows(e.RowIndex).Cells("gamma").Value = Math.Round(zero, 2)
            grdtrad.Rows(e.RowIndex).Cells("gmval").Value = Math.Round(zero, 2)
            grdtrad.Rows(e.RowIndex).Cells("vega").Value = Math.Round(zero, 2)
            grdtrad.Rows(e.RowIndex).Cells("vgval").Value = Math.Round(zero, 2)
            grdtrad.Rows(e.RowIndex).Cells("theta").Value = Math.Round(zero, 2)
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Value = Math.Round(zero, 2)
            grdtrad.Rows(e.RowIndex).Cells("volga").Value = Math.Round(zero, 2)
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Value = Math.Round(zero, 2)
            grdtrad.Rows(e.RowIndex).Cells("vanna").Value = Math.Round(zero, 2)
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Value = Math.Round(zero, 2)

            cal(0, grdtrad.CurrentRow)

        Else
            grdtrad.Rows(e.RowIndex).Cells("Strike").ReadOnly = False
            grdtrad.Rows(e.RowIndex).Cells("lv").ReadOnly = False
        End If

        'If Val(grdtrad.Rows(e.RowIndex).Cells("units").Value & "") = 0 Then
        '    grdtrad.Rows(e.RowIndex).Cells("Active").Value = False
        'Else
        '    grdtrad.Rows(e.RowIndex).Cells("Active").Value = True
        'End If

        If grdtrad.Columns(e.ColumnIndex).Name.ToUpper = "CPF" Then
            Dim grow As DataGridViewRow
            grow = grdtrad.CurrentRow
            If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
                grow.Cells("CPF").Value = "C"
            End If
            Dim arr As New ArrayList
            arr.Add("C")
            arr.Add("F")
            arr.Add("P")
            arr.Add("E")
            If Not arr.Contains(UCase(grow.Cells("CPF").Value)) Then
                MsgBox("Enter 'C','P','F' or 'E'.")
                grdtrad.Rows(e.RowIndex).Cells("CPF").Selected = True
                Exit Sub
            End If

            grow.Cells("CPF").Value = UCase(grow.Cells("CPF").Value)
            If Not IsDBNull(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "C" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "P" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
                    If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
                        grdtrad.Rows(e.RowIndex).Cells("delta").Value = one
                        grdtrad.Rows(e.RowIndex).Cells("lv").Value = zero.ToString("#0.00")
                        grdtrad.Rows(e.RowIndex).Cells("last").Value = CDbl(txtmkt.Text)
                    Else
                        grdtrad.Rows(e.RowIndex).Cells("delta").Value = zero
                        If grow.Cells("lv").Value <> "0" Then
                            If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "C" Then
                                If txtcvol.Text.Trim <> "" Then
                                    grow.Cells("lv").Value = Val(txtcvol.Text).ToString("#0.00")
                                Else
                                    grow.Cells("lv").Value = zero.ToString("#0.00")
                                End If
                                ' grdtrad.Rows(e.RowIndex).Cells(8).Value = val(txtcvol.Text)
                            Else
                                If txtpvol.Text.Trim <> "" Then
                                    grow.Cells("lv").Value = Val(txtpvol.Text).ToString("#0.00")
                                Else
                                    grow.Cells("lv").Value = zero.ToString("#0.00")
                                End If
                                'grdtrad.Rows(e.RowIndex).Cells(8).Value = val(txtpvol.Text)
                            End If
                        Else

                        End If
                    End If
                Else
                    'MsgBox("Enter 'C','P' or 'F'")
                    grdtrad.Rows(e.RowIndex).Cells("CPF").Selected = True
                    Exit Sub
                End If
            End If

        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "spval" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("spval").Value) Or grdtrad.Rows(e.RowIndex).Cells("spval").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("spval").Value) = "" Then
                If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
                    grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
                Else
                    grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
                End If
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "Strike" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("Strike").Value) Or grdtrad.Rows(e.RowIndex).Cells("Strike").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("Strike").Value) = "" Then
                grdtrad.Rows(e.RowIndex).Cells("Strike").Value = zero
            Else
                grdtrad.Rows(e.RowIndex).Cells("Strike").Value = Math.Abs(CDbl(grdtrad.Rows(e.RowIndex).Cells("Strike").Value))
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "units" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("units").Value) Or grdtrad.Rows(e.RowIndex).Cells("units").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("units").Value) = "" Then
                grdtrad.Rows(e.RowIndex).Cells("units").Value = zero
            ElseIf UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
                grdtrad.Rows(e.RowIndex).Cells("deltaval").Value = grdtrad.Rows(e.RowIndex).Cells("units").Value
                'to calculate deltaval
                cal_summary()
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "ltp" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("ltp").Value) Or grdtrad.Rows(e.RowIndex).Cells("ltp").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("ltp").Value) = "" Then
                grdtrad.Rows(e.RowIndex).Cells("ltp").Value = zero
            End If
            ''divyesh
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("ltp1").Value) Or grdtrad.Rows(e.RowIndex).Cells("ltp").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("ltp1").Value) = "" Then
                grdtrad.Rows(e.RowIndex).Cells("ltp1").Value = zero
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "last" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("last").Value) Or grdtrad.Rows(e.RowIndex).Cells("last").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("last").Value) = "" Then
                grdtrad.Rows(e.RowIndex).Cells("last").Value = zero
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "lv" Then
            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("lv").Value) Or grdtrad.Rows(e.RowIndex).Cells("lv").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("lv").Value) = "" Then
                zero = 0.01
                grdtrad.Rows(e.RowIndex).Cells("lv").Value = zero.ToString("#0.00")
                zero = 0
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "Active" Then

            Call cal_summary()

            grdtrad.EndEdit()

            Dim grow As DataGridViewRow
            grow = grdtrad.CurrentRow

            If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
                grow.Cells("CPF").Value = CStr("C")
            End If
            If IsDBNull(grow.Cells("spval").Value) Or grow.Cells("spval").Value Is Nothing Then
                grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
            Else
                grow.Cells("spval").Value = CDbl(grow.Cells("spval").Value)
            End If
            If IsDBNull(grow.Cells("Strike").Value) Or grow.Cells("Strike").Value Is Nothing Then
                grow.Cells("Strike").Value = zero
            End If
            If IsDBNull(grow.Cells("units").Value) Or grow.Cells("units").Value Is Nothing Then
                grow.Cells("units").Value = zero
            End If
            If IsDBNull(grow.Cells("last").Value) Or grow.Cells("last").Value Is Nothing Then
                grow.Cells("last").Value = zero
            End If
            If IsDBNull(grow.Cells("lv").Value) Or grow.Cells("lv").Value Is Nothing Then
                If txtcvol.Text.Trim <> "" Then
                    grow.Cells("lv").Value = CDbl(txtcvol.Text).ToString("#0.00")
                Else
                    grow.Cells("lv").Value = zero.ToString("#0.00")
                End If
            End If
            If IsDBNull(grow.Cells("delta").Value) Or grow.Cells("delta").Value Is Nothing Then
                grow.Cells("delta").Value = zero
            End If
            If IsDBNull(grow.Cells("deltaval").Value) Or grow.Cells("deltaval").Value Is Nothing Then
                grow.Cells("deltaval").Value = zero
            End If
            If IsDBNull(grow.Cells("gamma").Value) Or grow.Cells("gamma").Value Is Nothing Then
                grow.Cells("gamma").Value = zero
            End If
            If IsDBNull(grow.Cells("gmval").Value) Or grow.Cells("gmval").Value Is Nothing Then
                grow.Cells("gmval").Value = zero
            End If
            If IsDBNull(grow.Cells("vega").Value) Or grow.Cells("vega").Value Is Nothing Then
                grow.Cells("vega").Value = zero
            End If
            If IsDBNull(grow.Cells("vgval").Value) Or grow.Cells("vgval").Value Is Nothing Then
                grow.Cells("vgval").Value = zero
            End If
            If IsDBNull(grow.Cells("theta").Value) Or grow.Cells("theta").Value Is Nothing Then
                grow.Cells("theta").Value = zero
            End If
            If IsDBNull(grow.Cells("thetaval").Value) Or grow.Cells("thetaval").Value Is Nothing Then
                grow.Cells("thetaval").Value = zero
            End If

            If IsDBNull(grow.Cells("volga").Value) Or grow.Cells("volga").Value Is Nothing Then
                grow.Cells("volga").Value = zero
            End If
            If IsDBNull(grow.Cells("volgaval").Value) Or grow.Cells("volgaval").Value Is Nothing Then
                grow.Cells("volgaval").Value = zero
            End If
            If IsDBNull(grow.Cells("vanna").Value) Or grow.Cells("vanna").Value Is Nothing Then
                grow.Cells("vanna").Value = zero
            End If
            If IsDBNull(grow.Cells("vannaval").Value) Or grow.Cells("vannaval").Value Is Nothing Then
                grow.Cells("vannaval").Value = zero
            End If

            ''divyesh
            If IsDBNull(grow.Cells("delta1").Value) Or grow.Cells("delta1").Value Is Nothing Then
                grow.Cells("delta1").Value = zero
            End If
            If IsDBNull(grow.Cells("gamma1").Value) Or grow.Cells("gamma1").Value Is Nothing Then
                grow.Cells("gamma1").Value = zero
            End If
            If IsDBNull(grow.Cells("vega1").Value) Or grow.Cells("vega1").Value Is Nothing Then
                grow.Cells("vega1").Value = zero
            End If
            If IsDBNull(grow.Cells("theta1").Value) Or grow.Cells("theta1").Value Is Nothing Then
                grow.Cells("theta1").Value = zero
            End If
            If IsDBNull(grow.Cells("vgval1").Value) Or grow.Cells("vgval").Value Is Nothing Then
                grow.Cells("vgval1").Value = zero
            End If
            If IsDBNull(grow.Cells("gmval1").Value) Or grow.Cells("gmval").Value Is Nothing Then
                grow.Cells("gmval1").Value = zero
            End If
            If IsDBNull(grow.Cells("thetaval1").Value) Or grow.Cells("thetaval").Value Is Nothing Then
                grow.Cells("thetaval1").Value = zero
            End If
            If IsDBNull(grow.Cells("deltaval1").Value) Or grow.Cells("deltaval").Value Is Nothing Then
                grow.Cells("deltaval1").Value = zero
            End If

            If IsDBNull(grow.Cells("volgaval1").Value) Or grow.Cells("volgaval").Value Is Nothing Then
                grow.Cells("volgaval1").Value = zero
            End If
            If IsDBNull(grow.Cells("vannaval1").Value) Or grow.Cells("vannaval").Value Is Nothing Then
                grow.Cells("vannaval1").Value = zero
            End If


            grow.Cells("uid").Value = grdtrad.Rows.Count - 1
            'grdtrad.Refresh()
            'to attch context menu to new row
            'If (grdtrad.Rows(e.RowIndex).Cells("ltp").Value = 0) Then

            'REM 1 create contextmenustrip from vol column cells
            ReDim contMenu(grdtrad.Rows.Count - 1)
            contMenu(grdtrad.Rows.Count - 1) = New ContextMenuStrip
            'contMenu(grdtrad.Rows.Count - 1).ShowImageMargin = False
            Dim item As New ToolStripMenuItem("Unfreeze")
            ' item.CheckOnClick = True
            item.Tag = grdtrad.Rows.Count - 1
            AddHandler item.Click, AddressOf freezVol
            AddHandler contMenu(grdtrad.Rows.Count - 1).Opening, AddressOf contMenuOpen
            contMenu(grdtrad.Rows.Count - 1).Items.Add(item)
            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("lv").ContextMenuStrip = contMenu(grdtrad.Rows.Count - 1)
            REM 1: end

            'REM 1 create contextmenustrip from Expiry column cells
            ReDim contMenuExpiry(grdtrad.Rows.Count - 1)
            contMenuExpiry(grdtrad.Rows.Count - 1) = New ContextMenuStrip
            'contMenuExpiry(grdtrad.Rows.Count - 1).ShowImageMargin = False
            item = New ToolStripMenuItem("Unfreeze")
            ' item.CheckOnClick = True
            item.Tag = grdtrad.Rows.Count - 1
            AddHandler item.Click, AddressOf freezExpiry
            AddHandler contMenu(grdtrad.Rows.Count - 1).Opening, AddressOf contMenuOpenExpiry
            contMenuExpiry(grdtrad.Rows.Count - 1).Items.Add(item)
            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("TimeII").ContextMenuStrip = contMenuExpiry(grdtrad.Rows.Count - 1)
            REM 1: end
        End If
        grdtrad.EndEdit()
        If grdtrad.Columns(e.ColumnIndex).Name = "lv" Then
            If UCase(grdtrad.CurrentRow.Cells("CPF").Value) = "F" Or UCase(grdtrad.CurrentRow.Cells("CPF").Value) = "E" Then
                If Val(grdtrad.CurrentRow.Cells("last").Value) <> 0 Then
                    cal(8, grdtrad.CurrentRow)
                End If
            Else
                If Val(grdtrad.CurrentRow.Cells("Strike").Value) > 0 Then
                    cal(8, grdtrad.CurrentRow)
                End If
            End If
        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "last" Then
            cal(9, grdtrad.CurrentRow)
        Else
            If Val(grdtrad.CurrentRow.Cells("Strike").Value) > 0 And Val(grdtrad.CurrentRow.Cells("lv").Value) <> 0 Then
                cal(0, grdtrad.CurrentRow)
            End If

        End If
        'result(False)
        Dim bval As Double
        If (grdtrad.CurrentRow.Cells("lv").Value & "") = 0 Then
            If grdtrad.CurrentRow.Cells("CPF").Value = "C" Then
                If ((CDbl(grdtrad.CurrentRow.Cells("spval").Value) - CDbl(grdtrad.CurrentRow.Cells("Strike").Value)) > 0) Then
                    bval = (CDbl(grdtrad.CurrentRow.Cells("spval").Value) - CDbl(grdtrad.CurrentRow.Cells("Strike").Value))
                    grdtrad.CurrentRow.Cells("last").Value = ((bval - CDbl(grdtrad.CurrentRow.Cells("ltp").Value)) * CDbl(grdtrad.CurrentRow.Cells("units").Value))
                Else
                    bval = 0 ' CDbl(dr(col.ColumnName)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
                    grdtrad.CurrentRow.Cells("last").Value = ((bval - CDbl(grdtrad.CurrentRow.Cells("ltp").Value)) * CDbl(grdtrad.CurrentRow.Cells("units").Value))
                End If
            ElseIf grdtrad.CurrentRow.Cells("CPF").Value = "P" Then
                If ((CDbl(grdtrad.CurrentRow.Cells("Strike").Value) - CDbl(grdtrad.CurrentRow.Cells("spval").Value)) > 0) Then
                    bval = (CDbl(grdtrad.CurrentRow.Cells("Strike").Value) - CDbl(grdtrad.CurrentRow.Cells("spval").Value))
                    grdtrad.CurrentRow.Cells("last").Value = ((bval - CDbl(grdtrad.CurrentRow.Cells("ltp").Value)) * CDbl(grdtrad.CurrentRow.Cells("units").Value))
                Else
                    bval = 0 'CDbl(dr(col.ColumnName)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
                    grdtrad.CurrentRow.Cells("last").Value = ((bval - CDbl(grdtrad.CurrentRow.Cells("ltp").Value)) * CDbl(grdtrad.CurrentRow.Cells("units").Value))
                End If
            End If
        End If
        Call cal_summary()
    End Sub
    Private Sub grdtrad_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdtrad.EditingControlShowing
        AddHandler e.Control.KeyPress, AddressOf CheckCell

        'AddHandler e.Control.KeyDown, AddressOf cellkeydown
    End Sub
    Private Sub grdtrad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdtrad.KeyDown
        Dim KeyCode As Short = e.KeyCode
        Dim Shift As Short = e.KeyData \ &H10000

        If e.KeyCode = Keys.F2 Then
            If (grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "TimeI" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "TimeII" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "CPF" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "spval" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "Strike" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "units" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "ltp") Then
                grdtrad.CurrentCell.KeyEntersEditMode(e)
            End If
        ElseIf e.KeyCode = Keys.Escape Then
            'If grdtrad.CurrentRow.IsNewRow Then
            '    grdtrad.CancelEdit()
            'Else
            '    'grdtrad.Rows.RemoveAt(grdtrad.CurrentRow.Index)
            'End If
            'grdtrad.Rows.RemoveAt(grdtrad.CurrentRow.Index)
            grdtrad.CancelEdit()
            'grdtrad.BindingContext.Item(grdtrad.DataSource).CancelCurrentEdit()

        ElseIf e.KeyCode = Keys.Tab And Shift <> 1 Then
            If grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name.ToUpper = "LAST" Then
                If (grdtrad.CurrentRow.Index + 1) < (grdtrad.Rows.Count) Then
                    Me.grdtrad.CurrentCell = Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells("Active")
                Else
                    txtunits.Focus()
                End If
            End If
            If grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name.ToUpper = "DELTA" Then
                If (grdtrad.CurrentRow.Index + 1) < (grdtrad.Rows.Count) Then
                    Me.grdtrad.CurrentCell = Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells("Active")
                Else
                    txtunits.Focus()
                End If
            End If
        ElseIf e.KeyCode = Keys.Tab And Shift = 1 Then
            If grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name.ToUpper = "ACTIVE" Then
                If (grdtrad.CurrentRow.Index) <= grdtrad.Rows.Count Then
                    Me.grdtrad.CurrentCell = Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index - 1).Cells("delta")
                End If
            End If

            ' ''If grdtrad.CurrentCell.OwningColumn.Index = 4 Then
            ' ''    If grdtrad.CurrentRow.Cells("CPF").Value = "F" Or grdtrad.CurrentRow.Cells("CPF").Value = "E" Then
            ' ''        Me.grdtrad.CurrentCell = _
            ' ''                                 Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index).Cells("units")

            ' ''        e.SuppressKeyPress = True
            ' ''    End If
            ' ''    'ElseIf grdtrad.CurrentCell.OwningColumn.Index = 9 Then
            ' ''    '    If grdtrad.CurrentRow.Cells(3).Value = "F" Then
            ' ''    '        Me.grdtrad.CurrentCell = _
            ' ''    '                                  Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells(0)

            ' ''    '        e.SuppressKeyPress = True
            ' ''    '    End If
            ' ''ElseIf grdtrad.CurrentCell.OwningColumn.Index = 10 Then
            ' ''    If (Me.grdtrad.CurrentRow.Index + 1 <= Me.grdtrad.Rows.Count - 1) Then
            ' ''        Me.grdtrad.CurrentCell = Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells("Active")
            ' ''        e.SuppressKeyPress = True
            ' ''    Else
            ' ''        Me.grdtrad.CurrentCell = Me.grdtrad.Rows(0).Cells("Active")
            ' ''    End If
            ' ''End If
            ' ''If Shift = 1 And KeyCode = 9 Then
            ' ''    If grdtrad.CurrentCell.OwningColumn.Index = 0 Then
            ' ''        If (Me.grdtrad.CurrentRow.Index - 1 > 0) Then
            ' ''            Me.grdtrad.CurrentCell = Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index - 1).Cells(grdtrad.CurrentCell.OwningColumn.Index)
            ' ''            e.SuppressKeyPress = True
            ' ''        Else
            ' ''            'Me.grdtrad.CurrentCell = Me.grdtrad.Rows(0).Cells("last")
            ' ''        End If
            ' ''    End If
            ' ''End If
            End If
    End Sub
    Private Sub grdtrad_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs) Handles grdtrad.DataError

    End Sub
    Private Sub grdtrad_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellValueChanged
        If VarIsFrmLoad = False Then Exit Sub
        If (grdtrad.Columns(e.ColumnIndex).Name = "delta" Or grdtrad.Columns(e.ColumnIndex).Name = "deltaval" Or grdtrad.Columns(e.ColumnIndex).Name = "gamma" Or grdtrad.Columns(e.ColumnIndex).Name = "gmval" Or grdtrad.Columns(e.ColumnIndex).Name = "vega" Or grdtrad.Columns(e.ColumnIndex).Name = "vgval" Or grdtrad.Columns(e.ColumnIndex).Name = "theta" Or grdtrad.Columns(e.ColumnIndex).Name = "thetaval" Or grdtrad.Columns(e.ColumnIndex).Name = "volga" Or grdtrad.Columns(e.ColumnIndex).Name = "volgaval" Or grdtrad.Columns(e.ColumnIndex).Name = "vanna" Or grdtrad.Columns(e.ColumnIndex).Name = "vannaval") And e.RowIndex > -1 Then
            'grdtrad.Columns(e.ColumnIndex).Name = "TimeII" Or
            If Val(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
            ElseIf Val(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.DarkBlue
            Else
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
            End If
        End If
        If (grdtrad.Columns(e.ColumnIndex).Name = "TimeII") Then
            If (IsDate(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False) Then
                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtexp.Value
                'grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
                ' MsgBox("Please Enter Proper Date...")
                ' grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
            End If
        End If

        If (grdtrad.Columns(e.ColumnIndex).Name = "Active") Then
            'If (grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True) Then
            'grdtrad.EndEdit()
            If grdtrad.Columns(e.ColumnIndex).Name = "Active" Then
                Dim mxDate As Date = CDate("1-1-1980")
                Dim MinDate As Date = Now
                For Each gr As DataGridViewRow In grdtrad.Rows
                    If CBool(gr.Cells("Active").Value) = True Then
                        If CDate(gr.Cells("TimeII").Value) > mxDate Then
                            mxDate = CDate(gr.Cells("TimeII").Value)
                        End If

                        If CDate(gr.Cells("TimeII").Value) < MinDate Then
                            MinDate = CDate(gr.Cells("TimeII").Value)
                        End If
                    End If
                Next
                If mxDate = CDate("1-1-1980") Then
                    mxDate = MinDate
                End If
                dtexp.Value = mxDate
            End If

            Call cal_summary()
            'End If
        End If

        If (grdtrad.Columns(e.ColumnIndex).Name = "spval") Or (grdtrad.Columns(e.ColumnIndex).Name = "Strike") Then
            'grdtrad.EndEdit()
        End If

        If grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "c" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Yellow
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Yellow


        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "p" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LimeGreen
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LimeGreen

        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "f" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Orange
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.Orange
        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "e" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LightPink
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Pink
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Pink
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Pink
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Pink
            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LightPink

        End If
        If Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) < 0 Then
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.Red
        ElseIf Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) > 0 Then
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.SkyBlue
        Else
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.White
        End If

        grdtrad.Rows(e.RowIndex).Cells("ltp").Style.Format = "##0.00"
        grdtrad.Rows(e.RowIndex).Cells("last").Style.Format = "##0.00"

        grdtrad.Rows(e.RowIndex).Cells("delta").Style.Format = DecimalSetting.sDelta
        grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.Format = DecimalSetting.sDeltaval

        grdtrad.Rows(e.RowIndex).Cells("gamma").Style.Format = DecimalSetting.sGamma
        grdtrad.Rows(e.RowIndex).Cells("gmval").Style.Format = DecimalSetting.sGammaval

        grdtrad.Rows(e.RowIndex).Cells("vega").Style.Format = DecimalSetting.sVega
        grdtrad.Rows(e.RowIndex).Cells("vgval").Style.Format = DecimalSetting.sVegaval

        grdtrad.Rows(e.RowIndex).Cells("theta").Style.Format = DecimalSetting.sTheta
        grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.Format = DecimalSetting.sThetaval

        grdtrad.Rows(e.RowIndex).Cells("volga").Style.Format = DecimalSetting.sVolga
        grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.Format = DecimalSetting.sVolgaval

        grdtrad.Rows(e.RowIndex).Cells("vanna").Style.Format = DecimalSetting.sVanna
        grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.Format = DecimalSetting.sVannaval

        Call cal_summary()

    End Sub
    Private Sub grdtrad_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdtrad.RowsAdded
        If VarIsFrmLoad = False Then Exit Sub
        Dim zero As Double = 0
        If (e.RowIndex - 1) >= 0 Then
            grdtrad.Rows(e.RowIndex - 1).Cells("Active").Value = True
        End If
        grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dtexp.Value

        If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("spval").Value) Or grdtrad.Rows(e.RowIndex).Cells("spval").Value Is Nothing Then
            If txtmkt.Text.Trim <> "" Then
                grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
            Else
                grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
            End If
        End If

        'grdtrad.EndEdit()

        Dim grow As DataGridViewRow
        grow = grdtrad.Rows(e.RowIndex)

        If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
            grow.Cells("CPF").Value = "C"
        End If
        If IsDBNull(grow.Cells("spval").Value) Or grow.Cells("spval").Value Is Nothing Then
            grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
        Else
            grow.Cells("spval").Value = CDbl(grow.Cells("spval").Value)
        End If
        If IsDBNull(grow.Cells("Strike").Value) Or grow.Cells("Strike").Value Is Nothing Then
            grow.Cells("Strike").Value = zero
        End If
        If IsDBNull(grow.Cells("units").Value) Or grow.Cells("units").Value Is Nothing Then
            grow.Cells("units").Value = zero
        End If
        If IsDBNull(grow.Cells("last").Value) Or grow.Cells("last").Value Is Nothing Then
            grow.Cells("last").Value = zero
        End If
        If IsDBNull(grow.Cells("lv").Value) Or grow.Cells("lv").Value Is Nothing Then
            If txtcvol.Text.Trim <> "" Then
                grow.Cells("lv").Value = Val(txtcvol.Text)
            Else
                grow.Cells("lv").Value = zero
            End If
        End If
        If IsDBNull(grow.Cells("delta").Value) Or grow.Cells("delta").Value Is Nothing Then
            grow.Cells("delta").Value = zero
        End If
        If IsDBNull(grow.Cells("deltaval").Value) Or grow.Cells("deltaval").Value Is Nothing Then
            grow.Cells("deltaval").Value = zero
        End If
        If IsDBNull(grow.Cells("gamma").Value) Or grow.Cells("gamma").Value Is Nothing Then
            grow.Cells("gamma").Value = zero
        End If
        If IsDBNull(grow.Cells("gmval").Value) Or grow.Cells("gmval").Value Is Nothing Then
            grow.Cells("gmval").Value = zero
        End If
        If IsDBNull(grow.Cells("vega").Value) Or grow.Cells("vega").Value Is Nothing Then
            grow.Cells("vega").Value = zero
        End If
        If IsDBNull(grow.Cells("vgval").Value) Or grow.Cells("vgval").Value Is Nothing Then
            grow.Cells("vgval").Value = zero
        End If
        If IsDBNull(grow.Cells("theta").Value) Or grow.Cells("theta").Value Is Nothing Then
            grow.Cells("theta").Value = zero
        End If
        If IsDBNull(grow.Cells("thetaval").Value) Or grow.Cells("thetaval").Value Is Nothing Then
            grow.Cells("thetaval").Value = zero
        End If
        If IsDBNull(grow.Cells("volga").Value) Or grow.Cells("volga").Value Is Nothing Then
            grow.Cells("volga").Value = zero
        End If
        If IsDBNull(grow.Cells("volgaval").Value) Or grow.Cells("volgaval").Value Is Nothing Then
            grow.Cells("volgaval").Value = zero
        End If
        If IsDBNull(grow.Cells("vanna").Value) Or grow.Cells("vanna").Value Is Nothing Then
            grow.Cells("vanna").Value = zero
        End If
        If IsDBNull(grow.Cells("vannaval").Value) Or grow.Cells("vannaval").Value Is Nothing Then
            grow.Cells("vannaval").Value = zero
        End If

        'If (grdtrad.Columns(e.ColumnIndex).Name = "Dummy") And e.RowIndex > -1 Then
        'grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "1"
        'End If
        'grdtrad.Rows(e.RowIndex).Cells("Dummy").Value = 1

        grow.Cells("uid").Value = grdtrad.Rows.Count - 1
    End Sub
    Private Sub grdtrad_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdtrad.CellFormatting
        If grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "c" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Yellow
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Yellow

            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Yellow
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Yellow

        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "p" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LimeGreen
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LimeGreen

            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.LimeGreen
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.LimeGreen

            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LimeGreen

        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "f" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Orange
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Orange

            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Orange

            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.Orange
        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "e" Then
            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E"
            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LightPink
            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LightPink

            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.LightPink
            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.LightPink

            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LightPink
        End If
        If Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) < 0 Then
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.Red
        ElseIf Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) > 0 Then
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.SkyBlue
        Else
            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.White
        End If



        grdtrad.Rows(e.RowIndex).Cells("ltp").Style.Format = "##0.00"
        grdtrad.Rows(e.RowIndex).Cells("last").Style.Format = "##0.00"

        grdtrad.Rows(e.RowIndex).Cells("delta").Style.Format = DecimalSetting.sDelta
        grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.Format = DecimalSetting.sDeltaval

        grdtrad.Rows(e.RowIndex).Cells("gamma").Style.Format = DecimalSetting.sGamma
        grdtrad.Rows(e.RowIndex).Cells("gmval").Style.Format = DecimalSetting.sGammaval

        grdtrad.Rows(e.RowIndex).Cells("vega").Style.Format = DecimalSetting.sVega
        grdtrad.Rows(e.RowIndex).Cells("vgval").Style.Format = DecimalSetting.sVegaval

        grdtrad.Rows(e.RowIndex).Cells("theta").Style.Format = DecimalSetting.sTheta
        grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.Format = DecimalSetting.sThetaval

        grdtrad.Rows(e.RowIndex).Cells("volga").Style.Format = DecimalSetting.sVolga
        grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.Format = DecimalSetting.sVolgaval

        grdtrad.Rows(e.RowIndex).Cells("vanna").Style.Format = DecimalSetting.sVanna
        grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.Format = DecimalSetting.sVannaval

        'grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Black
        'grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Black

        'grdtrad.Rows(e.RowIndex).Cells("last").Style.BackColor = ColorPremium
        'grdtrad.Rows(e.RowIndex).Cells("delta").Style.BackColor = ColorDelta
        'grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.BackColor = ColorDelta
        'grdtrad.Rows(e.RowIndex).Cells("gamma").Style.BackColor = ColorGamma
        'grdtrad.Rows(e.RowIndex).Cells("gmval").Style.BackColor = ColorGamma
        'grdtrad.Rows(e.RowIndex).Cells("vega").Style.BackColor = ColorVega
        'grdtrad.Rows(e.RowIndex).Cells("vgval").Style.BackColor = ColorVega
        'grdtrad.Rows(e.RowIndex).Cells("theta").Style.BackColor = ColorTheta
        'grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.BackColor = ColorTheta
        'grdtrad.Rows(e.RowIndex).Cells("volga").Style.BackColor = ColorVolga
        'grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.BackColor = ColorVolga
        'grdtrad.Rows(e.RowIndex).Cells("vanna").Style.BackColor = ColorVanna
        'grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.BackColor = ColorVanna
    End Sub

    Private Sub grdprofit_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdprofit.CellDoubleClick, grddelta.CellDoubleClick, grdgamma.CellDoubleClick, grdvega.CellDoubleClick, grdtheta.CellDoubleClick, grdvolga.CellDoubleClick, grdvanna.CellDoubleClick
        Dim Typ As String = CType(sender, DataGridView).Tag.ToString
        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
            fill_result(0, e.RowIndex, e.ColumnIndex)
            Dim res As New resultfrm
            res.temptable = rtable
            res.ShowForm(Typ)
        End If
    End Sub

    Private Sub grdStaticCalc_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdStaticCalc.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex > 1 Then
                If Val(grdStaticCalc.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & "") < 0 Then
                    grdStaticCalc.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdStaticCalc.Rows(e.RowIndex).Cells(e.ColumnIndex).Value & "") > 0 Then
                    grdStaticCalc.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdStaticCalc.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If

                'grdStaticCalc.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N2"
                'Dim d As Double = Double.Parse(e.Value.ToString)
                'Dim str As String = "N" & RoundGrossMTM
                'e.Value = d.ToString(str)
                ' grdStaticCalc.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N2"
            End If
        End If
    End Sub

    Private Sub grdprofit_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdprofit.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex > 1 Then
                If Val(grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & RoundGrossMTM
                e.Value = d.ToString(str)
            End If
        End If
        
    End Sub
    Private Sub grdprofit_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdprofit.CellEnter, grddelta.CellEnter, grdgamma.CellEnter, grdtheta.CellEnter, grdvolga.CellEnter, grdvega.CellEnter, grdvanna.CellEnter
        For Each grow As DataGridViewRow In CType(sender, DataGridView).Rows
            For i As Integer = 1 To CType(sender, DataGridView).Columns.Count - 1
                If grow.Selected = True Then
                    grow.Cells(i).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
                Else
                    grow.Cells(i).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Regular, GraphicsUnit.World))
                End If
            Next
        Next
    End Sub
    Private Sub grddelta_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grddelta.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex > 1 Then
                If Val(grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                '  grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sDeltaval
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & DecimalSetting.iRDeltaval
                e.Value = d.ToString(str)
            End If
        End If
    End Sub
    Private Sub grdgamma_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdgamma.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex > 1 Then
                If Val(grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                'grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sGammaval
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & DecimalSetting.iRGammaval
                e.Value = d.ToString(str)

            End If
        End If
    End Sub
    Private Sub grdvega_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdvega.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex > 1 Then
                If Val(grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                'grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sVegaval
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & DecimalSetting.iRVegaval
                e.Value = d.ToString(str)
            End If
        End If
    End Sub
    Private Sub grdtheta_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdtheta.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex > 1 Then
                If Val(grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                ' grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sThetaval
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & DecimalSetting.iRThetaval
                e.Value = d.ToString(str)
            End If
        End If
    End Sub
    Private Sub grdvolga_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdvolga.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex > 1 Then
                If Val(grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & DecimalSetting.iRVolgaval
                e.Value = d.ToString(str)
            End If
        End If
    End Sub
    Private Sub grdvanna_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles grdvanna.CellFormatting
        If e.RowIndex > -1 Then
            If e.ColumnIndex = 0 Then
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.BackColor = Me.ColorSpot
                CType(sender, DataGridView).Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Font = (New Font("Microsoft Sans Serif", 12, FontStyle.Bold, GraphicsUnit.World))
            ElseIf e.ColumnIndex > 1 Then
                If Val(grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
                    grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
                ElseIf Val(grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
                    grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
                Else
                    grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
                End If
                Dim d As Double = Double.Parse(e.Value.ToString)
                Dim str As String = "N" & DecimalSetting.iRVannaval
                e.Value = d.ToString(str)
            End If
        End If
    End Sub
    Private Sub cellkeydown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Tab Then
            If grdtrad.CurrentCell.OwningColumn.Index = 4 Then
                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Then
                    Me.grdtrad.CurrentCell = _
                                              Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index).Cells("units")

                    e.SuppressKeyPress = True
                End If
            ElseIf grdtrad.CurrentCell.OwningColumn.Index = 6 Then
                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Then
                    Me.grdtrad.CurrentCell = _
                                              Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells("Active")
                    e.SuppressKeyPress = True
                End If
            End If

        End If
    End Sub
    Private Sub CheckCell(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If FirstDgt = 0 Then
            FirstDgt = Val(e.KeyChar.ToString)
        End If
        'Dim KeyAscii As Short = Asc(e.KeyChar)
        Dim col As Integer
        col = grdtrad.CurrentCell.ColumnIndex
        Select Case col
            Case 2
                dateonly(e)
            Case 4
                numonly(e)
            Case 5
                numonly(e)
            Case 6
                numonly(e)

            Case 7
                numonly(e)
            Case 8
                numonly(e)
            Case 10
                numonly(e)
            Case 3
                Dim arr As New ArrayList
                arr.Add(67)
                arr.Add(70)
                arr.Add(80)
                arr.Add(69)
                arr.Add(8)

                If Not arr.Contains(Asc(UCase(e.KeyChar))) Then
                    e.Handled = True
                End If


        End Select
        '... 

        '... code to check the input

        '... 

        'If KeyAscii = 0 Then

        '    e.Handled = True

        'End If

    End Sub

#End Region

    Private Sub ShowMenuToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowMenuToolStripMenuItem.Click
        If CType(sender, ToolStripMenuItem).Text = "Show Menu" Then
            CType(sender, ToolStripMenuItem).Text = "Hide Menu"
            MDI.MainMenuStrip.Visible = True
        ElseIf CType(sender, ToolStripMenuItem).Text = "Hide Menu" Then
            CType(sender, ToolStripMenuItem).Text = "Show Menu"
            MDI.MainMenuStrip.Visible = False
        End If
    End Sub

    Private Sub scenario1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If Not objanalysis Is Nothing Then
            objanalysis.refreshstarted = True
        End If
        Me.Icon = My.Resources.volhedge_icon
        'Me.WindowState = FormWindowState.Maximized
        'Me.Refresh()
    End Sub

    Private Sub scenario1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        chkscenario = False
        txtmkt.Text = 0
        TxtMStrike.Text = 0
        'If Not objanalysis Is Nothing Then
        '    objanalysis.refreshstarted = True
        'End If
        If MsgBox("Do you want to save Interval and No. of Strike??", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Save") = MsgBoxResult.Yes Then
            objScenarioDetail.Delete_scenario()
            objScenarioDetail.Interval = Val(txtinterval.Text)
            objScenarioDetail.Strike = Val(txtllimit.Text)
            objScenarioDetail.Interval_type = IIf(chkint.Checked = True, "Per", "Value")
            objScenarioDetail.Insert_scenario()
        End If
        Call analysis.searchcompany()
    End Sub

    Private Sub scenario1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'MDI.MainMenuStrip.Visible = True
    End Sub

    Private Sub scenario1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F9 Then
            'grdtrad.EndEdit()
            'grdtrad.Refresh()
            If Val(txtllimit.Text) > 0 And Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then
                result(False)
                'init_table()
                'cal_profit()
                'create_chart_profit()
                'create_chart_profit_Full()
                'cal_delta()
                'create_chart_delta()
                'create_chart_delta_Full()
                'cal_gamma()
                'create_chart_gamma()
                'create_chart_gamma_Full()
                'cal_vega()
                'create_chart_vega()
                'create_chart_vega_Full()
                'cal_theta()
                'create_chart_theta()
                'create_chart_theta_Full()

                'grdprofit.DataSource = profit
                'grdprofit.Refresh()
                'grddelta.DataSource = deltatable
                'grddelta.Refresh()
                'grdgamma.DataSource = gammatable
                'grdgamma.Refresh()
                'grdvega.DataSource = vegatable
                'grdvega.Refresh()
                'grdtheta.DataSource = thetatable
                'grdtheta.Refresh()
            End If
        ElseIf e.KeyCode = Keys.F1 Then
            result(True)
        ElseIf e.KeyCode = Keys.F4 Then
            If ShowPanel2 = False Then
                ShowPanel2 = True
                KeyF4Togal = True
                HideToolStripMenuItem.Checked = False
            Else
                KeyF4Togal = False
                BtnFixPnl2_Click(sender, e)
                ShowPanel2 = False
                HideToolStripMenuItem.Checked = True
            End If
        ElseIf e.KeyCode = Keys.F3 Then
            If ShowPanel1 = False Then
                ShowPanel1 = True
                KeyF3Togal = True
                AutoHideToolStripMenuItem.Checked = False
            Else
                KeyF3Togal = False
                BtnFix_Click(sender, e)
                ShowPanel1 = False
                AutoHideToolStripMenuItem.Checked = True
            End If

        ElseIf e.KeyCode = Keys.F8 Then
            CmdAllCV_Click(sender, e)
        ElseIf e.KeyCode = Keys.F11 Then
            export()
        ElseIf e.KeyCode = Keys.F12 Then
            import()
        ElseIf e.KeyCode = Keys.PageDown Then
            If tbcon.SelectedIndex < 9 Then tbcon.SelectTab(tbcon.SelectedIndex + 1)
        ElseIf e.KeyCode = Keys.PageUp Then
            If tbcon.SelectedIndex > 0 Then tbcon.SelectTab(tbcon.SelectedIndex - 1)
        ElseIf e.KeyCode = Keys.Escape And MDI.MainMenuStrip.Visible = False Then
            'Me.Close()
        ElseIf e.KeyCode = Keys.Escape And TabLPanelSpotDiff.Visible = True Then
            Call Button1_Click(sender, e)
        End If
    End Sub

    Private Sub scenario1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DecimalSetting.sDelta = IIf(Deltastr.Contains("."), Deltastr, Deltastr & ".")
        DecimalSetting.sDeltaval = IIf(Deltastr_Val.Contains("."), Deltastr_Val, Deltastr_Val & ".")
        DecimalSetting.sGamma = IIf(Gammastr.Contains("."), Gammastr, Gammastr & ".")
        DecimalSetting.sGammaval = IIf(Gammastr_Val.Contains("."), Gammastr_Val, Gammastr_Val & ".")
        DecimalSetting.sVega = IIf(Vegastr.Contains("."), Vegastr, Vegastr & ".")
        DecimalSetting.sVegaval = IIf(Vegastr_Val.Contains("."), Vegastr_Val, Vegastr_Val & ".")
        DecimalSetting.sTheta = IIf(Thetastr.Contains("."), Thetastr, Thetastr & ".")
        DecimalSetting.sThetaval = IIf(Thetastr_Val.Contains("."), Thetastr_Val, Thetastr_Val & ".")
        DecimalSetting.sVolga = IIf(Volgastr.Contains("."), Volgastr, Volgastr & ".")
        DecimalSetting.sVolgaval = IIf(Volgastr_Val.Contains("."), Volgastr_Val, Volgastr_Val & ".")
        DecimalSetting.sVanna = IIf(Vannastr.Contains("."), Vannastr, Vannastr & ".")
        DecimalSetting.sVannaval = IIf(Vannastr_Val.Contains("."), Vannastr_Val, Vannastr_Val & ".")

        DecimalSetting.iRDelta = IIf(Deltastr.Contains("."), Deltastr, Deltastr & ".").ToString.Trim.Length - 4
        DecimalSetting.iRDeltaval = IIf(Deltastr_Val.Contains("."), Deltastr_Val, Deltastr_Val & ".").ToString.Trim.Length - 4
        DecimalSetting.iRGamma = IIf(Gammastr.Contains("."), Gammastr, Gammastr & ".").ToString.Trim.Length - 4
        DecimalSetting.iRGammaval = IIf(Gammastr_Val.Contains("."), Gammastr_Val, Gammastr_Val & ".").ToString.Trim.Length - 4
        DecimalSetting.iRVega = IIf(Vegastr.Contains("."), Vegastr, Vegastr & ".").ToString.Trim.Length - 4
        DecimalSetting.iRVegaval = IIf(Vegastr_Val.Contains("."), Vegastr_Val, Vegastr_Val & ".").ToString.Trim.Length - 4
        DecimalSetting.iRTheta = IIf(Thetastr.Contains("."), Thetastr, Thetastr & ".").ToString.Trim.Length - 4
        DecimalSetting.iRThetaval = IIf(Thetastr_Val.Contains("."), Thetastr_Val, Thetastr_Val & ".").ToString.Trim.Length - 4
        DecimalSetting.iRVolga = IIf(Volgastr.Contains("."), Volgastr, Volgastr & ".").ToString.Trim.Length - 4
        DecimalSetting.iRVolgaval = IIf(Volgastr_Val.Contains("."), Volgastr_Val, Volgastr_Val & ".").ToString.Trim.Length - 4
        DecimalSetting.iRVanna = IIf(Vannastr.Contains("."), Vannastr, Vannastr & ".").ToString.Trim.Length - 4
        DecimalSetting.iRVannaval = IIf(Vannastr_Val.Contains("."), Vannastr_Val, Vannastr_Val & ".").ToString.Trim.Length - 4



        'MDI.MainMenuStrip.Visible = False
        VarIsFrmLoad = True
        VarIsFirstLoad = True
        Me.WindowState = FormWindowState.Maximized
        If Not objanalysis Is Nothing Then
            objanalysis.refreshstarted = False
        End If
        chkscenario = True
        Me.Text = Me.Text & "-" & scname
        If scenariotable.Rows.Count > 0 Then
            grossmtm = scenariotable.Rows(0)("grossMTM")
        End If
        If VarIsCurrency = False Then
            txtinterast.Text = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
        ElseIf VarIsCurrency = True Then
            txtinterast.Text = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='CurrrencyRateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
        End If
        Mrateofinterast = Val(txtinterast.Text)
        'DecimalSetting.iRTheta = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRTheta'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRTheta'")))
        'DecimalSetting.iRDelta = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRDelta'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRDelta'")))
        'DecimalSetting.iRVega = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRVega'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRVega'")))
        'DecimalSetting.iRGamma = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRGamma'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRGamma'")))
        txtcvol.Text = 0
        txtpvol.Text = 0
        txtmid = 0
        If trscen = True Then
            chkcheck.Checked = True
            gcheck = True
            dttoday.Value = time1

            dtexp.Value = CDate(scenariotable.Compute("Max(time2)", "")) 'time2
            txtdays.Text = DateDiff(DateInterval.Day, time1.Date, time2.Date)
            If dttoday.Value.Date < dtexp.Value.Date Then
                REM This code commented because of Day difference plus 1 days display
                'txtdays.Text = Val(txtdays.Text) + 1
            ElseIf dttoday.Value.Date = dtexp.Value.Date Then
                txtdays.Text = Val(txtdays.Text) + 0.5
            End If
            txtmkt.Text = mvalue 'Math.Round(mvalue, 0)
            TxtMStrike.Text = mvalue 'Math.Round(mvalue, 0)
            txtmid = mvalue
            Dim grow As New DataGridViewRow
            For Each drow As DataRow In scenariotable.Rows
                grow = New DataGridViewRow
                grdtrad.Rows.Add(grow)
                'grdtrad.Refresh()
            Next
            Dim i As Integer = 0
            txtunits.Text = 0
            txtdelval.Text = 0
            txtthval.Text = 0
            txtvgval.Text = 0
            txtgmval.Text = 0

            ''divyesh
            txtunits1.Text = 0
            txtdelval1.Text = 0
            txtthval1.Text = 0
            txtvgval1.Text = 0
            txtgmval1.Text = 0
            For Each drow As DataRow In scenariotable.Select("", "cpf")
                grdtrad.Rows(i).Cells("Active").Value = CBool(drow("status"))
                grdtrad.Rows(i).Cells("TimeI").Value = CDate(drow("time1"))
                grdtrad.Rows(i).Cells("TimeII").Value = CDate(drow("time2"))
                grdtrad.Rows(i).Cells("CPF").Value = CStr(drow("cpf"))
                grdtrad.Rows(i).Cells("spval").Value = CDbl(drow("spot"))
                grdtrad.Rows(i).Cells("Strike").Value = CDbl(drow("strikes"))
                grdtrad.Rows(i).Cells("units").Value = Val(drow("qty"))
                grdtrad.Rows(i).Cells("ltp").Value = Val(drow("ltp"))
                grdtrad.Rows(i).Cells("last").Value = CDbl(drow("rate"))
                If Val(drow("vol")) = 0 Then
                    grdtrad.Rows(i).Cells("lv").Value = CDbl(0.0)
                Else
                    grdtrad.Rows(i).Cells("lv").Value = Val(drow("vol"))
                End If
                grdtrad.Rows(i).Cells("delta").Value = Val(drow("delta"))
                grdtrad.Rows(i).Cells("deltaval").Value = Val(drow("deltaval"))
                grdtrad.Rows(i).Cells("gamma").Value = Val(drow("gamma"))
                grdtrad.Rows(i).Cells("gmval").Value = Val(drow("gmval"))
                grdtrad.Rows(i).Cells("vega").Value = Val(drow("vega"))
                grdtrad.Rows(i).Cells("vgval").Value = Val(drow("vgval"))
                grdtrad.Rows(i).Cells("theta").Value = Val(drow("theta"))
                grdtrad.Rows(i).Cells("thetaval").Value = Val(drow("thetaval"))

                grdtrad.Rows(i).Cells("volga").Value = Val(drow("volga"))
                grdtrad.Rows(i).Cells("volgaval").Value = Val(drow("volgaval"))
                grdtrad.Rows(i).Cells("vanna").Value = Val(drow("vanna"))
                grdtrad.Rows(i).Cells("vannaval").Value = Val(drow("vannaval"))

                grdtrad.Rows(i).Cells("uid").Value = Val(drow("uid"))

                ''divyesh
                grdtrad.Rows(i).Cells("delta1").Value = Val(drow("delta"))
                grdtrad.Rows(i).Cells("gamma1").Value = Val(drow("gamma"))
                grdtrad.Rows(i).Cells("vega1").Value = Val(drow("vega"))
                grdtrad.Rows(i).Cells("theta1").Value = Val(drow("theta"))
                grdtrad.Rows(i).Cells("volga1").Value = Val(drow("volga"))
                grdtrad.Rows(i).Cells("vanna1").Value = Val(drow("vanna"))

                grdtrad.Rows(i).Cells("deltaval1").Value = Val(drow("deltaval1"))
                grdtrad.Rows(i).Cells("gmval1").Value = Val(drow("gmval1"))
                grdtrad.Rows(i).Cells("vgval1").Value = Val(drow("vgval1"))
                grdtrad.Rows(i).Cells("thetaval1").Value = Val(drow("thetaval1"))
                grdtrad.Rows(i).Cells("volgaval1").Value = Val(drow("volgaval1"))
                grdtrad.Rows(i).Cells("vannaval1").Value = Val(drow("vannaval1"))


                grdtrad.Rows(i).Cells("ltp1").Value = Val(drow("ltp1"))
                grdtrad.Rows(i).Cells("DifFactor").Value = Val(drow("DifFactor") & "")

                'txtunits.Text = Math.Round(Val(txtunits.Text) + Val(grdtrad.Rows(i).Cells("ltp").Value), 2)
                txtdelval.Text = Format(Val(txtdelval.Text) + Val(grdtrad.Rows(i).Cells("deltaval").Value), DecimalSetting.sDeltaval)
                txtthval.Text = Format(Val(txtthval.Text) + Val(grdtrad.Rows(i).Cells("thetaval").Value), DecimalSetting.sThetaval)
                txtvgval.Text = Format(Val(txtvgval.Text) + Val(grdtrad.Rows(i).Cells("vgval").Value), DecimalSetting.sVegaval)
                txtgmval.Text = Format(Val(txtgmval.Text) + Val(grdtrad.Rows(i).Cells("gmval").Value), DecimalSetting.sGammaval)
                txtVolgaVal.Text = Format(Val(txtVolgaVal.Text) + Val(grdtrad.Rows(i).Cells("volgaval").Value), DecimalSetting.sVolgaval)
                TxtVannaVal.Text = Format(Val(TxtVannaVal.Text) + Val(grdtrad.Rows(i).Cells("vannaval").Value), DecimalSetting.sVannaval)


                ''divyesh 
                txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(grdtrad.Rows(i).Cells("deltaval1").Value), DecimalSetting.sDeltaval)
                txtthval1.Text = Format(Val(txtthval1.Text) + Val(grdtrad.Rows(i).Cells("thetaval1").Value), DecimalSetting.sThetaval)
                txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(grdtrad.Rows(i).Cells("vgval1").Value), DecimalSetting.sVegaval)
                txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(grdtrad.Rows(i).Cells("gmval1").Value), DecimalSetting.sGammaval)
                TxtVolgaVal1.Text = Format(Val(TxtVolgaVal1.Text) + Val(grdtrad.Rows(i).Cells("volgaval1").Value), DecimalSetting.sVolgaval)
                TxtVannaVal1.Text = Format(Val(TxtVannaVal1.Text) + Val(grdtrad.Rows(i).Cells("vannaval1").Value), DecimalSetting.sVannaval)

                i += 1
            Next
            grdtrad.Columns("TimeII").DefaultCellStyle.NullValue = Format(scenariotable.Rows(0).Item("time2"), "MM/dd/yyyy")
            scenariotable.Rows.Clear()
            trscen = False

            'ShowPanel1 = False
            'KeyF3Togal = False
            'Me.TableLayoutPanel1.RowStyles.Item(0) = (New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1.0!))
            'grdtrad.Focus()
        End If


        'to get interval,No of strike from database
        Dim dtScenario As New DataTable
        dtScenario = objScenarioDetail.select_scenario
        If dtScenario.Rows.Count > 0 Then
            If dtScenario.Rows(0)("interval_type").ToString = "Per" Then
                chkint.Checked = True
            Else
                chkint.Checked = False
            End If
            txtinterval.Text = Val(dtScenario.Rows(0)("interval").ToString)
            txtllimit.Text = Val(dtScenario.Rows(0)("strike").ToString)
            interval = txtinterval.Text
        Else
            'Call CalInterval()
            'If Val(TxtMStrike.Text) > 0 Then
            '    txtmid = Val(TxtMStrike.Text)
            '    If Val(txtmid) < 100 Then
            '        txtinterval.Text = 1
            '    ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
            '        txtinterval.Text = 5
            '    ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
            '        txtinterval.Text = 10
            '    ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
            '        txtinterval.Text = 100
            '    ElseIf Val(txtmid) > 10000 Then
            '        txtinterval.Text = 500
            '    End If
            '    interval = txtinterval.Text
            '    'interval = Math.Round(val(txtmid) * 1 / 100, 0)
            'End If
        End If

        Call CalInterval()

        txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
        ''divyesh
        txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)

        txtdays.Text = 0
        If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        If dttoday.Value.Date < dtexp.Value.Date Then
            REM This code commented because of Day difference plus 1 days display
            'txtdays.Text = Val(txtdays.Text) + 1
        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
            txtdays.Text = Val(txtdays.Text) + 0.5
        End If

        frmFlg = False
        create_active()
        'create contextmenustrip for vol column cells
        ReDim contMenu(grdtrad.Rows.Count - 1)
        Dim cnt As Integer = 0
        For Each cntmenu As ContextMenuStrip In contMenu
            cntmenu = New ContextMenuStrip
            Dim item As New ToolStripMenuItem("Unfreeze")
            ' item.CheckOnClick = True
            item.Tag = cnt.ToString
            AddHandler item.Click, AddressOf freezVol
            AddHandler cntmenu.Opening, AddressOf contMenuOpen
            cntmenu.Items.Add(item)
            grdtrad.Rows(cnt).Cells("lv").ContextMenuStrip = cntmenu
            cnt += 1
        Next

        'create contextmenustrip for expiry column cells
        ReDim contMenuExpiry(grdtrad.Rows.Count - 1)
        cnt = 0
        For Each cntmenu As ContextMenuStrip In contMenuExpiry
            cntmenu = New ContextMenuStrip
            Dim item As New ToolStripMenuItem("Unfreeze")
            ' item.CheckOnClick = True
            item.Tag = cnt.ToString
            AddHandler item.Click, AddressOf freezExpiry
            AddHandler cntmenu.Opening, AddressOf contMenuOpenExpiry
            cntmenu.Items.Add(item)
            grdtrad.Rows(cnt).Cells("TimeII").ContextMenuStrip = cntmenu
            cnt += 1
        Next

        For Each col As DataGridViewColumn In grdtrad.Columns
            If col.Index >= 9 Then
                col.HeaderCell.Style.BackColor = Color.Gray
            End If
            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Next

        Call Vol_Click(Vol, e)
        Call expiry_Click(expiry, e)
        If scname <> "" Then
            mAllCV = ""
            result(True)
        End If
        'For Each drow As DataGridViewRow In grdtrad.Rows
        '    drow.Cells("VolORG").Value = drow.Cells("lv").Value
        'Next
        If analysis.chkanalysis = True Then
            MDI.ToolStripcompanyCombo.Visible = False
            MDI.ToolStripMenuSearchComp.Visible = False
        End If
        'Default panel 1 Desplay
        Me.TableLayoutPanel1.RowStyles.Item(0) = (New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        GetSpotDiff(False)
    End Sub

    Private Sub SplitContainer1_Panel1_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SplitContainer1.Panel1.MouseHover, grdtrad.MouseHover
        If KeyF3Togal = False Then
            If MDI.MainMenuStrip.Visible = False Then
                If MousePosition.Y <= 55 Then
                    ShowPanel1 = True
                Else
                    ShowPanel1 = False
                End If
            Else
                If MousePosition.Y <= 105 Then
                    ShowPanel1 = True
                Else
                    ShowPanel1 = False
                End If
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If TmPnl1 = True Then
            If ShowPanel1 = True Then
                If TabLPanel1.Location.Y <= 0 Then
                    If TabLPanel1.Location.Y = 0 Then
                        txtmkt.Focus()
                        If KeyF3Togal = True Then
                            Call BtnFix_Click(sender, e)
                        End If
                    End If
                    TabLPanel1.Top = TabLPanel1.Location.Y + 1
                End If
            Else
                If TabLPanel1.Location.Y >= -28 Then
                    If TabLPanel1.Location.Y = -28 Then
                        grdtrad.Focus()
                    End If
                    TabLPanel1.Top = TabLPanel1.Location.Y - 1
                End If
            End If
        End If
        If TmPnl2 = True Then
            If ShowPanel2 = True Then

                If TabLPanel2.Location.Y >= ((SplitContainer1.Panel2.Height - 331) + 270) Then
                    If TabLPanel2.Location.Y = ((SplitContainer1.Panel2.Height - 331) + 270) Then
                        grdStaticCalc.Focus()
                        If KeyF4Togal = True Then
                            Call BtnFixPnl2_Click(sender, e)
                        End If
                    End If
                    TabLPanel2.Top = TabLPanel2.Location.Y - 1
                End If

            Else
                If TabLPanel2.Location.Y <= ((SplitContainer1.Panel2.Height - 331) + 350) Then
                    If TabLPanel2.Location.Y = ((SplitContainer1.Panel2.Height - 331) + 350) Then
                        'cmdresult.Focus()
                    End If
                    TabLPanel2.Top = TabLPanel2.Location.Y + 1
                End If
            End If
        End If

    End Sub

    Private Sub SplitContainer1_Panel1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles SplitContainer1.Panel1.MouseMove, grdtrad.MouseMove
        Call SplitContainer1_Panel1_MouseHover(sender, e)
    End Sub

    Private Sub BtnFix_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFix.Click
        If TmPnl1 = True Then
            'Timer1.Stop()
            TmPnl1 = False
            Me.TableLayoutPanel1.RowStyles.Item(0) = (New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            'Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Else
            TmPnl1 = True
            'Timer1.Start()
            Me.TableLayoutPanel1.RowStyles.Item(0) = (New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.0!))
            'Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.730376!))
        End If
    End Sub
    Private Sub BtnFixPnl2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFixPnl2.Click
        If TmPnl2 = True Then
            'Timer1.Stop()
            TmPnl2 = False
            'Me.TableLayoutPanel1.RowStyles.Item(0) = (New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
            'Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Else
            TmPnl2 = True
            'Timer1.Start()
            'Me.TableLayoutPanel1.RowStyles.Item(0) = (New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 1.0!))
            'Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.730376!))
        End If
    End Sub

    Private Sub txtmkt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmkt.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub

    Private Sub txtmkt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmkt.KeyPress
        numonly(e)
        flgUnderline = True
    End Sub

    Private Sub txtmkt_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmkt.Leave
        If Val(TxtMStrike.Text) = 0 Then
            TxtMStrike.Text = txtmkt.Text
        End If
        If txtmkt.Text.Trim = "" Then
            txtmkt.Text = 0
        End If
        If Val(txtmkt.Text) > 0 Then
            If Val(txtinterval.Text) = 0 Then
                Call CalInterval()
            End If
            'txtmid = Math.Floor(Val(txtmkt.Text))
            ''If Val(txtmid) < 100 Then
            ''    interval = 1
            ''ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
            ''    interval = 5
            ''ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
            ''    interval = 10
            ''ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
            ''    interval = 100
            ''ElseIf Val(txtmid) > 10000 Then
            ''    interval = 500
            ''End If
            If flgUnderline = False Then Exit Sub
            If grdtrad.Rows.Count > 0 Then
                For i As Integer = 0 To grdtrad.Rows.Count - 2
                    Dim grow As DataGridViewRow
                    grow = grdtrad.Rows(i)
                    grow.Cells("spval").Value = CDbl(txtmkt.Text) + grow.Cells("DifFactor").Value

                    If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                        'If IsDBNull(grow.Cells(4).Value) Or grow.Cells(4).Value Is Nothing Or CStr(grow.Cells(4).Value) = "" Then
                        grow.Cells("last").Value = grow.Cells("spval").Value 'CDbl(txtmkt.Text)
                        'End If
                    End If
                    cal(7, grow, True)
                Next
                grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("spval").Value = CDbl(txtmkt.Text) + grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("DifFactor").Value
                grdtrad.EndEdit()
                ' result()
            End If
        End If
        Call cal_summary()
        flgUnderline = False
    End Sub

    Private Sub TxtMStrike_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtMStrike.KeyPress
        numonly(e)
    End Sub

    Private Sub CmdAllCV_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CmdAllCV.Click
        REM In Scenario window For All Greeks Chart and GridData on One tab,According to Button press it shows chart or Griddata
        Dim mSelIndex As Integer
        mAllCV = ""
        result(True)

        'If CmdAllCV.Text = "Show &All Charts(F8)" Then
        '    CmdAllCV.Text = "Show &All Values(F8)"
        '    mAllCV = "Charts"
        '    'chtprofit.Visible = True
        '    'chtdelta.Visible = True
        '    'chtgamma.Visible = True
        '    'chtvega.Visible = True
        '    'chttheta.Visible = True
        '    'chtvolga.Visible = True
        '    'chtvanna.Visible = True
        'Else
        '    CmdAllCV.Text = "Show &All Charts(F8)"
        '    mAllCV = "Values"
        '    'chtprofit.Visible = True
        '    'chtdelta.Visible = True
        '    'chtgamma.Visible = True
        '    'chtvega.Visible = True
        '    'chttheta.Visible = True
        '    'chtvolga.Visible = True
        '    'chtvanna.Visible = True
        'End If

        If mAllCV.Trim <> "" Then
            mSelIndex = tbcon.SelectedIndex
            For SelIndex As Int16 = 0 To tbcon.TabPages.Count
                tbcon.SelectedIndex = SelIndex
                Select Case UCase(tbcon.SelectedTab.Name)
                    Case "TBPROFITCHART"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtprofit, grdprofit, lblprofit, True)
                        Else
                            SetChartGridForAll(chtprofit, grdprofit, lblprofit, False)
                        End If
                    Case "TBCHDELTA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtdelta, grddelta, lbldelta, True)
                        Else
                            SetChartGridForAll(chtdelta, grddelta, lbldelta, False)
                        End If
                    Case "TBCHGAMMA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtgamma, grdgamma, lblgamma, True)
                        Else
                            SetChartGridForAll(chtgamma, grdgamma, lblgamma, False)
                        End If
                    Case "TBCHVEGA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtvega, grdvega, lblvega, True)
                        Else
                            SetChartGridForAll(chtvega, grdvega, lblvega, False)
                        End If
                    Case "TBCHTHETA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chttheta, grdtheta, lbltheta, True)
                        Else
                            SetChartGridForAll(chttheta, grdtheta, lbltheta, False)
                        End If
                    Case "TBCHVOLGA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtvolga, grdvolga, lblvolga, True)
                        Else
                            SetChartGridForAll(chtvolga, grdvolga, lblvolga, False)
                        End If
                    Case "TBCHVANNA"
                        If mAllCV = "Charts" Then
                            SetChartGridForAll(chtvanna, grdvanna, lblvanna, True)
                        Else
                            SetChartGridForAll(chtvanna, grdvanna, lblvanna, False)
                        End If
                End Select
            Next
            tbcon.SelectedIndex = mSelIndex
        End If
        'SetResultButtonText(cmdresult, chtprofit, grdprofit, True, True)
        'SetResultButtonText(cmdresult, chtdelta, grddelta, True, True)
        'SetResultButtonText(cmdresult, chtgamma, grdgamma, True, True)
        'SetResultButtonText(cmdresult, chtvega, grdvega, True, True)
        'SetResultButtonText(cmdresult, chttheta, grdtheta, True, True)
        'SetResultButtonText(cmdresult, chtvolga, grdvolga, True, True)
        'SetResultButtonText(cmdresult, chtvanna, grdvanna, True, True)

        ''Select Case tbcon.SelectedTab.Name
        ''    Case ""
        ''    Case Else
        ''        SetResultButtonText(cmdresult, chtprofit, grdprofit, True)
        ''        SetResultButtonText(cmdresult, chtdelta, grddelta, True)
        ''        SetResultButtonText(cmdresult, chtgamma, grdgamma, True)
        ''        SetResultButtonText(cmdresult, chtvega, grdvega, True)
        ''        SetResultButtonText(cmdresult, chttheta, grdtheta, True)
        ''        SetResultButtonText(cmdresult, chtvolga, grdvolga, True)
        ''        SetResultButtonText(cmdresult, chtvanna, grdvanna, True)
        ''End Select
    End Sub
    Private Sub cmdcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdcheck.Click
        bCheckAll = True
        If grdact.ColumnCount > 0 Then
            If grdact.RowCount > 0 Then
                If checkall = True Then
                    checkall = False
                    For Each col As DataGridViewColumn In grdact.Columns
                        grdact.Rows(0).Cells(col.Index).Value = False
                    Next
                    cmdcheck.Text = "CheckAll"
                Else
                    checkall = True
                    For Each col As DataGridViewColumn In grdact.Columns
                        grdact.Rows(0).Cells(col.Index).Value = True
                    Next
                    cmdcheck.Text = "ClearAll"
                End If

            End If
        End If
        bCheckAll = False
        Call ButResult_Click(sender, e)
    End Sub


    Private Sub cmdresult_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdresult.Click
        REM In Scenario window For Selected Individual Greeks Chart and GridData on One tab,According to Button press it shows chart or Griddata
        mAllCV = ""
        result(True)

        '  Me.tbcon.DrawMode = TabDrawMode.OwnerDrawFixed
    End Sub
    Private Sub txtdays_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        numonly(e)
    End Sub
    Private Sub txtllimit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtllimit.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub
    Private Sub txtllimit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtllimit.KeyPress
        numonly(e)
    End Sub
    Private Sub txtinterval_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtinterval.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub
    Private Sub txtinterval_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtinterval.KeyPress
        numonly(e)
    End Sub

    Private Sub dttoday_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles dttoday.Leave
        txtdays.Text = 0
        If grdtrad.Rows.Count = 2 Then
            grdtrad.Rows(0).Cells("TimeI").Value = dttoday.Value
        ElseIf frmFlg = True Then 'this flg used bcoz first need not to assign expiry date
            For Each grow As DataGridViewRow In grdtrad.Rows
                If grow.Cells("TimeI").ReadOnly = False Then
                    grow.Cells("TimeI").Value = dttoday.Value
                End If
            Next
        End If

        If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        If dttoday.Value.Date < dtexp.Value.Date Then
            REM This code commented because of Day difference plus 1 days display
            'txtdays.Text = Val(txtdays.Text) + 1
        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
            txtdays.Text = Val(txtdays.Text) + 0.5
        End If
        frmFlg = False
        txtdays_Leave(sender, e)
        
    End Sub
    Private Sub dtexp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles dtexp.Leave
        txtdays.Text = 0
        If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        If dttoday.Value.Date < dtexp.Value.Date Then
            REM This code commented because of Day difference plus 1 days display
            'txtdays.Text = Val(txtdays.Text) + 1
        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
            txtdays.Text = Val(txtdays.Text) + 0.5
        End If
        For Each grow As DataGridViewRow In grdtrad.Rows
            If UCase(grow.Cells("CPF").Value) = "F" Or UCase(grow.Cells("CPF").Value) = "E" Then
                If Val(grow.Cells("last").Value) <> 0 Then
                    cal(8, grow, True)
                End If
            Else
                If Val(grow.Cells("Strike").Value) > 0 Then
                    cal(8, grow, True)
                End If
            End If
        Next
        frmFlg = False
        txtdays_Leave(sender, e)
        
    End Sub
    Private Sub txtdays_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtdays.Text.Trim <> "" And txtdays.Text <> "0" Then
            dtexp.Value = DateAdd(DateInterval.Day, CInt(txtdays.Text), dttoday.Value)
        End If
        cal_summary()

        create_active()
    End Sub

    Private Sub txtinterast_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtinterast.Leave
        Mrateofinterast = Val(txtinterast.Text)
    End Sub
    Private Sub txtinterast_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtinterast.KeyPress
        numonly(e)
    End Sub

    Private Sub CmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdExport.Click
        export()
    End Sub

    Private Sub chkcheck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcheck.CheckedChanged

        If grdtrad.RowCount > 1 Then
            If chkcheck.Checked = False Then
                gcheck = False
                For i As Integer = 0 To grdtrad.RowCount - 2
                    grdtrad.Rows(i).Cells("Active").Value = False
                Next
                'For Each grow As DataGridViewRow In grdtrad.Rows
                '    grow.Cells(0).Value = False
                'Next
            Else
                gcheck = True
                For i As Integer = 0 To grdtrad.RowCount - 2
                    grdtrad.Rows(i).Cells("Active").Value = True
                Next
                'For Each grow As DataGridViewRow In grdtrad.Rows
                '    grow.Cells(0).Value = True
                'Next
            End If


        End If

    End Sub

    Private Sub cmdexp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If grdtrad.Rows.Count > 1 Then
            cal_exp()
        End If
    End Sub

    Private Sub cmdimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdimport.Click
        Me.Cursor = Cursors.WaitCursor
        import()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub txtcvol_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtcvol.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub

    Private Sub txtcvol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles txtcvol.Leave
        If txtcvol.Text.Trim = "" Then
            txtcvol.Text = 0
        End If
        If Val(txtcvol.Text) > 0 Then
            If grdtrad.Rows.Count > 0 Then
                For Each grow As DataGridViewRow In grdtrad.Rows
                    If grow.Cells("CPF").Value = "C" Then
                        If grow.Cells("lv").ReadOnly = False Then
                            grow.Cells("lv").Value = Val(txtcvol.Text)
                        End If
                        Dim mt As Integer
                        Dim mmt As Integer
                        Dim futval As Double

                        mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
                        mmt = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date) - 1

                        futval = Val(grow.Cells("spval").Value)
                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, True, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100), 0)
                        'cal_summary()
                    End If
                Next
                'If grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("CPF").Value = "C" Then
                REM When user changes vol in top box, it should be automatically applied to first line vol as well.
                grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("lv").Value = CDbl(txtcvol.Text)
                grdtrad.EndEdit()
                'End If
                cal_summary()
            End If
        End If
    End Sub
    Private Sub txtcvol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcvol.KeyPress
        numonly(e)
    End Sub

    Private Sub txtpvol_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpvol.GotFocus
        SendKeys.Send("{HOME}+{END}")
    End Sub
    Private Sub txtpvol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles txtpvol.Leave
        If txtpvol.Text.Trim = "" Then
            txtpvol.Text = 0
        End If
        If Val(txtpvol.Text) > 0 Then
            If grdtrad.Rows.Count > 0 Then
                For Each grow As DataGridViewRow In grdtrad.Rows
                    If grow.Cells("CPF").Value = "P" Then
                        If grow.Cells("lv").ReadOnly = False Then
                            grow.Cells("lv").Value = Val(txtpvol.Text)
                        End If
                        Dim mt As Integer
                        Dim mmt As Integer
                        Dim futval As Double

                        mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
                        mmt = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date) - 1

                        futval = Val(grow.Cells("spval").Value)
                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, False, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100), 0)
                        'cal_summary()
                    End If
                Next
                'grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("lv").Value = CDbl(txtpvol.Text)
                grdtrad.EndEdit()
                cal_summary()
            End If
        End If
    End Sub
    Private Sub txtpvol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpvol.KeyPress
        numonly(e)
    End Sub

    Private Sub tbcon_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles tbcon.DrawItem
        Dim g As Graphics = e.Graphics
        Dim tp As TabPage = tbcon.TabPages(e.Index)
        Dim br As System.Drawing.Brush
        Dim sf As New StringFormat
        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width + 2, e.Bounds.Height - 2)
        sf.Alignment = StringAlignment.Near
        Dim strTitle As String = tp.Text
        If tbcon.SelectedIndex = e.Index Then
            Dim f As Font = New Font(tbcon.Font.Name, tbcon.Font.Size, FontStyle.Regular, tbcon.Font.Unit)
            br = New SolidBrush(Color.Black)
            g.FillRectangle(br, e.Bounds)
            '  g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.White)
            g.DrawString(strTitle, f, br, r, sf)


        Else
            Dim f As Font = New Font(tbcon.Font.Name, tbcon.Font.Size, FontStyle.Regular, tbcon.Font.Unit)
            br = New SolidBrush(Color.WhiteSmoke)
            g.FillRectangle(br, e.Bounds)
            'g.FillRectangle(br, e.Bounds)
            br = New SolidBrush(Color.Black)
            g.DrawString(strTitle, f, br, r, sf)

        End If
        tp.Refresh()
    End Sub

    Private Sub txtunits_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtunits.TextChanged
        If Val(txtunits.Text) > 0 Then
            txtunits.BackColor = Color.MediumSeaGreen
            txtunits.ForeColor = Color.Black
        ElseIf Val(txtunits.Text) < 0 Then
            txtunits.BackColor = Color.DarkOrange
            txtunits.ForeColor = Color.Black
        Else
            txtunits.BackColor = Color.FromArgb(255, 255, 128)
            txtunits.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtdelval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdelval.TextChanged
        If Val(txtdelval.Text) > 0 Then
            txtdelval.BackColor = Color.MediumSeaGreen
            txtdelval.ForeColor = Color.Black
        ElseIf Val(txtdelval.Text) < 0 Then
            txtdelval.BackColor = Color.DarkOrange
            txtdelval.ForeColor = Color.Black
        Else
            txtdelval.BackColor = Color.FromArgb(255, 255, 128)
            txtdelval.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtgmval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtgmval.TextChanged
        If Val(txtgmval.Text) > 0 Then
            txtgmval.BackColor = Color.MediumSeaGreen
            txtgmval.ForeColor = Color.Black
        ElseIf Val(txtgmval.Text) < 0 Then
            txtgmval.BackColor = Color.DarkOrange
            txtgmval.ForeColor = Color.Black
        Else
            txtgmval.BackColor = Color.FromArgb(255, 255, 128)
            txtgmval.ForeColor = Color.Black

        End If
    End Sub
    Private Sub txtvgval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvgval.TextChanged
        If Val(txtvgval.Text) > 0 Then
            txtvgval.BackColor = Color.MediumSeaGreen
            txtvgval.ForeColor = Color.Black
        ElseIf Val(txtvgval.Text) < 0 Then
            txtvgval.BackColor = Color.DarkOrange
            txtvgval.ForeColor = Color.Black
        Else
            txtvgval.BackColor = Color.FromArgb(255, 255, 128)
            txtvgval.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtthval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtthval.TextChanged
        If Val(txtthval.Text) > 0 Then
            txtthval.BackColor = Color.MediumSeaGreen
            txtthval.ForeColor = Color.Black
        ElseIf Val(txtthval.Text) < 0 Then
            txtthval.BackColor = Color.DarkOrange
            txtthval.ForeColor = Color.Black
        Else
            txtthval.BackColor = Color.FromArgb(255, 255, 128)
            txtthval.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtunits1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtunits1.TextChanged
        If Val(txtunits1.Text) > 0 Then
            txtunits1.BackColor = Color.MediumSeaGreen
            txtunits1.ForeColor = Color.Black
        ElseIf Val(txtunits1.Text) < 0 Then
            txtunits1.BackColor = Color.DarkOrange
            txtunits1.ForeColor = Color.Black
        Else
            txtunits1.BackColor = Color.FromArgb(255, 255, 128)
            txtunits1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtdelval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtdelval1.TextChanged
        If Val(txtdelval1.Text) > 0 Then
            txtdelval1.BackColor = Color.MediumSeaGreen
            txtdelval1.ForeColor = Color.Black
        ElseIf Val(txtdelval1.Text) < 0 Then
            txtdelval1.BackColor = Color.DarkOrange
            txtdelval1.ForeColor = Color.Black
        Else
            txtdelval1.BackColor = Color.FromArgb(255, 255, 128)
            txtdelval1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtgmval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtgmval1.TextChanged
        If Val(txtgmval1.Text) > 0 Then
            txtgmval1.BackColor = Color.MediumSeaGreen
            txtgmval1.ForeColor = Color.Black
        ElseIf Val(txtgmval1.Text) < 0 Then
            txtgmval1.BackColor = Color.DarkOrange
            txtgmval1.ForeColor = Color.Black
        Else
            txtgmval1.BackColor = Color.FromArgb(255, 255, 128)
            txtgmval1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtvgval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtvgval1.TextChanged
        If Val(txtvgval1.Text) > 0 Then
            txtvgval1.BackColor = Color.MediumSeaGreen
            txtvgval1.ForeColor = Color.Black
        ElseIf Val(txtvgval1.Text) < 0 Then
            txtvgval1.BackColor = Color.DarkOrange
            txtvgval1.ForeColor = Color.Black
        Else
            txtvgval1.BackColor = Color.FromArgb(255, 255, 128)
            txtvgval1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtthval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtthval1.TextChanged
        If Val(txtthval1.Text) > 0 Then
            txtthval1.BackColor = Color.MediumSeaGreen
            txtthval1.ForeColor = Color.Black
        ElseIf Val(txtthval1.Text) < 0 Then
            txtthval1.BackColor = Color.DarkOrange
            txtthval1.ForeColor = Color.Black
        Else
            txtthval1.BackColor = Color.FromArgb(255, 255, 128)
            txtthval1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub txtVolgaVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVolgaVal.TextChanged
        If Val(txtVolgaVal.Text) > 0 Then
            txtVolgaVal.BackColor = Color.MediumSeaGreen
            txtVolgaVal.ForeColor = Color.Black
        ElseIf Val(txtVolgaVal.Text) < 0 Then
            txtVolgaVal.BackColor = Color.DarkOrange
            txtVolgaVal.ForeColor = Color.Black
        Else
            txtVolgaVal.BackColor = Color.FromArgb(255, 255, 128)
            txtVolgaVal.ForeColor = Color.Black
        End If
    End Sub

    Private Sub TxtVolgaVal1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtVolgaVal1.TextChanged
        If Val(TxtVolgaVal1.Text) > 0 Then
            TxtVolgaVal1.BackColor = Color.MediumSeaGreen
            TxtVolgaVal1.ForeColor = Color.Black
        ElseIf Val(TxtVolgaVal1.Text) < 0 Then
            TxtVolgaVal1.BackColor = Color.DarkOrange
            TxtVolgaVal1.ForeColor = Color.Black
        Else
            TxtVolgaVal1.BackColor = Color.FromArgb(255, 255, 128)
            TxtVolgaVal1.ForeColor = Color.Black
        End If
    End Sub
    Private Sub TxtVannaVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtVannaVal.TextChanged
        If Val(TxtVannaVal.Text) > 0 Then
            TxtVannaVal.BackColor = Color.MediumSeaGreen
            TxtVannaVal.ForeColor = Color.Black
        ElseIf Val(TxtVannaVal.Text) < 0 Then
            TxtVannaVal.BackColor = Color.DarkOrange
            TxtVannaVal.ForeColor = Color.Black
        Else
            TxtVannaVal.BackColor = Color.FromArgb(255, 255, 128)
            TxtVannaVal.ForeColor = Color.Black
        End If
    End Sub
    Private Sub TxtVannaVal1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtVannaVal1.TextChanged
        If Val(TxtVannaVal1.Text) > 0 Then
            TxtVannaVal1.BackColor = Color.MediumSeaGreen
            TxtVannaVal1.ForeColor = Color.Black
        ElseIf Val(TxtVannaVal1.Text) < 0 Then
            TxtVannaVal1.BackColor = Color.DarkOrange
            TxtVannaVal1.ForeColor = Color.Black
        Else
            TxtVannaVal1.BackColor = Color.FromArgb(255, 255, 128)
            TxtVannaVal1.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtinterval_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) 'Handles txtinterval.Leave
        REM When scenario calculation passes value as negative in ltp, then it gives an overflow in values,For this Set Calulation Of Interval And No Of Strikes, multiplication of interval and no Of Strike must be less than CMP
        If chkint.Checked = True Then
            interval = Val(txtinterval.Text) * 100
        Else
            interval = Val(txtinterval.Text)
        End If
        If Val(TxtMStrike.Text) > 0 And Val(txtinterval.Text) > 0 And Val(txtllimit.Text) > 0 Then
            If Val(txtinterval.Text) > Math.Round(Val(TxtMStrike.Text) / Val(txtllimit.Text)) Then
                'MsgBox("Interval Must be Less Or Equal To " & Math.Round(Val(TxtMStrike.Text) / Val(txtllimit.Text), 0), MsgBoxStyle.Information)
                'txtinterval.Focus()
                'Exit Sub
            End If
        End If
    End Sub

    Private Sub chtprofit_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtprofit.DblClick ', grdprofit.DoubleClick
        chartName = "profit"
        ValLBLprofit = lblprofit.Text
        objProfitLossChart.ShowForm()
    End Sub
    Private Sub chttheta_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chttheta.DblClick ', grdtheta.DoubleClick
        chartName = "theta"
        ValLBLtheta = lbltheta.Text
        objProfitLossChart.ShowForm()
    End Sub

    Private Sub chtvega_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtvega.DblClick ', grdvega.DoubleClick
        chartName = "vega"
        ValLBLvega = lblvega.Text
        objProfitLossChart.ShowForm()
    End Sub

    Private Sub chtdelta_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtdelta.DblClick ', grddelta.DoubleClick
        chartName = "delta"
        ValLBLdelta = lbldelta.Text
        objProfitLossChart.ShowForm()
    End Sub

    Private Sub chtgamma_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtgamma.DblClick ', grdgamma.DoubleClick
        chartName = "gamma"
        ValLBLgamma = lblgamma.Text
        objProfitLossChart.ShowForm()
    End Sub

    Private Sub chtvolga_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtvolga.DblClick ', grdvolga.DoubleClick
        chartName = "volga"
        ValLBLvolga = lblvolga.Text
        objProfitLossChart.ShowForm()
    End Sub
    Private Sub chtvanna_DblClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles chtvanna.DblClick ', grdvanna.DoubleClick
        chartName = "vanna"
        ValLBLVanna = lblvanna.Text
        objProfitLossChart.ShowForm()
    End Sub

    Private Sub Vol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Vol.Click
        Try
            If Vol.Text = "Freeze Vol Column" Then
                For Each grow As DataGridViewRow In grdtrad.Rows
                    grow.Cells("lv").ReadOnly = True
                    Vol.Text = "Unfreeze Vol Column"
                Next
            Else
                For Each grow As DataGridViewRow In grdtrad.Rows
                    grow.Cells("lv").ReadOnly = False
                    Vol.Text = "Freeze Vol Column"
                Next
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub scenario1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Not XResolution = Screen.PrimaryScreen.Bounds.Width Then
            XResolution = Screen.PrimaryScreen.Bounds.Width
            Call setGridTrad()
        End If
    End Sub

    Private Sub chkint_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkint.CheckedChanged
        If chkint.Checked = True Then
            txtinterval.Text = 2
            'Label2.Visible = True
        Else
            'Label2.Visible = False
            txtinterval.Text = 100
        End If
    End Sub

    Private Sub grdtrad_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdtrad.CellMouseDown
        If e.RowIndex <> -1 Then
            cellno = e.RowIndex
            'grdtrad.EndEdit()
            'Call cal_summary()
        End If
    End Sub

    Private Sub expiry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles expiry.Click
        Try
            If expiry.Text = "Freeze Expiry Column" Then
                For Each grow As DataGridViewRow In grdtrad.Rows
                    grow.Cells("TimeII").ReadOnly = True
                    expiry.Text = "Unfreeze Expiry Column"
                Next
            Else
                For Each grow As DataGridViewRow In grdtrad.Rows
                    grow.Cells("TimeII").ReadOnly = False
                    expiry.Text = "Freeze Expiry Column"
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub btnincrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnincrease.Click
        involflag = True
        Call IncDecVolCal()
    End Sub
    Private Sub btndecrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndecrease.Click
        decvolflag = True
        Call IncDecVolCal()
    End Sub
    Private Sub IncDecVolCal() ''divyesh
        If txtvol.Text.Trim = "" Then
            txtvol.Text = 0
        End If
        If grdtrad.Rows.Count >= 0 Then
            For Each grow As DataGridViewRow In grdtrad.Rows
                If grow.Cells("CPF").Value = "C" Or grow.Cells("CPF").Value = "P" Then
                    If grow.Cells("lv").ReadOnly = False Then
                        If involflag = True Then
                            grow.Cells("lv").Value = grow.Cells("lv").Value + Val(txtvol.Text)
                        ElseIf decvolflag = True Then
                            If grow.Cells("lv").Value - Val(txtvol.Text) > 0 Then
                                grow.Cells("lv").Value = grow.Cells("lv").Value - Val(txtvol.Text)
                            End If
                        End If
                    End If
                    Dim mt As Double
                    Dim mmt As Double
                    Dim futval As Double

                    mt = DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date)
                    mmt = (DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)) - 1
                    REM For Rate Not To Be Zero  Alpesh & Udipth Sir
                    REM Change Mt=0.5 Because Everywhere Mt=0.5 
                    If mt = 0 Then
                        'mt = 0.05
                        'mmt = 0
                        mt = 0.5
                        mmt = 0
                    End If
                    If mmt = 0 Then
                        'mmt = 0.05
                        mmt = 0.5
                    End If
                    futval = Val(grow.Cells("spval").Value)
                    Dim VarIsCall As Boolean = False
                    If grow.Cells("CPF").Value = "C" Then
                        VarIsCall = True
                    End If
                    CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, VarIsCall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100), 0)
                    'cal_summary()
                End If
            Next
            involflag = False
            decvolflag = False
            'result(False)
            cal_summary()
        End If

    End Sub

    Private Sub txtvol_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtvol.KeyPress
        numonly(e)
    End Sub

    Private Sub tbcon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbcon.SelectedIndexChanged
        Select Case UCase(tbcon.SelectedTab.Name)
            Case "TBPROFITCHART"
                SetResultButtonText(cmdresult, chtprofit, grdprofit, lblprofit, False)
            Case "TBCHDELTA"
                SetResultButtonText(cmdresult, chtdelta, grddelta, lbldelta, False)
            Case "TBCHGAMMA"
                SetResultButtonText(cmdresult, chtgamma, grdgamma, lblgamma, False)
            Case "TBCHVEGA"
                SetResultButtonText(cmdresult, chtvega, grdvega, lblvega, False)
            Case "TBCHTHETA"
                SetResultButtonText(cmdresult, chttheta, grdtheta, lbltheta, False)
            Case "TBCHVOLGA"
                SetResultButtonText(cmdresult, chtvolga, grdvolga, lblvolga, False)
            Case "TBCHVANNA"
                SetResultButtonText(cmdresult, chtvanna, grdvanna, lblvanna, False)
        End Select
        For i As Integer = 0 To grdStaticCalc.Rows.Count - 2
            Call grdStaticCalcCellEndEdit(0, i)
        Next


    End Sub

    Private Sub txtllimit_Leave(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles txtllimit.Leave
        REM When scenario calculation passes value as negative in ltp, then it gives an overflow in values,For this Set Calulation Of Interval And No Of Strikes, multiplication of interval and no Of Strike must be less than CMP
        If Val(TxtMStrike.Text) > 0 And Val(txtinterval.Text) > 0 And Val(txtllimit.Text) > 0 Then
            If Val(txtllimit.Text) > Math.Round(Val(TxtMStrike.Text) / Val(txtinterval.Text)) Then
                'MsgBox("No. of Strike +/- Must Be Less Than " & Math.Round(Val(TxtMStrike.Text) / Val(txtinterval.Text), 0), MsgBoxStyle.Information)
                'txtllimit.Focus()
                'Exit Sub
            End If
        End If
    End Sub
    REM Whenever any line is unchecked delta of those option must be removed from that line as well
    Private Sub grdtrad_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdtrad.CurrentCellDirtyStateChanged
        If grdtrad.IsCurrentCellDirty Then
            grdtrad.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub ButResult_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButResult.Click
        REM  For Change In Grid Data Or Volatality Increment Click On Result Button  Show Charts
        result(False)
    End Sub

    Private Sub TxtMStrike_Leave(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles TxtMStrike.Leave
        txtmid = Val(TxtMStrike.Text)
    End Sub

    Private Sub TxtMStrike_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtMStrike.TextChanged
        txtmid = Val(TxtMStrike.Text)
    End Sub

    Private Sub HideGreeksToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideGreeksToolStripMenuItem.Click
        If CType(sender, ToolStripMenuItem).Text = "Show Greeks" Then
            CType(sender, ToolStripMenuItem).Text = "Hide Greeks"
            'MDI.MainMenuStrip.Visible = True
            grdtrad.Columns("delta").Visible = True
            grdtrad.Columns("gamma").Visible = True
            grdtrad.Columns("vega").Visible = True
            grdtrad.Columns("theta").Visible = True
            grdtrad.Columns("Volga").Visible = True
            grdtrad.Columns("Vanna").Visible = True

            'grdtrad.Columns("delta1").Visible = True
            'grdtrad.Columns("gamma1").Visible = True
            'grdtrad.Columns("vega1").Visible = True
            'grdtrad.Columns("theta1").Visible = True
            'grdtrad.Columns("Volga1").Visible = True
            'grdtrad.Columns("Vanna1").Visible = True

        ElseIf CType(sender, ToolStripMenuItem).Text = "Hide Greeks" Then
            CType(sender, ToolStripMenuItem).Text = "Show Greeks"
            'MDI.MainMenuStrip.Visible = False
            grdtrad.Columns("delta").Visible = False
            grdtrad.Columns("gamma").Visible = False
            grdtrad.Columns("vega").Visible = False
            grdtrad.Columns("theta").Visible = False
            grdtrad.Columns("Volga").Visible = False
            grdtrad.Columns("Vanna").Visible = False

            'grdtrad.Columns("delta1").Visible = False
            'grdtrad.Columns("gamma1").Visible = False
            'grdtrad.Columns("vega1").Visible = False
            'grdtrad.Columns("theta1").Visible = False
            'grdtrad.Columns("Volga1").Visible = False
            'grdtrad.Columns("Vanna1").Visible = False
        End If
    End Sub

    Private Sub HideGreeksValueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideGreeksValueToolStripMenuItem.Click
        If CType(sender, ToolStripMenuItem).Text = "Show Greeks Value" Then
            CType(sender, ToolStripMenuItem).Text = "Hide Greeks Value"
            'MDI.MainMenuStrip.Visible = True
            grdtrad.Columns("deltaval").Visible = True
            grdtrad.Columns("gmval").Visible = True
            grdtrad.Columns("vgval").Visible = True
            grdtrad.Columns("thetaval").Visible = True
            grdtrad.Columns("volgaval").Visible = True
            grdtrad.Columns("VannaVal").Visible = True

            'grdtrad.Columns("deltaval1").Visible = True
            'grdtrad.Columns("gmval1").Visible = True
            'grdtrad.Columns("vgval1").Visible = True
            'grdtrad.Columns("thetaval1").Visible = True
            'grdtrad.Columns("volgaval1").Visible = True
            'grdtrad.Columns("vannaVal1").Visible = True
        ElseIf CType(sender, ToolStripMenuItem).Text = "Hide Greeks Value" Then
            CType(sender, ToolStripMenuItem).Text = "Show Greeks Value"
            'MDI.MainMenuStrip.Visible = False
            grdtrad.Columns("deltaval").Visible = False
            grdtrad.Columns("gmval").Visible = False
            grdtrad.Columns("vgval").Visible = False
            grdtrad.Columns("thetaval").Visible = False
            grdtrad.Columns("volgaval").Visible = False
            grdtrad.Columns("VannaVal").Visible = False

            'grdtrad.Columns("deltaval1").Visible = False
            'grdtrad.Columns("gmval1").Visible = False
            'grdtrad.Columns("vgval1").Visible = False
            'grdtrad.Columns("thetaval1").Visible = False
            'grdtrad.Columns("volgaval1").Visible = False
            'grdtrad.Columns("vannaVal1").Visible = False
        End If
    End Sub

    
    Private Sub grdprofit_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdprofit.MouseHover, grddelta.MouseHover, grdgamma.MouseHover, grdvega.MouseHover, grdtheta.MouseHover, grdvolga.MouseHover, grdvanna.MouseHover
        ' Me.Text = "M:" & MousePosition.Y & " Pnl:" & TabLPanel2.Location.Y
        If KeyF4Togal = False Then
            If MousePosition.Y >= 690 Then
                ShowPanel2 = True
            Else
                ShowPanel2 = False
            End If
        End If

    End Sub

    Private Sub grdprofit_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdprofit.MouseMove, grddelta.MouseMove, grdgamma.MouseMove, grdvega.MouseMove, grdtheta.MouseMove, grdvolga.MouseMove, grdvanna.MouseMove
        grdprofit_MouseHover(sender, e)
    End Sub

    Private Sub grdStaticCalc_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdStaticCalc.CellContentClick

    End Sub

    Private Sub grdStaticCalc_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdStaticCalc.CellEndEdit
        Call grdStaticCalcCellEndEdit(e.ColumnIndex, e.RowIndex)
    End Sub
    Private Sub grdStaticCalcCellEndEdit(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        If Val(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value & "") <= 0 Then Exit Sub
        If grdStaticCalc.Columns(ColumnIndex).Name = "SpotValue" Then
            'grdStaticCalc.Rows(e.RowIndex).Cells("Percent").Value = Math.Round((grdStaticCalc.Rows(e.RowIndex).Cells("SpotValue").Value * 100 / txtmid) - 100)
            grdStaticCalc.Rows(RowIndex).Cells("Percent").Value = Math.Round(((Val(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) / Val(txtmid)) - 1) * 100, 0)
            'grdStaticCalc.Rows(e.RowIndex).Cells("
        ElseIf grdStaticCalc.Columns(ColumnIndex).Name = "Percent" Then
            grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value = Math.Round((100 + Val(grdStaticCalc.Rows(RowIndex).Cells("Percent").Value)) * txtmid / 100)
        End If

        Dim isCall As Boolean
        Dim totcpr As Double
        Dim bval As Double
        Dim bvol As Double

        Dim mVega As Double
        Dim mD1 As Double
        Dim mD2 As Double
        Dim cd As Date
        Dim SpotValue As Double

        Dim col As New DataGridViewTextBoxColumn
        Select Case UCase(tbcon.SelectedTab.Tag)
            Case "PROFIT"
                'For Each col In grdStaticCalc.Columns
                '    If UCase(col.Name) <> UCase("SpotValue") And UCase(col.Name) <> UCase("Percent") Then
                '        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = 0
                '        For Each grow As DataGridViewRow In grdtrad.Rows
                '            If grow.Cells("Active").Value = False Then Continue For
                '            If CBool(grow.Cells("Active").Value) = True Then
                '                SpotValue = CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) + CDbl(grow.Cells("DifFactor").Value)
                '                If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                '                    If CDbl(grow.Cells("spval").Value) = 0 Then
                '                        MsgBox("Enter Spot Value for Selected Future.")
                '                        grdtrad.Focus()
                '                        'Return False
                '                    End If
                '                    If CDbl(grow.Cells("units").Value) = 0 Then
                '                        MsgBox("Enter Quantity Value for Selected Future.")
                '                        grdtrad.Focus()
                '                        'Return False
                '                    End If
                '                    grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (CDbl(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + ((CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value)))
                '                    'dr(col.Name) = Math.Round(CDbl(dr(col.Name)) + ((CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), RoundGrossMTM)
                '                    'dr(col.Name) = Format(CDbl(dr(col.Name)) + ((CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), GrossMTMstr)

                '                Else
                '                    If grow.Cells("CPF").Value = "C" Then
                '                        isCall = True
                '                    Else
                '                        isCall = False
                '                    End If
                '                    'Comment By Alpesh For Ltp 0 is Allow
                '                    'If CDbl(grow.Cells("ltp").Value) = 0 Then
                '                    '    MsgBox("Enter Ltp Value for Selected Call or Put")
                '                    '    grdtrad.Focus()
                '                    '    Return False
                '                    'End If

                '                    If CDbl(grow.Cells("units").Value) = 0 Then
                '                        MsgBox("Enter Quantity Value for Selected Call or Put.")
                '                        grdtrad.Focus()
                '                        'Return False
                '                    End If
                '                    If CDbl(grow.Cells("spval").Value) = 0 Then
                '                        MsgBox("Enter Spot Value for Selected Call or Put.")
                '                        grdtrad.Focus()
                '                        'Exit Sub
                '                    End If
                '                    If CDbl(grow.Cells("lv").Value) = 0 Then
                '                        'MsgBox("Enter Volatality Value for Selected Call or Put.")
                '                        'grdtrad.Focus()
                '                        'Return False
                '                    End If
                '                    totcpr = 0
                '                    Dim cd As Date
                '                    If (IsDate(col.Name)) Then
                '                        cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                '                    Else
                '                        'cd = CDate(Format(CDate(Mid(col.Name, 8, Len(col.Name))), "MMM/dd/yyyy"))
                '                    End If

                '                    'dcount = DatePart(DateInterval.Day, cd)
                '                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(dtexp.Value.Date))
                '                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                '                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                '                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                '                        _mT1 = 0.5
                '                    End If
                '                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                '                        If (IsDate(col.Name)) Then
                '                            _mT = 0.5
                '                        Else
                '                            _mT = 0.5
                '                        End If

                '                    End If
                '                    If _mT = 0 Then
                '                        _mT = 0.00001
                '                        _mT1 = 0.00001
                '                    Else
                '                        _mT = (_mT) / 365
                '                        _mT1 = (_mT1) / 365
                '                    End If
                '                    If (IsDate(col.Name)) Then
                '                        bval = 0
                '                        bvol = 0
                '                        'bvol = Greeks.Black_Scholes(CDbl(txtmkt.Text), CDbl(grow.Cells(5).Value), Mrateofinterast, 0, (CDbl(grow.Cells(7).Value)), _mT1, iscall, True, 0, 6)
                '                        bvol = CDbl(grow.Cells("lv").Value) / 100
                '                        bval = Greeks.Black_Scholes(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, CDbl(grow.Cells("lv").Value) / 100, _mT, isCall, True, 0, 0)
                '                        totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                '                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value & "") + totcpr)
                '                        'dr(col.Name) = Math.Round(CDbl(dr(col.Name)) + totcpr, RoundGrossMTM)
                '                        ' dr(col.Name) = Format(CDbl(dr(col.Name)) + totcpr,GrossMTMstr)
                '                    Else
                '                        If (isCall = True) Then
                '                            If ((CDbl(SpotValue) - CDbl(grow.Cells("Strike").Value)) > 0) Then
                '                                bval = (CDbl(SpotValue) - CDbl(grow.Cells("Strike").Value))
                '                                totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                '                                grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value & "") + totcpr)
                '                            Else
                '                                bval = 0 ' CDbl(dr(col.Name)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
                '                                totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                '                                grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (CDbl(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr)
                '                            End If
                '                        Else
                '                            If ((CDbl(grow.Cells("Strike").Value) - CDbl(SpotValue)) > 0) Then
                '                                bval = (CDbl(grow.Cells("Strike").Value) - CDbl(SpotValue))
                '                                totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                '                                grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (CDbl(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr)
                '                            Else
                '                                bval = 0 'CDbl(dr(col.Name)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
                '                                totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                '                                grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (CDbl(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr)
                '                            End If
                '                        End If
                '                    End If

                '                End If

                '            End If


                '        Next
                '        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value += grossmtm
                '        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = Math.Round(Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value))
                '        'dcount = dcount + 1
                '    End If
                '    'i = i + 1
                '    'profitarr.Add(dr(col.Name))

                'Next
                For Each col In grdStaticCalc.Columns
                    If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent") Then
                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = 0
                        For Each grow As DataGridViewRow In grdtrad.Rows
                            If grow.Cells("Active").Value = False Then Continue For
                            'Dim cd As Date
                            If (IsDate(col.Name)) Then
                                cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                            Else
                                If UCase(col.Name) = "EXPIRY" Then
                                    cd = dtexp.Value
                                Else
                                    cd = CDate(Format(CDate(Mid(col.Name, 8, Len(col.Name))), "MMM/dd/yyyy"))
                                End If
                            End If
                            If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For

                            If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                                SpotValue = CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) + CDbl(grow.Cells("DifFactor").Value)
                                If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                                    If CDbl(grow.Cells("spval").Value) = 0 Then
                                        'MsgBox("Enter Spot Value for Selected Future.")
                                        'grdtrad.Focus()
                                        'Return False
                                    End If
                                    If CDbl(grow.Cells("units").Value) = 0 Then
                                        'MsgBox("Enter Quantity Value for Selected Future.")
                                        'grdtrad.Focus()
                                        'Return False
                                    End If
                                    'grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (CDbl(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + ((CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value)))
                                    grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (CDbl(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + ((CDbl(SpotValue) - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value)))
                                    'dr(col.Name) = Math.Round(CDbl(dr(col.Name)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), RoundGrossMTM)
                                    'dr(col.Name) = Format(CDbl(dr(col.Name)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), GrossMTMstr)

                                Else
                                    If grow.Cells("CPF").Value = "C" Then
                                        isCall = True
                                    Else
                                        isCall = False
                                    End If
                                    'Comment By Alpesh For Ltp 0 is Allow
                                    'If CDbl(grow.Cells("ltp").Value) = 0 Then
                                    '    MsgBox("Enter Ltp Value for Selected Call or Put")
                                    '    grdtrad.Focus()
                                    '    Return False
                                    'End If

                                    If CDbl(grow.Cells("units").Value) = 0 Then
                                        'MsgBox("Enter Quantity Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False
                                    End If
                                    If CDbl(grow.Cells("spval").Value) = 0 Then
                                        'MsgBox("Enter Spot Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Exit Sub
                                    End If
                                    If CDbl(grow.Cells("lv").Value) = 0 Then
                                        'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False
                                    End If
                                    totcpr = 0
                                    'Dim cd As Date
                                    If (IsDate(col.Name)) Then
                                        cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                    Else
                                        If UCase(col.Name) = "EXPIRY" Then
                                            cd = dtexp.Value
                                        Else
                                            cd = CDate(Format(CDate(Mid(col.Name, 8, Len(col.Name))), "MMM/dd/yyyy"))
                                        End If
                                    End If

                                    'dcount = DatePart(DateInterval.Day, cd)
                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(dtexp.Value.Date))
                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        _mT1 = 0.5
                                    End If
                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                        If (IsDate(col.Name)) Then
                                            _mT = 0.5
                                        Else
                                            _mT = 0.5
                                        End If

                                    End If
                                    If _mT = 0 Then
                                        _mT = 0.00001
                                        _mT1 = 0.00001
                                    Else
                                        _mT = (_mT) / 365
                                        _mT1 = (_mT1) / 365
                                    End If
                                    If (IsDate(col.Name)) Then
                                        bval = 0
                                        bvol = 0
                                        'bvol = Greeks.Black_Scholes(CDbl(txtmkt.Text), CDbl(grow.Cells(5).Value), Mrateofinterast, 0, (CDbl(grow.Cells(7).Value)), _mT1, iscall, True, 0, 6)
                                        bval = Greeks.Black_Scholes(SpotValue, CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value)) / 100, _mT, isCall, True, 0, 0)
                                        totcpr = ((Val(bval & "") - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value & "") + totcpr)
                                        'dr(col.Name) = Math.Round(CDbl(dr(col.Name)) + totcpr, RoundGrossMTM)
                                        ' dr(col.Name) = Format(CDbl(dr(col.Name)) + totcpr,GrossMTMstr)
                                    Else
                                        If (isCall = True) Then
                                            If ((CDbl(SpotValue) - CDbl(grow.Cells("Strike").Value)) > 0) Then
                                                bval = (CDbl(SpotValue) - CDbl(grow.Cells("Strike").Value))
                                                totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                                grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value & "") + totcpr)
                                            Else
                                                bval = 0 ' CDbl(dr(col.Name)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
                                                totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                                grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (CDbl(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr)
                                            End If
                                        Else
                                            If ((CDbl(grow.Cells("Strike").Value) - CDbl(SpotValue)) > 0) Then
                                                bval = (CDbl(grow.Cells("Strike").Value) - CDbl(SpotValue))
                                                totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                                grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (CDbl(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr)
                                            Else
                                                bval = 0 'CDbl(dr(col.Name)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
                                                totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
                                                grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (CDbl(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr)
                                            End If
                                        End If
                                    End If

                                End If

                            End If


                        Next
                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value += grossmtm
                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = Math.Round(Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value))
                        'dcount = dcount + 1
                    End If
                    'i = i + 1
                    'profitarr.Add(dr(col.Name))

                Next
            Case "DELTA"
                For Each col In grdStaticCalc.Columns
                    If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent") And UCase(col.Name) <> UCase("Expiry") Then
                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = 0
                        For Each grow As DataGridViewRow In grdtrad.Rows
                            If grow.Cells("Active").Value = False Then Continue For
                            'Dim cd As Date
                            If (IsDate(col.Name)) Then
                                cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                            Else
                                cd = CDate(Format(CDate(Mid(col.Name, 8, Len(col.Name))), "MMM/dd/yyyy"))

                            End If
                            If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For
                            If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                                SpotValue = CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) + CDbl(grow.Cells("DifFactor").Value)
                                If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                                    If Val(grow.Cells("spval").Value) = 0 Then
                                        'MsgBox("Enter Spot Value for Selected Future.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    If Val(grow.Cells("units").Value) = 0 Then
                                        'MsgBox("Enter Quantity Value for Selected Future.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = Math.Round(Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + Val(grow.Cells("deltaval").Value), DecimalSetting.iRDeltaval)
                                    ' dr(col.Name) = Format(val(dr(col.Name)) + val(grow.cells(11).value), DecimalSetting.sDeltaval)

                                Else
                                    If grow.Cells("CPF").Value = "C" Then
                                        isCall = True
                                    Else
                                        isCall = False
                                    End If
                                    If Val(grow.Cells("last").Value) = 0 Then
                                        'MsgBox("Enter Rate Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    If Val(grow.Cells("units").Value) = 0 Then
                                        'MsgBox("Enter Quantity Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    If Val(grow.Cells("spval").Value) = 0 Then
                                        'MsgBox("Enter Spot Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If

                                    If Val(grow.Cells("lv").Value) = 0 Then
                                        'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If

                                    totcpr = 0
                                    'Dim cd As Date '= CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                    If (IsDate(col.Name)) Then
                                        cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                        Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                        Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT1 = 0.5
                                        End If
                                        If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT = 0.5
                                        End If
                                        If _mT = 0 Then
                                            _mT = 0.00001
                                            _mT1 = 0.00001
                                        Else
                                            _mT = (_mT) / 365
                                            _mT1 = (_mT1) / 365
                                        End If
                                        bval = 0
                                        bvol = 0

                                        'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                        'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, isCall, True, 0, 6)
                                        bvol = Val(grow.Cells("lv").Value) / 100
                                        bval = Greeks.Black_Scholes(Val(SpotValue), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, isCall, True, 0, 1)
                                        totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr).ToString(DecimalSetting.sDeltaval)
                                        ' dr(col.Name) = Format(val(dr(col.Name)) + totcpr, DecimalSetting.sDeltaval)
                                    End If
                                End If
                            End If
                        Next
                        'dcount = dcount + 1
                    End If
                    'i = i + 1
                    'deltaarr.Add(dr(col.Name))
                Next
            Case "GAMMA"
                For Each col In grdStaticCalc.Columns
                    'If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent") Then	 
                     If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent") And UCase(col.Name) <> UCase("Expiry") Then
                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = 0
                        For Each grow As DataGridViewRow In grdtrad.Rows
                            If grow.Cells("Active").Value = False Then Continue For
                            'Dim cd As Date
                            If (IsDate(col.Name)) Then
                                cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                            Else
                                cd = CDate(Format(CDate(Mid(col.Name, 8, Len(col.Name))), "MMM/dd/yyyy"))
                            End If
                            If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For

                            If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                                SpotValue = CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) + CDbl(grow.Cells("DifFactor").Value)
                                If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                    '    'If val(grow.Cells(2).Value) = 0 Then
                                    '    '    MsgBox("Enter Spot Value for Selected Futre")
                                    '    '    grdtrad.Focus()
                                    '    '    Exit Sub
                                    '    'End If
                                    '    'If val(grow.Cells(5).Value) = 0 Then
                                    '    '    MsgBox("Enter Units Value for Selected Futre")
                                    '    '    grdtrad.Focus()
                                    '    '    Exit Sub
                                    '    'End If
                                    '    'dr(col.Name) = Math.Round(val(dr(col.Name)) + val(grow.cells(10).value), 2)

                                    'Else
                                    If grow.Cells("CPF").Value = "C" Then
                                        isCall = True
                                    Else
                                        isCall = False
                                    End If
                                    If Val(grow.Cells("last").Value) = 0 Then
                                        'MsgBox("Enter Rate Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    If Val(grow.Cells("units").Value) = 0 Then
                                        'MsgBox("Enter Quantity Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    If Val(grow.Cells("spval").Value) = 0 Then
                                        'MsgBox("Enter Spot Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False


                                    End If

                                    If Val(grow.Cells("lv").Value) = 0 Then
                                        'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    totcpr = 0
                                    'Dim cd As Date '= CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                    If (IsDate(col.Name)) Then
                                        cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                        Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                        Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT1 = 0.5
                                        End If
                                        If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT = 0.5
                                        End If
                                        If _mT = 0 Then
                                            _mT = 0.00001
                                            _mT1 = 0.00001
                                        Else
                                            _mT = (_mT) / 365
                                            _mT1 = (_mT1) / 365
                                        End If
                                        bval = 0
                                        bvol = 0
                                        'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                        'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, isCall, True, 0, 6)
                                        bvol = Val(grow.Cells("lv").Value) / 100
                                        bval = Greeks.Black_Scholes(Val(SpotValue), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, isCall, True, 0, 2)
                                        totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr).ToString(DecimalSetting.sGammaval)
                                        'dr(col.Name) = Format(val(dr(col.Name)) + totcpr, DecimalSetting.sGammaval)
                                    End If
                                End If
                            End If
                        Next
                        '                        dcount = dcount + 1
                    End If
                    '                   i = i + 1
                    '                  gammaarr.Add(dr(col.Name))
                Next
            Case "VEGA"
                For Each col In grdStaticCalc.Columns
                    'If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent(%)") Then
					 If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent") And UCase(col.Name) <> UCase("Expiry") Then
                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = 0
                        For Each grow As DataGridViewRow In grdtrad.Rows
                            If grow.Cells("Active").Value = False Then Continue For
                            'Dim cd As Date
                            If (IsDate(col.Name)) Then
                                cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                            Else
                                cd = CDate(Format(CDate(Mid(col.Name, 8, Len(col.Name))), "MMM/dd/yyyy"))
                            End If
                            If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For
                            If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                                SpotValue = CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) + CDbl(grow.Cells("DifFactor").Value)
                                If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                    '    'If val(grow.Cells(2).Value) = 0 Then
                                    '    '    MsgBox("Enter Spot Value for Selected Futre")
                                    '    '    grdtrad.Focus()
                                    '    '    Exit Sub
                                    '    'End If
                                    '    'If val(grow.Cells(5).Value) = 0 Then
                                    '    '    MsgBox("Enter Units Value for Selected Futre")
                                    '    '    grdtrad.Focus()
                                    '    '    Exit Sub
                                    '    'End If
                                    '    'dr(col.Name) = Math.Round(val(dr(col.Name)) + val(grow.cells(10).value), 2)

                                    'Else
                                    If grow.Cells("CPF").Value = "C" Then
                                        isCall = True
                                    Else
                                        isCall = False
                                    End If
                                    If Val(grow.Cells("last").Value) = 0 Then
                                        'MsgBox("Enter Rate Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        '                                     Return False

                                    End If
                                    If Val(grow.Cells("units").Value) = 0 Then
                                        'MsgBox("Enter Quantity Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        '                                    Return False

                                    End If
                                    If Val(grow.Cells("spval").Value) = 0 Then
                                        'MsgBox("Enter Spot Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        '                                   Return False

                                    End If

                                    If Val(grow.Cells("lv").Value) = 0 Then
                                        'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        '                                  Return False

                                    End If
                                    totcpr = 0
                                    'Dim cd As Date '= CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                    If (IsDate(col.Name)) Then
                                        cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                        Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                        Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT1 = 0.5
                                        End If
                                        If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT = 0.5
                                        End If
                                        If _mT = 0 Then
                                            _mT = 0.00001
                                            _mT1 = 0.00001
                                        Else
                                            _mT = (_mT) / 365
                                            _mT1 = (_mT1) / 365
                                        End If
                                        bval = 0
                                        bvol = 0
                                        'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                        'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, isCall, True, 0, 6)
                                        bvol = Val(grow.Cells("lv").Value) / 100
                                        bval = Greeks.Black_Scholes(Val(SpotValue), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, isCall, True, 0, 3)
                                        totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr).ToString(DecimalSetting.sVega)
                                        'dr(col.Name) = Format(val(dr(col.Name)) + totcpr, DecimalSetting.sVegaval)
                                    End If
                                End If
                            End If
                        Next
                        'dcount = dcount + 1
                    End If
                    'i = i + 1
                    'vegaarr.Add(dr(col.Name))
                Next
            Case "THETA"
                For Each col In grdStaticCalc.Columns
                    'If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent(%)") Then
                     If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent") And UCase(col.Name) <> UCase("Expiry") Then   
                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = 0
                        For Each grow As DataGridViewRow In grdtrad.Rows
                            If grow.Cells("Active").Value = False Then Continue For
                            'Dim cd As Date
                            If (IsDate(col.Name)) Then
                                cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                            Else
                                cd = CDate(Format(CDate(Mid(col.Name, 8, Len(col.Name))), "MMM/dd/yyyy"))
                            End If
                            If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For
                            If CBool(grow.Cells("Active").Value) = True  And Val(grow.Cells("units").Value) <> 0 Then
                                SpotValue = CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) + CDbl(grow.Cells("DifFactor").Value)
                                If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                    '    'If val(grow.Cells(2).Value) = 0 Then
                                    '    '    MsgBox("Enter Spot Value for Selected Future")
                                    '    '    grdtrad.Focus()
                                    '    '    Exit Sub
                                    '    'End If
                                    '    'If val(grow.Cells(5).Value) = 0 Then
                                    '    '    MsgBox("Enter Units Value for Selected Futre")
                                    '    '    grdtrad.Focus()
                                    '    '    Exit Sub
                                    '    'End If
                                    '    'dr(col.Name) = Math.Round(val(dr(col.Name)) + val(grow.cells(10).value), 2)

                                    'Else
                                    If grow.Cells("CPF").Value = "C" Then
                                        isCall = True
                                    Else
                                        isCall = False
                                    End If
                                    If Val(grow.Cells("last").Value) = 0 Then
                                        'MsgBox("Enter Rate Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        '                                      Return False
                                        '
                                    End If
                                    If Val(grow.Cells("units").Value) = 0 Then
                                        'MsgBox("Enter Quantity Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        '                                       Return False
                                        '
                                    End If
                                    If Val(grow.Cells("spval").Value) = 0 Then
                                        'MsgBox("Enter Spot Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If

                                    If Val(grow.Cells("lv").Value) = 0 Then
                                        'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    totcpr = 0
                                    'Dim cd As Date '= CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                    If (IsDate(col.Name)) Then
                                        cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                        Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                        Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT1 = 0.5
                                        End If
                                        If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT = 0.5
                                        End If
                                        If _mT = 0 Then
                                            _mT = 0.00001
                                            _mT1 = 0.00001
                                        Else
                                            _mT = (_mT) / 365
                                            _mT1 = (_mT1) / 365
                                        End If
                                        bval = 0
                                        bvol = 0
                                        'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                        'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, isCall, True, 0, 6)
                                        bvol = Val(grow.Cells("lv").Value) / 100
                                        bval = Greeks.Black_Scholes(Val(SpotValue), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, isCall, True, 0, 4)
                                        totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr).ToString(DecimalSetting.sThetaval)
                                        'dr(col.Name) = Format(val(dr(col.Name)) + totcpr, DecimalSetting.sThetaval)
                                    End If
                                End If
                            End If
                        Next
                        '                        dcount = dcount + 1
                    End If
                    '                   i = i + 1
                    '                  thetaarr.Add(dr(col.Name))
                Next
            Case "VOLGA"
                For Each col In grdStaticCalc.Columns
                    'If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent") Then
					 If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent") And UCase(col.Name) <> UCase("Expiry") Then
                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = 0
                        For Each grow As DataGridViewRow In grdtrad.Rows
                            If grow.Cells("Active").Value = False Then Continue For
                            'Dim cd As Date
                            If (IsDate(col.Name)) Then
                                cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                            Else
                                cd = CDate(Format(CDate(Mid(col.Name, 8, Len(col.Name))), "MMM/dd/yyyy"))
                            End If
                            If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For
                            If CBool(grow.Cells("Active").Value) = True  And Val(grow.Cells("units").Value) <> 0 Then
                                SpotValue = CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) + CDbl(grow.Cells("DifFactor").Value)
                                If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                    '    'If val(grow.Cells(2).Value) = 0 Then
                                    '    '    MsgBox("Enter Spot Value for Selected Future")
                                    '    '    grdtrad.Focus()
                                    '    '    Exit Sub
                                    '    'End If
                                    '    'If val(grow.Cells(5).Value) = 0 Then
                                    '    '    MsgBox("Enter Units Value for Selected Futre")
                                    '    '    grdtrad.Focus()
                                    '    '    Exit Sub
                                    '    'End If
                                    '    'dr(col.Name) = Math.Round(val(dr(col.Name)) + val(grow.cells(10).value), 2)

                                    'Else
                                    If grow.Cells("CPF").Value = "C" Then
                                        isCall = True
                                    Else
                                        isCall = False
                                    End If
                                    If Val(grow.Cells("last").Value) = 0 Then
                                        'MsgBox("Enter Rate Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        '                                     Return False

                                    End If
                                    If Val(grow.Cells("units").Value) = 0 Then
                                        'MsgBox("Enter Quantity Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    If Val(grow.Cells("spval").Value) = 0 Then
                                        'MsgBox("Enter Spot Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If

                                    If Val(grow.Cells("lv").Value) = 0 Then
                                        'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        '    Return False

                                    End If
                                    totcpr = 0
                                    'Dim cd As Date '= CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                    If (IsDate(col.Name)) Then
                                        cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                        Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                        Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT1 = 0.5
                                        End If
                                        If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT = 0.5
                                        End If
                                        If _mT = 0 Then
                                            _mT = 0.00001
                                            _mT1 = 0.00001
                                        Else
                                            _mT = (_mT) / 365
                                            _mT1 = (_mT1) / 365
                                        End If
                                        bval = 0
                                        bvol = 0

                                        'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                        'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, isCall, True, 0, 6)
                                        bvol = Val(grow.Cells("lv").Value) / 100
                                        'mVega = Greeks.Black_Scholes(Val(SpotValue), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, isCall, True, 0, 3)
                                        'mD1 = CalD1(Val(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
                                        'mD2 = CalD2(Val(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
                                        'bval = CalVolga(mVega, mD1, mD2, bvol)
                                        bval = Greeks.Black_Scholes(Val(SpotValue), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, isCall, True, 0, 8)
                                        totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr).ToString(DecimalSetting.sVolga)
                                        'dr(col.Name) = Format(val(dr(col.Name)) + totcpr, DecimalSetting.sVolgaval)
                                    End If
                                End If
                            End If
                        Next
                        '                        dcount = dcount + 1
                    End If
                    '                   i = i + 1
                    '                  volgaarr.Add(dr(col.Name))
                    'If volgaarr(3) = "NaN" Then MsgBox("A")
                Next
            Case "VANNA"
                For Each col In grdStaticCalc.Columns
                    'If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent") Then
					 If UCase(col.Name) <> UCase("spotvalue") And UCase(col.Name) <> UCase("Percent") And UCase(col.Name) <> UCase("Expiry") Then
                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = 0
                        For Each grow As DataGridViewRow In grdtrad.Rows
                            If grow.Cells("Active").Value = False Then Continue For
                            'Dim cd As Date
                            If (IsDate(col.Name)) Then
                                cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                            Else
                                cd = CDate(Format(CDate(Mid(col.Name, 8, Len(col.Name))), "MMM/dd/yyyy"))
                            End If
                            If DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date) < 0 Then Continue For
                            If CBool(grow.Cells("Active").Value) = True And Val(grow.Cells("units").Value) <> 0 Then
                                SpotValue = CDbl(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value) + CDbl(grow.Cells("DifFactor").Value)
                                If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
                                    '    'If val(grow.Cells(2).Value) = 0 Then
                                    '    '    MsgBox("Enter Spot Value for Selected Future")
                                    '    '    grdtrad.Focus()
                                    '    '    Exit Sub
                                    '    'End If
                                    '    'If val(grow.Cells(5).Value) = 0 Then
                                    '    '    MsgBox("Enter Units Value for Selected Futre")
                                    '    '    grdtrad.Focus()
                                    '    '    Exit Sub
                                    '    'End If
                                    '    'dr(col.Name) = Math.Round(val(dr(col.Name)) + val(grow.cells(10).value), 2)

                                    'Else
                                    If grow.Cells("CPF").Value = "C" Then
                                        isCall = True
                                    Else
                                        isCall = False
                                    End If
                                    If Val(grow.Cells("last").Value) = 0 Then
                                        'MsgBox("Enter Rate Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    If Val(grow.Cells("units").Value) = 0 Then
                                        'MsgBox("Enter Quantity Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    If Val(grow.Cells("spval").Value) = 0 Then
                                        'MsgBox("Enter Spot Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If

                                    If Val(grow.Cells("lv").Value) = 0 Then
                                        'MsgBox("Enter Volatality Value for Selected Call or Put.")
                                        'grdtrad.Focus()
                                        'Return False

                                    End If
                                    totcpr = 0
                                    'Dim cd As Date '= CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                    If (IsDate(col.Name)) Then
                                        cd = CDate(Format(CDate(col.Name), "MMM/dd/yyyy"))
                                        Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
                                        Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
                                        If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT1 = 0.5
                                        End If
                                        If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
                                            _mT = 0.5
                                        End If
                                        If _mT = 0 Then
                                            _mT = 0.00001
                                            _mT1 = 0.00001
                                        Else
                                            _mT = (_mT) / 365
                                            _mT1 = (_mT1) / 365
                                        End If
                                        bval = 0
                                        bvol = 0

                                        'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                        'bvol = Greeks.Black_Scholes(Val(TxtMStrike.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, isCall, True, 0, 6)
                                        bvol = Val(grow.Cells("lv").Value) / 100
                                        'mVega = Greeks.Black_Scholes(Val(SpotValue), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, isCall, True, 0, 3)
                                        'mD1 = CalD1(Val(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
                                        'mD2 = CalD2(Val(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
                                        'bval = CalVanna(Val(grdStaticCalc.Rows(RowIndex).Cells("SpotValue").Value), mVega, mD1, mD2, bvol, _mT)
                                        bval = Greeks.Black_Scholes(Val(SpotValue), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, isCall, True, 0, 7)

                                        'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
                                        'bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 4)

                                        totcpr = Val(bval & "") * Val(grow.Cells("units").Value)
                                        grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value = (Val(grdStaticCalc.Rows(RowIndex).Cells(col.Name).Value) + totcpr).ToString(DecimalSetting.sVannaval)
                                        'dr(col.Name) = Format(val(dr(col.Name)) + totcpr, DecimalSetting.sVannaval)
                                    End If
                                End If
                            End If
                        Next
                        'dcount = dcount + 1
                    End If
                    'i = i + 1
                    'vannaarr.Add(dr(col.Name))
                Next
        End Select
    End Sub

    Private Sub grdStaticCalc_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles grdStaticCalc.Scroll
        Try
            If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
                grdprofit.HorizontalScrollingOffset = e.NewValue
                grddelta.HorizontalScrollingOffset = e.NewValue
                grdgamma.HorizontalScrollingOffset = e.NewValue
                grdvega.HorizontalScrollingOffset = e.NewValue
                grdtheta.HorizontalScrollingOffset = e.NewValue
                grdvolga.HorizontalScrollingOffset = e.NewValue
                grdvanna.HorizontalScrollingOffset = e.NewValue
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdprofit_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles grdprofit.Scroll, grddelta.Scroll, grdgamma.Scroll, grdvega.Scroll, grdtheta.Scroll, grdvolga.Scroll, grdvanna.Scroll
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            grdStaticCalc.HorizontalScrollingOffset = e.NewValue
        End If
    End Sub

    Private Sub grdact_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdact.CellMouseUp
        If bCheckAll = False Then
            grdact.EndEdit()
            Call ButResult_Click(sender, e)
        End If
    End Sub


    Private Sub grdact_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdact.CellValueChanged
        If bCheckAll = False Then
            Call ButResult_Click(sender, e)
        End If
    End Sub


    Private Sub HideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HideToolStripMenuItem.Click
        If CType(sender, ToolStripMenuItem).Checked = True Then
            CType(sender, ToolStripMenuItem).Checked = False
            'Call BtnFixPnl2_Click(sender, e)
        Else
            CType(sender, ToolStripMenuItem).Checked = True
            'Call BtnFixPnl2_Click(sender, e)
        End If

        If ShowPanel2 = False Then
            ShowPanel2 = True
            KeyF4Togal = True
            'HideToolStripMenuItem.Checked = True
        Else
            KeyF4Togal = False
            BtnFixPnl2_Click(sender, e)
            ShowPanel2 = False
            'HideToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub AutoHideToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoHideToolStripMenuItem.Click
        If CType(sender, ToolStripMenuItem).Checked = True Then
            CType(sender, ToolStripMenuItem).Checked = False
            '    Call BtnFix_Click(sender, e)
        Else
            CType(sender, ToolStripMenuItem).Checked = True
            '    Call BtnFix_Click(sender, e)
        End If


        If ShowPanel1 = False Then
            ShowPanel1 = True
            KeyF3Togal = True
            'AutoHideToolStripMenuItem.Checked = True
        Else
            KeyF3Togal = False
            BtnFix_Click(sender, e)
            ShowPanel1 = False
            'AutoHideToolStripMenuItem.Checked = False
        End If
    End Sub


    Private Sub grdact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdact.Click
        'If bCheckAll = False Then
        '    grdact.RefreshEdit()
        '    Call ButResult_Click(sender, e)
        'End If
    End Sub

    Private Sub DecimalSettingToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecimalSettingToolStripMenuItem.Click
        DecimalSetting.ShowForm()

        'DecimalSetting.sDeltaval = Deltastr
        'DecimalSetting.sDeltaval = Deltastr_Val
        'DecimalSetting.sGamma = Gammastr
        'DecimalSetting.sGammaval = Gammastr_Val
        'DecimalSetting.sVega = Vegastr
        'DecimalSetting.sVegaval = Vegastr_Val
        'DecimalSetting.sTheta = Thetastr
        'DecimalSetting.sThetaval = Thetastr_Val
        'DecimalSetting.sVolga = Volgastr
        'DecimalSetting.sVolgaval = Volgastr_Val
        'DecimalSetting.sVanna = Vannastr
        'DecimalSetting.sVannaval = Vannastr_Val
    End Sub

    Private Sub Label11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label11.Click, Label2.Click

    End Sub
	
Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TabLPanelSpotDiff.Visible = False

    End Sub

    Private Sub TxtNextToFar_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNextToFar.KeyPress
        numonly(e)
        If Asc(e.KeyChar) = 13 Then
            Call GetSpotDiff(True)
        End If
    End Sub

    Private Sub TxtNextToFar_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNextToFar.Leave
        GetSpotDiff(True)
    End Sub
	
    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNextToFar.TextChanged

    End Sub



    Private Sub GetSpotDiff(ByVal bSet As Boolean)
        Dim CurrFutPrice As Double
        Dim NextFutPrice As Double
        Dim FarFutPrice As Double

        Dim CurrMonth As Integer = Month(Date.Now)
        Dim NextMonth As Integer = CurrMonth + 1
        Dim FarMonth As Integer = NextMonth + 1

        'Dim dt As Date
        ''Dim CurrMonth As Integer
        'dt = Date.Now
        'CurrMonth = Month(dt)

        ''Move to the end of the month
        'While (Month(dt) = CurrMonth)
        '    dt = dt.AddDays(1)
        'End While
        'dt = DateSerial(dt.Year, dt.Month, dt.Day - 1)

        ''Find the last thursday
        'While (Weekday(dt) <> vbThursday)
        '    dt = DateSerial(dt.Year, dt.Month, dt.Day - 1)
        'End While
        Try
            grdtrad.Sort(grdtrad.Columns("TimeII"), System.ComponentModel.ListSortDirection.Ascending)
        Catch ex As Exception
            MsgBox("Invalida Date inserted")
            Exit Sub
        End Try

        For Each grow As DataGridViewRow In grdtrad.Rows
            If grow.Cells("Active").Value = True Then
                If CDate(grow.Cells("TimeII").Value).Month = CurrMonth Then
                    CurrFutPrice = grow.Cells("spval").Value
                ElseIf CDate(grow.Cells("TimeII").Value).Month = NextMonth Then
                    NextFutPrice = grow.Cells("spval").Value
                ElseIf CDate(grow.Cells("TimeII").Value).Month = FarMonth Then
                    FarFutPrice = grow.Cells("spval").Value
                End If
            End If
        Next

        If bSet = False Then
            If CurrFutPrice <> 0 Then
                TxtCurrToNext.Text = Math.Round((NextFutPrice - CurrFutPrice), 4)
            Else
                TxtCurrToNext.Text = "0"
            End If
            If NextFutPrice <> 0 Then
                If (FarFutPrice - NextFutPrice) > 0 Then
                    TxtNextToFar.Text = Math.Round((FarFutPrice - NextFutPrice), 4)
                End If
            Else
                TxtNextToFar.Text = "0"
            End If

            For Each grow As DataGridViewRow In grdtrad.Rows
                If grow.Cells("Active").Value = True Then
                    If CDate(grow.Cells("TimeII").Value).Month = CurrMonth Then
                        grow.Cells("DifFactor").Value = 0
                    ElseIf CDate(grow.Cells("TimeII").Value).Month = NextMonth Then
                        grow.Cells("DifFactor").Value = Val(TxtCurrToNext.Text)
                    ElseIf CDate(grow.Cells("TimeII").Value).Month = FarMonth Then
                        grow.Cells("DifFactor").Value = Val(TxtNextToFar.Text)
                    End If
                End If
            Next
        Else
            'If Val(TxtCurrToNext.Text) = 0 Then
            '    TxtCurrToNext.Text = Math.Round((NextFutPrice - CurrFutPrice), 4)
            'End If
            'If Val(TxtNextToFar.Text) = 0 Then
            '    If (FarFutPrice - NextFutPrice) > 0 Then
            '        TxtNextToFar.Text = Math.Round((FarFutPrice - NextFutPrice), 4)
            '    End If
            'End If
            For Each grow As DataGridViewRow In grdtrad.Rows
                If grow.Cells("Active").Value = True Then
                    If CDate(grow.Cells("TimeII").Value).Month = CurrMonth Then
                        grow.Cells("DifFactor").Value = 0
                    ElseIf CDate(grow.Cells("TimeII").Value).Month = NextMonth Then
                        grow.Cells("DifFactor").Value = Val(TxtCurrToNext.Text)
                    ElseIf CDate(grow.Cells("TimeII").Value).Month = FarMonth Then
                        grow.Cells("DifFactor").Value = Val(TxtNextToFar.Text)
                    End If
                End If
            Next

            'txtmkt_Leave()
            If Val(txtmkt.Text) > 0 Then
                If Val(txtinterval.Text) = 0 Then
                    Call CalInterval()
                End If
                'txtmid = Math.Floor(Val(txtmkt.Text))
                ''If Val(txtmid) < 100 Then
                ''    interval = 1
                ''ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
                ''    interval = 5
                ''ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
                ''    interval = 10
                ''ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
                ''    interval = 100
                ''ElseIf Val(txtmid) > 10000 Then
                ''    interval = 500
                ''End If
                'If flgUnderline = False Then Exit Sub
                If grdtrad.Rows.Count > 0 Then
                    For i As Integer = 0 To grdtrad.Rows.Count - 2
                        Dim grow As DataGridViewRow
                        grow = grdtrad.Rows(i)
                        grow.Cells("spval").Value = CDbl(txtmkt.Text) + grow.Cells("DifFactor").Value

                        If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
                            'If IsDBNull(grow.Cells(4).Value) Or grow.Cells(4).Value Is Nothing Or CStr(grow.Cells(4).Value) = "" Then
                            grow.Cells("last").Value = grow.Cells("spval").Value 'CDbl(txtmkt.Text)
                            'End If
                        End If
                        cal(7, grow, True)
                    Next
                    grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("spval").Value = CDbl(txtmkt.Text) + grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("DifFactor").Value
                    grdtrad.EndEdit()
                    ' result()
                End If
            End If
            Call cal_summary()
            flgUnderline = False
            '=============

        End If
    End Sub

    Private Sub InvertToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InvertToolStripMenuItem.Click
        If tbcon.SelectedTab.BackColor <> Color.White Then
            tbcon.SelectedTab.BackColor = Color.White
            Select Case UCase(tbcon.SelectedTab.Name)
                Case "TBPROFITCHART"
                    SetBackCol(chtprofit, 0, 0, 0)
                Case "TBCHDELTA"
                    SetBackCol(chtdelta, 0, 0, 0)
                Case "TBCHGAMMA"
                    SetBackCol(chtgamma, 0, 0, 0)
                Case "TBCHVEGA"
                    SetBackCol(chtvega, 0, 0, 0)
                Case "TBCHTHETA"
                    SetBackCol(chttheta, 0, 0, 0)
                Case "TBCHVOLGA"
                    SetBackCol(chtvolga, 0, 0, 0)
                Case "TBCHVANNA"
                    SetBackCol(chtvanna, 0, 0, 0)
            End Select
        Else
            tbcon.SelectedTab.BackColor = Color.Black
            Select Case UCase(tbcon.SelectedTab.Name)
                Case "TBPROFITCHART"
                    SetBackCol(chtprofit, 255, 255, 255)
                Case "TBCHDELTA"
                    SetBackCol(chtdelta, 255, 255, 255)
                Case "TBCHGAMMA"
                    SetBackCol(chtgamma, 255, 255, 255)
                Case "TBCHVEGA"
                    SetBackCol(chtvega, 255, 255, 255)
                Case "TBCHTHETA"
                    SetBackCol(chttheta, 255, 255, 255)
                Case "TBCHVOLGA"
                    SetBackCol(chtvolga, 255, 255, 255)
                Case "TBCHVANNA"
                    SetBackCol(chtvanna, 255, 255, 255)
            End Select
        End If
    End Sub
    Private Sub SetBackCol(ByVal cht As AxMSChart20Lib.AxMSChart, ByVal r As Integer, ByVal g As Integer, ByVal b As Integer)
        cht.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).Labels.Item(1).VtFont.VtColor.Set(r, g, b)
        cht.Plot.Axis(VtChAxisId.VtChAxisIdX, 1).Labels.Item(1).VtFont.VtColor.Set(r, g, b)
        cht.Plot.Axis(VtChAxisId.VtChAxisIdY2, 1).Labels.Item(1).VtFont.VtColor.Set(r, g, b)
        cht.Plot.Axis(VtChAxisId.VtChAxisIdZ, 1).Labels.Item(1).VtFont.VtColor.Set(r, g, b)

        cht.Plot.Axis(VtChAxisId.VtChAxisIdX).Pen.VtColor.Set(r, g, b)
        cht.Plot.Axis(VtChAxisId.VtChAxisIdY).Pen.VtColor.Set(r, g, b)
        cht.Plot.Axis(VtChAxisId.VtChAxisIdZ).Pen.VtColor.Set(r, g, b)
        cht.Plot.Axis(VtChAxisId.VtChAxisIdY2).Pen.VtColor.Set(r, g, b)

        cht.Plot.Axis(VtChAxisId.VtChAxisIdX).AxisTitle.VtFont.VtColor.Set(r, g, b)
        cht.Plot.Axis(VtChAxisId.VtChAxisIdY).AxisTitle.VtFont.VtColor.Set(r, g, b)
        cht.Plot.Axis(VtChAxisId.VtChAxisIdZ).AxisTitle.VtFont.VtColor.Set(r, g, b)
        cht.Plot.Axis(VtChAxisId.VtChAxisIdY2).AxisTitle.VtFont.VtColor.Set(r, g, b)
    End Sub
    Private Sub chtprofit_MouseDownEvent(ByVal sender As System.Object, ByVal e As AxMSChart20Lib._DMSChartEvents_MouseDownEvent) Handles chtprofit.MouseDownEvent, chtdelta.MouseDownEvent, chtgamma.MouseDownEvent, chtvega.MouseDownEvent, chttheta.MouseDownEvent, chtvolga.MouseDownEvent, chtvanna.MouseDownEvent
        If e.button = 2 Then
            ContextMenuChart.Show(MousePosition.X, MousePosition.Y)
            'For SelIndex As Int16 = 0 To tbcon.TabPages.Count
            '    tbcon.SelectedIndex = SelIndex
            '    Select Case UCase(tbcon.SelectedTab.Name)
            '        Case "TBPROFITCHART"
            '            If mAllCV = "Charts" Then
            '                'SetChartGridForAll(chtprofit, grdprofit, lblprofit, True)

            '                chtprofit.Backdrop.Fill.Brush.FillColor.Set(255, 255, 255)
            '            Else
            '                'SetChartGridForAll(chtprofit, grdprofit, lblprofit, False)
            '            End If
            '        Case "TBCHDELTA"
            '            If mAllCV = "Charts" Then
            '                'SetChartGridForAll(chtdelta, grddelta, lbldelta, True)
            '                chtdelta.Backdrop.Fill.Brush.FillColor.Set(255, 255, 255)
            '            Else
            '                'SetChartGridForAll(chtdelta, grddelta, lbldelta, False)
            '            End If
            '        Case "TBCHGAMMA"
            '            If mAllCV = "Charts" Then
            '                'SetChartGridForAll(chtgamma, grdgamma, lblgamma, True)
            '                chtgamma.Backdrop.Fill.Brush.FillColor.Set(255, 255, 255)
            '            Else
            '                'SetChartGridForAll(chtgamma, grdgamma, lblgamma, False)
            '            End If
            '        Case "TBCHVEGA"
            '            If mAllCV = "Charts" Then
            '                'SetChartGridForAll(chtvega, grdvega, lblvega, True)
            '                chtvega.Backdrop.Fill.Brush.FillColor.Set(255, 255, 255)
            '            Else
            '                'SetChartGridForAll(chtvega, grdvega, lblvega, False)
            '            End If
            '        Case "TBCHTHETA"
            '            If mAllCV = "Charts" Then
            '                'SetChartGridForAll(chttheta, grdtheta, lbltheta, True)
            '                chttheta.Backdrop.Fill.Brush.FillColor.Set(255, 255, 255)
            '            Else
            '                'SetChartGridForAll(chttheta, grdtheta, lbltheta, False)
            '            End If
            '        Case "TBCHVOLGA"
            '            If mAllCV = "Charts" Then
            '                'SetChartGridForAll(chtvolga, grdvolga, lblvolga, True)
            '                chtvolga.Backdrop.Fill.Brush.FillColor.Set(255, 255, 255)
            '            Else
            '                'SetChartGridForAll(chtvolga, grdvolga, lblvolga, False)
            '            End If
            '        Case "TBCHVANNA"
            '            If mAllCV = "Charts" Then
            '                'SetChartGridForAll(chtvanna, grdvanna, lblvanna, True)
            '                chtvanna.Backdrop.Fill.Brush.FillColor.Set(255, 255, 255)
            '            Else
            '                'SetChartGridForAll(chtvanna, grdvanna, lblvanna, False)
            '            End If
            '    End Select
            'Next
        End If
    End Sub

    Private Sub chtprofit_ChartSelected(ByVal sender As System.Object, ByVal e As AxMSChart20Lib._DMSChartEvents_ChartSelectedEvent) Handles chtprofit.ChartSelected

    End Sub

    Private Sub FitToSizeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FitToSizeToolStripMenuItem.Click
        grdtrad.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)
    End Sub

    Private Sub grdvanna_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvanna.CellContentClick

    End Sub

    Private Sub grdprofit_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdprofit.CellContentClick

    End Sub
    'Private Sub grdprofit_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdprofit.MouseMove, grddelta.MouseMove, grdgamma.MouseMove, grdvega.MouseMove, grdtheta.MouseMove, grdvolga.MouseMove, grdvanna.MouseMove
    '    grdprofit_MouseHover(sender, e)
    'End Sub
    'Private Sub grdprofit_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ' Me.Text = "M:" & MousePosition.Y & " Pnl:" & TabLPanel2.Location.Y
    '    If KeyF4Togal = False Then
    '        If MousePosition.Y >= 690 Then
    '            ShowPanel2 = True
    '        Else
    '            ShowPanel2 = False
    '        End If
    '    End If
    'End Sub

    Private Sub grdtrad_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdtrad.CellContentClick

    End Sub

    Private Sub TxtCurrToNext_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCurrToNext.KeyPress
        numonly(e)
        If Asc(e.KeyChar) = 13 Then
            Call GetSpotDiff(True)
        End If
    End Sub

    Private Sub TxtCurrToNext_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCurrToNext.Leave
        GetSpotDiff(True)
    End Sub

    Private Sub TxtCurrToNext_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtCurrToNext.TextChanged

    End Sub

    Private Sub TabLPanelSpotDiff_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabLPanelSpotDiff.MouseClick
        tleft = TabLPanelSpotDiff.Left
        tTop = TabLPanelSpotDiff.Top
    End Sub

    Private Sub TabLPanelSpotDiff_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabLPanelSpotDiff.MouseDown, Label2.MouseDown
        'MsgBox(e.Clicks)
        FrmMove = True
        'TabLPanelSpotDiff.Left = tleft
        DragX = e.X
        DragY = e.Y
    End Sub

    Private Sub TabLPanelSpotDiff_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabLPanelSpotDiff.MouseMove, Label2.MouseMove
        'If MouseDnFlg = True Then
        '    TabLPanelSpotDiff.Left = e.X
        '    TabLPanelSpotDiff.Top = e.Y
        'End If

        Dim nx, ny
        If FrmMove Then
            nx = TabLPanelSpotDiff.Left + e.X - DragX
            ny = TabLPanelSpotDiff.Top + e.Y - DragY
            TabLPanelSpotDiff.Left = nx
            TabLPanelSpotDiff.Top = ny
        End If
    End Sub

    Private Sub TabLPanelSpotDiff_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TabLPanelSpotDiff.MouseUp, Label2.MouseUp
        Dim nx, ny
        nx = TabLPanelSpotDiff.Left + e.X - DragX
        ny = TabLPanelSpotDiff.Top + e.Y - DragY
        TabLPanelSpotDiff.Left = nx
        TabLPanelSpotDiff.Top = ny
        FrmMove = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        TabLPanelSpotDiff.Hide()
    End Sub

    Private Sub dtexp_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtexp.ValueChanged
        isStartEndDateChange = True
        'txtdays.Text = 0
        'If grdtrad.Rows.Count = 2 Then
        '    grdtrad.Rows(0).Cells("TimeI").Value = dttoday.Value
        'ElseIf frmFlg = True Then 'this flg used bcoz first need not to assign expiry date
        '    For Each grow As DataGridViewRow In grdtrad.Rows
        '        If grow.Cells("TimeI").ReadOnly = False Then
        '            grow.Cells("TimeI").Value = dttoday.Value
        '        End If
        '    Next
        'End If

        'If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
        '    txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        'End If
        'If dttoday.Value.Date < dtexp.Value.Date Then
        '    REM This code commented because of Day difference plus 1 days display
        '    'txtdays.Text = Val(txtdays.Text) + 1
        'ElseIf dttoday.Value.Date = dtexp.Value.Date Then
        '    txtdays.Text = Val(txtdays.Text) + 0.5
        'End If

        'For Each grow As DataGridViewRow In grdtrad.Rows
        '    If UCase(grow.Cells("CPF").Value) = "F" Or UCase(grow.Cells("CPF").Value) = "E" Then
        '        If Val(grow.Cells("last").Value) <> 0 Then
        '            cal(8, grow)
        '        End If
        '    Else
        '        If Val(grow.Cells("Strike").Value) > 0 Then
        '            cal(8, grow)
        '        End If
        '    End If
        'Next

        txtdays.Text = 0
        If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        If dttoday.Value.Date < dtexp.Value.Date Then
            REM This code commented because of Day difference plus 1 days display
            'txtdays.Text = Val(txtdays.Text) + 1
        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
            txtdays.Text = Val(txtdays.Text) + 0.5
        End If
        For Each grow As DataGridViewRow In grdtrad.Rows
            If UCase(grow.Cells("CPF").Value) = "F" Or UCase(grow.Cells("CPF").Value) = "E" Then
                If Val(grow.Cells("last").Value) <> 0 Then
                    cal(8, grow, True)
                End If
            Else
                If Val(grow.Cells("Strike").Value) > 0 Then
                    cal(8, grow, True)
                End If
            End If
        Next
        frmFlg = False
        create_active()
        Call cal_summary()
    End Sub

    Private Sub ResetToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ResetToolStripMenuItem.Click
        Try

        
            'For Each grow As DataGridViewRow In grdtrad.Rows
            grdtrad.Rows.Clear()
            'Next
            txtmkt.Text = ""
            txtcvol.Text = ""
            txtpvol.Text = ""
            txtvol.Text = ""
            dttoday.Value = Date.Now
            dtexp.Value = Date.Now
            TxtMStrike.Text = ""

            create_active()
            Call ButResult_Click(sender, e)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub dttoday_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dttoday.ValueChanged
        isStartEndDateChange = True
        txtdays.Text = 0
        If grdtrad.Rows.Count = 2 Then
            grdtrad.Rows(0).Cells("TimeI").Value = dttoday.Value
        ElseIf frmFlg = True Then 'this flg used bcoz first need not to assign expiry date
            For Each grow As DataGridViewRow In grdtrad.Rows
                If grow.Cells("TimeI").ReadOnly = False Then
                    grow.Cells("TimeI").Value = dttoday.Value
                End If
            Next
        End If

        If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
        End If
        If dttoday.Value.Date < dtexp.Value.Date Then
            REM This code commented because of Day difference plus 1 days display
            'txtdays.Text = Val(txtdays.Text) + 1
        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
            txtdays.Text = Val(txtdays.Text) + 0.5
        End If

        For Each grow As DataGridViewRow In grdtrad.Rows
            If UCase(grow.Cells("CPF").Value) = "F" Or UCase(grow.Cells("CPF").Value) = "E" Then
                If Val(grow.Cells("last").Value) <> 0 Then
                    cal(8, grow, True)
                End If
            Else
                If Val(grow.Cells("Strike").Value) > 0 Then
                    cal(8, grow, True)
                End If
            End If
        Next

        frmFlg = False
        create_active()

        Call cal_summary()
    End Sub

    Private Sub TabLPanel1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabLPanel1.Leave
        'Call txtmkt_Leave(sender, e)
        Call txtcvol_Leave(sender, e)
        Call txtpvol_Leave(sender, e)
        If isStartEndDateChange = True Then
            isStartEndDateChange = False
            Call dttoday_Leave(sender, e)
            Call dtexp_Leave(sender, e)
        End If
        Call txtinterval_Leave(sender, e)
        Call txtllimit_Leave(sender, e)
        Call TxtMStrike_Leave(sender, e)
    End Sub

    
    Private Sub SetSpotDifferenceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetSpotDifferenceToolStripMenuItem.Click
        TabLPanelSpotDiff.Visible = True
    End Sub

    Private Sub TabLPanelSpotDiff_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TabLPanelSpotDiff.Paint

    End Sub
End Class
' ''Public Class scenario1
' ''    Public objProfitLossChart As New frmprofitLossChart
' ''    Public VarIsCurrency As Boolean = False

' ''    Dim VarIsFrmLoad As Boolean = False
' ''    Dim VarIsFirstLoad As Boolean = False
' ''    Dim FirstTime As Boolean = True
' ''    Dim mAllCV As String = ""
' ''    Dim FirstDgt As Integer
' ''#Region "Variable"
' ''    Dim XResolution As Integer
' ''    Public Shared cellno As Integer
' ''    Public isVolCal As Boolean = False
' ''    
' ''    
' ''    Dim objExp As New expenses
' ''    Dim objScenarioDetail As New scenarioDetail
' ''    'Dim cpdtable As New DataTable
' ''    Dim fdtable As New DataTable
' ''    Dim profit As New DataTable
' ''    Dim deltatable As New DataTable
' ''    Dim gammatable As New DataTable
' ''    Dim vegatable As New DataTable
' ''    Dim thetatable As New DataTable
' ''    Dim volgatable As New DataTable
' ''    Dim vannatable As New DataTable
' ''    'Dim mObjData As New DataAnalysis.AnalysisData
' ''    'Dim mObjData As New OptGreeks.Calc
' ''    'Dim mObjData As New OptionG.Greeks
' ''    Dim Mrateofinterast As Double = 0
' ''    Dim objTrad As trading = New trading
' ''    Dim rtable As DataTable
' ''    Dim flgUnderline As Boolean = False
' ''    
' ''    
' ''    
' ''    'Dim DecimalSetting.iRGamma As Integer
' ''    'Dim DecimalSetting.iRTheta As Integer
' ''    'Dim DecimalSetting.iRDelta As Integer
' ''    'Dim DecimalSetting.iRVega As Integer
' ''    Dim gcheck As Boolean = False
' ''    
' ''    'Public mAllCV As String
' ''    Dim txtmid As Double
' ''    Dim interval As Double
' ''    Dim checkall As Boolean = True
' ''    Dim grossmtm As Double
' ''    Dim contMenu() As ContextMenuStrip
' ''    Dim contMenuExpiry() As ContextMenuStrip
' ''    Dim frmFlg As Boolean = False

' ''    Dim involflag As Boolean = False
' ''    Dim decvolflag As Boolean = False
' ''#End Region
' ''    Private Sub scenario_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
' ''        If Not objanalysis Is Nothing Then
' ''            objanalysis.refreshstarted = True
' ''        End If
' ''        Me.Icon = My.Resources.volhedge_icon
' ''        'Me.WindowState = FormWindowState.Maximized
' ''        'Me.Refresh()
' ''    End Sub
' ''    Private Sub scenario_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
' ''        chkscenario = False
' ''        txtmkt.Text = 0
' ''        'If Not objanalysis Is Nothing Then
' ''        '    objanalysis.refreshstarted = True
' ''        'End If
' ''        If MsgBox("Do you want to save Interval and No. of Strike??", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Save") = MsgBoxResult.Yes Then
' ''            objScenarioDetail.Delete_scenario()
' ''            objScenarioDetail.Interval = Val(txtinterval.Text)
' ''            objScenarioDetail.Strike = Val(txtllimit.Text)
' ''            objScenarioDetail.Interval_type = IIf(chkint.Checked = True, "Per", "Value")
' ''            objScenarioDetail.Insert_scenario()
' ''        End If
' ''        Call analysis.searchcompany()
' ''    End Sub
' ''    Private Sub scenario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
' ''        VarIsFrmLoad = True
' ''        VarIsFirstLoad = True
' ''        Me.WindowState = FormWindowState.Maximized
' ''        If Not objanalysis Is Nothing Then
' ''            objanalysis.refreshstarted = False
' ''        End If
' ''        chkscenario = True
' ''        Me.Text = Me.Text & "-" & scname
' ''        If scenariotable.Rows.Count > 0 Then
' ''            grossmtm = scenariotable.Rows(0)("grossMTM")
' ''        End If
' ''        If VarIsCurrency = False Then
' ''            txtinterast.Text = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
' ''        ElseIf VarIsCurrency = True Then
' ''            txtinterast.Text = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='CurrrencyRateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
' ''        End If
' ''        Mrateofinterast = Val(txtinterast.Text)
' ''        'DecimalSetting.iRTheta = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRTheta'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRTheta'")))
' ''        'DecimalSetting.iRDelta = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRDelta'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRDelta'")))
' ''        'DecimalSetting.iRVega = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRVega'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRVega'")))
' ''        'DecimalSetting.iRGamma = CInt(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRGamma'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='DecimalSetting.iRGamma'")))
' ''        txtcvol.Text = 0
' ''        txtpvol.Text = 0
' ''        txtmid = 0
' ''        If trscen = True Then
' ''            chkcheck.Checked = True
' ''            gcheck = True
' ''            dttoday.Value = time1
' ''            dtexp.Value = time2
' ''            txtdays.Text = DateDiff(DateInterval.Day, time1.Date, time2.Date)
' ''            If dttoday.Value.Date < dtexp.Value.Date Then
' ''                REM This code commented because of Day difference plus 1 days display
' ''                'txtdays.Text = Val(txtdays.Text) + 1
' ''            ElseIf dttoday.Value.Date = dtexp.Value.Date Then
' ''                txtdays.Text = Val(txtdays.Text) + 0.5
' ''            End If
' ''            txtmkt.Text = Math.Round(mvalue, 0)
' ''            txtmid = mvalue
' ''            Dim grow As New DataGridViewRow
' ''            For Each drow As DataRow In scenariotable.Rows
' ''                grow = New DataGridViewRow
' ''                grdtrad.Rows.Add(grow)
' ''                'grdtrad.Refresh()
' ''            Next
' ''            Dim i As Integer = 0
' ''            txtunits.Text = 0
' ''            txtdelval.Text = 0
' ''            txtthval.Text = 0
' ''            txtvgval.Text = 0
' ''            txtgmval.Text = 0

' ''            ''divyesh
' ''            txtunits1.Text = 0
' ''            txtdelval1.Text = 0
' ''            txtthval1.Text = 0
' ''            txtvgval1.Text = 0
' ''            txtgmval1.Text = 0
' ''            For Each drow As DataRow In scenariotable.Select("", "cpf")
' ''                grdtrad.Rows(i).Cells("Active").Value = CBool(drow("status"))
' ''                grdtrad.Rows(i).Cells("TimeI").Value = CDate(drow("time1"))
' ''                grdtrad.Rows(i).Cells("TimeII").Value = CDate(drow("time2"))
' ''                grdtrad.Rows(i).Cells("CPF").Value = CStr(drow("cpf"))
' ''                grdtrad.Rows(i).Cells("spval").Value = CDbl(drow("spot"))
' ''                grdtrad.Rows(i).Cells("Strike").Value = CDbl(drow("strikes"))
' ''                grdtrad.Rows(i).Cells("units").Value = Val(drow("qty"))
' ''                grdtrad.Rows(i).Cells("ltp").Value = Val(drow("ltp"))
' ''                grdtrad.Rows(i).Cells("last").Value = CDbl(drow("rate"))
' ''                If Val(drow("vol")) = 0 Then
' ''                    grdtrad.Rows(i).Cells("lv").Value = CDbl(0.0)
' ''                Else
' ''                    grdtrad.Rows(i).Cells("lv").Value = Val(drow("vol"))
' ''                End If
' ''                grdtrad.Rows(i).Cells("delta").Value = Val(drow("delta"))
' ''                grdtrad.Rows(i).Cells("deltaval").Value = Val(drow("deltaval"))
' ''                grdtrad.Rows(i).Cells("gamma").Value = Val(drow("gamma"))
' ''                grdtrad.Rows(i).Cells("gmval").Value = Val(drow("gmval"))
' ''                grdtrad.Rows(i).Cells("vega").Value = Val(drow("vega"))
' ''                grdtrad.Rows(i).Cells("vgval").Value = Val(drow("vgval"))
' ''                grdtrad.Rows(i).Cells("theta").Value = Val(drow("theta"))
' ''                grdtrad.Rows(i).Cells("thetaval").Value = Val(drow("thetaval"))

' ''                grdtrad.Rows(i).Cells("volga").Value = Val(drow("volga"))
' ''                grdtrad.Rows(i).Cells("volgaval").Value = Val(drow("volgaval"))
' ''                grdtrad.Rows(i).Cells("vanna").Value = Val(drow("vanna"))
' ''                grdtrad.Rows(i).Cells("vannaval").Value = Val(drow("vannaval"))

' ''                grdtrad.Rows(i).Cells("uid").Value = Val(drow("uid"))

' ''                ''divyesh
' ''                grdtrad.Rows(i).Cells("delta1").Value = Val(drow("delta"))
' ''                grdtrad.Rows(i).Cells("gamma1").Value = Val(drow("gamma"))
' ''                grdtrad.Rows(i).Cells("vega1").Value = Val(drow("vega"))
' ''                grdtrad.Rows(i).Cells("theta1").Value = Val(drow("theta"))
' ''                grdtrad.Rows(i).Cells("volga1").Value = Val(drow("volga"))
' ''                grdtrad.Rows(i).Cells("vanna1").Value = Val(drow("vanna"))

' ''                grdtrad.Rows(i).Cells("deltaval1").Value = Val(drow("deltaval1"))
' ''                grdtrad.Rows(i).Cells("gmval1").Value = Val(drow("gmval1"))
' ''                grdtrad.Rows(i).Cells("vgval1").Value = Val(drow("vgval1"))
' ''                grdtrad.Rows(i).Cells("thetaval1").Value = Val(drow("thetaval1"))
' ''                grdtrad.Rows(i).Cells("volgaval1").Value = Val(drow("volgaval1"))
' ''                grdtrad.Rows(i).Cells("vannaval1").Value = Val(drow("vannaval1"))


' ''                grdtrad.Rows(i).Cells("ltp1").Value = Val(drow("ltp1"))

' ''                'txtunits.Text = Math.Round(Val(txtunits.Text) + Val(grdtrad.Rows(i).Cells("ltp").Value), 2)
' ''                txtdelval.Text = Format(Val(txtdelval.Text) + Val(grdtrad.Rows(i).Cells("deltaval").Value), DecimalSetting.sDeltaval)
' ''                txtthval.Text = Format(Val(txtthval.Text) + Val(grdtrad.Rows(i).Cells("thetaval").Value), DecimalSetting.sThetaval)
' ''                txtvgval.Text = Format(Val(txtvgval.Text) + Val(grdtrad.Rows(i).Cells("vgval").Value), DecimalSetting.sVegaval)
' ''                txtgmval.Text = Format(Val(txtgmval.Text) + Val(grdtrad.Rows(i).Cells("gmval").Value), DecimalSetting.sGammaval)
' ''                txtVolgaVal.Text = Format(Val(txtVolgaVal.Text) + Val(grdtrad.Rows(i).Cells("volgaval").Value), DecimalSetting.sVolgaval)
' ''                TxtVannaVal.Text = Format(Val(TxtVannaVal.Text) + Val(grdtrad.Rows(i).Cells("vannaval").Value), DecimalSetting.sVannaval)


' ''                ''divyesh 
' ''                txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(grdtrad.Rows(i).Cells("deltaval1").Value), DecimalSetting.sDeltaval)
' ''                txtthval1.Text = Format(Val(txtthval1.Text) + Val(grdtrad.Rows(i).Cells("thetaval1").Value), DecimalSetting.sThetaval)
' ''                txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(grdtrad.Rows(i).Cells("vgval1").Value), DecimalSetting.sVegaval)
' ''                txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(grdtrad.Rows(i).Cells("gmval1").Value), DecimalSetting.sGammaval)
' ''                TxtVolgaVal1.Text = Format(Val(TxtVolgaVal1.Text) + Val(grdtrad.Rows(i).Cells("volgaval1").Value), DecimalSetting.sVolgaval)
' ''                TxtVannaVal1.Text = Format(Val(TxtVannaVal1.Text) + Val(grdtrad.Rows(i).Cells("vannaval1").Value), DecimalSetting.sVannaval)

' ''                i += 1
' ''            Next
' ''            grdtrad.Columns("TimeII").DefaultCellStyle.NullValue = Format(scenariotable.Rows(0).Item("time2"), "MM/dd/yyyy")
' ''            scenariotable.Rows.Clear()
' ''            trscen = False
' ''        End If


' ''        'to get interval,No of strike from database
' ''        Dim dtScenario As New DataTable
' ''        dtScenario = objScenarioDetail.select_scenario
' ''        If dtScenario.Rows.Count > 0 Then
' ''            If dtScenario.Rows(0)("interval_type").ToString = "Per" Then
' ''                chkint.Checked = True
' ''            Else
' ''                chkint.Checked = False
' ''            End If
' ''            txtinterval.Text = Val(dtScenario.Rows(0)("interval").ToString)
' ''            txtllimit.Text = Val(dtScenario.Rows(0)("strike").ToString)
' ''            interval = txtinterval.Text
' ''        Else
' ''            'Call CalInterval()
' ''            'If Val(txtmkt.Text) > 0 Then
' ''            '    txtmid = Val(txtmkt.Text)
' ''            '    If Val(txtmid) < 100 Then
' ''            '        txtinterval.Text = 1
' ''            '    ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
' ''            '        txtinterval.Text = 5
' ''            '    ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
' ''            '        txtinterval.Text = 10
' ''            '    ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
' ''            '        txtinterval.Text = 100
' ''            '    ElseIf Val(txtmid) > 10000 Then
' ''            '        txtinterval.Text = 500
' ''            '    End If
' ''            '    interval = txtinterval.Text
' ''            '    'interval = Math.Round(val(txtmid) * 1 / 100, 0)
' ''            'End If
' ''        End If

' ''        Call CalInterval()

' ''        txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
' ''        ''divyesh
' ''        txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)

' ''        txtdays.Text = 0
' ''        If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
' ''            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
' ''        End If
' ''        If dttoday.Value.Date < dtexp.Value.Date Then
' ''            REM This code commented because of Day difference plus 1 days display
' ''            'txtdays.Text = Val(txtdays.Text) + 1
' ''        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
' ''            txtdays.Text = Val(txtdays.Text) + 0.5
' ''        End If

' ''        create_active()
' ''        'create contextmenustrip for vol column cells
' ''        ReDim contMenu(grdtrad.Rows.Count - 1)
' ''        Dim cnt As Integer = 0
' ''        For Each cntmenu As ContextMenuStrip In contMenu
' ''            cntmenu = New ContextMenuStrip
' ''            Dim item As New ToolStripMenuItem("Unfreeze")
' ''            ' item.CheckOnClick = True
' ''            item.Tag = cnt.ToString
' ''            AddHandler item.Click, AddressOf freezVol
' ''            AddHandler cntmenu.Opening, AddressOf contMenuOpen
' ''            cntmenu.Items.Add(item)
' ''            grdtrad.Rows(cnt).Cells("lv").ContextMenuStrip = cntmenu
' ''            cnt += 1
' ''        Next

' ''        'create contextmenustrip for expiry column cells
' ''        ReDim contMenuExpiry(grdtrad.Rows.Count - 1)
' ''        cnt = 0
' ''        For Each cntmenu As ContextMenuStrip In contMenuExpiry
' ''            cntmenu = New ContextMenuStrip
' ''            Dim item As New ToolStripMenuItem("Unfreeze")
' ''            ' item.CheckOnClick = True
' ''            item.Tag = cnt.ToString
' ''            AddHandler item.Click, AddressOf freezExpiry
' ''            AddHandler cntmenu.Opening, AddressOf contMenuOpenExpiry
' ''            cntmenu.Items.Add(item)
' ''            grdtrad.Rows(cnt).Cells("TimeII").ContextMenuStrip = cntmenu
' ''            cnt += 1
' ''        Next

' ''        For Each col As DataGridViewColumn In grdtrad.Columns
' ''            If col.Index >= 9 Then
' ''                col.HeaderCell.Style.BackColor = Color.Gray
' ''            End If
' ''            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
' ''        Next

' ''        Call Vol_Click(Vol, e)
' ''        Call expiry_Click(expiry, e)
' ''        If scname <> "" Then
' ''            mAllCV = ""
' ''            result(True)
' ''        End If
' ''        'For Each drow As DataGridViewRow In grdtrad.Rows
' ''        '    drow.Cells("VolORG").Value = drow.Cells("lv").Value
' ''        'Next
' ''        If analysis.chkanalysis = True Then
' ''            MDI.ToolStripcompanyCombo.Visible = False
' ''            MDI.ToolStripMenuSearchComp.Visible = False
' ''        End If
' ''    End Sub
' ''    Private Sub cal_summary()
' ''        txtdelval.Text = 0
' ''        txtthval.Text = 0
' ''        txtvgval.Text = 0
' ''        txtgmval.Text = 0
' ''        txtVolgaVal.Text = 0
' ''        TxtVannaVal.Text = 0

' ''        txtdelval1.Text = 0
' ''        txtthval1.Text = 0
' ''        txtvgval1.Text = 0
' ''        txtgmval1.Text = 0
' ''        TxtVolgaVal1.Text = 0
' ''        TxtVannaVal1.Text = 0

' ''        'SetZeroVal()

' ''        txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
' ''        txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)

' ''        For Each grow As DataGridViewRow In grdtrad.Rows
' ''            If grow.Cells("Active").Value = True Then
' ''                txtdelval.Text = Format(Val(txtdelval.Text) + Val(grow.Cells("deltaval").Value), DecimalSetting.sDeltaval)
' ''                txtthval.Text = Format(Val(txtthval.Text) + Val(grow.Cells("thetaval").Value), DecimalSetting.sThetaval)
' ''                txtvgval.Text = Format(Val(txtvgval.Text) + Val(grow.Cells("vgval").Value), DecimalSetting.sVegaval)
' ''                txtgmval.Text = Format(Val(txtgmval.Text) + Val(grow.Cells("gmval").Value), DecimalSetting.sGammaval)
' ''                txtVolgaVal.Text = Format(Val(txtVolgaVal.Text) + Val(grow.Cells("volgaval").Value), DecimalSetting.sVolgaval)
' ''                TxtVannaVal.Text = Format(Val(TxtVannaVal.Text) + Val(grow.Cells("vannaval").Value), DecimalSetting.sVannaval)


' ''                'txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(grow.Cells("deltaval1").Value), DecimalSetting.sDeltaval)
' ''                'txtthval1.Text = Format(Val(txtthval1.Text) + Val(grow.Cells("thetaval1").Value), DecimalSetting.sThetaval)
' ''                'txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(grow.Cells("vgval1").Value), DecimalSetting.sVegaval)
' ''                'txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(grow.Cells("gmval1").Value), DecimalSetting.sGammaval)
' ''                'TxtVolgaVal1.Text = Format(Val(TxtVolgaVal1.Text) + Val(grow.Cells("volgaval1").Value), DecimalSetting.sVolgaval)
' ''                'TxtVannaVal1.Text = Format(Val(TxtVannaVal1.Text) + Val(grow.Cells("vannaval1").Value), DecimalSetting.sVannaval)
' ''            End If
' ''        Next
' ''        'result(False)
' ''    End Sub
' ''    'Private Sub SetZeroVal()
' ''    '    txtdelval.Text = 0
' ''    '    txtthval.Text = 0
' ''    '    txtvgval.Text = 0
' ''    '    txtgmval.Text = 0
' ''    '    txtVolgaVal.Text = 0
' ''    '    TxtVannaVal.Text = 0

' ''    '    txtdelval1.Text = 0
' ''    '    txtthval1.Text = 0
' ''    '    txtvgval1.Text = 0
' ''    '    txtgmval1.Text = 0
' ''    '    TxtVolgaVal1.Text = 0
' ''    '    TxtVannaVal1.Text = 0
' ''    'End Sub
' ''    Private Sub contMenuOpen(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
' ''        'Dim ind As Integer
' ''        'ind = CType(sender, ContextMenuStrip).Items(0).Tag
' ''        If cellno = -1 Then Exit Sub
' ''        If grdtrad.Rows(cellno).Cells("lv").ReadOnly = True Then
' ''            CType(sender, ContextMenuStrip).Items(0).Text = "Unfreeze"
' ''        Else
' ''            CType(sender, ContextMenuStrip).Items(0).Text = "Freeze"
' ''        End If
' ''    End Sub
' ''    Private Sub contMenuOpenExpiry(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
' ''        'Dim ind As Integer
' ''        'ind = CType(sender, ContextMenuStrip).Items(0).Tag
' ''        If cellno = -1 Then Exit Sub
' ''        If grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = True Then
' ''            CType(sender, ContextMenuStrip).Items(0).Text = "Unfreeze"
' ''        Else
' ''            CType(sender, ContextMenuStrip).Items(0).Text = "Freeze"
' ''        End If
' ''    End Sub

' ''    Private Sub freezVol(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If cellno = -1 Then Exit Sub
' ''        If CType(sender, ToolStripMenuItem).Text = "Unfreeze" Then
' ''            grdtrad.Rows(cellno).Cells("lv").ReadOnly = False
' ''            CType(sender, ToolStripMenuItem).Text = "Freeze"
' ''        Else
' ''            grdtrad.Rows(cellno).Cells("lv").ReadOnly = True
' ''            CType(sender, ToolStripMenuItem).Text = "Unfreeze"
' ''        End If
' ''    End Sub
' ''    Private Sub freezExpiry(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If cellno = -1 Then Exit Sub
' ''        If CType(sender, ToolStripMenuItem).Text = "Unfreeze" Then
' ''            grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = False
' ''            CType(sender, ToolStripMenuItem).Text = "Freeze"
' ''        Else
' ''            grdtrad.Rows(cellno).Cells("TimeII").ReadOnly = True
' ''            CType(sender, ToolStripMenuItem).Text = "Unfreeze"
' ''        End If
' ''    End Sub
' ''    Private Sub scenario_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
' ''        If e.KeyCode = Keys.F9 Then
' ''            'grdtrad.EndEdit()
' ''            'grdtrad.Refresh()
' ''            If Val(txtllimit.Text) > 0 And Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then
' ''                result(False)
' ''                'init_table()
' ''                'cal_profit()
' ''                'create_chart_profit()
' ''                'create_chart_profit_Full()
' ''                'cal_delta()
' ''                'create_chart_delta()
' ''                'create_chart_delta_Full()
' ''                'cal_gamma()
' ''                'create_chart_gamma()
' ''                'create_chart_gamma_Full()
' ''                'cal_vega()
' ''                'create_chart_vega()
' ''                'create_chart_vega_Full()
' ''                'cal_theta()
' ''                'create_chart_theta()
' ''                'create_chart_theta_Full()

' ''                'grdprofit.DataSource = profit
' ''                'grdprofit.Refresh()
' ''                'grddelta.DataSource = deltatable
' ''                'grddelta.Refresh()
' ''                'grdgamma.DataSource = gammatable
' ''                'grdgamma.Refresh()
' ''                'grdvega.DataSource = vegatable
' ''                'grdvega.Refresh()
' ''                'grdtheta.DataSource = thetatable
' ''                'grdtheta.Refresh()
' ''            End If
' ''        ElseIf e.KeyCode = Keys.F1 Then
' ''            result(True)
' ''        ElseIf e.KeyCode = Keys.F8 Then
' ''            CmdAllCV_Click(sender, e)
' ''        ElseIf e.KeyCode = Keys.F11 Then
' ''            export()
' ''        ElseIf e.KeyCode = Keys.F12 Then
' ''            import()
' ''        ElseIf e.KeyCode = Keys.PageDown Then
' ''            If tbcon.SelectedIndex < 9 Then tbcon.SelectTab(tbcon.SelectedIndex + 1)
' ''        ElseIf e.KeyCode = Keys.PageUp Then
' ''            If tbcon.SelectedIndex > 0 Then tbcon.SelectTab(tbcon.SelectedIndex - 1)
' ''        End If
' ''    End Sub
' ''#Region "Initial"
' ''    Private Sub init_datatable()
' ''        'cpdtable = New DataTable
' ''        'With cpdtable.Columns

' ''        '    .Add("status", GetType(Boolean))
' ''        '    .Add("mdate", GetType(Date))
' ''        '    .Add("spval", GetType(Double))
' ''        '    .Add("strikes", GetType(Double))
' ''        '    .Add("cp")
' ''        '    .Add("units", GetType(Double))
' ''        '    .Add("last", GetType(Double))
' ''        '    .Add("lv", GetType(Double))
' ''        '    .Add("delta", GetType(Double))
' ''        '    .Add("deltaval", GetType(Double))
' ''        '    .Add("theta", GetType(Double))
' ''        '    .Add("thetaval", GetType(Double))
' ''        '    .Add("vega", GetType(Double))
' ''        '    .Add("vgval", GetType(Double))
' ''        '    .Add("gamma", GetType(Double))
' ''        '    .Add("gmval", GetType(Double))
' ''        '    .Add("uid", GetType(Integer))

' ''        'End With
' ''    End Sub
' ''    Private Sub init_table()

' ''        If Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then
' ''            profit = New DataTable
' ''            'grdprofit.Columns.Clear()
' ''            deltatable = New DataTable
' ''            gammatable = New DataTable
' ''            vegatable = New DataTable
' ''            thetatable = New DataTable
' ''            volgatable = New DataTable
' ''            vannatable = New DataTable
' ''            Dim i As Integer = 0
' ''            If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
' ''                i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
' ''            End If
' ''            i += 1
' ''            Dim j As Integer
' ''            j = 0
' ''            Dim start As Double
' ''            Dim endd As Double
' ''            start = Val(txtmid) - (Val(txtllimit.Text) * Val(interval))
' ''            endd = Val(txtmid) + (Val(txtllimit.Text) * Val(interval))
' ''            Dim drow As DataRow

' ''            '######################################################################################
' ''            grdprofit.DataSource = Nothing
' ''            'grdprofit.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grdprofit.Columns.Count > 0 Then
' ''                grdprofit.Columns.Clear()
' ''            End If
' ''            Dim style1 As New DataGridViewCellStyle
' ''            style1.Format = "N2"
' ''            Dim acol As DataGridViewTextBoxColumn

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grdprofit.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grdprofit.Columns.Add(acol)


' ''            Dim grow As DataGridViewRow
' ''            For Each grow In grdact.Rows

' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            acol = New DataGridViewTextBoxColumn
' ''                            acol.HeaderText = gcol.HeaderText
' ''                            acol.DataPropertyName = gcol.DataPropertyName
' ''                            acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                            'acol.DefaultCellStyle.Format = RoundGrossMTM

' ''                            grdprofit.Columns.Add(acol)
' ''                            'acol.DefaultCellStyle.Format = "N2"


' ''                        End If
' ''                    End If
' ''                Next
' ''            Next


' ''            With profit.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                .Add(c)
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            'FOR MINUS LIMIT(STRI) FROM MID
' ''            Dim inter As Double = 0
' ''            Dim int As Double = 0
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = profit.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                If drow("spotvalue") < 0 Then Continue For
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100) '& " %"
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)

' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                drow(gcol.DataPropertyName) = 0
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                'If drow("spotvalue") > 0 Then
' ''                profit.Rows.Add(drow)
' ''                'End If
' ''            Next
' ''            'FOR PLUS LIMIT(STRI) FROM MID
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = profit.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100 '& " %"
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                drow(gcol.DataPropertyName) = 0
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                profit.Rows.Add(drow)
' ''            Next


' ''            '######################################################################################

' ''            grddelta.DataSource = Nothing
' ''            'grddelta.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grddelta.Columns.Count > 0 Then
' ''                grddelta.Columns.Clear()
' ''            End If

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grddelta.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grddelta.Columns.Add(acol)

' ''            For Each grow In grdact.Rows
' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            If (IsDate(gcol.DataPropertyName)) Then
' ''                                acol = New DataGridViewTextBoxColumn
' ''                                acol.HeaderText = gcol.HeaderText
' ''                                acol.DataPropertyName = gcol.DataPropertyName
' ''                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                                grddelta.Columns.Add(acol)
' ''                            End If
' ''                        End If
' ''                    End If
' ''                Next
' ''            Next


' ''            '''''''''''''''''' Detla Table Initalise

' ''            With deltatable.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    .Add(c)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            inter = 0
' ''            int = 0
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = deltatable.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0
' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100)
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
' ''                    'drow("Percent(%)") = Math.Round(((val(drow("SPOTVALUE")) / val(txtmid)) - 1) * 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                deltatable.Rows.Add(drow)
' ''            Next
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''            'FOR PLUS LIMIT(STRI) FROM MID
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = deltatable.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                deltatable.Rows.Add(drow)
' ''            Next
' ''            '######################################################################################

' ''            '############# Gamma Table Init
' ''            grdgamma.DataSource = Nothing
' ''            'grdgamma.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grdgamma.Columns.Count > 0 Then
' ''                grdgamma.Columns.Clear()
' ''            End If

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grdgamma.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grdgamma.Columns.Add(acol)

' ''            For Each grow In grdact.Rows
' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            If (IsDate(gcol.DataPropertyName)) Then
' ''                                acol = New DataGridViewTextBoxColumn
' ''                                acol.HeaderText = gcol.HeaderText
' ''                                acol.DataPropertyName = gcol.DataPropertyName
' ''                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                                grdgamma.Columns.Add(acol)
' ''                            End If
' ''                        End If
' ''                    End If
' ''                Next
' ''            Next



' ''            With gammatable.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    .Add(c)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = gammatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            inter = 0
' ''            int = 0
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = gammatable.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0
' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100)
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                gammatable.Rows.Add(drow)
' ''            Next
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = gammatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = gammatable.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                gammatable.Rows.Add(drow)
' ''            Next


' ''            ' Vega Table INit ######################################################################################
' ''            grdvega.DataSource = Nothing
' ''            ' grdvega.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grdvega.Columns.Count > 0 Then
' ''                grdvega.Columns.Clear()
' ''            End If


' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grdvega.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grdvega.Columns.Add(acol)

' ''            For Each grow In grdact.Rows
' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            If (IsDate(gcol.DataPropertyName)) Then
' ''                                acol = New DataGridViewTextBoxColumn
' ''                                acol.HeaderText = gcol.HeaderText
' ''                                acol.DataPropertyName = gcol.DataPropertyName
' ''                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                                grdvega.Columns.Add(acol)
' ''                            End If
' ''                        End If
' ''                    End If
' ''                Next
' ''            Next


' ''            With vegatable.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    .Add(c)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = vegatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            inter = 0
' ''            int = 0
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = vegatable.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0
' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100)
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                vegatable.Rows.Add(drow)
' ''            Next
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = vegatable.NewRow
' ''            'drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = vegatable.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                vegatable.Rows.Add(drow)
' ''            Next
' ''            '######################################################################################
' ''            grdtheta.DataSource = Nothing
' ''            'grdtheta.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grdtheta.Columns.Count > 0 Then
' ''                grdtheta.Columns.Clear()
' ''            End If

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grdtheta.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grdtheta.Columns.Add(acol)

' ''            For Each grow In grdact.Rows
' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            If (IsDate(gcol.DataPropertyName)) Then
' ''                                acol = New DataGridViewTextBoxColumn
' ''                                acol.HeaderText = gcol.HeaderText
' ''                                acol.DataPropertyName = gcol.DataPropertyName
' ''                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                                grdtheta.Columns.Add(acol)
' ''                            End If
' ''                        End If
' ''                    End If
' ''                Next
' ''            Next


' ''            With thetatable.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    .Add(c)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = thetatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            inter = 0
' ''            int = 0
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = thetatable.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0
' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100)
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                thetatable.Rows.Add(drow)
' ''            Next
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = thetatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = thetatable.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0

' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                thetatable.Rows.Add(drow)
' ''            Next

' ''            '------------------------------------------------------------------------------------------------------
' ''            'For Volga
' ''            grdvolga.DataSource = Nothing
' ''            'grdvolga.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grdvolga.Columns.Count > 0 Then
' ''                grdvolga.Columns.Clear()
' ''            End If

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grdvolga.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grdvolga.Columns.Add(acol)

' ''            For Each grow In grdact.Rows
' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            If (IsDate(gcol.DataPropertyName)) Then
' ''                                acol = New DataGridViewTextBoxColumn
' ''                                acol.HeaderText = gcol.HeaderText
' ''                                acol.DataPropertyName = gcol.DataPropertyName
' ''                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                                grdvolga.Columns.Add(acol)
' ''                            End If
' ''                        End If
' ''                    End If
' ''                Next
' ''            Next


' ''            With volgatable.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    .Add(c)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = volgatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            inter = 0
' ''            int = 0
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = volgatable.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0
' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100)
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                volgatable.Rows.Add(drow)
' ''            Next
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = volgatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = volgatable.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0

' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                volgatable.Rows.Add(drow)
' ''            Next

' ''            '------------------------------------------------------------------------------------------------------
' ''            'For Vanna
' ''            grdvanna.DataSource = Nothing
' ''            'grdVanna.Refresh()
' ''            'grdprofit.Rows.Clear()
' ''            If grdvanna.Columns.Count > 0 Then
' ''                grdvanna.Columns.Clear()
' ''            End If

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "SpotValue"
' ''            acol.DataPropertyName = "SpotValue"
' ''            acol.Frozen = True
' ''            grdvanna.Columns.Add(acol)

' ''            acol = New DataGridViewTextBoxColumn
' ''            acol.HeaderText = "Percent(%)"
' ''            acol.DataPropertyName = "Percent(%)"
' ''            acol.Frozen = True
' ''            grdvanna.Columns.Add(acol)

' ''            For Each grow In grdact.Rows
' ''                For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                    If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                        If grow.Cells(gcol.Index).Value = True Then
' ''                            If (IsDate(gcol.DataPropertyName)) Then
' ''                                acol = New DataGridViewTextBoxColumn
' ''                                acol.HeaderText = gcol.HeaderText
' ''                                acol.DataPropertyName = gcol.DataPropertyName
' ''                                acol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
' ''                                grdvanna.Columns.Add(acol)
' ''                            End If
' ''                        End If
' ''                    End If
' ''                Next
' ''            Next


' ''            With vannatable.Columns
' ''                .Add("SpotValue", GetType(Double))
' ''                .Add("Percent(%)", GetType(Double))
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                Dim c As String = gcol.DataPropertyName
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    .Add(c)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''            End With
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = Vannatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            inter = 0
' ''            int = 0
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = deltatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + start
' ''            For j = 1 To Val(txtllimit.Text) + 1
' ''                drow = vannatable.NewRow

' ''                ' drow("spotvalue") = (val(interval) * j) + start
' ''                drow("spotvalue") = txtmid - (Val(interval * (Val(txtllimit.Text) - int)))
' ''                drow("Percent(%)") = 0
' ''                If chkint.Checked = True Then
' ''                    inter = ((Val(txtinterval.Text) * (Val(txtllimit.Text) - int)) / 100)
' ''                    drow("Percent(%)") = -(inter * 100)
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (-inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                int += 1
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                vannatable.Rows.Add(drow)
' ''            Next
' ''            'For j = 1 To val(txtllimit.Text)
' ''            '    drow = Vannatable.NewRow
' ''            '    drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''            inter = 0
' ''            For j = 1 To Val(txtllimit.Text)
' ''                drow = vannatable.NewRow
' ''                drow("spotvalue") = (Val(interval) * j) + Val(txtmid)
' ''                drow("Percent(%)") = 0

' ''                If chkint.Checked = True Then
' ''                    inter = (Val(txtinterval.Text) * Val(j)) / 100
' ''                    drow("Percent(%)") = inter * 100
' ''                    drow("spotvalue") = Math.Round((txtmid * (100 + (inter * 100))) / 100, 0)
' ''                Else
' ''                    drow("Percent(%)") = Math.Round(((Val(drow("SPOTVALUE")) / Val(txtmid)) - 1) * 100, 0)
' ''                End If
' ''                For Each grow In grdact.Rows
' ''                    For Each gcol As DataGridViewCheckBoxColumn In grdact.Columns
' ''                        If Not IsDBNull(grow.Cells(gcol.Index).Value) Then
' ''                            If grow.Cells(gcol.Index).Value = True Then
' ''                                If (IsDate(gcol.DataPropertyName)) Then
' ''                                    drow(gcol.DataPropertyName) = 0

' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                Next
' ''                vannatable.Rows.Add(drow)
' ''            Next

' ''        Else
' ''            MsgBox("enter Value.")
' ''        End If


' ''    End Sub
' ''    Private Sub init_table1()

' ''        'If val(txtllimit.Text) > 0 And val(txtllimit.Text > 0) And val(interval) > 0 And val(txtmid) > 0 Then
' ''        '    profit = New DataTable

' ''        '    deltatable = New DataTable
' ''        '    gammatable = New DataTable
' ''        '    vegatable = New DataTable
' ''        '    thetatable = New DataTable
' ''        '    Dim i As Integer = 0
' ''        '    If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''        '        i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
' ''        '    End If
' ''        '    Dim j As Integer
' ''        '    j = 0
' ''        '    Dim start As Double
' ''        '    Dim endd As Double
' ''        '    start = val(txtmid.Text) - (val(txtllimit.Text) * val(interval))
' ''        '    endd = val(txtmid.Text) + (val(txtllimit.Text) * val(interval))
' ''        '    Dim drow As DataRow
' ''        '    '######################################################################################

' ''        '    With profit.Columns

' ''        '        .Add("SpotValue", GetType(Double))
' ''        '        For Each grow As DataGridViewRow In grdact.Rows
' ''        '            For Each gcol As DataGridViewColumn In grdact.Columns
' ''        '                If grow.Cells(gcol.Index).Value = True Then
' ''        '                    Dim c As String = gcol.HeaderText
' ''        '                    .Add(c)
' ''        '                End If
' ''        '            Next
' ''        '        Next
' ''        '        'For j = 0 To i - 1
' ''        '        '    Dim c As String = Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "" '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")
' ''        '        '    .Add(c)
' ''        '        'Next
' ''        '    End With

' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = profit.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + start
' ''        '        For Each grow As DataGridViewRow In grdact.Rows
' ''        '            For Each gcol As DataGridViewColumn In grdact.Columns
' ''        '                If grow.Cells(gcol.Index).Value = True Then
' ''        '                    drow(gcol.HeaderText) = 0
' ''        '                End If
' ''        '            Next
' ''        '        Next
' ''        '        'For k As Integer = 0 To i - 1
' ''        '        '    drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        'Next
' ''        '        profit.Rows.Add(drow)
' ''        '    Next
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = profit.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''        '        For Each grow As DataGridViewRow In grdact.Rows
' ''        '            For Each gcol As DataGridViewColumn In grdact.Columns
' ''        '                If grow.Cells(gcol.Index).Value = True Then
' ''        '                    drow(gcol.HeaderText) = 0
' ''        '                End If
' ''        '            Next
' ''        '        Next
' ''        '        'For k As Integer = 0 To i - 1
' ''        '        '    drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        'Next
' ''        '        profit.Rows.Add(drow)
' ''        '    Next
' ''        '    '######################################################################################

' ''        '    With deltatable.Columns
' ''        '        .Add("SpotValue", GetType(Double))
' ''        '        For j = 0 To i - 1
' ''        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
' ''        '        Next
' ''        '    End With
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = deltatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + start
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        deltatable.Rows.Add(drow)
' ''        '    Next
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = deltatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        deltatable.Rows.Add(drow)
' ''        '    Next
' ''        '    '######################################################################################

' ''        '    With gammatable.Columns
' ''        '        .Add("SpotValue", GetType(Double))
' ''        '        For j = 0 To i - 1
' ''        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") '& vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
' ''        '        Next
' ''        '    End With
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = gammatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + start
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        gammatable.Rows.Add(drow)
' ''        '    Next
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = gammatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        gammatable.Rows.Add(drow)
' ''        '    Next


' ''        '    '######################################################################################

' ''        '    With vegatable.Columns
' ''        '        .Add("SpotValue", GetType(Double))
' ''        '        For j = 0 To i - 1
' ''        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
' ''        '        Next
' ''        '    End With
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = vegatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + start
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        vegatable.Rows.Add(drow)
' ''        '    Next
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = vegatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        vegatable.Rows.Add(drow)
' ''        '    Next
' ''        '    '######################################################################################

' ''        '    With thetatable.Columns
' ''        '        .Add("SpotValue", GetType(Double))
' ''        '        For j = 0 To i - 1
' ''        '            .Add(Format(DateAdd(DateInterval.Day, j, dttoday.Value), "dd/MM/yyyy") & "") ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy"))
' ''        '        Next
' ''        '    End With
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = thetatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + start
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        thetatable.Rows.Add(drow)
' ''        '    Next
' ''        '    For j = 1 To val(txtllimit.Text)
' ''        '        drow = thetatable.NewRow
' ''        '        drow("spotvalue") = (val(interval) * j) + val(txtmid)
' ''        '        For k As Integer = 0 To i - 1
' ''        '            drow(Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MM/yyyy") & "") = 0 ' & vbCrLf & "" & Format(dtexp.Value, "dd/MMM/yyyy")) = 0
' ''        '        Next
' ''        '        thetatable.Rows.Add(drow)
' ''        '    Next





' ''        'Else
' ''        '    MsgBox("enter Value")
' ''        'End If
' ''    End Sub
' ''#End Region
' ''#Region "event"
' ''    Private Sub cmbresult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        REM In Scenario window For Selected Individual Greeks Chart and GridData on One tab,According to Button press it shows chart or Griddata
' ''        mAllCV = ""
' ''        result(True)

' ''        '  Me.tbcon.DrawMode = TabDrawMode.OwnerDrawFixed
' ''    End Sub
' ''    Private Sub result(ByVal ChangeFlg As Boolean)
' ''        'If val(txtllimit.Text) <= 0 Then
' ''        '    MsgBox("Enter Upper Limit")
' ''        '    txtllimit.Focus()
' ''        '    Exit Sub
' ''        'End If

' ''        If Val(txtllimit.Text) <= 0 Then
' ''            MsgBox("Enter No of Strike +/-  ")
' ''            txtllimit.Focus()
' ''            Exit Sub
' ''        End If
' ''        If Val(interval) <= 0 Then
' ''            MsgBox("Enter Interval.")
' ''            txtinterval.Focus()
' ''            Exit Sub
' ''        End If

' ''        'If val(txtmid) <= 0 Then
' ''        '    MsgBox("Enter Mid Value")
' ''        '    txtmid.Focus()
' ''        '    Exit Sub
' ''        'End If

' ''        REM For First Record Show Charts
' ''        If Val(txtmid) <= 0 Then
' ''            CalInterval()
' ''        End If

' ''        If Val(txtllimit.Text > 0) And Val(interval) > 0 And Val(txtmid) > 0 Then

' ''            init_table()
' ''            If cal_profit() = True Then
' ''                If ChangeFlg = True Then
' ''                    If tbcon.SelectedTab.Name = "tbprofitchart" Then
' ''                        SetResultButtonText(cmdresult, chtprofit, grdprofit, lblprofit, True)
' ''                    End If
' ''                Else
' ''                    If chtprofit.Visible = False Then chtprofit.Visible = True
' ''                End If
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_profit()
' ''            create_chart_profit_Full()

' ''            If cal_delta() = True Then
' ''                If ChangeFlg = True Then
' ''                    If tbcon.SelectedTab.Name = "tbchdelta" Then
' ''                        SetResultButtonText(cmdresult, chtdelta, grddelta, lbldelta, True)
' ''                    End If
' ''                End If
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_delta()
' ''            create_chart_delta_Full()
' ''            If cal_gamma() = True Then
' ''                If ChangeFlg = True Then
' ''                    If tbcon.SelectedTab.Name = "tbchgamma" Then
' ''                        SetResultButtonText(cmdresult, chtgamma, grdgamma, lblgamma, True)
' ''                    End If
' ''                End If
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_gamma()
' ''            Call create_chart_gamma_Full()

' ''            If cal_vega() = True Then
' ''                If ChangeFlg = True Then
' ''                    If tbcon.SelectedTab.Name = "tbchvega" Then
' ''                        SetResultButtonText(cmdresult, chtvega, grdvega, lblvega, True)
' ''                    End If
' ''                End If
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_vega()
' ''            create_chart_vega_Full()

' ''            If cal_theta() = True Then
' ''                If ChangeFlg = True Then
' ''                    If tbcon.SelectedTab.Name = "tbchtheta" Then
' ''                        SetResultButtonText(cmdresult, chttheta, grdtheta, lbltheta, True)
' ''                    End If
' ''                End If
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_theta()
' ''            create_chart_theta_Full()

' ''            If cal_volga() = True Then
' ''                If ChangeFlg = True Then
' ''                    If tbcon.SelectedTab.Name = "tbchvolga" Then
' ''                        SetResultButtonText(cmdresult, chtvolga, grdvolga, lblvolga, True)
' ''                    End If
' ''                End If
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_volga()
' ''            create_chart_volga_Full()


' ''            If cal_vanna() = True Then
' ''                If ChangeFlg = True Then
' ''                    If tbcon.SelectedTab.Name = "tbchvanna" Then
' ''                        SetResultButtonText(cmdresult, chtvanna, grdvanna, lblvanna, True)
' ''                    End If
' ''                End If
' ''            Else
' ''                Exit Sub
' ''            End If
' ''            create_chart_vanna()
' ''            create_chart_vanna_Full()

' ''            'If mAllCV Is Not Nothing Then
' ''            If mAllCV.Trim = "" Then
' ''                If CmdAllCV.Text = "Show &All Charts(F8)" Then
' ''                    CmdAllCV.Text = "Show &All Values(F8)"
' ''                    mAllCV = "Charts"
' ''                Else
' ''                    CmdAllCV.Text = "Show &All Charts(F8)"
' ''                    mAllCV = "Values"
' ''                End If
' ''            End If
' ''            'End If
' ''            'grdprofit.DataSource = profit
' ''            'grdprofit.Refresh()
' ''            'grddelta.DataSource = deltatable
' ''            'grddelta.Refresh()
' ''            'grdgamma.DataSource = gammatable
' ''            'grdgamma.Refresh()
' ''            'grdvega.DataSource = vegatable
' ''            'grdvega.Refresh()
' ''            'grdtheta.DataSource = thetatable
' ''            'grdtheta.Refresh()
' ''        End If
' ''    End Sub
' ''    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        Me.Close()
' ''        'Dim analysis As New analysis
' ''        ''analysis.MdiParent = Me
' ''        'analysis.ShowDialog()
' ''    End Sub

' ''    Private Sub txtdays_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub txtllimit_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        SendKeys.Send("{HOME}+{END}")
' ''    End Sub
' ''    Private Sub txtllimit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub
' ''    Private Sub txtmid_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub
' ''    Private Sub txtulimit_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub txtinterval_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        SendKeys.Send("{HOME}+{END}")
' ''    End Sub
' ''    Private Sub txtinterval_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub
' ''    Private Sub txtratediff_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub
' ''    Private Sub dttoday_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        txtdays.Text = 0
' ''        If grdtrad.Rows.Count = 2 Then
' ''            grdtrad.Rows(0).Cells("TimeI").Value = dttoday.Value
' ''        ElseIf frmFlg = True Then 'this flg used bcoz first need not to assign expiry date
' ''            For Each grow As DataGridViewRow In grdtrad.Rows
' ''                If grow.Cells("TimeI").ReadOnly = False Then
' ''                    grow.Cells("TimeI").Value = dttoday.Value
' ''                End If
' ''            Next
' ''        End If

' ''        If CDate(dttoday.Value.Date) <= CDate(dtexp.Value.Date) Then
' ''            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
' ''        End If
' ''        If dttoday.Value.Date < dtexp.Value.Date Then
' ''            REM This code commented because of Day difference plus 1 days display
' ''            'txtdays.Text = Val(txtdays.Text) + 1
' ''        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
' ''            txtdays.Text = Val(txtdays.Text) + 0.5
' ''        End If
' ''        create_active()
' ''    End Sub
' ''    Private Sub dtexp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        txtdays.Text = 0
' ''        If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            txtdays.Text = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
' ''        End If
' ''        If dttoday.Value.Date < dtexp.Value.Date Then
' ''            REM This code commented because of Day difference plus 1 days display
' ''            'txtdays.Text = Val(txtdays.Text) + 1
' ''        ElseIf dttoday.Value.Date = dtexp.Value.Date Then
' ''            txtdays.Text = Val(txtdays.Text) + 0.5
' ''        End If
' ''        create_active()
' ''    End Sub
' ''    Private Sub txtdays_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If txtdays.Text.Trim <> "" And txtdays.Text <> "0" Then
' ''            dtexp.Value = DateAdd(DateInterval.Day, CInt(txtdays.Text), dttoday.Value)
' ''        End If
' ''        create_active()
' ''    End Sub
' ''    Private Sub txtinterast_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        Mrateofinterast = Val(txtinterast.Text)
' ''    End Sub
' ''    Private Sub txtinterast_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub txtmkt_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        SendKeys.Send("{HOME}+{END}")
' ''    End Sub
' ''    Private Sub txtmkt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''        flgUnderline = True
' ''    End Sub
' ''    Private Sub txtmkt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If txtmkt.Text.Trim = "" Then
' ''            txtmkt.Text = 0
' ''        End If
' ''        If Val(txtmkt.Text) > 0 Then
' ''            If txtinterval.Text = 0 Then
' ''                Call CalInterval()
' ''            End If
' ''            'txtmid = Math.Floor(Val(txtmkt.Text))
' ''            ''If Val(txtmid) < 100 Then
' ''            ''    interval = 1
' ''            ''ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
' ''            ''    interval = 5
' ''            ''ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
' ''            ''    interval = 10
' ''            ''ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
' ''            ''    interval = 100
' ''            ''ElseIf Val(txtmid) > 10000 Then
' ''            ''    interval = 500
' ''            ''End If
' ''            If flgUnderline = False Then Exit Sub
' ''            If grdtrad.Rows.Count > 0 Then
' ''                For i As Integer = 0 To grdtrad.Rows.Count - 2
' ''                    Dim grow As DataGridViewRow
' ''                    grow = grdtrad.Rows(i)
' ''                    grow.Cells("spval").Value = CDbl(txtmkt.Text)
' ''                    If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
' ''                        'If IsDBNull(grow.Cells(4).Value) Or grow.Cells(4).Value Is Nothing Or CStr(grow.Cells(4).Value) = "" Then
' ''                        grow.Cells("last").Value = CDbl(txtmkt.Text)
' ''                        'End If
' ''                    End If
' ''                    cal(7, grow, True)
' ''                Next
' ''                grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("spval").Value = CDbl(txtmkt.Text)
' ''                grdtrad.EndEdit()
' ''                ' result()
' ''            End If
' ''        End If
' ''        Call cal_summary()
' ''        flgUnderline = False
' ''    End Sub

' ''    Private Sub cmdcheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If grdact.ColumnCount > 0 Then
' ''            If grdact.RowCount > 0 Then
' ''                If checkall = True Then
' ''                    checkall = False
' ''                    For Each col As DataGridViewColumn In grdact.Columns
' ''                        grdact.Rows(0).Cells(col.Index).Value = False
' ''                    Next
' ''                    cmdcheck.Text = "CheckAll"
' ''                Else
' ''                    checkall = True
' ''                    For Each col As DataGridViewColumn In grdact.Columns
' ''                        grdact.Rows(0).Cells(col.Index).Value = True
' ''                    Next
' ''                    cmdcheck.Text = "ClearAll"
' ''                End If

' ''            End If
' ''        End If

' ''    End Sub
' ''    Private Sub CmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        export()
' ''    End Sub

' ''    Private Sub chkcheck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

' ''        If grdtrad.RowCount > 1 Then
' ''            If chkcheck.Checked = False Then
' ''                gcheck = False
' ''                For i As Integer = 0 To grdtrad.RowCount - 2
' ''                    grdtrad.Rows(i).Cells("Active").Value = False
' ''                Next
' ''                'For Each grow As DataGridViewRow In grdtrad.Rows
' ''                '    grow.Cells(0).Value = False
' ''                'Next
' ''            Else
' ''                gcheck = True
' ''                For i As Integer = 0 To grdtrad.RowCount - 2
' ''                    grdtrad.Rows(i).Cells("Active").Value = True
' ''                Next
' ''                'For Each grow As DataGridViewRow In grdtrad.Rows
' ''                '    grow.Cells(0).Value = True
' ''                'Next
' ''            End If


' ''        End If

' ''    End Sub
' ''    Private Sub cmdexp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If grdtrad.Rows.Count > 1 Then
' ''            cal_exp()
' ''        End If
' ''    End Sub
' ''    Private Sub cmdimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        Me.Cursor = Cursors.WaitCursor
' ''        import()
' ''        Me.Cursor = Cursors.Default
' ''    End Sub

' ''    Private Sub txtcvol_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        SendKeys.Send("{HOME}+{END}")
' ''    End Sub

' ''    Private Sub txtcvol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If txtcvol.Text.Trim = "" Then
' ''            txtcvol.Text = 0
' ''        End If
' ''        If Val(txtcvol.Text) > 0 Then
' ''            If grdtrad.Rows.Count > 0 Then
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    If grow.Cells("CPF").Value = "C" Then
' ''                        If grow.Cells("lv").ReadOnly = False Then
' ''                            grow.Cells("lv").Value = Val(txtcvol.Text)
' ''                        End If
' ''                        Dim mt As Integer
' ''                        Dim mmt As Integer
' ''                        Dim futval As Double

' ''                        mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
' ''                        mmt = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date) - 1

' ''                        futval = Val(grow.Cells("spval").Value)
' ''                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, True, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
' ''                        cal_summary()
' ''                    End If
' ''                Next
' ''                'If grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("CPF").Value = "C" Then
' ''                REM When user changes vol in top box, it should be automatically applied to first line vol as well.
' ''                grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("lv").Value = CDbl(txtcvol.Text)
' ''                grdtrad.EndEdit()
' ''                'End If
' ''            End If
' ''        End If
' ''    End Sub
' ''    Private Sub txtcvol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub txtpvol_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        SendKeys.Send("{HOME}+{END}")
' ''    End Sub
' ''    Private Sub txtpvol_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If txtpvol.Text.Trim = "" Then
' ''            txtpvol.Text = 0
' ''        End If
' ''        If Val(txtpvol.Text) > 0 Then
' ''            If grdtrad.Rows.Count > 0 Then
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    If grow.Cells("CPF").Value = "P" Then
' ''                        If grow.Cells("lv").ReadOnly = False Then
' ''                            grow.Cells("lv").Value = Val(txtpvol.Text)
' ''                        End If
' ''                        Dim mt As Integer
' ''                        Dim mmt As Integer
' ''                        Dim futval As Double

' ''                        mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
' ''                        mmt = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date) - 1

' ''                        futval = Val(grow.Cells("spval").Value)
' ''                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, False, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
' ''                        cal_summary()
' ''                    End If
' ''                Next
' ''                'grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("lv").Value = CDbl(txtpvol.Text)
' ''                grdtrad.EndEdit()
' ''            End If
' ''        End If
' ''    End Sub
' ''    Private Sub txtpvol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub tbcon_DrawItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs)
' ''        Dim g As Graphics = e.Graphics
' ''        Dim tp As TabPage = tbcon.TabPages(e.Index)
' ''        Dim br As System.Drawing.Brush
' ''        Dim sf As New StringFormat
' ''        Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width + 2, e.Bounds.Height - 2)
' ''        sf.Alignment = StringAlignment.Near
' ''        Dim strTitle As String = tp.Text
' ''        If tbcon.SelectedIndex = e.Index Then
' ''            Dim f As Font = New Font(tbcon.Font.Name, tbcon.Font.Size, FontStyle.Regular, tbcon.Font.Unit)
' ''            br = New SolidBrush(Color.Black)
' ''            g.FillRectangle(br, e.Bounds)
' ''            '  g.FillRectangle(br, e.Bounds)
' ''            br = New SolidBrush(Color.White)
' ''            g.DrawString(strTitle, f, br, r, sf)


' ''        Else
' ''            Dim f As Font = New Font(tbcon.Font.Name, tbcon.Font.Size, FontStyle.Regular, tbcon.Font.Unit)
' ''            br = New SolidBrush(Color.WhiteSmoke)
' ''            g.FillRectangle(br, e.Bounds)
' ''            'g.FillRectangle(br, e.Bounds)
' ''            br = New SolidBrush(Color.Black)
' ''            g.DrawString(strTitle, f, br, r, sf)

' ''        End If
' ''        tp.Refresh()
' ''    End Sub
' ''    Private Sub txtunits_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtunits.Text) > 0 Then
' ''            txtunits.BackColor = Color.MediumSeaGreen
' ''            txtunits.ForeColor = Color.White
' ''        ElseIf Val(txtunits.Text) < 0 Then
' ''            txtunits.BackColor = Color.DarkOrange
' ''            txtunits.ForeColor = Color.White
' ''        Else
' ''            txtunits.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtunits.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtdelval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtdelval.Text) > 0 Then
' ''            txtdelval.BackColor = Color.MediumSeaGreen
' ''            txtdelval.ForeColor = Color.White
' ''        ElseIf Val(txtdelval.Text) < 0 Then
' ''            txtdelval.BackColor = Color.DarkOrange
' ''            txtdelval.ForeColor = Color.White
' ''        Else
' ''            txtdelval.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtdelval.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtgmval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtgmval.Text) > 0 Then
' ''            txtgmval.BackColor = Color.MediumSeaGreen
' ''            txtgmval.ForeColor = Color.White
' ''        ElseIf Val(txtgmval.Text) < 0 Then
' ''            txtgmval.BackColor = Color.DarkOrange
' ''            txtgmval.ForeColor = Color.White
' ''        Else
' ''            txtgmval.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtgmval.ForeColor = Color.Black

' ''        End If
' ''    End Sub
' ''    Private Sub txtvgval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtvgval.Text) > 0 Then
' ''            txtvgval.BackColor = Color.MediumSeaGreen
' ''            txtvgval.ForeColor = Color.White
' ''        ElseIf Val(txtvgval.Text) < 0 Then
' ''            txtvgval.BackColor = Color.DarkOrange
' ''            txtvgval.ForeColor = Color.White
' ''        Else
' ''            txtvgval.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtvgval.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtthval_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtthval.Text) > 0 Then
' ''            txtthval.BackColor = Color.MediumSeaGreen
' ''            txtthval.ForeColor = Color.White
' ''        ElseIf Val(txtthval.Text) < 0 Then
' ''            txtthval.BackColor = Color.DarkOrange
' ''            txtthval.ForeColor = Color.White
' ''        Else
' ''            txtthval.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtthval.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtunits1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtunits1.Text) > 0 Then
' ''            txtunits1.BackColor = Color.MediumSeaGreen
' ''            txtunits1.ForeColor = Color.White
' ''        ElseIf Val(txtunits1.Text) < 0 Then
' ''            txtunits1.BackColor = Color.DarkOrange
' ''            txtunits1.ForeColor = Color.White
' ''        Else
' ''            txtunits1.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtunits1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtdelval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtdelval1.Text) > 0 Then
' ''            txtdelval1.BackColor = Color.MediumSeaGreen
' ''            txtdelval1.ForeColor = Color.White
' ''        ElseIf Val(txtdelval1.Text) < 0 Then
' ''            txtdelval1.BackColor = Color.DarkOrange
' ''            txtdelval1.ForeColor = Color.White
' ''        Else
' ''            txtdelval1.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtdelval1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtgmval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtgmval1.Text) > 0 Then
' ''            txtgmval1.BackColor = Color.MediumSeaGreen
' ''            txtgmval1.ForeColor = Color.White
' ''        ElseIf Val(txtgmval1.Text) < 0 Then
' ''            txtgmval1.BackColor = Color.DarkOrange
' ''            txtgmval1.ForeColor = Color.White
' ''        Else
' ''            txtgmval1.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtgmval1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtvgval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtvgval1.Text) > 0 Then
' ''            txtvgval1.BackColor = Color.MediumSeaGreen
' ''            txtvgval1.ForeColor = Color.White
' ''        ElseIf Val(txtvgval1.Text) < 0 Then
' ''            txtvgval1.BackColor = Color.DarkOrange
' ''            txtvgval1.ForeColor = Color.White
' ''        Else
' ''            txtvgval1.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtvgval1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtthval1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtthval1.Text) > 0 Then
' ''            txtthval1.BackColor = Color.MediumSeaGreen
' ''            txtthval1.ForeColor = Color.White
' ''        ElseIf Val(txtthval1.Text) < 0 Then
' ''            txtthval1.BackColor = Color.DarkOrange
' ''            txtthval1.ForeColor = Color.White
' ''        Else
' ''            txtthval1.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtthval1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtVolgaVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(txtVolgaVal.Text) > 0 Then
' ''            txtVolgaVal.BackColor = Color.MediumSeaGreen
' ''            txtVolgaVal.ForeColor = Color.White
' ''        ElseIf Val(txtVolgaVal.Text) < 0 Then
' ''            txtVolgaVal.BackColor = Color.DarkOrange
' ''            txtVolgaVal.ForeColor = Color.White
' ''        Else
' ''            txtVolgaVal.BackColor = Color.FromArgb(255, 255, 128)
' ''            txtVolgaVal.ForeColor = Color.Black
' ''        End If
' ''    End Sub

' ''    Private Sub TxtVolgaVal1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(TxtVolgaVal1.Text) > 0 Then
' ''            TxtVolgaVal1.BackColor = Color.MediumSeaGreen
' ''            TxtVolgaVal1.ForeColor = Color.White
' ''        ElseIf Val(TxtVolgaVal1.Text) < 0 Then
' ''            TxtVolgaVal1.BackColor = Color.DarkOrange
' ''            TxtVolgaVal1.ForeColor = Color.White
' ''        Else
' ''            TxtVolgaVal1.BackColor = Color.FromArgb(255, 255, 128)
' ''            TxtVolgaVal1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub TxtVannaVal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(TxtVannaVal.Text) > 0 Then
' ''            TxtVannaVal.BackColor = Color.MediumSeaGreen
' ''            TxtVannaVal.ForeColor = Color.White
' ''        ElseIf Val(TxtVannaVal.Text) < 0 Then
' ''            TxtVannaVal.BackColor = Color.DarkOrange
' ''            TxtVannaVal.ForeColor = Color.White
' ''        Else
' ''            TxtVannaVal.BackColor = Color.FromArgb(255, 255, 128)
' ''            TxtVannaVal.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub TxtVannaVal1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If Val(TxtVannaVal1.Text) > 0 Then
' ''            TxtVannaVal1.BackColor = Color.MediumSeaGreen
' ''            TxtVannaVal1.ForeColor = Color.White
' ''        ElseIf Val(TxtVannaVal1.Text) < 0 Then
' ''            TxtVannaVal1.BackColor = Color.DarkOrange
' ''            TxtVannaVal1.ForeColor = Color.White
' ''        Else
' ''            TxtVannaVal1.BackColor = Color.FromArgb(255, 255, 128)
' ''            TxtVannaVal1.ForeColor = Color.Black
' ''        End If
' ''    End Sub
' ''    Private Sub txtinterval_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        REM When scenario calculation passes value as negative in ltp, then it gives an overflow in values,For this Set Calulation Of Interval And No Of Strikes, multiplication of interval and no Of Strike must be less than CMP
' ''        If chkint.Checked = True Then
' ''            interval = Val(txtinterval.Text) * 100
' ''        Else
' ''            interval = Val(txtinterval.Text)
' ''        End If
' ''        If Val(txtmkt.Text) > 0 And Val(txtinterval.Text) > 0 And Val(txtllimit.Text) > 0 Then
' ''            If Val(txtinterval.Text) > Math.Round(Val(txtmkt.Text) / Val(txtllimit.Text)) Then
' ''                MsgBox("Interval Must be Less Or Equal To " & Math.Round(Val(txtmkt.Text) / Val(txtllimit.Text), 0), MsgBoxStyle.Information)
' ''                txtinterval.Focus()
' ''                Exit Sub
' ''            End If
' ''        End If
' ''    End Sub
' ''#End Region
' ''#Region "Grid Event"
' ''    Private Sub cal(ByVal cno As Integer, ByVal currow As DataGridViewRow, Optional ByVal check As Boolean = False)
' ''        Dim iscall As Boolean = False
' ''        Dim zero As Double = 0
' ''        Dim one As Double = 1
' ''        Dim count As Integer
' ''        count = 0
' ''        Dim futval As Double
' ''        Dim brate As Double = 0
' ''        Dim srate As Double = 0
' ''        futval = 0
' ''        Dim sumunit As Double = 0
' ''        Dim mt As Double = 0
' ''        Dim mmt As Double = 0
' ''        Dim grow As DataGridViewRow
' ''        If check = True Then
' ''            grow = currow
' ''        Else
' ''            grow = grdtrad.CurrentRow
' ''        End If
' ''        If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
' ''            grow.Cells("CPF").Value = CStr("C")
' ''        Else
' ''            grow.Cells("CPF").Value = UCase(grow.Cells("CPF").Value)
' ''            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
' ''                grow.Cells("delta").Value = one
' ''            Else
' ''                grow.Cells("delta").Value = zero
' ''            End If
' ''        End If

' ''        grdtrad.EndEdit()
' ''        'If IsDBNull(grow.Cells(2).Value) Or Not IsDate(grow.Cells(2).Value) Then
' ''        '    MsgBox("Enter Maturity date")
' ''        '    Exit Sub
' ''        'End If
' ''        If Not IsDBNull(grow.Cells("CPF").Value) And Not grow.Cells("CPF").Value Is Nothing Then
' ''            If UCase(grow.Cells("CPF").Value) = "F" Or UCase(grow.Cells("CPF").Value) = "E" Then
' ''                grow.Cells("deltaval").Value = grow.Cells("units").Value
' ''                grow.Cells("delta").Value = Math.Round(one, 2)
' ''                grow.Cells("deltaval").Value = Math.Round(Val(grow.Cells("deltaval").Value), 2)
' ''                grow.Cells("Strike").Value = Math.Round(zero, 2)
' ''                If Val(grow.Cells("last").Value) = 0 Then
' ''                    If Val(grow.Cells("spval").Value) <> 0 Then
' ''                        grow.Cells("last").Value = Math.Round(CDbl(grow.Cells("spval").Value), 2)
' ''                    Else
' ''                        grow.Cells("last").Value = Math.Round(zero, 2)
' ''                    End If
' ''                End If
' ''                grow.Cells("lv").Value = Math.Round(zero, 2)
' ''                grow.Cells("gamma").Value = Math.Round(zero, 2)
' ''                grow.Cells("gmval").Value = Math.Round(zero, 2)
' ''                grow.Cells("vega").Value = Math.Round(zero, 2)
' ''                grow.Cells("vgval").Value = Math.Round(zero, 2)
' ''                grow.Cells("theta").Value = Math.Round(zero, 2)
' ''                grow.Cells("thetaval").Value = Math.Round(zero, 2)


' ''                grow.Cells("gamma1").Value = Math.Round(zero, 2)
' ''                grow.Cells("gmval1").Value = Math.Round(zero, 2)
' ''                grow.Cells("vega1").Value = Math.Round(zero, 2)
' ''                grow.Cells("vgval1").Value = Math.Round(zero, 2)
' ''                grow.Cells("theta").Value = Math.Round(zero, 2)
' ''                grow.Cells("thetaval1").Value = Math.Round(zero, 2)

' ''                grow.Cells("volga").Value = Math.Round(zero, 2)
' ''                grow.Cells("volgaval1").Value = Math.Round(zero, 2)
' ''                grow.Cells("vanna").Value = Math.Round(zero, 2)
' ''                grow.Cells("vannaval1").Value = Math.Round(zero, 2)

' ''                grdtrad.EndEdit()
' ''            Else
' ''                futval = Val(grow.Cells("spval").Value)
' ''                If IsDBNull(grow.Cells("TimeI").Value) Then
' ''                    grow.Cells("TimeI").Value = dttoday.Value.Date
' ''                    'ElseIf grow.Cells("TimeI").Value.ToString.Trim = "" Then
' ''                    '    grow.Cells("TimeI").Value = dttoday.Value.Date
' ''                ElseIf grow.Cells("TimeI").Value = Nothing Then
' ''                    grow.Cells("TimeI").Value = dttoday.Value.Date
' ''                End If
' ''                If IsDBNull(grow.Cells("TimeII").Value) Then
' ''                    grow.Cells("TimeII").Value = dtexp.Value.Date
' ''                    ' ElseIf grow.Cells("TimeII").Value.ToString.Trim = "" Then
' ''                ElseIf grow.Cells("TimeII").Value = Nothing Then
' ''                    grow.Cells("TimeII").Value = dtexp.Value.Date
' ''                End If
' ''                If CDate(grow.Cells("TimeI").Value) < CDate(grow.Cells("TimeII").Value) Then
' ''                    mt = (DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date))
' ''                    'mmt = DateDiff(DateInterval.Day, DateAdd(DateInterval.Day, CInt(txtdays.Text), Now.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                    'Else
' ''                    '    mt = DateDiff(DateInterval.Day, CDate(grow.Cells(1).Value), CDate(grow.Cells(2).Value))
' ''                End If
' ''                If CDate(grow.Cells("TimeI").Value).Date = CDate(grow.Cells("TimeII").Value).Date Then
' ''                    mt = 0.5
' ''                End If

' ''                'mt = DateDiff(DateInterval.Day, CDate(drow("mdate")), Now())
' ''                If grow.Cells("CPF").Value = "C" Then
' ''                    iscall = True
' ''                Else
' ''                    iscall = False
' ''                End If
' ''                If futval <> 0 Then
' ''                    If cno = 9 Then
' ''                        CalDatastkprice(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, iscall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
' ''                        '' CalData(futval, Val(grow.Cells(5).Value), Val(grow.Cells(7).Value), mt, iscall, True, grow, Val(grow.Cells(6).Value), (Val(grow.Cells(8).Value) / 100))
' ''                    Else
' ''                        CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, iscall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
' ''                        isVolCal = True
' ''                    End If
' ''                End If
' ''            End If
' ''        End If
' ''        txtunits.Text = 0
' ''        txtdelval.Text = 0
' ''        txtthval.Text = 0
' ''        txtvgval.Text = 0
' ''        txtgmval.Text = 0
' ''        txtVolgaVal.Text = 0
' ''        TxtVannaVal.Text = 0

' ''        ''divyesh
' ''        txtunits1.Text = 0
' ''        txtdelval1.Text = 0
' ''        txtthval1.Text = 0
' ''        txtvgval1.Text = 0
' ''        txtgmval1.Text = 0
' ''        TxtVolgaVal1.Text = 0
' ''        TxtVannaVal1.Text = 0

' ''        If grdtrad.Rows.Count > 1 Then
' ''            For Each row As DataGridViewRow In grdtrad.Rows
' ''                If row.Cells("Active").Value = True Then
' ''                    txtdelval.Text = Format(Val(txtdelval.Text) + Val(row.Cells("deltaval").Value), DecimalSetting.sDeltaval)
' ''                    txtthval.Text = Format(Val(txtthval.Text) + Val(row.Cells("thetaval").Value), DecimalSetting.sThetaval)
' ''                    txtvgval.Text = Format(Val(txtvgval.Text) + Val(row.Cells("vgval").Value), DecimalSetting.sVegaval)
' ''                    txtgmval.Text = Format(Val(txtgmval.Text) + Val(row.Cells("gmval").Value), DecimalSetting.sGammaval)
' ''                    txtVolgaVal.Text = Format(Val(txtVolgaVal.Text) + Val(row.Cells("volgaval").Value), DecimalSetting.sVolgaval)
' ''                    TxtVannaVal.Text = Format(Val(TxtVannaVal.Text) + Val(row.Cells("vannaval").Value), DecimalSetting.sVannaval)

' ''                    ''divyesh
' ''                    txtdelval1.Text = Format(Val(txtdelval1.Text) + Val(row.Cells("deltaval1").Value), DecimalSetting.sDeltaval)
' ''                    txtthval1.Text = Format(Val(txtthval1.Text) + Val(row.Cells("thetaval1").Value), DecimalSetting.sThetaval)
' ''                    txtvgval1.Text = Format(Val(txtvgval1.Text) + Val(row.Cells("vgval1").Value), DecimalSetting.sVegaval)
' ''                    txtgmval1.Text = Format(Val(txtgmval1.Text) + Val(row.Cells("gmval1").Value), DecimalSetting.sGammaval)
' ''                    TxtVolgaVal1.Text = Format(Val(TxtVolgaVal1.Text) + Val(row.Cells("volgaval1").Value), DecimalSetting.sVolgaval)
' ''                    TxtVannaVal1.Text = Format(Val(TxtVannaVal1.Text) + Val(row.Cells("vannaval1").Value), DecimalSetting.sVannaval)
' ''                End If
' ''            Next

' ''            txtunits.Text = Math.Round(cal_profitLoss, RoundGrossMTM)
' ''            ''divyesh
' ''            txtunits1.Text = Math.Round(cal_FutprofitLoss, RoundGrossMTM)
' ''        End If
' ''    End Sub
' ''    Private Sub grdtrad_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
' ''        'If (e.ColumnIndex = 5 Or e.ColumnIndex = 6 Or e.ColumnIndex =9) Then
' ''        '    cal()
' ''        'Else
' ''        If VarIsFirstLoad = True Then
' ''            VarIsFirstLoad = False
' ''            Exit Sub
' ''        End If
' ''        Dim zero As Double = 0
' ''        Dim one As Double = 1
' ''        If grdtrad.Columns(e.ColumnIndex).Name = "TimeI" Then
' ''            'ElseIf grdtrad.Columns(e.ColumnIndex).Name = "TimeII" Then
' ''            '    grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "CPF" Then
' ''            Dim grow As DataGridViewRow
' ''            grow = grdtrad.CurrentRow
' ''            If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
' ''                grow.Cells("CPF").Value = "C"
' ''            Else
' ''                Dim arr As New ArrayList
' ''                arr.Add("C")
' ''                arr.Add("F")
' ''                arr.Add("P")
' ''                arr.Add("E")
' ''                If Not arr.Contains(UCase(grow.Cells("CPF").Value)) Then
' ''                    MsgBox("Enter 'C','P','F' or 'E'.")
' ''                    grdtrad.Rows(e.RowIndex).Cells("CPF").Selected = True
' ''                    Exit Sub
' ''                End If
' ''                grow.Cells("CPF").Value = UCase(grow.Cells("CPF").Value)
' ''                If Not IsDBNull(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
' ''                    If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "C" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "P" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
' ''                        If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
' ''                            grdtrad.Rows(e.RowIndex).Cells("delta").Value = one
' ''                            grdtrad.Rows(e.RowIndex).Cells("lv").Value = zero
' ''                            grdtrad.Rows(e.RowIndex).Cells("last").Value = CDbl(txtmkt.Text)
' ''                        Else
' ''                            grdtrad.Rows(e.RowIndex).Cells("delta").Value = zero
' ''                            If grow.Cells("lv").Value <> "0" Then
' ''                                If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "C" Then
' ''                                    If txtcvol.Text.Trim <> "" Then
' ''                                        grow.Cells("lv").Value = Val(txtcvol.Text)
' ''                                    Else
' ''                                        grow.Cells("lv").Value = zero
' ''                                    End If
' ''                                    ' grdtrad.Rows(e.RowIndex).Cells(8).Value = val(txtcvol.Text)
' ''                                Else
' ''                                    If txtpvol.Text.Trim <> "" Then
' ''                                        grow.Cells("lv").Value = Val(txtpvol.Text)
' ''                                    Else
' ''                                        grow.Cells("lv").Value = zero
' ''                                    End If
' ''                                    'grdtrad.Rows(e.RowIndex).Cells(8).Value = val(txtpvol.Text)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Else
' ''                        'MsgBox("Enter 'C','P' or 'F'")
' ''                        grdtrad.Rows(e.RowIndex).Cells("CPF").Selected = True
' ''                        Exit Sub
' ''                    End If
' ''                End If
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "spval" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("spval").Value) Or grdtrad.Rows(e.RowIndex).Cells("spval").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("spval").Value) = "" Then
' ''                If UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
' ''                    grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
' ''                Else
' ''                    grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
' ''                End If
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "Strike" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("Strike").Value) Or grdtrad.Rows(e.RowIndex).Cells("Strike").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("Strike").Value) = "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("Strike").Value = zero
' ''            Else
' ''                grdtrad.Rows(e.RowIndex).Cells("Strike").Value = Math.Abs(CDbl(grdtrad.Rows(e.RowIndex).Cells("Strike").Value))
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "units" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("units").Value) Or grdtrad.Rows(e.RowIndex).Cells("units").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("units").Value) = "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("units").Value = zero
' ''            ElseIf UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "F" Or UCase(grdtrad.Rows(e.RowIndex).Cells("CPF").Value) = "E" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("deltaval").Value = grdtrad.Rows(e.RowIndex).Cells("units").Value
' ''                'to calculate deltaval
' ''                cal_summary()
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "ltp" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("ltp").Value) Or grdtrad.Rows(e.RowIndex).Cells("ltp").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("ltp").Value) = "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("ltp").Value = zero
' ''            End If
' ''            ''divyesh
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("ltp1").Value) Or grdtrad.Rows(e.RowIndex).Cells("ltp").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("ltp1").Value) = "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("ltp1").Value = zero
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "last" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("last").Value) Or grdtrad.Rows(e.RowIndex).Cells("last").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("last").Value) = "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("last").Value = zero
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "lv" Then
' ''            If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("lv").Value) Or grdtrad.Rows(e.RowIndex).Cells("lv").Value Is Nothing Or CStr(grdtrad.Rows(e.RowIndex).Cells("lv").Value) = "" Then
' ''                zero = 0.01
' ''                grdtrad.Rows(e.RowIndex).Cells("lv").Value = zero
' ''                zero = 0
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "Active" Then

' ''            Call cal_summary()

' ''            grdtrad.EndEdit()

' ''            Dim grow As DataGridViewRow
' ''            grow = grdtrad.CurrentRow

' ''            If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
' ''                grow.Cells("CPF").Value = CStr("C")
' ''            End If
' ''            If IsDBNull(grow.Cells("spval").Value) Or grow.Cells("spval").Value Is Nothing Then
' ''                grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
' ''            Else
' ''                grow.Cells("spval").Value = CDbl(grow.Cells("spval").Value)
' ''            End If
' ''            If IsDBNull(grow.Cells("Strike").Value) Or grow.Cells("Strike").Value Is Nothing Then
' ''                grow.Cells("Strike").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("units").Value) Or grow.Cells("units").Value Is Nothing Then
' ''                grow.Cells("units").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("last").Value) Or grow.Cells("last").Value Is Nothing Then
' ''                grow.Cells("last").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("lv").Value) Or grow.Cells("lv").Value Is Nothing Then
' ''                If txtcvol.Text.Trim <> "" Then
' ''                    grow.Cells("lv").Value = CDbl(txtcvol.Text)
' ''                Else
' ''                    grow.Cells("lv").Value = zero
' ''                End If
' ''            End If
' ''            If IsDBNull(grow.Cells("delta").Value) Or grow.Cells("delta").Value Is Nothing Then
' ''                grow.Cells("delta").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("deltaval").Value) Or grow.Cells("deltaval").Value Is Nothing Then
' ''                grow.Cells("deltaval").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("gamma").Value) Or grow.Cells("gamma").Value Is Nothing Then
' ''                grow.Cells("gamma").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("gmval").Value) Or grow.Cells("gmval").Value Is Nothing Then
' ''                grow.Cells("gmval").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vega").Value) Or grow.Cells("vega").Value Is Nothing Then
' ''                grow.Cells("vega").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vgval").Value) Or grow.Cells("vgval").Value Is Nothing Then
' ''                grow.Cells("vgval").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("theta").Value) Or grow.Cells("theta").Value Is Nothing Then
' ''                grow.Cells("theta").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("thetaval").Value) Or grow.Cells("thetaval").Value Is Nothing Then
' ''                grow.Cells("thetaval").Value = zero
' ''            End If

' ''            If IsDBNull(grow.Cells("volga").Value) Or grow.Cells("volga").Value Is Nothing Then
' ''                grow.Cells("volga").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("volgaval").Value) Or grow.Cells("volgaval").Value Is Nothing Then
' ''                grow.Cells("volgaval").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vanna").Value) Or grow.Cells("vanna").Value Is Nothing Then
' ''                grow.Cells("vanna").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vannaval").Value) Or grow.Cells("vannaval").Value Is Nothing Then
' ''                grow.Cells("vannaval").Value = zero
' ''            End If

' ''            ''divyesh
' ''            If IsDBNull(grow.Cells("delta1").Value) Or grow.Cells("delta1").Value Is Nothing Then
' ''                grow.Cells("delta1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("gamma1").Value) Or grow.Cells("gamma1").Value Is Nothing Then
' ''                grow.Cells("gamma1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vega1").Value) Or grow.Cells("vega1").Value Is Nothing Then
' ''                grow.Cells("vega1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("theta1").Value) Or grow.Cells("theta1").Value Is Nothing Then
' ''                grow.Cells("theta1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vgval1").Value) Or grow.Cells("vgval").Value Is Nothing Then
' ''                grow.Cells("vgval1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("gmval1").Value) Or grow.Cells("gmval").Value Is Nothing Then
' ''                grow.Cells("gmval1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("thetaval1").Value) Or grow.Cells("thetaval").Value Is Nothing Then
' ''                grow.Cells("thetaval1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("deltaval1").Value) Or grow.Cells("deltaval").Value Is Nothing Then
' ''                grow.Cells("deltaval1").Value = zero
' ''            End If

' ''            If IsDBNull(grow.Cells("volgaval1").Value) Or grow.Cells("volgaval").Value Is Nothing Then
' ''                grow.Cells("volgaval1").Value = zero
' ''            End If
' ''            If IsDBNull(grow.Cells("vannaval1").Value) Or grow.Cells("vannaval").Value Is Nothing Then
' ''                grow.Cells("vannaval1").Value = zero
' ''            End If


' ''            grow.Cells("uid").Value = grdtrad.Rows.Count - 1
' ''            'grdtrad.Refresh()
' ''            'to attch context menu to new row
' ''            'If (grdtrad.Rows(e.RowIndex).Cells("ltp").Value = 0) Then

' ''            'REM 1 create contextmenustrip from vol column cells
' ''            ReDim contMenu(grdtrad.Rows.Count - 1)
' ''            contMenu(grdtrad.Rows.Count - 1) = New ContextMenuStrip
' ''            'contMenu(grdtrad.Rows.Count - 1).ShowImageMargin = False
' ''            Dim item As New ToolStripMenuItem("Unfreeze")
' ''            ' item.CheckOnClick = True
' ''            item.Tag = grdtrad.Rows.Count - 1
' ''            AddHandler item.Click, AddressOf freezVol
' ''            AddHandler contMenu(grdtrad.Rows.Count - 1).Opening, AddressOf contMenuOpen
' ''            contMenu(grdtrad.Rows.Count - 1).Items.Add(item)
' ''            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("lv").ContextMenuStrip = contMenu(grdtrad.Rows.Count - 1)
' ''            REM 1: end

' ''            'REM 1 create contextmenustrip from Expiry column cells
' ''            ReDim contMenuExpiry(grdtrad.Rows.Count - 1)
' ''            contMenuExpiry(grdtrad.Rows.Count - 1) = New ContextMenuStrip
' ''            'contMenuExpiry(grdtrad.Rows.Count - 1).ShowImageMargin = False
' ''            item = New ToolStripMenuItem("Unfreeze")
' ''            ' item.CheckOnClick = True
' ''            item.Tag = grdtrad.Rows.Count - 1
' ''            AddHandler item.Click, AddressOf freezExpiry
' ''            AddHandler contMenu(grdtrad.Rows.Count - 1).Opening, AddressOf contMenuOpenExpiry
' ''            contMenuExpiry(grdtrad.Rows.Count - 1).Items.Add(item)
' ''            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells("TimeII").ContextMenuStrip = contMenuExpiry(grdtrad.Rows.Count - 1)
' ''            REM 1: end
' ''        End If
' ''        grdtrad.EndEdit()
' ''        If grdtrad.Columns(e.ColumnIndex).Name = "lv" Then
' ''            If UCase(grdtrad.CurrentRow.Cells("CPF").Value) = "F" Or UCase(grdtrad.CurrentRow.Cells("CPF").Value) = "E" Then
' ''                If Val(grdtrad.CurrentRow.Cells("last").Value) <> 0 Then
' ''                    cal(8, grdtrad.CurrentRow)
' ''                End If
' ''            Else
' ''                If Val(grdtrad.CurrentRow.Cells("Strike").Value) > 0 Then
' ''                    cal(8, grdtrad.CurrentRow)
' ''                End If
' ''            End If
' ''        ElseIf grdtrad.Columns(e.ColumnIndex).Name = "last" Then
' ''            cal(9, grdtrad.CurrentRow)
' ''        Else
' ''            If Val(grdtrad.CurrentRow.Cells("Strike").Value) > 0 And Val(grdtrad.CurrentRow.Cells("lv").Value) <> 0 Then
' ''                cal(0, grdtrad.CurrentRow)
' ''            End If

' ''        End If
' ''        'result(False)
' ''    End Sub
' ''    Private Sub grdtrad_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)
' ''        AddHandler e.Control.KeyPress, AddressOf CheckCell

' ''        'AddHandler e.Control.KeyDown, AddressOf cellkeydown
' ''    End Sub
' ''    Private Sub grdtrad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
' ''        Dim KeyCode As Short = e.KeyCode
' ''        Dim Shift As Short = e.KeyData \ &H10000

' ''        If e.KeyCode = Keys.F2 Then
' ''            If (grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "TimeI" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "TimeII" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "CPF" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "spval" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "Strike" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "units" Or grdtrad.Columns(grdtrad.CurrentCell.ColumnIndex).Name = "ltp") Then
' ''                grdtrad.CurrentCell.KeyEntersEditMode(e)
' ''            End If
' ''        ElseIf e.KeyCode = Keys.Escape Then
' ''            If grdtrad.CurrentRow.IsNewRow Then
' ''                grdtrad.CancelEdit()
' ''            Else
' ''                grdtrad.Rows.RemoveAt(grdtrad.CurrentRow.Index)
' ''            End If
' ''            'grdtrad.Rows.RemoveAt(grdtrad.CurrentRow.Index)
' ''            grdtrad.CancelEdit()
' ''            'grdtrad.BindingContext.Item(grdtrad.DataSource).CancelCurrentEdit()

' ''        ElseIf e.KeyCode = Keys.Tab Then
' ''            If grdtrad.CurrentCell.OwningColumn.Index = 4 Then
' ''                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Or grdtrad.CurrentRow.Cells("CPF").Value = "E" Then
' ''                    Me.grdtrad.CurrentCell = _
' ''                                             Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index).Cells("units")

' ''                    e.SuppressKeyPress = True
' ''                End If
' ''                'ElseIf grdtrad.CurrentCell.OwningColumn.Index = 9 Then
' ''                '    If grdtrad.CurrentRow.Cells(3).Value = "F" Then
' ''                '        Me.grdtrad.CurrentCell = _
' ''                '                                  Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells(0)

' ''                '        e.SuppressKeyPress = True
' ''                '    End If
' ''            ElseIf grdtrad.CurrentCell.OwningColumn.Index = 10 Then
' ''                If (Me.grdtrad.CurrentRow.Index + 1 <= Me.grdtrad.Rows.Count - 1) Then
' ''                    Me.grdtrad.CurrentCell = Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells("Active")
' ''                    e.SuppressKeyPress = True
' ''                Else
' ''                    Me.grdtrad.CurrentCell = Me.grdtrad.Rows(0).Cells("Active")
' ''                End If
' ''            End If
' ''            If Shift = 1 And KeyCode = 9 Then
' ''                If grdtrad.CurrentCell.OwningColumn.Index = 0 Then
' ''                    If (Me.grdtrad.CurrentRow.Index - 1 > 0) Then
' ''                        Me.grdtrad.CurrentCell = Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index - 1).Cells(grdtrad.CurrentCell.OwningColumn.Index)
' ''                        e.SuppressKeyPress = True
' ''                    Else
' ''                        'Me.grdtrad.CurrentCell = Me.grdtrad.Rows(0).Cells("last")
' ''                    End If
' ''                End If
' ''            End If
' ''        End If
' ''    End Sub
' ''    Private Sub grdtrad_DataError(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)

' ''    End Sub
' ''    Private Sub grdtrad_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
' ''        If VarIsFrmLoad = False Then Exit Sub
' ''        If (grdtrad.Columns(e.ColumnIndex).Name = "delta" Or grdtrad.Columns(e.ColumnIndex).Name = "deltaval" Or grdtrad.Columns(e.ColumnIndex).Name = "gamma" Or grdtrad.Columns(e.ColumnIndex).Name = "gmval" Or grdtrad.Columns(e.ColumnIndex).Name = "vega" Or grdtrad.Columns(e.ColumnIndex).Name = "vgval" Or grdtrad.Columns(e.ColumnIndex).Name = "theta" Or grdtrad.Columns(e.ColumnIndex).Name = "thetaval" Or grdtrad.Columns(e.ColumnIndex).Name = "volga" Or grdtrad.Columns(e.ColumnIndex).Name = "volgaval" Or grdtrad.Columns(e.ColumnIndex).Name = "vanna" Or grdtrad.Columns(e.ColumnIndex).Name = "vannaval") And e.RowIndex > -1 Then
' ''            'grdtrad.Columns(e.ColumnIndex).Name = "TimeII" Or
' ''            If Val(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.DarkBlue
' ''            Else
' ''                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''        End If
' ''        If (grdtrad.Columns(e.ColumnIndex).Name = "TimeII") Then
' ''            If (IsDate(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False) Then
' ''                grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtexp.Value
' ''                'grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
' ''                ' MsgBox("Please Enter Proper Date...")
' ''                ' grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
' ''            End If
' ''        End If

' ''        If (grdtrad.Columns(e.ColumnIndex).Name = "Active") Then
' ''            'If (grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = True) Then
' ''            grdtrad.EndEdit()
' ''            Call cal_summary()
' ''            'End If
' ''        End If

' ''        If (grdtrad.Columns(e.ColumnIndex).Name = "spval") Or (grdtrad.Columns(e.ColumnIndex).Name = "Strike") Then
' ''            'grdtrad.EndEdit()
' ''        End If

' ''        If grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "c" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Yellow
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Yellow


' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "p" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LimeGreen
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LimeGreen

' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "f" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Orange
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.Orange
' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "e" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LightPink
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Pink
' ''            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Pink
' ''            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Pink
' ''            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Pink
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LightPink

' ''        End If
' ''        If Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) < 0 Then
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.Red
' ''        ElseIf Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) > 0 Then
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.SkyBlue
' ''        Else
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.White
' ''        End If

' ''        grdtrad.Rows(e.RowIndex).Cells("ltp").Style.Format = "##0.00"
' ''        grdtrad.Rows(e.RowIndex).Cells("last").Style.Format = "##0.00"

' ''        grdtrad.Rows(e.RowIndex).Cells("delta").Style.Format = Deltastr
' ''        grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.Format = DecimalSetting.sDeltaval

' ''        grdtrad.Rows(e.RowIndex).Cells("gamma").Style.Format = DecimalSetting.sGamma
' ''        grdtrad.Rows(e.RowIndex).Cells("gmval").Style.Format = DecimalSetting.sGammaval

' ''        grdtrad.Rows(e.RowIndex).Cells("vega").Style.Format = DecimalSetting.sVega
' ''        grdtrad.Rows(e.RowIndex).Cells("vgval").Style.Format = DecimalSetting.sVegaval

' ''        grdtrad.Rows(e.RowIndex).Cells("theta").Style.Format = DecimalSetting.sTheta
' ''        grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.Format = DecimalSetting.sThetaval

' ''        grdtrad.Rows(e.RowIndex).Cells("volga").Style.Format = DecimalSetting.sVolga
' ''        grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.Format = DecimalSetting.sVolgaval

' ''        grdtrad.Rows(e.RowIndex).Cells("vanna").Style.Format = DecimalSetting.sVanna
' ''        grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.Format = DecimalSetting.sVannaval


' ''    End Sub
' ''    Private Sub grdtrad_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs)
' ''        If VarIsFrmLoad = False Then Exit Sub
' ''        Dim zero As Double = 0

' ''        grdtrad.Rows(e.RowIndex).Cells("TimeI").Value = dttoday.Value
' ''        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dtexp.Value

' ''        If IsDBNull(grdtrad.Rows(e.RowIndex).Cells("spval").Value) Or grdtrad.Rows(e.RowIndex).Cells("spval").Value Is Nothing Then
' ''            If txtmkt.Text.Trim <> "" Then
' ''                grdtrad.Rows(e.RowIndex).Cells("spval").Value = CDbl(txtmkt.Text)
' ''            Else
' ''                grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
' ''            End If
' ''        End If

' ''        'grdtrad.EndEdit()

' ''        Dim grow As DataGridViewRow
' ''        grow = grdtrad.Rows(e.RowIndex)

' ''        If IsDBNull(grow.Cells("CPF").Value) Or grow.Cells("CPF").Value Is Nothing Then
' ''            grow.Cells("CPF").Value = "C"
' ''        End If
' ''        If IsDBNull(grow.Cells("spval").Value) Or grow.Cells("spval").Value Is Nothing Then
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Value = zero
' ''        Else
' ''            grow.Cells("spval").Value = CDbl(grow.Cells("spval").Value)
' ''        End If
' ''        If IsDBNull(grow.Cells("Strike").Value) Or grow.Cells("Strike").Value Is Nothing Then
' ''            grow.Cells("Strike").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("units").Value) Or grow.Cells("units").Value Is Nothing Then
' ''            grow.Cells("units").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("last").Value) Or grow.Cells("last").Value Is Nothing Then
' ''            grow.Cells("last").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("lv").Value) Or grow.Cells("lv").Value Is Nothing Then
' ''            If txtcvol.Text.Trim <> "" Then
' ''                grow.Cells("lv").Value = Val(txtcvol.Text)
' ''            Else
' ''                grow.Cells("lv").Value = zero
' ''            End If
' ''        End If
' ''        If IsDBNull(grow.Cells("delta").Value) Or grow.Cells("delta").Value Is Nothing Then
' ''            grow.Cells("delta").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("deltaval").Value) Or grow.Cells("deltaval").Value Is Nothing Then
' ''            grow.Cells("deltaval").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("gamma").Value) Or grow.Cells("gamma").Value Is Nothing Then
' ''            grow.Cells("gamma").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("gmval").Value) Or grow.Cells("gmval").Value Is Nothing Then
' ''            grow.Cells("gmval").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("vega").Value) Or grow.Cells("vega").Value Is Nothing Then
' ''            grow.Cells("vega").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("vgval").Value) Or grow.Cells("vgval").Value Is Nothing Then
' ''            grow.Cells("vgval").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("theta").Value) Or grow.Cells("theta").Value Is Nothing Then
' ''            grow.Cells("theta").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("thetaval").Value) Or grow.Cells("thetaval").Value Is Nothing Then
' ''            grow.Cells("thetaval").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("volga").Value) Or grow.Cells("volga").Value Is Nothing Then
' ''            grow.Cells("volga").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("volgaval").Value) Or grow.Cells("volgaval").Value Is Nothing Then
' ''            grow.Cells("volgaval").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("vanna").Value) Or grow.Cells("vanna").Value Is Nothing Then
' ''            grow.Cells("vanna").Value = zero
' ''        End If
' ''        If IsDBNull(grow.Cells("vannaval").Value) Or grow.Cells("vannaval").Value Is Nothing Then
' ''            grow.Cells("vannaval").Value = zero
' ''        End If

' ''        'If (grdtrad.Columns(e.ColumnIndex).Name = "Dummy") And e.RowIndex > -1 Then
' ''        'grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = "1"
' ''        'End If
' ''        'grdtrad.Rows(e.RowIndex).Cells("Dummy").Value = 1

' ''        grow.Cells("uid").Value = grdtrad.Rows.Count - 1
' ''    End Sub
' ''    Private Sub grdtrad_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
' ''        If grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "c" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "C"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Yellow
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Yellow
' ''            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Yellow

' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "p" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "P"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LimeGreen
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.LimeGreen
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LimeGreen

' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "f" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "F"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.Orange
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.Orange
' ''        ElseIf grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E" Or grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "e" Then
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Value = "E"
' ''            grdtrad.Rows(e.RowIndex).Cells("Active").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeI").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("TimeII").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("CPF").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("spval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("Strike").Style.ForeColor = Color.LightPink
' ''            'grdtrad.Rows(e.RowIndex).Cells(6).Style.ForeColor = Color.Orange
' ''            grdtrad.Rows(e.RowIndex).Cells("ltp").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("lv").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("last").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("delta").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("gamma").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("gmval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("vega").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("vgval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("theta").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("volga").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("vanna").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.ForeColor = Color.LightPink
' ''            grdtrad.Rows(e.RowIndex).Cells("uid").Style.ForeColor = Color.LightPink
' ''        End If
' ''        If Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) < 0 Then
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.Red
' ''        ElseIf Val(grdtrad.Rows(e.RowIndex).Cells("units").Value) > 0 Then
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.SkyBlue
' ''        Else
' ''            grdtrad.Rows(e.RowIndex).Cells("units").Style.ForeColor = Color.White
' ''        End If

' ''        grdtrad.Rows(e.RowIndex).Cells("ltp").Style.Format = "##0.00"
' ''        grdtrad.Rows(e.RowIndex).Cells("last").Style.Format = "##0.00"

' ''        grdtrad.Rows(e.RowIndex).Cells("delta").Style.Format = Deltastr
' ''        grdtrad.Rows(e.RowIndex).Cells("deltaval").Style.Format = DecimalSetting.sDeltaval

' ''        grdtrad.Rows(e.RowIndex).Cells("gamma").Style.Format = DecimalSetting.sGamma
' ''        grdtrad.Rows(e.RowIndex).Cells("gmval").Style.Format = DecimalSetting.sGammaval

' ''        grdtrad.Rows(e.RowIndex).Cells("vega").Style.Format = DecimalSetting.sVega
' ''        grdtrad.Rows(e.RowIndex).Cells("vgval").Style.Format = DecimalSetting.sVegaval

' ''        grdtrad.Rows(e.RowIndex).Cells("theta").Style.Format = DecimalSetting.sTheta
' ''        grdtrad.Rows(e.RowIndex).Cells("thetaval").Style.Format = DecimalSetting.sThetaval

' ''        grdtrad.Rows(e.RowIndex).Cells("volga").Style.Format = DecimalSetting.sVolga
' ''        grdtrad.Rows(e.RowIndex).Cells("volgaval").Style.Format = DecimalSetting.sVolgaval

' ''        grdtrad.Rows(e.RowIndex).Cells("vanna").Style.Format = DecimalSetting.sVanna
' ''        grdtrad.Rows(e.RowIndex).Cells("vannaval").Style.Format = DecimalSetting.sVannaval

' ''    End Sub

' ''    Private Sub grdprofit_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            fill_result(0, e.RowIndex, e.ColumnIndex)
' ''            Dim res As New resultfrm
' ''            res.temptable = rtable
' ''            res.ShowDialog()
' ''        End If
' ''    End Sub
' ''    Private Sub grdprofit_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            'grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N2"
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & RoundGrossMTM
' ''            e.Value = d.ToString(str)
' ''            ' grdprofit.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = "N2"
' ''        End If

' ''    End Sub
' ''    Private Sub grddelta_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            '  grddelta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sDeltaval
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & DecimalSetting.iRDelta_Val
' ''            e.Value = d.ToString(str)
' ''        End If
' ''    End Sub
' ''    Private Sub grdgamma_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            'grdgamma.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sGammaval
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & DecimalSetting.iRGamma_Val
' ''            e.Value = d.ToString(str)

' ''        End If
' ''    End Sub
' ''    Private Sub grdvega_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            'grdvega.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sVegaval
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & DecimalSetting.iRVega_Val
' ''            e.Value = d.ToString(str)
' ''        End If
' ''    End Sub
' ''    Private Sub grdtheta_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            ' grdtheta.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.Format = DecimalSetting.sThetaval
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & DecimalSetting.iRTheta_Val
' ''            e.Value = d.ToString(str)
' ''        End If
' ''    End Sub
' ''    Private Sub grdvolga_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grdvolga.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & DecimalSetting.iRVolga_Val
' ''            e.Value = d.ToString(str)
' ''        End If
' ''    End Sub
' ''    Private Sub grdvanna_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs)
' ''        If e.ColumnIndex > 1 And e.RowIndex > -1 Then
' ''            If Val(grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) < 0 Then
' ''                grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.Red
' ''            ElseIf Val(grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) > 0 Then
' ''                grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.SkyBlue
' ''            Else
' ''                grdvanna.Rows(e.RowIndex).Cells(e.ColumnIndex).Style.ForeColor = Color.White
' ''            End If
' ''            Dim d As Double = Double.Parse(e.Value.ToString)
' ''            Dim str As String = "N" & DecimalSetting.iRVanna_Val
' ''            e.Value = d.ToString(str)
' ''        End If
' ''    End Sub
' ''    Private Sub cellkeydown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
' ''        If e.KeyCode = Keys.Tab Then
' ''            If grdtrad.CurrentCell.OwningColumn.Index = 4 Then
' ''                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Then
' ''                    Me.grdtrad.CurrentCell = _
' ''                                              Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index).Cells("units")

' ''                    e.SuppressKeyPress = True
' ''                End If
' ''            ElseIf grdtrad.CurrentCell.OwningColumn.Index = 6 Then
' ''                If grdtrad.CurrentRow.Cells("CPF").Value = "F" Then
' ''                    Me.grdtrad.CurrentCell = _
' ''                                              Me.grdtrad.Rows(Me.grdtrad.CurrentRow.Index + 1).Cells("Active")
' ''                    e.SuppressKeyPress = True
' ''                End If
' ''            End If

' ''        End If
' ''    End Sub
' ''    Private Sub CheckCell(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        If FirstDgt = 0 Then
' ''            FirstDgt = Val(e.KeyChar.ToString)
' ''        End If
' ''        'Dim KeyAscii As Short = Asc(e.KeyChar)
' ''        Dim col As Integer
' ''        col = grdtrad.CurrentCell.ColumnIndex
' ''        Select Case col
' ''            Case 2
' ''                dateonly(e)
' ''            Case 4
' ''                numonly(e)
' ''            Case 5
' ''                numonly(e)
' ''            Case 6
' ''                numonly(e)

' ''            Case 7
' ''                numonly(e)
' ''            Case 8
' ''                numonly(e)
' ''            Case 10
' ''                numonly(e)
' ''            Case 3
' ''                Dim arr As New ArrayList
' ''                arr.Add(67)
' ''                arr.Add(70)
' ''                arr.Add(80)
' ''                arr.Add(69)
' ''                arr.Add(8)

' ''                If Not arr.Contains(Asc(UCase(e.KeyChar))) Then
' ''                    e.Handled = True
' ''                End If


' ''        End Select
' ''        '... 

' ''        '... code to check the input

' ''        '... 

' ''        'If KeyAscii = 0 Then

' ''        '    e.Handled = True

' ''        'End If

' ''    End Sub

' ''#End Region
' ''#Region "Method"
' ''    Dim profitarr As New ArrayList
' ''    Dim thetaarr As New ArrayList
' ''    Dim deltaarr As New ArrayList
' ''    Dim gammaarr As New ArrayList
' ''    Dim vegaarr As New ArrayList
' ''    Dim volgaarr As New ArrayList
' ''    Dim vannaarr As New ArrayList
' ''    Private Sub CalDatastkprice(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal mT As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal grow As DataGridViewRow, ByVal qty As Double, ByVal lv As Double)
' ''        Try
' ''            Dim mDelta As Double
' ''            Dim mGama As Double
' ''            Dim mVega As Double
' ''            Dim mThita As Double
' ''            Dim mVolga As Double
' ''            Dim mVanna As Double
' ''            Dim mRah As Double
' ''            Dim mVolatility As Double
' ''            Dim tmpcpprice As Double = 0
' ''            tmpcpprice = cpprice

' ''            Dim mD1 As Double
' ''            Dim mD2 As Double

' ''            'Dim mIsCal As Boolean
' ''            'Dim mIsPut As Boolean
' ''            'Dim mIsFut As Boolean

' ''            mDelta = 0
' ''            mGama = 0
' ''            mVega = 0
' ''            mThita = 0
' ''            mVolga = 0
' ''            mVanna = 0
' ''            mRah = 0
' ''            mD1 = 0
' ''            mD2 = 0

' ''            Dim _mt As Double
' ''            'IF MATURITY IS 0 THEN _MT = 0.00001 
' ''            If mT = 0 Then
' ''                _mt = 0.0001
' ''            Else
' ''                _mt = (mT) / 365

' ''            End If
' ''            mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
' ''            'mVolatility = lv
' ''            Try

' ''                'If Not mIsFut Then
' ''                mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))

' ''                mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))

' ''                mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))

' ''                mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))

' ''                mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))

' ''                mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
' ''                mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

' ''                mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
' ''                mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)

' ''                'Else
' ''                ''mDelta = mDelta + (1 * drow("Qty"))
' ''                'End If



' ''                grow.Cells("lv").Value = Math.Round(mVolatility * 100, 2)
' ''                grow.Cells("delta").Value = Math.Round(mDelta, DecimalSetting.iRDelta)

' ''                grow.Cells("deltaval").Value = Math.Round(mDelta * qty, DecimalSetting.iRDelta_Val)

' ''                grow.Cells("gamma").Value = Math.Round(mGama, DecimalSetting.iRGamma)
' ''                grow.Cells("gmval").Value = Math.Round(mGama * qty, DecimalSetting.iRGamma_Val)
' ''                grow.Cells("vega").Value = Math.Round(mVega, DecimalSetting.iRVega)
' ''                grow.Cells("vgval").Value = Math.Round(mVega * qty, DecimalSetting.iRVega_Val)
' ''                grow.Cells("theta").Value = Math.Round(mThita, DecimalSetting.iRTheta)
' ''                grow.Cells("thetaval").Value = Math.Round(mThita * qty, DecimalSetting.iRTheta_Val)
' ''                grow.Cells("volga").Value = Math.Round(mVolga, DecimalSetting.iRVolga)
' ''                grow.Cells("volgaval").Value = Math.Round(mVolga * qty, DecimalSetting.iRVolga_Val)
' ''                grow.Cells("vanna").Value = Math.Round(mVanna, DecimalSetting.iRVanna)
' ''                grow.Cells("vannaval").Value = Math.Round(mVanna * qty, DecimalSetting.iRVanna_Val)

' ''                'grow.Cells(8).Value = Math.Round(mVolatility * 100, 2)
' ''                'grow.cells(10).value = Format(mDelta, Deltastr)
' ''                'grow.cells(11).value = Format(mDelta * qty, DecimalSetting.sDeltaval)
' ''                'grow.cells(12).value = Format(mGama, DecimalSetting.sGamma)
' ''                'grow.cells(13).value = Format(mGama * qty, DecimalSetting.sGammaval)
' ''                'grow.cells(14).value = Format(mVega, DecimalSetting.sVega)
' ''                'grow.cells(15).value = Format(mVega * qty, DecimalSetting.sVegaval)
' ''                'grow.cells(16).value = Format(mThita, DecimalSetting.sTheta)
' ''                'grow.cells(17).value = Format(mThita * qty, DecimalSetting.sThetaval)
' ''                grdtrad.EndEdit()
' ''            Catch ex As Exception

' ''            End Try
' ''        Catch ex As Exception
' ''            ' MsgBox(ex.ToString)
' ''        End Try


' ''        ''MsgBox(mDelta)

' ''    End Sub
' ''    Private Sub CalData(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal mT As Double, ByVal mmT As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal grow As DataGridViewRow, ByVal qty As Double, ByVal lv As Double)
' ''        Dim mDelta As Double
' ''        Dim mGama As Double
' ''        Dim mVega As Double
' ''        Dim mThita As Double
' ''        Dim mVolga As Double
' ''        Dim mVanna As Double
' ''        Dim mRah As Double

' ''        Dim mD1 As Double
' ''        Dim mD2 As Double
' ''        Dim mD11 As Double
' ''        Dim mD21 As Double

' ''        ''divyesh
' ''        Dim mDelta1 As Double
' ''        Dim mGama1 As Double
' ''        Dim mVega1 As Double
' ''        Dim mThita1 As Double
' ''        Dim mVolga1 As Double
' ''        Dim mVanna1 As Double
' ''        Dim mRah1 As Double

' ''        Dim mVolatility As Double
' ''        Dim tmpcpprice As Double = 0
' ''        Dim tmpcpprice1 As Double = 0

' ''        Dim cpprice1 As Double = 0

' ''        tmpcpprice = cpprice

' ''        tmpcpprice1 = cpprice1

' ''        'Dim mIsCal As Boolean
' ''        'Dim mIsPut As Boolean
' ''        'Dim mIsFut As Boolean

' ''        mDelta = 0
' ''        mGama = 0
' ''        mVega = 0
' ''        mThita = 0
' ''        mVolga = 0
' ''        mVanna = 0
' ''        mRah = 0

' ''        mDelta1 = 0
' ''        mGama1 = 0
' ''        mVega1 = 0
' ''        mThita1 = 0
' ''        mVolga1 = 0
' ''        mVanna1 = 0
' ''        mRah1 = 0

' ''        Dim _mt, _mt1 As Double
' ''        'IF MATURITY IS 0 THEN _MT = 0.00001 
' ''        If mT = 0 Then
' ''            _mt = 0.0001
' ''            _mt1 = 0.00001
' ''        Else
' ''            _mt = (mT) / 365
' ''            _mt1 = (mmT) / 365
' ''        End If
' ''        'mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
' ''        mVolatility = lv
' ''        'mVolatility1 = lv
' ''        Try
' ''            'If Not mIsFut Then
' ''            tmpcpprice = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 0))
' ''            mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))
' ''            mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))
' ''            mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))
' ''            mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))
' ''            mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))

' ''            mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
' ''            mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

' ''            mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
' ''            mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)


' ''            tmpcpprice1 = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 0))
' ''            mDelta1 = mDelta1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 1))
' ''            mGama1 = mGama1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 2))
' ''            mVega1 = mVega1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 3))
' ''            mThita1 = mThita1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 4))
' ''            mRah1 = mRah1 + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt1, mIsCall, mIsFut, 0, 5))


' ''            mD11 = mD11 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt1)
' ''            mD21 = mD21 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt1)

' ''            mVolga1 = mVolga1 + CalVolga(mVega1, mD11, mD21, mVolatility)
' ''            mVanna1 = mVanna1 + CalVanna(futval, mVega, mD11, mD21, mVolatility, _mt1)


' ''            'Else
' ''            ''mDelta = mDelta + (1 * drow("Qty"))
' ''            'End If

' ''            'grow.Cells(9).Value = Math.Round(tmpcpprice, round)
' ''            'grow.cells(10).value = Math.Round(mDelta, round)
' ''            'grow.cells(11).value = Math.Round(mDelta * qty, round)
' ''            'grow.cells(12).value = Math.Round(mThita, round)
' ''            'grow.cells(13).value = Math.Round(mThita * qty, round)
' ''            'grow.cells(14).value = Math.Round(mVega, round)
' ''            'grow.cells(15).value = Math.Round(mVega * qty, round)
' ''            'grow.cells(16).value = Math.Round(mGama, round)
' ''            'grow.cells(17).value = Math.Round(mGama * qty, round)

' ''            grow.Cells("last").Value = Math.Round(tmpcpprice, 2)
' ''            grow.Cells("delta").Value = Math.Round(mDelta, DecimalSetting.iRDelta)
' ''            grow.Cells("deltaval").Value = Math.Round(mDelta * qty, DecimalSetting.iRDelta_Val)
' ''            grow.Cells("gamma").Value = Math.Round(mGama, DecimalSetting.iRGamma)
' ''            grow.Cells("gmval").Value = Math.Round(mGama * qty, DecimalSetting.iRGamma_Val)
' ''            grow.Cells("vega").Value = Math.Round(mVega, DecimalSetting.iRVega)
' ''            grow.Cells("vgval").Value = Math.Round(mVega * qty, DecimalSetting.iRVega_Val)
' ''            grow.Cells("theta").Value = Math.Round(mThita, DecimalSetting.iRTheta)
' ''            grow.Cells("thetaval").Value = Math.Round(mThita * qty, DecimalSetting.iRTheta_Val)

' ''            grow.Cells("volga").Value = Math.Round(mVolga, DecimalSetting.iRVolga)
' ''            grow.Cells("volgaval").Value = Math.Round(mVolga * qty, DecimalSetting.iRVolga_Val)
' ''            grow.Cells("vanna").Value = Math.Round(mVanna, DecimalSetting.iRTheta)
' ''            grow.Cells("vannaval").Value = Math.Round(mVanna * qty, DecimalSetting.iRVanna_Val)


' ''            grow.Cells("ltp1").Value = Math.Round(tmpcpprice1, 2)
' ''            grow.Cells("delta1").Value = Math.Round(mDelta1, DecimalSetting.iRDelta)
' ''            grow.Cells("deltaval1").Value = Math.Round(mDelta1 * qty, DecimalSetting.iRDelta_Val)
' ''            grow.Cells("gamma1").Value = Math.Round(mGama1, DecimalSetting.iRDelta)
' ''            grow.Cells("gmval1").Value = Math.Round(mGama1 * qty, DecimalSetting.iRGamma_Val)
' ''            grow.Cells("vega1").Value = Math.Round(mVega1, DecimalSetting.iRDelta)
' ''            grow.Cells("vgval1").Value = Math.Round(mVega1 * qty, DecimalSetting.iRVega_Val)
' ''            grow.Cells("theta1").Value = Math.Round(mThita1, DecimalSetting.iRDelta)
' ''            grow.Cells("thetaval1").Value = Math.Round(mThita1 * qty, DecimalSetting.iRTheta_Val)

' ''            grow.Cells("volga1").Value = Math.Round(mVolga1, DecimalSetting.iRVolga)
' ''            grow.Cells("volgaval1").Value = Math.Round(mVolga1 * qty, DecimalSetting.iRVolga_Val)
' ''            grow.Cells("vanna1").Value = Math.Round(mVanna1, DecimalSetting.iRVanna)
' ''            grow.Cells("vannaval1").Value = Math.Round(mVanna1 * qty, DecimalSetting.iRVanna_Val)


' ''            'grow.Cells(9).Value = Math.Round(tmpcpprice, 2)
' ''            'grow.cells(10).value = Format(mDelta, Deltastr)
' ''            'grow.cells(11).value = Format(mDelta * qty, DecimalSetting.sDeltaval)
' ''            'grow.cells(12).value = Format(mGama, DecimalSetting.sGamma)
' ''            'grow.cells(13).value = Format(mGama * qty, DecimalSetting.sGammaval)
' ''            'grow.cells(14).value = Format(mVega, DecimalSetting.sVega)
' ''            'grow.cells(15).value = Format(mVega * qty, DecimalSetting.sVegaval)
' ''            'grow.cells(16).value = Format(mThita, DecimalSetting.sTheta)
' ''            'grow.cells(17).value = Format(mThita * qty, DecimalSetting.sThetaval)
' ''            grdtrad.EndEdit()
' ''        Catch ex As Exception

' ''        End Try

' ''        ''MsgBox(mDelta)


' ''    End Sub

' ''    'Private Function cal_profit() As Boolean
' ''    '    grdprofit.DataSource = Nothing
' ''    '    'grdprofit.Refresh()
' ''    '    Dim totfpr As Double
' ''    '    Dim totcpr As Double
' ''    '    profitarr = New ArrayList
' ''    '    Dim bval As Double
' ''    '    Dim bvol As Double

' ''    '    Dim i As Integer
' ''    '    i = 0
' ''    '    Dim dcount As Integer
' ''    '    dcount = 0
' ''    '    Dim iscall As Boolean
' ''    '    'ReDim profitarr(((profit.Rows.Count) * (profit.Columns.Count - 1)))
' ''    '    For Each dr As DataRow In profit.Rows
' ''    '        totfpr = 0
' ''    '        dcount = 0
' ''    '        For Each col As DataColumn In profit.Columns
' ''    '            If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''    '                For Each grow As DataGridViewRow In grdtrad.Rows
' ''    '                    If CBool(grow.Cells(0).Value) = True Then

' ''    '                        If grow.Cells(3).Value = "F" Then
' ''    '                            If Val(grow.Cells(4).Value) = 0 Then
' ''    '                                MsgBox("Enter Spot Value for Selected Future")
' ''    '                                grdtrad.Focus()
' ''    '                                Return False
' ''    '                            End If
' ''    '                            If Val(grow.Cells(6).Value) = 0 Then
' ''    '                                MsgBox("Enter Quantity Value for Selected Future")
' ''    '                                grdtrad.Focus()
' ''    '                                Return False

' ''    '                            End If
' ''    '                            dr(col.ColumnName) = (Val(dr(col.ColumnName)) + ((Val(dr("SpotValue")) - Val(grow.Cells(4).Value)) * Val(grow.Cells(6).Value))) + grossmtm
' ''    '                            'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + ((val(dr("SpotValue")) - val(grow.Cells(4).Value)) * val(grow.Cells(6).Value)), RoundGrossMTM)
' ''    '                            'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + ((val(dr("SpotValue")) - val(grow.Cells(4).Value)) * val(grow.Cells(6).Value)), GrossMTMstr)

' ''    '                        Else
' ''    '                            If grow.Cells(3).Value = "C" Then
' ''    '                                iscall = True
' ''    '                            Else
' ''    '                                iscall = False
' ''    '                            End If
' ''    '                            If Val(grow.Cells(9).Value) = 0 Then
' ''    '                                MsgBox("Enter Rate Value for Selected Call or Put")
' ''    '                                grdtrad.Focus()

' ''    '                                Return False

' ''    '                            End If
' ''    '                            If Val(grow.Cells(6).Value) = 0 Then
' ''    '                                MsgBox("Enter Quantity Value for Selected Call or Put")
' ''    '                                grdtrad.Focus()
' ''    '                                Return False

' ''    '                            End If
' ''    '                            If Val(grow.Cells(4).Value) = 0 Then
' ''    '                                MsgBox("Enter Spot Value for Selected Call or Put")
' ''    '                                grdtrad.Focus()
' ''    '                                Exit Function
' ''    '                            End If

' ''    '                            If Val(grow.Cells(8).Value) = 0 Then
' ''    '                                MsgBox("Enter Volatality Value for Selected Call or Put")
' ''    '                                grdtrad.Focus()
' ''    '                                Return False

' ''    '                            End If
' ''    '                            totcpr = 0
' ''    '                            Dim cd As Date
' ''    '                            If (IsDate(col.ColumnName)) Then
' ''    '                                cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''    '                            Else
' ''    '                                cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
' ''    '                            End If

' ''    '                            'dcount = DatePart(DateInterval.Day, cd)
' ''    '                            'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(dtexp.Value.Date))
' ''    '                            Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells(2).Value).Date)
' ''    '                            Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells(2).Value).Date)
' ''    '                            If CDate(dttoday.Value.Date) = CDate(grow.Cells(2).Value).Date Then
' ''    '                                _mT1 = 0.5
' ''    '                            End If
' ''    '                            If CDate(cd.Date) = CDate(grow.Cells(2).Value).Date Then
' ''    '                                If (IsDate(col.ColumnName)) Then
' ''    '                                    _mT = 0.5
' ''    '                                Else
' ''    '                                    _mT = 0.5
' ''    '                                End If

' ''    '                            End If
' ''    '                            If _mT = 0 Then
' ''    '                                _mT = 0.00001
' ''    '                                _mT1 = 0.00001
' ''    '                            Else
' ''    '                                _mT = (_mT) / 365
' ''    '                                _mT1 = (_mT1) / 365
' ''    '                            End If
' ''    '                            If (IsDate(col.ColumnName)) Then
' ''    '                                bval = 0
' ''    '                                bvol = 0
' ''    '                                bvol = mObjData.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells(5).Value), Mrateofinterast, 0, (Val(grow.Cells(9).Value)), _mT1, iscall, True, 0, 6)
' ''    '                                bval = mObjData.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells(5).Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 0)
' ''    '                                totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
' ''    '                                dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) + grossmtm
' ''    '                                'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + totcpr, RoundGrossMTM)
' ''    '                                ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr,GrossMTMstr)
' ''    '                            Else
' ''    '                                If (iscall = True) Then
' ''    '                                    If ((Val(dr("SpotValue")) - Val(grow.Cells(5).Value)) > 0) Then
' ''    '                                        bval = (Val(dr("SpotValue")) - Val(grow.Cells(5).Value))
' ''    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
' ''    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) + grossmtm
' ''    '                                    Else
' ''    '                                        bval = 0 ' val(dr(col.ColumnName)) + ((0 - val(grow.Cells(9).Value)) * val(grow.Cells(6).Value))
' ''    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
' ''    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) + grossmtm
' ''    '                                    End If
' ''    '                                Else
' ''    '                                    If ((Val(grow.Cells(5).Value) - Val(dr("SpotValue"))) > 0) Then
' ''    '                                        bval = (Val(grow.Cells(5).Value) - Val(dr("SpotValue")))
' ''    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
' ''    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) ' + grossmtm
' ''    '                                    Else
' ''    '                                        bval = 0 'val(dr(col.ColumnName)) + ((0 - val(grow.Cells(9).Value)) * val(grow.Cells(6).Value))
' ''    '                                        totcpr = ((bval - Val(grow.Cells(9).Value)) * Val(grow.Cells(6).Value))
' ''    '                                        dr(col.ColumnName) = (Val(dr(col.ColumnName)) + totcpr) ' + grossmtm
' ''    '                                    End If
' ''    '                                End If
' ''    '                            End If

' ''    '                        End If

' ''    '                    End If
' ''    '                Next
' ''    '                dcount = dcount + 1
' ''    '            End If
' ''    '            i = i + 1
' ''    '            profitarr.Add(dr(col.ColumnName))

' ''    '        Next
' ''    '    Next
' ''    '    grdprofit.DataSource = profit
' ''    '    Return True

' ''    '    'For Each col As DataColumn In profit.Columns
' ''    '    '    If UCase(col.ColumnName) <> UCase("spotvalue") Then
' ''    '    '        For Each drow As DataRow In profit.Rows

' ''    '    '        Next
' ''    '    '    End If
' ''    '    'Next

' ''    '    'grdprofit.Refresh()
' ''    'End Function
' ''    Private Function cal_profit() As Boolean
' ''        grdprofit.DataSource = Nothing
' ''        'grdprofit.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double
' ''        profitarr = New ArrayList
' ''        Dim bval As Double
' ''        Dim bvol As Double

' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean
' ''        'ReDim profitarr(((profit.Rows.Count) * (profit.Columns.Count - 1)))
' ''        For Each dr As DataRow In profit.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In profit.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True  And  Val(grow.Cells("units").Value) <> 0  Then

' ''                            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
' ''                                If CDbl(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Future.")
' ''                                    grdtrad.Focus()
' ''                                    Return False
' ''                                End If
' ''                                If CDbl(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Future.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value)))
' ''                                'dr(col.ColumnName) = Math.Round(CDbl(dr(col.ColumnName)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), RoundGrossMTM)
' ''                                'dr(col.ColumnName) = Format(CDbl(dr(col.ColumnName)) + ((CDbl(dr("SpotValue")) - CDbl(grow.Cells(4).Value)) * CDbl(grow.Cells(6).Value)), GrossMTMstr)

' ''                            Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                'Comment By Alpesh For Ltp 0 is Allow
' ''                                'If CDbl(grow.Cells("ltp").Value) = 0 Then
' ''                                '    MsgBox("Enter Ltp Value for Selected Call or Put")
' ''                                '    grdtrad.Focus()
' ''                                '    Return False
' ''                                'End If

' ''                                If CDbl(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If CDbl(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Exit Function
' ''                                End If

' ''                                If CDbl(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                totcpr = 0
' ''                                Dim cd As Date
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                Else
' ''                                    cd = CDate(Format(CDate(Mid(col.ColumnName, 8, Len(col.ColumnName))), "MMM/dd/yyyy"))
' ''                                End If

' ''                                'dcount = DatePart(DateInterval.Day, cd)
' ''                                'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(dtexp.Value.Date))
' ''                                Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                    _mT1 = 0.5
' ''                                End If
' ''                                If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                    If (IsDate(col.ColumnName)) Then
' ''                                        _mT = 0.5
' ''                                    Else
' ''                                        _mT = 0.5
' ''                                    End If

' ''                                End If
' ''                                If _mT = 0 Then
' ''                                    _mT = 0.00001
' ''                                    _mT1 = 0.00001
' ''                                Else
' ''                                    _mT = (_mT) / 365
' ''                                    _mT1 = (_mT1) / 365
' ''                                End If
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    bval = 0
' ''                                    bvol = 0
' ''                                    'bvol = Greeks.Black_Scholes(CDbl(txtmkt.Text), CDbl(grow.Cells(5).Value), Mrateofinterast, 0, (CDbl(grow.Cells(7).Value)), _mT1, iscall, True, 0, 6)
' ''                                    bval = Greeks.Black_Scholes(CDbl(dr("SpotValue")), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value)) / 100, _mT, iscall, True, 0, 0)
' ''                                    totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
' ''                                    'dr(col.ColumnName) = Math.Round(CDbl(dr(col.ColumnName)) + totcpr, RoundGrossMTM)
' ''                                    ' dr(col.ColumnName) = Format(CDbl(dr(col.ColumnName)) + totcpr,GrossMTMstr)
' ''                                Else
' ''                                    If (iscall = True) Then
' ''                                        If ((CDbl(dr("SpotValue")) - CDbl(grow.Cells("Strike").Value)) > 0) Then
' ''                                            bval = (CDbl(dr("SpotValue")) - CDbl(grow.Cells("Strike").Value))
' ''                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
' ''                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
' ''                                        Else
' ''                                            bval = 0 ' CDbl(dr(col.ColumnName)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
' ''                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
' ''                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
' ''                                        End If
' ''                                    Else
' ''                                        If ((CDbl(grow.Cells("Strike").Value) - CDbl(dr("SpotValue"))) > 0) Then
' ''                                            bval = (CDbl(grow.Cells("Strike").Value) - CDbl(dr("SpotValue")))
' ''                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
' ''                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
' ''                                        Else
' ''                                            bval = 0 'CDbl(dr(col.ColumnName)) + ((0 - CDbl(grow.Cells(7).Value)) * CDbl(grow.Cells(6).Value))
' ''                                            totcpr = ((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))
' ''                                            dr(col.ColumnName) = (CDbl(dr(col.ColumnName)) + totcpr)
' ''                                        End If
' ''                                    End If
' ''                                End If

' ''                            End If

' ''                        End If


' ''                    Next
' ''                    dr(col.ColumnName) += grossmtm
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1


' ''                profitarr.Add(dr(col.ColumnName))

' ''            Next
' ''        Next
' ''        grdprofit.DataSource = profit
' ''        Return True

' ''        'For Each col As DataColumn In profit.Columns
' ''        '    If UCase(col.ColumnName) <> UCase("spotvalue") Then
' ''        '        For Each drow As DataRow In profit.Rows

' ''        '        Next
' ''        '    End If
' ''        'Next

' ''        'grdprofit.Refresh()
' ''    End Function

' ''    Private Function cal_delta() As Boolean


' ''        grddelta.DataSource = Nothing
' ''        'grddelta.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double
' ''        Dim bval As Double
' ''        Dim bvol As Double


' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean

' ''        For Each dr As DataRow In deltatable.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In deltatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then


' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True  And  Val(grow.Cells("units").Value) <> 0  Then

' ''                            If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Future.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Future.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + Val(grow.Cells("deltaval").Value), DecimalSetting.iRDelta_Val)
' ''                                ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + val(grow.cells(11).value), DecimalSetting.sDeltaval)

' ''                            Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If Val(grow.Cells("last").Value) = 0 Then
' ''                                    MsgBox("Enter Rate Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If

' ''                                If Val(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If

' ''                                totcpr = 0
' ''                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
' ''                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT1 = 0.5
' ''                                    End If
' ''                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT = 0.5
' ''                                    End If
' ''                                    If _mT = 0 Then
' ''                                        _mT = 0.00001
' ''                                        _mT1 = 0.00001
' ''                                    Else
' ''                                        _mT = (_mT) / 365
' ''                                        _mT1 = (_mT1) / 365
' ''                                    End If
' ''                                    bval = 0
' ''                                    bvol = 0
' ''                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 1)
' ''                                    totcpr = (bval * Val(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, DecimalSetting.iRDelta_Val)
' ''                                    ' dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sDeltaval)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1
' ''                deltaarr.Add(dr(col.ColumnName))
' ''            Next
' ''        Next

' ''        grddelta.DataSource = deltatable
' ''        Return True

' ''        'grddelta.Refresh()
' ''    End Function
' ''    Private Function cal_gamma() As Boolean
' ''        grdgamma.DataSource = Nothing
' ''        'grdgamma.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double

' ''        Dim bval As Double
' ''        Dim bvol As Double

' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean

' ''        For Each dr As DataRow In gammatable.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In gammatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True  And  Val(grow.Cells("units").Value) <> 0  Then
' ''                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
' ''                                '    'If val(grow.Cells(2).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Spot Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'If val(grow.Cells(5).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Units Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

' ''                                'Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If Val(grow.Cells("last").Value) = 0 Then
' ''                                    MsgBox("Enter Rate Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False


' ''                                End If

' ''                                If Val(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                totcpr = 0
' ''                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
' ''                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT1 = 0.5
' ''                                    End If
' ''                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT = 0.5
' ''                                    End If
' ''                                    If _mT = 0 Then
' ''                                        _mT = 0.00001
' ''                                        _mT1 = 0.00001
' ''                                    Else
' ''                                        _mT = (_mT) / 365
' ''                                        _mT1 = (_mT1) / 365
' ''                                    End If
' ''                                    bval = 0
' ''                                    bvol = 0
' ''                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 2)
' ''                                    totcpr = (bval * Val(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, DecimalSetting.iRGamma_Val)
' ''                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sGammaval)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1
' ''                gammaarr.Add(dr(col.ColumnName))
' ''            Next
' ''        Next

' ''        grdgamma.DataSource = gammatable
' ''        Return True

' ''        'grdgamma.Refresh()
' ''    End Function
' ''    Private Function cal_vega() As Boolean

' ''        grdvega.DataSource = Nothing
' ''        'grdvega.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double
' ''        Dim bval As Double
' ''        Dim bvol As Double


' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean

' ''        For Each dr As DataRow In vegatable.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In vegatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then


' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True  And  Val(grow.Cells("units").Value) <> 0  Then

' ''                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
' ''                                '    'If val(grow.Cells(2).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Spot Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'If val(grow.Cells(5).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Units Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

' ''                                'Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If Val(grow.Cells("last").Value) = 0 Then
' ''                                    MsgBox("Enter Rate Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If

' ''                                If Val(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                totcpr = 0
' ''                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
' ''                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT1 = 0.5
' ''                                    End If
' ''                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT = 0.5
' ''                                    End If
' ''                                    If _mT = 0 Then
' ''                                        _mT = 0.00001
' ''                                        _mT1 = 0.00001
' ''                                    Else
' ''                                        _mT = (_mT) / 365
' ''                                        _mT1 = (_mT1) / 365
' ''                                    End If
' ''                                    bval = 0
' ''                                    bvol = 0
' ''                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 3)
' ''                                    totcpr = (bval * Val(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, DecimalSetting.iRVega_Val)
' ''                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sVegaval)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1
' ''                vegaarr.Add(dr(col.ColumnName))
' ''            Next
' ''        Next

' ''        grdvega.DataSource = vegatable
' ''        Return True

' ''        'grdvega.Refresh()

' ''    End Function
' ''    Private Function cal_theta() As Boolean
' ''        grdtheta.DataSource = Nothing
' ''        'grdtheta.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double

' ''        thetaarr = New ArrayList
' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean
' ''        Dim bval As Double
' ''        Dim bvol As Double

' ''        For Each dr As DataRow In thetatable.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In thetatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True  And  Val(grow.Cells("units").Value) <> 0  Then

' ''                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
' ''                                '    'If val(grow.Cells(2).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Spot Value for Selected Future")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'If val(grow.Cells(5).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Units Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

' ''                                'Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If Val(grow.Cells("last").Value) = 0 Then
' ''                                    MsgBox("Enter Rate Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If

' ''                                If Val(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                totcpr = 0
' ''                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
' ''                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT1 = 0.5
' ''                                    End If
' ''                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT = 0.5
' ''                                    End If
' ''                                    If _mT = 0 Then
' ''                                        _mT = 0.00001
' ''                                        _mT1 = 0.00001
' ''                                    Else
' ''                                        _mT = (_mT) / 365
' ''                                        _mT1 = (_mT1) / 365
' ''                                    End If
' ''                                    bval = 0
' ''                                    bvol = 0
' ''                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 4)
' ''                                    totcpr = (bval * Val(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, DecimalSetting.iRTheta_Val)
' ''                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sThetaval)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1
' ''                thetaarr.Add(dr(col.ColumnName))
' ''            Next
' ''        Next

' ''        grdtheta.DataSource = thetatable
' ''        Return True

' ''        'grdtheta.Refresh()

' ''    End Function

' ''    Private Function cal_volga() As Boolean
' ''        grdvolga.DataSource = Nothing
' ''        'grdvolga.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double

' ''        volgaarr = New ArrayList
' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean
' ''        Dim bval As Double
' ''        Dim bvol As Double
' ''        Dim mD1 As Double
' ''        Dim mD2 As Double
' ''        Dim mVega As Double

' ''        For Each dr As DataRow In volgatable.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In volgatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True  And  Val(grow.Cells("units").Value) <> 0  Then

' ''                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
' ''                                '    'If val(grow.Cells(2).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Spot Value for Selected Future")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'If val(grow.Cells(5).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Units Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

' ''                                'Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If Val(grow.Cells("last").Value) = 0 Then
' ''                                    MsgBox("Enter Rate Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If

' ''                                If Val(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                totcpr = 0
' ''                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
' ''                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT1 = 0.5
' ''                                    End If
' ''                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT = 0.5
' ''                                    End If
' ''                                    If _mT = 0 Then
' ''                                        _mT = 0.00001
' ''                                        _mT1 = 0.00001
' ''                                    Else
' ''                                        _mT = (_mT) / 365
' ''                                        _mT1 = (_mT1) / 365
' ''                                    End If
' ''                                    bval = 0
' ''                                    bvol = 0

' ''                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    mVega = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 3)

' ''                                    mD1 = CalD1(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
' ''                                    mD2 = CalD2(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)

' ''                                    bval = CalVolga(mVega, mD1, mD2, bvol)
' ''                                    'mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mT)

' ''                                    'bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 4)

' ''                                    totcpr = (bval * Val(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, DecimalSetting.iRVolga_Val)
' ''                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sVolgaval)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1
' ''                volgaarr.Add(dr(col.ColumnName))
' ''                'If volgaarr(3) = "NaN" Then MsgBox("A")
' ''            Next
' ''        Next

' ''        grdvolga.DataSource = volgatable
' ''        Return True

' ''        'grdvolga.Refresh()

' ''    End Function
' ''    Private Function cal_vanna() As Boolean
' ''        grdvanna.DataSource = Nothing
' ''        'grdvanna.Refresh()
' ''        Dim totfpr As Double
' ''        Dim totcpr As Double

' ''        vannaarr = New ArrayList
' ''        Dim i As Integer
' ''        i = 0
' ''        Dim dcount As Integer
' ''        dcount = 0
' ''        Dim iscall As Boolean
' ''        Dim bval As Double
' ''        Dim bvol As Double
' ''        Dim mD1 As Double
' ''        Dim mD2 As Double
' ''        Dim mVega As Double

' ''        For Each dr As DataRow In vannatable.Rows
' ''            totfpr = 0
' ''            dcount = 0
' ''            For Each col As DataColumn In vannatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    For Each grow As DataGridViewRow In grdtrad.Rows
' ''                        If grow.Cells("Active").Value = False Then Continue For
' ''                        If CBool(grow.Cells("Active").Value) = True  And  Val(grow.Cells("units").Value) <> 0  Then

' ''                            If grow.Cells("CPF").Value <> "F" And grow.Cells("CPF").Value <> "E" Then
' ''                                '    'If val(grow.Cells(2).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Spot Value for Selected Future")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'If val(grow.Cells(5).Value) = 0 Then
' ''                                '    '    MsgBox("Enter Units Value for Selected Futre")
' ''                                '    '    grdtrad.Focus()
' ''                                '    '    Exit Sub
' ''                                '    'End If
' ''                                '    'dr(col.ColumnName) = Math.Round(val(dr(col.ColumnName)) + val(grow.cells(10).value), 2)

' ''                                'Else
' ''                                If grow.Cells("CPF").Value = "C" Then
' ''                                    iscall = True
' ''                                Else
' ''                                    iscall = False
' ''                                End If
' ''                                If Val(grow.Cells("last").Value) = 0 Then
' ''                                    MsgBox("Enter Rate Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("units").Value) = 0 Then
' ''                                    MsgBox("Enter Quantity Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                If Val(grow.Cells("spval").Value) = 0 Then
' ''                                    MsgBox("Enter Spot Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If

' ''                                If Val(grow.Cells("lv").Value) = 0 Then
' ''                                    MsgBox("Enter Volatality Value for Selected Call or Put.")
' ''                                    grdtrad.Focus()
' ''                                    Return False

' ''                                End If
' ''                                totcpr = 0
' ''                                Dim cd As Date '= CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                If (IsDate(col.ColumnName)) Then
' ''                                    cd = CDate(Format(CDate(col.ColumnName), "MMM/dd/yyyy"))
' ''                                    Dim _mT As Double = DateDiff(DateInterval.Day, CDate(cd.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    'Dim _mT As Double = DateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, dcount, dttoday.Value.Date)), CDate(grow.Cells(2).Value))
' ''                                    Dim _mT1 As Double = DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)
' ''                                    If CDate(dttoday.Value.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT1 = 0.5
' ''                                    End If
' ''                                    If CDate(cd.Date) = CDate(grow.Cells("TimeII").Value).Date Then
' ''                                        _mT = 0.5
' ''                                    End If
' ''                                    If _mT = 0 Then
' ''                                        _mT = 0.00001
' ''                                        _mT1 = 0.00001
' ''                                    Else
' ''                                        _mT = (_mT) / 365
' ''                                        _mT1 = (_mT1) / 365
' ''                                    End If
' ''                                    bval = 0
' ''                                    bvol = 0

' ''                                    bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    mVega = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 3)

' ''                                    mD1 = CalD1(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)
' ''                                    mD2 = CalD2(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, bvol, _mT)

' ''                                    'bval = CalVolga(mVega, mD1, mD2, bvol)
' ''                                    bval = CalVanna(Val(dr("SpotValue")), mVega, mD1, mD2, bvol, _mT)


' ''                                    'bvol = Greeks.Black_Scholes(Val(txtmkt.Text), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, (Val(grow.Cells("last").Value)), _mT1, iscall, True, 0, 6)
' ''                                    'bval = Greeks.Black_Scholes(Val(dr("SpotValue")), Val(grow.Cells("Strike").Value), Mrateofinterast, 0, bvol, _mT, iscall, True, 0, 4)

' ''                                    totcpr = (bval * Val(grow.Cells("units").Value))
' ''                                    dr(col.ColumnName) = Math.Round(Val(dr(col.ColumnName)) + totcpr, DecimalSetting.iRVanna_Val)
' ''                                    'dr(col.ColumnName) = Format(val(dr(col.ColumnName)) + totcpr, DecimalSetting.sVannaval)
' ''                                End If
' ''                            End If
' ''                        End If
' ''                    Next
' ''                    dcount = dcount + 1
' ''                End If
' ''                i = i + 1
' ''                vannaarr.Add(dr(col.ColumnName))
' ''            Next
' ''        Next

' ''        grdvanna.DataSource = vannatable
' ''        Return True

' ''        'grdvanna.Refresh()

' ''    End Function


' ''    Private Sub create_chart_profit()
' ''        chtprofit.ColumnCount = 0
' ''        chtprofit.ColumnLabelCount = 0
' ''        chtprofit.ColumnLabelIndex = 0
' ''        chtprofit.AutoIncrement = False
' ''        chtprofit.RowCount = 0
' ''        chtprofit.RowLabelCount = 0
' ''        chtprofit.RowLabelIndex = 0
' ''        chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


' ''        If profit.Columns.Count > 1 Then
' ''            If profit.Columns.Count < 3 Then
' ''                Exit Sub
' ''            End If
' ''            chtprofit.ColumnCount = profit.Columns.Count - 3
' ''            chtprofit.ColumnLabelCount = 1
' ''            chtprofit.ColumnLabelIndex = 1
' ''            chtprofit.AutoIncrement = False
' ''            chtprofit.RowCount = profit.Rows.Count - 1
' ''            chtprofit.RowLabelCount = 1
' ''            chtprofit.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(profitarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(profitarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            'With chtprofit
' ''            '    Dim div As Double
' ''            '    div = (((Math.Abs(maxval) - Math.Abs(minval))) * 40) / 100
' ''            '    chtprofit.chartType = VtChChartType.VtChChartType2dLine
' ''            '    YAxis = .Plot.Axis(VtChAxisId.VtChAxisIdY, 1)
' ''            '    'Set Axis Scale Min and Max values      
' ''            '    YAxis.ValueScale.Auto = False
' ''            '    YAxis.CategoryScale.Auto = False
' ''            '    YAxis.ValueScale.Minimum = minval
' ''            '    YAxis.ValueScale.Maximum = maxval
' ''            '    YAxis.ValueScale.MajorDivision = Format(((Math.Abs(maxval) - Math.Abs(minval)) \ div), "##0")
' ''            '    YAxis.ValueScale.MinorDivision = 2
' ''            'End With
' ''            'YAxis = Nothing


' ''            chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(profit.Rows.Count - 1, profit.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In profit.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    r = 0
' ''                    For Each drow As DataRow In profit.Rows
' ''                        cha(r, c) = Val(drow(col.ColumnName)) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lblprofit.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblprofit.Text = "-"
' ''            End If
' ''            chtprofit.ChartData = cha

' ''            chtprofit.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0





' ''            For Each col As DataColumn In profit.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    chtprofit.Column = c
' ''                    chtprofit.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")

' ''                    i += 1
' ''                    c += 1
' ''                End If
' ''                'If c >= grdprofit.Columns.Count Then
' ''                '    Exit For
' ''                'End If
' ''            Next
' ''            r = 1

' ''            For Each drow As DataRow In profit.Rows
' ''                chtprofit.Row = r
' ''                chtprofit.RowLabel = drow("spotvalue")

' ''                r += 1
' ''            Next
' ''            'MsgBox(chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).AxisGrid.MajorPen.)
' ''            'chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 0).AxisGrid.MajorPen.Style = VtPenStyle.VtPenStyleDotted
' ''            'chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).AxisGrid.MinorPen.Style = VtPenStyle.VtPenStyleDitted
' ''            '  chtprofit.Plot.Axis(0).Labels(0).Format
' ''            'MsgBox(chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).Labels(0))

' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_profit_Full()
' ''        objProfitLossChart.chtprofit.ColumnCount = 0
' ''        objProfitLossChart.chtprofit.ColumnLabelCount = 0
' ''        objProfitLossChart.chtprofit.ColumnLabelIndex = 0
' ''        objProfitLossChart.chtprofit.AutoIncrement = False
' ''        objProfitLossChart.chtprofit.RowCount = 0
' ''        objProfitLossChart.chtprofit.RowLabelCount = 0
' ''        objProfitLossChart.chtprofit.RowLabelIndex = 0
' ''        objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone


' ''        If profit.Columns.Count > 2 Then
' ''            objProfitLossChart.chtprofit.ColumnCount = profit.Columns.Count - 3
' ''            objProfitLossChart.chtprofit.ColumnLabelCount = 1
' ''            objProfitLossChart.chtprofit.ColumnLabelIndex = 1
' ''            objProfitLossChart.chtprofit.AutoIncrement = False
' ''            objProfitLossChart.chtprofit.RowCount = profit.Rows.Count - 1
' ''            objProfitLossChart.chtprofit.RowLabelCount = 1
' ''            objProfitLossChart.chtprofit.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(profitarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(profitarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            'With objProfitLossChart.chtprofit
' ''            '    Dim div As Double
' ''            '    div = (((Math.Abs(maxval) - Math.Abs(minval))) * 40) / 100
' ''            '    objProfitLossChart.chtprofit.chartType = VtChChartType.VtChChartType2dLine
' ''            '    YAxis = .Plot.Axis(VtChAxisId.VtChAxisIdY, 1)
' ''            '    'Set Axis Scale Min and Max values      
' ''            '    YAxis.ValueScale.Auto = False
' ''            '    YAxis.CategoryScale.Auto = False
' ''            '    YAxis.ValueScale.Minimum = minval
' ''            '    YAxis.ValueScale.Maximum = maxval
' ''            '    YAxis.ValueScale.MajorDivision = Format(((Math.Abs(maxval) - Math.Abs(minval)) \ div), "##0")
' ''            '    YAxis.ValueScale.MinorDivision = 2
' ''            'End With
' ''            'YAxis = Nothing


' ''            objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(profit.Rows.Count - 1, profit.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In profit.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    r = 0
' ''                    For Each drow As DataRow In profit.Rows
' ''                        cha(r, c) = Val(drow(col.ColumnName)) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lblprofit.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblprofit.Text = "-"
' ''            End If
' ''            objProfitLossChart.chtprofit.ChartData = cha

' ''            objProfitLossChart.chtprofit.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0





' ''            For Each col As DataColumn In profit.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then
' ''                    objProfitLossChart.chtprofit.Column = c
' ''                    objProfitLossChart.chtprofit.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")

' ''                    i += 1
' ''                    c += 1
' ''                End If
' ''                'If c >= grdprofit.Columns.Count Then
' ''                '    Exit For
' ''                'End If
' ''            Next
' ''            r = 1

' ''            For Each drow As DataRow In profit.Rows
' ''                objProfitLossChart.chtprofit.Row = r
' ''                objProfitLossChart.chtprofit.RowLabel = drow("spotvalue")

' ''                r += 1
' ''            Next
' ''            'MsgBox(objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).AxisGrid.MajorPen.)
' ''            'objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 0).AxisGrid.MajorPen.Style = VtPenStyle.VtPenStyleDotted
' ''            'objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY, 1).AxisGrid.MinorPen.Style = VtPenStyle.VtPenStyleDitted
' ''            '  objProfitLossChart.chtprofit.Plot.Axis(0).Labels(0).Format
' ''            'MsgBox(objProfitLossChart.chtprofit.Plot.Axis(VtChAxisId.VtChAxisIdY).Labels(0))

' ''        End If

' ''    End Sub
' ''    Private Sub create_chart_delta()
' ''        chtdelta.ColumnCount = 0
' ''        chtdelta.ColumnLabelCount = 0
' ''        chtdelta.ColumnLabelIndex = 0
' ''        chtdelta.AutoIncrement = False
' ''        chtdelta.RowCount = deltatable.Rows.Count - 1
' ''        chtdelta.RowLabelCount = 0
' ''        chtdelta.RowLabelIndex = 0
' ''        chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If deltatable.Columns.Count > 2 Then
' ''            chtdelta.ColumnCount = deltatable.Columns.Count - 3
' ''            chtdelta.ColumnLabelCount = 1
' ''            chtdelta.ColumnLabelIndex = 1
' ''            chtdelta.AutoIncrement = False
' ''            chtdelta.RowCount = deltatable.Rows.Count - 1
' ''            chtdelta.RowLabelCount = 1
' ''            chtdelta.RowLabelIndex = 1

' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(deltaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(deltaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(deltatable.Rows.Count - 1, deltatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In deltatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In deltatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            chtdelta.ChartData = cha
' ''            If div > 1 Then
' ''                lbldelta.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lbldelta.Text = "-"
' ''            End If
' ''            chtdelta.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0

' ''            For Each col As DataColumn In deltatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    chtdelta.Column = c
' ''                    chtdelta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grddelta.Columns.Count Then
' ''                    '    Exit For
' ''                    'End If
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In deltatable.Rows
' ''                chtdelta.Row = r
' ''                chtdelta.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_delta_Full()
' ''        objProfitLossChart.chtdelta.ColumnCount = 0
' ''        objProfitLossChart.chtdelta.ColumnLabelCount = 0
' ''        objProfitLossChart.chtdelta.ColumnLabelIndex = 0
' ''        objProfitLossChart.chtdelta.AutoIncrement = False
' ''        objProfitLossChart.chtdelta.RowCount = deltatable.Rows.Count - 1

' ''        objProfitLossChart.chtdelta.RowLabelCount = 0
' ''        objProfitLossChart.chtdelta.RowLabelIndex = 0
' ''        objProfitLossChart.chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If deltatable.Columns.Count > 2 Then
' ''            objProfitLossChart.chtdelta.ColumnCount = deltatable.Columns.Count - 3
' ''            objProfitLossChart.chtdelta.ColumnLabelCount = 1
' ''            objProfitLossChart.chtdelta.ColumnLabelIndex = 1
' ''            objProfitLossChart.chtdelta.AutoIncrement = False
' ''            objProfitLossChart.chtdelta.RowCount = deltatable.Rows.Count - 1
' ''            objProfitLossChart.chtdelta.RowLabelCount = 1
' ''            objProfitLossChart.chtdelta.RowLabelIndex = 1

' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(deltaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(deltaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            objProfitLossChart.chtdelta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(deltatable.Rows.Count - 1, deltatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In deltatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In deltatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            objProfitLossChart.chtdelta.ChartData = cha
' ''            If div > 1 Then
' ''                lbldelta.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lbldelta.Text = "-"
' ''            End If
' ''            objProfitLossChart.chtdelta.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0

' ''            For Each col As DataColumn In deltatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    objProfitLossChart.chtdelta.Column = c
' ''                    objProfitLossChart.chtdelta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grddelta.Columns.Count Then
' ''                    '    Exit For
' ''                    'End If
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In deltatable.Rows
' ''                objProfitLossChart.chtdelta.Row = r
' ''                objProfitLossChart.chtdelta.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_gamma()
' ''        chtgamma.ColumnCount = 0
' ''        chtgamma.ColumnLabelCount = 0
' ''        chtgamma.ColumnLabelIndex = 0
' ''        chtgamma.AutoIncrement = False
' ''        chtgamma.RowCount = 0
' ''        chtgamma.RowLabelCount = 0
' ''        chtgamma.RowLabelIndex = 0
' ''        chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If gammatable.Columns.Count > 2 Then
' ''            chtgamma.ColumnCount = gammatable.Columns.Count - 3
' ''            chtgamma.ColumnLabelCount = 1
' ''            chtgamma.ColumnLabelIndex = 1
' ''            chtgamma.AutoIncrement = False
' ''            chtgamma.RowCount = gammatable.Rows.Count - 1
' ''            chtgamma.RowLabelCount = 1
' ''            chtgamma.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(gammaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(gammaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(gammatable.Rows.Count - 1, gammatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In gammatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In gammatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            chtgamma.ChartData = cha
' ''            If div > 1 Then
' ''                lblgamma.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblgamma.Text = "-"
' ''            End If
' ''            chtgamma.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            For Each col As DataColumn In gammatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    chtgamma.Column = c
' ''                    chtgamma.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                    'End If
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In gammatable.Rows
' ''                chtgamma.Row = r
' ''                chtgamma.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_gamma_Full()

' ''        objProfitLossChart.chtgamma.ColumnCount = 0
' ''        objProfitLossChart.chtgamma.ColumnLabelCount = 0
' ''        objProfitLossChart.chtgamma.ColumnLabelIndex = 0
' ''        objProfitLossChart.chtgamma.AutoIncrement = False
' ''        objProfitLossChart.chtgamma.RowCount = 0
' ''        objProfitLossChart.chtgamma.RowLabelCount = 0
' ''        objProfitLossChart.chtgamma.RowLabelIndex = 0
' ''        objProfitLossChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If gammatable.Columns.Count > 2 Then
' ''            objProfitLossChart.chtgamma.ColumnCount = gammatable.Columns.Count - 3
' ''            objProfitLossChart.chtgamma.ColumnLabelCount = 1
' ''            objProfitLossChart.chtgamma.ColumnLabelIndex = 1
' ''            objProfitLossChart.chtgamma.AutoIncrement = False
' ''            objProfitLossChart.chtgamma.RowCount = gammatable.Rows.Count - 1
' ''            objProfitLossChart.chtgamma.RowLabelCount = 1
' ''            objProfitLossChart.chtgamma.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(gammaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(gammaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            objProfitLossChart.chtgamma.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(gammatable.Rows.Count - 1, gammatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In gammatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In gammatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            objProfitLossChart.chtgamma.ChartData = cha
' ''            If div > 1 Then
' ''                lblgamma.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblgamma.Text = "-"
' ''            End If
' ''            objProfitLossChart.chtgamma.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            For Each col As DataColumn In gammatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    objProfitLossChart.chtgamma.Column = c
' ''                    objProfitLossChart.chtgamma.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                    'End If
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In gammatable.Rows
' ''                objProfitLossChart.chtgamma.Row = r
' ''                objProfitLossChart.chtgamma.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub

' ''    Private Sub create_chart_vega()
' ''        chtvega.ColumnCount = 0
' ''        chtvega.ColumnLabelCount = 0
' ''        chtvega.ColumnLabelIndex = 0
' ''        chtvega.AutoIncrement = False
' ''        chtvega.RowCount = 0
' ''        chtvega.RowLabelCount = 0
' ''        chtvega.RowLabelIndex = 0
' ''        chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If vegatable.Columns.Count > 2 Then

' ''            chtvega.ColumnCount = vegatable.Columns.Count - 3
' ''            chtvega.ColumnLabelCount = 1
' ''            chtvega.ColumnLabelIndex = 1
' ''            chtvega.AutoIncrement = False
' ''            chtvega.RowCount = vegatable.Rows.Count - 1
' ''            chtvega.RowLabelCount = 1
' ''            chtvega.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(vegaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(vegaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(vegatable.Rows.Count - 1, vegatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In vegatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In vegatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            chtvega.ChartData = cha
' ''            If div > 1 Then
' ''                lblvega.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblvega.Text = "-"
' ''            End If
' ''            chtvega.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            For Each col As DataColumn In vegatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    chtvega.Column = c
' ''                    chtvega.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next


' ''            r = 1
' ''            For Each drow As DataRow In vegatable.Rows
' ''                chtvega.Row = r
' ''                chtvega.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_vega_Full()
' ''        objProfitLossChart.chtvega.ColumnCount = 0
' ''        objProfitLossChart.chtvega.ColumnLabelCount = 0
' ''        objProfitLossChart.chtvega.ColumnLabelIndex = 0
' ''        objProfitLossChart.chtvega.AutoIncrement = False
' ''        objProfitLossChart.chtvega.RowCount = 0
' ''        objProfitLossChart.chtvega.RowLabelCount = 0
' ''        objProfitLossChart.chtvega.RowLabelIndex = 0
' ''        objProfitLossChart.chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If vegatable.Columns.Count > 2 Then

' ''            objProfitLossChart.chtvega.ColumnCount = vegatable.Columns.Count - 3
' ''            objProfitLossChart.chtvega.ColumnLabelCount = 1
' ''            objProfitLossChart.chtvega.ColumnLabelIndex = 1
' ''            objProfitLossChart.chtvega.AutoIncrement = False
' ''            objProfitLossChart.chtvega.RowCount = vegatable.Rows.Count - 1
' ''            objProfitLossChart.chtvega.RowLabelCount = 1
' ''            objProfitLossChart.chtvega.RowLabelIndex = 1
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(vegaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(vegaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            objProfitLossChart.chtvega.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim cha(vegatable.Rows.Count - 1, vegatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In vegatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In vegatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            objProfitLossChart.chtvega.ChartData = cha
' ''            If div > 1 Then
' ''                lblvega.Text = "Rs.in ( " & str & ")"
' ''            Else
' ''                lblvega.Text = "-"
' ''            End If
' ''            objProfitLossChart.chtvega.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            For Each col As DataColumn In vegatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    objProfitLossChart.chtvega.Column = c
' ''                    objProfitLossChart.chtvega.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1
' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next


' ''            r = 1
' ''            For Each drow As DataRow In vegatable.Rows
' ''                objProfitLossChart.chtvega.Row = r
' ''                objProfitLossChart.chtvega.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_theta()
' ''        chttheta.ColumnCount = 0
' ''        chttheta.ColumnLabelCount = 0
' ''        chttheta.ColumnLabelIndex = 0
' ''        chttheta.AutoIncrement = False
' ''        chttheta.RowCount = 0
' ''        chttheta.RowLabelCount = 0
' ''        chttheta.RowLabelIndex = 0
' ''        chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If thetatable.Columns.Count > 2 Then
' ''            chttheta.ColumnCount = thetatable.Columns.Count - 3
' ''            chttheta.ColumnLabelCount = 1
' ''            chttheta.ColumnLabelIndex = 1
' ''            chttheta.AutoIncrement = False
' ''            chttheta.RowCount = thetatable.Rows.Count - 1
' ''            chttheta.RowLabelCount = 1
' ''            chttheta.RowLabelIndex = 1
' ''            chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(thetaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(thetaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            Dim cha(thetatable.Rows.Count - 1, thetatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In thetatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In thetatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lbltheta.Text = "Rs. in ( " & str & ")"
' ''            Else
' ''                lbltheta.Text = "-"
' ''            End If
' ''            chttheta.ChartData = cha

' ''            chttheta.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
' ''            'End If
' ''            For Each col As DataColumn In thetatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    chttheta.Column = c
' ''                    chttheta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1

' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In thetatable.Rows
' ''                chttheta.Row = r
' ''                chttheta.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_theta_Full()
' ''        objProfitLossChart.chttheta.ColumnCount = 0
' ''        objProfitLossChart.chttheta.ColumnLabelCount = 0
' ''        objProfitLossChart.chttheta.ColumnLabelIndex = 0
' ''        objProfitLossChart.chttheta.AutoIncrement = False
' ''        objProfitLossChart.chttheta.RowCount = 0
' ''        objProfitLossChart.chttheta.RowLabelCount = 0
' ''        objProfitLossChart.chttheta.RowLabelIndex = 0
' ''        objProfitLossChart.chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If thetatable.Columns.Count > 2 Then
' ''            objProfitLossChart.chttheta.ColumnCount = thetatable.Columns.Count - 3
' ''            objProfitLossChart.chttheta.ColumnLabelCount = 1
' ''            objProfitLossChart.chttheta.ColumnLabelIndex = 1
' ''            objProfitLossChart.chttheta.AutoIncrement = False
' ''            objProfitLossChart.chttheta.RowCount = thetatable.Rows.Count - 1
' ''            objProfitLossChart.chttheta.RowLabelCount = 1
' ''            objProfitLossChart.chttheta.RowLabelIndex = 1
' ''            objProfitLossChart.chttheta.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(thetaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(thetaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            Dim cha(thetatable.Rows.Count - 1, thetatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In thetatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In thetatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lbltheta.Text = "Rs. in ( " & str & ")"
' ''            Else
' ''                lbltheta.Text = "-"
' ''            End If
' ''            objProfitLossChart.chttheta.ChartData = cha

' ''            objProfitLossChart.chttheta.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
' ''            'End If
' ''            For Each col As DataColumn In thetatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    objProfitLossChart.chttheta.Column = c
' ''                    objProfitLossChart.chttheta.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1

' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In thetatable.Rows
' ''                objProfitLossChart.chttheta.Row = r
' ''                objProfitLossChart.chttheta.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    '------------
' ''    Private Sub create_chart_volga()
' ''        chtvolga.ColumnCount = 0
' ''        chtvolga.ColumnLabelCount = 0
' ''        chtvolga.ColumnLabelIndex = 0
' ''        chtvolga.AutoIncrement = False
' ''        chtvolga.RowCount = 0
' ''        chtvolga.RowLabelCount = 0
' ''        chtvolga.RowLabelIndex = 0
' ''        chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If volgatable.Columns.Count > 2 Then
' ''            chtvolga.ColumnCount = volgatable.Columns.Count - 3
' ''            chtvolga.ColumnLabelCount = 1
' ''            chtvolga.ColumnLabelIndex = 1
' ''            chtvolga.AutoIncrement = False
' ''            chtvolga.RowCount = volgatable.Rows.Count - 1
' ''            chtvolga.RowLabelCount = 1
' ''            chtvolga.RowLabelIndex = 1
' ''            chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(volgaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(volgaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            Dim cha(volgatable.Rows.Count - 1, volgatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In volgatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In volgatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lblvolga.Text = "Rs. in ( " & str & ")"
' ''            Else
' ''                lblvolga.Text = "-"
' ''            End If
' ''            chtvolga.ChartData = cha

' ''            chtvolga.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
' ''            'End If
' ''            For Each col As DataColumn In volgatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    chtvolga.Column = c
' ''                    chtvolga.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1

' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In volgatable.Rows
' ''                chtvolga.Row = r
' ''                chtvolga.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_volga_Full()
' ''        objProfitLossChart.chtvolga.ColumnCount = 0
' ''        objProfitLossChart.chtvolga.ColumnLabelCount = 0
' ''        objProfitLossChart.chtvolga.ColumnLabelIndex = 0
' ''        objProfitLossChart.chtvolga.AutoIncrement = False
' ''        objProfitLossChart.chtvolga.RowCount = 0
' ''        objProfitLossChart.chtvolga.RowLabelCount = 0
' ''        objProfitLossChart.chtvolga.RowLabelIndex = 0
' ''        objProfitLossChart.chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If volgatable.Columns.Count > 2 Then
' ''            objProfitLossChart.chtvolga.ColumnCount = volgatable.Columns.Count - 3
' ''            objProfitLossChart.chtvolga.ColumnLabelCount = 1
' ''            objProfitLossChart.chtvolga.ColumnLabelIndex = 1
' ''            objProfitLossChart.chtvolga.AutoIncrement = False
' ''            objProfitLossChart.chtvolga.RowCount = volgatable.Rows.Count - 1
' ''            objProfitLossChart.chtvolga.RowLabelCount = 1
' ''            objProfitLossChart.chtvolga.RowLabelIndex = 1
' ''            objProfitLossChart.chtvolga.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(volgaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(volgaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            Dim cha(volgatable.Rows.Count - 1, volgatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In volgatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In volgatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lblvolga.Text = "Rs. in ( " & str & ")"
' ''            Else
' ''                lblvolga.Text = "-"
' ''            End If
' ''            objProfitLossChart.chtvolga.ChartData = cha

' ''            objProfitLossChart.chtvolga.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
' ''            'End If
' ''            For Each col As DataColumn In volgatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    objProfitLossChart.chtvolga.Column = c
' ''                    objProfitLossChart.chtvolga.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1

' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In volgatable.Rows
' ''                objProfitLossChart.chtvolga.Row = r
' ''                objProfitLossChart.chtvolga.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    '-------------
' ''    Private Sub create_chart_vanna()
' ''        chtvanna.ColumnCount = 0
' ''        chtvanna.ColumnLabelCount = 0
' ''        chtvanna.ColumnLabelIndex = 0
' ''        chtvanna.AutoIncrement = False
' ''        chtvanna.RowCount = 0
' ''        chtvanna.RowLabelCount = 0
' ''        chtvanna.RowLabelIndex = 0
' ''        chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If vannatable.Columns.Count > 2 Then
' ''            chtvanna.ColumnCount = vannatable.Columns.Count - 3
' ''            chtvanna.ColumnLabelCount = 1
' ''            chtvanna.ColumnLabelIndex = 1
' ''            chtvanna.AutoIncrement = False
' ''            chtvanna.RowCount = vannatable.Rows.Count - 1
' ''            chtvanna.RowLabelCount = 1
' ''            chtvanna.RowLabelIndex = 1
' ''            chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(vannaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(vannaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            Dim cha(vannatable.Rows.Count - 1, vannatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In vannatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In vannatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lblvanna.Text = "Rs. in ( " & str & ")"
' ''            Else
' ''                lblvanna.Text = "-"
' ''            End If
' ''            chtvanna.ChartData = cha

' ''            chtvanna.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
' ''            'End If
' ''            For Each col As DataColumn In vannatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    chtvanna.Column = c
' ''                    chtvanna.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1

' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In vannatable.Rows
' ''                chtvanna.Row = r
' ''                chtvanna.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub create_chart_vanna_Full()
' ''        objProfitLossChart.chtvanna.ColumnCount = 0
' ''        objProfitLossChart.chtvanna.ColumnLabelCount = 0
' ''        objProfitLossChart.chtvanna.ColumnLabelIndex = 0
' ''        objProfitLossChart.chtvanna.AutoIncrement = False
' ''        objProfitLossChart.chtvanna.RowCount = 0
' ''        objProfitLossChart.chtvanna.RowLabelCount = 0
' ''        objProfitLossChart.chtvanna.RowLabelIndex = 0
' ''        objProfitLossChart.chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''        If vannatable.Columns.Count > 2 Then
' ''            objProfitLossChart.chtvanna.ColumnCount = vannatable.Columns.Count - 3
' ''            objProfitLossChart.chtvanna.ColumnLabelCount = 1
' ''            objProfitLossChart.chtvanna.ColumnLabelIndex = 1
' ''            objProfitLossChart.chtvanna.AutoIncrement = False
' ''            objProfitLossChart.chtvanna.RowCount = vannatable.Rows.Count - 1
' ''            objProfitLossChart.chtvanna.RowLabelCount = 1
' ''            objProfitLossChart.chtvanna.RowLabelIndex = 1
' ''            objProfitLossChart.chtvanna.Plot.Axis(VtChAxisId.VtChAxisIdX).Tick.Style = VtChAxisTickStyle.VtChAxisTickStyleNone
' ''            Dim div As Long = 1
' ''            Dim str As String = ""
' ''            'Dim YAxis As Axis
' ''            Dim minval As Double = Val(MinValOfIntArray(vannaarr.ToArray))
' ''            Dim maxval As Double = Val(MaxValOfIntArray(vannaarr.ToArray))
' ''            If maxval >= 100000000 Or Math.Abs(minval) >= 100000000 Then
' ''                div = 100000
' ''                str = "Lacs"
' ''            ElseIf maxval >= 10000000 Or Math.Abs(minval) >= 10000000 Then
' ''                div = 1000
' ''                str = "Thousands"
' ''            ElseIf maxval >= 1000000 Or Math.Abs(minval) >= 1000000 Then
' ''                div = 100
' ''                str = "Hundreds"
' ''            Else
' ''                div = 1
' ''            End If
' ''            Dim cha(vannatable.Rows.Count - 1, vannatable.Columns.Count - 3) As Object
' ''            Dim c As Integer
' ''            c = 0
' ''            Dim r As Integer
' ''            r = 0
' ''            For Each col As DataColumn In vannatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    r = 0
' ''                    For Each drow As DataRow In vannatable.Rows
' ''                        cha(r, c) = drow(col.ColumnName) / div
' ''                        r += 1
' ''                    Next
' ''                    c += 1
' ''                End If
' ''            Next
' ''            If div > 1 Then
' ''                lblvanna.Text = "Rs. in ( " & str & ")"
' ''            Else
' ''                lblvanna.Text = "-"
' ''            End If
' ''            objProfitLossChart.chtvanna.ChartData = cha

' ''            objProfitLossChart.chtvanna.ShowLegend = False
' ''            c = 1
' ''            Dim i As Integer = 0
' ''            'If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            '    i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date) + 1)
' ''            'End If
' ''            For Each col As DataColumn In vannatable.Columns
' ''                If UCase(col.ColumnName) <> UCase("spotvalue") And UCase(col.ColumnName) <> UCase("Percent(%)") Then

' ''                    objProfitLossChart.chtvanna.Column = c
' ''                    objProfitLossChart.chtvanna.ColumnLabel = Format(DateAdd(DateInterval.Day, i, dttoday.Value), "dd/MMM/yyyy")
' ''                    i += 1

' ''                    c += 1
' ''                    'If c >= grdgamma.Columns.Count Then
' ''                    '    Exit For
' ''                End If
' ''            Next
' ''            r = 1
' ''            For Each drow As DataRow In vannatable.Rows
' ''                objProfitLossChart.chtvanna.Row = r
' ''                objProfitLossChart.chtvanna.RowLabel = drow("spotvalue")
' ''                r += 1
' ''            Next
' ''        End If
' ''    End Sub
' ''    Private Sub cal_exp()
' ''        Dim exptable As New DataTable

' ''        exptable = objExp.Select_Expenses

' ''        Dim texp As Double = 0

' ''        For Each drow As DataGridViewRow In grdtrad.Rows
' ''            If drow.Cells("Active").Value = False Then Continue For
' ''            If Val(drow.Cells("units").Value) > 0 Then
' ''                If drow.Cells("CPF").Value = "F" Then
' ''                    texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(futl)", "")) / exptable.Compute("max(futlp)", "")))
' ''                Else
' ''                    If Val(exptable.Compute("max(spl)", "")) <> 0 Then
' ''                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * (Val(drow.Cells("last").Value) + Val(drow.Cells("last").Value))) * exptable.Compute("max(spl)", "")) / exptable.Compute("max(splp)", "")))
' ''                    Else
' ''                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(prel)", "")) / exptable.Compute("max(prelp)", "")))
' ''                    End If
' ''                End If
' ''            Else
' ''                If drow.Cells("CPF").Value = "F" Then
' ''                    texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(futs)", "")) / exptable.Compute("max(futsp)", "")))
' ''                Else
' ''                    If Val(exptable.Compute("max(spl)", "")) <> 0 Then
' ''                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * (Val(drow.Cells("last").Value) + Val(drow.Cells("last").Value))) * exptable.Compute("max(sps)", "")) / exptable.Compute("max(spsp)", "")))
' ''                    Else
' ''                        texp = texp + Math.Abs(Val(((Val(drow.Cells("units").Value) * Val(drow.Cells("last").Value)) * exptable.Compute("max(pres)", "")) / exptable.Compute("max(presp)", "")))
' ''                    End If
' ''                End If
' ''            End If
' ''        Next
' ''        txtexp.Text = Math.Round(texp, 2)
' ''    End Sub
' ''    Private Sub import()
' ''        Dim opfile As OpenFileDialog
' ''        opfile = New OpenFileDialog
' ''        opfile.Filter = "Files(*.xls)|*.xls"
' ''        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
' ''            read_file(opfile.FileName)
' ''        End If
' ''    End Sub
' ''    Private Sub read_file(ByVal fpath As String)
' ''        Try
' ''            If fpath <> "" Then
' ''                Dim fi As New FileInfo(fpath)
' ''                'Dim dv As DataView
' ''                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & fpath
' ''                'Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
' ''                Dim drow As DataRow
' ''                Dim objConn As New OleDbConnection(sConnectionString)

' ''                objConn.Open()

' ''                'Dim objCmdSelect As New OleDbCommand("SELECT Staus,'Time I','Time II',C/P/F,Spot,Strike,Qty,Rate,'Vol(%)',Delta,'Del. Val',Theta,Th.Val,Vega,'Vg. Val',Gamma,'Gam. Val',uid FROM " & fi.Name, objConn)
' ''                'Dim objCmdSelect As New OleDbCommand("SELECT * FROM " & fi.Name, objConn)
' ''                Dim objCmdSelect As New OleDbCommand("SELECT * FROM [Scenario$]", objConn)

' ''                Dim objAdapter1 As New OleDbDataAdapter

' ''                objAdapter1.SelectCommand = objCmdSelect
' ''                Dim tempdata As New DataTable
' ''                tempdata = New DataTable
' ''                objAdapter1.Fill(tempdata)
' ''                objConn.Close()
' ''                grdtrad.Rows.Clear()
' ''                txtunits.Text = 0
' ''                txtdelval.Text = 0
' ''                txtthval.Text = 0
' ''                txtvgval.Text = 0
' ''                txtgmval.Text = 0
' ''                txtVolgaVal.Text = 0
' ''                TxtVannaVal.Text = 0
' ''                Dim grow As New DataGridViewRow
' ''                For Each drow In tempdata.Rows
' ''                    grow = New DataGridViewRow
' ''                    grdtrad.Rows.Add(grow)
' ''                Next
' ''                Dim i As Integer = 0
' ''                For Each drow In tempdata.Rows

' ''                    grdtrad.Rows(i).Cells("Active").Value = True
' ''                    grdtrad.Rows(i).Cells("TimeI").Value = CDate(drow("Date"))
' ''                    grdtrad.Rows(i).Cells("TimeII").Value = CDate(drow("Expiry"))
' ''                    grdtrad.Rows(i).Cells("CPF").Value = CStr(drow("C/P/F"))
' ''                    grdtrad.Rows(i).Cells("spval").Value = CDbl(drow("Underlying"))
' ''                    grdtrad.Rows(i).Cells("Strike").Value = CDbl(drow("Strike"))
' ''                    grdtrad.Rows(i).Cells("units").Value = Val(drow("Qty"))
' ''                    grdtrad.Rows(i).Cells("ltp").Value = Val(drow("ltp"))
' ''                    grdtrad.Rows(i).Cells("last").Value = CDbl(drow("Rate"))
' ''                    grdtrad.Rows(i).Cells("lv").Value = Val(drow("Vol(%)"))
' ''                    grdtrad.Rows(i).Cells("delta").Value = Val(drow("Delta"))
' ''                    grdtrad.Rows(i).Cells("deltaval").Value = Val(drow("Del Val"))
' ''                    grdtrad.Rows(i).Cells("theta").Value = Val(drow("Theta"))
' ''                    grdtrad.Rows(i).Cells("thetaval").Value = Val(drow("Th val"))
' ''                    grdtrad.Rows(i).Cells("vega").Value = Val(drow("Vega"))
' ''                    grdtrad.Rows(i).Cells("vgval").Value = Val(drow("Vg val"))
' ''                    grdtrad.Rows(i).Cells("gamma").Value = Val(drow("Gamma"))
' ''                    grdtrad.Rows(i).Cells("gmval").Value = Val(drow("Gam val"))

' ''                    grdtrad.Rows(i).Cells("volga").Value = Val(drow("Volga"))
' ''                    grdtrad.Rows(i).Cells("volgaval").Value = Val(drow("Vo val"))
' ''                    grdtrad.Rows(i).Cells("vanna").Value = Val(drow("Vanna"))
' ''                    grdtrad.Rows(i).Cells("vannaval").Value = Val(drow("Va val"))


' ''                    grdtrad.Rows(i).Cells("uid").Value = Val(drow("uid"))



' ''                    '  txtunits.Text = Val(txtunits.Text) + Val(grdtrad.Rows(i).Cells(6).Value)
' ''                    txtdelval.Text = Val(txtdelval.Text) + Val(grdtrad.Rows(i).Cells("delta").Value)
' ''                    txtthval.Text = Val(txtthval.Text) + Val(grdtrad.Rows(i).Cells("thetaval").Value)
' ''                    txtvgval.Text = Val(txtvgval.Text) + Val(grdtrad.Rows(i).Cells("vega").Value)
' ''                    txtgmval.Text = Val(txtgmval.Text) + Val(grdtrad.Rows(i).Cells("gamma").Value)
' ''                    txtVolgaVal.Text = Val(txtVolgaVal.Text) + Val(grdtrad.Rows(i).Cells("volga").Value)
' ''                    TxtVannaVal.Text = Val(TxtVannaVal.Text) + Val(grdtrad.Rows(i).Cells("vanna").Value)


' ''                    i += 1
' ''                Next


' ''            End If
' ''        Catch ex As Exception
' ''            MsgBox("File Not Processed.")
' ''            ' MsgBox(ex.ToString)
' ''        End Try




' ''    End Sub
' ''    Private Sub init_result()
' ''        rtable = New DataTable
' ''        With rtable.Columns

' ''            '.Add("timeI", GetType(Date))
' ''            '.Add("timeII", GetType(Date))
' ''            '.Add("cpf")
' ''            '.Add("spval", GetType(Double))
' ''            '.Add("strike", GetType(Double))
' ''            '.Add("units", GetType(Double))
' ''            '.Add("last", GetType(Double))
' ''            '.Add("lv", GetType(Double))
' ''            '.Add("delta", GetType(Double))
' ''            '.Add("deltaval", GetType(Double))
' ''            '.Add("theta", GetType(Double))
' ''            '.Add("thetaval", GetType(Double))
' ''            '.Add("vega", GetType(Double))
' ''            '.Add("vgval", GetType(Double))
' ''            '.Add("gamma", GetType(Double))
' ''            '.Add("gmval", GetType(Double))

' ''            .Add("timeI")
' ''            .Add("timeII")
' ''            .Add("cpf")
' ''            .Add("spval", GetType(Double))
' ''            .Add("strike", GetType(Double))
' ''            .Add("units", GetType(Double))
' ''            .Add("last", GetType(Double))
' ''            .Add("lv", GetType(Double))
' ''            .Add("delta", GetType(Double))
' ''            .Add("deltaval", GetType(Double))
' ''            .Add("theta", GetType(Double))
' ''            .Add("thetaval", GetType(Double))
' ''            .Add("vega", GetType(Double))
' ''            .Add("vgval", GetType(Double))
' ''            .Add("gamma", GetType(Double))
' ''            .Add("gmval", GetType(Double))
' ''            .Add("volga", GetType(Double))
' ''            .Add("volgaval", GetType(Double))
' ''            .Add("vanna", GetType(Double))
' ''            .Add("vannaval", GetType(Double))
' ''            .Add("pl", GetType(Double))


' ''        End With
' ''    End Sub
' ''    Private Sub fill_result(ByVal ind As Integer, ByVal rind As Integer, ByVal cind As Integer)
' ''        REM Grid  Double Click Show Volga and Vanna 
' ''        If rind = -1 Then Exit Sub
' ''        init_result()
' ''        Dim iscall As Boolean = False
' ''        Dim drow As DataRow
' ''        Dim _mT As Double = 0
' ''        Dim mD1 As Double = 0
' ''        Dim mD2 As Double = 0
' ''        Dim mVolatility As Double = 0


' ''        Select Case ind
' ''            Case 0

' ''                'If CDate(profit.Columns(cind).ColumnName) < CDate(dtexp.Text) Then
' ''                '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
' ''                '    'Else
' ''                '    '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
' ''                'End If

' ''                'mt = DateDiff(DateInterval.Day, CDate(drow("mdate")), Now())

' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    If grow.Cells("Active").Value = False Then Continue For
' ''                    If CBool(grow.Cells("Active").Value) = True  And  Val(grow.Cells("units").Value) <> 0  Then
' ''                        drow = rtable.NewRow
' ''                        If (IsDate(profit.Columns(cind).ColumnName)) Then
' ''                            drow("timeI") = Format(CDate(profit.Columns(cind).ColumnName), "MMM/dd/yyyy")

' ''                        ElseIf (cind) = profit.Columns.Count - 1 Then
' ''                            drow("timeI") = Format(CDate(Mid(profit.Columns(cind).ColumnName, 7, Len(profit.Columns(cind).ColumnName) - 1)), "MMM/dd/yyyy")
' ''                        Else
' ''                            Exit Sub
' ''                            'drow("timeI") = Format(CDate(Mid(profit.Columns(cind).ColumnName, 1, Len(profit.Columns(cind).ColumnName) - 4)), "MMM/dd/yyyy")
' ''                        End If



' ''                        'drow("timeII") = Format(CDate(dtexp.Text), "MMM/dd/yyyy")
' ''                        'drow("timeI") = Format(CDate(grow.Cells(1).Value), "MMM/dd/yyyy")
' ''                        drow("timeII") = Format(CDate(grow.Cells("TimeII").Value), "MMM/dd/yyyy")
' ''                        drow("cpf") = grow.Cells("CPF").Value
' ''                        drow("spval") = Val(profit.Rows(rind)(0))
' ''                        drow("strike") = Val(grow.Cells("Strike").Value)
' ''                        drow("units") = Val(grow.Cells("units").Value)
' ''                        drow("last") = Val(grow.Cells("ltp").Value)            ' 9 is used
' ''                        drow("lv") = Val(grow.Cells("lv").Value)
' ''                        If grow.Cells("CPF").Value = "F" Or grow.Cells("CPF").Value = "E" Then
' ''                            drow("delta") = Val(grow.Cells("delta").Value)
' ''                            drow("deltaval") = Val(grow.Cells("deltaval").Value)
' ''                            drow("theta") = Val(0)
' ''                            drow("thetaval") = Val(0)
' ''                            drow("vega") = Val(0)
' ''                            drow("vgval") = Val(0)
' ''                            drow("gamma") = Val(0)
' ''                            drow("gmval") = Val(0)
' ''                            drow("volga") = Val(0)
' ''                            drow("volgaval") = Val(0)
' ''                            drow("vanna") = Val(0)
' ''                            drow("vannaval") = Val(0)
' ''                            drow("pl") = Math.Round(Val(((Val(profit.Rows(rind)(0)) - Val(grow.Cells("spval").Value)) * Val(grow.Cells("units").Value))), 2)
' ''                        Else

' ''                            If CDate(drow("timeI")) < CDate(drow("timeII")) Then
' ''                                If (IsDate(profit.Columns(cind).ColumnName)) Then
' ''                                    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName).Date, CDate(drow("timeII")).Date)
' ''                                Else
' ''                                    _mT = DateDiff(DateInterval.Day, CDate(Mid(profit.Columns(cind).ColumnName, 1, Len(profit.Columns(cind).ColumnName) - 4)).Date, CDate(drow("timeII")).Date)
' ''                                End If
' ''                                'Else
' ''                                '    _mT = DateDiff(DateInterval.Day, CDate(profit.Columns(cind).ColumnName), CDate(dtexp.Text))
' ''                            End If

' ''                            If grow.Cells("CPF").Value = "C" Then
' ''                                iscall = True
' ''                            Else
' ''                                iscall = False
' ''                            End If

' ''                            If _mT = 0 Then
' ''                                _mT = 0.0001
' ''                            Else
' ''                                _mT = (_mT) / 365
' ''                            End If
' ''                            Dim bval As Double
' ''                            bval = 0
' ''                            bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 1)
' ''                            drow("delta") = Math.Round(CDbl(bval), 2)
' ''                            drow("deltaval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 2)
' ''                            bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 4)
' ''                            drow("theta") = Math.Round(CDbl(bval), 2)
' ''                            drow("thetaval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)))
' ''                            bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 3)
' ''                            drow("vega") = Math.Round(CDbl(bval), 2)
' ''                            drow("vgval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 2)
' ''                            bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 2)
' ''                            drow("gamma") = Math.Round(CDbl(bval), 5)
' ''                            drow("gmval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 5)

' ''                            'Volga , Vanna
' ''                            mVolatility = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 6)

' ''                            mD1 = mD1 + CalD1(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, mVolatility, _mT)
' ''                            mD2 = mD2 + CalD2(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, mVolatility, _mT)

' ''                            drow("volga") = CalVolga(drow("vega"), mD1, mD2, mVolatility)
' ''                            drow("vanna") = CalVanna(CDbl(profit.Rows(rind)(0)), drow("vega"), mD1, mD2, mVolatility, _mT)

' ''                            drow("volgaval") = Math.Round(CDbl(drow("volga") * CDbl(grow.Cells("units").Value)), 5)
' ''                            drow("vannaval") = Math.Round(CDbl(drow("vanna") * CDbl(grow.Cells("units").Value)), 5)


' ''                            'bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 2)
' ''                            'drow("gamma") = Math.Round(CDbl(bval), 5)
' ''                            'drow("gmval") = Math.Round(CDbl(CDbl(bval) * CDbl(grow.Cells("units").Value)), 5)


' ''                            bval = Greeks.Black_Scholes(CDbl(profit.Rows(rind)(0)), CDbl(grow.Cells("Strike").Value), Mrateofinterast, 0, (CDbl(grow.Cells("lv").Value) / 100), _mT, iscall, True, 0, 0)

' ''                            drow("pl") = Math.Round(CDbl(((bval - CDbl(grow.Cells("ltp").Value)) * CDbl(grow.Cells("units").Value))), 2)  ' 9 is used
' ''                        End If
' ''                        rtable.Rows.Add(drow)
' ''                    End If
' ''                Next
' ''                drow = rtable.NewRow
' ''                drow("timeI") = ""
' ''                drow("timeII") = ""
' ''                drow("cpf") = "Total"
' ''                drow("spval") = 0
' ''                drow("strike") = 0
' ''                drow("units") = Val(rtable.Compute("sum(units)", ""))
' ''                drow("last") = 0
' ''                drow("lv") = 0
' ''                drow("delta") = 0
' ''                drow("deltaval") = Val(rtable.Compute("sum(deltaval)", ""))
' ''                drow("theta") = Val(0)
' ''                drow("thetaval") = Val(rtable.Compute("sum(thetaval)", ""))
' ''                drow("vega") = Val(0)
' ''                drow("vgval") = Val(rtable.Compute("sum(vgval)", ""))
' ''                drow("gamma") = Val(0)
' ''                drow("gmval") = Val(rtable.Compute("sum(gmval)", ""))
' ''                drow("volga") = Val(0)
' ''                drow("volgaval") = Val(rtable.Compute("sum(volgaval)", ""))
' ''                drow("vanna") = Val(0)
' ''                drow("vannaval") = Val(rtable.Compute("sum(vannaval)", ""))
' ''                drow("pl") = Math.Round(Val(rtable.Compute("sum(pl)", "")), 2) + grossmtm
' ''                rtable.Rows.Add(drow)
' ''        End Select
' ''    End Sub
' ''    Public Function MaxValOfIntArray(ByRef TheArray As Object) As Double
' ''        'This function gives max value of int array without sorting an array
' ''        Dim i As Integer
' ''        Dim MaxIntegersIndex As Integer
' ''        MaxIntegersIndex = 0

' ''        For i = 1 To UBound(TheArray)
' ''            If Val(TheArray(i)) > Val(TheArray(MaxIntegersIndex)) Then
' ''                MaxIntegersIndex = i
' ''            End If
' ''        Next
' ''        'index of max value is MaxValOfIntArray
' ''        MaxValOfIntArray = Val(TheArray(MaxIntegersIndex))
' ''    End Function
' ''    Public Function MinValOfIntArray(ByRef TheArray As Object) As Double
' ''        'This function gives max value of int array without sorting an array
' ''        Dim i As Integer
' ''        Dim MaxIntegersIndex As Integer
' ''        MaxIntegersIndex = 0

' ''        For i = 1 To UBound(TheArray)
' ''            If Val(TheArray(i)) < Val(TheArray(MaxIntegersIndex)) Then
' ''                MaxIntegersIndex = i
' ''            End If
' ''        Next
' ''        'index of max value is MaxValOfIntArray
' ''        MinValOfIntArray = Val(TheArray(MaxIntegersIndex))
' ''    End Function
' ''    Private Sub export()
' ''        Dim savedi As New SaveFileDialog
' ''        savedi.Filter = "Files(*.xls)|*.xls"
' ''        If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
' ''            Dim grd(7) As DataGridView
' ''            grd(0) = grdtrad
' ''            grd(1) = grdprofit
' ''            grd(2) = grddelta
' ''            grd(3) = grdtheta
' ''            grd(4) = grdvega
' ''            grd(5) = grdgamma
' ''            grd(6) = grdvolga
' ''            grd(7) = grdvanna
' ''            Dim sname(7) As String
' ''            sname(0) = "Scenario"
' ''            sname(1) = "Profit & Loss"
' ''            sname(2) = "Delta"
' ''            sname(3) = "Theta"
' ''            sname(4) = "Vega"
' ''            sname(5) = "Gamma"
' ''            sname(6) = "Volga"
' ''            sname(7) = "Vanna"
' ''            exporttoexcel(grd, savedi.FileName, sname, "scenario")
' ''            'exporttoexcel(grdprofit)
' ''            'Dim str As String
' ''            'str = Mid(savedi.FileName, 1, Len(savedi.FileName) - 4)
' ''            'exportExcel(grdtrad, str & "_scenario.csv")
' ''            'exportExcel(grdprofit, str & "_ProfitLoss.csv")
' ''            'exportExcel(grddelta, str & "_Delta.csv")
' ''            'exportExcel(grdtheta, str & "_Theta.csv")
' ''            'exportExcel(grdvega, str & "_Vega.csv")
' ''            'exportExcel(grdgamma, str & "_Gamma.csv")
' ''            'MsgBox("Export Complete.", MsgBoxStyle.Information)
' ''        End If
' ''    End Sub
' ''    Private Sub create_active()
' ''        grdact.Columns.Clear()
' ''        Dim i As Integer = 0
' ''        If CDate(dttoday.Value.Date) < CDate(dtexp.Value.Date) Then
' ''            i = (DateDiff(DateInterval.Day, dttoday.Value.Date, dtexp.Value.Date))
' ''        End If
' ''        i += 1
' ''        Dim gcol As DataGridViewCheckBoxColumn
' ''        Dim temptable As New DataTable
' ''        temptable = New DataTable
' ''        Dim tdate As Date
' ''        For k As Integer = 0 To i - 1
' ''            tdate = Format(DateAdd(DateInterval.Day, k, dttoday.Value), "dd/MMM/yyyy")
' ''            If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
' ''                gcol = New DataGridViewCheckBoxColumn
' ''                gcol.HeaderText = tdate
' ''                gcol.DataPropertyName = tdate
' ''                gcol.Width = 70
' ''                grdact.Columns.Add(gcol)
' ''                temptable.Columns.Add(tdate, GetType(Boolean))
' ''                gcol.DefaultCellStyle.NullValue = False
' ''            End If
' ''        Next

' ''        tdate = Format(dtexp.Value, "dd/MMM/yyyy")
' ''        If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
' ''            gcol = New DataGridViewCheckBoxColumn
' ''            gcol.HeaderText = "Expiry"
' ''            gcol.DataPropertyName = "Expiry " & tdate
' ''            grdact.Columns.Add(gcol)
' ''            temptable.Columns.Add("Expiry " & tdate, GetType(Boolean))
' ''            gcol.DefaultCellStyle.NullValue = False
' ''        End If

' ''        If grdact.Columns.Count > 0 Then
' ''            Dim drow As DataRow
' ''            drow = temptable.NewRow
' ''            'For k As Integer = 0 To i - 1
' ''            tdate = Format(DateAdd(DateInterval.Day, 0, dttoday.Value), "dd/MMM/yyyy")
' ''            If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
' ''                drow(tdate) = True
' ''            End If
' ''            Try
' ''                tdate = Format(DateAdd(DateInterval.Day, (i - 1), dttoday.Value), "dd/MMM/yyyy")
' ''                If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
' ''                    drow(tdate) = True
' ''                    drow("Expiry " & tdate) = True
' ''                End If
' ''            Catch ex As Exception
' ''                MsgBox("Beginning date can't be greater than expiry date.")
' ''                Exit Sub
' ''            End Try
' ''            Dim t As Integer
' ''            t = Math.Round((Val(grdact.ColumnCount - 1)) / 2, 0)
' ''            tdate = Format(DateAdd(DateInterval.Day, t, dttoday.Value), "dd/MMM/yyyy")
' ''            If UCase(WeekdayName(Weekday(tdate))) <> "SUNDAY" And UCase(WeekdayName(Weekday(tdate))) <> "SATURDAY" Then
' ''                drow(tdate) = True
' ''            End If
' ''            ' Next
' ''            temptable.Rows.Add(drow)
' ''            grdact.DataSource = temptable
' ''            'grdact.Refresh()
' ''        End If

' ''        'assign expiry date to selected expiry date
' ''        If frmFlg = True Then 'this flg used bcoz first need not to assign expiry date
' ''            For Each grow As DataGridViewRow In grdtrad.Rows
' ''                If grow.Cells("TimeII").ReadOnly = False Then
' ''                    grow.Cells("TimeII").Value = dtexp.Value
' ''                End If
' ''            Next
' ''        End If
' ''        frmFlg = True
' ''        'If grdtrad.Rows(grdtrad.NewRowIndex).Cells("TimeII").ReadOnly = False Then
' ''        '    grdtrad.Rows(grdtrad.NewRowIndex).Cells("TimeII").Value = dtexp.Value
' ''        'End If

' ''        If grdtrad.Rows.Count > 1 Then
' ''            grdtrad.Rows(grdtrad.Rows.Count - 1).Cells(4).Value = grdtrad.Rows(grdtrad.Rows.Count - 2).Cells(4).Value
' ''        End If
' ''    End Sub
' ''#End Region
' ''    Private Sub chkint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        If chkint.Checked = True Then
' ''            txtinterval.Text = 2
' ''            Label2.Visible = True
' ''        Else
' ''            Label2.Visible = False
' ''            txtinterval.Text = 100
' ''        End If
' ''    End Sub
' ''    Private Function cal_profitLoss() As Double
' ''        Dim profit As Double = 0
' ''        For Each grow As DataGridViewRow In grdtrad.Rows
' ''            If grow.Cells("Active").Value = False Then Continue For
' ''            If grow.Cells("Active").Value = True Then profit += (grow.Cells("units").Value) * (grow.Cells("last").Value - grow.Cells("ltp").Value)
' ''        Next
' ''        profit += grossmtm
' ''        Return profit
' ''    End Function
' ''    Private Function cal_FutprofitLoss() As Double
' ''        Dim profit As Double = 0
' ''        For Each grow As DataGridViewRow In grdtrad.Rows
' ''            If grow.Cells("Active").Value = False Then Continue For
' ''            If grow.Cells("Active").Value = True Then profit += (grow.Cells("units").Value) * (grow.Cells("last").Value - grow.Cells("ltp1").Value)
' ''        Next
' ''        profit += grossmtm
' ''        Return profit
' ''    End Function
' ''    Private Sub chtprofit_DblClick(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        chartName = "profit"
' ''        ValLBLprofit = lblprofit.Text
' ''        objProfitLossChart.ShowForm()
' ''    End Sub
' ''    Private Sub chttheta_DblClick(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        chartName = "theta"
' ''        ValLBLtheta = lbltheta.Text
' ''        objProfitLossChart.ShowForm()
' ''    End Sub

' ''    Private Sub chtvega_DblClick(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        chartName = "vega"
' ''        ValLBLvega = lblvega.Text
' ''        objProfitLossChart.ShowForm()
' ''    End Sub

' ''    Private Sub chtdelta_DblClick(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        chartName = "delta"
' ''        ValLBLdelta = lbldelta.Text
' ''        objProfitLossChart.ShowForm()
' ''    End Sub

' ''    Private Sub chtgamma_DblClick(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        chartName = "gamma"
' ''        ValLBLgamma = lblgamma.Text
' ''        objProfitLossChart.ShowForm()
' ''    End Sub

' ''    Private Sub chtvolga_DblClick(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        chartName = "volga"
' ''        ValLBLvolga = lblvolga.Text
' ''        objProfitLossChart.ShowForm()
' ''    End Sub
' ''    Private Sub chtvanna_DblClick(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        chartName = "vanna"
' ''        ValLBLVanna = lblvanna.Text
' ''        objProfitLossChart.ShowForm()
' ''    End Sub


' ''    Private Sub Vol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Vol.Click
' ''        Try
' ''            If Vol.Text = "Freeze Vol Column" Then
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    grow.Cells("lv").ReadOnly = True
' ''                    Vol.Text = "Unfreeze Vol Column"
' ''                Next
' ''            Else
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    grow.Cells("lv").ReadOnly = False
' ''                    Vol.Text = "Freeze Vol Column"
' ''                Next
' ''            End If

' ''        Catch ex As Exception
' ''            MsgBox(ex.ToString)
' ''        End Try
' ''    End Sub

' ''    Private Sub grdtrad_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
' ''        If e.RowIndex <> -1 Then
' ''            cellno = e.RowIndex
' ''            'grdtrad.EndEdit()
' ''            'Call cal_summary()
' ''        End If
' ''    End Sub

' ''    Private Sub scenario_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
' ''        If Not XResolution = Screen.PrimaryScreen.Bounds.Width Then
' ''            XResolution = Screen.PrimaryScreen.Bounds.Width
' ''            Call setGridTrad()
' ''        End If
' ''    End Sub
' ''    Private Sub setGridTrad()
' ''        Dim intX As Integer
' ''        intX = Screen.PrimaryScreen.Bounds.Width
' ''        Dim diff As Double = intX - 1024

' ''        If diff = 0 Then
' ''            Exit Sub
' ''        ElseIf diff > 0 Then
' ''            diff = diff / 16
' ''            Dim i As Integer
' ''            For i = 0 To grdtrad.Columns.Count - 1
' ''                grdtrad.Columns(i).Width += diff + 2
' ''            Next
' ''        Else
' ''            diff = diff / 16
' ''            Dim i As Integer
' ''            For i = 0 To grdtrad.Columns.Count - 1
' ''                grdtrad.Columns(i).Width += diff
' ''            Next
' ''        End If
' ''    End Sub

' ''    Private Sub grdprofit_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

' ''    End Sub

' ''    Private Sub expiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles expiry.Click
' ''        Try
' ''            If expiry.Text = "Freeze Expiry Column" Then
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    grow.Cells("TimeII").ReadOnly = True
' ''                    expiry.Text = "Unfreeze Expiry Column"
' ''                Next
' ''            Else
' ''                For Each grow As DataGridViewRow In grdtrad.Rows
' ''                    grow.Cells("TimeII").ReadOnly = False
' ''                    expiry.Text = "Freeze Expiry Column"
' ''                Next
' ''            End If
' ''        Catch ex As Exception
' ''            MsgBox(ex.ToString)
' ''        End Try
' ''    End Sub

' ''    Private Sub btnincrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        involflag = True
' ''        Call IncDecVolCal()
' ''    End Sub
' ''    Private Sub btndecrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        decvolflag = True
' ''        Call IncDecVolCal()
' ''    End Sub
' ''    Private Sub IncDecVolCal() ''divyesh
' ''        If txtvol.Text.Trim = "" Then
' ''            txtvol.Text = 0
' ''        End If
' ''        If grdtrad.Rows.Count >= 0 Then
' ''            For Each grow As DataGridViewRow In grdtrad.Rows
' ''                If grow.Cells("CPF").Value = "C" Or grow.Cells("CPF").Value = "P" Then
' ''                    If grow.Cells("lv").ReadOnly = False Then
' ''                        If involflag = True Then
' ''                            grow.Cells("lv").Value = Format(grow.Cells("lv").Value + Val(txtvol.Text), "#0.00")
' ''                        ElseIf decvolflag = True Then
' ''                            If grow.Cells("lv").Value - Val(txtvol.Text) > 0 Then
' ''                                grow.Cells("lv").Value = Format(grow.Cells("lv").Value - Val(txtvol.Text), "#0.00")
' ''                            End If
' ''                        End If
' ''                    End If
' ''                    Dim mt As Double
' ''                    Dim mmt As Double
' ''                    Dim futval As Double

' ''                    mt = DateDiff(DateInterval.Day, CDate(grow.Cells("TimeI").Value).Date, CDate(grow.Cells("TimeII").Value).Date)
' ''                    mmt = (DateDiff(DateInterval.Day, CDate(dttoday.Value.Date), CDate(grow.Cells("TimeII").Value).Date)) - 1
' ''                    REM For Rate Not To Be Zero  Alpesh & Udipth Sir
' ''                    REM Change Mt=0.5 Because Everywhere Mt=0.5 
' ''                    If mt = 0 Then
' ''                        'mt = 0.05
' ''                        'mmt = 0
' ''                        mt = 0.5
' ''                        mmt = 0
' ''                    End If
' ''                    If mmt = 0 Then
' ''                        'mmt = 0.05
' ''                        mmt = 0.5
' ''                    End If
' ''                    futval = Val(grow.Cells("spval").Value)
' ''                    Dim VarIsCall As Boolean = False
' ''                    If grow.Cells("CPF").Value = "C" Then
' ''                        VarIsCall = True
' ''                    End If
' ''                    CalData(futval, Val(grow.Cells("Strike").Value), Val(grow.Cells("last").Value), mt, mmt, VarIsCall, True, grow, Val(grow.Cells("units").Value), (Val(grow.Cells("lv").Value) / 100))
' ''                    cal_summary()
' ''                End If
' ''            Next
' ''            involflag = False
' ''            decvolflag = False
' ''            'result(False)
' ''        End If

' ''    End Sub
' ''    Private Sub txtvol_Press(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
' ''        numonly(e)
' ''    End Sub

' ''    Private Sub Label21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

' ''    End Sub

' ''    Private Sub grdtrad_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
' ''        'If (grdtrad.Columns(e.ColumnIndex).Name = "TimeII") Then
' ''        '    If (IsDate(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False) Then
' ''        '        'grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtexp.Value
' ''        '        grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
' ''        '        MsgBox("Please Enter Proper Date...")
' ''        '        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dttoday.Value
' ''        '    End If
' ''        'End If
' ''        'If (grdtrad.Columns(e.ColumnIndex).Name = "TimeII") Then
' ''        '    If (IsDate(grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) = False) Then
' ''        '        grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = dtexp.Value
' ''        '        grdtrad.Rows(e.RowIndex).Cells(e.ColumnIndex).Selected = True
' ''        '        MsgBox("Please Enter Proper Date...")
' ''        '        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dttoday.Value
' ''        '    Else
' ''        '        grdtrad.Rows(e.RowIndex).Cells("TimeII").Value = dttoday.Value
' ''        '    End If
' ''        'End If

' ''    End Sub


' ''    Private Sub tbcon_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        Select Case UCase(tbcon.SelectedTab.Name)
' ''            Case "TBPROFITCHART"
' ''                SetResultButtonText(cmdresult, chtprofit, grdprofit, lblprofit, False)
' ''            Case "TBCHDELTA"
' ''                SetResultButtonText(cmdresult, chtdelta, grddelta, lbldelta, False)
' ''            Case "TBCHGAMMA"
' ''                SetResultButtonText(cmdresult, chtgamma, grdgamma, lblgamma, False)
' ''            Case "TBCHVEGA"
' ''                SetResultButtonText(cmdresult, chtvega, grdvega, lblvega, False)
' ''            Case "TBCHTHETA"
' ''                SetResultButtonText(cmdresult, chttheta, grdtheta, lbltheta, False)
' ''            Case "TBCHVOLGA"
' ''                SetResultButtonText(cmdresult, chtvolga, grdvolga, lblvolga, False)
' ''            Case "TBCHVANNA"
' ''                SetResultButtonText(cmdresult, chtvanna, grdvanna, lblvanna, False)
' ''        End Select
' ''    End Sub
' ''    'Private Sub SetResultButtonTextOnTabChange(ByVal Cht As AxMSChart20Lib.AxMSChart)
' ''    '    If Cht.Visible = True Then
' ''    '        cmdresult.Text = "G"
' ''    '    Else
' ''    '        cmdresult.Text = "C"
' ''    '    End If
' ''    'End Sub
' ''    Private Sub SetResultButtonText(ByVal Cmd As Button, ByVal cht As AxMSChart20Lib.AxMSChart, ByVal grd As DataGridView, ByVal Lbl As System.Windows.Forms.Label, ByVal SetVisiblity As Boolean)
' ''        If cht.Visible = True Then
' ''            If SetVisiblity = True Then
' ''                Cmd.Text = "&Chart(F1)"
' ''                cht.Visible = False
' ''                Lbl.Visible = False
' ''                grd.Visible = True
' ''            Else
' ''                Cmd.Text = "&Value(F1)"
' ''            End If
' ''        Else
' ''            If SetVisiblity = True Then
' ''                Cmd.Text = "&Value(F1)"
' ''                cht.Visible = True
' ''                Lbl.Visible = True
' ''                grd.Visible = False
' ''            Else
' ''                Cmd.Text = "&Chart(F1)"
' ''            End If
' ''        End If
' ''    End Sub
' ''    Private Sub CmdAllCV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        REM In Scenario window For All Greeks Chart and GridData on One tab,According to Button press it shows chart or Griddata
' ''        Dim mSelIndex As Integer
' ''        mAllCV = ""
' ''        result(True)

' ''        'If CmdAllCV.Text = "Show &All Charts(F8)" Then
' ''        '    CmdAllCV.Text = "Show &All Values(F8)"
' ''        '    mAllCV = "Charts"
' ''        '    'chtprofit.Visible = True
' ''        '    'chtdelta.Visible = True
' ''        '    'chtgamma.Visible = True
' ''        '    'chtvega.Visible = True
' ''        '    'chttheta.Visible = True
' ''        '    'chtvolga.Visible = True
' ''        '    'chtvanna.Visible = True
' ''        'Else
' ''        '    CmdAllCV.Text = "Show &All Charts(F8)"
' ''        '    mAllCV = "Values"
' ''        '    'chtprofit.Visible = True
' ''        '    'chtdelta.Visible = True
' ''        '    'chtgamma.Visible = True
' ''        '    'chtvega.Visible = True
' ''        '    'chttheta.Visible = True
' ''        '    'chtvolga.Visible = True
' ''        '    'chtvanna.Visible = True
' ''        'End If

' ''        If mAllCV.Trim <> "" Then
' ''            mSelIndex = tbcon.SelectedIndex
' ''            For SelIndex As Int16 = 0 To tbcon.TabPages.Count
' ''                tbcon.SelectedIndex = SelIndex
' ''                Select Case UCase(tbcon.SelectedTab.Name)
' ''                    Case "TBPROFITCHART"
' ''                        If mAllCV = "Charts" Then
' ''                            SetChartGridForAll(chtprofit, grdprofit, lblprofit, True)
' ''                        Else
' ''                            SetChartGridForAll(chtprofit, grdprofit, lblprofit, False)
' ''                        End If
' ''                    Case "TBCHDELTA"
' ''                        If mAllCV = "Charts" Then
' ''                            SetChartGridForAll(chtdelta, grddelta, lbldelta, True)
' ''                        Else
' ''                            SetChartGridForAll(chtdelta, grddelta, lbldelta, False)
' ''                        End If
' ''                    Case "TBCHGAMMA"
' ''                        If mAllCV = "Charts" Then
' ''                            SetChartGridForAll(chtgamma, grdgamma, lblgamma, True)
' ''                        Else
' ''                            SetChartGridForAll(chtgamma, grdgamma, lblgamma, False)
' ''                        End If
' ''                    Case "TBCHVEGA"
' ''                        If mAllCV = "Charts" Then
' ''                            SetChartGridForAll(chtvega, grdvega, lblvega, True)
' ''                        Else
' ''                            SetChartGridForAll(chtvega, grdvega, lblvega, False)
' ''                        End If
' ''                    Case "TBCHTHETA"
' ''                        If mAllCV = "Charts" Then
' ''                            SetChartGridForAll(chttheta, grdtheta, lbltheta, True)
' ''                        Else
' ''                            SetChartGridForAll(chttheta, grdtheta, lbltheta, False)
' ''                        End If
' ''                    Case "TBCHVOLGA"
' ''                        If mAllCV = "Charts" Then
' ''                            SetChartGridForAll(chtvolga, grdvolga, lblvolga, True)
' ''                        Else
' ''                            SetChartGridForAll(chtvolga, grdvolga, lblvolga, False)
' ''                        End If
' ''                    Case "TBCHVANNA"
' ''                        If mAllCV = "Charts" Then
' ''                            SetChartGridForAll(chtvanna, grdvanna, lblvanna, True)
' ''                        Else
' ''                            SetChartGridForAll(chtvanna, grdvanna, lblvanna, False)
' ''                        End If
' ''                End Select
' ''            Next
' ''            tbcon.SelectedIndex = mSelIndex
' ''        End If
' ''        'SetResultButtonText(cmdresult, chtprofit, grdprofit, True, True)
' ''        'SetResultButtonText(cmdresult, chtdelta, grddelta, True, True)
' ''        'SetResultButtonText(cmdresult, chtgamma, grdgamma, True, True)
' ''        'SetResultButtonText(cmdresult, chtvega, grdvega, True, True)
' ''        'SetResultButtonText(cmdresult, chttheta, grdtheta, True, True)
' ''        'SetResultButtonText(cmdresult, chtvolga, grdvolga, True, True)
' ''        'SetResultButtonText(cmdresult, chtvanna, grdvanna, True, True)

' ''        ''Select Case tbcon.SelectedTab.Name
' ''        ''    Case ""
' ''        ''    Case Else
' ''        ''        SetResultButtonText(cmdresult, chtprofit, grdprofit, True)
' ''        ''        SetResultButtonText(cmdresult, chtdelta, grddelta, True)
' ''        ''        SetResultButtonText(cmdresult, chtgamma, grdgamma, True)
' ''        ''        SetResultButtonText(cmdresult, chtvega, grdvega, True)
' ''        ''        SetResultButtonText(cmdresult, chttheta, grdtheta, True)
' ''        ''        SetResultButtonText(cmdresult, chtvolga, grdvolga, True)
' ''        ''        SetResultButtonText(cmdresult, chtvanna, grdvanna, True)
' ''        ''End Select

' ''    End Sub
' ''    Private Sub SetChartGridForAll(ByVal cht As AxMSChart20Lib.AxMSChart, ByVal grd As DataGridView, ByVal lbl As System.Windows.Forms.Label, ByVal Flg As Boolean)
' ''        cht.Visible = Flg
' ''        lbl.Visible = Flg
' ''        grd.Visible = Not Flg
' ''    End Sub
' ''    REM On change of cmp , if interval is 0, it calculate interval
' ''    Private Sub CalInterval()
' ''        If Val(txtmkt.Text) > 0 Then
' ''            txtmid = Val(txtmkt.Text)
' ''            If Val(txtmid) < 100 Then
' ''                txtinterval.Text = 1
' ''            ElseIf Val(txtmid) > 100 And Val(txtmid) < 500 Then
' ''                txtinterval.Text = 5
' ''            ElseIf Val(txtmid) > 100 And Val(txtmid) < 1000 Then
' ''                txtinterval.Text = 10
' ''            ElseIf Val(txtmid) > 1000 And Val(txtmid) < 10000 Then
' ''                txtinterval.Text = 100
' ''            ElseIf Val(txtmid) > 10000 Then
' ''                txtinterval.Text = 500
' ''            End If
' ''            interval = txtinterval.Text
' ''        End If
' ''    End Sub

' ''    Private Sub txtllimit_Leave(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        REM When scenario calculation passes value as negative in ltp, then it gives an overflow in values,For this Set Calulation Of Interval And No Of Strikes, multiplication of interval and no Of Strike must be less than CMP
' ''        If Val(txtmkt.Text) > 0 And Val(txtinterval.Text) > 0 And Val(txtllimit.Text) > 0 Then
' ''            If Val(txtllimit.Text) > Math.Round(Val(txtmkt.Text) / Val(txtinterval.Text)) Then
' ''                MsgBox("No. of Strike +/- Must Be Less Than " & Math.Round(Val(txtmkt.Text) / Val(txtinterval.Text), 0), MsgBoxStyle.Information)
' ''                txtllimit.Focus()
' ''                Exit Sub
' ''            End If
' ''        End If
' ''    End Sub
' ''    REM Whenever any line is unchecked delta of those option must be removed from that line as well
' ''    Private Sub grdtrad_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs)
' ''        If grdtrad.IsCurrentCellDirty Then
' ''            grdtrad.CommitEdit(DataGridViewDataErrorContexts.Commit)
' ''        End If
' ''    End Sub
' ''    Private Sub ButResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
' ''        REM  For Change In Grid Data Or Volatality Increment Click On Result Button  Show Charts
' ''        result(False)
' ''    End Sub

' ''    Private Sub chtprofit_ChartSelected(ByVal sender As System.Object, ByVal e As AxMSChart20Lib._DMSChartEvents_ChartSelectedEvent)

' ''    End Sub

' ''    Private Sub grdtrad_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

' ''    End Sub

' ''    Private Sub grdtrad_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

' ''    End Sub
' ''End Class
