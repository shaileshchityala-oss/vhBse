Public Class rptMaturity
    Public Shared chkrptMaturity As Boolean
    Dim dtable As New DataTable
    Dim objTrad As trading = New trading
    Dim syb As String
    Dim objBh As New bhav_copy
    Dim tempdata As New DataTable
    ''' <summary>
    ''' Initialize Data Table 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub init_table()
        dtable = New DataTable
        With dtable.Columns
            .Add("script")
            .Add("instrument")
            .Add("company")
            .Add("cpf")
            .Add("mdate", GetType(Date))
            .Add("entrydate", GetType(Date))
            .Add("strike", GetType(Double))
            .Add("qty", GetType(Double))
            .Add("ATP", GetType(Double))
        End With
    End Sub
    ''' <summary>
    ''' Fill Data Table from Database and assign value to Grid View Filter by Entry Date
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub fill_table()
        Dim dr As DataRow
        Dim count As Integer
        Dim ar As New ArrayList
        Dim table As New DataTable

        If cmbentry.SelectedIndex > -1 Then
            table = objTrad.Trading_Ltp(cmbentry.SelectedValue)
            dtable.Rows.Clear()
            For Each drow As DataRow In table.Rows
                count = CInt(table.Compute("count(script)", "script='" & drow("script") & "'"))
                If count > 1 Then
                    If Not ar.Contains(drow("script")) Then
                        Dim brate As Double = 0
                        Dim srate As Double = 0
                        ar.Add(drow("script"))
                        dr = dtable.NewRow()
                        dr("script") = CStr(drow("script"))
                        dr("instrument") = CStr(drow("instrumentname"))
                        dr("strike") = Val(drow("strikerate"))
                        dr("cpf") = CStr(drow("cp"))
                        dr("qty") = Val(table.Compute("sum(qty)", "script='" & drow("script") & "'"))
                        For Each row As DataRow In table.Select("script='" & drow("script") & "'")
                            If Val(row("qty")) < 0 Then
                                srate = srate + (-Val(row("tot")))
                            Else
                                brate = brate + Val(row("tot"))
                            End If
                        Next

                        dr("ATP") = drow("LTP")

                        dr("company") = CStr(drow("company"))
                        dr("mdate") = CDate(Format(CDate(drow("mdate")), "MMM/dd/yyyy"))
                        dr("entrydate") = drow("entry_date")
                        If Val(dr("qty")) <> 0 Then
                            dtable.Rows.Add(dr)
                        End If
                    End If
                Else
                    dr = dtable.NewRow()
                    dr("script") = CStr(drow("script"))
                    dr("instrument") = CStr(drow("instrumentname"))
                    dr("strike") = Val(drow("strikerate"))
                    dr("cpf") = CStr(drow("cp"))
                    dr("qty") = Val(drow("qty"))
                    dr("atp") = Val(drow("ltp"))
                    dr("company") = CStr(drow("company"))
                    dr("mdate") = CDate(Format(CDate(drow("mdate")), "MMM/dd/yyyy"))
                    dr("entrydate") = drow("entry_date")
                    If Val(dr("qty")) <> 0 Then
                        dtable.Rows.Add(dr)
                    End If
                End If
            Next
            grdtrad.DataSource = dtable
            grdtrad.Refresh()
        Else
            MsgBox("Please load Bhavcopy.")
            Exit Sub
        End If

    End Sub


    ''' <summary>
    '''  set ChkrptMaturity Flage False
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rptMaturity_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        chkrptMaturity = False

    End Sub
    ''' <summary>
    ''' assign Data to entry date combo box
    ''' set ChkrptMaturity Flage True
    ''' call Initialization Methode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rptMaturity_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        tempdata = objBh.select_Date()
        cmbentry.DataSource = tempdata
        cmbentry.DisplayMember = "entry_date"
        cmbentry.ValueMember = "entry_date"

        chkrptMaturity = True
        init_table()



    End Sub
    ''' <summary>
    ''' ' Export FO data of Grid view in the Excel File on Button Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'grdtrad.DataSource = dtable
        'grdtrad.Refresh()
        'grdeq.DataSource = eqtable
        'grdeq.Refresh()
        EXPORT_IMPORT_POSITION = Val(GdtSettings.Compute("max(SettingKey)", "SettingName='EXPORT_IMPORT_POSITION'").ToString)

        If (EXPORT_IMPORT_POSITION = 2) Then
            If grdtrad.Rows.Count > 0 Then
                Dim savedi As New SaveFileDialog
                savedi.Filter = "Files(*.XLS)|*.XLS"
                If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                    'exportExcel(grdtrad, savedi.FileName)
                    'exportExcel(grdeq, savedi.FileName)
                    Dim grd(0) As DataGridView
                    grd(0) = grdtrad
                    Dim sname(0) As String
                    sname(0) = "Trading"
                    exporttoexcel(grd, savedi.FileName, sname, "other")
                End If
            End If
        ElseIf (EXPORT_IMPORT_POSITION = 1) Then

            Dim savedi As New SaveFileDialog
            savedi.Filter = "File(*.csv)|*.Csv"
            If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim dt As DataTable
                dt = CType(grdtrad.DataSource, DataTable)
                Dim dtgrd As DataTable
                Dim name(dt.Columns.Count) As String

                Dim dr As DataRow
                dtgrd = New DataTable
                With dtgrd.Columns
                    .Add("Scrip")
                    .Add("Instrument")
                    .Add("Security")
                    .Add("CPF")
                    .Add("Exp. Date", GetType(Date))
                    .Add("Entrydate", GetType(Date))
                    .Add("Strike", GetType(Double))
                    .Add("Qty", GetType(Double))
                    .Add("ATP", GetType(Double))
                    .Add("exp_date", GetType(Date))
                End With

                Dim cal As DataRow
                dr = dtgrd.NewRow()
                For Each dr5 As DataRow In dt.Rows
                    cal = dtgrd.NewRow()
                    cal("Scrip") = dr5("script")
                    cal("Instrument") = dr5("instrument")
                    cal("Security") = dr5("company")
                    cal("CPF") = dr5("cpf")
                    cal("Exp. Date") = dr5("mdate")
                    cal("Entrydate") = dr5("entrydate")
                    cal("Strike") = dr5("Strike")
                    cal("Qty") = dr5("Qty")
                    cal("ATP") = dr5("ATP")
                    cal("exp_date") = dr5("mdate")
                    dtgrd.Rows.Add(cal)

                    dtgrd.AcceptChanges()

                Next
                exporttocsv(dtgrd, savedi.FileName, "other")
                MsgBox("Export Successfully")
                OPEN_Export_File(savedi.FileName)
            End If
        End If
    End Sub


    ''' <summary>
    '''  Export FO data of Grid view in the Excel File on F11 Key Press
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rptposition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F11 Then
            If grdtrad.Rows.Count > 0 Then
                Dim savedi As New SaveFileDialog
                savedi.Filter = "Files(*.XLS)|*.XLS"
                If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
                    'exportExcel(grdtrad, savedi.FileName)
                    'exportExcel(grdeq, savedi.FileName)
                    Dim grd(0) As DataGridView
                    grd(0) = grdtrad
                    Dim sname(0) As String
                    sname(0) = "Trading"
                    exporttoexcel(grd, savedi.FileName, sname, "other")
                End If
            End If

        End If
    End Sub


    ''' <summary>
    ''' On Show Button Click Data Table display Data after fill from DataBase filter by Entry Date Combo box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmdShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShow.Click
        fill_table()
    End Sub

    Private Sub rptMaturity_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If


    End Sub
End Class
