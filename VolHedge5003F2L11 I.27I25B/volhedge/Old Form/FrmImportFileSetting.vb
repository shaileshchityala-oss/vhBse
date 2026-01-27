Public Class FrmImportFileSetting
    Dim objTrad As trading = New trading
    Dim DtSetting As New DataTable
    Private Sub ChkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        ChkGetsEQ.Checked = chkAll.Checked
        ChkGetsFO.Checked = chkAll.Checked
        ChkOdinEQ.Checked = chkAll.Checked
        ChkOdinFO.Checked = chkAll.Checked
        ChkNowEQ.Checked = chkAll.Checked
        ChkNowFO.Checked = chkAll.Checked
        chkNotisEq.Checked = chkAll.Checked
        chkNotisFo.Checked = chkAll.Checked
        ChkNeatFO.Checked = chkAll.Checked
        ChkGetsDatabase.Checked = chkAll.Checked
        chkNSEEq.Checked = chkAll.Checked
        chkNSEFo.Checked = chkAll.Checked

        ChkGetsEQm.Checked = chkAll.Checked
        ChkGetsFOm.Checked = chkAll.Checked
        ChkOdinEQm.Checked = chkAll.Checked
        ChkOdinFOm.Checked = chkAll.Checked
        ChkNowEQm.Checked = chkAll.Checked
        ChkNowFOm.Checked = chkAll.Checked
        chkNotisEqm.Checked = chkAll.Checked
        chkNotisFom.Checked = chkAll.Checked
        ChkNeatFOm.Checked = chkAll.Checked
        ChkGetsDatabasem.Checked = chkAll.Checked
        chkNSEEqM.Checked = chkAll.Checked
        chkNSEFoM.Checked = chkAll.Checked

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub FillDataToTextBox()

        DtSetting = objTrad.Settings
        With DtSetting
            For Each DR As DataRow In DtSetting.Rows
                Select Case DR("SETTINGNAME").ToString.ToUpper
                    Case "NEAT FO FILE PATH"
                        txtNeatFoFilePath.Text = DR("SETTINGKEY").ToString
                    Case "NEAT FO FILE CODE"
                        txtNeatFoFileCode.Text = DR("SETTINGKEY").ToString
                    Case "NEAT FO FILE FORMAT"
                        lblNeatFileFormat.Text = DR("SETTINGKEY").ToString

                    Case "GETS FO FILE PATH"
                        txtGetsFoFilePath.Text = DR("SETTINGKEY").ToString
                    Case "GETS FO FILE FORMAT"
                        lblGetsFOFileFormat.Text = DR("SETTINGKEY").ToString
                    Case "GETS EQ FILE PATH"
                        txtGetsEqFilePath.Text = DR("SETTINGKEY").ToString
                    Case "GETS EQ FILE FORMAT"
                        lblGetsEQFileFormat.Text = DR("SETTINGKEY").ToString

                    Case "ODIN FO FILE PATH"
                        txtodinFoFilePath.Text = DR("SETTINGKEY").ToString
                    Case "ODIN FO FILE FORMAT"
                        lblOdinFOFileFormat.Text = DR("SETTINGKEY").ToString
                    Case "ODIN EQ FILE PATH"
                        txtOdinEqFilePath.Text = DR("SETTINGKEY").ToString
                    Case "ODIN EQ FILE FORMAT"
                        lblOdinEQFileFormat.Text = DR("SETTINGKEY").ToString

                    Case "NOW FO FILE PATH"
                        txtNowFOFilePath.Text = DR("SETTINGKEY").ToString
                    Case "NOW FO FILE FORMAT"
                        lblNowFOFileFormat.Text = DR("SETTINGKEY").ToString
                    Case "NOW EQ FILE PATH"
                        txtNowEQFilePath.Text = DR("SETTINGKEY").ToString
                    Case "NOW EQ FILE FORMAT"
                        lblNowEQFileFormat.Text = DR("SETTINGKEY").ToString

                    Case "NOTIS FO FILE PATH"
                        txtNotisFoFilePath.Text = DR("SETTINGKEY").ToString
                    Case "NOTIS FO FILE FORMAT"
                        lblNotisFOFormat.Text = DR("SETTINGKEY").ToString
                    Case "NOTIS EQ FILE PATH"
                        txtNotisEQFilePath.Text = DR("SETTINGKEY").ToString
                    Case "NOTIS EQ FILE FORMAT"
                        lblNotisEQFormat.Text = DR("SETTINGKEY").ToString


                    Case "GETS SERVER NAME"
                        txtGETSServerName.Text = DR("SETTINGKEY").ToString
                    Case "GETS DATABASE NAME"
                        txtGETSDatabase.Text = DR("SETTINGKEY").ToString
                    Case "GETS USER NAME"
                        txtGETSUserName.Text = DR("SETTINGKEY").ToString
                    Case "GETS PASSWORD"
                        txtGETSPassword.Text = DR("SETTINGKEY").ToString
                 

                    Case "ODIN SERVER NAME"
                        txtODINServerName.Text = DR("SETTINGKEY").ToString
                    Case "ODIN DATABASE NAME"
                        txtODINDatabase.Text = DR("SETTINGKEY").ToString
                    Case "ODIN USER NAME"
                        txtODINUserName.Text = DR("SETTINGKEY").ToString
                    Case "ODIN PASSWORD"
                        txtODINPassword.Text = DR("SETTINGKEY").ToString


                        ''Check Box Setting Apply
                    Case "AUTO NEAT FO"
                        ChkNeatFO.Checked = DR("SETTINGKEY")
                    Case "AUTO GETS FO"
                        ChkGetsFO.Checked = DR("SETTINGKEY")
                    Case "AUTO GETS EQ"
                        ChkGetsEQ.Checked = DR("SETTINGKEY")
                    Case "AUTO ODIN FO"
                        ChkOdinFO.Checked = DR("SETTINGKEY")
                    Case "AUTO ODIN EQ"
                        ChkOdinEQ.Checked = DR("SETTINGKEY")
                    Case "AUTO NOW FO"
                        ChkNowFO.Checked = DR("SETTINGKEY")
                    Case "AUTO NOW EQ"
                        ChkNowEQ.Checked = DR("SETTINGKEY")
                    Case "AUTO NOTIS FO"
                        chkNotisFo.Checked = DR("SETTINGKEY")
                    Case "AUTO NOTIS EQ"
                        chkNotisEq.Checked = DR("SETTINGKEY")
                    Case "AUTO GETS DATABASE"
                        ChkGetsDatabase.Checked = DR("SETTINGKEY")
                    Case "AUTO ODIN DATABASE"
                        ChkODINDatabase.Checked = DR("SETTINGKEY")

                        ''MenualCheck Box Setting Apply
                    Case "AUTO NEAT FOM"
                        ChkNeatFOm.Checked = DR("SETTINGKEY")
                    Case "AUTO GETS FOM"
                        ChkGetsFOm.Checked = DR("SETTINGKEY")
                    Case "AUTO GETS EQM"
                        ChkGetsEQm.Checked = DR("SETTINGKEY")
                    Case "AUTO ODIN FOM"
                        ChkOdinFOm.Checked = DR("SETTINGKEY")
                    Case "AUTO ODIN EQM"
                        ChkOdinEQm.Checked = DR("SETTINGKEY")
                    Case "AUTO NOW FOM"
                        ChkNowFOm.Checked = DR("SETTINGKEY")
                    Case "AUTO NOW EQM"
                        ChkNowEQm.Checked = DR("SETTINGKEY")
                    Case "AUTO NOTIS FOM"
                        chkNotisFom.Checked = DR("SETTINGKEY")
                    Case "AUTO NOTIS EQM"
                        chkNotisEqm.Checked = DR("SETTINGKEY")
                    Case "AUTO GETS DATABASEM"
                        ChkGetsDatabasem.Checked = DR("SETTINGKEY")
                    Case "AUTO ODIN DATABASEM"
                        ChkODINDatabasem.Checked = DR("SETTINGKEY")
                    Case "NSE_CCODE"
                        txtccode.Text = DR("SETTINGKEY")
                    Case "REFRESH_TIME"
                        txtreftime.Text = DR("SETTINGKEY")


                    Case "NSE_FILE_NAME_FO"
                        lblNseFOFormat.Text = DR("SETTINGKEY").ToString
                    Case "NSE_FILE_NAME_EQ"
                        lblNseEQFormat.Text = DR("SETTINGKEY").ToString
                    Case "NSE_FILE_PATH_FO"
                        txtNSEFoFilePath.Text = DR("SETTINGKEY").ToString
                    Case "NSE_FILE_PATH_EQ"
                        txtNSEEQFilePath.Text = DR("SETTINGKEY").ToString
                    Case "AUTO NSE FO"
                        chkNSEFo.Checked = DR("SETTINGKEY")
                    Case "AUTO NSE EQ"
                        chkNSEFo.Checked = DR("SETTINGKEY")
                   
                    Case "AUTO NSE FOM"
                        chkNSEFoM.Checked = DR("SETTINGKEY")
                    Case "AUTO NSE EQM"
                        chkNSEEqM.Checked = DR("SETTINGKEY")


                End Select
            Next

        End With

    End Sub
    Private Sub SaveData()
        Try

        If txtreftime.Text.Trim = "" Then
                txtreftime.Text = 30
            End If
            If txtreftime.Text <= 0 Then
                MsgBox("You cannot enter refresh timer value less then zero or zero")
                txtreftime.Text = 30
                txtreftime.Focus()
                Exit Sub
            End If
            For Each DR As DataRow In DtSetting.Rows
                Dim setting_name As String = DR("SettingName").ToString.ToUpper
                Dim setting_key As String = "Nothing"
                Select Case DR("SettingName").ToString.ToUpper
                    Case "NEAT FO FILE PATH"
                        setting_key = txtNeatFoFilePath.Text
                    Case "NEAT FO FILE CODE"
                        setting_key = txtNeatFoFileCode.Text
                    Case "NEAT FO FILE FORMAT"
                        setting_key = lblNeatFileFormat.Text

                    Case "GETS FO FILE PATH"
                        setting_key = txtGetsFoFilePath.Text
                    Case "GETS FO FILE FORMAT"
                        setting_key = lblGetsFOFileFormat.Text
                    Case "GETS EQ FILE PATH"
                        setting_key = txtGetsEqFilePath.Text
                    Case "GETS EQ FILE FORMAT"
                        setting_key = lblGetsEQFileFormat.Text

                    Case "ODIN FO FILE PATH"
                        setting_key = txtodinFoFilePath.Text
                    Case "ODIN FO FILE FORMAT"
                        setting_key = lblOdinFOFileFormat.Text
                    Case "ODIN EQ FILE PATH"
                        setting_key = txtOdinEqFilePath.Text
                    Case "ODIN EQ FILE FORMAT"
                        setting_key = lblOdinEQFileFormat.Text

                    Case "NOW FO FILE PATH"
                        setting_key = txtNowFOFilePath.Text
                    Case "NOW FO FILE FORMAT"
                        setting_key = lblNowFOFileFormat.Text
                    Case "NOW EQ FILE PATH"
                        setting_key = txtNowEQFilePath.Text
                    Case "NOW EQ FILE FORMAT"
                        setting_key = lblNowEQFileFormat.Text


                    Case "NOTIS FO FILE PATH"
                        setting_key = txtNotisFoFilePath.Text
                    Case "NOTIS FO FILE FORMAT"
                        setting_key = lblNotisFOFormat.Text
                    Case "NOTIS EQ FILE PATH"
                        setting_key = txtNotisEQFilePath.Text
                    Case "NOTIS EQ FILE FORMAT"
                        setting_key = lblNotisEQFormat.Text

                    Case "GETS SERVER NAME"
                        setting_key = txtGETSServerName.Text
                    Case "GETS DATABASE NAME"
                        setting_key = txtGETSDatabase.Text
                    Case "GETS USER NAME"
                        setting_key = txtGETSUserName.Text
                    Case "GETS PASSWORD"
                        setting_key = txtGETSPassword.Text
                    Case "ODIN SERVER NAME"
                        setting_key = txtODINServerName.Text
                    Case "ODIN DATABASE NAME"
                        setting_key = txtODINDatabase.Text
                    Case "ODIN USER NAME"
                        setting_key = txtODINUserName.Text
                    Case "ODIN PASSWORD"
                        setting_key = txtODINPassword.Text

                        ''Check Box Setting Apply
                    Case "AUTO NEAT FO"
                        setting_key = ChkNeatFO.Checked
                    Case "AUTO GETS FO"
                        setting_key = ChkGetsFO.Checked
                    Case "AUTO GETS EQ"
                        setting_key = ChkGetsEQ.Checked
                    Case "AUTO ODIN FO"
                        setting_key = ChkOdinFO.Checked
                    Case "AUTO ODIN EQ"
                        setting_key = ChkOdinEQ.Checked
                    Case "AUTO NOW FO"
                        setting_key = ChkNowFO.Checked
                    Case "AUTO NOW EQ"
                        setting_key = ChkNowEQ.Checked
                    Case "AUTO GETS DATABASE"
                        setting_key = ChkGetsDatabase.Checked
                    Case "AUTO ODIN DATABASE"
                        setting_key = ChkODINDatabase.Checked
                    Case "NSE_CCODE"
                        setting_key = txtccode.Text
                    Case "AUTO NOTIS FO"
                        setting_key = chkNotisFo.Checked
                    Case "AUTO NOTIS EQ"
                        setting_key = chkNotisEq.Checked

                        'MENUAL CHECK BOX SETTING APPLY
                    Case "AUTO NEAT FOM"
                        setting_key = ChkNeatFOm.Checked
                    Case "AUTO GETS FOM"
                        setting_key = ChkGetsFOm.Checked
                    Case "AUTO GETS EQM"
                        setting_key = ChkGetsEQm.Checked
                    Case "AUTO ODIN FOM"
                        setting_key = ChkOdinFOm.Checked
                    Case "AUTO ODIN EQM"
                        setting_key = ChkOdinEQm.Checked
                    Case "AUTO NOW FOM"
                        setting_key = ChkNowFOm.Checked
                    Case "AUTO NOW EQM"
                        setting_key = ChkNowEQm.Checked
                    Case "AUTO GETS DATABASEM"
                        setting_key = ChkGetsDatabasem.Checked
                    Case "AUTO ODIN DATABASEM"
                        setting_key = ChkODINDatabasem.Checked
                    Case "AUTO NOTIS FOM"
                        setting_key = chkNotisFom.Checked
                    Case "AUTO NOTIS EQM"
                        setting_key = chkNotisEqm.Checked
                    Case "REFRESH_TIME"
                        setting_key = txtreftime.Text

                    Case "NSE_FILE_NAME_FO"
                        setting_key = lblNseFOFormat.Text
                    Case "NSE_FILE_NAME_EQ"
                        setting_key = lblNseEQFormat.Text
                    Case "NSE_FILE_PATH_FO"
                        setting_key = txtNSEFoFilePath.Text
                    Case "NSE_FILE_PATH_EQ"
                        setting_key = txtNSEEQFilePath.Text
                    Case "AUTO NSE FO"
                        setting_key = chkNSEFo.Checked
                    Case "AUTO NSE EQ"
                        setting_key = chkNSEFo.Checked
                    Case "AUTO NSE FOM"
                        setting_key = chkNSEFoM.Checked
                    Case "AUTO NSE EQM"
                        setting_key = chkNSEEqM.Checked

                End Select
                If setting_key <> "Nothing" Then
                    objTrad.SettingName = setting_name
                    objTrad.SettingKey = setting_key
                    objTrad.Uid = CInt(DR("uid"))
                    objTrad.Update_setting()
                End If
            Next
            MsgBox("Setting Apply Successfully ", MsgBoxStyle.Information)
            Call Rounddata()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
    
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Call SaveData()
    End Sub
    Private Sub FrmImportFileSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call FillDataToTextBox()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtNeatFoFilePath.Text) = True Then
            opfile.SelectedPath = txtNeatFoFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtNeatFoFilePath.Text = str
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtGetsFoFilePath.Text) = True Then
            opfile.SelectedPath = txtGetsFoFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtGetsFoFilePath.Text = str
        End If
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtGetsEqFilePath.Text) = True Then
            opfile.SelectedPath = txtGetsEqFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtGetsEqFilePath.Text = str
        End If

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtodinFoFilePath.Text) = True Then
            opfile.SelectedPath = txtodinFoFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtodinFoFilePath.Text = str
        End If

    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtOdinEqFilePath.Text) = True Then
            opfile.SelectedPath = txtOdinEqFilePath.Text
        End If

        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtOdinEqFilePath.Text = str
        End If

    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtNowFOFilePath.Text) = True Then
            opfile.SelectedPath = txtNowFOFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtNowFOFilePath.Text = str
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtNowEQFilePath.Text) = True Then
            opfile.SelectedPath = txtNowEQFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtNowEQFilePath.Text = str
        End If
    End Sub

    Private Sub txtNeatFoFilePath_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNeatFoFilePath.LostFocus, txtGetsEqFilePath.LostFocus, txtGetsFoFilePath.LostFocus, txtNowEQFilePath.LostFocus, txtNowEQFilePath.LostFocus, txtOdinEqFilePath.LostFocus, txtodinFoFilePath.LostFocus
        If CType(sender, TextBox).Text = "" Then
            Exit Sub
        End If
        If System.IO.Directory.Exists((CType(sender, TextBox).Text)) = False Then
            MsgBox("Invalid File Path !!", MsgBoxStyle.Exclamation)
            CType(sender, TextBox).Text = ""
            CType(sender, TextBox).Focus()
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Me.Cursor = Cursors.WaitCursor
        Dim ConTest As New System.Data.SqlClient.SqlConnection(" Data Source=" & txtGETSServerName.Text & ";Initial Catalog=" & txtGETSDatabase.Text & ";User ID=" & txtGETSUserName.Text & ";Password=" & txtGETSPassword.Text)
        Try
            ConTest.Open()
            ConTest.Close()
            MsgBox("Test Connection Succeeded  ", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
        End Try
        ConTest.Dispose()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Me.Cursor = Cursors.WaitCursor
        Dim ConTest As New System.Data.SqlClient.SqlConnection(" Data Source=" & txtODINServerName.Text & ";Initial Catalog=" & txtODINDatabase.Text & ";User ID=" & txtODINUserName.Text & ";Password=" & txtODINPassword.Text)
        Try
            ConTest.Open()
            ConTest.Close()
            MsgBox("Test Connection Succeeded  ", MsgBoxStyle.Information)
        Catch ex As Exception
            MsgBox("Test Connection Fail !! ", MsgBoxStyle.Critical)
        End Try
        ConTest.Dispose()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtNotisFoFilePath.Text) = True Then
            opfile.SelectedPath = txtNotisFoFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtNotisFoFilePath.Text = str
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtNotisEQFilePath.Text) = True Then
            opfile.SelectedPath = txtNotisEQFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtNotisEQFilePath.Text = str
        End If
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtNSEFoFilePath.Text) = True Then
            opfile.SelectedPath = txtNSEFoFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtNSEFoFilePath.Text = str
        End If
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Dim opfile As New FolderBrowserDialog
        If System.IO.Directory.Exists(txtNSEEQFilePath.Text) = True Then
            opfile.SelectedPath = txtNSEEQFilePath.Text
        End If
        If opfile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim str As String = opfile.SelectedPath
            str = Mid(opfile.SelectedPath, Len(str), 1)
            If str = "\" Then
                str = Mid(opfile.SelectedPath, 1, Len(opfile.SelectedPath) - 1)
            Else
                str = opfile.SelectedPath
            End If
            txtNSEEQFilePath.Text = str
        End If
    End Sub
End Class