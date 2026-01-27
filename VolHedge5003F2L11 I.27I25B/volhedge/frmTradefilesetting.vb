Imports VolHedge.DAL
Imports System.Data.OleDb
Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Public Class frmTradefilesetting
    Dim Entryno As Integer
    Dim instrumentname As Integer
    Dim company As Integer
    Dim mdate As Integer
    Dim CP As Integer
    Dim strikerate As Integer
    Dim NNFID As Integer
    Dim CTCLID As Integer
    Dim BUY_Cell As Integer
    Dim QTY As Integer
    Dim Rate As Integer
    Dim entrydate As Integer
    Dim OrderNo As Integer
    Public Shared FormateName As String
    Public Shared Formatetype As String
    Dim selectedProfile As String
    Public DeleteProfileFlag As Boolean
    Dim Select_FisFoTrd As String = "SELECT   QFisFoTrd.company, QFisFoTrd.entryno, QFisFoTrd.buysell, QFisFoTrd.orderno, QFisFoTrd.script, QFisFoTrd.instrumentname, QFisFoTrd.strikerate, QFisFoTrd.cp, QFisFoTrd.mdate, QFisFoTrd.Dealer, QFisFoTrd.entrydate, QFisFoTrd.Qty, QFisFoTrd.Rate, QFisFoTrd.IsLiq, QFisFoTrd.lActivityTime, QFisFoTrd.entry_date, QFisFoTrd.Tot,  QFisFoTrd.FileFlag, Contract.Token AS Tokenno, IIf(([contract].[OScript]<>''),[contract].[Token],0) AS Token1,QFisFoTrd.OrgDealer,0 AS token, Format(QFisFoTrd.mdate,'mm/yyyy') AS mo, (IIf(QFisFoTrd.qty=0,1,QFisFoTrd.qty)*QFisFoTrd.rate) AS tot, CDate(Format([entrydate],'mmm/dd/yyyy')) AS entry_date, (IIf(qty=0,1,qty)*(strikeRate+rate)) AS tot2, IIf((cp='X' Or cp=''),'F',cp) AS cpf, IIf([qty]>0,[qty],0) AS BuyQty, IIf([QFisFoTrd.qty]<0,[QFisFoTrd.qty],0) AS SaleQty, IIf([qty]>0,[QFisFoTrd.qty]*QFisFoTrd.rate,0) AS BuyVal, IIf([QFisFoTrd.qty]<0,[QFisFoTrd.qty]*QFisFoTrd.rate,0) AS SaleVal " &
                          " FROM QFisFoTrd INNER JOIN Contract ON QFisFoTrd.script = Contract.script;"

    Dim Select_Field As String = "select top 1 * from QFisFoTrd;"

    Dim Select_SavedData As String = "select distinct [FormateName] from Trad_columnsetting;"
    Public Sub ShowForm()
        Me.ShowDialog()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim data As DataTable = New DataTable
        'Dim Schema As DataTable = New DataTable
        'Schema = data_access.GetSchema
        'If Schema.Rows.Count > 0 Then
        data.Columns.Add("Name")
        data.Columns.Add("Index")
        'For Each row As DataRow In Schema.Rows
        data.Rows.Add("entryno")
        data.Rows.Add("instrumentname")
        data.Rows.Add("company")
        data.Rows.Add("mdate")
        data.Rows.Add("CP")
        data.Rows.Add("strikerate")
        data.Rows.Add("NNFID")
        data.Rows.Add("CTCLID")
        data.Rows.Add("BUY/Cell")
        data.Rows.Add("QTY")
        data.Rows.Add("Rate")
        data.Rows.Add("entrydate")
        data.Rows.Add("OrderNo")
        'data.Rows.Add(row.Item("COLUMN_NAME"))
        'Next
        'End If
        dgvTradeFile.DataSource = data
        dgvTradeFile.Columns(0).ReadOnly = True

        Dim dttableSavedPos As DataTable = DTSavedPosition()
        Dim dv1 As DataView
        dv1 = New DataView(dttableSavedPos, "", "", DataViewRowState.CurrentRows)
        cmbProfile.DataSource = dv1.ToTable(True, "FormateName")
        cmbProfile.DisplayMember = "FormateName"
        cmbProfile.ValueMember = "FormateName"
        cmbProfile.Refresh()
        If cmbProfile.Text <> "" Then
            dgvTradeFile.DataSource = ""
            dgvTradeFile.DataSource = DTSavedEntry()
            txtFormateName.Text = cmbProfile.Text
        End If
        txtFormateName.Visible = False
        Label1.Visible = False
        cmbtype.SelectedIndex = 0
    End Sub
    Private Function DTFistFoTrd() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = Select_Field
            data_access.cmd_type = CommandType.Text
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Function DTSavedPosition() As DataTable
        Try
            data_access.ParamClear()
            data_access.cmd_type = CommandType.Text
            data_access.Cmd_Text = Select_SavedData
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Private Function DTSavedEntry() As DataTable
        Try
            data_access.ParamClear()
            data_access.AddParam("@ColumnName", OleDbType.VarChar, 255, cmbProfile.Text)
            data_access.Cmd_Text = SP_Select_Position
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Function SavedViewList()
        Try
            If DeleteProfileFlag = True Then
                data_access.Cmd_Text = " drop proc " + selectedProfile.Trim() + ""
            Else
                data_access.Cmd_Text = " drop proc " + txtFormateName.Text.Trim() + ""
            End If

            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure            '        Return True
        Catch ex As Exception
            MsgBox(ex.ToString)
            data_access.cmd_type = CommandType.StoredProcedure
        End Try
    End Function

    Private Function DeletePosition()
        Try
            If DeleteProfileFlag = True Then
                data_access.Cmd_Text = " delete from Trad_ColumnSetting where FormateName= '" + selectedProfile + "'"
            Else
                data_access.Cmd_Text = " delete from Trad_ColumnSetting where FormateName= '" + txtFormateName.Text + "'"
            End If

            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure            '        Return True
        Catch ex As Exception
            MsgBox(ex.ToString)
            data_access.cmd_type = CommandType.StoredProcedure
        End Try
    End Function
    Private Function DeleteImportCustomType()
        Try

            If cmbtype.SelectedItem = "FO" Then
                selectedProfile = "CustomFO_" + cmbProfile.Text
            ElseIf cmbtype.SelectedItem = "EQ" Then
                selectedProfile = "CustomEQ_" + cmbProfile.Text
            Else
                selectedProfile = "CustomCurr_" + cmbProfile.Text


            End If

            If DeleteProfileFlag = True Then
                data_access.Cmd_Text = " delete from Import_Type where Text_Type = '" + selectedProfile + "'"
            Else
                data_access.Cmd_Text = " delete from Import_Type where Text_Type = '" + selectedProfile + "'"
            End If

            data_access.cmd_type = CommandType.Text
            data_access.ExecuteNonQuery()
            data_access.cmd_type = CommandType.StoredProcedure            '        Return True
        Catch ex As Exception
            MsgBox(ex.ToString)
            data_access.cmd_type = CommandType.StoredProcedure
        End Try
    End Function

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click


        If txtFormateName.Text = "" And txtFormateName.Visible = True Then
            txtFormateName.Focus()
        ElseIf cmbProfile.Text = "" And cmbProfile.Visible = True Then
            Return

        End If
        If cmbProfile.Text <> "" And cmbProfile.Visible = True Then
            txtFormateName.Text = cmbProfile.Text
        End If


        SavedViewList()
        DeletePosition()
        DeleteImportCustomType()
        'If SavedViewList.ToString > 0 Then
        '    FormateName = txtFormateName.Text
        'Else
        Dim View As String = ""
        Dim Dt As New DataTable
        Dt.Columns.Add("ColumnName")
        Dt.Columns.Add("Position")
        Dt.Columns.Add("FormatName")
        For cnt As Integer = 0 To dgvTradeFile.Rows.Count - 1
            Dt.Rows.Add(dgvTradeFile.Rows(cnt).Cells.Item(0).FormattedValue.ToString(), dgvTradeFile.Rows(cnt).Cells.Item(1).FormattedValue.ToString(), txtFormateName.Text)
        Next
        'For cnt As Integer = 0 To dgvTradeFile.ColumnCount - 1
        '    Dt.Rows.Add(dgvTradeFile.Columns(cnt).Name, dgvTradeFile.Rows(0).Cells.Item(cnt).FormattedValue.ToString(), txtFormateName.Text)
        'Next
        Dt.AcceptChanges()
        FormateName = txtFormateName.Text
        Formatetype = cmbtype.Text

        Entryno = CUtils.GetCellValueInt(dgvTradeFile, 0, 1)
        instrumentname = CUtils.GetCellValueInt(dgvTradeFile, 1, 1)
        company = CUtils.GetCellValueInt(dgvTradeFile, 2, 1)
        mdate = CUtils.GetCellValueInt(dgvTradeFile, 3, 1)
        CP = CUtils.GetCellValueInt(dgvTradeFile, 4, 1)
        strikerate = CUtils.GetCellValueInt(dgvTradeFile, 5, 1)
        NNFID = CUtils.GetCellValueInt(dgvTradeFile, 6, 1)
        CTCLID = CUtils.GetCellValueInt(dgvTradeFile, 7, 1)
        BUY_Cell = CUtils.GetCellValueInt(dgvTradeFile, 8, 1)
        QTY = CUtils.GetCellValueInt(dgvTradeFile, 9, 1)
        Rate = CUtils.GetCellValueInt(dgvTradeFile, 10, 1)
        entrydate = CUtils.GetCellValueInt(dgvTradeFile, 11, 1)
        OrderNo = CUtils.GetCellValueInt(dgvTradeFile, 12, 1)

        If Entryno = 0 AndAlso
           instrumentname = 0 AndAlso
           company = 0 AndAlso
           mdate = 0 AndAlso
           CP = 0 AndAlso
           strikerate = 0 AndAlso
           NNFID = 0 AndAlso
           CTCLID = 0 AndAlso
           BUY_Cell = 0 AndAlso
           QTY = 0 AndAlso
           Rate = 0 AndAlso
           entrydate = 0 AndAlso
           OrderNo = 0 Then
            MessageBox.Show("Enter Values")
            Return

        End If

        'Entryno = dgvTradeFile.Rows(0).Cells.Item(1).FormattedValue.ToString()
        'instrumentname = dgvTradeFile.Rows(1).Cells.Item(1).FormattedValue.ToString()
        'company = dgvTradeFile.Rows(2).Cells.Item(1).FormattedValue.ToString()
        'mdate = dgvTradeFile.Rows(3).Cells.Item(1).FormattedValue.ToString()
        'CP = dgvTradeFile.Rows(4).Cells.Item(1).FormattedValue.ToString()
        'strikerate = dgvTradeFile.Rows(5).Cells.Item(1).FormattedValue.ToString()
        'NNFID = dgvTradeFile.Rows(6).Cells.Item(1).FormattedValue.ToString() '27
        'CTCLID = dgvTradeFile.Rows(7).Cells.Item(1).FormattedValue.ToString() '12
        'BUY_Cell = dgvTradeFile.Rows(8).Cells.Item(1).FormattedValue.ToString()
        'QTY = dgvTradeFile.Rows(9).Cells.Item(1).FormattedValue.ToString()
        'Rate = dgvTradeFile.Rows(10).Cells.Item(1).FormattedValue.ToString()
        'entrydate = dgvTradeFile.Rows(11).Cells.Item(1).FormattedValue.ToString()
        'OrderNo = dgvTradeFile.Rows(12).Cells.Item(1).FormattedValue.ToString()



        Dim tablename As String
        If cmbtype.Text = "FO" Then
            tablename = "trading"

            If NNFID = 0 Then
                View = "SELECT Val([Field" & Entryno & "]) AS entryno" &
                   ", NotFo.Field" & instrumentname & " AS instrumentname" &
                   ", NotFo.Field" & company & " AS company" &
                   ", NotFo.mdate" &
                   ", IIf(IsNull([Field" & strikerate & "]),'0',[Field" & strikerate & "]) AS strikerate" &
                   ", IIf((Left([Field" & instrumentname & "],3)='FUT'),'F',Left([Field" & CP & "],1)) AS cp" &
                   ", UCase(IIf((Left([Field" & CP & "],1)='C' Or Left([Field" & CP & "],1)='P'),[Field" & instrumentname & "] & '  ' & [Field" & company & "] & '  ' & Format([mdate],'ddmmmyyyy')&'  ' & Format(cdbl([Field" & strikerate & "]),'#0.00') & '  ' & [Field" & CP & "],[Field" & instrumentname & "] & '  ' & [Field" & company & "] & '  ' & Format([mdate],'ddmmmyyyy'))) AS script" &
                   ", [Field" & BUY_Cell & "] AS buysell" &
                   ", Val(IIf((Left([Field" & BUY_Cell & "],1)='B' or Val([Field" & BUY_Cell & "])=1),Val([Field" & QTY & "]),(Val([Field" & QTY & "]))*-1)) AS Qty" &
                   ", Val([Field" & Rate & "]) AS Rate" &
                   ", CDate([Field" & entrydate & "]) AS entrydate" &
                   ", NotFo.Field" & OrderNo & " AS orderno" &
                   ", False AS IsLiq" &
                   ", DateDiff('s',CDate('1-1-1980'),CDate([Field" & entrydate & "])) AS lActivityTime" &
                   ", DateSerial(Year(CDate([Field" & entrydate & "])),Month(CDate([Field" & entrydate & "])),Day(CDate([Field" & entrydate & "]))) " &
                   "AS entry_date" &
                   ", Val(IIf((Val([Field" & BUY_Cell & "])=1),Val([Field" & QTY & "]),(Val([Field" & QTY & "]))*-1))*Val([Field" & Rate & "]) AS Tot" &
                   ", '0' AS Dealer1" &
                   ", '0' AS Dealer" &
                   ", '0' AS OrgDealer" &
                   ", 'NOTICEFOTEXT' AS FileFlag" &
                   "  FROM (SELECT CDate(IIf(len([Field" & mdate & "])=8,Left([Field" & mdate & "],1) & '/' & Mid([Field" & mdate & "],2,3) & '/' & " &
                   "Right([Field" & mdate & "],4),Left([Field" & mdate & "],2) & '/' & Mid([Field" & mdate & "],3,3) & '/' & Right([Field" & mdate & "],4))) AS mdate" &
                   ", * FROM CustomFoTrd )  AS NotFo LEFT JOIN " & tablename & " ON (Val(NotFo.Field" & Entryno & ")=" & tablename & ".entryno) AND (NotFo.Field" & OrderNo & "=" & tablename & ".orderno)" &
                   "WHERE (((" & tablename & ".entryno) Is Null)) and NotFo.Field" & company & " in (select symbol from TblRefreshSymbol);"

            Else

                View = "SELECT Val([Field" & Entryno & "]) AS entryno" &
                       ", NotFo.Field" & instrumentname & " AS instrumentname" &
                       ", NotFo.Field" & company & " AS company" &
                       ", NotFo.mdate" &
                       ", IIf(IsNull([Field" & strikerate & "]),'0',[Field" & strikerate & "]) AS strikerate" &
                       ", IIf((Left([Field" & instrumentname & "],3)='FUT'),'F',Left([Field" & CP & "],1)) AS cp" &
                       ", UCase(IIf((Left([Field" & CP & "],1)='C' Or Left([Field" & CP & "],1)='P'),[Field" & instrumentname & "] & '  ' & [Field" & company & "] & '  ' & Format([mdate],'ddmmmyyyy')&'  ' & Format(cdbl([Field" & strikerate & "]),'#0.00') & '  ' & [Field" & CP & "],[Field" & instrumentname & "] & '  ' & [Field" & company & "] & '  ' & Format([mdate],'ddmmmyyyy'))) AS script" &
                       ", 'NOTIS-' & IIf((Len([Field" & NNFID & "])>12),Left([Field" & NNFID & "],12),[Field" & CTCLID & "]) AS Dealer" &
                       ", [Field" & BUY_Cell & "] AS buysell" &
                       ", Val(IIf((Left([Field" & BUY_Cell & "],1)='B' or Val([Field" & BUY_Cell & "])=1),Val([Field" & QTY & "]),(Val([Field" & QTY & "]))*-1)) AS Qty" &
                       ", Val([Field" & Rate & "]) AS Rate" &
                       ", CDate([Field" & entrydate & "]) AS entrydate" &
                       ", NotFo.Field" & OrderNo & " AS orderno" &
                       ", NotFo.Field" & NNFID & " AS Dealer1" &
                       ", False AS IsLiq" &
                       ", DateDiff('s',CDate('1-1-1980'),CDate([Field" & entrydate & "])) AS lActivityTime" &
                       ", DateSerial(Year(CDate([Field" & entrydate & "])),Month(CDate([Field" & entrydate & "])),Day(CDate([Field" & entrydate & "]))) " &
                       "AS entry_date" &
                       ", Val(IIf((Left([Field" & BUY_Cell & "],1)='B'),Val([Field" & QTY & "]),(Val([Field" & QTY & "]))*-1))*Val([Field" & Rate & "]) AS Tot" &
                       ", 'NOTICEFOTEXT' AS FileFlag" &
                       ", IIf((Len([Field" & NNFID & "])>12),[Field" & CTCLID & "] & Left([Field" & NNFID & "],12),[Field" & CTCLID & "]) AS OrgDealer" &
                       "  FROM (SELECT CDate(IIf(len([Field" & mdate & "])=8,Left([Field" & mdate & "],1) & '/' & Mid([Field" & mdate & "],2,3) & '/' & " &
                       "Right([Field" & mdate & "],4),Left([Field" & mdate & "],2) & '/' & Mid([Field" & mdate & "],3,3) & '/' & Right([Field" & mdate & "],4))) AS mdate" &
                       ", * FROM CustomFoTrd WHERE Field" & NNFID & " is Not Null)  AS NotFo LEFT JOIN " & tablename & " ON (Val(NotFo.Field" & Entryno & ")=" & tablename & ".entryno) AND (NotFo.Field" & OrderNo & "=" & tablename & ".orderno)" &
                       "WHERE (((" & tablename & ".entryno) Is Null)) and NotFo.Field" & company & " in (select symbol from TblRefreshSymbol);"


            End If

            data_access.CreateView(txtFormateName.Text, View)
            Update_DataGrid_summaryColumn_Setting_OnWidthIndex(Dt)
            Dt.Dispose()
            If cmbtype.Text = "FO" Then
                Insert_CustomFile(txtFormateName.Text, "CustomFO_")
            ElseIf cmbtype.Text = "EQ" Then
                Insert_CustomFile(txtFormateName.Text, "CustomEQ_")
            ElseIf cmbtype.Text = "CURR" Then
                Insert_CustomFile(txtFormateName.Text, "CustomCurr_")
            End If


            MessageBox.Show("Position Saved Successfully...")
            cmbProfile.Visible = True
            Label2.Visible = True
            Me.Close()

        ElseIf cmbtype.Text = "EQ" Then
            tablename = "equity_trading"




            View = " SELECT Val([Field" & Entryno & "]) AS entryno" &
                ", OdiEq.Field" & company & " AS company" &
", OdiEq.Field" & CP & " AS cp" &
", [Field" & company & "] & '  ' & [Field" & CP & "] AS Script" &
", 'ODIN-' & [Field" & CTCLID & "] AS Dealer" &
", Val([Field" & BUY_Cell & "]) AS buysell " &
", IIf((Val([Field" & BUY_Cell & "])=1),Val([Field12]),(Val([Field12])*-1)) AS Qty" &
", Val([Field13]) AS Rate " &
", CDate([Field20]) AS entrydate" &
", OdiEq.Field22 AS orderno " &
", Val(IIf((Val([Field" & BUY_Cell & "])=1),Val([Field" & QTY & "]),(Val([Field" & QTY & "])*-1)))*Val([Field" & Rate & "]) AS tot" &
", False AS IsLiq " &
", DateDiff('s',CDate('1-1-1980'),CDate([Field" & entrydate & "])) AS lActivityTime " &
", DateSerial(Year(CDate([Field" & entrydate & "])) ,Month(CDate([Field" & entrydate & "])),Day(CDate([Field" & entrydate & "]))) AS entry_date" &
", 'ODINEQTEXT' AS FileFlag " &
", OdiEq.Field" & CP & " AS eq" &
", [Field" & CTCLID & "] AS OrgDealer" &
                " FROM (SELECT * FROM CustomFoTrd WHERE Field" & CTCLID & " is Not Null)  AS OdiEq LEFT JOIN equity_trading ON (OdiEq.Field" & OrderNo & "=equity_trading.orderno) AND (Val(OdiEq.Field" & Entryno & ")=equity_trading.entryno)" &
                " WHERE equity_trading.entryno Is Null and OdiEq.Field" & company & " in (select symbol from TblRefreshSymbol);"



            data_access.CreateView(txtFormateName.Text, View)
            Update_DataGrid_summaryColumn_Setting_OnWidthIndex(Dt)
            Dt.Dispose()
            If cmbtype.Text = "FO" Then
                Insert_CustomFile(txtFormateName.Text, "CustomFO_")
            ElseIf cmbtype.Text = "EQ" Then
                Insert_CustomFile(txtFormateName.Text, "CustomEQ_")
            ElseIf cmbtype.Text = "CURR" Then
                Insert_CustomFile(txtFormateName.Text, "CustomCurr_")

            End If


            MessageBox.Show("Position Saved Successfully...")
            cmbProfile.Visible = True
            Label2.Visible = True
            Me.Close()



        ElseIf cmbtype.Text = "CURR" Then
            tablename = "Currency_trading"

            If NNFID = 0 Then
                View = "SELECT Val([Field" & Entryno & "]) AS entryno" &
                   ", NotFo.Field" & instrumentname & " AS instrumentname" &
                   ", NotFo.Field" & company & " AS company" &
                   ", NotFo.mdate" &
                   ", IIf(IsNull([Field" & strikerate & "]),'0',[Field" & strikerate & "]) AS strikerate" &
                   ", IIf((Left([Field" & instrumentname & "],3)='FUT'),'F',Left([Field" & CP & "],1)) AS cp" &
                   ", UCase(IIf((Left([Field" & CP & "],1)='C' Or Left([Field" & CP & "],1)='P'),[Field" & instrumentname & "] & '  ' & [Field" & company & "] & '  ' & Format([mdate],'ddmmmyyyy')&'  ' & [Field" & strikerate & "] & '  ' & [Field" & CP & "],[Field" & instrumentname & "] & '  ' & [Field" & company & "] & '  ' & Format([mdate],'ddmmmyyyy'))) AS script" &
                   ", Val([Field" & BUY_Cell & "]) AS buysell" &
                   ", Val(IIf((Val([Field" & BUY_Cell & "])=1),Val([Field" & QTY & "]),(Val([Field" & QTY & "]))*-1)) AS Qty" &
                   ", Val([Field" & Rate & "]) AS Rate" &
                   ", CDate([Field" & entrydate & "]) AS entrydate" &
                   ", NotFo.Field" & OrderNo & " AS orderno" &
                   ", False AS IsLiq" &
                   ", DateDiff('s',CDate('1-1-1980'),CDate([Field" & entrydate & "])) AS lActivityTime" &
                   ", DateSerial(Year(CDate([Field" & entrydate & "])),Month(CDate([Field" & entrydate & "])),Day(CDate([Field" & entrydate & "]))) " &
                   "AS entry_date" &
                   ", Val(IIf((Val([Field" & BUY_Cell & "])=1),Val([Field" & QTY & "]),(Val([Field" & QTY & "]))*-1))*Val([Field" & Rate & "]) AS Tot" &
                   ", '0' AS Dealer1" &
                   ", '0' AS Dealer" &
                   ", '0' AS OrgDealer" &
                   ", 'NOTICEFOTEXT' AS FileFlag" &
                   "  FROM (SELECT CDate(IIf(len([Field" & mdate & "])=8,Left([Field" & mdate & "],1) & '/' & Mid([Field" & mdate & "],2,3) & '/' & " &
                   "Right([Field" & mdate & "],4),Left([Field" & mdate & "],2) & '/' & Mid([Field" & mdate & "],3,3) & '/' & Right([Field" & mdate & "],4))) AS mdate" &
                   ", * FROM CustomFoTrd )  AS NotFo LEFT JOIN " & tablename & " ON (Val(NotFo.Field" & Entryno & ")=" & tablename & ".entryno) AND (NotFo.Field" & OrderNo & "=" & tablename & ".orderno)" &
                   "WHERE (((" & tablename & ".entryno) Is Null)) and NotFo.Field" & company & " in (select symbol from TblRefreshSymbol);"

            Else

                View = "SELECT Val([Field" & Entryno & "]) AS entryno" &
                       ", NotFo.Field" & instrumentname & " AS instrumentname" &
                       ", NotFo.Field" & company & " AS company" &
                       ", NotFo.mdate" &
                       ", IIf(IsNull([Field" & strikerate & "]),'0',[Field" & strikerate & "]) AS strikerate" &
                       ", IIf((Left([Field" & instrumentname & "],3)='FUT'),'F',Left([Field" & CP & "],1)) AS cp" &
                       ", UCase(IIf((Left([Field" & CP & "],1)='C' Or Left([Field" & CP & "],1)='P'),[Field" & instrumentname & "] & '  ' & [Field" & company & "] & '  ' & Format([mdate],'ddmmmyyyy')&'  ' & [Field" & strikerate & "] & '  ' & [Field" & CP & "],[Field" & instrumentname & "] & '  ' & [Field" & company & "] & '  ' & Format([mdate],'ddmmmyyyy'))) AS script" &
                       ", 'NOTIS-' & IIf((Len([Field" & NNFID & "])>12),Left([Field" & NNFID & "],12),[Field" & CTCLID & "]) AS Dealer" &
                       ", Val([Field" & BUY_Cell & "]) AS buysell" &
                       ", Val(IIf((Val([Field" & BUY_Cell & "])=1),Val([Field" & QTY & "]),(Val([Field" & QTY & "]))*-1)) AS Qty" &
                       ", Val([Field" & Rate & "]) AS Rate" &
                       ", CDate([Field" & entrydate & "]) AS entrydate" &
                       ", NotFo.Field" & OrderNo & " AS orderno" &
                       ", NotFo.Field" & NNFID & " AS Dealer1" &
                       ", False AS IsLiq" &
                       ", DateDiff('s',CDate('1-1-1980'),CDate([Field" & entrydate & "])) AS lActivityTime" &
                       ", DateSerial(Year(CDate([Field" & entrydate & "])),Month(CDate([Field" & entrydate & "])),Day(CDate([Field" & entrydate & "]))) " &
                       "AS entry_date" &
                       ", Val(IIf((Val([Field" & BUY_Cell & "])=1),Val([Field" & QTY & "]),(Val([Field" & QTY & "]))*-1))*Val([Field" & Rate & "]) AS Tot" &
                       ", 'NOTICEFOTEXT' AS FileFlag" &
                       ", IIf((Len([Field" & NNFID & "])>12),[Field" & CTCLID & "] & Left([Field" & NNFID & "],12),[Field" & CTCLID & "]) AS OrgDealer" &
                       "  FROM (SELECT CDate(IIf(len([Field" & mdate & "])=8,Left([Field" & mdate & "],1) & '/' & Mid([Field" & mdate & "],2,3) & '/' & " &
                       "Right([Field" & mdate & "],4),Left([Field" & mdate & "],2) & '/' & Mid([Field" & mdate & "],3,3) & '/' & Right([Field" & mdate & "],4))) AS mdate" &
                       ", * FROM CustomFoTrd WHERE Field" & NNFID & " is Not Null)  AS NotFo LEFT JOIN " & tablename & " ON (Val(NotFo.Field" & Entryno & ")=" & tablename & ".entryno) AND (NotFo.Field" & OrderNo & "=" & tablename & ".orderno)" &
                       "WHERE (((" & tablename & ".entryno) Is Null)) and NotFo.Field" & company & " in (select symbol from TblRefreshSymbol);"


            End If

            data_access.CreateView(txtFormateName.Text, View)
            Update_DataGrid_summaryColumn_Setting_OnWidthIndex(Dt)
            Dt.Dispose()
            If cmbtype.Text = "FO" Then
                Insert_CustomFile(txtFormateName.Text, "CustomFO_")
            ElseIf cmbtype.Text = "EQ" Then
                Insert_CustomFile(txtFormateName.Text, "CustomEQ_")
            ElseIf cmbtype.Text = "CURR" Then
                Insert_CustomFile(txtFormateName.Text, "CustomCurr_")
            End If


            MessageBox.Show("Position Saved Successfully...")
            cmbProfile.Visible = True
            Label2.Visible = True
            Me.Close()



            '            SELECT Val([Field1]) AS entryno, OdiEq.Field3 AS company, OdiEq.Field4 AS cp, [Field3] & '  ' & [Field4] AS Script, 'ODIN-' & [Field9] AS Dealer, Val([Field11]) AS buysell, IIf((Val([Field11])=1),Val([Field12]),(Val([Field12])*-1)) AS Qty, Val([Field13]) AS Rate, CDate([Field20]) AS entrydate, OdiEq.Field22 AS orderno, Val(IIf((Val([Field11])=1),Val([Field12]),(Val([Field12])*-1)))*Val([Field13]) AS tot, False AS IsLiq, DateDiff('s',CDate('1-1-1980'),CDate([Field20])) AS lActivityTime, DateSerial(Year(CDate([Field20])),Month(CDate([Field20])),Day(CDate([Field20]))) AS entry_date, 'ODINEQTEXT' AS FileFlag, OdiEq.Field4 AS eq, [Field9] AS OrgDealer
            'FROM (SELECT * FROM OdiEqTrd WHERE Field23 is Not Null)  AS OdiEq LEFT JOIN equity_trading ON (OdiEq.Field22=equity_trading.orderno) AND (Val(OdiEq.Field1)=equity_trading.entryno)
            'WHERE equity_trading.entryno Is Null and OdiEq.Field3 in (select symbol from TblRefreshSymbol);

        End If
        'End If

    End Sub
    Private Const SP_INSERT_Position As String = "insert_Trad_ColumnSetting"
    Private Const SP_INSERT_TradeFile As String = "insert_Import_Type"
    Private Const SP_Select_Position As String = "select_trad_position"
    Private Const SP_Select_View As String = "Select_View"
    Public Sub Update_DataGrid_summaryColumn_Setting_OnWidthIndex(ByVal DtColProfile As DataTable)
        data_access.ParamClear()
        For Each Dr As DataRow In DtColProfile.Rows
            If Dr("columnName").ToString() = "" Then

                'DtColProfile.Rows.Remove(Dr)

            Else
                data_access.AddParam("@ColumnName", OleDbType.VarChar, 255, Dr("ColumnName"))
                data_access.AddParam("@position", OleDbType.Integer, 18, Dr("Position"))
                data_access.AddParam("@FormateName", OleDbType.VarChar, 255, Dr("FormatName"))
            End If

        Next
        data_access.Cmd_Text = SP_INSERT_Position
        data_access.ExecuteMultiple(3)
    End Sub
    Public Sub Insert_CustomFile(ByVal TradeFileName As String, ByVal Text_Type As String)
        data_access.ParamClear()
        data_access.AddParam("@Import_Type", OleDbType.VarChar, 255, "Custom TEXT File")
        data_access.AddParam("@Server_Type", OleDbType.VarChar, 255, "")
        data_access.AddParam("@Text_Type", OleDbType.VarChar, 255, Text_Type + TradeFileName)
        data_access.AddParam("@FileName_Format", OleDbType.VarChar, 255, "MMddTRD.txt")
        data_access.Cmd_Text = SP_INSERT_TradeFile
        data_access.ExecuteNonQuery()
    End Sub

    Public Sub Update_DataGrid()
        data_access.ParamClear()
        data_access.AddParam("@FormateName", OleDbType.VarChar, 255, cmbProfile.Text)
        data_access.Cmd_Text = SP_Select_Position
        data_access.ExecuteMultiple(0)
    End Sub

    Private Sub cmbProfile_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmbProfile.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txtFormateName.Visible = True Then
                btnOk.Text = "Save"
            Else
                btnOk.Text = "Update"
            End If
            dgvTradeFile.DataSource = ""
            dgvTradeFile.DataSource = DTSavedEntry()
            txtFormateName.Text = cmbProfile.Text
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtFormateName.Visible = True
        Label1.Visible = True

        cmbProfile.Visible = False
        Label2.Visible = False
        'Dim data As DataTable = New DataTable
        'data.Columns.Add("Name")
        'data.Columns.Add("Index")

        'data.Rows.Add("entryno")
        'data.Rows.Add("instrumentname")
        'data.Rows.Add("company")
        'data.Rows.Add("mdate")
        'data.Rows.Add("CP")
        'data.Rows.Add("strikerate")
        'data.Rows.Add("NNFID")
        'data.Rows.Add("CTCLID")
        'data.Rows.Add("BUY/Cell")
        'data.Rows.Add("QTY")
        'data.Rows.Add("Rate")
        'data.Rows.Add("entrydate")
        'data.Rows.Add("OrderNo")

        'dgvTradeFile.DataSource = data

        'Dim dv1 As DataView
        'dv1 = New DataView(DTSavedPosition, "", "", DataViewRowState.CurrentRows)
        'cmbProfile.DataSource = dv1.ToTable(True, "FormateName")
        'cmbProfile.DisplayMember = "FormateName"
        'cmbProfile.ValueMember = "FormateName"
        'cmbProfile.Refresh()
        txtFormateName.Text = ""
    End Sub

    Private Sub frmTradefilesetting_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub

    Private Sub frmTradefilesetting_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        If MsgBox("Are you want to Delete?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If

        If cmbProfile.Text <> "" And cmbtype.Text <> "" And cmbProfile.Visible = True Then
            DeleteProfileFlag = True
            selectedProfile = cmbProfile.Text
            DeletePosition()
            DeleteImportCustomType()
            SavedViewList()
            cmbProfile.Refresh()
            Dim dv1 As DataView
            dv1 = New DataView(DTSavedPosition, "", "", DataViewRowState.CurrentRows)
            cmbProfile.DataSource = dv1.ToTable(True, "FormateName")
            cmbProfile.DisplayMember = "FormateName"
            cmbProfile.ValueMember = "FormateName"
            cmbProfile.Refresh()
            cmbProfile.Focus()
        Else
            cmbProfile.Visible = True
            Label2.Visible = True
            txtFormateName.Visible = False
            Label1.Visible = False
        End If
        DeleteProfileFlag = False
    End Sub

    Private Sub cmbProfile_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbProfile.SelectedIndexChanged
        If txtFormateName.Visible = True Then
            btnOk.Text = "Save"
        Else
            btnOk.Text = "Update"
        End If
        dgvTradeFile.DataSource = ""
        dgvTradeFile.DataSource = DTSavedEntry()
        txtFormateName.Text = cmbProfile.Text
    End Sub

    Private Sub cmbtype_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbtype.SelectedIndexChanged
        'dgvTradeFile.DataSource = ""
        'dgvTradeFile.DataSource = DTSavedEntry()
        Dim data As DataTable = New DataTable
        'Dim Schema As DataTable = New DataTable
        'Schema = data_access.GetSchema
        'If Schema.Rows.Count > 0 Then
        data.Columns.Add("Name")
        data.Columns.Add("Index")
        'For Each row As DataRow In Schema.Rows
        data.Rows.Add("entryno")
        data.Rows.Add("instrumentname")
        data.Rows.Add("company")
        data.Rows.Add("mdate")
        data.Rows.Add("CP")
        data.Rows.Add("strikerate")
        data.Rows.Add("NNFID")
        data.Rows.Add("CTCLID")
        data.Rows.Add("BUY/Cell")
        data.Rows.Add("QTY")
        data.Rows.Add("Rate")
        data.Rows.Add("entrydate")
        data.Rows.Add("OrderNo")
        'data.Rows.Add(row.Item("COLUMN_NAME"))
        'Next
        'End If
        dgvTradeFile.DataSource = data
        dgvTradeFile.Columns(0).ReadOnly = True
        txtFormateName.Text = cmbProfile.Text
    End Sub
    Private Sub btnBrowseBackupPath_Click(sender As Object, e As EventArgs) Handles btnBrowseBackupPath.Click
        Dim opfile As OpenFileDialog
        opfile = New OpenFileDialog

        opfile.Filter = "Files(*.csv)|*.csv"


        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtfilepath.Text = opfile.FileName
        End If
    End Sub

    Private Sub btnexport_Click(sender As Object, e As EventArgs) Handles btnexport.Click
        Dim SaveFileDialog As New SaveFileDialog
        SaveFileDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveFileDialog.Filter = "Files (*.csv)|*.csv|All Files (*.*)|*.*"

        If (SaveFileDialog.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK) Then
            Dim FileName As String = SaveFileDialog.FileName
            ' TODO: Add code here to save the current contents of the form to a file.
            Dim dt As DataTable = DTSavedEntry()
            export_template_csv(dt, FileName)

        End If
    End Sub
    Private Sub export_template_csv(ByVal dt As DataTable, ByVal filePath As String)
        Try


            ' Create or overwrite the CSV file
            Using writer As New StreamWriter(filePath)
                ' Write the header row
                For Each column As DataColumn In dt.Columns
                    writer.Write(column.ColumnName)
                    If Not column.Equals(dt.Columns(dt.Columns.Count - 1)) Then
                        writer.Write(",")
                    End If
                Next
                writer.WriteLine()

                ' Write data rows
                For Each row As DataRow In dt.Rows
                    For Each column As DataColumn In dt.Columns
                        writer.Write(row(column).ToString())
                        If Not column.Equals(dt.Columns(dt.Columns.Count - 1)) Then
                            writer.Write(",")
                        End If
                    Next
                    writer.WriteLine()
                Next
            End Using

            MsgBox("Template exported..")
        Catch ex As Exception
            MsgBox("Template not exported..")

        End Try


    End Sub

    Private Sub btnimport_Click(sender As Object, e As EventArgs) Handles btnimport.Click

        Dim isEmpty As Boolean = String.IsNullOrEmpty(txtfilepath.Text.Trim())

        If isEmpty Then
            MessageBox.Show("Please Select File Path")
            txtfilepath.Focus()
            Return
        End If

        'If String.IsNullOrEmpty(txtfilepath.Text) Then Return '
        If Not File.Exists(txtfilepath.Text) Then
            MessageBox.Show("File Not Found")
            txtfilepath.Focus()
            Return
        End If

        dgvTradeFile.DataSource = Nothing

        dgvTradeFile.Rows.Clear()
        dgvTradeFile.Columns.Clear()
        ' Create TextFieldParser to read the CSV file
        Using parser As New TextFieldParser(txtfilepath.Text)
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(",")

            ' Read header row to populate DataGridView columns
            If Not parser.EndOfData Then
                Dim fields As String() = parser.ReadFields()
                For Each field As String In fields
                    dgvTradeFile.Columns.Add(field, field)
                Next
            End If

            ' Read data rows to populate DataGridView rows
            While Not parser.EndOfData
                Dim fields As String() = parser.ReadFields()
                dgvTradeFile.Rows.Add(fields)
            End While
        End Using


        MsgBox("File imported...")
    End Sub
End Class