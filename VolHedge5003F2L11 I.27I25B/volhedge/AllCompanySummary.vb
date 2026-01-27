Imports System.Threading
Imports VolHedge.OptionG
Public Class AllCompanySummary
    Dim dtgrid As New DataTable
    Dim maincal As New DataTable
    Dim onjana As New analysis
    Dim thrcal As Thread
    Dim Mrateofinterast As Double = 0
    Dim veqdel As Double
    Dim startflg As Boolean = False


    Private Sub AllCompanySummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        inittable()
        Filldtgrid()
        DGVSummary.DataSource = dtgrid
        Timer1.Start()
    End Sub
    Private Sub inittable()
        dtgrid.Columns.Add("Company", GetType(String))
        dtgrid.Columns.Add("CP", GetType(String))
        dtgrid.Columns.Add("Mdate", GetType(String))
        dtgrid.Columns.Add("Delta", GetType(Double))
        dtgrid.Columns.Add("gamma", GetType(Double))
        dtgrid.Columns.Add("vega", GetType(Double))
        dtgrid.Columns.Add("Theta", GetType(Double))
        dtgrid.Columns.Add("volga", GetType(Double))
        dtgrid.Columns.Add("vanna", GetType(Double))
        dtgrid.Columns.Add("grossMTM", GetType(Double))
        dtgrid.Columns.Add("deltaRs", GetType(Double))

        dtgrid.Columns.Add("Scenario1", GetType(Double))
        dtgrid.Columns.Add("Scenario2", GetType(Double))

        dtgrid.Columns.Add("initMargin", GetType(Double))
        dtgrid.Columns.Add("ExpoMargin", GetType(Double))
        dtgrid.Columns.Add("TotalMargin", GetType(Double))

        dtgrid.Columns.Add("Expense", GetType(Double))
        dtgrid.Columns.Add("NetMTM", GetType(Double))


    End Sub
    Private Sub Filldtgrid()
        If RefreshsummaryExpirywise = False Then
            dtgrid.Rows.Clear()
            Dim dv As DataView
            Dim dt As New DataTable
            dv = New DataView(maintable, " ", "strikes,cp,company", DataViewRowState.CurrentRows)
            dt = dv.ToTable(True, "company")
            Dim dr1 As DataRow
            For Each dr As DataRow In dt.Rows
                dr1 = dtgrid.NewRow()
                dr1("Company") = dr("Company")
                dr1("Mdate") = ""
                dr1("CP") = ""
                dr1("Delta") = 0
                dr1("gamma") = 0
                dr1("vega") = 0
                dr1("theta") = 0
                dr1("volga") = 0
                dr1("vanna") = 0
                dr1("grossMTM") = 0
                dr1("deltaRs") = 0

                dr1("Scenario1") = 0
                dr1("Scenario2") = 0

                dr1("initMargin") = 0
                dr1("ExpoMargin") = 0
                dr1("TotalMargin") = 0

                dr1("Expense") = 0
                dr1("NetMTM") = 0

                dtgrid.Rows.Add(dr1)
            Next
        Else
            dtgrid.Rows.Clear()

            Dim dv As DataView
            Dim dt As New DataTable
            dv = New DataView(maintable, " ", "strikes,cp,company", DataViewRowState.CurrentRows)
            dt = dv.ToTable(True, "company", "mdate", "CP")
            Dim dr1 As DataRow
            For Each dr As DataRow In dt.Rows
                dr1 = dtgrid.NewRow()
                dr1("Company") = dr("Company")
                dr1("Mdate") = CDate(dr("Mdate")).ToString("dd-MMM-yyyy")
                dr1("CP") = dr("CP")
                dr1("Delta") = 0
                dr1("gamma") = 0
                dr1("vega") = 0
                dr1("theta") = 0
                dr1("volga") = 0
                dr1("vanna") = 0
                dr1("grossMTM") = 0
                dr1("deltaRs") = 0

                dr1("Scenario1") = 0
                dr1("Scenario2") = 0

                dr1("initMargin") = 0
                dr1("ExpoMargin") = 0
                dr1("TotalMargin") = 0

                dr1("Expense") = 0
                dr1("NetMTM") = 0
                dtgrid.Rows.Add(dr1)
            Next
        End If

    End Sub
    Private Sub CalCulation()
        If startflg = False Then
            Cal_DeltaGammaVegaThetaSummary()
        End If


    End Sub
    Private Sub CalDataalert1(ByVal futval As Double, ByVal stkprice As Double, ByVal cpprice As Double, ByVal cpprice1 As Double, ByVal mT As Double, ByVal mIsCall As Boolean, ByVal mIsFut As Boolean, ByVal drow As DataRow, ByVal qty As Double)
        Try
            Dim mDelta As Double
            Dim mGama As Double
            Dim mVega As Double
            Dim mThita As Double
            Dim mVolga As Double
            Dim mVanna As Double
            Dim mRah As Double


            Dim mVolatility As Double
            Dim tmpcpprice As Double = 0
            Dim tmpcpprice1 As Double = 0
            Dim mD1 As Double
            Dim mD2 As Double

            tmpcpprice = cpprice
            tmpcpprice1 = cpprice1

            mDelta = 0
            mGama = 0
            mVega = 0
            mThita = 0
            mVolga = 0
            mVanna = 0
            mRah = 0
            mVolga = 0
            mVanna = 0
            mD1 = 0
            mD2 = 0
            Dim _mt As Double

            If mT = 0 Then
                _mt = 0.00001
            Else
                _mt = (mT) / 365
            End If
            If stkprice = 0 Then  'future volatility =0
                mVolatility = 0
            ElseIf tmpcpprice1 = 0 Then
                mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice, _mt, mIsCall, mIsFut, 0, 6)
            Else
                If mIsCall = True Then
                    mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, False, mIsFut, 0, 6)
                Else
                    mVolatility = Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, tmpcpprice1, _mt, True, mIsFut, 0, 6)
                End If
            End If

            ' Try
            If mVolatility = 0 Then
                mVolatility = 0.1
                mDelta = 1
            Else
                'mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))
                mDelta = mDelta + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 1))
            End If



            mGama = mGama + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 2))

            mVega = mVega + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 3))

            mThita = mThita + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 4))

            mRah = mRah + (Greeks.Black_Scholes(futval, stkprice, Mrateofinterast, 0, mVolatility, _mt, mIsCall, mIsFut, 0, 5))

            'Using Function 
            mD1 = mD1 + CalD1(futval, stkprice, Mrateofinterast, mVolatility, _mt)
            mD2 = mD2 + CalD2(futval, stkprice, Mrateofinterast, mVolatility, _mt)

            mVolga = mVolga + CalVolga(mVega, mD1, mD2, mVolatility)
            mVanna = mVanna + CalVanna(futval, mVega, mD1, mD2, mVolatility, _mt)


            'drow("delta") = Math.Round(val(drow("delta")) + (val(Math.Round(mDelta, roundDelta)) * qty), roundDelta)
            'drow("theta") = Math.Round(val(drow("theta")) + (val(Math.Round(mThita, roundTheta)) * qty), roundTheta)
            'drow("vega") = Math.Round(val(drow("vega")) + (val(Math.Round(mVega, roundVega)) * qty), roundVega)
            'drow("gamma") = Math.Round(val(drow("gamma")) + (val(Math.Round(mGama, roundGamma)) * qty), roundGamma)
            drow("last") = cpprice
            If drow("ISVolFix") = False Then
                drow("lv") = mVolatility * 100
            Else
                drow("lv") = drow("lv")
            End If

            drow("deltaval") = Val(Math.Round(mDelta * qty, roundDelta_Val))
            drow("vgval") = Val(Math.Round(mVega * qty, roundVega_Val))
            drow("gmval") = Val(Math.Round(mGama * qty, roundGamma_Val))

            drow("volgaval") = Val(Math.Round(mVolga * qty, roundVolga_Val))
            drow("vannaval") = Val(Math.Round(mVanna * qty, roundVanna_Val))

            If mThita < 0 Then
                If Math.Abs(mThita) > cpprice Then
                    mThita = -Math.Round(cpprice, roundTheta)
                    'drow("thetaval") = Math.Round(mThita* qty, roundTheta_Val)
                End If
            Else
                If Math.Abs(mThita) > cpprice Then
                    mThita = Math.Round(cpprice, roundTheta)
                    'drow("thetaval") = Math.Round(mThita * qty, roundTheta_Val)
                End If
            End If
            drow("thetaval") = Val(Math.Round(mThita * qty, roundTheta_Val))


            'Catch ex As Exception
            '    MsgBox(ex.ToString)
            ' End Try

        Catch ex As Exception
            MsgBox("futval = " & futval & vbCrLf _
             & "stkprice = " & stkprice & vbCrLf _
             & "ltppr = " & cpprice & vbCrLf _
             & "ltppr1 = " & cpprice1 & vbCrLf _
             & "qty = " & qty & vbCrLf _
             & "script = " & drow("script") & vbCrLf & ex.ToString)
        End Try


    End Sub
    Public Sub Cal_DeltaGammaVegaThetaSummary()
        '  Try
        startflg = True
        Dim acomp As New DataTable
        acomp = maintable.Copy
        If acomp.Columns.Contains("deltaRs") = False Then
            acomp.Columns.Add("deltaRs", GetType(Double))

            acomp.Columns.Add("lasts1", GetType(Double))
            acomp.Columns.Add("lasts2", GetType(Double))

            acomp.Columns.Add("Scenario1", GetType(Double))
            acomp.Columns.Add("Scenario2", GetType(Double))
        End If
      


        'Try



        Dim ltppr As Double = 0
        Dim ltppr1 As Double = 0
        Dim fltppr As Double = 0
        Dim mt As Double = 0
        Dim isfut As Boolean = False
        Dim iscall As Boolean = False
        Dim alltp As Double = 0
        Dim token As Long
        Dim token1 As Long
        Dim togrossmtm As Double = 0
        Dim prgrossmtm As Double = 0
        Dim prExp As Double = 0
        Dim toExp As Double = 0
        Dim texp As Double = 0
        Dim pLtp, nLtp, pVol, nVol As Double

        pVol = 10 'Val(txtVolVal.Text)
        nVol = 10 'Val(txtVolVal1.Text)


        pLtp = 10 'Val(txtLtpVal.Text)
        nLtp = 10 'Val(txtLtpVal1.Text)

        For Each mrow As DataRow In dtgrid.Select("company <> 'Total'")

            Dim DTTmp As DataTable
            If RefreshsummaryExpirywise = False Then
                DTTmp = New DataView(acomp, "company='" & mrow("company") & "' AND mdate Is Not Null", "mdate", DataViewRowState.CurrentRows).ToTable(True, "ftoken")
            Else
                Try

                
                If mrow("CP").ToString() = "E" Then
                    DTTmp = New DataView(acomp, "company='" & mrow("company") & "' and CP = 'E'  ", "", DataViewRowState.CurrentRows).ToTable(True, "ftoken")
                Else
                    DTTmp = New DataView(acomp, "company='" & mrow("company") & "' and Mdate = '" & CDate(mrow("Mdate")) & "' and CP <> 'E' AND mdate Is Not Null", "mdate", DataViewRowState.CurrentRows).ToTable(True, "ftoken")
                End If
                Catch ex As Exception

                End Try
            End If



            Dim VarCompFToken As Long
            If DTTmp.Rows.Count > 0 Then
                VarCompFToken = DTTmp.Rows(0)("ftoken")
            End If
            Dim dracom() As DataRow

            Dim varConition1 As String = ""
            If RefreshsummaryExpirywise = False Then
                varConition1 = " company='" & mrow("company") & "'"
            Else
                If mrow("CP").ToString() = "E" Then
                    varConition1 = "company='" & mrow("company") & "' and CP = 'E' "
                Else
                    varConition1 = " company='" & mrow("company") & "' and mdate=#" & CDate(mrow("mdate")) & "#"
                End If

            End If


            dracom = acomp.Select(varConition1)

            For Each drow As DataRow In dracom
                drow("lasts1") = Val(drow("last") & "")
                drow("lasts2") = Val(drow("last") & "")
                If CBool(drow("isliq")) = True Then
                    token = CLng(drow("tokanno"))
                    token1 = CLng(drow("token1"))
                Else
                    token = CLng(drow("tokanno"))
                    token1 = 0
                End If
                If drow("iscurrency") = False Then
                    fltppr = Val(fltpprice(CLng(drow("ftoken"))))
                Else
                    fltppr = Val(Currfltpprice(CLng(drow("ftoken"))))
                End If

                If ltpprice.Contains(token) Then

                    If token1 > 0 Then
                        ltppr = Val(ltpprice(token))
                        ltppr1 = Val(ltpprice(token1))
                    Else
                        ltppr = Val(ltpprice(token))
                        ltppr1 = 0
                    End If
                    mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                    If Now.Date = CDate(drow("mdate")).Date Then
                        mt = 0.5
                    End If
                    If drow("cp") = "C" Then
                        iscall = True
                    Else
                        iscall = False
                    End If
                    If Val(drow("units")) <> 0 And (drow("cp") = "C" Or drow("cp") = "P") Then
                        If fltppr > 0 Then
                            CalDataalert1(fltppr, Val(drow("strikes").ToString), ltppr, ltppr1, mt, iscall, isfut, drow, Val(drow("units").ToString))
                        Else
                            If veqdel > 0 Then
                                CalDataalert1(veqdel, Val(drow("strikes").ToString), ltppr, ltppr1, mt, iscall, isfut, drow, Val(drow("units").ToString))

                            End If
                        End If
                    End If
                End If


                If Currltpprice.Contains(token) Then
                    If token1 > 0 Then
                        ltppr = Val(Currltpprice(token))
                        ltppr1 = Val(Currltpprice(token1))
                    Else
                        ltppr = Val(Currltpprice(token))
                        ltppr1 = 0
                    End If
                    mt = DateDiff(DateInterval.Day, Now.Date, CDate(drow("mdate")).Date)
                    If Now.Date = CDate(drow("mdate")).Date Then
                        mt = 0.5
                    End If
                    If drow("cp") = "C" Then
                        iscall = True
                    Else
                        iscall = False
                    End If
                    If Val(drow("units")) <> 0 And (drow("cp") = "C" Or drow("cp") = "P") Then
                        If fltppr > 0 Then
                            'If drow("Script").ToString.Contains("Eurinr") = True Then MsgBox("a")
                            'Debug.Print(drow("Script"))
                            CalDataalert1(fltppr, Val(drow("strikes").ToString), ltppr, ltppr1, mt, iscall, isfut, drow, Val(drow("units").ToString))
                        Else
                            If veqdel > 0 Then
                                'Debug.Print(drow("Script"))
                                CalDataalert1(veqdel, Val(drow("strikes").ToString), ltppr, ltppr1, mt, iscall, isfut, drow, Val(drow("units").ToString))
                                '  drow("grossmtm") = Math.Round((val(drow("units")) * (val(drow("last")) - val(drow("traded")))), 2)
                            End If
                        End If
                    End If
                End If


                Dim VarCompFLTP As Double = 0
                If drow("iscurrency") = False Then
                    VarCompFLTP = Val(fltpprice(VarCompFToken))

                Else
                    VarCompFLTP = Val(Currfltpprice(VarCompFToken))
                End If
                If IsDBNull(drow("deltaval")) Then
                    drow("deltaval") = 0
                    acomp.AcceptChanges()
                End If


                drow("deltaRS") = Val(IIf(IsDBNull(drow("deltaval")) = True, 0, drow("deltaval")) * VarCompFLTP)
                
                If drow("cp") = "F" Then
                    If drow("iscurrency") = False Then
                        alltp = Val(fltpprice(CLng(drow("tokanno"))))
                    Else
                        alltp = Val(Currfltpprice(CLng(drow("tokanno"))))
                    End If


                    drow("last") = alltp
                    drow("lasts1") = alltp
                    drow("lasts2") = alltp
                    If Val(drow("units")) = 0 Then
                        drow("grossmtm") = -Val(drow("traded"))
                        
                    Else
                        drow("grossmtm") = (Val(drow("units")) * (Val(alltp) - Val(drow("traded"))))

                        texp = 0

                        
                        'Scenario2
                        texp = 0


                    End If

                ElseIf drow("cp") = "E" Then
                    alltp = Val(eltpprice(CLng(drow("tokanno"))))

                    drow("last") = alltp
                    If Val(fltpprice(CLng(drow("ftoken")))) = 0 Then
                        drow("lasts1") = alltp
                        drow("lasts2") = alltp
                    Else
                        drow("lasts1") = Val(fltpprice(CLng(drow("ftoken"))))
                        drow("lasts2") = Val(fltpprice(CLng(drow("ftoken"))))
                    End If

                    If Val(drow("units")) = 0 Then
                        drow("grossmtm") = -Val(drow("traded"))
                        
                    Else
                        drow("grossmtm") = (Val(drow("units")) * (Val(alltp) - Val(drow("traded"))))
                        texp = 0
                        
                        texp = 0
                        
                    End If
                Else


                    If Val(drow("units")) = 0 Then
                        drow("grossmtm") = -Val(drow("traded"))
                        
                    Else
                        drow("grossmtm") = (Val(drow("units")) * (Val(drow("last")) - Val(drow("traded"))))
                        texp = 0



                        texp = 0

                    End If
                End If

                drow("NetMTM") = Val(drow("totexp")) + Val(drow("grossmtm"))


                acomp.AcceptChanges()

            Next

           
            isfut = True


            If mrow("company") = "Total" Then Continue For
            mrow.BeginEdit()
            Dim dr As DataRow()
            Dim varConition As String = ""
            If RefreshsummaryExpirywise = False Then
                varConition = " company='" & mrow("company") & "'"
            Else
                varConition = " company='" & mrow("company") & "' and mdate=#" & CDate(mrow("mdate")) & "#"
            End If






            mrow("delta") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(deltaval)", varConition)) = True, 0, acomp.Compute("sum(deltaval)", varConition)), Deltastr_Val))
            mrow("deltaRS") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(deltaRS)", varConition)) = True, 0, acomp.Compute("sum(deltaRS)", varConition)), Deltastr_Val))
            mrow("theta") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(thetaval)", varConition)) = True, 0, acomp.Compute("sum(thetaval)", varConition)), Thetastr_Val))
            mrow("vega") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(vgval)", varConition)) = True, 0, acomp.Compute("sum(vgval)", varConition)), Vegastr_Val))
            mrow("gamma") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(gmval)", varConition)) = True, 0, acomp.Compute("sum(gmval)", varConition)), Gammastr_Val))
            mrow("volga") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(volgaval)", varConition)) = True, 0, acomp.Compute("sum(volgaval)", varConition)), Volgastr_Val))
            mrow("vanna") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(vannaval)", varConition)) = True, 0, acomp.Compute("sum(vannaval)", varConition)), Vannastr_Val))
            mrow("grossmtm") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(grossmtm)", varConition)) = True, 0, acomp.Compute("sum(grossmtm)", varConition)), GrossMTMstr))


            dr = acomp.Select(varConition)




            Dim cp As String = ""

            If dr.Length > 0 Then
                cp = dr(0).Item("cp").ToString()
            End If
            Dim prexp1 As Double
            Dim exp As Double
            Dim Var_Condition As String
            If RefreshsummaryExpirywise = True Then
                Dim G_DTExpenseDataCopy As DataTable = G_DTExpenseData.Copy
                If G_DTExpenseDataCopy.Columns.Contains("Mdate") = False Then
                    G_DTExpenseDataCopy.Columns.Add("Mdate", GetType(String))
                End If
                For Each drexp As DataRow In G_DTExpenseDataCopy.Rows
                    drexp("Mdate") = Format(CDate(drexp("Exp_date")), "MMMddyy")
                Next
                Var_Condition = "company='" & mrow("company") & "'"
                Dim PatchExpDiff As Double
                If analysis.DtExpDiff.Rows.Count > 0 Then
                    PatchExpDiff = 0 'IIf(IsDBNull(analysis.DtExpDiff.Compute("Sum(ExpenseDiff)", Var_Condition)) = True, 0, analysis.DtExpDiff.Compute("Sum(ExpenseDiff)", Var_Condition))
                Else
                    PatchExpDiff = 0
                End If

                If cp = "E" Then
                    prexp1 = -Format(Math.Abs(CFexpense) + Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "company='" & mrow("company") & "' and Month='" & mrow("Month") & "'and entry_date < #" & fDate(Today.Date) & "# ").ToString) + PatchExpDiff), Expensestr)
                    exp = -Format(Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "company='" & mrow("company") & "'and Month='" & mrow("Month") & "' and entry_date >= #" & fDate(Today.Date) & "# ").ToString)), Expensestr)

                Else
                    prexp1 = -Format(Math.Abs(CFexpense) + Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "company='" & mrow("company") & "'and Month='" & mrow("Month") & "' and entry_date < #" & fDate(Today.Date) & "# and exp_date >= #" & fDate(Today) & "#").ToString) + PatchExpDiff), Expensestr)
                    exp = -Format(Math.Abs(Val(G_DTExpenseDataCopy.Compute("sum(Expense)", "company='" & mrow("company") & "'and Month='" & mrow("Month") & "' and entry_date >= #" & fDate(Today.Date) & "# and exp_date >= #" & fDate(Today) & "#").ToString)), Expensestr)
                End If
            Else
                Var_Condition = "company='" & mrow("company") & "'"
                Dim PatchExpDiff As Double = 0 ' IIf(IsDBNull(analysis.DtExpDiff.Compute("Sum(ExpenseDiff)", Var_Condition)) = True, 0, analysis.DtExpDiff.Compute("Sum(ExpenseDiff)", Var_Condition))
                If cp = "E" Then
                    prexp1 = -Format(Math.Abs(CFexpense) + Math.Abs(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & mrow("company") & "' and entry_date < #" & fDate(Today.Date) & "# ").ToString) + PatchExpDiff), Expensestr)
                    exp = -Format(Math.Abs(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & mrow("company") & "' and entry_date >= #" & fDate(Today.Date) & "# ").ToString)), Expensestr)

                Else
                    prexp1 = -Format(Math.Abs(CFexpense) + Math.Abs(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & mrow("company") & "' and entry_date < #" & fDate(Today.Date) & "# and exp_date >= #" & fDate(Today) & "#").ToString) + PatchExpDiff), Expensestr)
                    exp = -Format(Math.Abs(Val(G_DTExpenseData.Compute("sum(Expense)", "company='" & mrow("company") & "' and entry_date >= #" & fDate(Today.Date) & "# and exp_date >= #" & fDate(Today) & "#").ToString)), Expensestr)
                End If
            End If


            mrow.EndEdit()



            mrow("Expense") = -(Math.Abs(Val(prexp1)) + Math.Abs(Val(exp)))


            mrow("NetMTM") = mrow("Expense") + mrow("grossmtm")

            Dim eq1 = IIf(IsDBNull(acomp.Compute("sum(units)", "company='" & mrow("company") & "' and cp='E'")) = True, 0, acomp.Compute("sum(units)", "company='" & mrow("company") & "' and cp='E'"))
            Dim eq2 = IIf(IsDBNull(acomp.Compute("sum(traded)", "company='" & mrow("company") & "' and cp='E'")) = True, 0, acomp.Compute("sum(traded)", "company='" & mrow("company") & "' and cp='E'"))
            Dim equity As Double = eq1 * eq2

            Dim tot1 As Double = 0
            Dim gmtom As Double = 0
            
            If RefreshsummaryScenario = True Then
                mrow("Scenario1") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(Scenario1)", "company='" & mrow("company") & "'")) = True, 0, acomp.Compute("sum(Scenario1)", "company='" & mrow("company") & "'")), SquareMTMstr))
                mrow("Scenario2") = Val(Format(IIf(IsDBNull(acomp.Compute("sum(Scenario2)", "company='" & mrow("company") & "'")) = True, 0, acomp.Compute("sum(Scenario2)", "company='" & mrow("company") & "'")), SquareMTMstr))
            End If

            
            If RefreshsummaryExpirywise = True Then
                Dim Str As String = mrow("month").ToString.Split(" ")(0)
                If Str = mrow("Company") Then
                    Str = "All"
                End If
                If analysis.mTbl_SPAN_output.Rows.Count > 0 Then
                    mrow("initMargin") = Val(Format(IIf(IsDBNull(analysis.mTbl_SPAN_output.Compute("sum(spanreq)", "ClientCode='" & mrow("company") + "/" + Str & "'")) = True, 0, analysis.mTbl_SPAN_output.Compute("sum(spanreq)", "ClientCode='" & mrow("company") + "/" + Str & "'")), inmargstr)) - Val(Format(IIf(IsDBNull(analysis.mTbl_SPAN_output.Compute("sum(anov)", "ClientCode='" & mrow("company") + "/" + Str & "'")) = True, 0, analysis.mTbl_SPAN_output.Compute("sum(anov)", "ClientCode='" & mrow("company") + "/" + Str & "'")), inmargstr))
                    mrow("ExpoMargin") = Val(Format(IIf(IsDBNull(analysis.mTbl_SPAN_output.Compute("sum(exposure_margin)", "ClientCode='" & mrow("company") + "/" + Str & "'")) = True, 0, analysis.mTbl_SPAN_output.Compute("sum(exposure_margin)", "ClientCode='" & mrow("company") + "/" + Str & "'")), exmargstr))
                Else
                    mrow("initMargin") = 0
                    mrow("ExpoMargin") = 0
                End If

            Else
                If analysis.mTbl_SPAN_output Is Nothing = False Then
                    If analysis.mTbl_SPAN_output.Rows.Count > 0 Then


                        mrow("initMargin") = Val(Format(IIf(IsDBNull(analysis.mTbl_SPAN_output.Compute("sum(spanreq)", "ClientCode='" & mrow("company") + "/All" & "'")) = True, 0, analysis.mTbl_SPAN_output.Compute("sum(spanreq)", "ClientCode='" & mrow("company") + "/All" & "'")), inmargstr)) - Val(Format(IIf(IsDBNull(analysis.mTbl_SPAN_output.Compute("sum(anov)", "ClientCode='" & mrow("company") + "/All" & "'")) = True, 0, analysis.mTbl_SPAN_output.Compute("sum(anov)", "ClientCode='" & mrow("company") + "/All" & "'")), inmargstr))
                        mrow("ExpoMargin") = Val(Format(IIf(IsDBNull(analysis.mTbl_SPAN_output.Compute("sum(exposure_margin)", "ClientCode='" & mrow("company") + "/All" & "'")) = True, 0, analysis.mTbl_SPAN_output.Compute("sum(exposure_margin)", "ClientCode='" & mrow("company") + "/All" & "'")), exmargstr))
                    Else
                        mrow("initMargin") = 0
                        mrow("ExpoMargin") = 0
                    End If
                End If
            End If


            mrow("TotalMargin") = Math.Round((mrow("initMargin") + mrow("ExpoMargin") + equity) / 100000, 2)
            mrow("delta") = Val(Format(IIf(IsDBNull(dtgrid.Compute("sum(delta)", varConition)) = True, 0, dtgrid.Compute("sum(delta)", varConition)), Deltastr_Val))
            mrow("deltaRS") = Val(Format(IIf(IsDBNull(dtgrid.Compute("sum(deltaRS)", varConition)) = True, 0, dtgrid.Compute("sum(deltaRS)", varConition)), Deltastr_Val))
            mrow("theta") = Val(Format(IIf(IsDBNull(dtgrid.Compute("sum(theta)", varConition)) = True, 0, dtgrid.Compute("sum(theta)", varConition)), Thetastr_Val))
            mrow("vega") = Val(Format(IIf(IsDBNull(dtgrid.Compute("sum(vega)", varConition)) = True, 0, dtgrid.Compute("sum(vega)", varConition)), Vegastr_Val))
            mrow("gamma") = Val(Format(IIf(IsDBNull(dtgrid.Compute("sum(gamma)", varConition)) = True, 0, dtgrid.Compute("sum(gamma)", varConition)), Gammastr_Val))
            mrow("volga") = Val(Format(IIf(IsDBNull(dtgrid.Compute("sum(volga)", varConition)) = True, 0, dtgrid.Compute("sum(volga)", varConition)), Volgastr_Val))
            mrow("vanna") = Val(Format(IIf(IsDBNull(dtgrid.Compute("sum(vanna)", varConition)) = True, 0, dtgrid.Compute("sum(vanna)", varConition)), Vannastr_Val))
            mrow("grossmtm") = Val(Format(IIf(IsDBNull(dtgrid.Compute("sum(grossmtm)", varConition)) = True, 0, dtgrid.Compute("sum(grossmtm)", varConition)), GrossMTMstr))


            mrow("Scenario1") = Math.Round(Val(dtgrid.Compute("sum(Scenario1)", varConition).ToString), 2)
            mrow("Scenario2") = Math.Round(Val(dtgrid.Compute("sum(Scenario2)", varConition).ToString), 2)



            mrow("initMargin") = Math.Round(Val(dtgrid.Compute("sum(initMargin)", varConition).ToString), 2)
            mrow("ExpoMargin") = Math.Round(Val(dtgrid.Compute("sum(ExpoMargin)", varConition).ToString), 2)
            mrow("TotalMargin") = Math.Round(Val(dtgrid.Compute("sum(TotalMargin)", varConition).ToString), 2)
            mrow("Expense") = Math.Round(Val(dtgrid.Compute("sum(Expense)", varConition).ToString), 2)
            mrow("NetMTM") = Math.Round(Val(dtgrid.Compute("sum(NetMTM)", varConition).ToString), 2)
        Next



        startflg = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        thrcal = New Thread(AddressOf CalCulation)
        thrcal.Start()
        DGVSummary.Refresh()
    End Sub

    Private Sub chkExpiryWise_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExpiryWise.CheckedChanged
        If chkExpiryWise.Checked Then
            RefreshsummaryExpirywise = True
            startflg = True
            Filldtgrid()
            startflg = False

        Else
            Try
                RefreshsummaryExpirywise = False
                Filldtgrid()
            Catch ex As Exception
            End Try
        End If
    End Sub


End Class