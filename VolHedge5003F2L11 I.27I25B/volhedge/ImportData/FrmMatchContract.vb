Imports System.Configuration
Imports System.IO

Public Class FrmMatchContract

    Dim objTrade As New trading
    Dim curfo As String = ""
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        If MsgBox("Are you sure to Update All Trade With New ExpiryDate?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        REM Reset Password
        'ResetPassword()

        'backup old database
        Dim str_folder_path As String
        str_folder_path = System.Windows.Forms.Application.StartupPath() & "\backup_" & Format(Now(), "ddMMyy")
        If Not Directory.Exists(str_folder_path) Then
            Directory.CreateDirectory(str_folder_path)
        End If
        Dim str As String = ConfigurationSettings.AppSettings("dbname")
        Dim _connection_string As String = System.Windows.Forms.Application.StartupPath() & "" & str

        Dim cur_date_str As String
        cur_date_str = Format(Now, "ddMMMyyyy_HHmm")
        Dim tstr As String = str_folder_path & "\greek_backup_" & cur_date_str & ".mdb"

        FileCopy(_connection_string, tstr)


        For Each DGVitem As DataGridViewRow In DGV_MatchCon.Rows
            If Not IsDate(DGVitem.Cells("nmdate").Value) Then
                MsgBox("invalid date value inserted.")
                Return
            End If
        Next



        For Each DGVitem As DataGridViewRow In DGV_MatchCon.Rows

            If IsDate(DGVitem.Cells("nmdate").Value) Then
                If curfo = "FO" Then

                    objTrade.Exec_Qry("Update trading Set mdate = #" & CDate(DGVitem.Cells("nmdate").Value) & "#  Where mdate=#" & CDate(DGVitem.Cells("mdate").Value) & "#")
                    Dim j As Long = 0
                    For i As Integer = 0 To 100000
                        j = j + i
                    Next
                    objTrade.Exec_Qry("Update trading Set script = IIf(trading.instrumentname='FUTIDX',trading.instrumentname + '  ' + trading.company + '  ' + Format([mdate],'ddmmmyyyy'), " & _
                        " trading.instrumentname + '  ' + trading.company + '  ' + Format([mdate],'ddmmmyyyy') + '  ' + Format(trading.strikerate,'0.00') + '  ' + iif(trading.cp='C','CE','PE')) Where mdate=#" & CDate(DGVitem.Cells("nmdate").Value) & "#")

                    For i As Integer = 0 To 100000
                        j = j + i
                    Next

                    objTrade.Exec_Qry("Delete From Expense_Data Where exp_date = #" & CDate(DGVitem.Cells("mdate").Value) & "#")

                ElseIf curfo = "CUR" Then
                    objTrade.Exec_Qry("Update Currency_trading Set mdate = #" & CDate(DGVitem.Cells("nmdate").Value) & "#  Where mdate=#" & CDate(DGVitem.Cells("mdate").Value) & "#")
                    Dim j As Integer = 0
                    For i As Integer = 0 To 100000
                        j = j + i
                    Next
                    objTrade.Exec_Qry("Update Currency_trading Set script = IIf(instrumentname='FUTIDX',instrumentname + '  ' + company + '  ' + UCase(Format([mdate],'ddmmmyyyy')), " & _
                        " instrumentname + '  ' + company + '  ' + UCase(Format([mdate],'ddmmmyyyy') + '  ' + Format(strikerate,'0.00') + '  ' + iif(cp='C','CE','PE')  )) Where mdate=#" & CDate(DGVitem.Cells("nmdate").Value) & "#")
                    For i As Integer = 0 To 100000
                        j = j + i
                    Next

                    objTrade.Exec_Qry("Delete From Expense_Data Where exp_date = #" & CDate(DGVitem.Cells("mdate").Value) & "#")
                End If

            End If

        Next

        'Dim TradStr As String = "SELECT trading.uid, trading.script, trading.mdate, " & _
        '  " IIf(trading.instrumentname='FUTIDX',trading.instrumentname + '  ' + trading.company + '  ' + UCase(Format([mdate],'ddmmmyyyy')), " & _
        '  " trading.instrumentname + '  ' + trading.company + '  ' + UCase(Format([mdate],'ddmmmyyyy') + '  ' + Format(trading.strikerate,'0.00') + '  ' + iif(trading.cp='C','CE','PE')  ))  AS NewScrip, trading.instrumentname, trading.company, UCase(Format([mdate],'ddmmmyyyy')) AS M, Format(trading.strikerate,'0.00') AS Expr1, trading.cp, Contract.script, Contract.OScript " & _
        '  " FROM trading LEFT JOIN Contract ON trading.script = Contract.script WHERE (((Contract.script) Is Null));"

        'Dim ExpStr As String = "SELECT Expense_Data.entry_date, Expense_Data.company, Expense_Data.script, Expense_Data.exp_date, Expense_Data.expense, Contract.script " & _
        '" FROM Expense_Data LEFT JOIN Contract ON Expense_Data.script = Contract.script WHERE (((Contract.script) Is Null));"
        MsgBox("Successfully Updated." & vbCrLf & "Now Click Ok And Restart Application.")
        End
    End Sub

    Public Sub ShowForm(ByVal focur As String)
        curfo = focur
        Me.ShowDialog()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub FrmMatchContract_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dt As New DataTable
        Dim dt1 As New DataTable

        Dim Str As String = ""
        If curfo = "FO" Then
            dt1 = objTrade.select_Query("SELECT '01/' & Format(Min(DateAdd('s',[expiry_date],CDate('1-1-1980'))),'MMM/yy') AS Mindate FROM Contract;")

            Str = "SELECT DISTINCT trading.mdate, '' AS nmdate FROM trading LEFT JOIN Contract ON trading.script = Contract.script WHERE (((Contract.script) Is Null)) And trading.mdate > #" & dt1.Rows(0)("Mindate") & "#;"
        ElseIf curfo = "CUR" Then
            dt1 = objTrade.select_Query("SELECT '01/' & Format(Min(DateAdd('s',[expiry_date],CDate('1-1-1980'))),'MMM/yy') AS Mindate FROM Currency_Contract;")
            Str = "SELECT DISTINCT Currency_trading.mdate, '' AS nmdate FROM Currency_trading LEFT JOIN Currency_Contract ON Currency_trading.script = Currency_Contract.script WHERE (((Currency_Contract.script) Is Null)) And Currency_trading.mdate > #" & dt1.Rows(0)("Mindate") & "#;"
        End If

        dt = objTrade.select_Query(Str)

        DGV_MatchCon.DataSource = dt

    End Sub
End Class