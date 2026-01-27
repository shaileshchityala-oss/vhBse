Imports System
Imports System.Configuration
Imports System.IO
Imports System.Threading
Imports System.Net
Imports System.Data
'for export to excel
'Imports Microsoft.Office.Interop
Imports Microsoft.Office
Imports System.Runtime.InteropServices.Marshal
Imports VolHedge.OptionG
Imports System.Diagnostics

'Imports System.Data.OleDb
Imports System.Text
Imports VolHedge.DAL
Imports System.ComponentModel

Public Class frmGapUpDown
    Dim G_DtPositionwise_GAP As New DataTable
    Public DtExpDiff As New DataTable
    Public GVar_LTPPer1 As Double
    Public GVar_LTPPer2 As Double
    Public GVar_VolPer1 As Double
    Public GVar_VolPer2 As Double

    Public GVar_LTPPer3 As Double
    Public GVar_LTPPer4 As Double
    Public GVar_VolPer3 As Double
    Public GVar_VolPer4 As Double

    Public GVar_LTPPer5 As Double
    Public GVar_LTPPer6 As Double
    Public GVar_VolPer5 As Double
    Public GVar_VolPer6 As Double

    Dim mPerf As CPerfCheck = New CPerfCheck()
    Dim mStrdbg As String
    Public GVar_LTPPer7 As Double
    Public GVar_LTPPer8 As Double
    Public GVar_VolPer7 As Double
    Public GVar_VolPer8 As Double
    Dim objTrad As trading = New trading
    Private Sub btnrefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnrefresh.Click

        Refresh_Data()
    End Sub
    'Private Sub cal_projMtm_vol(ByVal futval As Double, ByVal stkprice As Double, ByVal mT As Integer, ByVal mmT As Integer, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal drow As DataRow, ByVal vol As Double)

    '    Dim _mt, _mt1 As Double
    '    Dim ltp As Double

    '    If mT = 0 Then
    '        _mt = 0.00001
    '        _mt1 = 0.00001
    '    Else
    '        _mt = (mT) / 365
    '        '_mt1 = ((mT + 1) - CInt(txtnoofday.Text)) / 365
    '        _mt1 = (mmT) / 365
    '    End If
    '    'If DAYTIME_VOLANDGREEK_CAL = 1 Then
    '    '    _mt = Get_DayTime_mt(CDate(drow("mdate")).Date, _mt1)


    '    'Else
    '    '    If mT = 0 Then
    '    '        _mt = 0.00001
    '    '        _mt1 = 0.00001
    '    '    Else
    '    '        _mt = (mT) / 365
    '    '        '_mt1 = ((mT + 1) - CInt(txtnoofday.Text)) / 365
    '    '        _mt1 = (mmT) / 365
    '    '    End If

    '    'End If

    '    If stkprice > 0 Then
    '        If vol <> 0 Then

    '            ltp = (Greeks.Black_Scholes(futval, stkprice, Rateofinterest, 0, Val(vol / 100), _mt, mIsCall, False, 0, 0))
    '            drow("last") = ltp
    '        End If
    '    Else
    '        drow("last") = futval
    '        'drow("last") = (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, vol, _mt, False, True, 0, 0))
    '    End If

    'End Sub
    Private Sub Sub_Add_DealerScript_GAP_Row()
        Dim DrNewDlrScr As DataRow

        Dim maintablecopy As New DataTable
        maintablecopy = maintable.Copy
        Dim dtDealer As DataTable = New DataView(maintable, "", "company", DataViewRowState.CurrentRows).ToTable(True, "company")
        G_DtPositionwise_GAP.Rows.Clear()

        Dim vargrossmtom1 As Double = 0
        Dim vargrossmtom2 As Double = 0
        Dim varsqofexp1 As Double = 0
        Dim varsqofexp2 As Double = 0
        Dim VarLTP1 As Double = 0
        Dim VarLTP2 As Double = 0

        Dim vargrossmtom3 As Double = 0
        Dim vargrossmtom4 As Double = 0
        Dim varsqofexp3 As Double = 0
        Dim varsqofexp4 As Double = 0
        Dim VarLTP3 As Double = 0
        Dim VarLTP4 As Double = 0

        Dim vargrossmtom5 As Double = 0
        Dim vargrossmtom6 As Double = 0
        Dim varsqofexp5 As Double = 0
        Dim varsqofexp6 As Double = 0
        Dim varsqofexp7 As Double = 0
        Dim varsqofexp8 As Double = 0
        Dim VarLTP5 As Double = 0
        Dim VarLTP6 As Double = 0

        Dim VarLTP7 As Double = 0
        Dim VarLTP8 As Double = 0

        Dim vargrossmtom7 As Double = 0
        Dim vargrossmtom8 As Double = 0
        Dim totExpense As Double
        Dim dtDealerG As DataTable = New DataView(dtDealer, "", "company", DataViewRowState.CurrentRows).ToTable(True, "COMPANY")

        mPerf.WriteLogStr("-----------------------------------------------------------")

        For Each Dr1 As DataRow In dtDealerG.Select("")

            Dim htsy As New Hashtable
            REM Store Dealer Script wise
            Dim curTotal1 As Double = 0
            Dim curTotal2 As Double = 0
            Dim curTotal3 As Double = 0
            Dim curTotal4 As Double = 0
            Dim curTotal5 As Double = 0
            Dim curTotal6 As Double = 0
            Dim curTotal7 As Double = 0
            Dim curTotal8 As Double = 0

            For Each item As DictionaryEntry In ht_PlusMinuseLTPVol
                If htsy.Contains(Dr1("COMPANY")) = True Then
                    Continue For
                End If

                totExpense = 0
                varsqofexp1 = 0
                varsqofexp2 = 0
                varsqofexp3 = 0

                varsqofexp4 = 0
                varsqofexp5 = 0
                varsqofexp6 = 0
                varsqofexp7 = 0
                varsqofexp8 = 0

                vargrossmtom1 = 0
                vargrossmtom2 = 0
                vargrossmtom3 = 0
                vargrossmtom4 = 0
                vargrossmtom5 = 0
                vargrossmtom6 = 0
                vargrossmtom7 = 0
                vargrossmtom8 = 0


                For Each Dr As DataRow In maintablecopy.Select(" company='" & Dr1("COMPANY") & "'")
                    If htsy.Contains(Dr1("COMPANY")) = False Then
                        htsy.Add(Dr1("COMPANY"), Dr1("COMPANY"))
                    End If

                    Dim objStrfo11 As Struct_PlusMinuseLTPVol
                    objStrfo11 = New Struct_PlusMinuseLTPVol
                    If ht_PlusMinuseLTPVol.Contains(Convert.ToInt32(Dr("tokanno"))) = True Then
                        objStrfo11 = ht_PlusMinuseLTPVol(Convert.ToInt32(Dr("tokanno")))


                        VarLTP1 = objStrfo11.VarLTP1
                        VarLTP2 = objStrfo11.VarLTP2

                        VarLTP3 = objStrfo11.VarLTP3
                        VarLTP4 = objStrfo11.VarLTP4

                        VarLTP5 = objStrfo11.VarLTP5
                        VarLTP6 = objStrfo11.VarLTP6

                        VarLTP7 = objStrfo11.VarLTP7
                        VarLTP8 = objStrfo11.VarLTP8
                    End If

                    'Struct_LTPVol = CType(CLng(Dr("tokanno")), Struct_PlusMinuseLTPVol)
                    'VarLTP1 = Struct_LTPVol.VarLTP1
                    'VarLTP2 = Struct_LTPVol.VarLTP2

                    'VarLTP3 = Struct_LTPVol.VarLTP3
                    'VarLTP4 = Struct_LTPVol.VarLTP4

                    'VarLTP5 = Struct_LTPVol.VarLTP5
                    'VarLTP6 = Struct_LTPVol.VarLTP6

                    'VarLTP7 = Struct_LTPVol.VarLTP7
                    'VarLTP8 = Struct_LTPVol.VarLTP8

                    Dim varnetprice As Double = Dr("traded")
                    Dim varunit As Double = Dr("units")

                    Dim tempVarLtp3 As Double = objStrfo11.VarLTP3
                    Dim tempVarLtp4 As Double = objStrfo11.VarLTP4

                    If varunit = 0 Then
                        vargrossmtom1 += (IIf(varunit = 0, 1, varunit) * (-(varnetprice / 1)))
                        vargrossmtom2 += (IIf(varunit = 0, 1, varunit) * (-(varnetprice / 1)))

                        vargrossmtom3 += (IIf(varunit = 0, 1, varunit) * (-(varnetprice / 1)))
                        vargrossmtom4 += (IIf(varunit = 0, 1, varunit) * (-(varnetprice / 1)))

                        vargrossmtom5 += (IIf(varunit = 0, 1, varunit) * (-(varnetprice / 1)))
                        vargrossmtom6 += (IIf(varunit = 0, 1, varunit) * (-(varnetprice / 1)))

                        vargrossmtom7 += (IIf(varunit = 0, 1, varunit) * (-(varnetprice / 1)))
                        vargrossmtom8 += (IIf(varunit = 0, 1, varunit) * (-(varnetprice / 1)))
                    Else
                        vargrossmtom1 += (varunit * (VarLTP1 - varnetprice))
                        vargrossmtom2 += (varunit * (VarLTP2 - varnetprice))

                        'vargrossmtom3 += (varunit * (Math.Round(VarLTP3, 2) - varnetprice))
                        'vargrossmtom4 += (varunit * (Math.Round(VarLTP4, 2) - varnetprice))
                        vargrossmtom3 += (varunit * (VarLTP3 - varnetprice))
                        vargrossmtom4 += (varunit * (VarLTP4 - varnetprice))

                        vargrossmtom5 += (varunit * (VarLTP5 - varnetprice))
                        vargrossmtom6 += (varunit * (VarLTP6 - varnetprice))
                        vargrossmtom7 += (varunit * (VarLTP7 - varnetprice))
                        vargrossmtom8 += (varunit * (VarLTP8 - varnetprice))

                        curTotal3 = (varunit * (VarLTP4 - varnetprice))
                    End If

                    REM end 1.2.2 sq. of expense calculate
                    If Dr("cp") = "E" Then
                        varsqofexp1 += ((Math.Abs(varunit) * VarLTP1) * ndbs) / ndbsp
                        varsqofexp2 += ((Math.Abs(varunit) * VarLTP2) * ndbs) / ndbsp

                        varsqofexp3 += ((Math.Abs(varunit) * VarLTP3) * ndbs) / ndbsp
                        varsqofexp4 += ((Math.Abs(varunit) * VarLTP4) * ndbs) / ndbsp

                        varsqofexp5 += ((Math.Abs(varunit) * VarLTP5) * ndbs) / ndbsp
                        varsqofexp6 += ((Math.Abs(varunit) * VarLTP6) * ndbs) / ndbsp

                        varsqofexp7 += ((Math.Abs(varunit) * VarLTP7) * ndbs) / ndbsp
                        varsqofexp8 += ((Math.Abs(varunit) * VarLTP8) * ndbs) / ndbsp
                    ElseIf Dr("cp") = "F" Then
                        If Val(varunit) > 0 Then
                            varsqofexp1 += ((Math.Abs(varunit) * VarLTP1) * futs) / futsp
                            varsqofexp2 += ((Math.Abs(varunit) * VarLTP2) * futs) / futsp

                            varsqofexp3 += ((Math.Abs(varunit) * VarLTP3) * futs) / futsp
                            varsqofexp4 += ((Math.Abs(varunit) * VarLTP4) * futs) / futsp
                            varsqofexp5 += ((Math.Abs(varunit) * VarLTP5) * futs) / futsp
                            varsqofexp6 += ((Math.Abs(varunit) * VarLTP6) * futs) / futsp

                            varsqofexp7 += ((Math.Abs(varunit) * VarLTP7) * futs) / futsp
                            varsqofexp8 += ((Math.Abs(varunit) * VarLTP8) * futs) / futsp



                        Else
                            varsqofexp1 += ((Math.Abs(varunit) * VarLTP1) * futl) / futlp
                            varsqofexp2 += ((Math.Abs(varunit) * VarLTP2) * futl) / futlp

                            varsqofexp3 += ((Math.Abs(varunit) * VarLTP3) * futl) / futlp
                            varsqofexp4 += ((Math.Abs(varunit) * VarLTP4) * futl) / futlp
                            varsqofexp5 += ((Math.Abs(varunit) * VarLTP5) * futl) / futlp
                            varsqofexp6 += ((Math.Abs(varunit) * VarLTP6) * futl) / futlp
                            varsqofexp7 += ((Math.Abs(varunit) * VarLTP7) * futl) / futlp
                            varsqofexp8 += ((Math.Abs(varunit) * VarLTP8) * futl) / futlp
                        End If
                    Else
                        If Val(spl) <> 0 Then
                            If varunit > 0 Then
                                varsqofexp1 += ((Math.Abs(varunit) * (Dr("strikes") + VarLTP1)) * sps) / spsp
                                varsqofexp2 += ((Math.Abs(varunit) * (Dr("strikes") + VarLTP2)) * sps) / spsp

                                varsqofexp3 += ((Math.Abs(varunit) * (Dr("strikes") + VarLTP3)) * sps) / spsp
                                varsqofexp4 += ((Math.Abs(varunit) * (Dr("strikes") + VarLTP4)) * sps) / spsp
                                varsqofexp5 += ((Math.Abs(varunit) * (Dr("strikes") + VarLTP5)) * sps) / spsp
                                varsqofexp6 += ((Math.Abs(varunit) * (Dr("strikes") + VarLTP6)) * sps) / spsp
                                varsqofexp7 += ((Math.Abs(varunit) * (Dr("strikes") + VarLTP7)) * sps) / spsp
                                varsqofexp8 += ((Math.Abs(varunit) * (Dr("strikes") + VarLTP8)) * sps) / spsp
                            Else
                                varsqofexp1 += ((Math.Abs(Val(varunit) * (Dr("strikes") + VarLTP1)) * spl) / splp)
                                varsqofexp2 += ((Math.Abs(Val(varunit) * (Dr("strikes") + VarLTP2)) * spl) / splp)

                                varsqofexp3 += ((Math.Abs(Val(varunit) * (Dr("strikes") + VarLTP3)) * spl) / splp)
                                varsqofexp4 += ((Math.Abs(Val(varunit) * (Dr("strikes") + VarLTP4)) * spl) / splp)
                                varsqofexp5 += ((Math.Abs(Val(varunit) * (Dr("strikes") + VarLTP5)) * spl) / splp)
                                varsqofexp6 += ((Math.Abs(Val(varunit) * (Dr("strikes") + VarLTP6)) * spl) / splp)
                                varsqofexp7 += ((Math.Abs(Val(varunit) * (Dr("strikes") + VarLTP7)) * spl) / splp)
                                varsqofexp8 += ((Math.Abs(Val(varunit) * (Dr("strikes") + VarLTP8)) * spl) / splp)
                            End If
                        Else
                            If varunit > 0 Then
                                varsqofexp1 += ((Math.Abs(Val(varunit) * VarLTP1) * pres) / presp)
                                varsqofexp2 += ((Math.Abs(Val(varunit) * VarLTP2) * pres) / presp)

                                varsqofexp3 += ((Math.Abs(Val(varunit) * VarLTP3) * pres) / presp)
                                varsqofexp4 += ((Math.Abs(Val(varunit) * VarLTP4) * pres) / presp)
                                varsqofexp5 += ((Math.Abs(Val(varunit) * VarLTP5) * pres) / presp)
                                varsqofexp6 += ((Math.Abs(Val(varunit) * VarLTP6) * pres) / presp)
                                varsqofexp7 += ((Math.Abs(Val(varunit) * VarLTP7) * pres) / presp)
                                varsqofexp8 += ((Math.Abs(Val(varunit) * VarLTP8) * pres) / presp)
                            Else
                                varsqofexp1 += (Val((Math.Abs(varunit) * VarLTP1) * prel) / prelp)
                                varsqofexp2 += (Val((Math.Abs(varunit) * VarLTP2) * prel) / prelp)

                                varsqofexp3 += (Val((Math.Abs(varunit) * VarLTP3) * prel) / prelp)
                                varsqofexp4 += (Val((Math.Abs(varunit) * VarLTP4) * prel) / prelp)
                                varsqofexp5 += (Val((Math.Abs(varunit) * VarLTP5) * prel) / prelp)
                                varsqofexp6 += (Val((Math.Abs(varunit) * VarLTP6) * prel) / prelp)
                                varsqofexp7 += (Val((Math.Abs(varunit) * VarLTP7) * prel) / prelp)
                                varsqofexp8 += (Val((Math.Abs(varunit) * VarLTP8) * prel) / prelp)
                            End If
                        End If
                    End If


                    Dim G_DTExpenseDataCopy As DataTable = G_DTExpenseData.Copy
                    If G_DTExpenseDataCopy.Columns.Contains("mdate") = False Then
                        G_DTExpenseDataCopy.Columns.Add("Mdate", GetType(String))
                    End If
                    For Each drexp As DataRow In G_DTExpenseDataCopy.Rows
                        drexp("mdate") = Format(CDate(drexp("Exp_date")), "dd-MMM-yyyy")
                    Next
                    Dim prexp1 As Double
                    Dim exp As Double
                    Dim varcondition, compname As String
                    compname = Dr1("company")
                    varcondition = "company='" & Dr1("company") & "'"
                    Dim PatchExpDiff As Double
                    Dim mdate As Date = Dr("mdate")
                    DtExpDiff = objTrad.Select_Patch_Expense
                    If DtExpDiff.Rows.Count > 0 Then
                        PatchExpDiff = IIf(IsDBNull(DtExpDiff.Compute("Sum(ExpenseDiff)", varcondition)) = True, 0, DtExpDiff.Compute("Sum(ExpenseDiff)", varcondition))
                    Else

                        PatchExpDiff = 0
                    End If

                    'If drowsymbol("CP") = "E" Then
                    '    prexp1 = -Format(Math.Abs(CFexpense) + Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "company='" & compname & "' and Mdate='" & mrow("Mdate") & "'and entry_date < #" & fDate(Today.Date) & "# ").ToString) + PatchExpDiff), Expensestr)
                    '    exp = -Format(Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "company='" & compname & "'and mdate='" & compname & "' and entry_date >= #" & fDate(Today.Date) & "# ").ToString)), Expensestr)

                    'Else
                    If gstr_ProductName = "OMI" Then
                        Dim BCast1 As Date
                        BCast1 = DateAdd(DateInterval.Second, Math.Max(VarFoBCurrentDate, VarCurBCurrentDate), CDate("1-1-1980")).ToString("dd-MMM-yyy")
                        prexp1 = -Format(Math.Abs(CFexpense) + Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "company='" & compname & "'and mdate='" & CDate(mdate).ToString("dd-MMM-yyyy") & "' and entry_date < #" & fDate(BCast1) & "# and exp_date >= #" & fDate(BCast1) & "#").ToString) + PatchExpDiff), Expensestr)
                        exp = -Format(Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "company='" & compname & "'and mdate='" & CDate(mdate).ToString("dd-MMM-yyyy") & "' and entry_date >= #" & fDate(BCast1) & "# and exp_date >= #" & fDate(BCast1) & "#").ToString)), Expensestr)
                    Else
                        prexp1 = -Format(Math.Abs(CFexpense) + Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "company='" & compname & "'and mdate='" & CDate(mdate).ToString("dd-MMM-yyyy") & "' and entry_date < #" & fDate(Today.Date) & "# and exp_date >= #" & fDate(Today) & "#").ToString) + PatchExpDiff), Expensestr)
                        'exp = -Format(Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "company='" & compname & "'and mdate='" & CDate(mdate).ToString("dd-MMM-yyyy") & "' and entry_date >= #" & fDate(Today.Date) & "# and exp_date >= #" & fDate(Today) & "#").ToString)), Expensestr)

                        exp = Format(Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "script='" & Dr("SCRIPT") & "'").ToString)), Expensestr)

                    End If

                    totExpense += -(Math.Abs(Val(prexp1)) + Math.Abs(Val(exp)))




                    'Dim varnetprice As Double = Dr("traded")
                    'Dim varunit As Double = Dr("units")

                    'mStrdbg = "Vol :" & Val(Dr("lv")).ToString("0.0000") & " BsVal : " & objStrfo11.VarLTP3.ToString("0.0000") & " : " & "trade :" & varnetprice.ToString("0.0000") & " Units : " & varunit.ToString("0.0000") & ": Gmtm : " & vargrossmtom3.ToString() & "Exp :" & totExpense.ToString("0.00") & "CurTot :" & curTotal3
                    'mStrdbg = Val(Dr("lv")).ToString("0.0000") & "," & objStrfo11.VarLTP3.ToString("0.0000") & "," & varnetprice.ToString("0.0000") & "," & varunit.ToString("0.0000") & "," & vargrossmtom3.ToString() & "," & totExpense.ToString("0.00") & "," & curTotal3
                    'Dim curlv As Double = Val(Dr("lv"))
                    'mStrdbg = curlv.ToString("0.0000") & ","
                    'mStrdbg += objStrfo11.VarLTP4.ToString("0.0000") & ","
                    'mStrdbg += varunit.ToString("0.0000") & ","
                    'mStrdbg += varnetprice.ToString("0.0000") & ","
                    'mStrdbg += curTotal3.ToString(0.00) & ","
                    'mStrdbg += totExpense.ToString("0.00") & ","
                    'mStrdbg += vargrossmtom4.ToString() & ","


                    'mPerf.WriteLogStr(mStrdbg)





                Next




                mPerf.WriteLogStr("-----------------------------------------------------------")


                G_DtPositionwise_GAP.Columns("projmtom1").DataType = GetType(Double)
                G_DtPositionwise_GAP.Columns("projmtom2").DataType = GetType(Double)
                G_DtPositionwise_GAP.Columns("projmtom3").DataType = GetType(Double)
                G_DtPositionwise_GAP.Columns("projmtom4").DataType = GetType(Double)
                G_DtPositionwise_GAP.Columns("projmtom5").DataType = GetType(Double)
                G_DtPositionwise_GAP.Columns("projmtom6").DataType = GetType(Double)
                G_DtPositionwise_GAP.Columns("projmtom7").DataType = GetType(Double)
                G_DtPositionwise_GAP.Columns("projmtom8").DataType = GetType(Double)
                DrNewDlrScr = G_DtPositionwise_GAP.NewRow
                'If varunit = 0 Then
                '    DrNewDlrScr("projmtom1") = 0
                '    DrNewDlrScr("projmtom2") = 0

                '    DrNewDlrScr("projmtom3") = 0
                '    DrNewDlrScr("projmtom4") = 0
                '    DrNewDlrScr("projmtom5") = 0
                '    DrNewDlrScr("projmtom6") = 0
                '    DrNewDlrScr("projmtom7") = 0
                '    DrNewDlrScr("projmtom8") = 0
                'Else
                DrNewDlrScr("company") = Dr1("company")
                'DrNewDlrScr("projmtom1") = Math.Round((vargrossmtom1 + (totExpense * -1)), 2) '+ (-varsqofexp1)
                'DrNewDlrScr("projmtom2") = Math.Round((vargrossmtom2 + (totExpense * -1)), 2) '+ (-varsqofexp2)

                'DrNewDlrScr("projmtom3") = Math.Round((vargrossmtom3 + (totExpense * -1)), 2) '+ (-varsqofexp3)
                'DrNewDlrScr("projmtom4") = Math.Round((vargrossmtom4 + (totExpense * -1)), 2) '+ (-varsqofexp4)
                'DrNewDlrScr("projmtom5") = Math.Round((vargrossmtom5 + (totExpense * -1)), 2) '+ (-varsqofexp5)
                'DrNewDlrScr("projmtom6") = Math.Round((vargrossmtom6 + (totExpense * -1)), 2) '+ (-varsqofexp6)
                'DrNewDlrScr("projmtom7") = Math.Round((vargrossmtom7 + (totExpense * -1)), 2) '+ (-varsqofexp5)
                'DrNewDlrScr("projmtom8") = Math.Round((vargrossmtom8 + (totExpense * -1)), 2) '+ (-varsqofexp6)


                '----------------------------------------------------------------------------------------------------
                Dim dblPjm1 As Double = Math.Round((vargrossmtom1 + (totExpense)), 2) + (-varsqofexp1)
                Dim dblPjm2 As Double = Math.Round((vargrossmtom2 + (totExpense)), 2) + (-varsqofexp2)
                Dim dblPjm3 As Double = Math.Round((vargrossmtom3 + (totExpense)), 2) + (-varsqofexp3)


                'Dim dblPjm3aa As Double = Format(vargrossmtom3 - (totExpense + varsqofexp3), SquareMTMstr)
                Dim dblPjm4 As Double = Math.Round((vargrossmtom4 + (totExpense)), 2) + (-varsqofexp4)
                Dim dblPjm5 As Double = Math.Round((vargrossmtom5 + (totExpense)), 2) + (-varsqofexp5)
                Dim dblPjm6 As Double = Math.Round((vargrossmtom6 + (totExpense)), 2) + (-varsqofexp6)
                Dim dblPjm7 As Double = Math.Round((vargrossmtom7 + (totExpense)), 2) + (-varsqofexp5)
                Dim dblPjm8 As Double = Math.Round((vargrossmtom8 + (totExpense )), 2) + (-varsqofexp6)

                DrNewDlrScr("projmtom1") = dblPjm1.ToString("0")
                DrNewDlrScr("projmtom2") = dblPjm2.ToString("0")
                DrNewDlrScr("projmtom3") = dblPjm3.ToString("0")
                DrNewDlrScr("projmtom4") = dblPjm4.ToString("0")
                DrNewDlrScr("projmtom5") = dblPjm5.ToString("0")
                DrNewDlrScr("projmtom6") = dblPjm6.ToString("0")
                DrNewDlrScr("projmtom7") = dblPjm7.ToString("0")
                DrNewDlrScr("projmtom8") = dblPjm8.ToString("0")


                'End If
                G_DtPositionwise_GAP.Rows.Add(DrNewDlrScr)
            Next
            REM End
        Next

        DrNewDlrScr = G_DtPositionwise_GAP.NewRow


        DrNewDlrScr("company") = "Total"
        DrNewDlrScr("projmtom1") = Math.Round(Val(G_DtPositionwise_GAP.Compute("sum(projmtom1)", "").ToString), 2)

        DrNewDlrScr("projmtom2") = Math.Round(Val(G_DtPositionwise_GAP.Compute("sum(projmtom2)", "").ToString), 2)

        DrNewDlrScr("projmtom3") = Math.Round(Val(G_DtPositionwise_GAP.Compute("sum(projmtom3)", "").ToString), 2)
        DrNewDlrScr("projmtom4") = Math.Round(Val(G_DtPositionwise_GAP.Compute("sum(projmtom4)", "").ToString), 2)
        DrNewDlrScr("projmtom5") = Math.Round(Val(G_DtPositionwise_GAP.Compute("sum(projmtom5)", "").ToString), 2)
        DrNewDlrScr("projmtom6") = Math.Round(Val(G_DtPositionwise_GAP.Compute("sum(projmtom6)", "").ToString), 2)
        DrNewDlrScr("projmtom7") = Math.Round(Val(G_DtPositionwise_GAP.Compute("sum(projmtom7)", "").ToString), 2)
        DrNewDlrScr("projmtom8") = Math.Round(Val(G_DtPositionwise_GAP.Compute("sum(projmtom8)", "").ToString), 2)
        'End If
        G_DtPositionwise_GAP.Rows.Add(DrNewDlrScr)




        DGDATA.DataSource = G_DtPositionwise_GAP
    End Sub
    Private Sub frmGapUpDown_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        mPerf.SetFileName("GapUpDwn")

        GdtSettings = objTrad.Settings
        For Each DR As DataRow In GdtSettings.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim Setting_Key As String = DR("Settingkey").ToString
            Select Case DR("SettingName").ToString.ToUpper
                Case "PROJMTOM1"

                    txtltp1.Text = Setting_Key.Split(",")(0)
                    txtvol1.Text = Setting_Key.Split(",")(1)

                Case "PROJMTOM2"
                    txtltp2.Text = Setting_Key.Split(",")(0)
                    txtvol2.Text = Setting_Key.Split(",")(1)
                Case "PROJMTOM3"

                    txtltp3.Text = Setting_Key.Split(",")(0)
                    txtvol3.Text = Setting_Key.Split(",")(1)
                Case "PROJMTOM4"
                    txtltp4.Text = Setting_Key.Split(",")(0)
                    txtvol4.Text = Setting_Key.Split(",")(1)
                Case "PROJMTOM5"
                    txtltp5.Text = Setting_Key.Split(",")(0)
                    txtvol5.Text = Setting_Key.Split(",")(1)
                Case "PROJMTOM6"
                    txtltp6.Text = Setting_Key.Split(",")(0)
                    txtvol6.Text = Setting_Key.Split(",")(1)
                Case "PROJMTOM7"
                    txtltp7.Text = Setting_Key.Split(",")(0)
                    txtvol7.Text = Setting_Key.Split(",")(1)
                Case "PROJMTOM8"
                    txtltp8.Text = Setting_Key.Split(",")(0)
                    txtvol8.Text = Setting_Key.Split(",")(1)
            End Select
        Next


        GVar_LTPPer1 = Val(txtltp1.Text)
        GVar_LTPPer2 = Val(txtltp2.Text)
        GVar_LTPPer3 = Val(txtltp3.Text)
        GVar_LTPPer4 = Val(txtltp4.Text)
        GVar_LTPPer5 = Val(txtltp5.Text)
        GVar_LTPPer6 = Val(txtltp6.Text)
        GVar_LTPPer7 = Val(txtltp7.Text)
        GVar_LTPPer8 = Val(txtltp8.Text)

        GVar_VolPer1 = Val(txtvol1.Text)
        GVar_VolPer2 = Val(txtvol2.Text)
        GVar_VolPer3 = Val(txtvol3.Text)
        GVar_VolPer4 = Val(txtvol4.Text)
        GVar_VolPer5 = Val(txtvol5.Text)
        GVar_VolPer6 = Val(txtvol6.Text)
        GVar_VolPer7 = Val(txtvol7.Text)
        GVar_VolPer8 = Val(txtvol8.Text)
        G_DtPositionwise_GAP = New DataTable
        With G_DtPositionwise_GAP.Columns
            .Add("Company")
            .Add("projmtom1")
            .Add("projmtom2")
            .Add("projmtom3")
            .Add("projmtom4")
            .Add("projmtom5")
            .Add("projmtom6")
            .Add("projmtom7")
            .Add("projmtom8")

        End With
        DGDATA.DataSource = G_DtPositionwise_GAP
        Refresh_Data()
        timRefresh.Start()
        'btnrefresh_Click(sender, e)
    End Sub

    Private Sub btnsave_Click(sender As System.Object, e As System.EventArgs) Handles btnsave.Click
        For Each DR As DataRow In GdtSettings.Rows
            Dim setting_name As String = DR("SettingName").ToString.ToUpper
            Dim Setting_Key As String = DR("Settingkey").ToString
            Select Case DR("SettingName").ToString.ToUpper

                Case "PROJMTOM1"
                    Setting_Key = txtltp1.Text + "," + txtvol1.Text
                Case "PROJMTOM2"
                    Setting_Key = txtltp2.Text + "," + txtvol2.Text
                Case "PROJMTOM3"
                    Setting_Key = txtltp3.Text + "," + txtvol3.Text
                Case "PROJMTOM4"
                    Setting_Key = txtltp4.Text + "," + txtvol4.Text
                Case "PROJMTOM5"
                    Setting_Key = txtltp5.Text + "," + txtvol5.Text
                Case "PROJMTOM6"
                    Setting_Key = txtltp6.Text + "," + txtvol6.Text
                Case "PROJMTOM7"
                    Setting_Key = txtltp7.Text + "," + txtvol7.Text
                Case "PROJMTOM8"
                    Setting_Key = txtltp8.Text + "," + txtvol8.Text
            End Select
            If Setting_Key <> "Nothing" Then
                objTrad.SettingName = setting_name
                objTrad.SettingKey = Setting_Key
                objTrad.Uid = CInt(DR("uid"))
                objTrad.Update_setting()
            End If
        Next




        MsgBox("Save SuccessFully...")
    End Sub

    Private Sub DGDATA_CellFormatting(sender As System.Object, e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGDATA.CellFormatting
        Dim VarColor As Color
        Dim MyColor As System.Drawing.Color
        If Val(DGDATA.Rows(e.RowIndex).Cells("projmtom1").Value) < 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom1").Style.ForeColor = Color.LightCoral
        ElseIf Val(DGDATA.Rows(e.RowIndex).Cells("projmtom1").Value) > 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom1").Style.ForeColor = Color.LimeGreen
        Else
            DGDATA.Rows(e.RowIndex).Cells("projmtom1").Style.ForeColor = Color.White
        End If


        If Val(DGDATA.Rows(e.RowIndex).Cells("projmtom2").Value) < 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom2").Style.ForeColor = Color.LightCoral
        ElseIf Val(DGDATA.Rows(e.RowIndex).Cells("projmtom2").Value) > 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom2").Style.ForeColor = Color.LimeGreen
        Else
            DGDATA.Rows(e.RowIndex).Cells("projmtom2").Style.ForeColor = Color.White
        End If




        If Val(DGDATA.Rows(e.RowIndex).Cells("projmtom3").Value) < 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom3").Style.ForeColor = Color.LightCoral
        ElseIf Val(DGDATA.Rows(e.RowIndex).Cells("projmtom3").Value) > 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom3").Style.ForeColor = Color.LimeGreen
        Else
            DGDATA.Rows(e.RowIndex).Cells("projmtom3").Style.ForeColor = Color.White
        End If




        If Val(DGDATA.Rows(e.RowIndex).Cells("projmtom4").Value) < 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom4").Style.ForeColor = Color.LightCoral
        ElseIf Val(DGDATA.Rows(e.RowIndex).Cells("projmtom4").Value) > 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom4").Style.ForeColor = Color.LimeGreen
        Else
            DGDATA.Rows(e.RowIndex).Cells("projmtom4").Style.ForeColor = Color.White
        End If



        If Val(DGDATA.Rows(e.RowIndex).Cells("projmtom5").Value) < 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom5").Style.ForeColor = Color.LightCoral
        ElseIf Val(DGDATA.Rows(e.RowIndex).Cells("projmtom5").Value) > 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom5").Style.ForeColor = Color.LimeGreen
        Else
            DGDATA.Rows(e.RowIndex).Cells("projmtom5").Style.ForeColor = Color.White
        End If


        If Val(DGDATA.Rows(e.RowIndex).Cells("projmtom6").Value) < 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom6").Style.ForeColor = Color.LightCoral
        ElseIf Val(DGDATA.Rows(e.RowIndex).Cells("projmtom6").Value) > 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom6").Style.ForeColor = Color.LimeGreen
        Else
            DGDATA.Rows(e.RowIndex).Cells("projmtom6").Style.ForeColor = Color.White
        End If

        If Val(DGDATA.Rows(e.RowIndex).Cells("projmtom7").Value) < 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom7").Style.ForeColor = Color.LightCoral
        ElseIf Val(DGDATA.Rows(e.RowIndex).Cells("projmtom7").Value) > 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom7").Style.ForeColor = Color.LimeGreen
        Else
            DGDATA.Rows(e.RowIndex).Cells("projmtom7").Style.ForeColor = Color.White
        End If

        If Val(DGDATA.Rows(e.RowIndex).Cells("projmtom8").Value) < 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom8").Style.ForeColor = Color.LightCoral
        ElseIf Val(DGDATA.Rows(e.RowIndex).Cells("projmtom8").Value) > 0 Then
            DGDATA.Rows(e.RowIndex).Cells("projmtom8").Style.ForeColor = Color.LimeGreen
        Else
            DGDATA.Rows(e.RowIndex).Cells("projmtom8").Style.ForeColor = Color.White
        End If



    End Sub

    'Private Sub DGDATA_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGDATA.CellContentClick

    'End Sub
    'Private Sub Label3_Click(sender As System.Object, e As System.EventArgs) Handles Label3.Click

    'End Sub

    'Private Sub Label4_Click(sender As System.Object, e As System.EventArgs) Handles Label4.Click

    'End Sub

    'Private Sub Label5_Click(sender As System.Object, e As System.EventArgs) Handles Label5.Click

    'End Sub

    'Private Sub Label6_Click(sender As System.Object, e As System.EventArgs) Handles Label6.Click

    'End Sub

    'Private Sub Label25_Click(sender As System.Object, e As System.EventArgs) Handles Label25.Click

    'End Sub

    'Private Sub Label24_Click(sender As System.Object, e As System.EventArgs) Handles Label24.Click

    'End Sub

    'Private Sub Label23_Click(sender As System.Object, e As System.EventArgs) Handles Label23.Click

    'End Sub

    'Private Sub Label22_Click(sender As System.Object, e As System.EventArgs) Handles Label22.Click

    'End Sub

    'Private Sub txtltp1_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtltp1.TextChanged

    'End Sub

    'Private Sub txtltp8_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtltp8.TextChanged

    'End Sub

    'Private Sub txtvol8_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtvol8.TextChanged

    'End Sub

    'Private Sub txtltp7_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtltp7.TextChanged

    'End Sub

    'Private Sub txtvol7_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtvol7.TextChanged

    'End Sub

    'Private Sub txtltp6_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtltp6.TextChanged

    'End Sub

    'Private Sub txtvol6_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtvol6.TextChanged

    'End Sub

    'Private Sub txtvol5_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtvol5.TextChanged

    'End Sub

    'Private Sub txtltp5_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtltp5.TextChanged

    'End Sub

    'Private Sub txtltp4_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtltp4.TextChanged

    'End Sub

    'Private Sub txtvol4_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtvol4.TextChanged

    'End Sub

    'Private Sub txtltp3_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtltp3.TextChanged

    'End Sub

    'Private Sub txtvol3_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtvol3.TextChanged

    'End Sub

    'Private Sub txtltp2_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtltp2.TextChanged

    'End Sub

    'Private Sub txtvol2_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtvol2.TextChanged

    'End Sub

    'Private Sub txtvol1_TextChanged(sender As System.Object, e As System.EventArgs) Handles txtvol1.TextChanged

    'End Sub

    'Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    'End Sub


    Private Sub Refresh_Data()
        'analysis.change_tab(analysis.compname, "")
        ht_PlusMinuseLTPVol.Clear()
        Dim VarLTP1 As Double
        Dim VarLTP2 As Double
        Dim VarVol1 As Double
        Dim VarVol2 As Double

        Dim ltppr As Double = 0
        Dim ltppr1 As Double = 0
        Dim fltppr As Double = 0
        Dim mt As Double = 0
        Dim mmt As Double = 0
        'Dim mmt As Double = 0
        Dim isfut As Boolean = False
        Dim iscall As Boolean = False
        Dim VarStrikes As Double

        Dim objTrad As trading = New trading

        Dim VarLTP3 As Double
        Dim VarLTP4 As Double
        Dim VarVol3 As Double
        Dim VarVol4 As Double


        Dim VarLTP5 As Double
        Dim VarLTP6 As Double
        Dim VarVol5 As Double
        Dim VarVol6 As Double



        Dim VarLTP7 As Double
        Dim VarLTP8 As Double
        Dim VarVol7 As Double
        Dim VarVol8 As Double
        Dim maintablecopy As New DataTable
        maintablecopy = maintable.Copy
        G_DtPositionwise_GAP.Rows.Clear()
        Dim compname As String
        Dim varcondition As String
        Dim dvacomp As DataTable

        GVar_LTPPer1 = Val(txtltp1.Text)
        GVar_LTPPer2 = Val(txtltp2.Text)
        GVar_LTPPer3 = Val(txtltp3.Text)
        GVar_LTPPer4 = Val(txtltp4.Text)
        GVar_LTPPer5 = Val(txtltp5.Text)
        GVar_LTPPer6 = Val(txtltp6.Text)
        GVar_LTPPer7 = Val(txtltp7.Text)
        GVar_LTPPer8 = Val(txtltp8.Text)

        GVar_VolPer1 = Val(txtvol1.Text)
        GVar_VolPer2 = Val(txtvol2.Text)
        GVar_VolPer3 = Val(txtvol3.Text)
        GVar_VolPer4 = Val(txtvol4.Text)
        GVar_VolPer5 = Val(txtvol5.Text)
        GVar_VolPer6 = Val(txtvol6.Text)
        GVar_VolPer7 = Val(txtvol7.Text)
        GVar_VolPer8 = Val(txtvol8.Text)

        'Dim dtr() As DataRow = maintablecopy.DefaultView.ToTable(True, "tokanno", "cp", "strikes", "last", "ftoken", "lv", "mdate", "token1", "IsCurrency").Rows
        'mPerf.PrintDataTable(maintablecopy)
        'mPerf.PrintDataTable(currtable)
        For Each Dr As DataRow In maintablecopy.DefaultView.ToTable(True, "tokanno", "cp", "strikes", "last", "ftoken", "lv", "mdate", "token1", "IsCurrency").Rows
            Try

                Dim VarTokanNo As Integer = Dr("tokanno")
                Dim ltp As Double

                fltppr = Val(fltpprice(CLng(Dr("ftoken"))))
                If Dr("cp") = "F" Then
                    ltp = Val(fltpprice(Dr("tokanno")))
                    Dr("last") = ltp
                    REM calculation for +ltp - vol
                    VarLTP1 = Dr("last") + (Dr("last") * GVar_LTPPer1) / 100
                    VarLTP2 = Dr("last") + (Dr("last") * GVar_LTPPer2) / 100

                    VarLTP3 = Dr("last") + (Dr("last") * GVar_LTPPer3) / 100
                    VarLTP4 = Dr("last") + (Dr("last") * GVar_LTPPer4) / 100

                    VarLTP5 = Dr("last") + (Dr("last") * GVar_LTPPer5) / 100
                    VarLTP6 = Dr("last") + (Dr("last") * GVar_LTPPer6) / 100

                    VarLTP7 = Dr("last") + (Dr("last") * GVar_LTPPer7) / 100
                    VarLTP8 = Dr("last") + (Dr("last") * GVar_LTPPer8) / 100
                    REM End
                ElseIf Dr("cp") = "E" Then
                    REM calculation for +ltp - vol
                    VarLTP1 = Dr("last") + (Dr("last") * GVar_LTPPer1) / 100
                    VarLTP2 = Dr("last") + (Dr("last") * GVar_LTPPer2) / 100

                    VarLTP3 = Dr("last") + (Dr("last") * GVar_LTPPer3) / 100
                    VarLTP4 = Dr("last") + (Dr("last") * GVar_LTPPer4) / 100

                    VarLTP5 = Dr("last") + (Dr("last") * GVar_LTPPer5) / 100
                    VarLTP6 = Dr("last") + (Dr("last") * GVar_LTPPer6) / 100

                    VarLTP7 = Dr("last") + (Dr("last") * GVar_LTPPer7) / 100
                    VarLTP8 = Dr("last") + (Dr("last") * GVar_LTPPer8) / 100
                    REM End
                Else  ' if Option type is call or put
                    'If fltppr = "" Then
                    'Else
                    '    fltppr = fltppr
                    'End If

                    ' If Dr("vol").ToString() <> "" Then

                    Dim VOL As Double = 0
                    If Dr("lv") = 0 Then
                        VOL = Get_Vol(CLng(Dr("ftoken")), VarTokanNo, Dr("token1"), Dr("cp"), Dr("IsCurrency"), Dr("mdate"), Dr("strikes"))
                        Dr("lv") = VOL
                    End If
                    'Debug.WriteLine("Vol:" & Dr("lv") & " " & Dr("ftoken") & " : " & Dr("token1"))

                    VarVol1 = Dr("lv") + (Dr("lv") * GVar_VolPer1) / 100
                    VarVol2 = Dr("lv") + (Dr("lv") * GVar_VolPer2) / 100

                    VarLTP1 = fltppr + (fltppr * GVar_LTPPer1) / 100
                    VarLTP2 = fltppr + (fltppr * GVar_LTPPer2) / 100



                    VarVol3 = Dr("lv") + (Dr("lv") * GVar_VolPer3) / 100
                    VarVol4 = Dr("lv") + (Dr("lv") * GVar_VolPer4) / 100

                    VarLTP3 = fltppr + (fltppr * GVar_LTPPer3) / 100
                    VarLTP4 = fltppr + (fltppr * GVar_LTPPer4) / 100



                    VarVol5 = Dr("lv") + (Dr("lv") * GVar_VolPer5) / 100
                    VarVol6 = Dr("lv") + (Dr("lv") * GVar_VolPer6) / 100

                    VarLTP5 = fltppr + (fltppr * GVar_LTPPer5) / 100
                    VarLTP6 = fltppr + (fltppr * GVar_LTPPer6) / 100

                    VarLTP7 = fltppr + (fltppr * GVar_LTPPer7) / 100
                    VarLTP8 = fltppr + (fltppr * GVar_LTPPer8) / 100

                    VarVol7 = Dr("lv") + (Dr("lv") * GVar_VolPer7) / 100
                    VarVol8 = Dr("lv") + (Dr("lv") * GVar_VolPer8) / 100
                    VarStrikes = Dr("strikes")
                    isfut = True
                    'End If
                    Dim noday As Int32 = NoofDay
                    If noday > 1 Then
                        noday = noday
                        'ElseIf noday = 1 And UCase(WeekdayName(Weekday(Now))) = "FRIDAY" Then
                    ElseIf noday = 1 And UCase(DateTime.Now.DayOfWeek.ToString()) = "FRIDAY" Then
                        noday = 3
                    Else
                        noday = 1
                    End If

                    If CAL_GREEK_WITH_BCASTDATE = 1 Then
                        Dim BCast1 As Date
                        'BCast1 = DateAdd(DateInterval.Second, VarBCurrentDate, CDate("1-1-1980")).ToString("dd-MMM-yyyy HH:mm:ss")
                        BCast1 = DateAdd(DateInterval.Second, Math.Max(VarFoBCurrentDate, VarCurBCurrentDate), CDate("1-1-1980")).ToString("dd-MMM-yyyy")
                        mt = UDDateDiff(DateInterval.Day, BCast1, CDate(Dr("mdate")).Date)
                        mmt = UDDateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, CInt(noday), BCast1)).Date, CDate(Dr("mdate")).Date)
                        If BCast1 = CDate(Dr("mdate")).Date Then
                            mt += 0.5
                        End If
                        If CDate(DateAdd(DateInterval.Day, CInt(noday), BCast1)).Date = CDate(Dr("mdate")).Date Then
                            mmt += 0.5
                        End If
                    Else
                        mt = UDDateDiff(DateInterval.Day, Now.Date, CDate(Dr("mdate")).Date)
                        mmt = UDDateDiff(DateInterval.Day, CDate(DateAdd(DateInterval.Day, CInt(noday), Now())).Date, CDate(Dr("mdate")).Date)
                        If Now.Date = CDate(Dr("mdate")).Date Then
                            mt += 0.5
                        End If
                        If CDate(DateAdd(DateInterval.Day, CInt(noday), Now())).Date = CDate(Dr("mdate")).Date Then
                            mmt += 0.5
                        End If



                    End If
                    'Dim _mt, _mt1 As Double
                    'Dim ltp As Double

                    'If mt = 0 Then
                    '    _mt = 0.00001
                    '    _mt1 = 0.00001
                    'Else
                    '    _mt = (mt) / 365
                    '    '_mt1 = ((mT + 1) - CInt(txtnoofday.Text)) / 365
                    '    _mt1 = (mmt) / 365
                    'End If

                    REM 1.1.3.1: Check Option type is call or put
                    If Dr("cp") = "C" Then
                        iscall = True
                    Else
                        iscall = False
                    End If
                    REM 1.1.3.1: END

                    ltp = Val(ltpprice(Dr("tokanno")))
                    REM calculation for +ltp - vol
                    VarLTP1 = Sub_cal_ltpOn_vol(VarLTP1, VarStrikes, mt, mmt, iscall, isfut, VarVol1, ltp)
                    VarLTP2 = Sub_cal_ltpOn_vol(VarLTP2, VarStrikes, mt, mmt, iscall, isfut, VarVol2, ltp)

                    VarLTP3 = Sub_cal_ltpOn_vol(VarLTP3, VarStrikes, mt, mmt, iscall, isfut, VarVol3, ltp)
                    VarLTP4 = Sub_cal_ltpOn_vol(VarLTP4, VarStrikes, mt, mmt, iscall, isfut, VarVol4, ltp)


                    VarLTP5 = Sub_cal_ltpOn_vol(VarLTP5, VarStrikes, mt, mmt, iscall, isfut, VarVol5, ltp)
                    VarLTP6 = Sub_cal_ltpOn_vol(VarLTP6, VarStrikes, mt, mmt, iscall, isfut, VarVol6, ltp)

                    VarLTP7 = Sub_cal_ltpOn_vol(VarLTP7, VarStrikes, mt, mmt, iscall, isfut, VarVol7, ltp)
                    VarLTP8 = Sub_cal_ltpOn_vol(VarLTP8, VarStrikes, mt, mmt, iscall, isfut, VarVol8, ltp)
                    REM End



                End If



                REM Add +ltp - vol % in Hashtable
                Struct_LTPVol.VarLTP1 = VarLTP1
                Struct_LTPVol.VarLTP2 = VarLTP2
                Struct_LTPVol.VarVol1 = VarVol1
                Struct_LTPVol.VarVol2 = VarVol2

                Struct_LTPVol.VarLTP3 = VarLTP3
                Struct_LTPVol.VarLTP4 = VarLTP4
                Struct_LTPVol.VarVol3 = VarVol3
                Struct_LTPVol.VarVol4 = VarVol4

                Struct_LTPVol.VarLTP5 = VarLTP5
                Struct_LTPVol.VarLTP6 = VarLTP6
                Struct_LTPVol.VarVol5 = VarVol5
                Struct_LTPVol.VarVol6 = VarVol6

                Struct_LTPVol.VarLTP7 = VarLTP7
                Struct_LTPVol.VarLTP8 = VarLTP8
                Struct_LTPVol.VarVol7 = VarVol7
                Struct_LTPVol.VarVol8 = VarVol8

                If ht_PlusMinuseLTPVol.Contains(VarTokanNo) = True Then
                    ht_PlusMinuseLTPVol(VarTokanNo) = Struct_LTPVol
                Else
                    ht_PlusMinuseLTPVol.Add(VarTokanNo, Struct_LTPVol)
                End If
                REM end

                'mStrdbg = " " & Dr("ftoken")
                'mStrdbg += " : " & Dr("token1")
                'mStrdbg += " Fltp :" & fltppr.ToString("0.00")
                'mStrdbg += " ltp :" & ltp.ToString("0.0000")
                mStrdbg = " Vol:" & Val(Dr("lv")).ToString("0.0000")
                mStrdbg += " BVL3:" & VarLTP3.ToString("0.0000")
                mStrdbg += " BVL4:" & VarLTP4.ToString("0.0000")
                mPerf.WriteLogStr("Gap Up :" & mStrdbg)

            Catch ex As Exception

            End Try
        Next



        Sub_Add_DealerScript_GAP_Row()
    End Sub

    Private Sub timRefresh_Tick(sender As Object, e As EventArgs) Handles timRefresh.Tick
        If chkGapUpDownUpdate.Checked = True Then
            Refresh_Data()
        End If
    End Sub

    Private Sub frmGapUpDown_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        chkGapUpDownUpdate.Checked = False
        timRefresh.Stop()
    End Sub
End Class