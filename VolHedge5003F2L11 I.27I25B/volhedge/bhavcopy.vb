Imports System
Imports System.io
Imports System.Data
Imports System.Data.OleDb
Imports VolHedge.OptionG
Imports VolHedge.DAL
''' <summary>
''' bhavcopy Class To use Process Bhavcopy And Export To Excel File in Terms of Filter Data Given Parametter
''' </summary>
''' <remarks></remarks>
Public Class bhavcopy
    Public Shared chkbhavcopy As Boolean
    Dim tempdata As DataTable
    Dim objTrad As trading = New trading
    Dim Mrateofinterast As Double = 0

    '    Dim mObjData As New DataAnalysis.AnalysisData
    'Dim obj_get_price As New get_price.get_price("M8HUM1T3Q15R9L")
    Dim objBh As New bhav_copy
    Dim dv As DataView
    Dim syb, expdate, opttype As String
    Dim downloadbhavcopy As String = ""
    ''' <summary>
    ''' In this Function First check the file path if it is correct format then coy data from excel file
    ''' and load that data to data grid view and in the temp table
    ''' and also store data in the Database after calculation of that data
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub read_file()
        Try
            Dim fpath As String
            fpath = CStr(objTrad.Settings.Compute("max(SettingKey)", "SettingName='BhavCopy'"))
            Mrateofinterast = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
            If fpath <> "" Then
                Dim fi As New FileInfo(fpath)
                Dim dv As DataView
                Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

                Dim objConn As New OleDbConnection(sConnectionString)

                objConn.Open()

                Dim objCmdSelect As New OleDbCommand("SELECT INSTRUMENT,SYMBOL,EXPIRY_DT, STRIKE_PR ,OPTION_TYP,SETTLE_PR,CONTRACTS,VAL_INLAKH,TIMESTAMP FROM " & fi.Name, objConn)

                Dim objAdapter1 As New OleDbDataAdapter

                objAdapter1.SelectCommand = objCmdSelect

                tempdata = New DataTable
                tempdata.Columns.Add("script")
                tempdata.Columns.Add("vol")
                tempdata.Columns.Add("futval")
                tempdata.Columns.Add("mt")
                tempdata.Columns.Add("iscall")
                tempdata.AcceptChanges()
                Dim mt As Double
                Dim futval As Double
                Dim iscall As Boolean
                Dim drow As DataRow
                objAdapter1.Fill(tempdata)
                objConn.Close()
                dv = New DataView(tempdata, " option_typ='XX'", "", DataViewRowState.OriginalRows)
                Dim str(2) As String
                str(0) = "EXPIRY_DT"
                str(1) = "SETTLE_PR"
                str(2) = "SYMBOL"
                Dim tdata As New DataTable
                tdata = dv.ToTable(True, str)

                For Each drow In tempdata.Rows
                    If drow("option_typ") = "XX" Then
                        drow("script") = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(drow("EXPIRY_DT"), "ddMMMyyyy")
                        drow("vol") = 0
                        drow("futval") = 0
                        drow("mt") = 0

                    Else
                        drow("script") = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(drow("EXPIRY_DT"), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")) * 100, "###0.00") & "  " & drow("OPTION_TYP")
                        futval = 0
                        drow("vol") = 0
                        For Each row As DataRow In tdata.Select(" EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' ")
                            futval = row("SETTLE_PR")
                        Next
                        If Mid(drow("OPTION_TYP"), 1, 1) = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        mt = UDDateDiff(DateInterval.Day, Now.Date, CDate(drow("EXPIRY_DT")).Date)
                        If mt = 0 Then
                            mt = 0.0001
                        Else
                            mt = (mt) / 365
                        End If
                        If futval > 0 Then
                            drow("vol") = Vol(futval, Val(drow("STRIKE_PR")), Val(drow("SETTLE_PR")), mt, iscall, True) * 100
                        End If

                        drow("futval") = futval
                        drow("mt") = mt
                        drow("iscall") = iscall

                    End If
                Next
                tempdata.AcceptChanges()
                objBh.insert(tempdata)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
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
    ''' <summary>
    ''' this button click event call read_file function
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdprocess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        read_file()
    End Sub

    ''' <summary>
    ''' this click event filter data by Company name,Expiry Date and Option Type
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        'dv = New DataView(tempdata, "option_type<>'XX' and symbol in " & syb & " and exp_date in " & expdate & "  and option_type in " & opttype & " and entry_date= ' " & CDate(cmbentry.SelectedValue) & "'  and vol >=" & val(txtvol.Text) & " and contract >=" & val(txtcontract.Text) & " and val_inlakh >=" & val(txtvalue.Text) & "", "symbol,exp_date", DataViewRowState.OriginalRows)
        'syb = cmbsymbol.SelectedValue.ToString  and
        If Not syb Is Nothing And Not exp_date Is Nothing And Not opttype Is Nothing Then
            dv = New DataView(GdtBhavcopy, "", "symbol,exp_date", DataViewRowState.OriginalRows)
            dv.RowFilter = " symbol in " & syb & " and exp_date in " & expdate & "  and option_type in " & opttype & " and option_type<>'XX' and entry_date= #" & fDate(CDate(cmbentry.SelectedValue)) & "#  and vol >=" & Val(txtvol.Text) & " and vol < " & Val(txtvolb.Text) & " and contract >=" & Val(txtcontract.Text) & " and contract <=" & Val(txtcontractb.Text) & " and val_inlakh >=" & Val(txtvalue.Text) & " and val_inlakh <=" & Val(txtvalueb.Text) & ""
            DataGridView1.DataSource = dv.ToTable
            DataGridView1.Refresh()
        End If
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
        chkbhavcopy = False
    End Sub

    ''' <summary>
    ''' call the Fill_Data Function
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bhavcopy_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        fill_data()
    End Sub

    ''' <summary>
    ''' call the numonly Function which set only Numeric value from text box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtvol_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtvol.KeyPress
        numonly(e)
    End Sub

    ''' <summary>
    ''' call the numonly Function which set only Numeric value from text box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtcontract_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcontract.KeyPress
        numonly(e)
    End Sub

    ''' <summary>
    ''' ''' call the numonly Function which set only Numeric value from text box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtvalue_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtvalue.KeyPress
        numonly(e)
    End Sub

    ''' <summary>
    ''' this validate even set txtvol text box value zero when it is blank
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtvol_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtvol.Validating
        If txtvol.Text.Trim = "" Then
            txtvol.Text = 0
        End If
    End Sub

    ''' <summary>
    ''' this validate even set txtcontract text box value zero when it is blank
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtcontract_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtcontract.Validating
        If txtcontract.Text.Trim = "" Then
            txtcontract.Text = 0
        End If
    End Sub

    ''' <summary>
    ''' this validate even set txtvalue text box value zero when it is blank
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtvalue_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtvalue.Validating
        If txtvalue.Text.Trim = "" Then
            txtvalue.Text = 0
        End If
    End Sub

    ''' <summary>
    ''' This selection index change event,if any company is select this event fill the Expiry date combo box
    '''  and option type combo box data source
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbsymbol_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbsymbol.SelectedIndexChanged

        If cmbsymbol.Items.Count > 0 And cmbsymbol.SelectedValue.ToString <> "" Then
            'cmbexp.Items.Clear()
            'cmbopt.Items.Clear()
            'cmbentry.Items.Clear()
            syb = "('" & cmbsymbol.SelectedValue.ToString & "' )"
            dv = New DataView(GdtBhavcopy, "option_type<>'XX' and symbol='" & cmbsymbol.SelectedValue.ToString & "'", "symbol", DataViewRowState.OriginalRows)
            cmbexp.DataSource = dv.ToTable(True, "exp_date")
            cmbexp.DisplayMember = "exp_date"
            cmbexp.ValueMember = "exp_date"
            'If cmbexp.Items.Count <= 0 Then
            '    cmbexp.Items.Add("Na")
            'End If
            cmbopt.DataSource = dv.ToTable(True, "option_type")
            cmbopt.DisplayMember = "option_type"
            cmbopt.ValueMember = "option_type"
            'If cmbopt.Items.Count <= 0 Then
            '    cmbopt.Items.Add("Na")
            'End If
            cmbentry.DataSource = dv.ToTable(True, "entry_date")
            cmbentry.DisplayMember = "entry_date"
            cmbentry.ValueMember = "entry_date"
            'If cmbentry.Items.Count <= 0 Then
            '    cmbentry.Items.Add("Na")
            'End If
        End If
    End Sub

    ''' <summary>
    ''' this checkbox checked then it display or display all the availabel company in the Grid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chksymbol_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chksymbol.CheckedChanged
        If cmbsymbol.Items.Count > 0 Then
            If chksymbol.Checked = True Then
                cmbsymbol.Enabled = False
                Dim i As Integer
                For Each obj As Object In cmbsymbol.Items
                    If i = 0 Then
                        syb = "('" & obj(0).ToString & "'"
                    Else
                        syb = syb & ",'" & obj(0).ToString & "'"
                    End If
                    i += 1
                Next
                syb = syb & ")"
                dv = New DataView(GdtBhavcopy, "option_type<>'XX'", "symbol", DataViewRowState.OriginalRows)
                cmbexp.DataSource = dv.ToTable(True, "exp_date")
                cmbexp.DisplayMember = "exp_date"
                cmbexp.ValueMember = "exp_date"
                cmbopt.DataSource = dv.ToTable(True, "option_type")
                cmbopt.DisplayMember = "option_type"
                cmbopt.ValueMember = "option_type"
                cmbentry.DataSource = dv.ToTable(True, "entry_date")
                cmbentry.DisplayMember = "entry_date"
                cmbentry.ValueMember = "entry_date"
            Else
                cmbsymbol.Enabled = True
                cmbsymbol.SelectedIndex = 0
                syb = "( '" & cmbsymbol.SelectedValue.ToString & "' )"
                dv = New DataView(GdtBhavcopy, "option_type<>'XX' and symbol = '" & cmbsymbol.SelectedValue.ToString & "'", "symbol", DataViewRowState.OriginalRows)
                cmbexp.DataSource = dv.ToTable(True, "exp_date")
                cmbexp.DisplayMember = "exp_date"
                cmbexp.ValueMember = "exp_date"
                cmbopt.DataSource = dv.ToTable(True, "option_type")
                cmbopt.DisplayMember = "option_type"
                cmbopt.ValueMember = "option_type"
                cmbentry.DataSource = dv.ToTable(True, "entry_date")
                cmbentry.DisplayMember = "entry_date"
                cmbentry.ValueMember = "entry_date"
            End If
        End If
    End Sub

    ''' <summary>
    ''' This check box checked changed event enbaled cmbExpiry Date Combo box and it takes all the expiry date which is available on this combo box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkexp_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkexp.CheckedChanged
        If cmbexp.Items.Count > 0 Then

            If chkexp.Checked = True Then
                cmbexp.Enabled = False
                Dim i As Integer
                For Each obj As Object In cmbexp.Items
                    If i = 0 Then
                        expdate = "(#" & CDate(obj(0).ToString).Date.ToString("dd/MMM/yyyy") & "#"
                    Else
                        expdate = expdate & ",#" & CDate(obj(0).ToString).Date.ToString("dd/MMM/yyyy") & "#"
                    End If
                    i += 1
                Next

                expdate = expdate & ")"
            Else
                cmbexp.Enabled = True
                cmbexp.SelectedIndex = 0
                expdate = "(#" & CDate(cmbexp.SelectedValue.ToString).Date.ToString("dd/MMM/yyyy") & "#)"
            End If
        End If
    End Sub

    ''' <summary>
    ''' This check box checked changed event enbaled cmbOption Type Combo box and it takes all the Option Type which is available on this combo box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub chkopt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkopt.CheckedChanged
        If cmbopt.Items.Count > 0 Then
            If chkopt.Checked = True Then
                cmbopt.Enabled = False
                Dim i As Integer
                For Each obj As Object In cmbopt.Items
                    If i = 0 Then
                        opttype = "('" & obj(0).ToString & "'"
                    Else
                        opttype = opttype & ",'" & obj(0).ToString & "'"
                    End If
                    i += 1
                Next
                opttype = opttype & ")"
            Else
                cmbopt.Enabled = True
                cmbopt.SelectedIndex = 0
                opttype = "( '" & cmbopt.SelectedValue.ToString & "')"
            End If
        End If
    End Sub

    ''' <summary>
    ''' get expiry date which is selected for filteration data and display it
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbexp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbexp.SelectedIndexChanged
        expdate = "( #" & CDate(cmbexp.SelectedValue.ToString).Date.ToString("dd/MMM/yyyy") & "# )"
    End Sub

    ''' <summary>
    ''' get Option Type which is selected for filteration data and display it
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbopt_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbopt.SelectedIndexChanged
        opttype = "( '" & cmbopt.SelectedValue.ToString & "' )"
    End Sub


    ''' <summary>
    ''' export gridview data in the Excel File
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim savedi As New SaveFileDialog
        savedi.Filter = "Files(*.xls)|*.xls"
        If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                       
            'exporttoexcel(grd, savedi.FileName, sname, "other")

            REM: Bhavcopy Export Process Optimization 10 min To 10 Sec 22nd June 11 By Viral
            ExporttoHtml(DataGridView1, savedi.FileName)
            ', sname, "other")
            REM End

            REM: Bhavcopy Export Process Optimization 10 min To 1 min 22nd June 11 By Viral
            'SaveBCToExl(dv.ToTable, savedi.FileName, sname, "other")
            REM End
        End If
    End Sub

    ''' <summary>
    ''' takes only Numeric Value on the txtvolb key Press
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtvolb_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtvolb.KeyPress
        numonly(e)
    End Sub

    ''' <summary>
    ''' takes only Numeric Value on the txtcontractb key Press
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtcontractb_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcontractb.KeyPress
        numonly(e)
    End Sub

    ''' <summary>
    ''' takes only Numeric Value on the txtvalueb key Press
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtvalueb_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtvalueb.KeyPress
        numonly(e)
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
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim analysis1 As New bhavcopyprocess

        analysis1.ShowDialog()

        If GVarIsNewBhavcopy = True Or BhavCopyFlag = True Then
            fill_data()
            BhavCopyFlag = False
        End If
    End Sub
    ''' <summary>
    ''' in this Methode assign data to all controls like 
    ''' Cmbsymbol fill Company from gdtbhavcopy datatable
    ''' Fill Expiry Date to cmb ep_Date by gdtBhavcopy Data Table 
    ''' Fill Option Type to cmbOption_Type by gdtBhavcopy Data Table
    ''' Fill Entry Date to CmbEntry_date Combo by gdtBhavcopy Data Table
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fill_data()
        '  chkbhavcopy = True
        'tempdata = objBh.select_data()

        dv = New DataView(GdtBhavcopy, "option_type<>'XX'", "symbol", DataViewRowState.OriginalRows)
        cmbsymbol.DataSource = dv.ToTable(True, "symbol")
        cmbsymbol.DisplayMember = "symbol"
        cmbsymbol.ValueMember = "symbol"

        'If cmbsymbol.Items.Count <= 0 Then
        '    cmbsymbol.Items.Add("Na")
        'End If
        cmbexp.DataSource = dv.ToTable(True, "exp_date")
        cmbexp.DisplayMember = "exp_date"
        cmbexp.ValueMember = "exp_date"


        'If cmbexp.Items.Count <= 0 Then
        '    cmbexp.Items.Add("Na")
        'End If
        cmbopt.DataSource = dv.ToTable(True, "option_type")
        cmbopt.DisplayMember = "option_type"
        cmbopt.ValueMember = "option_type"
        'If cmbopt.Items.Count <= 0 Then
        '    cmbopt.Items.Add("Na")
        'End If
        cmbentry.DataSource = dv.ToTable(True, "entry_date")
        cmbentry.DisplayMember = "entry_date"
        cmbentry.ValueMember = "entry_date"
        'If cmbentry.Items.Count <= 0 Then
        '    cmbentry.Items.Add("Na")
        'End If
        If cmbsymbol.Items.Count > 0 Then
            cmbsymbol.SelectedIndex = 1
            cmbsymbol.SelectedIndex = 0
        End If
        'Me.WindowState = FormWindowState.Maximized
        'Me.Refresh()
    End Sub
    Public Sub DownloadSpanFile(ByVal Fname As String)
        Try


            Dim url As String = ""

            url = "https://support.finideas.com/Bhavcopy/"

            Dim filepath As String = Application.StartupPath + "\" + "DownloadBhavcopy\"


            Dim filename As String = filepath & Fname


            Dim filepathdir As String = Application.StartupPath + "\" + "DownloadBhavcopy\"
            If System.IO.Directory.Exists(filepathdir) Then
                Dim directory As New System.IO.DirectoryInfo(filepathdir)


                For Each file As System.IO.FileInfo In directory.GetFiles()
                    Try
                        file.Delete()

                    Catch ex As Exception

                    End Try

                Next
            End If

            If Not System.IO.Directory.Exists(filepathdir) Then
                System.IO.Directory.CreateDirectory(filepathdir)
            End If
            Dim i As Integer = 1
            Dim sourcefname As String = ""
            If NEW_BHAVCOPY = 1 Then
                sourcefname = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
            Else
                sourcefname = "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"

            End If

aa:
            DownloadFileFTP(Fname, url, Application.StartupPath + "\" + "DownloadBhavcopy\")
            Dim info2 As New FileInfo(Application.StartupPath + "\" + "DownloadBhavcopy\" + sourcefname)
            Dim length2 As Long = info2.Length
            'Dim filepath1 As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + fnamecsv
            Dim sourcepath As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + sourcefname
            If Not System.IO.File.Exists(sourcepath) Or length2 = 0 Then
                i = i + 1
                If NEW_BHAVCOPY = 1 Then
                    Fname = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
                    sourcefname = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1 * i).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"

                Else

                    Fname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                    sourcefname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                End If
                GoTo aa


                'Else
                '    Dim info2 As New FileInfo(Application.StartupPath + "\" + "DownloadBhavcopy\" + sourcefname)
                '    Dim length2 As Long = info2.Length
                '    If length2 = 0 Then
                '        i = i + 1
                '        Fname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"
                '        sourcefname = "fo" + DateTime.Now.AddDays(-1 * i).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"

                '        GoTo aa
                '    End If
            End If




            downloadbhavcopy = Fname.Replace(".zip", "")
            ' ZipHelp.UnZip(filepath, Path.GetDirectoryName(filepath), 4096)
            UnZip1(sourcepath, Path.GetDirectoryName(sourcepath), 4096)
            sourcepath = Application.StartupPath + "\" + "DownloadSpanFile\" + sourcefname




            'Dim client As New WebClient()

            'Try
            '    Dim uri As New Uri(url)
            '    'client.Headers.Add("Accept: text/html, application/xhtml+xml, */*")
            '    'client.Headers.Add("User-Agent: Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)")
            '    client.DownloadFileAsync(uri, filename)

            '    While client.IsBusy
            '        System.Threading.Thread.Sleep(1000)

            '    End While
            '    'Console.WriteLine(ex.Message);
            'Catch eex As Exception
            '    MsgBox("Error")
            '    'Catch ex As UriFormatException
            'End Try


        Catch ex As Exception
            MsgBox("Bhavcopy Update Error..")
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    
    Public Sub removebhavcopy()
        GdtBhavcopy = objBh.select_data()
        Dim dt As DataTable = New DataView(GdtBhavcopy, "", "entry_date ASC", DataViewRowState.CurrentRows).ToTable(True, "entry_date")
        If dt.Rows.Count >= BHAVCOPYPROCESSDAY Then

            For i As Integer = 0 To dt.Rows.Count - BHAVCOPYPROCESSDAY - 1
                Dim entrydate As Date = CDate(dt.Rows(i)(0))
                removebhavcopy(entrydate)
            Next

        End If
    End Sub
    Public Function RemoveBhavcopy(ByVal entry_date As Date) As DataTable
        Dim SP_delete_bhavcopy_Date As String = "delete_bhavcopy_Date"
        Try
            data_access.ParamClear()
            data_access.AddParam("@date1", OleDbType.Date, 50, CDate(entry_date))
            data_access.Cmd_Text = SP_delete_bhavcopy_Date
            data_access.cmd_type = CommandType.StoredProcedure
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            '  MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function
    Private Sub read_filebhav(ByVal path As String)
        REM Change In processing Bhavcopy and selecting parameters, Decimal Strike Prices are properly displayed for the records 
        Dim tempdata As DataTable
        Dim objTrad As trading = New trading
        Dim Mrateofinterast As Double = 0
        Dim DtBCP As DataTable
        Try

            Dim fpath As String
            fpath = CStr(path)
            Mrateofinterast = Val(IIf(IsDBNull(objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")), 0, objTrad.Settings.Compute("max(SettingKey)", "SettingName='Rateofinterest'")))
            If fpath <> "" Then
                Dim fi As New FileInfo(fpath)
                Dim dv As DataView
                tempdata = New DataTable
                DtBCP = New DataTable
                tempdata.Columns.Add("script")
                tempdata.Columns.Add("vol")
                tempdata.Columns.Add("futval")
                tempdata.Columns.Add("mt")
                tempdata.Columns.Add("iscall")
                tempdata.AcceptChanges()

                'Call Proc_Data_FromBhavCopyCsv(fpath)
                Dim impBHav As ImportData.ImportOperation
                impBHav = New ImportData.ImportOperation
                import_Data.CopyToData(fpath, "BHAVCOPY")



                If NEW_BHAVCOPY = 1 Then
                    import_Data.CopyToData(fpath, "BHAVCOPYNEW")
                Else
                    import_Data.CopyToData(fpath, "BHAVCOPY")
                End If
                Call impBHav.ImportBhavCopy()

                impBHav = Nothing
                DtBCP = objBh.select_TblBhavCopy()

                tempdata.Merge(DtBCP)
                '-----------------------------------------------------------------------
                '' ''Dim fi As New FileInfo(fpath)
                ' ''Dim dv As DataView
                ' ''Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName
                '' ''Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;IMEX=1;Data Source=" & fi.DirectoryName
                '' '';HDR=Yes;FMT=Delimited

                ' ''Dim objConn As New OleDbConnection(sConnectionString)

                ' ''objConn.Open()

                ' ''Dim objCmdSelect As New OleDbCommand("SELECT INSTRUMENT,SYMBOL,EXPIRY_DT,STRIKE_PR,OPTION_TYP,SETTLE_PR,CONTRACTS,VAL_INLAKH,TIMESTAMP FROM " & fi.Name, objConn)

                ' ''Dim objAdapter1 As New OleDbDataAdapter

                ' ''objAdapter1.SelectCommand = objCmdSelect

                ' ''tempdata = New DataTable
                ' ''tempdata.Columns.Add("script")
                ' ''tempdata.Columns.Add("vol")
                ' ''tempdata.Columns.Add("futval")
                ' ''tempdata.Columns.Add("mt")
                ' ''tempdata.Columns.Add("iscall")
                ' ''tempdata.AcceptChanges()

                '-----------------------------------------------------------------------

                Dim mt As Double
                Dim futval As Double
                Dim iscall As Boolean
                Dim drow As DataRow
                'objAdapter1.Fill(tempdata)
                'objConn.Close()

                'dv = New DataView(tempdata, "OPTION_TYP='XX'", "", DataViewRowState.OriginalRows)
                dv = New DataView(tempdata, "option_typ='XX'", "", DataViewRowState.CurrentRows)
                'dv = New DataView(Dt, "OPTION_TYP='XX'", "", DataViewRowState.OriginalRows)
                Dim str(2) As String
                str(0) = "EXPIRY_DT"
                str(1) = "SETTLE_PR"
                str(2) = "SYMBOL"
                Dim tdata As New DataTable
                tdata = dv.ToTable(True, str)
                Dim row As DataRow
                Dim script As String
                For Each drow In tempdata.Rows
                    If drow("option_typ") = "XX" Then
                        script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy")
                        drow("script") = UCase(script.Trim)
                        drow("vol") = 0
                        drow("futval") = 0
                        drow("mt") = 0
                    Else
                        script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(CDate(drow("EXPIRY_DT")), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")), "###0.00") & "  " & drow("OPTION_TYP")
                        'script = drow("INSTRUMENT") & "  " & drow("SYMBOL") & "  " & Format(drow("EXPIRY_DT"), "ddMMMyyyy") & "  " & Format(Val(drow("STRIKE_PR")), "####.##") & "  " & drow("OPTION_TYP")
                        drow("script") = UCase(script.Trim)
                        futval = 0
                        drow("vol") = 0
                        For Each row In tdata.Select(" EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' ")
                            futval = row("SETTLE_PR")
                        Next
                        'futval = Val(tempdata.Compute("Max(SETTLE_PR)", " EXPIRY_DT='" & drow("EXPIRY_DT") & "' and SYMBOL= '" & drow("SYMBOL") & "' And option_typ='XX'").ToString() & "")
                        'row("SETTLE_PR")

                        If Mid(drow("OPTION_TYP"), 1, 1) = "C" Then
                            iscall = True
                        Else
                            iscall = False
                        End If
                        Dim ccdate As Date = CDate(drow("TIMESTAMP").ToString.Replace("-", "/"))
                        mt = DateDiff(DateInterval.Day, ccdate.Date, CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date)
                        If ccdate.Date = CDate(drow("EXPIRY_DT").ToString.Replace("-", "/")).Date Then
                            mt = 0.5
                        End If
                        If mt = 0 Then
                            mt = 0.0001
                        Else
                            mt = (mt) / 365
                        End If
                        If futval > 0 Then
                            drow("vol") = Vol(futval, Val(drow("STRIKE_PR")), Val(drow("SETTLE_PR")), mt, iscall, True) * 100
                        End If
                        drow("futval") = futval
                        drow("mt") = mt
                        drow("iscall") = iscall
                    End If
                Next
                tempdata.AcceptChanges()
                objBh.insertNew(tempdata)

                GdtBhavcopy = objBh.select_data()
                GVarIsNewBhavcopy = True
                BhavCopyFlag = True

                Dim Item As DictionaryEntry
                Dim ArrFKey As New ArrayList
                Dim ArrCPKey As New ArrayList
                Dim ArrEKey As New ArrayList
                Dim VaLLTPPrice As New Double
                For Each Item In fltpprice
                    ArrFKey.Add(Item.Key)
                Next
                For Each Item In ltpprice
                    ArrCPKey.Add(Item.Key)
                Next
                For Each Item In eltpprice
                    ArrEKey.Add(Item.Key)
                Next
                For i As Integer = 0 To ArrFKey.Count - 1
                    If cpfmaster.Select("token=" & ArrFKey(i) & "").Length > 0 Then
                        Dim VarScript As String = cpfmaster.Select("token=" & ArrFKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrFKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next
                For i As Integer = 0 To ArrCPKey.Count - 1
                    If cpfmaster.Select("token=" & ArrCPKey(i) & "").Length > 0 Then
                        Dim VarScript As String = cpfmaster.Select("token=" & ArrCPKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrCPKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next
                For i As Integer = 0 To ArrEKey.Count - 1
                    If eqmaster.Select("token=" & ArrEKey(i) & "").Length > 0 Then
                        Dim VarScript As String = eqmaster.Select("token=" & ArrEKey(i) & "")(0).Item("Script")
                        If GdtBhavcopy.Select("Script='" & VarScript & "'").Length > 0 Then
                            fltpprice(ArrEKey(i)) = GdtBhavcopy.Select("Script='" & VarScript & "'")(0).Item("ltp")
                        End If
                    End If
                Next

                If analysis.chkanalysis = True Then
                    Call analysis.AssignBhavcopyLTP(True)
                End If

                Label1.Text = "Bhavcopy Processed Successfully."
                MsgBox("Bhavcopy Processed Successfully.", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            Label1.Text = "Bhavcopy Not Processed."
            MsgBox("Bhavcopy Not Processed.", MsgBoxStyle.Information)
            'MsgBox("Select valid file")
            '/MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim fnamecsv As String
        If NEW_BHAVCOPY = 1 Then
            fnamecsv = "BhavCopy_NSE_FO_0_0_0_" + DateTime.Now.AddDays(-1).ToString("yyyyMMdd").ToUpper() + "_F_0000.csv.zip"
        Else
            fnamecsv = "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv.zip"

        End If
        downloadbhavcopy = fnamecsv
        'Dim sourcefname As String = "nsccl." + DateTime.Now.ToString("yyyyMMdd") + ".i01.spn"
        'fo16JUN2021bhav.csv.zip()
        DownloadSpanFile(fnamecsv)

        removebhavcopy()
        ' Dim str As String = Application.StartupPath + "\" + "DownloadBhavcopy\" + "fo" + DateTime.Now.AddDays(-1).ToString("ddMMMyyyy").ToUpper() + "bhav.csv"


        read_filebhav(Application.StartupPath + "\" + "DownloadBhavcopy\" + downloadbhavcopy)
    End Sub
End Class
