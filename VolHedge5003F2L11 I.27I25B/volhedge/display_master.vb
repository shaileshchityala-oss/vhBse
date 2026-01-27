Public Class display_master
    Dim masterdata As DataTable
    Dim objTrad As New trading
    Dim cmbh As Integer
    Public portfolio As String
    Dim dv As DataView
    Dim temptable As New DataTable
    Dim objVol As New findvolitility
    Dim porttable As New DataTable

    Private Sub display_master_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub
    Private Sub diplay_master_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblportfolio.Text = portfolio
        masterdata = New DataTable
        masterdata = cpfmaster
        dv = New DataView(masterdata, "option_type in ('CA','CE','PA','PE')", "symbol", DataViewRowState.CurrentRows)
        cmbcomp.DataSource = dv.ToTable(True, "symbol")
        cmbcomp.DisplayMember = "symbol"
        cmbcomp.ValueMember = "symbol"
        cmbcomp.Refresh()
        cmbcomp.SelectedValue = "NIFTY"
        cmbh = cmbcomp.Height
        init_table()
        portfoliodata()
        'Me.WindowState = FormWindowState.Normal
        'Me.Refresh()
    End Sub
    Private Sub cmbcomp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcomp.Enter
        'cmbcomp.DroppedDown = True
        'cmbheight = True
        cmbcomp.Height = 150
        cmbcomp.BringToFront()
    End Sub
    Private Sub cmbcomp_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcomp.Leave
        'If cmbheight = True Then
        cmbcomp.Height = cmbh
        '    cmbheight = False
        'End If
        If cmbcomp.Text <> "" And cmbcomp.Items.Count > 0 Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and option_type in ('CA','CE','PA','PE') ", "strike_price,option_type", DataViewRowState.CurrentRows)
            If dv.ToTable.Rows.Count <= 0 Then
                MsgBox("Select valid Symbol.")
                cmbcomp.Text = ""
                cmbcomp.Focus()
                Exit Sub
            End If
            cmbcp.DataSource = dv.ToTable(True, "option_type")
            cmbcp.DisplayMember = "option_type"
            cmbcp.ValueMember = "option_type"
            cmbcp.Refresh()
            cmbcp.SelectedItem = 0
            cmbstrike.DataSource = dv.ToTable(True, "strike_price")
            cmbstrike.DisplayMember = "strike_price"
            cmbstrike.ValueMember = "strike_price"
            cmbstrike.Refresh()
            cmbdate.DataSource = dv.ToTable(True, "expdate")
            cmbdate.DisplayMember = "expdate"
            cmbdate.ValueMember = "expdate"
            cmbdate.Refresh()
            cmbcp.SelectedItem = 0
        End If
    End Sub
    Private Sub init_table()
        With temptable.Columns

            .Add("script")
            .Add("token")
            .Add("symbol")
            .Add("strike_price")
            .Add("option_type")
            .Add("expdate1")
            .Add("instrumentname")
        End With
    End Sub
    Private Sub portfoliodata()
        porttable = New DataTable
        porttable = objVol.Selectvol(lblportfolio.Text)
        'grdport.DataSource = Nothing
        grdport.DataSource = porttable
    End Sub
    
    'Private Sub cmbcp_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcp.SelectedIndexChanged
    '    If cmbcp.Text <> "" Then
    '        Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "'", "strike_price", DataViewRowState.CurrentRows)
    '        cmbstrike.DataSource = dv.ToTable(True, "strike_price")
    '        cmbstrike.DisplayMember = "strike_price"
    '        cmbstrike.ValueMember = "strike_price"
    '        cmbstrike.Refresh()
    '    End If
    'End Sub
    'Private Sub cmbstrike_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.SelectedIndexChanged
    '    If cmbstrike.Text <> "" Then
    '        Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and InstrumentName='" & cmbinstrument.Text & "' and option_type='" & cmbcp.Text & "' and strike_price=" & CDbl(cmbstrike.Text) & " ", "strike_price", DataViewRowState.CurrentRows)
    '        cmbdate.DataSource = dv.ToTable(True, "expdate1")
    '        cmbdate.DisplayMember = "expdate1"
    '        cmbdate.ValueMember = "expdate1"
    '        cmbdate.Refresh()
    '    End If
    'End Sub

    Private Sub cmbstrike_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.Leave
        cmbstrike.Height = cmbh
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If chkcomp.Checked Then
            Dim str As String = ""
            str = "option_type in ('CA','CE','PA','PE')"
            If chkcomp.Checked = True Then
                If cmbcomp.Text <> "" Then
                    str &= " and symbol ='" & cmbcomp.Text & "'"
                End If
            End If
            If chkcpf.Checked = True Then
                If cmbcp.SelectedValue <> "" Then
                    str &= " and option_type ='" & cmbcp.SelectedValue & "'"
                End If
            End If
            If chkstrike.Checked = True Then
                If cmbstrike.Text <> "" Then
                    If IsNumeric(cmbstrike.Text) Then
                        str &= " and strike_price =" & CInt(cmbstrike.Text) & ""
                    End If
                End If
            End If
            If chkdate.Checked = True Then
                If cmbdate.SelectedValue <> "" Then
                    str &= " and expdate1 =#" & cmbdate.SelectedValue & "#"
                End If
            End If
            dv.RowFilter = str
            chkcheck.Checked = True

            Dim chr1(6) As String
            chr1(0) = "script"
            chr1(1) = "token"
            chr1(2) = "symbol"
            chr1(3) = "strike_price"
            chr1(4) = "option_type"
            chr1(5) = "expdate1"
            chr1(6) = "instrumentname"

            temptable.Rows.Clear()
            temptable = dv.ToTable(True, chr1)
            temptable.Columns.Add("status", GetType(Boolean))
            temptable.AcceptChanges()
            'For Each row As DataRow In temptable.Rows
            '    row("status") = True
            '    For Each drow As DataRow In porttable.Select("token=" & row("token") & "")

            '    Next
            'Next
            Dim row As DataRow
            Dim i As Integer = 0
            Dim j As Integer = temptable.Rows.Count
            Dim check As Boolean = False
            While i < j
                check = False
                row = temptable.Rows(i)
                row("status") = True
                For Each drow As DataRow In porttable.Select("token=" & row("token") & "")
                    check = True
                    Exit For
                Next
                If check = True Then
                    If MsgBox(row("script") & " Script alerady exist in portfilio ,Would you like to add ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        i += 1
                    Else
                        temptable.Rows.Remove(row)
                        j -= 1
                    End If
                Else
                    i += 1
                End If
            End While
           

            grdvol.DataSource = New DataView(temptable, "", "strike_price,option_type,expdate1", DataViewRowState.CurrentRows).ToTable
            chkcheck.Checked = True
            dv.RowFilter = "option_type in ('CA','CE','PA','PE')"
        End If
    End Sub

    Private Sub cmdclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdclear.Click
        'dv.RowFilter = "option_type in ('CA','CE','PA','PE')"
        'Dim chr1(6) As String
        'chr1(0) = "script"
        'chr1(1) = "token"
        'chr1(2) = "symbol"
        'chr1(3) = "strike_price"
        'chr1(4) = "option_type"
        'chr1(5) = "expdate1"
        'chr1(6) = "instrumentname"
        'Dim temptable As New DataTable
        'temptable = New DataTable
        'temptable = dv.ToTable(True, chr1).Clone
        'temptable.Columns.Add("status", GetType(Boolean))
        'temptable.AcceptChanges()
        temptable.Rows.Clear()
        grdvol.DataSource = temptable
        chkcheck.Checked = False
    End Sub

    Private Sub cmdscen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdscen.Click
        If temptable.Rows.Count > 0 Then
            Dim j As Integer
            j = 0
            If porttable.Rows.Count > 0 Then
                j = porttable.Compute("max(ordseq)", "")
            End If
            j += 1
            objVol.Insert(temptable, grdvol, lblportfolio.Text, j)
            chkcheck.Checked = False
            portfoliodata()
        End If
    End Sub

    Private Sub cmddelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        objVol.deletevol(lblportfolio.Text, porttable, grdport)
        portfoliodata()
        chkcheck1.Checked = False
    End Sub

    Private Sub chkcheck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcheck.CheckedChanged
        If grdvol.RowCount > 1 Then
            If chkcheck.Checked = False Then
                For i As Integer = 0 To grdvol.RowCount - 1
                    grdvol.Rows(i).Cells(7).Value = False
                Next
            Else
                For i As Integer = 0 To grdvol.RowCount - 1
                    grdvol.Rows(i).Cells(7).Value = True
                Next
            End If


        End If
    End Sub

    Private Sub chkcheck1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkcheck1.CheckedChanged
        If grdport.RowCount > 1 Then
            If chkcheck1.Checked = False Then
                For i As Integer = 0 To grdport.RowCount - 1
                    grdport.Rows(i).Cells(0).Value = False
                Next
            Else
                For i As Integer = 0 To grdport.RowCount - 1
                    grdport.Rows(i).Cells(0).Value = True
                Next
            End If


        End If
    End Sub

    Private Sub cmdexit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdexit.Click
        Me.Close()
    End Sub

    Private Sub grdvol_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdvol.CellContentClick

    End Sub

    Private Sub cmbstrike_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbstrike.Enter
        If cmbcomp.Text <> "" And cmbcomp.Items.Count > 0 And cmbdate.SelectedValue <> "" And cmbstrike.Items.Count <= 0 Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and option_type in ('CA','CE','PA','PE') ", "strike_price", DataViewRowState.CurrentRows)
            cmbstrike.DataSource = dv.ToTable(True, "strike_price")
            cmbstrike.DisplayMember = "strike_price"
            cmbstrike.ValueMember = "strike_price"
            cmbstrike.Refresh()
        End If
        cmbstrike.Height = 150
        cmbstrike.BringToFront()
    End Sub

    Private Sub cmbdate_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbdate.Enter
        cmbcomp.Height = cmbh

        If cmbcomp.Text <> "" And cmbcomp.Items.Count > 0 Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and option_type in ('CA','CE','PA','PE') ", "option_type", DataViewRowState.CurrentRows)
            cmbdate.DataSource = dv.ToTable(True, "expdate")
            cmbdate.DisplayMember = "expdate"
            cmbdate.ValueMember = "expdate"
            cmbdate.Refresh()
            cmbcp.SelectedItem = 0
        End If
    End Sub

    Private Sub cmbcp_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbcp.Enter
        cmbstrike.Height = cmbh
        cmbcomp.Height = cmbh
        If cmbstrike.Text.Trim = "" Then
            cmbstrike.Text = "0"
        End If
        If cmbcomp.Text <> "" And cmbcomp.Items.Count > 0 And cmbdate.SelectedValue <> "" And cmbstrike.Text <> "0" Then
            Dim dv As DataView = New DataView(masterdata, "symbol='" & cmbcomp.Text & "' and option_type in ('CA','CE','PA','PE') ", "option_type", DataViewRowState.CurrentRows)
            cmbcp.DataSource = dv.ToTable(True, "option_type")
            cmbcp.DisplayMember = "option_type"
            cmbcp.ValueMember = "option_type"
            cmbcp.Refresh()
            cmbcp.SelectedItem = 0

        End If
    End Sub

    Private Sub display_master_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
        End If

    End Sub
End Class