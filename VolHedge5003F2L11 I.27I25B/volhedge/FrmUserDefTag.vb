
Public Class FrmUserDefTag
    Dim ObjUserDefTag As New UserDefTag
    Dim dtSymbol As DataTable

    Public Function ShowForm(ByVal sTag As String) As UserDefTag
        ObjUserDefTag.sTagName = sTag
        ObjUserDefTag.bIsValid = False
        Me.ShowDialog()
        Return ObjUserDefTag
    End Function

    Private Sub FrmUserDefTag_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            ObjUserDefTag.bIsValid = False
            Me.Close()
        ElseIf e.KeyCode = Keys.Enter Then
        Call BtnApply_Click(BtnApply, New System.EventArgs)
        End If
    End Sub

    Private Sub FrmUserDefTag_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        dtSymbol = New DataTable
        dtSymbol = ObjUserDefTag.Select_Symbol
        Dim dv As DataView = New DataView(dtSymbol, "", "symbol", DataViewRowState.CurrentRows)
        CmbComp.DataSource = dv.ToTable(True, "symbol")
        CmbComp.DisplayMember = "symbol"
        CmbComp.ValueMember = "symbol"
        'If dv.ToTable(True, "symbol").Compute("count(symbol)", "symbol='NIFTY'") > 0 Then
        ' CmbComp.SelectedValue = "NIFTY"
        'CmbComp.Select()
        'End If


        CmbComp.SelectedValue = GetSymbol(ObjUserDefTag.sTagName)
        CmbComp.Select()
        TxtTabName.Text = GetTabName(ObjUserDefTag.sTagName)

    End Sub

    Private Sub BtnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnApply.Click

        If CmbComp.Text.Trim = "" Then
            MsgBox("Invalid Company Selected.", MsgBoxStyle.Information)
            CmbComp.Focus()
            Return
        End If
        'If TxtTabName.Text.Trim = "" Then
        '    MsgBox("Invalid Tab Name.", MsgBoxStyle.Information)
        '    TxtTabName.Focus()
        '    Return
        'End If

        If TxtTabName.Text.Trim.ToUpper = CmbComp.Text.Trim.ToUpper Then
            MsgBox("Invalid Tab Name.", MsgBoxStyle.Information)
            TxtTabName.Focus()
            Return
        End If
        If TxtTabName.Text.Trim.Length > 20 Then
            MsgBox("Invalid Tab Name.(Only 20 Char allowed)", MsgBoxStyle.Information)
            TxtTabName.Focus()
            Return
        End If
        If TxtTabName.Text = "" Then
            ObjUserDefTag.sTagName = CmbComp.Text.Trim
        Else
            ObjUserDefTag.sTagName = CmbComp.Text.Trim & "/" & TxtTabName.Text.Trim
        End If

        ObjUserDefTag.bIsValid = True
        MsgBox("Applied Successfully..", MsgBoxStyle.Information)
        Me.Close()
    End Sub
End Class