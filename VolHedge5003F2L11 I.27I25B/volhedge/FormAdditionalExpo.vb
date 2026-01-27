Imports System.Data.OleDb
Imports System.IO
Imports System.Threading

Public Class FormAdditionalExpo

    Dim AEL_Additional_Expo As New DataTable
    Dim objTrad As New trading

    Dim Thr_AELContract As Thread
    Dim AELContractPath As String
    Public Totalcnt As Integer

    Dim objTrad_Thr As New Cls_AEL
    Private Sub FormAdditionalExpo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        objTrad.createtable()
        init_AEL_Additional_Expo()
        fill_aelcontracts()
    End Sub
    Private Sub init_AEL_Additional_Expo()
        AEL_Additional_Expo = New DataTable
        With AEL_Additional_Expo.Columns
            .Add("uid")
            .Add("InsType")
            .Add("Symbol")
            .Add("Exp_Date")
            .Add("Strike_Price")
            .Add("Opt_Type")
            .Add("CA_Level")
            .Add("ELM_Per")
        End With
    End Sub
    Private Sub fill_aelcontracts()
        AEL_Additional_Expo.Rows.Clear()
        AEL_Additional_Expo = objTrad.AEL_Additional_expo()
        grdaelcontracts.DataSource = AEL_Additional_Expo
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        OpenFileDialog1.FileName = "*.csv"
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            Try

                AELContractPath = OpenFileDialog1.FileName
                grdaelcontracts.DataSource = Nothing
                AEL_Additional_Expo.Rows.Clear()
                'objTrad.delete_AEL_Additional_Expo()

                Thr_AELContract = New Thread(AddressOf ThrAELContracts)
                Thr_AELContract.Name = "ThrAELContract"
                Thr_AELContract.Start()

                TimerAel.Interval = 1000
                TimerAel.Enabled = True

            Catch ex As Exception
                '
            End Try

        End If
    End Sub

    Public Sub ThrAELContracts()
        Try



            Dim tempdata As New DataTable
            ' Dim fpath As String
            'fpath = OpenFileDialog1.FileName


            Dim fi As New FileInfo(AELContractPath)

            Dim sConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Text;Data Source=" & fi.DirectoryName

            Dim objConn As New OleDbConnection(sConnectionString)

            objConn.Open()

            DropExTbl(objConn, "Additional_AEL_expo")




            'Dim objCmdSelect As New OleDbCommand
            Dim objAdapter1 As New OleDbDataAdapter("SELECT * FROM " & fi.Name, objConn)
            'objAdapter1.SelectCommand = objCmdSelect

            tempdata = New DataTable
            objAdapter1.Fill(tempdata)
            objConn.Close()

            'Dim cnt As Integer

            Totalcnt = tempdata.Rows.Count

            If tempdata.Rows.Count > 0 Then

                'InsType, Symbol, ExpDate, StrikePrice, OptType, CALevel, ELMPer
                tempdata.Columns(0).ColumnName = "InsType"
                tempdata.Columns(1).ColumnName = "Symbol"
                tempdata.Columns(2).ColumnName = "ExpDate"
                tempdata.Columns(3).ColumnName = "StrikePrice"
                tempdata.Columns(4).ColumnName = "OptType"
                tempdata.Columns(5).ColumnName = "CALevel"
                tempdata.Columns(6).ColumnName = "ELMPer"

                tempdata.AcceptChanges()

                objTrad_Thr.insert_additional_expo(tempdata)

                MsgBox("Import Completed.", MsgBoxStyle.Information)
            Else
                MsgBox("Import Failed.", MsgBoxStyle.Critical)
            End If

        Catch ex As Exception

        End Try


    End Sub



    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerAel.Tick
        txtAELImported.Text = "Completed  " & objTrad_Thr.countAEL & "  Out of  " & Totalcnt
        If objTrad_Thr.countAEL = Totalcnt Then
            AEL_Additional_Expo.Rows.Clear()
            AEL_Additional_Expo = objTrad.AEL_Additional_expo()
            grdaelcontracts.DataSource = AEL_Additional_Expo

        End If

    End Sub
    Private Sub DropExTbl(ByVal objConn As OleDbConnection, ByVal sTbl As String)
        On Error Resume Next
        Dim objCmdSelect As New OleDbCommand("DELETE FROM " & sTbl & " ", objConn)
        objCmdSelect.ExecuteNonQuery()
    End Sub
End Class