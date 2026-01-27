Public Class FrmUserMaster

    Dim ObjLoginData As New ClsLoginData
    Dim DTUserMasterEn As New DataTable
    Dim DTUserMasterde As New DataTable
    Dim VarUserId As String

    Private Sub FrmUserMaster_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        MDI.objFrmUserMaster = Nothing
    End Sub
    
    ''' <summary>
    ''' FrmDealerMaster_KeyDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>While Escape Key stroke then Form Close</remarks>
    Private Sub FrmDealerMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' FrmDealerMaster_Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>DataGrid fill, Fill Group Name Combo box</remarks>
    Private Sub FrmDealerMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        DTUserMasterde = ObjLoginData.Select_User_Master(False)
        DTUserMasterEn = ObjLoginData.Select_User_Master(True)
        Call Fill_Datagrid()
        Call btnCancel_Click(sender, e)
        If VarUserId <> "" Then
            Call Fill_TextBox(VarUserId)
        End If



    End Sub
    
    ''' <summary>
    ''' btnExit_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Click to exit from current form</remarks>
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        WriteLog("User master Form Closing.. by " & GVar_LogIn_User & "")
        Me.Close()
    End Sub

    ''' <summary>
    ''' Fill_Datagrid
    ''' </summary>
    ''' <remarks>Using this process to fill Grid according filteration criteria</remarks>
    Private Sub Fill_Datagrid()
        Dim Str As String = "0=0 "
        'Str = "SELECT Dealer,Dealer_Name,Dealer_Group_Name FROM Select_Dealer_Master WHERE 0=0"
        If txtSearchUserId.Text <> "" Then
            Str = Str & " AND F2 LIKE '%" & txtSearchUserId.Text & "%'"
        End If
        If txtSearchUserName.Text <> "" Then
            Str = Str & " AND F4 LIKE '%" & txtSearchUserName.Text & "%'"
        End If
        If txtSearchBranch.Text <> "" Then
            Str = Str & " AND F5 LIKE '%" & txtSearchBranch.Text & "%'"
        End If
        Dim Dt3 As DataTable = New DataView(DTUserMasterde, Str, "F1", DataViewRowState.CurrentRows).ToTable
        DGUserMaster.Rows.Clear()
        For Each Dr As DataRow In Dt3.Rows
            Dim RowIdx As Integer = DGUserMaster.Rows.Add()
            DGUserMaster.Rows(RowIdx).Cells("UserId").Value = Dr("F2")
            DGUserMaster.Rows(RowIdx).Cells("Pwd").Value = Dr("F3")
            DGUserMaster.Rows(RowIdx).Cells("UserName").Value = Dr("F4")
            DGUserMaster.Rows(RowIdx).Cells("Branch").Value = Dr("F5")
            DGUserMaster.Rows(RowIdx).Cells("Product").Value = Dr("F6")
            DGUserMaster.Rows(RowIdx).Cells("Allowed").Value = Dr("F7")
            DGUserMaster.Rows(RowIdx).Cells("Limited").Value = Dr("F8")
            DGUserMaster.Rows(RowIdx).Cells("ExDate").Value = Dr("F9")
            DGUserMaster.Rows(RowIdx).Cells("Status").Value = Dr("F10")
            DGUserMaster.Rows(RowIdx).Cells("IsAdmin").Value = Dr("F11")

            'Dim VarIsImport As String = Dr("IsImport").ToString
            'DGUserMaster.Rows(RowIdx).Cells("IsImport").Value = Boolean.Parse(IIf(VarIsImport = "", True, VarIsImport))
            'Dim VarIsHighLight As String = Dr("IsHighLight").ToString
            'DGUserMaster.Rows(RowIdx).Cells("IsHighLight").Value = Boolean.Parse(IIf(VarIsHighLight = "", False, VarIsHighLight))
        Next
    End Sub

    ''' <summary>
    ''' btnCancel_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Click to cancel all process</remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtUserId.Text = ""
        TxtPwd.Text = ""
        txtUserName.Text = ""
        TxtBranch.Text = ""

        ChkUserAllowed.Checked = False
        ChkUserLimited.Checked = False
        ChkUserAdmin.Checked = False
        dtpExpDate.Value = Now.Date


        btnNew.Enabled = True
    End Sub

    ''' <summary>
    ''' CheckValidation
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>This function use to check validation</remarks>
    Private Function CheckValidation() As Boolean

        REM This block use to Check Same Dealer Name Already Exist or not
        If DTUserMasterde.Select("F2='" & txtUserId.Text & "'").Length > 0 Then
            If MsgBox("User Name Already Exist !!!" & vbCrLf & "Do you want to update existing user information?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                WriteLog("Update Existing User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
            Else
                txtUserName.Focus()
                Return False
                Exit Function
            End If
        End If
        REM End


        REM Check Allowed User 
        If ChkUserAllowed.Checked Then
            Dim AllowCnt As Integer
            AllowCnt = DTUserMasterde.Compute("count(F7)", "F7=true")
            If GFun_CheckLicUserCount(AllowCnt) = False Then
                Return False
                Exit Function
            End If
        End If
        REM End

        Return True
    End Function
    Private Sub SetData()
        REM Data transfer to data class
        ObjLoginData.Userid = txtUserId.Text
        ObjLoginData.pwd = TxtPwd.Text
        ObjLoginData.Username = txtUserName.Text
        ObjLoginData.Branch = TxtBranch.Text
        ObjLoginData.Allowed = ChkUserAllowed.Checked.ToString()
        ObjLoginData.Limited = ChkUserLimited.Checked.ToString()
        If ChkUserLimited.Checked = True Then
            ObjLoginData.ExDate = dtpExpDate.Text
        End If
        ObjLoginData.isAdmin = ChkUserAdmin.Checked.ToString
        ObjLoginData.Product = "FinExcel"
        ObjLoginData.Status = "out"
    End Sub
    ''' <summary>
    ''' btnSave_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>To Click update and save new dealer</remarks>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        SetData()

        REM end
        If CheckValidation() = False Then
            Exit Sub
        End If


        REM Insert or Update User Master table in database
        'Dim VarID As Integer = Val(ObjLoginData.ExecuteReturn("SELECT ID FROM UserMaster WHERE UserId='" & ObjLoginData._Userid & "'"))
        'If VarID = 0 Then
        ObjLoginData.Insert_User_Master()
        'Else
        'ObjLoginData.Update_Dealer_Master(lblDealerID.Text, txtDealerCode.Text, txtDealerName.Text.Trim, VarDealerGroupID, txtLocation.Text, txtPhoneNo.Text, Val(txtDepositAmount.Text), Val(txtMargin.Text), ChkIsImport.Checked, ChkIsHighLight.Checked)
        'End If
        REM End

        
        REM Update Dealer Master Datatable
        'Fatch And Merge Or ReExecute
        DTUserMasterde = ObjLoginData.Select_User_Master(False)
        DTUserMasterEn = ObjLoginData.Select_User_Master(True)
        REM End


        MsgBox("User Save Successfully ", MsgBoxStyle.Information)
        WriteLog("Save/Edit User='" & txtUserId.Text & "' information by " & GVar_LogIn_User & "")
        Call btnCancel_Click(sender, e)
        'Call FillDlrDatatable()
        Call Fill_Datagrid()
        Call btnNew_Click(sender, e)
        'txtUserId.Focus()
    End Sub

    ''' <summary>
    ''' DGDealerMaster_DoubleClick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>While double-click on Datagrid to display selected dealer into textbox</remarks>
    Private Sub DGDealerMaster_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGUserMaster.DoubleClick
        If DGUserMaster.SelectedRows.Count > 0 Then
            Call btnCancel_Click(sender, e)
            Call Fill_TextBox(DGUserMaster.SelectedRows(0).Cells("UserId").Value)
        Else
            MsgBox("No User Available !!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    ''' <summary>
    ''' Fill_TextBox
    ''' </summary>
    ''' <param name="VarUserId"></param>
    ''' <returns></returns>
    ''' <remarks>According to passing Dealer Code to display dealer info. into textbox</remarks>
    Private Function Fill_TextBox(ByVal VarUserId As String) As Boolean
        Dim Dt As New DataTable
        Dt = New DataView(DTUserMasterde, "F2='" & VarUserId & "'", "", DataViewRowState.CurrentRows).ToTable
        txtUserId.Text = VarUserId
        If VarUserId <> "" Then
            For i As Integer = 0 To DGUserMaster.RowCount - 1
                If DGUserMaster.Rows(i).Cells("UserId").Value = VarUserId Then
                    DGUserMaster.Rows(i).Selected = True
                    DGUserMaster.FirstDisplayedScrollingRowIndex = i
                    Exit For
                End If
            Next
        End If
        If Dt.Rows.Count > 0 Then
            txtUserId.Text = Dt.Rows(0).Item("F2").ToString
            TxtPwd.Text = Dt.Rows(0).Item("F3").ToString
            txtUserName.Text = Dt.Rows(0).Item("F4").ToString
            TxtBranch.Text = Dt.Rows(0).Item("F5").ToString
            Try
                ChkUserAllowed.Checked = CBool(Dt.Rows(0).Item("F7").ToString)
            Catch ex As Exception
                MsgBox("Invalid Value Stored in Allowed.")
            End Try
            Try
                ChkUserLimited.Checked = CBool(Dt.Rows(0).Item("F8").ToString)
            Catch ex As Exception
                MsgBox("Invalid Value Stored in Limited.")
            End Try

            dtpExpDate.Value = CDate(Dt.Rows(0).Item("F9").ToString)

            Try
                ChkUserAdmin.Checked = CBool(Dt.Rows(0).Item("F11").ToString)
            Catch ex As Exception
                MsgBox("Invalid Value Stored in IsAdmin.")
            End Try


            txtUserId.Focus()
            Return True
        Else
            txtUserId.Focus()
            Return False
        End If
    End Function

    ''' <summary>
    ''' txtSearchDealerCode_TextChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>according to filter text to filter datagrid</remarks>
    Private Sub txtSearchDealerCode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSearchUserId.TextChanged, txtSearchUserName.TextChanged, txtSearchBranch.TextChanged
        Call Fill_Datagrid()
    End Sub

    ''' <summary>
    ''' DGDealerMaster_KeyDown
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>On Delete key keystroke to check first any trade available for that dealer after delete that dealer</remarks>
    Private Sub DGDealerMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGUserMaster.KeyDown
        If e.KeyCode = Keys.Delete Then
            If DGUserMaster.SelectedRows.Count = 0 Then Exit Sub
            If MsgBox("Are you sure to delete this User ??", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
            Dim VarIsDeleteRecord As Boolean = False
            For i As Integer = 0 To DGUserMaster.SelectedRows.Count - 1
                ObjLoginData.Delete_User_Master(DGUserMaster.SelectedRows(i).Cells("userid").Value.ToString)
                WriteLog("Delete User='" & DGUserMaster.SelectedRows(i).Cells("userid").Value.ToString & "' by " & GVar_LogIn_User & "")
            Next

            If VarIsDeleteRecord = True Then
                Call Fill_Datagrid()
                Call btnCancel_Click(sender, e)
            End If
        End If
    End Sub


    ''' <summary>
    ''' btnNew_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Click to new dealer add</remarks>
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Call btnCancel_Click(sender, e)
        'txtDealerCode.ReadOnly = False
        'txtDealerCode.TabStop = True
        txtUserId.Focus()
        btnNew.Enabled = False
    End Sub

#Region "Import & Export Grid"
    ''' <summary>
    ''' btnExport_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>On Click to Export all dealer into Excel file</remarks>
    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim savedi As New SaveFileDialog
        savedi.Filter = "Files(*.XLS)|*.XLS"
        If savedi.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim grd(0) As DataGridView
            grd(0) = DGUserMaster
            Dim sname(0) As String
            sname(0) = "Dealer Master"
            Dim ArrColList As New ArrayList REM To Add Column name to export into excel file
            ArrColList.Add("Dealer_Code")
            ArrColList.Add("Dealer_Name")
            ArrColList.Add("Dealer_Group_Name")
            ArrColList.Add("Location")
            ArrColList.Add("Phone")
            ArrColList.Add("DepositAmount")
            ArrColList.Add("Margin")
            ArrColList.Add("IsImport")
            ArrColList.Add("IsHighLight")
            Call exporttoexcel(grd, savedi.FileName, sname, "FO", ArrColList)
        End If
    End Sub

    ''' <summary>
    ''' Check_DealerLimit
    ''' </summary>
    ''' <param name="DtDlr"></param>
    ''' <returns></returns>
    ''' <remarks>This method call to check Dealer License</remarks>
    Private Function Check_DealerLimit(ByVal DtDlr As DataTable) As Boolean
        'Dim ArrNewDlr As New ArrayList
        'For Each Dr As DataRow In DtDlr.Rows
        '    For Each StrDlr As String In Dr("Dealer_Code").ToString.Split(",")
        '        If Ght_DealerCode.Contains(StrDlr) = False Then
        '            ArrNewDlr.Add(StrDlr)
        '        End If
        '    Next
        'Next
        'Return GFun_CheckLicDealerCount(Ght_DealerCode.Count + ArrNewDlr.Count)
    End Function

    ''' <summary>
    ''' btnImport_Click
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>On Click to import dealer and dealer group from excel file</remarks>
    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '        Dim Varfpath As String
        '        Dim opfile As OpenFileDialog
        '        opfile = New OpenFileDialog
        '        opfile.Filter = "Files(*.xls)|*.xls"
        '        If opfile.ShowDialog() <> Windows.Forms.DialogResult.OK Then
        '            Exit Sub
        '        End If
        '        Dim VarMaxDealer_ID As Integer = Val(G_DTUserMaster.Compute("MAX(Dealer_ID)", "").ToString)
        '        Dim VarMaxGroup_ID As Integer = Val(G_DTDealerGroupMaster.Compute("MAX(Dealer_Group_ID)", "").ToString)
        '        If VarMaxGroup_ID < 0 Then
        '            VarMaxGroup_ID = 0
        '        End If

        '        Varfpath = opfile.FileName

        '        Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" & Varfpath
        '        Dim objConn As New OleDb.OleDbConnection(sConnectionString)
        '        objConn.Open()
        '        Dim StrQuery As String = "SELECT [Dealer Code] AS Dealer_Code,[Dealer Name] AS Dealer_Name,[Dealer Group] AS Dealer_Group_Name,[Location],[Phone No#] AS Phone,[Deposit Amt] AS DepositAmount,[Margin (Lac)] AS Margin,[High Light] AS HighLight,[Import Data] AS Import_Data FROM [Dealer Master$] WHERE [Dealer Code]<>''"
        '        Dim objCmdSelect As New OleDb.OleDbCommand(StrQuery, objConn)
        '        Dim objAdapter1 As New OleDb.OleDbDataAdapter
        '        Dim Dttempdata As New DataTable
        '        objAdapter1.SelectCommand = objCmdSelect
        '        Try
        '            objAdapter1.Fill(Dttempdata)
        '        Catch ex As Exception
        '            If ex.Message.ToUpper.Contains("'Dealer Master$' IS NOT A VALID NAME") Then
        '                MsgBox("Invalid Dealer Master Excel File Format !!", MsgBoxStyle.Critical)
        '                Me.Cursor = Cursors.Default
        '                Exit Sub
        '            Else
        '                MsgBox(ex.Message)
        '                Me.Cursor = Cursors.Default
        '                Exit Sub
        '            End If
        '        Finally
        '            objConn.Close()
        '        End Try

        '        REM This Block use to Checking No. of Dealer in License
        '        If Check_DealerLimit(Dttempdata) = False Then
        '            Exit Sub
        '        End If
        '        REM ENd

        '        For Each Dr As DataRow In Dttempdata.Rows
        '            REM New Dealer Group Add into Dealer Group Master
        '            If Dr("Dealer_Group_Name").ToString <> "" Then
        '                Dim DrGroup() As DataRow = G_DTDealerGroupMaster.Select("Dealer_Group_Name='" & Dr("Dealer_Group_Name") & "'")
        '                If DrGroup.Length = 0 Then
        '                    VarMaxGroup_ID += 1
        '                    ReDim DrGroup(0)
        '                    DrGroup(0) = G_DTDealerGroupMaster.NewRow
        '                    DrGroup(0)("Dealer_Group_ID") = VarMaxGroup_ID
        '                    DrGroup(0)("Dealer_Group_Name") = Dr("Dealer_Group_Name")
        '                    DrGroup(0)("Mgt_Level_ID") = -1
        '                    G_DTDealerGroupMaster.Rows.Add(DrGroup(0))
        '                    ObjTrad.Insert_Dealer_Group_Master(DrGroup(0)("Dealer_Group_ID"), DrGroup(0)("Dealer_Group_Name"), DrGroup(0)("Mgt_Level_ID"), "")
        '                End If
        '            End If
        '            REM End

        '            REM Insert & Update Row into Dealer Master
        '            Dim VarDealerCode As String = ""
        '            VarDealerCode = Trim(Dr("Dealer_Code"))
        '            Dim DrDlrRow() As DataRow = G_DTUserMaster.Select("Dealer='" & VarDealerCode & "'")
        '            If DrDlrRow.Length = 0 Then
        '                REM This Block use to Checking Any Duplicate Dealer Exist or Not
        '                For cnt As Integer = 0 To Trim(Dr("Dealer_Code")).Split(",").Length - 1
        '                    'If Ght_DealerCode.Contains(Trim(Dr("Dealer_Code")).Split(",")(cnt)) = True Then
        '                    '    MsgBox("Dealer Code '" & Trim(Dr("Dealer_Code")).Split(",")(cnt) & "' Already Exist !!", MsgBoxStyle.Exclamation)
        '                    '    GoTo lblSkip
        '                    'End If
        '                Next
        '                REM End
        '                ReDim DrDlrRow(0)
        '                VarMaxDealer_ID += 1

        '                REM Add New Dealer Row into Dealer Master datatable
        '                DrDlrRow(0) = G_DTUserMaster.NewRow
        '                DrDlrRow(0)("ID") = VarMaxDealer_ID
        '                DrDlrRow(0)("Dealer_ID") = VarMaxDealer_ID
        '                DrDlrRow(0)("Dealer") = VarDealerCode
        '                DrDlrRow(0)("Dealer_Name") = Dr("Dealer_Name").ToString
        '                Dim VarDealer_Group_ID As Integer = G_DTDealerGroupMaster.Compute("MAX(Dealer_Group_ID)", "Dealer_Group_Name='" & Dr("Dealer_Group_Name").ToString & "'")
        '                DrDlrRow(0)("Dealer_Group_ID") = VarDealer_Group_ID
        '                DrDlrRow(0)("Dealer_Group_Name") = Dr("Dealer_Group_Name").ToString
        '                DrDlrRow(0)("Location") = Dr("Location").ToString
        '                DrDlrRow(0)("Phone") = Dr("Phone").ToString
        '                DrDlrRow(0)("DepositAmount") = Val(Dr("DepositAmount").ToString)
        '                DrDlrRow(0)("margin") = Val(Dr("margin").ToString)
        '                DrDlrRow(0)("IsHighLight") = Dr("HighLight")
        '                DrDlrRow(0)("IsImport") = Dr("Import_Data")
        '                G_DTUserMaster.Rows.Add(DrDlrRow(0))
        '                ObjTrad.Insert_Dealer_Master(DrDlrRow(0)("Dealer_ID"), DrDlrRow(0)("Dealer"), DrDlrRow(0)("Dealer_Name"), DrDlrRow(0)("Dealer_Group_ID"), DrDlrRow(0)("Location"), DrDlrRow(0)("Phone"), DrDlrRow(0)("DepositAmount"), DrDlrRow(0)("margin"), DrDlrRow(0)("IsImport"), DrDlrRow(0)("IsHighLight"))
        '                REM ENd
        '            Else
        '                REM Update Existing Dealer Row into Dealer Master
        '                DrDlrRow(0)("Dealer_Name") = Dr("Dealer_Name").ToString
        '                Dim VarDealer_Group_ID As Integer = G_DTDealerGroupMaster.Compute("MAX(Dealer_Group_ID)", "Dealer_Group_Name='" & Dr("Dealer_Group_Name").ToString & "'")
        '                DrDlrRow(0)("Dealer_Group_ID") = VarDealer_Group_ID
        '                DrDlrRow(0)("Dealer_Group_Name") = Dr("Dealer_Group_Name").ToString
        '                DrDlrRow(0)("Location") = Dr("Location").ToString
        '                DrDlrRow(0)("Phone") = Dr("Phone").ToString
        '                DrDlrRow(0)("DepositAmount") = Val(Dr("DepositAmount").ToString)
        '                DrDlrRow(0)("margin") = Val(Dr("margin").ToString)
        '                DrDlrRow(0)("IsHighLight") = Dr("HighLight")
        '                DrDlrRow(0)("IsImport") = Dr("Import_Data")
        '                ObjTrad.Update_Dealer_Master(DrDlrRow(0)("Dealer_ID"), DrDlrRow(0)("Dealer"), DrDlrRow(0)("Dealer_Name"), DrDlrRow(0)("Dealer_Group_ID"), DrDlrRow(0)("Location"), DrDlrRow(0)("Phone"), DrDlrRow(0)("DepositAmount"), DrDlrRow(0)("margin"), DrDlrRow(0)("IsImport"), DrDlrRow(0)("IsHighLight"))
        '                REM End
        '            End If
        '            REM End
        'lblSkip:
        '        Next
        '        Call Fill_Datagrid()
        '        'Call GSub_SetCheckDealerStr()
        '        MsgBox("Dealer file import successfully  ", MsgBoxStyle.Information)
    End Sub
#End Region

#Region "Restict Dealer Code Text-box from Enter Keys"

    ''' <summary>
    ''' txtDealerCode_KeyPress
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Enter key not allow in Dealer code Textbox</remarks>
    Private Sub txtDealerCode_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(Keys.Enter) Or e.KeyChar = Chr(Keys.Return) Then
            e.KeyChar = Chr(0)
            e.Handled = True
        End If
    End Sub

#End Region

    Private Sub TableLayoutPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs)

    End Sub

    Private Sub btnReSetPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReSetPass.Click
        SetData()
        ObjLoginData.ReSetPassword()
        DTUserMasterde = ObjLoginData.Select_User_Master(False)
        DTUserMasterEn = ObjLoginData.Select_User_Master(True)
        MsgBox("Password reset successfully " & vbCrLf & "For User:" & txtUserId.Text & "", MsgBoxStyle.Information)
        WriteLog("Password reset successfully User='" & txtUserId.Text & "' by " & GVar_LogIn_User & "")
        Call Fill_Datagrid()
    End Sub

    Private Sub DGUserMaster_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGUserMaster.CellContentClick

    End Sub
End Class