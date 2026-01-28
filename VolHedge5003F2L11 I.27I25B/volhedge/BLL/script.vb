Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class script
#Region "variable"
    Dim _intrumentName As String
    Dim _company As String
    Dim _mdate As Date
    Dim _strikerate As Double
    Dim _script As String
    Dim _units As Double
    Dim _rate As Double
    Dim _cp As String
    Dim _entrydate As Date
    Dim _token As Long
    Dim _assetToken As Long
    Dim _orderno As Long
    Dim _isliq As Boolean
    Dim _Dealer As String
    Dim _Exchange As String
    Dim _LotSize As Int32
#End Region

#Region "Property"
    Public Property InstrumentName() As String
        Get
            Return _intrumentName
        End Get
        Set(ByVal value As String)
            _intrumentName = value
        End Set
    End Property
    Public Property Dealer() As String
        Get
            Return _Dealer
        End Get
        Set(ByVal value As String)
            _Dealer = value
        End Set
    End Property
    Public Property Company() As String
        Get
            Return _company
        End Get
        Set(ByVal value As String)
            _company = value
        End Set
    End Property
    Public Property Mdate() As Date
        Get
            Return _mdate
        End Get
        Set(ByVal value As Date)
            _mdate = value
        End Set
    End Property
    Public Property StrikeRate() As Double
        Get
            Return _strikerate
        End Get
        Set(ByVal value As Double)
            _strikerate = value
        End Set
    End Property
    Public Property Units() As Double
        Get
            Return _units
        End Get
        Set(ByVal value As Double)
            _units = value
        End Set
    End Property
    Public Property Rate() As Double
        Get
            Return _rate
        End Get
        Set(ByVal value As Double)
            _rate = value
        End Set
    End Property
    Public Property CP() As String
        Get
            Return _cp
        End Get
        Set(ByVal value As String)
            _cp = value
        End Set
    End Property
    Public Property Script() As String
        Get
            Return _script
        End Get
        Set(ByVal value As String)
            _script = value
        End Set
    End Property
    Public Property EntryDate() As Date
        Get
            Return _entrydate
        End Get
        Set(ByVal value As Date)
            _entrydate = value
        End Set
    End Property
    Public Property Token() As Long
        Get
            Return _token
        End Get
        Set(ByVal value As Long)
            _token = value
        End Set
    End Property
    '==============================================keval
    Public Property asset_tokan() As Long
        Get
            Return _assetToken
        End Get
        Set(ByVal value As Long)
            _assetToken = value
        End Set
    End Property
    Public Property orderno() As Long
        Get
            Return _orderno
        End Get
        Set(ByVal value As Long)
            _orderno = value
        End Set
    End Property
    '=======================================keval
    Public Property Isliq() As Boolean
        Get
            Return _isliq
        End Get
        Set(ByVal value As Boolean)
            _isliq = value
        End Set
    End Property

    Public Property Exchange() As String
        Get
            Return _Exchange
        End Get
        Set(ByVal value As String)
            _Exchange = value
        End Set
    End Property

    Public Property LotSize() As Int32
        Get
            Return _LotSize
        End Get
        Set(ByVal value As Int32)
            _LotSize = value
        End Set
    End Property


#End Region

#Region "SP"
    Private Const SP_Script_Insert As String = "script_insert"
    Private Const SP_INSERT_EQUITY As String = "insert_equity" '' for Equity file process
    Private Const SP_INSERT_Currency_Trading As String = "INSERT_Currency_Trading" '' for INSERT_Currency_Trading file process

    Private Const sp_delete_trading_byuid As String = "delete_trading_byuid"
    Private Const SP_delete_equity_trading_byuid As String = "delete_equity_trading_byuid"
    Private Const SP_delete_Currency_trading_byuid As String = "delete_currency_trading_byuid"

    Private Const sp_select_Trading_uid As String = "select_trading_uid"
    Private Const sp_select_equity_uid As String = "select_equity_uid"
    Private Const sp_select_Currency_Trading_uid As String = "select_Currency_trading_uid"
#End Region

#Region "Method"
    Public Function Insert() As DataTable
        Dim dt As DataTable
        dt = New DataTable
        data_access.ParamClear()
        data_access.AddParam("@instrumentname", OleDbType.VarChar, 50, InstrumentName)
        data_access.AddParam("@company", OleDbType.VarChar, 50, Company)
        data_access.AddParam("@mdate", OleDbType.Date, 18, Mdate)
        data_access.AddParam("@strikerate", OleDbType.Double, 18, StrikeRate)
        data_access.AddParam("@cp", OleDbType.VarChar, 18, CP)
        data_access.AddParam("@script", OleDbType.VarChar, 100, Script)
        data_access.AddParam("@qty", OleDbType.Double, 18, Units)
        data_access.AddParam("@rate", OleDbType.Double, 18, Rate)
        data_access.AddParam("@entrydate", OleDbType.Date, 18, EntryDate)
        data_access.AddParam("@entryno", OleDbType.Integer, 18, 0)
        data_access.AddParam("@token1", OleDbType.Integer, 18, Token)
        'data_access.AddParam("@asset_tokan", OleDbType.Integer, 18, asset_tokan)
        data_access.AddParam("@isliq", OleDbType.Boolean, 18, Isliq)
        data_access.AddParam("@orderno", OleDbType.VarChar, 18, CStr(0))
        data_access.AddParam("@lActivityTime", OleDbType.Integer, 18, CInt(0))
        data_access.AddParam("@FileFlag", OleDbType.VarChar, 18, 0)
        data_access.AddParam("@Dealer", OleDbType.VarChar, 100, CStr(Dealer))




        Dim tot As Double
        Dim BuyQty As Double
        Dim SaleQty As Double
        Dim BuyVal As Double
        Dim SaleVal As Double
        Dim tot2 As Double
        If Units = 0 Then
            tot = Rate
        Else
            tot = Units * Rate
        End If


        If Units = 0 Then
            tot2 = Val(StrikeRate) + Rate
        Else
            tot2 = Units * (Val(StrikeRate) + Rate)
        End If

        Dim cpf As String
        If CP = "X" Or CP = "" Then
            cpf = "F"
        Else
            cpf = CP
        End If


        If Units > 0 Then
            BuyQty = Units
        Else
            BuyQty = 0
        End If

        If Units < 0 Then
            SaleQty = Units
        Else
            SaleQty = 0
        End If

        If Units > 0 Then
            BuyVal = Units * Rate
        Else
            BuyVal = 0
        End If

        If Units < 0 Then
            SaleVal = Units * Rate
        Else
            SaleVal = 0
        End If
        data_access.AddParam("@mo", OleDbType.VarChar, 30, CStr(Format(CDate(Mdate), "mm/yyyy")))
        data_access.AddParam("@token", OleDbType.Integer, 18, Val(0))
        data_access.AddParam("@tot", OleDbType.Double, 18, Val(tot))
        data_access.AddParam("@entry_date", OleDbType.Date, 18, EntryDate) 'CDate(CDate(EntryDate).ToString("dd/MMM/yyyy")))
        data_access.AddParam("@tot2", OleDbType.Double, 18, Val(tot2))
        data_access.AddParam("@cpf", OleDbType.VarChar, 30, cpf)
        data_access.AddParam("@BuyQty", OleDbType.Integer, 30, Val(BuyQty))
        data_access.AddParam("@SaleQty", OleDbType.Integer, 30, Val(SaleQty))
        data_access.AddParam("@BuyVal", OleDbType.Double, 30, Val(BuyVal))
        data_access.AddParam("@SaleVal", OleDbType.Double, 30, Val(SaleVal))
        data_access.AddParam("@exchange", OleDbType.VarChar, 20, Exchange)


        data_access.Cmd_Text = SP_Script_Insert
        dt = data_access.ExecuteNonQuery()
        Return dt
    End Function
    Public Sub insert_equity()
        data_access.ParamClear()
        data_access.AddParam("@script", OleDbType.VarChar, 50, CStr(Script))
        data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(Company))
        data_access.AddParam("@eq", OleDbType.VarChar, 18, CStr(CP))
        data_access.AddParam("@qty", OleDbType.Double, 18, Val(Units))
        data_access.AddParam("@rate", OleDbType.Double, 18, Val(Rate))
        data_access.AddParam("@entrydate", OleDbType.Date, 18, CDate(EntryDate))
        data_access.AddParam("@entryno", OleDbType.Integer, 18, CInt(0))
        data_access.AddParam("@orderno", OleDbType.VarChar, 18, CStr(0))
        data_access.AddParam("@lActivityTime", OleDbType.VarChar, 18, CInt(0))
        data_access.AddParam("@FileFlag", OleDbType.VarChar, 18, (0))
        data_access.AddParam("@Dealer", OleDbType.VarChar, 50, Dealer)
        data_access.AddParam("@exchange", OleDbType.VarChar, 20, Exchange)
        data_access.Cmd_Text = SP_INSERT_EQUITY
        data_access.ExecuteNonQuery()
    End Sub
    Public Sub Insert_Currency_Trading()
        data_access.ParamClear()
        data_access.AddParam("@instrumentname", OleDbType.VarChar, 50, InstrumentName)
        data_access.AddParam("@company", OleDbType.VarChar, 50, Company)
        data_access.AddParam("@mdate", OleDbType.Date, 18, Mdate)
        data_access.AddParam("@strikerate", OleDbType.Double, 18, StrikeRate)
        data_access.AddParam("@cp", OleDbType.VarChar, 18, CP)
        data_access.AddParam("@script", OleDbType.VarChar, 100, Script)
        data_access.AddParam("@qty", OleDbType.Double, 18, Units)
        data_access.AddParam("@rate", OleDbType.Double, 18, Rate)
        data_access.AddParam("@entrydate", OleDbType.Date, 18, EntryDate)
        data_access.AddParam("@entryno", OleDbType.Integer, 18, 0)
        data_access.AddParam("@token1", OleDbType.Integer, 18, Token)
        'data_access.AddParam("@asset_tokan", OleDbType.Integer, 18, asset_tokan)
        data_access.AddParam("@isliq", OleDbType.Boolean, 18, Isliq)
        data_access.AddParam("@orderno", OleDbType.VarChar, 18, CStr(0))
        data_access.AddParam("@lActivityTime", OleDbType.VarChar, 18, CInt(0))
        data_access.AddParam("@FileFlag", OleDbType.VarChar, 18, 0)
        data_access.AddParam("@Dealer", OleDbType.VarChar, 50, Dealer)
        data_access.Cmd_Text = SP_INSERT_Currency_Trading
        data_access.ExecuteNonQuery()
    End Sub

    Public Sub Insert_trading(ByVal dtable As DataTable)
        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@instrumentname", OleDbType.VarChar, 50, CStr(drow("Instrument")))
            data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(drow("security")))
            data_access.AddParam("@mdate", OleDbType.Date, 18, CDate(drow("Exp# Date")))
            data_access.AddParam("@strikerate", OleDbType.Double, 18, Val(drow("Strike")))
            data_access.AddParam("@cp", OleDbType.VarChar, 18, CStr(Mid(drow("CPf"), 1, 1)))
            data_access.AddParam("@script", OleDbType.VarChar, 100, CStr(drow("Script")))
            data_access.AddParam("@qty", OleDbType.Double, 18, Val(drow("qty")))
            data_access.AddParam("@rate", OleDbType.Double, 18, Val(drow("ATP")))
            data_access.AddParam("@entrydate", OleDbType.Date, 18, CDate(drow("EntryDate")))
            data_access.AddParam("@entryno", OleDbType.Integer, 18, CInt(0))
            data_access.AddParam("@token1", OleDbType.Integer, 18, Val(drow("token1")))
            data_access.AddParam("@isliq", OleDbType.Boolean, 18, drow("Isliq"))
            data_access.AddParam("@orderno", OleDbType.VarChar, 18, CStr(0))
            data_access.AddParam("@lActivityTime", OleDbType.VarChar, 18, CInt(0))
            data_access.AddParam("@FileFlag", OleDbType.VarChar, 18, (0))
            data_access.AddParam("@Dealer", OleDbType.VarChar, 50, IIf(IsDBNull(drow("Dealer")), 0, drow("Dealer")))




            Dim tot As Double
            Dim BuyQty As Double
            Dim SaleQty As Double
            Dim BuyVal As Double
            Dim SaleVal As Double
            Dim tot2 As Double
            If drow("qty") = 0 Then
                tot = drow("ATP")
            Else
                tot = drow("qty") * drow("ATP")
            End If


            If drow("qty") = 0 Then
                tot2 = Val(drow("Strike")) + drow("ATP")
            Else
                tot2 = drow("qty") * (Val(drow("Strike")) + drow("ATP"))
            End If

            Dim cpf As String
            If CStr(Mid(drow("CPf"), 1, 1)) = "X" Or CStr(Mid(drow("CPf"), 1, 1)) = "" Then
                cpf = "F"
            Else
                cpf = CStr(Mid(drow("CPf"), 1, 1))
            End If


            If drow("qty") > 0 Then
                BuyQty = drow("qty")
            Else
                BuyQty = 0
            End If

            If drow("qty") < 0 Then
                SaleQty = drow("qty")
            Else
                SaleQty = 0
            End If

            If drow("qty") > 0 Then
                BuyVal = drow("qty") * drow("ATP")
            Else
                BuyVal = 0
            End If

            If drow("qty") < 0 Then
                SaleVal = drow("qty") * drow("ATP")
            Else
                SaleVal = 0
            End If
            data_access.AddParam("@mo", OleDbType.VarChar, 30, CStr(Format(CDate(drow("Exp# Date")), "mm/yyyy")))
            data_access.AddParam("@token", OleDbType.Integer, 18, Val(0))
            data_access.AddParam("@tot", OleDbType.Double, 18, Val(tot))
            data_access.AddParam("@entry_date", OleDbType.Date, 18, CDate(drow("EntryDate"))) 'CDate(CDate(drow("EntryDate")).ToString("dd/MMM/yyyy")))
            data_access.AddParam("@tot2", OleDbType.Double, 18, Val(tot2))
            data_access.AddParam("@cpf", OleDbType.VarChar, 30, cpf)
            data_access.AddParam("@BuyQty", OleDbType.Integer, 30, Val(BuyQty))
            data_access.AddParam("@SaleQty", OleDbType.Integer, 30, Val(SaleQty))
            data_access.AddParam("@BuyVal", OleDbType.Double, 30, Val(BuyVal))
            data_access.AddParam("@SaleVal", OleDbType.Double, 30, Val(SaleVal))
            Dim exch As String = drow("exchange").ToString()
            data_access.AddParam("@exchange", OleDbType.VarChar, 30, exch)



        Next
        data_access.Cmd_Text = SP_Script_Insert
        data_access.ExecuteMultiple(27)

        


    End Sub
    Public Sub Insert_eqtrading(ByVal dtable As DataTable)
        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@script", OleDbType.VarChar, 100, CStr(drow("Script")))
            data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(drow("security")))
            data_access.AddParam("@eq", OleDbType.VarChar, 18, CStr(drow("eq")))
            data_access.AddParam("@qty", OleDbType.Double, 18, Val(drow("qty")))
            data_access.AddParam("@rate", OleDbType.Double, 18, Val(drow("atp")))
            data_access.AddParam("@entrydate", OleDbType.Date, 18, CDate(drow("EntryDate")))
            data_access.AddParam("@entryno", OleDbType.Integer, 18, CInt(0))
            data_access.AddParam("@orderno", OleDbType.VarChar, 18, CStr(0))
            data_access.AddParam("@lActivityTime", OleDbType.VarChar, 18, CInt(0))
            data_access.AddParam("@FileFlag", OleDbType.VarChar, 18, (0))
            data_access.AddParam("@Dealer", OleDbType.VarChar, 50, IIf(IsDBNull(drow("Dealer")), 0, drow("Dealer")))
         
        Next
        data_access.Cmd_Text = SP_INSERT_EQUITY
        data_access.ExecuteMultiple(11)

    End Sub
    Public Sub Insert_Currency_trading(ByVal dtable As DataTable)
        data_access.ParamClear()
        For Each drow As DataRow In dtable.Rows
            data_access.AddParam("@instrumentname", OleDbType.VarChar, 50, CStr(drow("Instrument")))
            data_access.AddParam("@company", OleDbType.VarChar, 50, CStr(drow("security")))
            data_access.AddParam("@mdate", OleDbType.Date, 18, CDate(drow("Exp# Date")))
            data_access.AddParam("@strikerate", OleDbType.Double, 18, Val(drow("Strike")))
            data_access.AddParam("@cp", OleDbType.VarChar, 18, CStr(Mid(drow("CPf"), 1, 1)))
            data_access.AddParam("@script", OleDbType.VarChar, 100, CStr(drow("Script")))
            data_access.AddParam("@qty", OleDbType.Double, 18, Val(drow("qty")))
            data_access.AddParam("@rate", OleDbType.Double, 18, Val(drow("ATP")))
            data_access.AddParam("@entrydate", OleDbType.Date, 18, CDate(drow("EntryDate")))
            data_access.AddParam("@entryno", OleDbType.Integer, 18, CInt(0))
            data_access.AddParam("@token1", OleDbType.Integer, 18, Val(drow("token1")))
            data_access.AddParam("@isliq", OleDbType.Boolean, 18, drow("Isliq"))
            data_access.AddParam("@orderno", OleDbType.VarChar, 18, CStr(0))
            data_access.AddParam("@lActivityTime", OleDbType.VarChar, 18, CInt(0))
            data_access.AddParam("@FileFlag", OleDbType.VarChar, 18, (0))
            data_access.AddParam("@Dealer", OleDbType.VarChar, 50, IIf(IsDBNull(drow("Dealer")), 0, drow("Dealer")))
        Next
        data_access.Cmd_Text = SP_INSERT_Currency_Trading
        data_access.ExecuteMultiple(16)
    End Sub


    Public Sub Delete_trad(ByVal script As String, ByVal uid As Double)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = sp_delete_trading_byuid
            data_access.AddParam("@script", OleDbType.VarChar, 150, script)
            data_access.AddParam("@uid", OleDbType.Double, 18, Val(uid))
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub Delete_eqtrad(ByVal script As String, ByVal uid As Double)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_equity_trading_byuid
            data_access.AddParam("@script", OleDbType.VarChar, 150, script)
            data_access.AddParam("@uid", OleDbType.Double, 18, Val(uid))
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub Delete_Currency_Trading_byUID(ByVal script As String, ByVal uid As Double)
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_delete_Currency_trading_byuid
            data_access.AddParam("@script", OleDbType.VarChar, 150, script)
            data_access.AddParam("@uid", OleDbType.Double, 18, Val(uid))
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Function select_trading_uid() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = sp_select_Trading_uid
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Script :: select_trading_uid :: " & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function select_equity_uid() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = sp_select_equity_uid
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Script :: select_equity_uid :: " & ex.ToString)
            Return Nothing
        End Try
    End Function
    Public Function select_Currency_trading_uid() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = sp_select_Currency_Trading_uid
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox("Script :: select_Currency_trading_uid :: " & ex.ToString)
            Return Nothing
        End Try
    End Function


    Public Sub insert_FOTrade_in_maintable(ByVal script As String, ByVal dtAna As DataTable, ByVal prExp As Double, ByVal toExp As Double, ByVal entrydt As Date, ByVal sCompName As String, pExchange As String)

        '***********************************************************************************
        'fill public maintable of analysis
        Dim mrow1(), mrow As DataRow
        Dim remarks As String = ""
        Dim bIsVolFix As Boolean = False
        Dim dbllv As Double = 0


        Dim preQty As Double = 0
        Dim preDate As Date
        Dim preSpot As Double = 0
        Dim preVol As Double = 0
        Dim preDelVal As Double = 0
        Dim preVegVal As Double = 0
        Dim preTheVal As Double = 0

        Dim curSpot As Double = 0
        Dim curVol As Double = 0
        Dim curDelVal As Double = 0
        Dim curVegVal As Double = 0
        Dim curTheVal As Double = 0

        Dim preTotalMTM As Double = 0
        Dim preGrossMTM As Double = 0
        Dim curTotalMTM As Double = 0
        Dim curGrossMTM As Double = 0


        'if position available then select it
        mrow1 = maintable.Select("script = '" & script & "' And company='" & sCompName & "'", "")
        If Not mrow1 Is Nothing Then
            If mrow1.Length <> 0 Then
                'save previous expenses
                mrow = mrow1(0)
                remarks = mrow("remarks").ToString
                bIsVolFix = CBool(mrow("IsVolFix"))
                dbllv = Val(mrow("lv").ToString)

                If entrydt = Today.Date Then
                    ' toExp += Val(mrow("toExp").ToString)
                    prExp = Val(mrow("prExp").ToString)
                Else
                    toExp = Val(mrow("toExp").ToString)
                    ' prExp = Val(mrow("prExp").ToString)
                End If


                preQty = Val(mrow("preQty") & "")
                preDate = CDate(IIf(IsDBNull(mrow("preDate")), Now.Date, mrow("preDate")))
                preSpot = Val(mrow("preSpot") & "")
                preVol = Val(mrow("preVol") & "")
                preDelVal = Val(mrow("preDelVal") & "")
                preVegVal = Val(mrow("preVegVal") & "")
                preTheVal = Val(mrow("preTheVal") & "")

                curSpot = Val(mrow("curSpot") & "")
                curVol = Val(mrow("curVol") & "")
                curDelVal = Val(mrow("curDelVal") & "")
                curVegVal = Val(mrow("curVegVal") & "")
                curTheVal = Val(mrow("curTheVal") & "")

                preTotalMTM = Val(mrow("preTotalMTM") & "")
                preGrossMTM = Val(mrow("preGrossMTM") & "")
                curTotalMTM = Val(mrow("curTotalMTM") & "")
                curGrossMTM = Val(mrow("curGrossMTM") & "")

                maintable.Rows.Remove(mrow)
            End If
        End If

        mrow = maintable.NewRow
        mrow("month") = Format(CDate(dtAna.Rows(0)("mdate")), "MMM yy")

        mrow("strikes") = dtAna.Rows(0)("strikes")
        mrow("script") = dtAna.Rows(0)("script")
        mrow("company") = dtAna.Rows(0)("company")
        mrow("mdate_months") = (CDate(Format(CDate(dtAna.Rows(0)("mdate")), "MMM/dd/yyyy")).Year * 12) + (CDate(Format(CDate(dtAna.Rows(0)("mdate")), "MMM/dd/yyyy")).Month)
        mrow("entrydate") = dtAna.Rows(0)("entrydate")
        mrow("prqty") = dtAna.Rows(0)("prqty")
        mrow("toqty") = dtAna.Rows(0)("toqty")
        mrow("cp") = dtAna.Rows(0)("cp")
        mrow("units") = dtAna.Rows(0)("units")
        mrow("Lots") = Val(dtAna.Rows(0)("units")) / cpfmaster.Compute("MAX(lotsize)", "script = '" & script & "'")
        mrow("traded") = dtAna.Rows(0)("traded")
        If mrow("cp") = "F" Then
            mrow("delta") = 1
            mrow("deltaval") = Val(mrow("units"))
            mrow("deltaval1") = Val(mrow("units"))
        Else 'for Option
            mrow("deltaval") = 0
            mrow("deltaval1") = 0
        End If
        mrow("theta") = 0
        mrow("vega") = 0
        mrow("gamma") = 0
        mrow("volga") = 0
        mrow("vanna") = 0
        mrow("thetaval") = 0
        mrow("vgval") = 0
        mrow("gmval") = 0
        mrow("volgaval") = 0
        mrow("vannaval") = 0
        mrow("thetaval1") = 0
        mrow("vgval1") = 0
        mrow("gmval1") = 0
        mrow("volgaval1") = 0
        mrow("vannaval1") = 0

        mrow("last") = dtAna.Rows(0)("last")
        mrow("flast") = dtAna.Rows(0)("flast")
        mrow("last1") = dtAna.Rows(0)("last1")
        mrow("lv") = dtAna.Rows(0)("lv")
        mrow("lv1") = 0 'dtAna.Rows(0)("lv1")
        mrow("MktVol") = dtAna.Rows(0)("MktVol")
        mrow("mdate") = dtAna.Rows(0)("mdate")
        mrow("fut_mdate") = dtAna.Rows(0)("mdate")
        mrow("token1") = dtAna.Rows(0)("token1")
        mrow("asset_tokan") = Val(dtAna.Rows(0)("asset_tokan") & "") 'HT_AssetToken(CLng(dtAna.Rows(0)("token1"))) ' dtAna.Rows(0)("asset_tokan")
        mrow("isliq") = dtAna.Rows(0)("isliq")
        mrow("tokanno") = dtAna.Rows(0)("tokanno")
        mrow("exchange") = dtAna.Rows(0)("exchange")

        'mrow("ftoken") = dtAna.Rows(0)("ftoken")
        'GdtSettings.Rows("settingName").Item= 

        'mrow("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & mrow("company") & "' and expdate1='" & mrow("mdate") & "' AND option_type='XX'").ToString)
        mrow("ftoken") = Val(cpfmaster.Compute("max(token)", "Symbol='" & GetSymbol(mrow("company")) & "' and expdate1=#" & Format(mrow("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)

        If mrow("ftoken") = 0 Then
            Dim DtFMonthDate1 As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(mrow("company")) & "' AND option_type='XX'and expdate1>=#" & Format(mrow("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
            If DtFMonthDate1.Rows.Count > 0 Then
                mrow("ftoken") = DtFMonthDate1.Rows(0)("token")
                mrow("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
            Else
                Dim DtFMonthDate As DataTable = New DataView(cpfmaster, "Symbol='" & GetSymbol(mrow("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                If DtFMonthDate.Rows.Count > 0 Then
                    If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                        mrow("ftoken") = DtFMonthDate.Rows(0)("token")
                        mrow("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                    ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                        mrow("ftoken") = DtFMonthDate.Rows(1)("token")
                        mrow("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                    ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                        mrow("ftoken") = DtFMonthDate.Rows(2)("token")
                        mrow("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                    End If
                End If
            End If

        End If


            mrow("status") = dtAna.Rows(0)("status")
            mrow("prExp") = prExp
            mrow("toExp") = toExp
            mrow("totExp") = -(prExp + toExp)
            mrow("grossmtm") = Val(dtAna.Rows(0)("units")) * (Val(dtAna.Rows(0)("last")) - Val(dtAna.Rows(0)("traded")))
            mrow("netmtm") = Val(mrow("grossmtm")) + Val(mrow("totExp"))
            mrow("Remarks") = remarks
            mrow("toatp") = 0
            mrow("pratp") = 0
            mrow("IsCurrency") = False

            mrow("IsVolFix") = bIsVolFix
            If bIsVolFix Then
                mrow("lv") = dbllv
            End If
            mrow("MktVol") = dbllv
            mrow("DeltaN") = 0
        mrow("GammaN") = 0
        mrow("VegaN") = 0
        mrow("ThetaN") = 0
        mrow("VolgaN") = 0
        mrow("VannaN") = 0

            mrow("IsCalc") = True


        REM For ProffitDiff    By Viral 06-07-11	  It Should  Assign Value As assign VolFix Flag (in fo,cm, curr)By Viral 3-11-11
        mrow("preQty") = preQty
        mrow("preDate") = preDate
        mrow("preSpot") = preSpot
        mrow("preVol") = preVol
        mrow("preDelVal") = preDelVal
        mrow("preVegVal") = preVegVal
        mrow("preTheVal") = preTheVal

            mrow("curSpot") = curSpot
            mrow("curVol") = curVol
            mrow("curDelVal") = curDelVal
            mrow("curVegVal") = curVegVal
            mrow("curTheVal") = curTheVal

            mrow("preTotalMTM") = preTotalMTM
            mrow("preGrossMTM") = preGrossMTM
            mrow("curTotalMTM") = curTotalMTM
        mrow("curGrossMTM") = curGrossMTM
        Dim VOL As Double
       
            'mrow("PreDate") = True
            maintable.AcceptChanges()

            'mrow("prExp") = prExp
            'mrow("toExp") = toExp
            If CLng(mrow("tokanno")) <> 0 Then
                maintable.Rows.Add(mrow)
            End If
            maintable.AcceptChanges()

    End Sub
    Public Sub insert_EQTrade_in_maintable(ByVal script As String, ByVal dtAna As DataTable, ByVal prExp As Double, ByVal toExp As Double, ByVal entrydt As Date, ByVal sCompName As String, pExchange As String)
        'fill public maintable of analysis
        Dim mrow1(), mrow As DataRow
        Dim remarks As String = ""
        Dim bIsVolFix As Boolean = False
        Dim dbllv As Double = 0

        Dim preQty As Double = 0
        Dim preDate As Date
        Dim preSpot As Double = 0
        Dim preVol As Double = 0
        Dim preDelVal As Double = 0
        Dim preVegVal As Double = 0
        Dim preTheVal As Double = 0

        Dim curSpot As Double = 0
        Dim curVol As Double = 0
        Dim curDelVal As Double = 0
        Dim curVegVal As Double = 0
        Dim curTheVal As Double = 0

        Dim preTotalMTM As Double = 0
        Dim preGrossMTM As Double = 0
        Dim curTotalMTM As Double = 0
        Dim curGrossMTM As Double = 0

        'if position available then select it
        mrow1 = maintable.Select("script = '" & script & "' And company='" & sCompName & "' AND exchange='" & pExchange & "'", "")
        If Not mrow1 Is Nothing Then
            If mrow1.Length <> 0 Then
                mrow = mrow1(0)
                remarks = mrow("remarks").ToString
                bIsVolFix = CBool(mrow("IsVolFix"))
                dbllv = Val(mrow("lv").ToString)
                If entrydt = Today.Date Then
                    'toExp = Val(mrow("toExp").ToString)
                    prExp = Val(mrow("prExp").ToString)
                Else
                    toExp = Val(mrow("toExp").ToString)
                    ' prExp = Val(mrow("prExp").ToString)
                End If

                preQty = Val(mrow("preQty") & "")
                preDate = CDate(IIf(IsDBNull(mrow("preDate")), Now.Date, mrow("preDate")))
                preSpot = Val(mrow("preSpot") & "")
                preVol = Val(mrow("preVol") & "")
                preDelVal = Val(mrow("preDelVal") & "")
                preVegVal = Val(mrow("preVegVal") & "")
                preTheVal = Val(mrow("preTheVal") & "")

                curSpot = Val(mrow("curSpot") & "")
                curVol = Val(mrow("curVol") & "")
                curDelVal = Val(mrow("curDelVal") & "")
                curVegVal = Val(mrow("curVegVal") & "")
                curTheVal = Val(mrow("curTheVal") & "")

                preTotalMTM = Val(mrow("preTotalMTM") & "")
                preGrossMTM = Val(mrow("preGrossMTM") & "")
                curTotalMTM = Val(mrow("curTotalMTM") & "")
                curGrossMTM = Val(mrow("curGrossMTM") & "")


                maintable.Rows.Remove(mrow)
            End If
        End If
        'add caluclated position to maintable
        mrow = maintable.NewRow
        mrow("month") = ""
        mrow("strikes") = 0
        mrow("script") = dtAna.Rows(0)("script")
        mrow("company") = dtAna.Rows(0)("company")
        mrow("mdate_months") = (CDate(Format(CDate(dtAna.Rows(0)("mdate")), "MMM/dd/yyyy")).Year * 12) + (CDate(Format(CDate(dtAna.Rows(0)("mdate")), "MMM/dd/yyyy")).Month)
        mrow("entrydate") = dtAna.Rows(0)("entrydate")
        mrow("prqty") = dtAna.Rows(0)("prqty")
        mrow("toqty") = dtAna.Rows(0)("toqty")
        mrow("cp") = dtAna.Rows(0)("cp")
        mrow("units") = dtAna.Rows(0)("units")
        mrow("traded") = dtAna.Rows(0)("traded")
        mrow("delta") = 1

        mrow("deltaval") = Val(mrow("units"))
        mrow("deltaval1") = Val(mrow("units"))
        mrow("theta") = 0
        mrow("vega") = 0
        mrow("gamma") = 0
        mrow("volga") = 0
        mrow("vanna") = 0
        mrow("thetaval") = 0
        mrow("vgval") = 0
        mrow("gmval") = 0
        mrow("volgaval") = 0
        mrow("vannaval") = 0
        mrow("thetaval1") = 0
        mrow("vgval1") = 0
        mrow("gmval1") = 0
        mrow("volgaval1") = 0
        mrow("vannaval1") = 0

        mrow("last") = dtAna.Rows(0)("last")
        mrow("flast") = dtAna.Rows(0)("flast")
        mrow("last1") = 0
        mrow("lv") = 0
        mrow("MktVol") = 0
        mrow("lv1") = 0
        mrow("mdate") = dtAna.Rows(0)("mdate")

        mrow("token1") = 0
        mrow("asset_tokan") = 0
        mrow("isliq") = dtAna.Rows(0)("isliq")
        mrow("tokanno") = dtAna.Rows(0)("tokanno")
        mrow("ftoken") = dtAna.Rows(0)("ftoken")
        If cpfmaster.Select("Symbol='" & GetSymbol(mrow("company")) & "' AND option_type='XX'").Length Then
            mrow("fut_mdate") = cpfmaster.Compute("MIN(ExpDate1)", "Symbol='" & GetSymbol(mrow("company")) & "' AND option_type='XX'")
            'mrow("ftoken") = cpfmaster.Compute("MAX(Token)", "Symbol='" & mrow("company") & "' AND ExpDate1=#" & mrow("fut_mdate") & "# AND option_type='XX'")
            mrow("ftoken") = cpfmaster.Compute("MAX(Token)", "Symbol='" & GetSymbol(mrow("company")) & "' AND ExpDate1=#" & Format(mrow("fut_mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'")
        Else
            'mrow("fut_mdate") = Today 'dtAna.Rows(0)("mdate")
            mrow("fut_mdate") = Format(Today, "dd-MMM-yyyy")
            mrow("ftoken") = 0
        End If
        mrow("status") = dtAna.Rows(0)("status")
        mrow("prExp") = Val(mrow("prExp").ToString) + prExp
        mrow("toExp") = toExp
        mrow("toatp") = 0
        mrow("pratp") = 0
        mrow("prExp") = prExp
        mrow("toExp") = toExp
        mrow("totExp") = -(prExp + toExp)
        mrow("grossmtm") = Val(dtAna.Rows(0)("units")) * (Val(dtAna.Rows(0)("last")) - Val(dtAna.Rows(0)("traded")))
        mrow("netmtm") = Val(mrow("grossmtm")) + Val(mrow("totExp"))
        mrow("Remarks") = remarks
        mrow("Lots") = 0
        mrow("IsCurrency") = False 
        mrow("IsVolFix") = bIsVolFix
        If bIsVolFix Then
            mrow("lv") = dbllv
        End If
        mrow("MktVol") = dbllv
        mrow("DeltaN") = 0
        mrow("GammaN") = 0
        mrow("VegaN") = 0
        mrow("ThetaN") = 0
        mrow("VolgaN") = 0
        mrow("VannaN") = 0

        mrow("IsCalc") = True

        REM For ProffitDiff    By Viral 06-07-11
        mrow("preQty") = preQty
        mrow("preDate") = preDate
        mrow("preSpot") = preSpot
        mrow("preVol") = preVol
        mrow("preDelVal") = preDelVal
        mrow("preVegVal") = preVegVal
        mrow("preTheVal") = preTheVal

        mrow("curSpot") = curSpot
        mrow("curVol") = curVol
        mrow("curDelVal") = curDelVal
        mrow("curVegVal") = curVegVal
        mrow("curTheVal") = curTheVal

        mrow("preTotalMTM") = preTotalMTM
        mrow("preGrossMTM") = preGrossMTM
        mrow("curTotalMTM") = curTotalMTM
        mrow("curGrossMTM") = curGrossMTM
        mrow("exchange") = pExchange

        If CLng(mrow("tokanno")) <> 0 Then
            maintable.Rows.Add(mrow)
        End If
        maintable.AcceptChanges()

    End Sub
    Public Sub insert_CurrencyTrade_in_maintable(ByVal script As String, ByVal dtAna As DataTable, ByVal prExp As Double, ByVal toExp As Double, ByVal entrydt As Date, ByVal sCompName As String)
        '***********************************************************************************
        'fill public maintable of analysis
        Dim mrow1(), mrow As DataRow
        Dim remarks As String = ""
        Dim bIsVolFix As Boolean = False
        Dim dbllv As Double = 0

        Dim preQty As Double
        Dim preDate As Date
        Dim preSpot As Double = 0
        Dim preVol As Double = 0
        Dim preDelVal As Double = 0
        Dim preVegVal As Double = 0
        Dim preTheVal As Double = 0

        Dim curSpot As Double = 0
        Dim curVol As Double = 0
        Dim curDelVal As Double = 0
        Dim curVegVal As Double = 0
        Dim curTheVal As Double = 0

        Dim preTotalMTM As Double = 0
        Dim preGrossMTM As Double = 0
        Dim curTotalMTM As Double = 0
        Dim curGrossMTM As Double = 0

        'if position available then select it
        mrow1 = maintable.Select("script = '" & script & "' And company='" & sCompName & "'", "")
        If Not mrow1 Is Nothing Then
            If mrow1.Length <> 0 Then
                'save previous expenses
                mrow = mrow1(0)
                remarks = mrow("remarks").ToString
                bIsVolFix = CBool(mrow("IsVolFix"))
                dbllv = Val(mrow("lv").ToString)

                If entrydt = Today.Date Then
                    ' toExp += Val(mrow("toExp").ToString)
                    prExp = Val(mrow("prExp").ToString)
                Else
                    toExp = Val(mrow("toExp").ToString)
                    ' prExp = Val(mrow("prExp").ToString)
                End If

                preQty = Val(mrow("preQty") & "")
                preDate = CDate(IIf(IsDBNull(mrow("preDate")), Now.Date, mrow("preDate")))
                preSpot = Val(mrow("preSpot") & "")
                preVol = Val(mrow("preVol") & "")
                preDelVal = Val(mrow("preDelVal") & "")
                preVegVal = Val(mrow("preVegVal") & "")
                preTheVal = Val(mrow("preTheVal") & "")

                curSpot = Val(mrow("curSpot") & "")
                curVol = Val(mrow("curVol") & "")
                curDelVal = Val(mrow("curDelVal") & "")
                curVegVal = Val(mrow("curVegVal") & "")
                curTheVal = Val(mrow("curTheVal") & "")

                preTotalMTM = Val(mrow("preTotalMTM") & "")
                preGrossMTM = Val(mrow("preGrossMTM") & "")
                curTotalMTM = Val(mrow("curTotalMTM") & "")
                curGrossMTM = Val(mrow("curGrossMTM") & "")


                maintable.Rows.Remove(mrow)
            End If
        End If
        mrow = maintable.NewRow
        mrow("month") = Format(CDate(dtAna.Rows(0)("mdate")), "MMM yy")
        mrow("strikes") = dtAna.Rows(0)("strikes")
        mrow("script") = dtAna.Rows(0)("script")
        mrow("company") = dtAna.Rows(0)("company")
        mrow("mdate_months") = (CDate(Format(CDate(dtAna.Rows(0)("mdate")), "MMM/dd/yyyy")).Year * 12) + (CDate(Format(CDate(dtAna.Rows(0)("mdate")), "MMM/dd/yyyy")).Month)
        mrow("entrydate") = dtAna.Rows(0)("entrydate")
        mrow("prqty") = dtAna.Rows(0)("prqty")
        mrow("toqty") = dtAna.Rows(0)("toqty")
        mrow("cp") = dtAna.Rows(0)("cp")
        mrow("units") = dtAna.Rows(0)("units")
        mrow("traded") = dtAna.Rows(0)("traded")
        If mrow("cp") = "F" Then
            mrow("delta") = 1
            mrow("deltaval") = Val(mrow("units"))
            mrow("deltaval1") = Val(mrow("units"))
        Else 'for Option
            mrow("deltaval") = 0
            mrow("deltaval1") = 0
        End If
        mrow("theta") = 0
        mrow("vega") = 0
        mrow("gamma") = 0
        mrow("volga") = 0
        mrow("vanna") = 0
        mrow("thetaval") = 0
        mrow("vgval") = 0
        mrow("gmval") = 0
        mrow("volgaval") = 0
        mrow("vannaval") = 0
        mrow("thetaval1") = 0
        mrow("vgval1") = 0
        mrow("gmval1") = 0
        mrow("volgaval1") = 0
        mrow("vannaval1") = 0

        mrow("last") = dtAna.Rows(0)("last")
        mrow("flast") = dtAna.Rows(0)("flast")
        mrow("last1") = dtAna.Rows(0)("last1")
        mrow("lv") = dtAna.Rows(0)("lv")
        mrow("lv") = dtAna.Rows(0)("lv")
        mrow("MktVol") = dtAna.Rows(0)("lv")
        mrow("lv1") = dtAna.Rows(0)("lv1")
        mrow("mdate") = dtAna.Rows(0)("mdate")
        mrow("fut_mdate") = dtAna.Rows(0)("mdate")
        mrow("token1") = dtAna.Rows(0)("token1")
        mrow("asset_tokan") = Val(dtAna.Rows(0)("asset_tokan") & "")
        mrow("isliq") = dtAna.Rows(0)("isliq")
        mrow("tokanno") = dtAna.Rows(0)("tokanno")
        'Jignesh
        'mrow("ftoken") = Val(Currencymaster.Compute("max(token)", "Symbol='" & mrow("company") & "' and expdate1='" & mrow("mdate") & "' AND option_type='XX'").ToString)
        mrow("ftoken") = Val(Currencymaster.Compute("max(token)", "Symbol='" & GetSymbol(mrow("company")) & "' and expdate1=#" & Format(mrow("mdate"), "dd-MMM-yyyy") & "# AND option_type='XX'").ToString)

        If mrow("ftoken") = 0 Then
            Dim DtFMonthDate1 As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(mrow("company")) & "' AND option_type='XX'and expdate1>=#" & Format(mrow("mdate"), "dd-MMM-yyyy") & "#", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
            If DtFMonthDate1.Rows.Count > 0 Then
                mrow("ftoken") = DtFMonthDate1.Rows(0)("token")
                mrow("fut_mdate") = DtFMonthDate1.Rows(0)("expdate1")
            Else
                Dim DtFMonthDate As DataTable = New DataView(Currencymaster, "Symbol='" & GetSymbol(mrow("company")) & "' AND option_type='XX'", "expdate1", DataViewRowState.CurrentRows).ToTable(True, "expdate1", "token")
                If DtFMonthDate.Rows.Count > 0 Then
                    If UCase(GVarMaturity_Far_month) = "CURRENT MONTH" Then
                        mrow("ftoken") = DtFMonthDate.Rows(0)("token")
                        mrow("fut_mdate") = DtFMonthDate.Rows(0)("expdate1")
                    ElseIf UCase(GVarMaturity_Far_month) = "NEXT MONTH" Then
                        mrow("ftoken") = DtFMonthDate.Rows(1)("token")
                        mrow("fut_mdate") = DtFMonthDate.Rows(1)("expdate1")
                    ElseIf UCase(GVarMaturity_Far_month) = "FAR MONTH" Then
                        mrow("ftoken") = DtFMonthDate.Rows(2)("token")
                        mrow("fut_mdate") = DtFMonthDate.Rows(2)("expdate1")
                    End If
                End If
            End If


        End If

            'mrow("ftoken") = dtAna.Rows(0)("ftoken")
            mrow("status") = dtAna.Rows(0)("status")
            mrow("prExp") = prExp
            mrow("toExp") = toExp
            mrow("totExp") = -(prExp + toExp)
            mrow("grossmtm") = Val(dtAna.Rows(0)("units")) * (Val(dtAna.Rows(0)("last")) - Val(dtAna.Rows(0)("traded")))
            mrow("netmtm") = Val(mrow("grossmtm")) + Val(mrow("totExp"))
            mrow("Remarks") = remarks
            mrow("toatp") = 0
            mrow("pratp") = 0
            mrow("Lots") = mrow("Units") / Currencymaster.Compute("MAX(multiplier)", "Script='" & mrow("Script") & "'")
            mrow("IsCurrency") = True

            mrow("IsVolFix") = bIsVolFix
            If bIsVolFix Then
                mrow("lv") = dbllv
            End If
            mrow("MktVol") = dtAna.Rows(0)("lv")
            mrow("DeltaN") = 0
        mrow("GammaN") = 0
        mrow("VegaN") = 0
        mrow("ThetaN") = 0
        mrow("VolgaN") = 0
        mrow("VannaN") = 0

            mrow("IsCalc") = True

            REM For ProffitDiff    By Viral 06-07-11
            mrow("preQty") = preQty
            mrow("preDate") = preDate
            mrow("preSpot") = preSpot
            mrow("preVol") = preVol
            mrow("preDelVal") = preDelVal
            mrow("preVegVal") = preVegVal
            mrow("preTheVal") = preTheVal

            mrow("curSpot") = curSpot
            mrow("curVol") = curVol
            mrow("curDelVal") = curDelVal
            mrow("curVegVal") = curVegVal
            mrow("curTheVal") = curTheVal

            mrow("preTotalMTM") = preTotalMTM
            mrow("preGrossMTM") = preGrossMTM
            mrow("curTotalMTM") = curTotalMTM
            mrow("curGrossMTM") = curGrossMTM

            maintable.AcceptChanges()

            'mrow("prExp") = prExp
            'mrow("toExp") = toExp
            If CLng(mrow("tokanno")) <> 0 Then
                maintable.Rows.Add(mrow)
            End If
            maintable.AcceptChanges()

    End Sub
#End Region
End Class
