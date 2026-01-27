Imports System.Data.OleDb
Imports System.Text
Imports System.Threading
Imports VolHedge.DAL
Imports VolHedge.OptionG

Public Class AtmvolWatch
    Dim limit As Double = 1

    Dim strCompany As String
    Dim Call_vol As Double

    Dim Put_vol As Double

    Dim Average_vol As Double
    Dim strInstrument As String
    Dim dtcpfmaster As DataTable

    Dim dtvol As DataTable = New DataTable
    Dim DtFMonthDate As DataTable
    Dim dtcount As DataTable
    Dim dtMarketTable1 As New DataTable
    Dim dtMarketTable2 As New DataTable
    Dim dtMarketTable3 As New DataTable
    Dim dtTemp As DataTable
    Dim DtUTokens1, DtUTokens2, DtUTokens3 As DataTable
    Dim DtDTokens1, DtDTokens2, DtDTokens3 As DataTable
    Public tmpCpfmaster_new As New DataTable
    Dim cpfmaster_new As New DataTable



    Dim fltpprsynfut As Double
    Dim ED As New clsEnDe
    Dim dgap As Double = 0
    Dim fltppr As Double = 0
    Dim synfutpr As Double = 0
    Dim CEltppr As Double = 0
    Dim PEltppr As Double = 0
    Dim Fltp1 As Double = 0
    Dim Fltp2 As Double = 0
    Dim Fltp3 As Double = 0

    Dim dMidStrike As Double
    Dim dblFutPrice1, dblFutPrice2, dblFutPrice3 As Double
    Dim MidStrike1 As Double = 0
    Dim MidStrike2 As Double = 0
    Dim MidStrike3 As Double = 0

    Dim token As Long
    Dim token1 As Long
    Dim strTokens As String = ""
    Public ArrTokenSeries As New ArrayList
    Dim objTrad As New trading
    Dim ObjTrading As New trading
    Dim flg_fillUpdateHeader As Boolean = True

    Dim sp_insert_vol As String = "insert_atmvol"
    Dim sp_select_vol As String = "select_atmvol"
    Dim sp_delete_vol As String = "delete_atmvol"
    Dim sp_delete_symbol_vol As String = "delete_symbol_atmvol"

    Dim trd As New trading
    Dim flg_calcSynFut As Boolean = False
    Dim Flg_refreshdata As Boolean = False
    Private Sub AtmvolWatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Timer1.Enabled = False
        grvvolwatch.ReadOnly = True
        cpfmaster_new = cpfmaster.Copy
        FillCboCompany()

        InitVolWatchTable()
        'fillcmbdate()
        select_data()
        'Timer1.Enabled = True
        'Refresh_tmpCpfmasterData()

        'Flg_CheckedMonth = False
        ''Me.TopMost = False

    End Sub


    Private Sub FillCboCompany()
        'Dim Dtcname As New DataTable()
        Dim column As String() = {"symbol"}
        'Dtcname = cpfmaster.DefaultView.ToTable("dd", False, column)
        Dim dtcpfmaster As New DataTable
        dtcpfmaster = cpfmaster.Clone
        dtcpfmaster.AcceptChanges()
        Dim drcpf() As DataRow
        drcpf = cpfmaster.Select("option_type in ('CA','CE','PA','PE')", "symbol")
        For Each dataRow As DataRow In drcpf
            dtcpfmaster.ImportRow(dataRow)
        Next

        Dim Dtcname As New DataTable
        'Dtcname = New DataView(cpfmaster, "option_type in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows).ToTable("TRUE", column)
        Dtcname = New DataView(dtcpfmaster, "", "", DataViewRowState.CurrentRows).ToTable("TRUE", column)
        'Dtcname = dv.ToTable(True, "symbol")
        Dim dtCurrmaster As New DataTable
        dtCurrmaster = dtCurrmaster.Clone
        Dim drCurr() As DataRow
        Dim Dtcnamecurr As New DataTable
        drCurr = Currencymaster.Select("InstrumentName = 'FUTCUR'", "symbol")
        For Each dataRow1 As DataRow In drCurr
            dtCurrmaster.ImportRow(dataRow1)
        Next
        ' dv = New DataView(Currencymaster, "InstrumentName = 'FUTCUR'", "symbol", DataViewRowState.CurrentRows)
        'Dtcnamecurr = New DataView(Currencymaster, "InstrumentName = 'FUTCUR'", "symbol", DataViewRowState.CurrentRows).ToTable("TRUE", column)
        Dtcnamecurr = New DataView(Currencymaster, "", "", DataViewRowState.CurrentRows).ToTable("TRUE", column)
        Dtcname.Merge(Dtcnamecurr)
        For Each dr1 As DataRow In Dtcname.Rows
            Dim str_dealer_Name As String = dr1("symbol").ToString.Trim
            If str_dealer_Name = "" Then
                str_dealer_Name = dr1("symbol").ToString.Trim
            End If
            cmbCompany.Items.Add(str_dealer_Name)
        Next


        cmbCompany.Refresh()

        Dim dt As New DataTable
        dt = cpfmaster.Clone
        dt.AcceptChanges()
        Dim dr() As DataRow
        dr = cpfmaster.Select("Symbol='" & cmbCompany.Text & "' AND  InstrumentName IN ('FUTSTK','FUTIDX','FUTIVX')", "expdate1")
        For Each dataRow As DataRow In dr
            dt.ImportRow(dataRow)
        Next


        cmbCompany.Refresh()
    End Sub
    Private Sub fillcmbdate(ByRef symbol As String)
        cmbexpiry.DataSource = Nothing
        If (symbol = "NIFTY" Or symbol = "BANKNIFTY" Or symbol = "FINNIFTY" Or symbol = "MIDCPNIFTY") Then


            Dim dv As DataView = New DataView(dtcpfmaster, "symbol='" & symbol & "'", "expdate1", DataViewRowState.CurrentRows)
            'Dim dv As DataView = New DataView(dtcpfmaster, "symbol='" & symbol & "' and option_type='XX' AND expdate1 >='" & Now.Date & "'   ", "strike_price", DataViewRowState.CurrentRows)
            cmbexpiry.DataSource = dv.ToTable(True, "expdate")
            cmbexpiry.DisplayMember = "expdate"
            cmbexpiry.ValueMember = "expdate"
        Else

            Dim dv As DataView = New DataView(dtcpfmaster, "symbol='" & symbol & "' and InstrumentName='FUTSTK' and option_type='XX' AND expdate1 >='" & Now.Date & "'   ", "strike_price", DataViewRowState.CurrentRows)
            cmbexpiry.DataSource = dv.ToTable(True, "expdate")
            cmbexpiry.DisplayMember = "expdate"
            cmbexpiry.ValueMember = "expdate"
        End If

    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Try
            Dim thrcalvcol As Threading.Thread
            thrcalvcol = New Thread(AddressOf CAL_VOL_DATA)
            thrcalvcol.Name = "thrcalvcolatm"
            thrcalvcol.Start()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try

            If dtvol.Rows.Count < 20 Then


                Dim strcomp As String = cmbCompany.Text
                strCompany = strcomp

                Refresh_tmpCpfmasterData(strCompany)
                For Each dr In dtvol.Rows
                    If (dr("Script_Name") = strCompany) Then
                        dtvol.Rows.Remove(dr)
                        dtvol.AcceptChanges()
                        Exit For
                    End If
                Next


                Dim mT As Double
                If strCompany.Trim = "" Then Return



                If RBsynfut.Checked = True Then
                    If (strCompany = "NIFTY" Or strCompany = "BANKNIFTY") Then

                        Dim dv As DataView
                        dv = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  expdate1='" & cmbexpiry.Text & "' and Option_Type = 'CE'", "strike_price", DataViewRowState.CurrentRows)
                        dtTemp = dv.ToTable()

                    End If
                Else

                    Dim dv As DataView
                    dv = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  expdate1='" & cmbexpiry.Text & "' and Option_Type = 'CE'", "strike_price", DataViewRowState.CurrentRows)
                    dtTemp = dv.ToTable()

                End If



                For Each drow As DataRow In dtTemp.Rows

                    regtoken(drow("ftoken"))
                    If Not fltpprice.Contains(CLng(drow("ftoken"))) Then
                        Dim dt As String = getrate(Convert.ToInt64(drow("ftoken")))
                        While (dt = Nothing)

                            dt = getrate(Convert.ToInt64(drow("ftoken")))

                        End While

                        fltppr = Math.Round(ED.DFo(Val(dt.ToString())), 2)
                    Else

                        fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                    End If




                    mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(drow("expdate")).Date)
                    If (fltppr <> 0) Then
                        Exit For
                    End If


                Next
                If RBsynfut.Checked = True And (strCompany = "NIFTY" Or strCompany = "BANKNIFTY") Then


                    synfutpr = get_SFut(strCompany, CDate(cmbexpiry.Text), Convert.ToDouble(fltppr))

                    dgap = dtTemp.Rows(1)("strike_Price") - dtTemp.Rows(0)("strike_Price")
                    For i As Integer = 0 To dtTemp.Rows.Count
                        If (synfutpr < dtTemp.Rows(i)("Strike_Price")) Then
                            If dgap < (dtTemp.Rows(i)("Strike_Price") - synfutpr) Then
                                dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                Exit For
                            Else
                                'i = i - 1
                                If (dgap / 2) > (dtTemp.Rows(i)("Strike_Price") - synfutpr) Then
                                    dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                    Exit For
                                Else
                                    i = i - 1
                                    dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                Else
                    dgap = dtTemp.Rows(1)("strike_Price") - dtTemp.Rows(0)("strike_Price")
                    For i As Integer = 0 To dtTemp.Rows.Count
                        If (fltppr < dtTemp.Rows(i)("Strike_Price")) Then
                            If dgap < (dtTemp.Rows(i)("Strike_Price") - fltppr) Then
                                dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                Exit For
                            Else
                                'i = i - 1
                                If (dgap / 2) > (dtTemp.Rows(i)("Strike_Price") - fltppr) Then
                                    dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                    Exit For
                                Else
                                    i = i - 1
                                    dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                    Exit For
                                End If
                            End If
                        End If
                    Next



                End If


                Dim script As String
                script = "OPTSTK "

                Dim dv1 As DataView
                dv1 = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  expdate1='" & cmbexpiry.Text & "' and Option_Type = 'CE'  and strike_price='" + dMidStrike.ToString() + "'", "strike_price", DataViewRowState.CurrentRows)
                Dim dtTemp1 As DataTable = dv1.ToTable()

                For Each dr In dtTemp1.Rows
                    regtoken(dr("token"))
                    If Not ltpprice.Contains(CLng(dr("token"))) Then
                        Dim dt As String = getrate(Convert.ToInt64(dr("token")))
                        While (dt = Nothing)

                            dt = getrate(Convert.ToInt64(dr("token")))

                        End While
                        CEltppr = Math.Round(ED.DFo(Val(dt.ToString())), 2)

                    Else

                        CEltppr = Val(ltpprice(CLng(dr("token"))))
                    End If

                Next

                'call vol calculate-----
                Dim mVolatility_call, mVolatility_put, futval, stkprice, Mrateofinterast, mIsCall, mIsFut As Double
                Dim mVolPrev_Call As Double
                If RBsynfut.Checked = True And (strCompany = "NIFTY" Or strCompany = "BANKNIFTY") Then
                    futval = synfutpr
                Else

                    futval = fltppr
                End If
                stkprice = dMidStrike
                Mrateofinterast = 0
                mIsCall = True
                mIsFut = False

                Dim _mt As Double
                If mT = 0 Then
                    _mt = 0.00001

                Else
                    _mt = (mT) / 365

                End If

                Dim dPremium As Double = CEltppr
                mVolatility_call = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, dPremium, _mt, True, False, 0, 6)

                Call_vol = Math.Round(mVolatility_call * 100, RoundVol)


                'put vol value---
                Dim dv2 As DataView
                dv2 = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  expdate1='" & cmbexpiry.Text & "' and Option_Type = 'PE'  and strike_price='" + dMidStrike.ToString() + "'", "strike_price", DataViewRowState.CurrentRows)
                Dim dtTemp3 As DataTable = dv2.ToTable()

                For Each dr In dtTemp3.Rows
                    regtoken(dr("token"))
                    If Not ltpprice.Contains(CLng(dr("token"))) Then
                        Dim dt As String = getrate(Convert.ToInt64(dr("token")))
                        While (dt = Nothing)

                            dt = getrate(Convert.ToInt64(dr("token")))

                        End While
                        PEltppr = Math.Round(ED.DFo(Val(dt.ToString())), 2)

                    Else

                        PEltppr = Val(ltpprice(CLng(dr("token"))))
                    End If
                Next
                dPremium = PEltppr
                mVolatility_put = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, dPremium, _mt, True, False, 0, 6)


                Put_vol = Math.Round(mVolatility_put * 100, RoundVol)
                Dim vol_alert As Double = 0
                If txtalert.Text <> "" Then
                    vol_alert = Convert.ToDouble(txtalert.Text)
                End If

                Dim drvoldata As DataRow
                drvoldata = dtvol.NewRow

                drvoldata("Script_Name") = strcomp
                drvoldata("Expirydate") = cmbexpiry.Text
                drvoldata("ATM_STRIKE") = dMidStrike
                drvoldata("FUT_LTP") = futval
                drvoldata("CE_LTP") = CEltppr
                drvoldata("PE_LTP") = PEltppr
                drvoldata("CE_VOL") = Call_vol
                drvoldata("PE_VOL") = Put_vol
                drvoldata("AVE_VOL") = Math.Round((Call_vol + Put_vol) / 2, 2)

                drvoldata("Condition") = cmbcondition.Text
                drvoldata("ALARM") = vol_alert
                drvoldata("Alert_flag") = 0

                dtvol.Rows.Add(drvoldata)
                dtvol.AcceptChanges()



                grvvolwatch.DataSource = dtvol

                Flg_refreshdata = False



                'Timer1.Enabled = True

            Else
                MsgBox("You can't enter symbol more then 20.")
            End If


        Catch ex As Exception

        End Try



    End Sub

    Private Sub calcSynFut(ByVal strCompany As String)

        Try


            Dim vv As DataView = New DataView(dtcpfmaster, "symbol='" & strCompany & "'", "expdate1", DataViewRowState.CurrentRows)
            Dim dtcount As DataTable = vv.ToTable(True, "expdate1", "symbol", "ftoken")
            Dim i As Integer = 0
            For Each drow As DataRow In dtcount.Rows
                Try
                    If i > 6 Then
                        Exit For
                    End If
                    i = i + 1
                    If hashsyn.Contains(drow("expdate1") & "_" & strCompany) = False Then
                        hashsyn.Add(drow("expdate1") & "_" & strCompany, "0")
                    End If
                    Dim fltpprsynfut As Double


                    fltpprsynfut = Val(fltpprice(CLng(drow("ftoken"))))

                    If hashsyn.Contains(drow("expdate1") & "_" & drow("symbol")) Then
                        hashsyn(drow("expdate1") & "_" & drow("symbol")) = get_SFut(drow("symbol"), CDate(drow("expdate1")), Convert.ToDouble(fltpprsynfut))

                    End If

                Catch ex As Exception
                    hashsyn(drow("expdate1") & "_" & drow("symbol")) = 0
                End Try






            Next
            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbCompany_Leave(sender As Object, e As EventArgs) Handles cmbCompany.Leave
        Try

            Refresh_tmpCpfmasterData(cmbCompany.Text)
            fillcmbdate(cmbCompany.Text)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmbCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCompany.SelectedIndexChanged
        Try
            Timer1.Enabled = False
            Refresh_tmpCpfmasterData(cmbCompany.Text)
            fillcmbdate(cmbCompany.Text)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub RBsynfut_CheckedChanged(sender As Object, e As EventArgs) Handles RBsynfut.CheckedChanged
        If RBsynfut.Checked Then

        Else

        End If
    End Sub

    Private Sub RBsynfut1_CheckedChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub SAVE_Click(sender As Object, e As EventArgs) Handles SAVE.Click
        delete_voldata()

        insert_voldata(dtvol)


        MsgBox("Saved successfully..")


    End Sub
    Public Sub delete_voldata()
        Try
            data_access.ParamClear()

            data_access.Cmd_Text = sp_delete_vol
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.ExecuteNonQuery()
        Catch ex As Exception

        End Try



    End Sub
    Public Sub select_data()
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = sp_select_vol
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.FillList(dtvol)
        Catch ex As Exception

        End Try
        grvvolwatch.DataSource = dtvol
        'Timer1.Enabled = True

    End Sub
    Public Sub insert_voldata(ByVal dtable As DataTable)
        Try
            For Each drow As DataRow In dtable.Rows

                data_access.ParamClear()

                data_access.AddParam("@Script_Name", OleDbType.VarChar, 50, CStr(drow("Script_Name")))
                data_access.AddParam("@Expirydate", OleDbType.VarChar, 50, CStr(drow("Expirydate")))
                data_access.AddParam("@ATM_STRIKE", OleDbType.VarChar, 50, CStr(drow("ATM_STRIKE")))
                data_access.AddParam("@FUT_LTP", OleDbType.Double, 18, Val(drow("FUT_LTP")))
                data_access.AddParam("@CE_LTP", OleDbType.Double, 18, Val(drow("CE_LTP")))
                data_access.AddParam("@PE_LTP", OleDbType.Double, 18, Val(drow("PE_LTP")))
                data_access.AddParam("@CE_VOL", OleDbType.Double, 18, Val(drow("CE_VOL")))
                data_access.AddParam("@PE_VOL", OleDbType.Double, 18, Val(drow("PE_VOL")))
                data_access.AddParam("@AVE_VOL", OleDbType.Double, 18, Val(drow("AVE_VOL")))
                data_access.AddParam("@Condition", OleDbType.VarChar, 50, CStr(drow("Condition")))
                data_access.AddParam("@ALARM", OleDbType.Double, 18, Val(drow("ALARM")))
                data_access.AddParam("@Alert_flag", OleDbType.Double, 18, Val(drow("Alert_flag")))

                data_access.Cmd_Text = sp_insert_vol
                data_access.cmd_type = CommandType.StoredProcedure
                data_access.ExecuteNonQuery()

            Next

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            data_access.ParamClear()

            data_access.AddParam("@Script_name", OleDbType.VarChar, 50, CStr(cmbCompany.Text))
            data_access.Cmd_Text = sp_delete_symbol_vol
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.ExecuteNonQuery()

            select_data()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Timer1.Enabled = True
        Flg_refreshdata = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Timer1.Enabled = False
    End Sub

    Private Sub btnrefresh_Click(sender As Object, e As EventArgs) Handles btnrefresh.Click
        CAL_VOL_DATA()
    End Sub

    Private Sub AtmvolWatch_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        delete_voldata()
        insert_voldata(dtvol)
        Timer1.Enabled = False
        'MsgBox("Do you want to save ?", MsgBoxStyle.YesNoCancel + MsgBoxStyle.Question)
        'If MsgBoxResult.Yes Then
        '    insert_voldata(dtvol)
        'End If
    End Sub

    Private Sub grvvolwatch_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles grvvolwatch.DataError

    End Sub

    Private Sub AtmvolWatch_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.H Then
            ' Hide the form
            Me.Hide()

            'Me.Location = New Point(-Me.Width, -Me.Height)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim strresult As Integer = MsgBox("Do you want to delete data?", MsgBoxStyle.YesNo + MsgBoxStyle.Question)

        If strresult = DialogResult.Yes Then
            Try

                delete_voldata()
                MsgBox("Data deleted Successfully..")
            Catch ex As Exception
                MsgBox("Data not deleted ")

            End Try
        ElseIf strresult = DialogResult.No Then

        End If

        select_data()

    End Sub

    Private Sub grvvolwatch_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles grvvolwatch.CellContentClick

    End Sub

    Private Sub grvvolwatch_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles grvvolwatch.CellDoubleClick
        If e.RowIndex >= 0 And e.ColumnIndex >= 0 Then


            Dim selectedRow As DataGridViewRow = grvvolwatch.Rows(e.RowIndex)
            Dim sym As DataGridViewCell = selectedRow.Cells(0)
            Dim exp As DataGridViewCell = selectedRow.Cells(1)
            Dim condition As DataGridViewCell = selectedRow.Cells(9)
            Dim alert As DataGridViewCell = selectedRow.Cells(10)

            cmbCompany.Text = sym.Value.ToString
            cmbexpiry.Text = exp.Value.ToString
            cmbcondition.Text = condition.Value.ToString
            txtalert.Text = alert.Value.ToString


        End If
    End Sub


    'Private Sub chkIsWeekly_CheckedChanged(sender As Object, e As EventArgs)
    '    If chkIsWeekly.Checked Then
    '        RBsynfut.Visible = True
    '        RBsynfut.Checked = True

    '        'RBcalVol.Visible = True
    '        Timer1.Enabled = False
    '    Else
    '        RBsynfut.Visible = False
    '        'RBcalVol.Visible = False
    '        Timer1.Enabled = False
    '    End If
    '    If chkIsWeekly.Checked Then

    '        For Each strcomp As String In symbol_select.Items
    '            If (strcomp = "NIFTY" Or strcomp = "BANKNIFTY") And chkIsWeekly.Checked = True Then


    '                Refresh_tmpCpfmasterData(strcomp)
    '                Dim vv As DataView = New DataView(dtcpfmaster, "symbol='" & strcomp & "'", "expdate1", DataViewRowState.CurrentRows)

    '                cmbexpiry.DataSource = vv.ToTable(True, "expdate1")
    '                cmbexpiry.DisplayMember = "expdate1"
    '                cmbexpiry.ValueMember = "expdate1"

    '            ElseIf (strcomp = "NIFTY" Or strcomp = "BANKNIFTY") Then

    '                fillcmbdate(strcomp)
    '            End If

    '        Next

    '    Else

    '        For Each strcomp As String In symbol_select.Items

    '            fillcmbdate(strcomp)
    '        Next
    '    End If
    'End Sub

    Private Sub InitVolWatchTable()

        dtvol.Columns.Add("Script_Name", GetType(String))
        dtvol.Columns.Add("Expirydate", GetType(String))

        dtvol.Columns.Add("ATM_STRIKE", GetType(Double))
        dtvol.Columns.Add("FUT_LTP", GetType(Double))
        dtvol.Columns.Add("CE_LTP", GetType(Double))
        dtvol.Columns.Add("PE_LTP", GetType(Double))

        dtvol.Columns.Add("CE_VOL", GetType(Double))
        dtvol.Columns.Add("PE_VOL", GetType(Double))
        dtvol.Columns.Add("AVE_VOL", GetType(Double))
        dtvol.Columns.Add("Condition", GetType(String))
        dtvol.Columns.Add("ALARM", GetType(Double))
        dtvol.Columns.Add("Alert_flag", GetType(Double))

        dtvol.AcceptChanges()



    End Sub

    Public Function regtoken(ByVal tokenno As String) As DataTable
        Try
            If (ltpprice(CLng(tokenno)) Is Nothing Or ltpprice(CLng(tokenno)) = 0 Or (Not FoRegTokens.Contains(tokenno))) And (NetMode = "API" Or NetMode = "JL" Or NetMode = "TCP") Then
                Objsql.AppendFoTokens(CLng(tokenno).ToString)
            End If
        Catch ex As Exception

        End Try


    End Function
    Public Function getrate(ByVal token As Int64) As String
        Try
            Dim Query As String = "select f4 As Rate from tbl01004 With(Nolock) Where f1=" + token.ToString() + ""
            'DAL.DA_SQL.Cmd_Text = Query

            Return DAL.DA_SQL.ExecuteScalar_openposition(Query)

            'Return DAL.DA_SQL.FillList()
        Catch ex As Exception
            'MsgBox(ex.ToString)
            Return Nothing
        End Try

    End Function
    Public Function CAL_VOL_DATA() As String
        Try

            If Flg_refreshdata = False Then
                Flg_refreshdata = True

                For Each DR1 In dtvol.Rows
                    Dim dgap1 As Double = 0
                    Dim script_name As String
                    script_name = DR1("Script_Name")
                    Refresh_tmpCpfmasterData(script_name)


                    Dim mT As Double


                    Dim dvdate As DataView = New DataView(dtcpfmaster, "symbol='" & script_name & "' and InstrumentName='FUTSTK' and option_type='XX' AND expdate1 >='" & Now.Date & "'   ", "strike_price", DataViewRowState.CurrentRows)
                    Dim dtdate As DataTable = dvdate.ToTable(True, "expdate")
                    Dim j As Int16 = 1
                    Dim strexpdate As String

                    For Each dr In dtdate.Rows
                        If j < 2 Then

                            j = j + 1
                            strexpdate = dr("expdate")

                        End If

                    Next

                    If (script_name = "NIFTY" Or script_name = "BANKNIFTY") Then

                        Dim dv As DataView
                        dv = New DataView(dtcpfmaster, "Symbol='" & script_name & "' AND  expdate1='" & DR1("Expirydate") & "' and Option_Type = 'CE'", "strike_price", DataViewRowState.CurrentRows)
                        dtTemp = dv.ToTable()

                    Else
                        Dim dv As DataView
                        dv = New DataView(dtcpfmaster, "Symbol='" & script_name & "' AND  expdate1='" & DR1("Expirydate") & "' and Option_Type = 'CE'", "strike_price", DataViewRowState.CurrentRows)
                        dtTemp = dv.ToTable()

                    End If



                    For Each drow As DataRow In dtTemp.Rows

                        regtoken(drow("ftoken"))
                        If Not fltpprice.Contains(CLng(drow("ftoken"))) Then
                            Dim dt As String = getrate(Convert.ToInt64(drow("ftoken")))
                            While (dt = Nothing)

                                dt = getrate(Convert.ToInt64(drow("ftoken")))

                            End While

                            fltppr = Math.Round(ED.DFo(Val(dt.ToString())), 2)
                        Else

                            fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                        End If




                        mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(drow("expdate")).Date)
                        If (fltppr <> 0) Then
                            Exit For
                        End If


                    Next


                    If RBsynfut.Checked = True And (script_name = "NIFTY" Or script_name = "BANKNIFTY") Then


                        synfutpr = get_SFut(script_name, CDate(DR1("Expirydate")), Convert.ToDouble(fltppr))

                        dgap = dtTemp.Rows(1)("strike_Price") - dtTemp.Rows(0)("strike_Price")
                        For i As Integer = 0 To dtTemp.Rows.Count
                            If (synfutpr < dtTemp.Rows(i)("Strike_Price")) Then
                                If dgap < (dtTemp.Rows(i)("Strike_Price") - synfutpr) Then
                                    dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                    Exit For
                                Else
                                    'i = i - 1
                                    If (dgap / 2) > (dtTemp.Rows(i)("Strike_Price") - synfutpr) Then
                                        dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                        Exit For
                                    Else
                                        i = i - 1
                                        dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    Else
                        dgap = dtTemp.Rows(1)("strike_Price") - dtTemp.Rows(0)("strike_Price")
                        For i As Integer = 0 To dtTemp.Rows.Count
                            If (fltppr < dtTemp.Rows(i)("Strike_Price")) Then
                                If dgap < (dtTemp.Rows(i)("Strike_Price") - fltppr) Then
                                    dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                    Exit For
                                Else
                                    'i = i - 1
                                    If (dgap / 2) > (dtTemp.Rows(i)("Strike_Price") - fltppr) Then
                                        dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                        Exit For
                                    Else
                                        i = i - 1
                                        dMidStrike = dtTemp.Rows(i)("Strike_Price")
                                        Exit For
                                    End If
                                End If
                            End If
                        Next



                    End If

                    Dim script As String
                    script = "OPTSTK "

                    Dim dv1 As DataView
                    dv1 = New DataView(dtcpfmaster, "Symbol='" & script_name & "' AND  expdate1='" & DR1("Expirydate") & "' and Option_Type = 'CE'  and strike_price='" + dMidStrike.ToString() + "'", "strike_price", DataViewRowState.CurrentRows)
                    Dim dtTemp1 As DataTable = dv1.ToTable()

                    For Each DR In dtTemp1.Rows
                        regtoken(DR("token"))
                        If Not ltpprice.Contains(CLng(DR("token"))) Then
                            Dim dt As String = getrate(Convert.ToInt64(DR("token")))
                            While (dt = Nothing)

                                dt = getrate(Convert.ToInt64(DR("token")))

                            End While
                            CEltppr = Math.Round(ED.DFo(Val(dt.ToString())), 2)

                        Else

                            CEltppr = Val(ltpprice(CLng(DR("token"))))
                        End If

                    Next

                    'call vol calculate-----
                    Dim mVolatility_call, mVolatility_put, futval, stkprice, Mrateofinterast, mIsCall, mIsFut As Double
                    Dim mVolPrev_Call As Double
                    If RBsynfut.Checked = True And (script_name = "NIFTY" Or DR1("Script_Name") = "BANKNIFTY") Then
                        futval = synfutpr
                    Else

                        futval = fltppr
                    End If
                    stkprice = dMidStrike
                    Mrateofinterast = 0
                    mIsCall = True
                    mIsFut = False

                    Dim _mt As Double
                    If mT = 0 Then
                        _mt = 0.00001

                    Else
                        _mt = (mT) / 365

                    End If

                    Dim dPremium As Double = CEltppr
                    mVolatility_call = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, dPremium, _mt, True, False, 0, 6)

                    Call_vol = Math.Round(mVolatility_call * 100, RoundVol)


                    'put vol value---
                    Dim dv2 As DataView
                    dv2 = New DataView(dtcpfmaster, "Symbol='" & script_name & "' AND  expdate1='" & DR1("Expirydate") & "' and Option_Type = 'PE'  and strike_price='" + dMidStrike.ToString() + "'", "strike_price", DataViewRowState.CurrentRows)
                    Dim dtTemp3 As DataTable = dv2.ToTable()

                    For Each DR In dtTemp3.Rows
                        regtoken(DR("token"))
                        If Not ltpprice.Contains(CLng(DR("token"))) Then
                            Dim dt As String = getrate(Convert.ToInt64(DR("token")))
                            While (dt = Nothing)

                                dt = getrate(Convert.ToInt64(DR("token")))

                            End While
                            PEltppr = Math.Round(ED.DFo(Val(dt.ToString())), 2)

                        Else

                            PEltppr = Val(ltpprice(CLng(DR("token"))))
                        End If
                    Next

                    Dim dPremium1 As Double = PEltppr
                    mVolatility_put = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, dPremium1, _mt, False, False, 0, 6)

                    Put_vol = Math.Round(mVolatility_put * 100, RoundVol)

                    'vol alert checking....
                    Dim avg_vol As Double
                    Dim Alert_flag As Double
                    Dim vol_alert As Double = 0
                    avg_vol = Math.Round((Call_vol + Put_vol) / 2, 2)
                    If DR1("ALARM").ToString() <> "" Then
                        vol_alert = Convert.ToDouble(DR1("ALARM"))
                    End If
                    Alert_flag = Convert.ToDouble(DR1("Alert_flag"))

                    If DR1("Condition").ToString() <> "" And Alert_flag = 0 Then

                        If (DR1("Condition") = "<") Then
                            If avg_vol < vol_alert Then
                                MsgBox("ALERT! VOL of " + script_name + " is less then ALARM ")
                                Alert_flag = 1
                            End If
                        ElseIf (DR1("Condition") = ">") Then
                            If avg_vol > vol_alert Then
                                MsgBox("ALERT! VOL of " + script_name + " is greater then ALARM ")
                                Alert_flag = 1
                            End If
                        ElseIf (DR1("Condition") = "<=") Then
                            If avg_vol <= vol_alert Then
                                MsgBox("ALERT! VOL of " + script_name + " is less then ALARM ")
                                Alert_flag = 1
                            End If
                        ElseIf (DR1("Condition") = ">=") Then
                            If avg_vol >= vol_alert Then
                                MsgBox("ALERT! VOL of " + script_name + " is greater then ALARM ")
                                Alert_flag = 1
                            End If
                        ElseIf (DR1("Condition") = "=") Then
                            If avg_vol = vol_alert Then
                                MsgBox("ALERT! VOL of " + script_name + " is equal to ALARM ")
                                Alert_flag = 1
                            End If
                        End If

                    End If


                    DR1("ATM_STRIKE") = dMidStrike
                    DR1("FUT_LTP") = futval
                    DR1("CE_LTP") = CEltppr
                    DR1("PE_LTP") = PEltppr
                    DR1("CE_VOL") = Call_vol
                    DR1("PE_VOL") = Put_vol
                    DR1("AVE_VOL") = avg_vol
                    DR1("Condition") = DR1("Condition").ToString()
                    DR1("ALARM") = vol_alert
                    DR1("Alert_flag") = Alert_flag
                    ' dtvol.AcceptChanges()

                Next


                '   grvvolwatch.DataSource = dtvol
                Flg_refreshdata = False


            End If

        Catch ex As Exception

        End Try


    End Function
    Private Function Get_MidStrike(ByVal dltp As Double, ByVal strExpDate As String, ByVal strCompany As String, ByRef dGap As Double)
        ' dim DtTemp as New DataTable = New DataView(dtcpfmaster , "Symbol='" & cmbCompany.Text & "' AND  expdate1=" & DtFMonthDate.Rows(0)("expdate1").ToString & " AND option_type = 'CE'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
        Try

            Dim dv As DataView
            dv = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  expdate1='" & strExpDate & "' and Option_Type = 'CE'", "strike_price", DataViewRowState.CurrentRows)
            Dim dtTemp As DataTable = dv.ToTable()

            Dim dMidStrike As Double
            dGap = dtTemp.Rows(1)("strike_Price") - dtTemp.Rows(0)("strike_Price")
            For i As Integer = 0 To dtTemp.Rows.Count
                If (dltp < dtTemp.Rows(i)("Strike_Price")) Then
                    If dGap < (dtTemp.Rows(i)("Strike_Price") - dltp) Then
                        dMidStrike = dtTemp.Rows(i)("Strike_Price")
                        Exit For
                    Else
                        'i = i - 1
                        If (dGap / 2) > (dtTemp.Rows(i)("Strike_Price") - dltp) Then
                            dMidStrike = dtTemp.Rows(i)("Strike_Price")
                            Exit For
                        Else
                            i = i - 1
                            dMidStrike = dtTemp.Rows(i)("Strike_Price")
                            Exit For
                        End If
                    End If
                End If
            Next
            Return dMidStrike

        Catch ex As Exception

        End Try
    End Function
    Private Sub Refresh_tmpCpfmasterData(ByVal strsymbol As String)

        Try

            strcompmkt = strsymbol
            tmpCpfmaster = cpfmaster_new
            strInstrument = "'FUTSTK','FUTIDX','FUTIVX'"


            'tmpCpfmaster = cpfmaster
            If tmpCpfmaster.Columns.Contains("StrikeMod") = False Then
                tmpCpfmaster.Columns.Add("StrikeMod", GetType(Double))
            End If
            tmpCpfmaster.Columns("StrikeMod").DefaultValue = 0



            If strsymbol = "NIFTY" Or strsymbol = "BANKNIFTY" Then




                For Each drr As DataRow In tmpCpfmaster.Rows
                    If drr("InstrumentName").ToString() = "FUTIDX" Then
                        drr("StrikeMod") = 0
                    Else
                        Try

                            drr("StrikeMod") = (drr("Strike_Price") / 100) Mod 1
                        Catch ex As Exception
                            drr("StrikeMod") = 1
                        End Try
                    End If
                Next




                Dim dt As New DataTable
                dt = tmpCpfmaster.Clone
                dt.AcceptChanges()
                Dim dr() As DataRow
                dr = tmpCpfmaster.Select("Symbol='" & strsymbol & "' And StrikeMod = 0", "")
                For Each dataRow As DataRow In dr
                    dt.ImportRow(dataRow)
                Next
                dtcpfmaster = dt

                '  dtcpfmaster = New DataView(tmpCpfmaster, "Symbol='" & strCompany & "' And StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
            Else
                Dim dt As New DataTable
                dt = tmpCpfmaster.Clone
                dt.AcceptChanges()
                Dim dr() As DataRow
                dr = tmpCpfmaster.Select("Symbol='" & strsymbol & "'", "")
                For Each dataRow As DataRow In dr
                    dt.ImportRow(dataRow)
                Next
                dtcpfmaster = dt
                '  dtcpfmaster = New DataView(tmpCpfmaster, "Symbol='" & strCompany & "'", "", DataViewRowState.CurrentRows).ToTable()
            End If
        Catch ex As Exception


        End Try

    End Sub
    '    Private Sub Cal_Greeks(ByRef dtMarketTable As DataTable, ByVal mdate As Date, ByVal mtoken As Long, ByVal nextmonth As Date)
    '        Dim PEBuyprice As Double = 0
    '        Dim PESellprice As Double = 0
    '        Dim CEBuyprice As Double = 0
    '        Dim CESellprice As Double = 0
    '        Dim FutBuyprice As Double = 0
    '        Dim Futsellprice As Double = 0
    '        Dim MARKETLotSize As Double
    '        Try
    '            WriteLogMarketwatchlog("Cal_Greeks start")
    '            Dim tok As Long
    '            '  'Write_Log2("Step2B: Cal_Greeks Process Start..")
    '            'CHANGE_IN_OI = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='CHANGE_IN_OI'").ToString)
    '            For Each dr1 As DataRow In dtMarketTable.Select()
    '                ''''''''''''''''''''''''
    '                If NetMode = "TCP" Or NetMode = "API" Or NetMode = "JL" Then
    '                    If Not ArrTokenSeries.Contains(dr1("CallToken")) Then
    '                        ArrTokenSeries.Add(dr1("CallToken"))
    '                        strTokens = strTokens & IIf(strTokens.Length > 0, ",", "") & dr1("CallToken").ToString()
    '                        If flgAPI_K = "TRUEDATA" Then
    '                            FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(dr1("CallToken").ToString())))
    '                        End If

    '                    End If

    '                    If Not ArrTokenSeries.Contains(dr1("PutToken")) Then
    '                        ArrTokenSeries.Add(dr1("PutToken"))
    '                        strTokens = strTokens & IIf(strTokens.Length > 0, ",", "") & dr1("PutToken").ToString()
    '                        FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(dr1("PutToken").ToString())))
    '                    End If
    '                    strTokens = strTokens & IIf(strTokens.Length > 0, ",", "") & dr1("PutToken").ToString()
    '                    If DtFMonthDate.Rows.Count > 0 Then
    '                        ' If chkIsWeekly.Checked = True And strCompany = "BANKNIFTY" Then
    '                        If chkIsWeekly.Checked = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                            Dim index1 As Double ' = Val(eIdxprice("Nifty Bank"))
    '                            If RBcalVol.Checked = True Then
    '                                If strCompany = "NIFTY" Then
    '                                    index1 = Val(eIdxprice("Nifty 50"))
    '                                ElseIf strCompany = "BANKNIFTY" Then
    '                                    index1 = Val(eIdxprice("Nifty Bank"))
    '                                ElseIf strCompany = "FINNIFTY" Then
    '                                    index1 = Val(eIdxprice("NiftyFinService"))

    '                                End If
    '                            Else
    '                                'If boolIsCurrency Then
    '                                '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDate.Rows(0)(1))))

    '                                'Else
    '                                Fltp1 = Val(fltpprice(CLng(DtFMonthDate.Rows(0)(1))))
    '                                'End If
    '                                'If boolIsCurrency Then
    '                                '    index1 = get_SFutCurr(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                                'Else
    '                                index1 = Val(hashsyn(DtFMonthDate.Rows(0)("expdate1") + "_" + strCompany) & "") ' get_SFut(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                                'End If

    '                            End If
    '                            Dim mT11 As Double
    '                            If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                                Dim BCast1 As Date
    '                                'If boolIsCurrency Then
    '                                '    BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                                'Else
    '                                BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                                'End If

    '                                mT11 = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()).Date)
    '                            Else
    '                                mT11 = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()).Date)
    '                            End If


    '                            Dim index As Double = (((index1 * Rateofinterest) / 365) * mT11) + index1
    '                            Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(Date.Now) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '                            If drmonth2.Rows.Count > 0 Then


    '                                tok = CLng(drmonth2.Rows(0)("token"))
    '                            End If
    '                            'Dim strike As Double = Val(eIdxprice("Nifty Bank"))
    '                            'Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & strike & "  and expdate1 >=#" & CDate(Date.Now) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

    '                            'tok = drmonth2.Rows(0)("token")
    '                            If Not ArrTokenSeries.Contains(Val(tok)) Then
    '                                ArrTokenSeries.Add(Val(CLng(tok) & ""))
    '                                strTokens = strTokens & IIf(strTokens.Length > 0, ",", "") & tok.ToString()
    '                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(tok.ToString())))
    '                            End If
    '                        Else
    '                            If Not ArrTokenSeries.Contains(Val(DtFMonthDate.Rows(0)(1) & "")) Then
    '                                ArrTokenSeries.Add(Val(DtFMonthDate.Rows(0)(1) & ""))
    '                                strTokens = strTokens & IIf(strTokens.Length > 0, ",", "") & DtFMonthDate.Rows(0)(1).ToString()
    '                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(DtFMonthDate.Rows(0)(1).ToString())))
    '                            End If
    '                        End If

    '                    End If
    '                    If DtFMonthDate.Rows.Count > 1 Then
    '                        'If chkIsWeekly.Checked = True And strCompany = "BANKNIFTY" Then
    '                        '    Dim index1 As Double = Val(eIdxprice("Nifty Bank"))
    '                        If chkIsWeekly.Checked = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                            Dim index1 As Double ' = Val(eIdxprice("Nifty Bank"))

    '                            If RBcalVol.Checked = True Then
    '                                If strCompany = "NIFTY" Then
    '                                    index1 = Val(eIdxprice("Nifty 50"))
    '                                ElseIf strCompany = "BANKNIFTY" Then
    '                                    index1 = Val(eIdxprice("Nifty Bank"))

    '                                ElseIf strCompany = "FINNIFTY" Then
    '                                    index1 = Val(eIdxprice("NiftyFinService"))
    '                                End If
    '                            Else
    '                                'If boolIsCurrency Then
    '                                '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDate.Rows(1)(1))))

    '                                'Else
    '                                Fltp1 = Val(fltpprice(CLng(DtFMonthDate.Rows(1)(1))))
    '                                'End If
    '                                'If boolIsCurrency Then
    '                                '    index1 = get_SFutCurr(strCompany, DtFMonthDate.Rows(1)("expdate1"), Convert.ToDouble(Fltp1))
    '                                'Else
    '                                index1 = Val(hashsyn(DtFMonthDate.Rows(1)("expdate1") & "_" & strCompany) & "") 'get_SFut(strCompany, DtFMonthDate.Rows(1)("expdate1"), Convert.ToDouble(Fltp1))
    '                                'End If

    '                            End If


    '                            Dim mT11 As Double
    '                            If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                                Dim BCast1 As Date
    '                                'If boolIsCurrency Then
    '                                '    BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                                'Else
    '                                BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                                'End If

    '                                mT11 = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()).Date)
    '                            Else
    '                                mT11 = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()).Date)
    '                            End If


    '                            'Dim mT11 As Double = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()).Date)
    '                            Dim index As Double = (((index1 * Rateofinterest) / 365) * mT11) + index1
    '                            Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(Date.Now) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '                            Try
    '                                tok = CLng(drmonth2.Rows(0)("token"))
    '                            Catch ex As Exception

    '                            End Try

    '                            'Dim strike As Double = Val(eIdxprice("Nifty Bank"))
    '                            'Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & strike & "  and expdate1 >=#" & CDate(Date.Now) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

    '                            'tok = drmonth2.Rows(0)("token")
    '                            If Not ArrTokenSeries.Contains(Val(tok & "")) Then
    '                                ArrTokenSeries.Add(Val(CLng(tok) & ""))
    '                                strTokens = strTokens & IIf(strTokens.Length > 0, ",", "") & tok.ToString()
    '                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(tok.ToString())))
    '                            End If
    '                        Else
    '                            If Not ArrTokenSeries.Contains(Val(DtFMonthDate.Rows(1)(1) & "")) Then
    '                                ArrTokenSeries.Add(Val(DtFMonthDate.Rows(1)(1) & ""))
    '                                strTokens = strTokens & IIf(strTokens.Length > 0, ",", "") & DtFMonthDate.Rows(1)(1).ToString()
    '                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(DtFMonthDate.Rows(1)(1).ToString())))
    '                            End If
    '                        End If

    '                    End If
    '                    If DtFMonthDate.Rows.Count > 2 Then
    '                        If chkIsWeekly.Checked = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                            Dim index1 As Double '= Val(eIdxprice("Nifty Bank"))
    '                            If RBcalVol.Checked = True Then
    '                                If strCompany = "NIFTY" Then
    '                                    index1 = Val(eIdxprice("Nifty 50"))
    '                                ElseIf strCompany = "BANKNIFTY" Then
    '                                    index1 = Val(eIdxprice("Nifty Bank"))
    '                                ElseIf strCompany = "FINNIFTY" Then
    '                                    index1 = Val(eIdxprice("NiftyFinService"))
    '                                End If
    '                            Else
    '                                'If boolIsCurrency Then
    '                                '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDate.Rows(2)(1))))

    '                                'Else
    '                                Fltp1 = Val(fltpprice(CLng(DtFMonthDate.Rows(2)(1))))
    '                                'End If

    '                                'If boolIsCurrency = True Then
    '                                '    Dim curexpd As Date = Currencymaster.Compute("max(expdate1)", "token=" & CLng(DtFMonthDate.Rows(2)(1)) & "") 'DtFMonthDate.Rows(2)("expdate1")
    '                                '    index1 = get_SFutCurr(strCompany, curexpd, Convert.ToDouble(Fltp1))
    '                                'Else
    '                                index1 = Val(hashsyn(DtFMonthDate.Rows(2)("expdate1") & "_" & strCompany) & "") ' get_SFut(strCompany, DtFMonthDate.Rows(2)("expdate1"), Convert.ToDouble(Fltp1))
    '                                'End If

    '                            End If
    '                            Dim mT11 As Double
    '                            If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                                Dim BCast1 As Date
    '                                'If boolIsCurrency Then
    '                                '    BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                                'Else
    '                                BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                                'End If

    '                                mT11 = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()).Date)
    '                            Else
    '                                mT11 = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()).Date)
    '                            End If

    '                            'Dim mT11 As Double = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()).Date)
    '                            Dim index As Double = (((index1 * Rateofinterest) / 365) * mT11) + index1
    '                            Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(Date.Now) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

    '                            tok = CLng(drmonth2.Rows(0)("token"))
    '                            'Dim strike As Double = Val(eIdxprice("Nifty Bank"))
    '                            'Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & strike & "  and expdate1 >=#" & CDate(Date.Now) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

    '                            'tok = drmonth2.Rows(0)("token")
    '                            If Not ArrTokenSeries.Contains(Val(CLng(tok) & "")) Then
    '                                ArrTokenSeries.Add(Val(CLng(tok) & ""))
    '                                'strTokens = strTokens & IIf(strTokens.Length > 0, ",", "") & tok.ToString()
    '                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(tok.ToString())))
    '                            End If
    '                        Else
    '                            If Not ArrTokenSeries.Contains(Val(DtFMonthDate.Rows(2)(1) & "")) Then
    '                                ArrTokenSeries.Add(Val(DtFMonthDate.Rows(2)(1) & ""))
    '                                strTokens = strTokens & IIf(strTokens.Length > 0, ",", "") & DtFMonthDate.Rows(2)(1).ToString()
    '                                FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(DtFMonthDate.Rows(2)(1).ToString())))
    '                            End If
    '                        End If

    '                    End If
    '                End If

    '                Dim prevLTP, prev2LTP, nextLTP, next2LTP As Double
    '                'If boolIsCurrency Then
    '                '    If Currltpprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                '        dr1("CE") = Math.Round(Val(Currltpprice(CLng(dr1("CallToken")))), RoundCurrencyLTP)   'Call Strike Price
    '                '    End If

    '                '    If Currltpprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                '        dr1("PE") = Math.Round(Val(Currltpprice(CLng(dr1("PutToken")))), RoundCurrencyLTP)    'Put Strike Price                
    '                '    End If


    '                '    If chkIsWeekly.Checked = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                '        Dim index1 As Double ' = Val(eIdxprice("Nifty Bank"))
    '                '        If RBcalVol.Checked = True Then
    '                '            If strCompany = "NIFTY" Then
    '                '                index1 = Val(eIdxprice("Nifty 50"))
    '                '            ElseIf strCompany = "BANKNIFTY" Then
    '                '                index1 = Val(eIdxprice("Nifty Bank"))
    '                '            ElseIf strCompany = "FINNIFTY" Then
    '                '                index1 = Val(eIdxprice("NiftyFinService"))
    '                '            End If
    '                '        Else
    '                '            If boolIsCurrency Then
    '                '                Fltp1 = Val(Currfltpprice(CLng(DtFMonthDate.Rows(0)(1))))

    '                '            Else
    '                '                Fltp1 = Val(fltpprice(CLng(DtFMonthDate.Rows(0)(1))))
    '                '            End If
    '                '            If boolIsCurrency = True Then
    '                '                index1 = get_SFutCurr(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                '            Else
    '                '                index1 = Val(hashsyn(DtFMonthDate.Rows(0)("expdate1") & "_" & strCompany) & "") ' get_SFut(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                '            End If


    '                '        End If
    '                '        dr1("futltp") = Val(index1)
    '                '    Else
    '                '        If Currfltpprice.Contains(mtoken) Then
    '                '            dr1("futltp") = Val(Currfltpprice(mtoken)) 'Future LTP                
    '                '        End If
    '                '    End If
    '                '    prevLTP = Math.Round(Val(Currltpprice(CLng(dr1("CallPreToken")))), RoundCurrencyLTP)  'Val(dtMarketTable.Compute("max(CE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND  strike1 = '" & dr1("strike1") - dGap & "'") & "")
    '                '    prev2LTP = Math.Round(Val(Currltpprice(CLng(dr1("CallPre2Token")))), RoundCurrencyLTP) 'Val(dtMarketTable.Compute("max(CE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") - (dGap * 2) & "'") & "")
    '                '    nextLTP = Math.Round(Val(Currltpprice(CLng(dr1("CallNextToken")))), RoundCurrencyLTP)  'Val(dtMarketTable.Compute("max(CE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") + dGap & "'") & "")
    '                '    next2LTP = Math.Round(Val(Currltpprice(CLng(dr1("CallNext2Token")))), RoundCurrencyLTP)  'Val(dtMarketTable.Compute("max(CE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") + (dGap * 2) & "'") & "")

    '                'Else


    '                '    If ltpprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                '        dr1("CE") = Math.Round(Val(ltpprice(CLng(dr1("CallToken")))), Roundltp)   'Call Strike Price
    '                '        CEBuyprice = Math.Round(Val(buyprice(CLng(dr1("CallToken")))), Roundltp)
    '                '        CESellprice = Math.Round(Val(saleprice(CLng(dr1("CallToken")))), Roundltp)
    '                '    End If

    '                '    If ltpprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                '        dr1("PE") = Math.Round(Val(ltpprice(CLng(dr1("PutToken")))), Roundltp)    'Put Strike Price
    '                '        PEBuyprice = Math.Round(Val(buyprice(CLng(dr1("PutToken")))), Roundltp)
    '                '        PESellprice = Math.Round(Val(saleprice(CLng(dr1("PutToken")))), Roundltp)

    '                '    End If


    '                '    If chkIsWeekly.Checked = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                '        Dim index1 As Double ' = Val(eIdxprice("Nifty Bank"))
    '                '        If RBcalVol.Checked = True Then
    '                '            If strCompany = "NIFTY" Then
    '                '                index1 = Val(eIdxprice("Nifty 50"))
    '                '            ElseIf strCompany = "BANKNIFTY" Then
    '                '                index1 = Val(eIdxprice("Nifty Bank"))
    '                '            ElseIf strCompany = "FINNIFTY" Then
    '                '                index1 = Val(eIdxprice("NiftyFinService"))
    '                '            End If
    '                '        Else
    '                '            If boolIsCurrency Then
    '                '                Fltp1 = Val(Currfltpprice(CLng(DtFMonthDate.Rows(0)(1))))

    '                '            Else
    '                '                Fltp1 = Val(fltpprice(CLng(DtFMonthDate.Rows(0)(1))))
    '                '            End If
    '                '            If boolIsCurrency = True Then
    '                '                index1 = get_SFutCurr(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                '            Else
    '                '                index1 = Val(hashsyn(DtFMonthDate.Rows(0)("expdate1") & "_" & strCompany) & "") 'get_SFut(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                '            End If


    '                '        End If
    '                '        dr1("futltp") = Val(index1)
    '                '        FutBuyprice = Val(index1)
    '                '        Futsellprice = Val(index1)
    '                '    Else
    '                '        If fltpprice.Contains(mtoken) Then
    '                '            dr1("futltp") = Val(fltpprice(mtoken)) 'Future LTP
    '                '            FutBuyprice = Val(fbuyprice(mtoken))
    '                '            Futsellprice = Val(fsaleprice(mtoken))
    '                '        End If
    '                '    End If
    '                '    'Status/StopLoss
    '                '    If TrendStopLoss.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                '        dr1("CEStopLoss") = Val(TrendStopLoss(CLng(dr1("CallToken"))))
    '                '    End If
    '                '    If TrendStopLoss.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                '        dr1("PEStopLoss") = Val(TrendStopLoss(CLng(dr1("PutToken"))))
    '                '    End If

    '                '    If TrendStatus.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                '        dr1("CEStatus") = GetStatus(Val(TrendStatus(CLng(dr1("CallToken")))), Val(TrendStopLoss(CLng(dr1("CallToken")))), Val(ltpprice(CLng(dr1("CallToken")))))
    '                '    End If
    '                '    If TrendStatus.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                '        dr1("PEStatus") = GetStatus(Val(TrendStatus(CLng(dr1("PutToken")))), Val(TrendStopLoss(CLng(dr1("PutToken")))), Val(ltpprice(CLng(dr1("PutToken")))))
    '                '    End If



    '                '    prevLTP = Math.Round(Val(ltpprice(CLng(dr1("CallPreToken")))), Roundltp)  'Val(dtMarketTable.Compute("max(CE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND  strike1 = '" & dr1("strike1") - dGap & "'") & "")
    '                '    prev2LTP = Math.Round(Val(ltpprice(CLng(dr1("CallPre2Token")))), Roundltp) 'Val(dtMarketTable.Compute("max(CE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") - (dGap * 2) & "'") & "")
    '                '    nextLTP = Math.Round(Val(ltpprice(CLng(dr1("CallNextToken")))), Roundltp)  'Val(dtMarketTable.Compute("max(CE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") + dGap & "'") & "")
    '                '    next2LTP = Math.Round(Val(ltpprice(CLng(dr1("CallNext2Token")))), Roundltp)  'Val(dtMarketTable.Compute("max(CE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") + (dGap * 2) & "'") & "")

    '                'End If


    '                'If dr1("strike1") = 7800 Then
    '                '    dr1("CallBF") = Math.Round((dr1("CE") * 2) - prevLTP - nextLTP, RoundBF)     'Call BF
    '                'End If
    '                dr1("CallBF") = Math.Round((dr1("CE") * 2) - prevLTP - nextLTP, RoundBF)     'Call BF

    '                dr1("CallBF2") = Math.Round((dr1("CE") * 2) - prev2LTP - next2LTP, RoundBF2)  'Call BF2

    '                dr1("CallStraddle") = Math.Round(dr1("CE") + dr1("PE"), RoundStraddle)     'Call Straddle

    '                dr1("CR") = Math.Round(dr1("futltp") - (dr1("Strike1") + dr1("CE") - dr1("PE")), RoundStraddle)




    '                If Not nextLTP = 0 Then
    '                    dr1("CallRatio") = Math.Round(dr1("CE") / nextLTP, RoundRatio)          'Call Ratio
    '                End If

    '                '''''''''''''''''''Calculate Delta, Gamma, Vega, Theta for Call Start'''''''''''''''''
    '                Dim mVolatility, futval, stkprice, Mrateofinterast, mIsCall, mIsFut As Double
    '                Dim mVolPrev_Call As Double
    '                futval = dr1("futltp")
    '                stkprice = dr1("Strike1")
    '                Mrateofinterast = 0
    '                mIsCall = True
    '                mIsFut = False

    '                Dim mT As Double '= UDDateDiff(DateInterval.Day, Now.Date, CDate(dr1("Maturity")).Date)


    '                If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                    Dim BCast1 As Date
    '                    If boolIsCurrency Then
    '                        BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                    Else
    '                        BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                    End If

    '                    mT = UDDateDiff(DateInterval.Day, BCast1, CDate(dr1("Maturity")).Date)
    '                Else
    '                    mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(dr1("Maturity")).Date)
    '                End If
    '                'mT = mT / 365
    '                Dim _mt As Double
    '                If mT = 0 Then
    '                    _mt = 0.00001
    '                    '_mt1 = 0.00001
    '                Else
    '                    _mt = (mT) / 365
    '                    '_mt1 = ((mT + 1) - CInt(txtnoofday.Text)) / 365
    '                    '_mt1 = (mmT) / 365
    '                End If

    '                Dim n As Integer = 0
    '                Dim nC1, nC2 As Double
    '                n = IIf((DateTime.Now.DayOfWeek = 1), 3, 1)

    '                If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                    Dim BCast1 As Date
    '                    If boolIsCurrency Then
    '                        BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                    Else
    '                        BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                    End If

    '                    nC2 = UDDateDiff(DateInterval.Day, DateAdd(DateInterval.Day, (-1 * n), BCast1), CDate(dr1("Maturity")).Date)
    '                Else
    '                    nC2 = UDDateDiff(DateInterval.Day, DateAdd(DateInterval.Day, (-1 * n), Now.Date), CDate(dr1("Maturity")).Date)
    '                End If



    '                If nC2 = 0 Then
    '                    'nC1 = 0.00001
    '                    nC1 = 0.5
    '                Else
    '                    nC1 = nC2 / 365
    '                End If

    '                Dim nC As Double
    '                'n = IIf((DateTime.Now.DayOfWeek = 1), 3, 1)
    '                'nC = ((nC1 + n) / 365)
    '                nC = nC1


    '                If Now.Date = CDate(dr1("Maturity")).Date Then
    '                    _mt = 0.5 / 365

    '                End If
    '                If CDate(DateAdd(DateInterval.Day, (-1 * n), Now.Date)).Date = CDate(dr1("Maturity")).Date Then
    '                    nC = 0.5 / 365
    '                End If
    '                'Write_Log2("Step2B.1: Calculate CallDelta,CallGamma Start..")



    '                Dim dPremium As Double = dr1("CE")
    '                mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, dPremium, _mt, True, False, 0, 6)
    '                If chkIsWeekly.Checked = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "USDINR") Then
    '                    mVolPrev_Call = Greeks.Black_Scholes(Val(idxclose), stkprice, Mrateofinterast, 0, Val(closeprice(CLng(dr1("CallToken")))), nC, True, False, 0, 6)
    '                Else
    '                    mVolPrev_Call = Greeks.Black_Scholes(Val(closeprice(CLng(mtoken))), stkprice, Mrateofinterast, 0, Val(closeprice(CLng(dr1("CallToken")))), nC, True, False, 0, 6)
    '                End If

    '                'Dim dcall As Double = Greeks.Black_Scholes(7959.2, 8000, Mrateofinterast, 0, 85.6, 0.038, True, False, 0, 6)





    '                dr1("CallDelta") = Math.Round((Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1)), roundDelta)
    '                dr1("CallGamma") = Math.Round((Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2)), roundGamma)
    '                dr1("CallVega") = Math.Round((Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3)), roundVega)
    '                dr1("CallTheta") = Math.Round((Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4)), roundTheta)
    '                dr1("CallVol") = Math.Round(mVolatility * 100, RoundVol)
    '                'Write_Log2("Step2B.1: Calculate CallDelta,CallGamma Stop..")
    '                Dim mtt1 As Double = 0
    '                Dim Day1 As Integer = CInt(Val(txtnoofday.Text))
    '                If Val(txtnoofday.Text) < 1 Then
    '                    mtt1 = IIf(Val(txtnoofday.Text) = 0, 0.0001, Val(txtnoofday.Text)) / 365
    '                Else
    '                    If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                        Dim BCast1 As Date
    '                        If boolIsCurrency Then
    '                            BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                        Else
    '                            BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                        End If


    '                        If CDate(dr1("Maturity")).Date.AddDays(Day1 * -1) <= BCast1 Then
    '                            If CDate(dr1("Maturity")).Date.AddDays(Day1 * -1) = BCast1 Then
    '                                mtt1 = 0.5 / 365
    '                            Else
    '                                mtt1 = 0.00001
    '                            End If
    '                        Else
    '                            mtt1 = Day1 / 365
    '                            'mtt1 = UDDateDiff(DateInterval.Day, Now.Date.AddDays(Day1).Date, CDate(dr1("Maturity")).Date) / 365
    '                        End If
    '                    Else
    '                        If CDate(dr1("Maturity")).Date.AddDays(Day1 * -1) <= Now.Date Then
    '                            If CDate(dr1("Maturity")).Date.AddDays(Day1 * -1) = Now.Date Then
    '                                mtt1 = 0.5 / 365
    '                            Else
    '                                mtt1 = 0.00001
    '                            End If
    '                        Else
    '                            mtt1 = Day1 / 365
    '                            'mtt1 = UDDateDiff(DateInterval.Day, Now.Date.AddDays(Day1).Date, CDate(dr1("Maturity")).Date) / 365
    '                        End If
    '                    End If


    '                    'If DateAdd(DateInterval.Day, Day1, Now.Date).Date >= CDate(dr1("Maturity")).Date Then
    '                    '    mtt1 = 0.5 / 365
    '                    'Else
    '                    '    mtt1 = Day1 / 365
    '                    'End If
    '                End If

    '                dr1("CallVol1") = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, dPremium, mtt1, True, False, 0, 6) * 100
    '                'dtMarketTable.Columns.Add("CallVol1", GetType(Double))
    '                '
    '                dr1("CallVolChg") = Math.Round((mVolatility - mVolPrev_Call) * 100, RoundVol) ' Put Vol Chg(%)

    '                ''''''''''''''''''Calculate Delta, Gamma, Vega, Theta for Call End'''''''''''''''''


    '                Dim prevLTP_Put, prev2LTP_Put, nextLTP_Put, next2LTP_Put As Double
    '                If boolIsCurrency Then
    '                    prevLTP_Put = Math.Round(Val(Currltpprice(CLng(dr1("PutPreToken")))), RoundCurrencyLTP)  'Val(dtMarketTable.Compute("max(PE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND  strike1 = '" & dr1("strike1") - dGap & "'") & "")
    '                    prev2LTP_Put = Math.Round(Val(Currltpprice(CLng(dr1("PutPre2Token")))), RoundCurrencyLTP)   'Val(dtMarketTable.Compute("max(PE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") - (dGap * 2) & "'") & "")
    '                    nextLTP_Put = Math.Round(Val(Currltpprice(CLng(dr1("PutNextToken")))), RoundCurrencyLTP)  'Val(dtMarketTable.Compute("max(PE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") + dGap & "'") & "")
    '                    next2LTP_Put = Math.Round(Val(Currltpprice(CLng(dr1("PutNext2Token")))), RoundCurrencyLTP)  'Val(dtMarketTable.Compute("max(PE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") + (dGap * 2) & "'") & "")

    '                Else
    '                    prevLTP_Put = Math.Round(Val(ltpprice(CLng(dr1("PutPreToken")))), Roundltp)  'Val(dtMarketTable.Compute("max(PE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND  strike1 = '" & dr1("strike1") - dGap & "'") & "")
    '                    prev2LTP_Put = Math.Round(Val(ltpprice(CLng(dr1("PutPre2Token")))), Roundltp)   'Val(dtMarketTable.Compute("max(PE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") - (dGap * 2) & "'") & "")
    '                    nextLTP_Put = Math.Round(Val(ltpprice(CLng(dr1("PutNextToken")))), Roundltp)  'Val(dtMarketTable.Compute("max(PE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") + dGap & "'") & "")
    '                    next2LTP_Put = Math.Round(Val(ltpprice(CLng(dr1("PutNext2Token")))), Roundltp)  'Val(dtMarketTable.Compute("max(PE)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND strike1 = '" & dr1("strike1") + (dGap * 2) & "'") & "")

    '                End If

    '                dr1("PutBF") = Math.Round((dr1("PE") * 2) - prevLTP_Put - nextLTP_Put, RoundBF)   'Put BF

    '                dr1("PutBF2") = Math.Round((dr1("PE") * 2) - prev2LTP_Put - next2LTP_Put, RoundBF2) 'Put BF2

    '                If Not dr1("PE") = 0 Then
    '                    dr1("PutRatio") = Math.Round(nextLTP_Put / dr1("PE"), RoundRatio)   'Put Ratio
    '                End If

    '                Dim mVolatility_Put, mVolPrev_Put As Double
    '                futval = dr1("futltp")
    '                stkprice = dr1("Strike1")
    '                Mrateofinterast = 0
    '                mIsCall = False
    '                mIsFut = False
    '                'Format(CDate(drow("Exp# Date")), "ddMMMyyyy")
    '                Dim mT1 As Double
    '                If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                    Dim BCast1 As Date
    '                    If boolIsCurrency Then
    '                        BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                    Else
    '                        BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                    End If

    '                    mT1 = UDDateDiff(DateInterval.Day, Format(BCast1, "ddMMMyyyy"), Format(CDate(dr1("Maturity")).Date, "ddMMMyyyy"))
    '                Else
    '                    mT1 = UDDateDiff(DateInterval.Day, Format(Now.Date, "ddMMMyyyy"), Format(CDate(dr1("Maturity")).Date, "ddMMMyyyy"))
    '                End If

    '                Dim _mt1 As Double
    '                If mT1 = 0 Then
    '                    _mt1 = 0.00001
    '                Else
    '                    _mt1 = (mT1) / 365
    '                End If

    '                Dim n1 As Integer = 0
    '                Dim nT1, nT2 As Double
    '                If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                    Dim BCast1 As Date
    '                    If boolIsCurrency Then
    '                        BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                    Else
    '                        BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                    End If

    '                    nT2 = UDDateDiff(DateInterval.Day, BCast1, CDate(dr1("Maturity")).Date)
    '                Else
    '                    nT2 = UDDateDiff(DateInterval.Day, Now.Date, CDate(dr1("Maturity")).Date)
    '                End If


    '                If nT2 = 0 Then
    '                    nT1 = 0.00001
    '                Else
    '                    nT1 = nT2 / 365
    '                End If

    '                Dim nT As Double
    '                'n1 = IIf((DateTime.Now.DayOfWeek = 1), 3, 1)
    '                'nT = ((nT1 + n) / 365)
    '                nT = nT1

    '                If Now.Date = CDate(dr1("Maturity")).Date Then
    '                    _mt1 = 0.5 / 365

    '                End If
    '                If CDate(DateAdd(DateInterval.Day, (-1 * n), Now.Date)).Date = CDate(dr1("Maturity")).Date Then
    '                    nC1 = 0.5 / 365
    '                End If



    '                mVolatility_Put = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, dr1("PE"), _mt1, False, False, 0, 6)
    '                If chkIsWeekly.Checked = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "USDINR") Then
    '                    mVolPrev_Put = Greeks.Black_Scholes(Val(idxclose), stkprice, Mrateofinterast, 0, Val(closeprice(CLng(dr1("PutToken")))), nC, False, False, 0, 6)
    '                Else
    '                    mVolPrev_Put = Greeks.Black_Scholes(Val(closeprice(CLng(mtoken))), stkprice, Mrateofinterast, 0, Val(closeprice(CLng(dr1("PutToken")))), nC, False, False, 0, 6)
    '                End If


    '                dr1("PutDelta") = Math.Round((Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility_Put, _mt1, mIsCall, mIsFut, 0, 1)), roundDelta)
    '                dr1("PutGamma") = Math.Round((Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility_Put, _mt1, mIsCall, mIsFut, 0, 2)), roundGamma)
    '                dr1("PutVega") = Math.Round((Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility_Put, _mt1, mIsCall, mIsFut, 0, 3)), roundVega)
    '                dr1("PutTheta") = Math.Round((Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility_Put, _mt1, mIsCall, mIsFut, 0, 4)), roundTheta)
    '                dr1("PutVol") = Math.Round(mVolatility_Put * 100, RoundVol)

    '                If Val(txtnoofday.Text) < 1 Then
    '                    mtt1 = IIf(Val(txtnoofday.Text) = 0, 0.0001, Val(txtnoofday.Text)) / 365
    '                Else
    '                    Day1 = CInt(Val(txtnoofday.Text))
    '                    mtt1 = 0
    '                    If DateAdd(DateInterval.Day, Day1, Now.Date).Date >= CDate(dr1("Maturity")).Date Then
    '                        mtt1 = 0.5 / 365
    '                    Else
    '                        mtt1 = Convert.ToDouble(Day1) / 365
    '                    End If
    '                End If



    '                dr1("PutVol1") = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, dr1("PE"), mtt1, False, False, 0, 6) * 100


    '                dr1("PCP") = Math.Round(dr1("CE") - dr1("PE") - dr1("futltp") + dr1("strike1"), RoundPCP) 'PCP
    '                dr1("PCPA") = Math.Round(CESellprice - PESellprice - Futsellprice + dr1("strike1"), RoundPCP) 'PCP
    '                dr1("PCPB") = Math.Round(CEBuyprice - PEBuyprice - FutBuyprice + dr1("strike1"), RoundPCP) 'PCP


    '                Dim Closing_Price As Double
    '                If closeprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                    Closing_Price = Val(closeprice(CLng(dr1("CallToken"))))   'Closing Price
    '                    dr1("CallChg") = Math.Round(dr1("CE") - Closing_Price, IIf(boolIsCurrency, RoundCurrencyLTP, Roundltp))  ' Call Change(LTP)
    '                End If

    '                If closeprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                    Closing_Price = Val(closeprice(CLng(dr1("PutToken"))))   'Closing Price
    '                    dr1("PutChg") = Math.Round(dr1("PE") - Closing_Price, IIf(boolIsCurrency, RoundCurrencyLTP, Roundltp))  ' Put Change(LTP)
    '                End If

    '                dr1("PutVolChg") = Math.Round((mVolatility_Put - mVolPrev_Put) * 100, RoundVol) ' Put Vol Chg(%)

    '                If volumeprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                    dr1("CallVolume") = Math.Round(Val(volumeprice(CLng(dr1("CallToken")))), RoundVolume)   'Call Volume
    '                End If

    '                If volumeprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                    dr1("PutVolume") = Math.Round(Val(volumeprice(CLng(dr1("PutToken")))), RoundVolume)   'Put Volume
    '                End If

    '                'If NetMode <> "NET" Then
    '                'Open Interest for Call
    '                If Val(dr1("CallToken")) = 49741 Then
    '                    Dim prevOI1111 As Double = 22
    '                End If
    '                If OpenInterestprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then

    '                    Dim prevOI As Double

    '                    If CHANGE_IN_OI = 1 Then
    '                        If NetMode = "UDP" Then
    '                            prevOI = Val((PrevOIDay(dr1("CallToken")) & "")) 'Prev OpenInterest ("Call"))
    '                        ElseIf NetMode = "TCP" Or NetMode = "JL" Then

    '                            prevOI = Val((OpenInterestbhavcopy(dr1("CallToken")) & "")) 'Prev OpenInterest ("Call"))
    '                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then

    '                            prevOI = Val((OpenInterestbhavcopy(dr1("CallToken")) & "")) 'Prev OpenInterest ("Call"))
    '                        End If

    '                    ElseIf CHANGE_IN_OI = 2 Then
    '                        prevOI = Val(PrevOI_Call(CLng(dr1("CallToken"))) & "")  'Prev OpenInterest ("Call")
    '                    End If


    '                    MARKETLotSize = HT_FOLotContrct(strCompany + Format(CDate(mdate), "ddMMMyyyy"))

    '                    dr1("CallOI") = Math.Round(Val(OpenInterestprice(CLng(dr1("CallToken")))) / MARKETLotSize, RoundOpenInt)   'OI (Call)

    '                    If dr1("CallOI") <> prevOI Then
    '                        dr1("CallOIChg") = Math.Round(dr1("CallOI") - prevOI, RoundOpenInt)          'Chg in OI (Call)
    '                    End If

    '                    If PrevOI_Call.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                        PrevOI_Call.Item(CLng(Val(dr1("CallToken") & ""))) = dr1("CallOI")
    '                    Else
    '                        PrevOI_Call.Add(CLng(Val(dr1("CallToken") & "")), dr1("CallOI"))
    '                    End If


    '                End If

    '                'Open Interest for Put
    '                If OpenInterestprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then

    '                    Dim prevOI As Double


    '                    If CHANGE_IN_OI = 1 Then
    '                        If NetMode = "UDP" Then
    '                            prevOI = Val(PrevOIDay(dr1("PutToken")) & "")  'Prev OpenInterest (Put)
    '                        ElseIf NetMode = "TCP" Or NetMode = "JL" Then
    '                            prevOI = Val((OpenInterestbhavcopy(dr1("PutToken")) & "")) 'Prev OpenInterest ("Call"))
    '                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
    '                            prevOI = Val((OpenInterestbhavcopy(dr1("PutToken")) & ""))
    '                        End If
    '                    ElseIf CHANGE_IN_OI = 2 Then
    '                        prevOI = Val(PrevOI_Put(CLng(dr1("PutToken"))) & "")  'Prev OpenInterest (Put)

    '                    End If
    '                    dr1("PutOI") = Math.Round(Val(OpenInterestprice(CLng(dr1("PutToken"))) / MARKETLotSize), RoundOpenInt)   'OI (Put)

    '                    If dr1("PutOI") <> prevOI Then
    '                        dr1("PutOIChg") = Math.Round(dr1("PutOI") - prevOI, RoundOpenInt)         'Chg in OI (Put)
    '                    End If


    '                    If PrevOI_Put.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                        PrevOI_Put.Item(CLng(Val(dr1("PutToken") & ""))) = dr1("PutOI")
    '                    Else
    '                        PrevOI_Put.Add(CLng(Val(dr1("PutToken") & "")), dr1("PutOI"))
    '                    End If

    '                End If

    '                dr1("TotalOI") = Math.Round((dr1("PutOI") + dr1("CallOI")), RoundOpenInt)  'Total OI
    '                Try


    '                    Dim SumTotalOI As Double
    '                    SumTotalOI = Val(dtMarketTable.Compute("sum(TotalOI)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' ") & "")
    '                    If Not SumTotalOI = 0 Then
    '                        dr1("PutTotalOIPer") = Math.Round(dr1("TotalOI") / SumTotalOI, RoundOpenInt)   'Total OI(%)
    '                    End If

    '                Catch ex As Exception
    '                    dr1("PutTotalOIPer") = 0

    '                End Try

    '                Dim SumCallTotalOI As Double
    '                SumCallTotalOI = Val(dtMarketTable.Compute("sum(CallOI)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' ") & "")
    '                If Not SumCallTotalOI = 0 Then
    '                    dr1("CallOIPer") = Math.Round(dr1("CallOI") / SumCallTotalOI, RoundOpenInt)   'Total OI(%)(Call)
    '                End If

    '                Dim SumPutTotalOI As Double
    '                SumPutTotalOI = Val(dtMarketTable.Compute("sum(PutOI)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' ") & "")
    '                If Not SumPutTotalOI = 0 Then
    '                    dr1("PutOIPer") = Math.Round(dr1("PutOI") / SumPutTotalOI, RoundOpenInt)   'Total OI(%)(Put)
    '                End If
    '                'End If


    '                dr1("CallBullSpread") = Math.Round(dr1("CE") - nextLTP, RoundBullSpread)     'Call Bull Spread
    '                dr1("PutBearSpread") = Math.Round(nextLTP_Put - dr1("PE"), RoundBearSpread)  'Put Bear Spread

    '                Dim nextCallVol, nextCallVega, nextPutVol, nextPutVega As Double
    '                Dim nextLTPCall, nextLTPPut, nextStrike As Double
    '                nextStrike = Get_NextStrike(stkprice, mdate, strCompany, 1)

    '                If boolIsCurrency Then
    '                    nextLTPCall = Math.Round(Val(Currltpprice(CLng(dr1("CallNextToken")))), RoundCurrencyLTP)
    '                    nextLTPPut = Math.Round(Val(Currltpprice(CLng(dr1("PutNextToken")))), RoundCurrencyLTP)
    '                Else
    '                    nextLTPCall = Math.Round(Val(ltpprice(CLng(dr1("CallNextToken")))), Roundltp)
    '                    nextLTPPut = Math.Round(Val(ltpprice(CLng(dr1("PutNextToken")))), Roundltp)
    '                End If

    '                'Write_Log2("Step2B.2: Calculate nextCallVol,nextCallVega Start..")
    '                nextCallVol = Greeks.Black_Scholes(futval, nextStrike, Mrateofinterast, 0, nextLTPCall, _mt1, True, False, 0, 6)
    '                nextCallVega = Math.Round((Greeks.Black_Scholes(futval, nextStrike, Mrateofinterast, 0, nextCallVol, _mt, True, False, 0, 3)), roundVega)

    '                nextPutVol = Greeks.Black_Scholes(futval, nextStrike, Mrateofinterast, 0, nextLTPPut, _mt1, False, False, 0, 6)
    '                nextPutVega = Math.Round((Greeks.Black_Scholes(futval, nextStrike, Mrateofinterast, 0, nextPutVol, _mt, False, False, 0, 3)), roundVega)

    '                'nextCallVol = Val(dtMarketTable.Compute("max(CallVol)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND  strike1 = '" & dr1("strike1") + dGap & "'") & "")
    '                'nextCallVega = Val(dtMarketTable.Compute("max(CallVega)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND  strike1 = '" & dr1("strike1") + dGap & "'") & "")
    '                'nextPutVol = Val(dtMarketTable.Compute("max(PutVol)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND  strike1 = '" & dr1("strike1") + dGap & "'") & "")
    '                'nextPutVega = Val(dtMarketTable.Compute("max(PutVega)", "Symbol ='" & strCompany & "' AND Maturity='" & mdate & "' AND  strike1 = '" & dr1("strike1") + dGap & "'") & "")

    '                dr1("C2C") = Math.Round((dr1("CallVol") - (nextCallVol * 100)) * avg(dr1("CallVega"), nextCallVega), RoundC2C)    'C2C
    '                dr1("P2P") = Math.Round((dr1("PutVol") - (nextPutVol * 100)) * avg(dr1("PutVega"), nextPutVega), RoundP2P)        'P2Ps
    '                dr1("C2P") = Math.Round((dr1("PutVol") - (nextCallVol * 100)) * avg(nextCallVega, dr1("PutVega")), RoundC2P)      'C2P								
    '                'Write_Log2("Step2B.2: Calculate nextCallVol,nextCallVega Stop..")
    '                Dim NextMonthToken As Long
    '                Dim NextMonthLTP As Double
    '                'Write_Log2("Step2B.2: Calculate Call Calender,Put Calender Start..")
    '                'Call Calender


    '                'If stkprice = 8000 Then
    '                '    Dim d As Int16 = 0
    '                'End If

    '                If IsDBNull(dtcpfmaster.Compute("max(Token)", "Symbol ='" & strCompany & "' AND expdate1='" & nextmonth & "' AND Option_Type = 'CE' AND strike_price = '" & dr1("strike1") & "'")) Then
    '                    NextMonthToken = 0
    '                Else
    '                    NextMonthToken = dtcpfmaster.Compute("max(Token)", "Symbol ='" & strCompany & "' AND expdate1='" & nextmonth & "' AND Option_Type = 'CE' AND strike_price = '" & dr1("strike1") & "'")
    '                    If boolIsCurrency Then
    '                        If Currltpprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                            NextMonthLTP = Val(Currltpprice(NextMonthToken))   'Next Month Strikeprice
    '                            dr1("CallCalender") = Math.Round(NextMonthLTP - dr1("CE"), RoundCalender)  'Call Calender
    '                        End If
    '                    Else
    '                        If ltpprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                            NextMonthLTP = Val(ltpprice(NextMonthToken))   'Next Month Strikeprice
    '                            dr1("CallCalender") = Math.Round(NextMonthLTP - dr1("CE"), RoundCalender)  'Call Calender
    '                        End If
    '                    End If
    '                End If

    '                'Put Calender
    '                If IsDBNull(dtcpfmaster.Compute("max(Token)", "Symbol ='" & strCompany & "' AND expdate1='" & nextmonth & "' AND Option_Type = 'PE' AND strike_price = '" & dr1("strike1") & "'")) Then
    '                    NextMonthToken = 0
    '                Else
    '                    NextMonthToken = dtcpfmaster.Compute("max(Token)", "Symbol ='" & strCompany & "' AND expdate1='" & nextmonth & "' AND Option_Type = 'PE' AND strike_price = '" & dr1("strike1") & "'")
    '                    If boolIsCurrency Then
    '                        If Currltpprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                            NextMonthLTP = Val(Currltpprice(NextMonthToken)) 'Next Month Strikeprice
    '                            dr1("PutCalender") = Math.Round(NextMonthLTP - dr1("PE"), RoundCalender)  'Put Calender
    '                        End If
    '                    Else
    '                        If ltpprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                            NextMonthLTP = Val(ltpprice(NextMonthToken)) 'Next Month Strikeprice
    '                            dr1("PutCalender") = Math.Round(NextMonthLTP - dr1("PE"), RoundCalender)  'Put Calender
    '                        End If
    '                    End If
    '                End If
    '            Next
    '            'Write_Log2("Step2B.2: Calculate Call Calender,Put Calender Stop..")
    '            ''On Error Resume Next
    '            ''For Each dcol As DataColumn In dtMarketTable.Columns
    '            ''    Dim max As Double = dtMarketTable.Compute("max(" & dcol.ColumnName & ")", "")
    '            ''    Dim min As Double = dtMarketTable.Compute("min(" & dcol.ColumnName & ")", "")
    '            ''    dgv_Exp1.Columns("col" & dcol.ColumnName).Tag = max.ToString & "," & min.ToString
    '            ''    dgv_Exp1.Columns("col" & dcol.ColumnName).ToolTipText = max.ToString & "," & min.ToString
    '            ''Next

    '        Catch ex As Exception
    '            'Write_Log2("Error: Cal_Greeks Process Stop..")
    '            WriteLogMarketwatchlog("Error In Cal_Greeks Calculation" + ex.ToString())
    '        End Try
    '        'Write_Log2("Step2B: Cal_Greeks Process Stop..")
    '    End Sub


    '    Private Sub Refresh_Data(ByVal isWeekly As Boolean, ByVal IsOnLoad As Boolean)
    'Reload:
    '        If IsOnLoad Then
    '            'Viral 10-10-2018
    '            If (((flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API") Or NetMode = "TCP") Then

    '                Objsql.DeleteFoToken()

    '            End If
    '        End If

    '        'While (flg_Greek_Calculations)
    '        '    Thread.Sleep(1000)
    '        'End While

    '        Try


    '            WriteLogMarketwatchlog("Volwatch Refresh Data Start..")
    '            'flgrefreshdata = True

    '            'Write_Log2("Step2A:MarketWatch Refresh_Data Process Start..")
    '            'strCompany = cmbCompany.Text
    '            'If strCompany.Contains("INR") Then
    '            '    boolIsCurrency = True
    '            '    tmpCpfmaster = Currencymaster
    '            '    strInstrument = "'FUTCUR'"
    '            'Else
    '            '    boolIsCurrency = False
    '            '    tmpCpfmaster = cpfmaster
    '            '    strInstrument = "'FUTSTK','FUTIDX','FUTIVX'"
    '            'End If
    '            ' and (Strike_Price/100)% 1 = 0
    '            'If strCompany = "NIFTY" And chkSkip.Checked = True Then
    '            '    'If tmpCpfmaster.Columns.Contains("StrikeMod") = False Then
    '            '    '    tmpCpfmaster.Columns.Add("StrikeMod", GetType(Double))
    '            '    'End If


    '            '    'For Each dr As DataRow In tmpCpfmaster.Select("Symbol='" & strCompany & "'")
    '            '    '    If dr("InstrumentName").ToString() = "FUTIDX" Then
    '            '    '        dr("StrikeMod") = 0
    '            '    '    Else
    '            '    '        dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
    '            '    '    End If
    '            '    'Next

    '            '    dtcpfmaster = New DataView(tmpCpfmaster, "Symbol='" & strCompany & "' And StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
    '            'Else
    '            '    dtcpfmaster = New DataView(tmpCpfmaster, "Symbol='" & strCompany & "'", "", DataViewRowState.CurrentRows).ToTable()
    '            'End If

    '            Dim tok As Long = 0
    '            If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") And RBcalVol.Checked = True Then

    '                Dim strike As Double
    '                If strCompany = "NIFTY" Then
    '                    strike = Val(eIdxprice("Nifty 50"))
    '                ElseIf strCompany = "BANKNIFTY" Then
    '                    strike = Val(eIdxprice("Nifty Bank"))

    '                ElseIf strCompany = "FINNIFTY" Then
    '                    strike = Val(eIdxprice("NiftyFinService"))
    '                End If

    '                Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & strike & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '                If drmonth2 Is Nothing Then
    '                    Return
    '                End If
    '                If drmonth2.Rows.Count <= 0 Then
    '                    Return
    '                End If

    '                tok = drmonth2.Rows(0)("token")


    '            End If
    '            Dim dt As New DataTable
    '            dt = dtcpfmaster.Clone
    '            dt.AcceptChanges()
    '            Dim drmonth() As DataRow
    '            Dim drmonth22 As DataTable
    '            drmonth = dtcpfmaster.Select("Symbol='" & strCompany & "' AND  InstrumentName IN (" & strInstrument & ")", "expdate1")
    '            Dim DtFMonthDatesyn As DataTable
    '            If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "USDINR") Then
    '                DtFMonthDatesyn = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName IN (" & strInstrument & ")", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token", "ftoken")
    '            Else
    '                DtFMonthDatesyn = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName IN (" & strInstrument & ")", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '            End If



    '            'drmonth22 = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ")", DataViewRowState.CurrentRows).to
    '            If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "USDINR") Then
    '                drmonth22 = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token", "ftoken")
    '                drmonth = drmonth22.Select()
    '            End If

    '            For Each dataRow As DataRow In drmonth
    '                dt.ImportRow(dataRow)
    '            Next


    '            'DtFMonthDate = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName IN (" & strInstrument & ")", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '            If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "USDINR") Then
    '                DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "ftoken")
    '            Else
    '                DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '            End If

    '            'To Search Future Tokens of all 3 months

    '            'To set Expiry Date on headers

    '            If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                Dim strike As Double
    '                Dim Fltpsyn As Long
    '                If RBcalVol.Checked = True Then
    '                    If strCompany = "NIFTY" Then
    '                        strike = Val(eIdxprice("Nifty 50"))
    '                    ElseIf strCompany = "BANKNIFTY" Then
    '                        strike = Val(eIdxprice("Nifty Bank"))
    '                    ElseIf strCompany = "FINNIFTY" Then
    '                        strike = Val(eIdxprice("NiftyFinService"))
    '                    End If
    '                Else
    '                    'If boolIsCurrency Then
    '                    '    Fltpsyn = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

    '                    'Else
    '                    'Fltpsyn = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
    '                    'End If
    '                    'strike = get_SFut(strCompany, DtFMonthDatesyn.Rows(0)("expdate1"), Convert.ToDouble(Fltpsyn))
    '                    'If boolIsCurrency Then
    '                    '        Fltpsyn = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

    '                    '    Else
    '                    Fltpsyn = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
    '                    WriteLogMarketwatchlog("Fltpsyn=" + Fltpsyn.ToString())
    '                End If
    '                'If boolIsCurrency = True Then
    '                '        strike = get_SFutCurr(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltpsyn))
    '                '    Else
    '                strike = Val(hashsyn(DtFMonthDate.Rows(0)("expdate1") + "_" + strCompany) & "") ' get_SFut(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltpsyn))
    '                'End If

    '                WriteLogMarketwatchlog("fltpprice=" + fltpprice.Count.ToString() + "strikeFltpsyn=" + strike.ToString() + strCompany.ToString() + DtFMonthDate.Rows(0)("expdate1").ToString() + Fltpsyn.ToString)
    '                'End If

    '                If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "USDINR") Then
    '                    DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "ftoken")
    '                    'WriteLogMarketwatchlog("DtFMonthDate=" + DtFMonthDate.Rows.Count.ToString())
    '                Else
    '                    DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '                End If
    '                ' DtFMonthDate = New DataView(dt, "", "", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

    '                Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & strike & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '                If drmonth2 Is Nothing Then
    '                    '  MessageBox.Show("1")
    '                    Return
    '                End If
    '                If drmonth2.Rows.Count <= 0 Then
    '                    '    MessageBox.Show("2,drmonth2=" + drmonth2.Rows.Count.ToString() + strCompany.ToString() + strInstrument.ToString () + strike.ToString ())
    '                    Return
    '                End If
    '                Try


    '                    tok = drmonth2.Rows(0)("token")

    '                Catch ex As Exception

    '                End Try
    '            End If
    '            WriteLogMarketwatchlog("volwatch All  DataTable Clear..")
    '            If Fltp1 = 0 Then
    '                dtMarketTable1.Clear()
    '            End If
    '            If Fltp2 = 0 Then
    '                dtMarketTable2.Clear()
    '            End If

    '            If Fltp3 = 0 Then
    '                dtMarketTable3.Clear()
    '            End If
    '            If cmbexpiry.Text = "Current Month" Then
    '                WriteLogMarketwatchlog("volwatch Current Month  Grid Data Start..")

    '                If Fltp1 = 0 And DtFMonthDate.Rows.Count >= 1 Then

    '                    'While (IsProcess)
    '                    '    Thread.Sleep(10)
    '                    'End While
    '                    dtMarketTable1.Clear()
    '                    'If boolIsCurrency Then
    '                    '    DtUTokens1 = ObjTrading.UpperToken_Script_Curr(CDate(DtFMonthDate.Rows(0)("expdate1")), strCompany)
    '                    'Else
    '                    Dim dtUpr As New DataTable
    '                    dtUpr = ObjTrading.UpperToken_Script(CDate(DtFMonthDate.Rows(0)("expdate1")), strCompany)

    '                    If strCompany = "NIFTY" And chkSkip.Checked = True Then
    '                        If dtUpr.Columns.Contains("StrikeMod") = False Then
    '                            dtUpr.Columns.Add("StrikeMod", GetType(Double))
    '                        End If
    '                        For Each dr As DataRow In dtUpr.Select()
    '                            'If dr("InstrumentName").ToString() = "FUTIDX" Then
    '                            'dr("StrikeMod") = 0
    '                            'Else
    '                            'dr("StrikeModdr") = (dr("Strike_Price") / 100) Mod 1
    '                            dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
    '                            'End If
    '                        Next
    '                        DtUTokens1 = New DataView(dtUpr, "StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
    '                    Else
    '                        DtUTokens1 = dtUpr
    '                    End If
    '                    'End If
    '                    If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                        Dim index1 As Double
    '                        If RBcalVol.Checked = True Then
    '                            If strCompany = "NIFTY" Then
    '                                index1 = Val(eIdxprice("Nifty 50"))
    '                            ElseIf strCompany = "BANKNIFTY" Then
    '                                index1 = Val(eIdxprice("Nifty Bank"))
    '                            ElseIf strCompany = "FINNIFTY" Then

    '                                index1 = Val(eIdxprice("NiftyFinService"))
    '                            End If
    '                        Else
    '                            'If boolIsCurrency Then
    '                            '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

    '                            'Else
    '                            '    Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
    '                            'End If
    '                            'index1 = get_SFut(strCompany, DtFMonthDatesyn.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                            'If boolIsCurrency Then
    '                            '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

    '                            'Else
    '                            Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
    '                            'End If
    '                            'If boolIsCurrency = True Then
    '                            '    index1 = get_SFutCurr(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                            'Else
    '                            index1 = Val(hashsyn(DtFMonthDate.Rows(0)("expdate1") + "_" + strCompany) & "") 'get_SFut(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                            'End If


    '                        End If
    '                        Dim mT As Double
    '                        If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                            Dim BCast1 As Date
    '                            'If boolIsCurrency Then
    '                            '    BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                            'Else
    '                            BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                            'End If


    '                            mT = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()).Date)
    '                        Else
    '                            mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()).Date)
    '                        End If


    '                        Dim index As Double = (((index1 * Rateofinterest) / 365) * mT) + index1
    '                        Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

    '                        tok = drmonth2.Rows(0)("token")
    '                        If NetMode = "TCP" Or NetMode = "JL" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(tok).ToString)
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(tok).ToString)
    '                            'End If
    '                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(tok).ToString)
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(tok).ToString)
    '                            'End If
    '                        ElseIf NetMode = "API" Then
    '                            'If boolIsCurrency = False Then
    '                            FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(tok)))
    '                            'End If
    '                        End If
    '                    Else

    '                        If NetMode = "TCP" Or NetMode = "JL" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(0)(1) & "").ToString)
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(0)(1) & "").ToString)

    '                            'End If
    '                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(0)(1) & "").ToString)
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(0)(1) & "").ToString)

    '                            'End If
    '                        ElseIf NetMode = "API" Then
    '                            'If boolIsCurrency = False Then
    '                            FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(DtFMonthDate.Rows(0)(1) & "")))
    '                            'End If

    '                        End If
    '                    End If


    '                    'dgv_Exp1.Sort(dgv_Exp1.Columns("col1Strike1"), System.ComponentModel.ListSortDirection.Ascending)
    '                    If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                        Dim index1 As Double
    '                        If RBcalVol.Checked = True Then
    '                            If strCompany = "NIFTY" Then
    '                                index1 = Val(eIdxprice("` 50"))
    '                            ElseIf strCompany = "BANKNIFTY" Then
    '                                index1 = Val(eIdxprice("Nifty Bank"))
    '                            ElseIf strCompany = "FINNIFTY" Then

    '                                index1 = Val(eIdxprice("NiftyFinService"))
    '                            End If
    '                        Else
    '                            'If boolIsCurrency Then
    '                            '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

    '                            'Else
    '                            '    Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
    '                            'End If
    '                            'index1 = get_SFut(strCompany, DtFMonthDatesyn.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                            'If boolIsCurrency Then
    '                            '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))

    '                            'Else
    '                            Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(0)("token"))))
    '                            'End If
    '                            'If boolIsCurrency = True Then
    '                            '    index1 = get_SFutCurr(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                            'Else
    '                            index1 = Val(hashsyn(DtFMonthDate.Rows(0)("expdate1") + "_" + strCompany) & "") ' get_SFut(strCompany, DtFMonthDate.Rows(0)("expdate1"), Convert.ToDouble(Fltp1))
    '                            'End If


    '                        End If

    '                        Dim mT As Double
    '                        If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                            Dim BCast1 As Date
    '                            'If boolIsCurrency Then
    '                            '    BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                            'Else
    '                            BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                            'End If

    '                            mT = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()).Date)
    '                        Else
    '                            mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()).Date)
    '                        End If

    '                        Dim index As Double = (((index1 * Rateofinterest) / 365) * mT) + index1
    '                        Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

    '                        tok = drmonth2.Rows(0)("token")
    '                        search_fields(dtMarketTable1, DtUTokens1, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()), Val(CLng(tok) & ""), MidStrike1, isWeekly, index)
    '                        WriteLogMarketwatchlog("dtMarketTable1 data=" + dtMarketTable1.Rows.Count.ToString())
    '                        'If boolIsCurrency Then
    '                        '    Fltp1 = Val(Currfltpprice(CLng(tok)))
    '                        'Else
    '                        Fltp1 = Val(fltpprice(CLng(tok)))
    '                        'End If
    '                    Else
    '                        search_fields(dtMarketTable1, DtUTokens1, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()), Val(DtFMonthDate.Rows(0)(1) & ""), MidStrike1, isWeekly, (CLng(tok)))
    '                        WriteLogMarketwatchlog("dtMarketTable1 data=" + dtMarketTable1.Rows.Count.ToString())
    '                        'If boolIsCurrency Then
    '                        '    Fltp1 = Val(Currfltpprice(CLng(tok)))

    '                        'Else
    '                        Fltp1 = Val(fltpprice(CLng(tok)))
    '                        'End If
    '                        '  Dim dd As Double = get_SFut(strCompany, CDate(DtFMonthDate.Rows(0)("expdate1").ToString()), Convert.ToDouble(Fltp1))
    '                    End If


    '                    'Header1 = Header1 & "       FUTURE LTP : " & Fltp1
    '                    'collapsExpandPanel1.HeaderText = Header1
    '                End If

    '                grvvolwatch.DataSource = dtMarketTable1

    '                WriteLogMarketwatchlog("MarketWatch First Grid Data Stop..")
    '            End If
    '            If cmbexpiry.Text = "Next Month" Then

    '                WriteLogMarketwatchlog("MarketWatch Second Grid Data Start..")
    '                If Fltp2 = 0 And DtFMonthDate.Rows.Count >= 2 Then
    '                    'While (IsProcess)
    '                    '    Thread.Sleep(10)
    '                    'End While
    '                    dtMarketTable2.Clear()
    '                    'If boolIsCurrency Then
    '                    '    DtUTokens2 = ObjTrading.UpperToken_Script_Curr(CDate(DtFMonthDate.Rows(1)("expdate1")), strCompany)
    '                    'Else
    '                    Dim dtUpr As New DataTable
    '                        dtUpr = ObjTrading.UpperToken_Script(CDate(DtFMonthDate.Rows(1)("expdate1")), strCompany)

    '                        If strCompany = "NIFTY" And chkSkip.Checked = True Then
    '                            If dtUpr.Columns.Contains("StrikeMod") = False Then
    '                                dtUpr.Columns.Add("StrikeMod", GetType(Double))
    '                            End If
    '                            For Each dr As DataRow In dtUpr.Select()
    '                                'If dr("InstrumentName").ToString() = "FUTIDX" Then
    '                                'dr("StrikeMod") = 0
    '                                'Else
    '                                dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
    '                                'End If
    '                            Next
    '                            DtUTokens2 = New DataView(dtUpr, "StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
    '                        Else
    '                            DtUTokens2 = dtUpr
    '                        End If



    '                    'End If
    '                    If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                        Dim index1 As Double
    '                        If RBcalVol.Checked = True Then
    '                            If strCompany = "NIFTY" Then
    '                                index1 = Val(eIdxprice("Nifty 50"))
    '                            ElseIf strCompany = "BANKNIFTY" Then
    '                                index1 = Val(eIdxprice("Nifty Bank"))
    '                            ElseIf strCompany = "FINNIFTY" Then

    '                                index1 = Val(eIdxprice("NiftyFinService"))
    '                            End If
    '                        Else
    '                            'DtFMonthDate
    '                            'If boolIsCurrency Then
    '                            '    Fltp2 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(1)("token"))))

    '                            'Else
    '                            Fltp2 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(1)("token"))))
    '                            'End If
    '                            'If boolIsCurrency Then
    '                            '    index1 = get_SFutCurr(strCompany, DtFMonthDate.Rows(1)("expdate1"), Convert.ToDouble(Fltp2))
    '                            'Else
    '                            index1 = Val(hashsyn(DtFMonthDate.Rows(1)("expdate1") + "_" + strCompany) & "") ' get_SFut(strCompany, DtFMonthDate.Rows(1)("expdate1"), Convert.ToDouble(Fltp2))
    '                            'End If

    '                            'If boolIsCurrency Then
    '                            '    Fltp2 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(1)("token"))))

    '                            'Else
    '                            '    Fltp2 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(1)("token"))))
    '                            'End If
    '                            'index1 = get_SFut(strCompany, DtFMonthDatesyn.Rows(1)("expdate1"), Convert.ToDouble(Fltp2))
    '                        End If
    '                        Dim mT As Double
    '                        If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                            Dim BCast1 As Date
    '                            'If boolIsCurrency Then
    '                            '    BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                            'Else
    '                            BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                            'End If

    '                            mT = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()).Date)
    '                        Else
    '                            mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()).Date)
    '                        End If


    '                        Dim index As Double = (((index1 * Rateofinterest) / 365) * mT) + index1
    '                        Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '                        Try
    '                            tok = drmonth2.Rows(0)("token")
    '                        Catch ex As Exception
    '                            tok = 0
    '                        End Try

    '                        If NetMode = "TCP" Or NetMode = "JL" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(tok).ToString)
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(tok).ToString)

    '                            'End If
    '                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(tok).ToString)
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(tok).ToString)

    '                            'End If
    '                        ElseIf NetMode = "API" Then
    '                            'If boolIsCurrency = False Then
    '                            FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(tok)))
    '                            'End If

    '                        End If
    '                        search_fields(dtMarketTable2, DtUTokens2, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()), Val(tok & ""), MidStrike2, isWeekly, index)
    '                        WriteLogMarketwatchlog("dtMarketTable2 data=" + dtMarketTable2.Rows.Count.ToString())
    '                        'dgv_exp2.Sort(dgv_exp2.Columns("col2Strike1"), System.ComponentModel.ListSortDirection.Ascending)
    '                        'If boolIsCurrency Then
    '                        '    Fltp2 = Val(Currfltpprice(CLng(tok)))
    '                        'Else
    '                        Fltp2 = Val(fltpprice(CLng(tok)))
    '                        'End If

    '                        'Header2 = Header2 & "       FUTURE LTP : " & Fltp2
    '                        'collapsExpandPanel2.HeaderText = Header2

    '                    Else
    '                        If NetMode = "TCP" Or NetMode = "JL" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(1)(1) & "").ToString)
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(1)(1) & "").ToString)

    '                            'End If
    '                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(1)(1) & "").ToString)
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(1)(1) & "").ToString)

    '                            'End If
    '                        ElseIf NetMode = "API" Then
    '                            'If boolIsCurrency = False Then
    '                            FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(DtFMonthDate.Rows(1)(1) & "")))
    '                            'End If
    '                        End If
    '                        search_fields(dtMarketTable2, DtUTokens2, CDate(DtFMonthDate.Rows(1)("expdate1").ToString()), Val(DtFMonthDate.Rows(1)(1) & ""), MidStrike2, isWeekly, 0)
    '                        WriteLogMarketwatchlog("dtMarketTable2 data=" + dtMarketTable2.Rows.Count.ToString())
    '                        'dgv_exp2.Sort(dgv_exp2.Columns("col2Strike1"), System.ComponentModel.ListSortDirection.Ascending)
    '                        'If boolIsCurrency Then
    '                        '    Fltp2 = Val(Currfltpprice(CLng(DtFMonthDate.Rows(1)(1))))
    '                        'Else
    '                        Fltp2 = Val(fltpprice(CLng(DtFMonthDate.Rows(1)(1))))
    '                        'End If

    '                        'Header2 = Header2 & "       FUTURE LTP : " & Fltp2
    '                        'collapsExpandPanel2.HeaderText = Header2
    '                    End If

    '                End If

    '                grvvolwatch.DataSource = dtMarketTable2
    '                WriteLogMarketwatchlog("MarketWatch Second Grid Data Stop..")
    '            End If
    '            If cmbexpiry.Text = "Far Month" Then

    '                WriteLogMarketwatchlog("MarketWatch third Grid Data Start..")
    '                If Fltp3 = 0 And DtFMonthDate.Rows.Count >= 3 Then
    '                    'While (IsProcess)
    '                    '    Thread.Sleep(10)
    '                    'End While
    '                    dtMarketTable3.Clear()
    '                    'If boolIsCurrency Then
    '                    '    DtUTokens3 = ObjTrading.UpperToken_Script_Curr(CDate(DtFMonthDate.Rows(2)("expdate1")), strCompany)
    '                    'Else
    '                    Dim dtUpr As New DataTable
    '                        dtUpr = ObjTrading.UpperToken_Script(CDate(DtFMonthDate.Rows(2)("expdate1")), strCompany)
    '                        If strCompany = "NIFTY" And chkSkip.Checked = True Then
    '                            If dtUpr.Columns.Contains("StrikeMod") = False Then
    '                                dtUpr.Columns.Add("StrikeMod", GetType(Double))
    '                            End If
    '                            For Each dr As DataRow In dtUpr.Select()
    '                                'If dr("InstrumentName").ToString() = "FUTIDX" Then
    '                                'dr("StrikeMod") = 0
    '                                'Else
    '                                dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
    '                                'End If
    '                            Next
    '                            DtUTokens3 = New DataView(dtUpr, "StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
    '                        Else
    '                            DtUTokens3 = dtUpr
    '                        End If

    '                    'End If
    '                    'If isWeekly = True And strCompany = "BANKNIFTY" Then
    '                    '    Dim index1 As Double = Val(eIdxprice("Nifty Bank"))
    '                    If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                        Dim index1 As Double
    '                        If RBcalVol.Checked = True Then
    '                            If strCompany = "NIFTY" Then
    '                                index1 = Val(eIdxprice("Nifty 50"))
    '                            ElseIf strCompany = "BANKNIFTY" Then
    '                                index1 = Val(eIdxprice("Nifty Bank"))
    '                            ElseIf strCompany = "FINNIFTY" Then

    '                                index1 = Val(eIdxprice("NiftyFinService"))
    '                            End If
    '                        Else
    '                            'If boolIsCurrency Then
    '                            '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(2)("token"))))

    '                            'Else
    '                            Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(2)("token"))))
    '                            'End If
    '                            Dim dtsyn As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND     expdate1 =#" & fDate(CDate(DtFMonthDate.Rows(2)("expdate1"))) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
    '                            If dtsyn Is Nothing Then
    '                                Return
    '                            End If
    '                            If dtsyn.Rows.Count <= 0 Then
    '                                Return
    '                            End If

    '                            'If boolIsCurrency Then
    '                            '    index1 = get_SFutCurr(strCompany, dtsyn.Rows(2)("expdate1"), Convert.ToDouble(Fltp1))
    '                            'Else
    '                            index1 = Val(hashsyn(dtsyn.Rows(2)("expdate1") + "_" + strCompany) & "") ' get_SFut(strCompany, dtsyn.Rows(2)("expdate1"), Convert.ToDouble(Fltp1))
    '                            'End If

    '                            'If boolIsCurrency Then
    '                            '    Fltp1 = Val(Currfltpprice(CLng(DtFMonthDatesyn.Rows(2)("token"))))

    '                            'Else
    '                            '    Fltp1 = Val(fltpprice(CLng(DtFMonthDatesyn.Rows(2)("token"))))
    '                            'End If
    '                            'index1 = get_SFut(strCompany, DtFMonthDatesyn.Rows(2)("expdate1"), Convert.ToDouble(Fltp1))
    '                        End If


    '                        Dim mT As Double
    '                        If CAL_GREEK_WITH_BCASTDATE = 1 Then
    '                            Dim BCast1 As Date
    '                            'If boolIsCurrency Then
    '                            '    BCast1 = DateAdd(DateInterval.Second, VarCurBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                            'Else
    '                            BCast1 = DateAdd(DateInterval.Second, VarFoBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
    '                            'End If

    '                            mT = UDDateDiff(DateInterval.Day, BCast1, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()).Date)
    '                        Else
    '                            mT = UDDateDiff(DateInterval.Day, Now.Date, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()).Date)
    '                        End If
    '                        Dim index As Double = (((index1 * Rateofinterest) / 365) * mT) + index1
    '                        Dim drmonth2 As DataTable = New DataView(dtcpfmaster, "Symbol='" & strCompany & "' AND  InstrumentName not IN (" & strInstrument & ") and strike_price<=" & index & "  and expdate1 >=#" & fDate(CDate(Date.Now)) & "# ", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")

    '                        tok = drmonth2.Rows(0)("token")
    '                        If NetMode = "TCP" Or NetMode = "JL" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(tok))
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(tok))

    '                            'End If
    '                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(tok))
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(tok))

    '                            'End If
    '                        ElseIf NetMode = "API" Then
    '                            'If boolIsCurrency = False Then
    '                            FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(tok)))
    '                            'End If
    '                        End If
    '                        search_fields(dtMarketTable3, DtUTokens3, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()), Val(tok & ""), MidStrike3, isWeekly, index)
    '                        WriteLogMarketwatchlog("dtMarketTable3 data=" + dtMarketTable3.Rows.Count.ToString())
    '                        'dgv_exp3.Sort(dgv_exp3.Columns("col3Strike1"), System.ComponentModel.ListSortDirection.Ascending)
    '                        'If boolIsCurrency Then
    '                        '    Fltp3 = Val(Currfltpprice(CLng(tok)))
    '                        'Else
    '                        Fltp3 = Val(fltpprice(CLng(tok)))
    '                        'End If

    '                        'Header3 = Header3 & "       FUTURE LTP : " & Fltp3
    '                        'collapsExpandPanel3.HeaderText = Header3
    '                    Else
    '                        If NetMode = "TCP" Or NetMode = "JL" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(2)(1) & "").ToString)
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(2)(1) & "").ToString)

    '                            'End If
    '                        ElseIf (flgAPI_K = "TCP" Or flgAPI_K = "TRUEDATA") And NetMode = "API" Then
    '                            'If boolIsCurrency Then
    '                            '    Objsql.AppendCurTokens(CLng(DtFMonthDate.Rows(2)(1) & "").ToString)
    '                            'Else
    '                            Objsql.AppendFoTokens(CLng(DtFMonthDate.Rows(2)(1) & "").ToString)

    '                            'End If
    '                        ElseIf NetMode = "API" Then
    '                            'If boolIsCurrency = False Then
    '                            FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(DtFMonthDate.Rows(2)(1) & "")))
    '                            'End If
    '                        End If
    '                        search_fields(dtMarketTable3, DtUTokens3, CDate(DtFMonthDate.Rows(2)("expdate1").ToString()), Val(DtFMonthDate.Rows(2)(1) & ""), MidStrike3, isWeekly, 0)
    '                        WriteLogMarketwatchlog("dtMarketTable3 data=" + dtMarketTable3.Rows.Count.ToString())
    '                        'dgv_exp3.Sort(dgv_exp3.Columns("col3Strike1"), System.ComponentModel.ListSortDirection.Ascending)
    '                        'If boolIsCurrency Then
    '                        '    Fltp3 = Val(Currfltpprice(CLng(DtFMonthDate.Rows(2)(1))))
    '                        'Else
    '                        Fltp3 = Val(fltpprice(CLng(DtFMonthDate.Rows(2)(1))))
    '                        'End If

    '                        'Header3 = Header3 & "       FUTURE LTP : " & Fltp3
    '                        'collapsExpandPanel3.HeaderText = Header3
    '                    End If



    '                End If

    '                grvvolwatch.DataSource = dtMarketTable3
    '                WriteLogMarketwatchlog("MarketWatch third Grid Data Stop..")
    '            End If
    '        Catch ex As Exception
    '            WriteLogMarketwatchlog("MarketWatch All Data Fill error..")
    '        End Try
    '        WriteLogMarketwatchlog("MarketWatch All Data Fill..")
    '        'new header Fill mthod
    '        WriteLogMarketwatchlog("MarketWatch Fill Header Data Stop..")
    '        'If isWeekly = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY") Then
    '        '    If boolIsCurrency Then
    '        '        If DtFMonthDate.Rows.Count >= 1 Then
    '        '            Header1 = Header1 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel1.HeaderText = Header1
    '        '        Else
    '        '            collapsExpandPanel1.HeaderText = ""
    '        '        End If

    '        '        If DtFMonthDate.Rows.Count >= 2 Then
    '        '            Header2 = Header2 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel2.HeaderText = Header2
    '        '        Else
    '        '            collapsExpandPanel2.HeaderText = ""
    '        '        End If

    '        '        If DtFMonthDate.Rows.Count >= 3 Then
    '        '            Header3 = Header3 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel3.HeaderText = Header3
    '        '        Else
    '        '            collapsExpandPanel3.HeaderText = ""
    '        '        End If
    '        '    Else
    '        '        If DtFMonthDate.Rows.Count >= 1 Then
    '        '            Header1 = Header1 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel1.HeaderText = Header1
    '        '        Else
    '        '            collapsExpandPanel1.HeaderText = ""
    '        '        End If

    '        '        If DtFMonthDate.Rows.Count >= 2 Then
    '        '            Header2 = Header2 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel2.HeaderText = Header2
    '        '        Else
    '        '            collapsExpandPanel2.HeaderText = ""
    '        '        End If

    '        '        If DtFMonthDate.Rows.Count >= 3 Then
    '        '            Header3 = Header3 & "       [FUTURE LTP : " & Val(Val(eIdxprice(strsymbol))) & "]       [Change in Future: ""]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel3.HeaderText = Header3
    '        '        Else
    '        '            collapsExpandPanel3.HeaderText = ""
    '        '        End If
    '        '    End If
    '        'Else
    '        '    If boolIsCurrency Then
    '        '        If DtFMonthDate.Rows.Count >= 1 Then
    '        '            Header1 = Header1 & "       [FUTURE LTP : " & Val(Currfltpprice(CLng(DtFMonthDate.Rows(0)("token")))) & "]       [Change in Future: " & (Val(Currfltpprice(CLng(DtFMonthDate.Rows(0)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(0)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel1.HeaderText = Header1
    '        '        Else
    '        '            collapsExpandPanel1.HeaderText = ""
    '        '        End If

    '        '        If DtFMonthDate.Rows.Count >= 2 Then
    '        '            Header2 = Header2 & "       [FUTURE LTP : " & Val(Currfltpprice(CLng(DtFMonthDate.Rows(1)("token")))) & "]       [Change in Future: " & (Val(Currfltpprice(CLng(DtFMonthDate.Rows(1)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(1)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel2.HeaderText = Header2
    '        '        Else
    '        '            collapsExpandPanel2.HeaderText = ""
    '        '        End If

    '        '        If DtFMonthDate.Rows.Count >= 3 Then
    '        '            Header3 = Header3 & "       [FUTURE LTP : " & Val(Currfltpprice(CLng(DtFMonthDate.Rows(2)("token")))) & "]       [Change in Future: " & (Val(Currfltpprice(CLng(DtFMonthDate.Rows(2)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(2)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel3.HeaderText = Header3
    '        '        Else
    '        '            collapsExpandPanel3.HeaderText = ""
    '        '        End If
    '        '    Else
    '        '        If DtFMonthDate.Rows.Count >= 1 Then
    '        '            Header1 = Header1 & "       [FUTURE LTP : " & Val(fltpprice(CLng(DtFMonthDate.Rows(0)("token")))) & "]       [Change in Future: " & (Val(fltpprice(CLng(DtFMonthDate.Rows(0)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(0)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel1.HeaderText = Header1
    '        '        Else
    '        '            collapsExpandPanel1.HeaderText = ""
    '        '        End If

    '        '        If DtFMonthDate.Rows.Count >= 2 Then
    '        '            Header2 = Header2 & "       [FUTURE LTP : " & Val(fltpprice(CLng(DtFMonthDate.Rows(1)("token")))) & "]       [Change in Future: " & (Val(fltpprice(CLng(DtFMonthDate.Rows(1)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(1)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel2.HeaderText = Header2
    '        '        Else
    '        '            collapsExpandPanel2.HeaderText = ""
    '        '        End If

    '        '        If DtFMonthDate.Rows.Count >= 3 Then
    '        '            Header3 = Header3 & "       [FUTURE LTP : " & Val(fltpprice(CLng(DtFMonthDate.Rows(2)("token")))) & "]       [Change in Future: " & (Val(fltpprice(CLng(DtFMonthDate.Rows(2)("token")))) - Val(closeprice(CLng(DtFMonthDate.Rows(2)("token"))))).ToString("0.00") & "]      [Last Refresh Time : " & DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy hh:mm:ss") & "]"
    '        '            collapsExpandPanel3.HeaderText = Header3
    '        '        Else
    '        '            collapsExpandPanel3.HeaderText = ""
    '        '        End If
    '        '    End If

    '        'End If

    '        'flgrefreshdata = False

    '        'dgv_Exp1.Sort(dgv_Exp1.Columns("col1Strike1"), System.ComponentModel.ListSortDirection.Ascending)
    '        'dgv_exp2.Sort(dgv_exp2.Columns("col2Strike1"), System.ComponentModel.ListSortDirection.Ascending)
    '        'dgv_exp3.Sort(dgv_exp3.Columns("col3Strike1"), System.ComponentModel.ListSortDirection.Ascending)
    '        'Write_Log2("Step2A:MarketWatch Refresh_Data Process Stop..")
    '        WriteLogMarketwatchlog("MarketWatch Refresh Data Stop..")
    '    End Sub

    '    Public Sub search_fields(ByRef dtMarketTable As DataTable, ByVal dtStrike As DataTable, ByVal Mdate As Date, ByVal mtoken As Long, ByRef MidStrike As Double, ByVal isWeekly As Boolean, ByVal index As Double)
    '        Try


    '            Dim dr1 As DataRow
    '            Dim strike As Double
    '            'Dim MidStrike As Double
    '            Dim CallHashTokens As New Hashtable
    '            Dim PutHashTokens As New Hashtable
    '            If chkIsWeekly.Checked = True And (strCompany = "BANKNIFTY" Or strCompany = "NIFTY" Or strCompany = "FINNIFTY" Or strCompany = "USDINR") Then
    '                'If boolIsCurrency Then
    '                '    If Val(Currfltpprice(CLng(mtoken))) = 0 Then Exit Sub
    '                'Else
    '                '    If Val(fltpprice(CLng(mtoken))) = 0 Then Exit Sub
    '                'End If
    '                'strike = Val(eIdxprice("Nifty Bank"))
    '                If RBcalVol.Checked = True Then
    '                    If strCompany = "NIFTY" Then
    '                        strike = Val(eIdxprice("Nifty 50"))
    '                    ElseIf strCompany = "BANKNIFTY" Then
    '                        strike = Val(eIdxprice("Nifty Bank"))
    '                    ElseIf strCompany = "FINNIFTY" Then
    '                        strike = Val(eIdxprice("NiftyFinService"))

    '                    End If
    '                Else
    '                    If strCompany = "NIFTY" Then
    '                        strike = index 'Val(eIdxprice("Nifty 50"))
    '                    ElseIf strCompany = "BANKNIFTY" Then
    '                        strike = index ' Val(eIdxprice("Nifty Bank"))
    '                    ElseIf strCompany = "FINNIFTY" Then
    '                        strike = index ' Val(eIdxprice("Nifty Bank"))
    '                    Else
    '                        strike = index
    '                    End If
    '                End If
    '                If Val(strike) = 0 Then Exit Sub
    '                'If boolIsCurrency Then
    '                '    Fltp1 = Val(Currfltpprice(CLng(dtStrike.Rows(0)("CE_token"))))
    '                '    Fltp1 = Val(fltpprice(mtoken))
    '                'Else
    '                '    Fltp1 = Val(fltpprice(CLng(dtStrike.Rows(0)("CE_token"))))
    '                '    Fltp1 = Val(fltpprice(mtoken))
    '                'End If
    '                'strike = get_SFut(strCompany, Mdate, Convert.ToDouble(Fltp1))
    '            Else
    '                'If boolIsCurrency Then
    '                '    If Val(Currfltpprice(CLng(mtoken))) = 0 Then Exit Sub
    '                'Else

    '                If NetMode = "API" And flgAPI_K = "TRUEDATA" Then
    '                        FoIdentifierQueue.Enqueue(HT_GetIdentifierFromTokan(CLng(mtoken)))
    '                    End If
    '                    If Val(fltpprice(CLng(mtoken))) = 0 Then Exit Sub
    '                'End If
    '            End If


    '            Dim drow1() As DataRow
    '            drow1 = dtStrike.Select("", "Strike_Price")

    '            For i As Integer = 0 To dtStrike.Select("", "Strike_Price").Length - 1
    '                'If boolIsCurrency Then
    '                '    If (drow1(i)("strike_price") > IIf(isWeekly = True, strike, Val(Currfltpprice(CLng(mtoken))))) Then
    '                '        Dim val1, val2 As Double
    '                '        val1 = Math.Abs(Val(Currfltpprice(CLng(mtoken))) - drow1(i)("strike_price"))
    '                '        If drow1.GetLowerBound(0) > (i - 1) Then
    '                '            val2 = Math.Abs(Val(Currfltpprice(CLng(mtoken))) - drow1(i - 1)("strike_price"))
    '                '        End If
    '                '        If (val1 < val2) Then
    '                '            MidStrike = drow1(i)("strike_price")

    '                '            CallHashTokens.Add(drow1(i)("strike_price"), drow1(i)("CE_Token"))
    '                '            PutHashTokens.Add(drow1(i)("strike_price"), drow1(i)("PE_Token"))
    '                '            For j As Integer = 1 To iUpDownstrike
    '                '                If (i - j) >= drow1.GetLowerBound(0) Then
    '                '                    CallHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("CE_Token"))
    '                '                    PutHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("PE_Token"))
    '                '                End If
    '                '            Next
    '                '            For k As Integer = 1 To iUpDownstrike
    '                '                If (i + k) <= drow1.GetUpperBound(0) Then
    '                '                    CallHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("CE_Token"))
    '                '                    PutHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("PE_Token"))
    '                '                End If
    '                '            Next
    '                '            Exit For
    '                '        Else
    '                '            i = i - 1
    '                '            MidStrike = drow1(i)("strike_price")
    '                '            CallHashTokens.Add(drow1(i)("strike_price"), drow1(i)("CE_Token"))
    '                '            PutHashTokens.Add(drow1(i)("strike_price"), drow1(i)("PE_Token"))
    '                '            For j As Integer = 1 To iUpDownstrike
    '                '                If (i - j) >= drow1.GetLowerBound(0) Then
    '                '                    CallHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("CE_Token"))
    '                '                    PutHashTokens.Add(drow1(i - j)("strike_price"), drow1(i - j)("PE_Token"))
    '                '                End If
    '                '            Next
    '                '            For k As Integer = 1 To iUpDownstrike
    '                '                If (i + k) <= drow1.GetUpperBound(0) Then
    '                '                    CallHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("CE_Token"))
    '                '                    PutHashTokens.Add(drow1(i + k)("strike_price"), drow1(i + k)("PE_Token"))
    '                '                End If
    '                '            Next
    '                '            Exit For
    '                '        End If
    '                '    End If
    '                'Else
    '                If (drow1(i)("strike_price") > IIf(isWeekly, strike, Val(fltpprice(CLng(mtoken))))) Then
    '                        Dim val1, val2 As Double
    '                        val1 = Math.Abs(Val(fltpprice(CLng(mtoken))) - drow1(i)("strike_price"))
    '                        If drow1.GetLowerBound(0) > (i - 1) Then
    '                            val2 = Math.Abs(Val(fltpprice(CLng(mtoken))) - drow1(i - 1)("strike_price"))
    '                        End If

    '                        If (val1 < val2) Then
    '                            MidStrike = drow1(i)("strike_price")

    '                            CallHashTokens.Add(drow1(i)("strike_price"), drow1(i)("CE_Token"))
    '                            PutHashTokens.Add(drow1(i)("strike_price"), drow1(i)("PE_Token"))

    '                        Exit For
    '                        Else
    '                            i = i - 1
    '                            MidStrike = drow1(i)("strike_price")
    '                            CallHashTokens.Add(drow1(i)("strike_price"), drow1(i)("CE_Token"))
    '                            PutHashTokens.Add(drow1(i)("strike_price"), drow1(i)("PE_Token"))

    '                        Exit For
    '                        End If
    '                    End If
    '                'End If
    '            Next

    '            For Each Entry1 As Collections.DictionaryEntry In CallHashTokens

    '                dr1 = dtMarketTable.NewRow

    '                ''to add strike prices
    '                'dr1("strike1") = Entry1.Key
    '                'dr1("strike2") = Entry1.Key
    '                'dr1("strike3") = Entry1.Key
    '                'dr1("CE") = 0
    '                'dr1("PE") = 0
    '                'dr1("futltp") = 0
    '                'dr1("CallOI") = 0
    '                'dr1("PutOI") = 0
    '                'dr1("CallToken") = Entry1.Value
    '                'dr1("PutToken") = PutHashTokens(Entry1.Key)

    '                'dr1("CallPreToken") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, -1, "CE")
    '                'dr1("CallPre2Token") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, -2, "CE")
    '                'dr1("CallNextToken") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, 1, "CE")
    '                'dr1("CallNext2Token") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, 2, "CE")

    '                'dr1("PutPreToken") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, -1, "PE")
    '                'dr1("PutPre2Token") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, -2, "PE")
    '                'dr1("PutNextToken") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, 1, "PE")
    '                'dr1("PutNext2Token") = Get_PreviousStrike(dr1("Strike1"), Mdate, strCompany, 2, "PE")

    '                dr1("Symbol") = strCompany
    '                'dr1("Maturity") = Mdate

    '                'If boolIsCurrency Then
    '                '    If Currltpprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                '        dr1("CE") = Val(Currltpprice(CLng(dr1("CallToken"))))   'Call Strike Price
    '                '    End If

    '                '    If Currltpprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                '        dr1("PE") = Val(Currltpprice(CLng(dr1("PutToken"))))    'Put Strike Price
    '                '    End If

    '                '    If Currfltpprice.Contains(mtoken) Then
    '                '        dr1("futltp") = Val(Currfltpprice(mtoken)) 'Future LTP
    '                '        dblFutPrice1 = dr1("futltp")
    '                '    End If
    '                'Else
    '                If ltpprice.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                        dr1("CE") = Val(ltpprice(CLng(dr1("CallToken"))))   'Call Strike Price
    '                    End If
    '                    If ltpprice.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                        dr1("PE") = Val(ltpprice(CLng(dr1("PutToken"))))    'Put Strike Price
    '                    End If

    '                'Status/StopLoss

    '                'If TrendStopLoss.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                '    dr1("CEStopLoss") = Val(TrendStopLoss(CLng(dr1("CallToken"))))
    '                'End If
    '                'If TrendStopLoss.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                '    dr1("PEStopLoss") = Val(TrendStopLoss(CLng(dr1("PutToken"))))
    '                'End If


    '                'If TrendStatus.Contains(CLng(Val(dr1("CallToken") & ""))) Then
    '                '    dr1("CEStatus") = GetStatus(Val(TrendStatus(CLng(dr1("CallToken")))), Val(TrendStopLoss(CLng(dr1("CallToken")))), Val(ltpprice(CLng(dr1("CallToken")))))
    '                'End If
    '                'If TrendStatus.Contains(CLng(Val(dr1("PutToken") & ""))) Then
    '                '    dr1("PEStatus") = GetStatus(Val(TrendStatus(CLng(dr1("PutToken")))), Val(TrendStopLoss(CLng(dr1("PutToken")))), Val(ltpprice(CLng(dr1("PutToken")))))
    '                'End If
    '                If isWeekly = True Then
    '                        dr1("futltp") = Val(index) 'Future LTP
    '                        dblFutPrice1 = dr1("futltp")
    '                    Else
    '                        If fltpprice.Contains(mtoken) Then
    '                            dr1("futltp") = Val(fltpprice(mtoken)) 'Future LTP
    '                            dblFutPrice1 = dr1("futltp")
    '                        End If
    '                    End If

    '                'End If
    '                dtMarketTable.Rows.Add(dr1)
    '            Next
    '        Catch ex As Exception
    '            MessageBox.Show(ex.ToString())
    '            'WriteLogMarketwatchlog("MarketWatch Search_Fields Error..", ex.ToString())
    '        End Try
    '    End Sub

    '    Private Sub Timersyn_Tick(sender As Object, e As EventArgs) Handles Timersyn.Tick
    '        If flg_calcSynFut = False Then
    '            flg_calcSynFut = True
    '            Dim thrCalcSyncFuture As Threading.Thread
    '            thrCalcSyncFuture = New Thread(AddressOf calcSynFut)
    '            thrCalcSyncFuture.Name = "thrCalcSyncFutureMW"
    '            thrCalcSyncFuture.Start()
    '        End If

    '    End Sub
    '    Private Sub calcSynFut()
    '        'If RBsynfut.Checked = True Then


    '        Dim vv As DataView = New DataView(dtcpfmaster, "symbol='" & strCompany & "'", "expdate1", DataViewRowState.CurrentRows)
    '        Dim dtcount As DataTable = vv.ToTable(True, "expdate1", "symbol", "ftoken")
    '        Dim i As Integer = 0
    '        For Each drow As DataRow In dtcount.Rows
    '            Try
    '                If i > 6 Then
    '                    Exit For
    '                End If
    '                i = i + 1
    '                If hashsyn.Contains(drow("expdate1") & "_" & strCompany) = False Then
    '                    hashsyn.Add(drow("expdate1") & "_" & strCompany, "0")
    '                End If
    '                Dim fltpprsynfut As Double
    '                'If boolIsCurrency = True Then
    '                '    If hashsyn.Contains(drow("expdate1") & "_" & strCompany) Then
    '                '        If Currfltpprice.Contains(CLng(drow("ftoken"))) Then   'select LTP from fltpprice hashtable                                        
    '                '            fltpprsynfut = Val(Currfltpprice(CLng(drow("ftoken"))))

    '                '            hashsyn(drow("expdate1") & "_" & strCompany) = get_SFutCurr(drow("symbol"), CDate(drow("expdate1")), Convert.ToDouble(fltpprsynfut))
    '                '        Else
    '                '            hashsyn(drow("expdate1") & "_" & strCompany) = 0
    '                '        End If
    '                '    End If
    '                'Else

    '                fltpprsynfut = Val(fltpprice(CLng(drow("ftoken"))))

    '                If hashsyn.Contains(drow("expdate1") & "_" & drow("symbol")) Then
    '                    hashsyn(drow("expdate1") & "_" & drow("symbol")) = get_SFut(drow("symbol"), CDate(drow("expdate1")), Convert.ToDouble(fltpprsynfut))

    '                End If
    '                'End If

    '            Catch ex As Exception
    '                hashsyn(drow("expdate1") & "_" & drow("symbol")) = 0
    '            End Try






    '        Next
    '        'End If
    '        flg_calcSynFut = False
    '    End Sub
    '    Private Sub chkSkip_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSkip.CheckedChanged, chkIsWeekly.CheckedChanged
    '        If tmpCpfmaster.Rows.Count > 0 Then
    '            If strCompany = "" Then
    '                strCompany = "NIFTY"
    '            Else
    '                strCompany = cmbCompany.Text
    '            End If

    '            If strCompany = "NIFTY" And chkSkip.Checked = True Then
    '                'If tmpCpfmaster.Columns.Contains("StrikeMod") = False Then
    '                '    tmpCpfmaster.Columns.Add("StrikeMod", GetType(Double))
    '                'End If


    '                'For Each dr As DataRow In tmpCpfmaster.Select("Symbol='" & strCompany & "'")
    '                '    If dr("InstrumentName").ToString() = "FUTIDX" Then
    '                '        dr("StrikeMod") = 0
    '                '    Else
    '                '        dr("StrikeMod") = (dr("Strike_Price") / 100) Mod 1
    '                '    End If
    '                'Next

    '                dtcpfmaster = New DataView(tmpCpfmaster, "Symbol='" & strCompany & "' And StrikeMod = 0", "", DataViewRowState.CurrentRows).ToTable()
    '            Else
    '                dtcpfmaster = New DataView(tmpCpfmaster, "Symbol='" & strCompany & "'", "", DataViewRowState.CurrentRows).ToTable()
    '            End If
    '        End If
    '        Fltp1 = 0
    '        Fltp2 = 0
    '        Fltp3 = 0
    '        If chkIsWeekly.Checked Then
    '            RBsynfut.Visible = True
    '            RBsynfut.Checked = True

    '            RBcalVol.Visible = True
    '            Timersyn.Enabled = True
    '        Else
    '            RBsynfut.Visible = False
    '            RBcalVol.Visible = False
    '            Timersyn.Enabled = False
    '        End If

    '        'If isload = False Then
    '        '    btnLoad_Click(sender, e)
    '        'End If
    '        btnload_Click(sender, e)
    '        Flg_CheckedMonth = True
    '    End Sub
    '    Private Sub ATMHighlighter(ByVal dgv_exp As DataGridView, ByVal dblFutPrice As Double)
    '        Dim intLFutPrice As Integer = Integer.Parse((Math.Round((dblFutPrice / 100), 0) * 100).ToString())
    '        For i As Integer = 0 To dgv_exp.Rows.Count - 2
    '            If Val(dgv_exp.Rows(i).Cells(0).Value.ToString() & "") = intLFutPrice Then
    '                dgv_exp.Rows(i).DefaultCellStyle.ForeColor = Color.DarkOrchid
    '                Dim font As System.Drawing.Font = New Font(FontFamily.GenericSerif, 10, FontStyle.Bold)
    '                dgv_exp.Rows(i).DefaultCellStyle.Font = font
    '                Exit For
    '            End If
    '        Next
    '    End Sub

    '    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
    '        'If Timer4.Enabled = True Then
    '        '    Timer4.Enabled = False
    '        'End If

    '        If Flg_CheckedMonth = True Then
    '            Call btnload_Click(sender, e)
    '        Else
    '            'Timer1Event()
    '        End If
    '    End Sub


    '    Private Sub cmbexpiry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbexpiry.SelectedIndexChanged
    '        Flg_CheckedMonth = True
    '    End Sub
End Class