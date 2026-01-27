Imports System
Imports System.io
Imports System.Data
Imports System.Data.OleDb
Imports Volhedge.OptionG
''' <summary>
''' bhavcopy Class To use Process Bhavcopy And Export To Excel File in Terms of Filter Data Given Parametter
''' </summary>
''' <remarks></remarks>
Public Class SettelmentBhav
    Dim Objbhavcopy As New bhav_copy
    Dim tempdata As DataTable
    Dim Mrateofinterast As Double = Val(IIf(IsDBNull(GdtSettings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, GdtSettings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))

    '    Dim mObjData As New DataAnalysis.AnalysisData
    'Dim obj_get_price As New get_price.get_price("M8HUM1T3Q15R9L")
    Dim objBh As New bhav_copy
    Dim dv As DataView
    Dim syb, expdate, opttype As String
    
    ''' <summary>
    ''' this function calculate volatility by the help of black shole function and returns volatility
    ''' </summary>
    ''' <param name="futval"></param>
    ''' <param name="stkprice"></param>
    ''' <param name="cpprice"></param>
    ''' <param name="_mt"></param>
    ''' <param name="mIsCall"></param>
    ''' <param name="mIsFut"></param>
    ''' <returns>mVolatility</returns>
    ''' <remarks></remarks>
    Private Function Vol(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal _mt As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean) As Double

        Dim tmpcpprice As Double = 0
        Dim mVolatility As Double
        tmpcpprice = cpprice
        'IF MATURITY IS 0 THEN _MT = 0.00001 
        mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
        Return mVolatility
    End Function
    Private Function mnth(ByVal m As String) As Integer
        Select Case m
            Case "JAN"
                Return 1
            Case "FEB"
                Return 2
            Case "MAR"
                Return 3
            Case "APR"
                Return 4
            Case "MAY"
                Return 5
            Case "JUN"
                Return 6
            Case "JUL"
                Return 7
            Case "AUG"
                Return 8
            Case "SEP"
                Return 9
            Case "OCT"
                Return 10
            Case "NOV"
                Return 11
            Case "DEC"
                Return 12
        End Select
    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Dim dtSettelmentBhav As DataTable
        dtSettelmentBhav = Objbhavcopy.Select_SettelBhavCopy(dtpEntryDate.Value.Date)
        dv = New DataView(dtSettelmentBhav, "", "company", DataViewRowState.CurrentRows)
        'dv.RowFilter = " symbol in " & syb & " and exp_date in " & expdate & "  and option_type in " & opttype & " and option_type<>'XX' and entry_date= #" & CDate(cmbentry.SelectedValue) & "#  and vol >=" & Val(txtvol.Text) & " and vol < " & Val(txtvolb.Text) & " and contract >=" & Val(txtcontract.Text) & " and contract <=" & Val(txtcontractb.Text) & " and val_inlakh >=" & Val(txtvalue.Text) & " and val_inlakh <=" & Val(txtvalueb.Text) & ""
        DGSettelPr.DataSource = dv.ToTable() 'dv.ToTable(True, "Script,symbol,ExpiryDate,SETTLE_PR,vol,OptionType,StrikePrice,InstrumentName,TIMESTAMP,futval,mt".Split(","))
        DGSettelPr.Refresh()
    End Sub

    ''' <summary>
    ''' set the VolHedge Icon
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bhavcopy_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Maximized
        'Me.Refresh()
        Me.Icon = My.Resources.volhedge_icon
    End Sub

    ''' <summary>
    ''' set Chk Bhavcopy flage false it means that bhavcopy is close
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bhavcopy_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'chkbhavcopy = False
    End Sub

    ''' <summary>
    ''' call the Fill_Data Function
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bhavcopy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    ''' <summary>
    ''' open File Dialog Box and Down load data from excel file in the Data Grid view
    ''' it checkes it whether it is new bhavcopy and bhavcopyflag it true or false
    ''' it both flag are true then fill data methode call
    ''' which fill data in the Grid View From Excel Format
    ''' and set Bhavcopy Flag False
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        'Dim analysis1 As New bhavcopyprocess

        'analysis1.ShowDialog()

        'If GVarIsNewBhavcopy = True Or BhavCopyFlag = True Then
        '    'fill_data()
        '    BhavCopyFlag = False
        'End If

        If grdtrad.Rows.Count > 0 Then
            Dim savedi As New SaveFileDialog
            savedi.Filter = "Files(*.XLS)|*.XLS"
            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim grd(0) As DataGridView
                grd(0) = grdtrad
                Dim sname(0) As String
                sname(0) = "Profit & Loss summary"

                ''divyesh
                Dim ArrColList As New ArrayList
                ArrColList.Add("scrip")
                ArrColList.Add("buyqty")
                ArrColList.Add("buyrate")
                ArrColList.Add("buyvalue")
                ArrColList.Add("sellqty")
                ArrColList.Add("sellrate")
                ArrColList.Add("sellvalue")
                ArrColList.Add("netqty")
                ArrColList.Add("netrate")
                ArrColList.Add("netvalue")
                ArrColList.Add("ltp")
                ArrColList.Add("grossmtm")
                ArrColList.Add("expense")
                ArrColList.Add("STT")
                ArrColList.Add("netmtm")
                Call exporttoexcel(grd, savedi.FileName, sname, "other", ArrColList)
                'MsgBox("Export Completed.")
                MsgBox("Exported Successfully.")
                OPEN_Export_File(savedi.FileName)
            End If
        End If

    End Sub
    
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Dim dtSetBCopy As New DataTable

        dtSetBCopy.Columns.Add("company", GetType(String))
        dtSetBCopy.Columns.Add("Settel_PR", GetType(Double))
        dtSetBCopy.Columns.Add("ExpiryDate", GetType(String))

        'Dim Mrateofinterast As Double
        'Mrateofinterast = 


        For i As Integer = 0 To DGSettelPr.Rows.Count - 1
            Dim grow As New DataGridViewRow
            grow = DGSettelPr.Rows.Item(i)
            Dim drow As DataRow
            drow = dtSetBCopy.NewRow
            drow("company") = grow.Cells("company").Value
            drow("Settel_PR") = grow.Cells("Settel_PR").Value
            drow("ExpiryDate") = dtpEntryDate.Value.Date

            dtSetBCopy.Rows.Add(drow)
            dtSetBCopy.AcceptChanges()
        Next
        dtSetBCopy.AcceptChanges()
        Dim temptable As New DataTable
        Objbhavcopy.insertSettelBhav(dtSetBCopy, dtpEntryDate.Value.Date)
        'dtSetBCopy 
        temptable = Objbhavcopy.PnLExpirycalc(dtpEntryDate.Value.Date)
        grdtrad.DataSource = temptable
        grdtrad.Refresh()

        txtgprofit.Text = Format(IIf(IsDBNull(temptable.Compute("sum(grossmtm)", "")) = False, temptable.Compute("sum(grossmtm)", ""), 0), "#0.00")
        txtnprofit.Text = Format(IIf(IsDBNull(temptable.Compute("sum(netmtm)", "")) = False, temptable.Compute("sum(netmtm)", ""), 0), "#0.00")
        txtexpense.Text = Format(IIf(IsDBNull(temptable.Compute("sum(expense)", "")) = False, temptable.Compute("sum(expense)", ""), 0), "#0.00")
        txtstt.Text = Format(IIf(IsDBNull(temptable.Compute("sum(STT)", "")) = False, temptable.Compute("sum(STT)", ""), 0), "#0.00")

    End Sub


    Private Sub DGSettelPr_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGSettelPr.CellEndEdit
        'Dim FVal As Double = Val(DataGridView1.Item("futval", e.RowIndex).Value & "")
        'Dim strikePrice As Double = Val(DataGridView1.Item("strike", e.RowIndex).Value & "")
        'Dim SettlePrice As Double = Val(DataGridView1.Item("SETTLE_PR", e.RowIndex).Value & "")
        'Dim Mt As Double = Val(DataGridView1.Item("mt", e.RowIndex).Value & "")
        'Dim isCall As Boolean
        'If DataGridView1.Item("option_type", e.RowIndex).Value & "" = "CE" Then
        '    isCall = True
        'Else
        '    isCall = False
        'End If
        'Dim isFut As Boolean
        'If Microsoft.VisualBasic.Left(DataGridView1.Item("Instrument", e.RowIndex).Value, 3) = "FUT" Then
        '    isFut = True
        'Else
        '    isFut = False
        'End If

        'DataGridView1.Item("Vol1", e.RowIndex).Value = Vol(FVal, strikePrice, SettlePrice, Mt, isCall, isFut) * 100
    End Sub

    Private Sub DGSettelPr_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGSettelPr.CellContentClick

    End Sub

    Private Sub TableLayoutPanel4_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub
End Class
