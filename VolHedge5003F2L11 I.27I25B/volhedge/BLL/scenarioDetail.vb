Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL
Public Class scenarioDetail

#Region "variable"


    Dim _ScenarioName As String
    Dim _MarketScenarioName As String
    Dim _CMP As Double
    Dim _CVol As Double
    Dim _PVol As Double
    Dim _StartDate As Date
    Dim _EndDate As Date
    Dim _interval As Double
    Dim _strike As Integer
    Dim _MStrike As Double
    Dim _interval_type As String
    Dim _SelectedDate As String
    Dim _PNL As Double

    Public dtScenarioMast As New DataTable
    Public dtScenarioDetail As New DataTable

#End Region

#Region "Property"


    Public Property ScenarioName() As String
        Get
            Return _ScenarioName
        End Get
        Set(ByVal value As String)
            _ScenarioName = value
        End Set
    End Property
    Public Property MarketScenarioName() As String
        Get
            Return _MarketScenarioName
        End Get
        Set(ByVal value As String)
            _MarketScenarioName = value
        End Set
    End Property

    Public Property CMP() As Double
        Get
            Return _CMP
        End Get
        Set(ByVal value As Double)
            _CMP = value
        End Set
    End Property

    Public Property CVol() As Double
        Get
            Return _CVol
        End Get
        Set(ByVal value As Double)
            _CVol = value
        End Set
    End Property


    Public Property PVol() As Double
        Get
            Return _PVol
        End Get
        Set(ByVal value As Double)
            _PVol = value
        End Set
    End Property


    Public Property StartDate() As Date
        Get
            Return _StartDate
        End Get
        Set(ByVal value As Date)
            _StartDate = value
        End Set
    End Property


    Public Property EndDate() As Date
        Get
            Return _EndDate
        End Get
        Set(ByVal value As Date)
            _EndDate = value
        End Set
    End Property


    Public Property interval() As Double
        Get
            Return _interval
        End Get
        Set(ByVal value As Double)
            _interval = value
        End Set
    End Property


    Public Property strike() As Integer
        Get
            Return _strike
        End Get
        Set(ByVal value As Integer)
            _strike = value
        End Set
    End Property


    Public Property MStrike() As Double

        Get
            Return _MStrike
        End Get
        Set(ByVal value As Double)
            _MStrike = value
        End Set
    End Property


    Public Property interval_type() As String
        Get
            Return _interval_type
        End Get
        Set(ByVal value As String)
            _interval_type = value
        End Set
    End Property


    Public Property SelectedDate() As String
        Get
            Return _SelectedDate
        End Get
        Set(ByVal value As String)
            _SelectedDate = value
        End Set
    End Property

    Public Property PNL() As Double
        Get
            Return _PNL
        End Get
        Set(ByVal value As Double)
            _PNL = value
        End Set
    End Property
#End Region

#Region "SP"
    Private Const SP_delete_scenario As String = "delete_scenario"
    Private Const SP_insert_scenario As String = "insert_scenario"
    Private Const SP_select_scenario As String = "select_scenario"

    Private Const SP_delete_scenarioDetail As String = "delete_scenarioDetail"
    Private Const SP_insert_scenarioDetail As String = "insert_scenarioDetail"
    Private Const SP_select_scenarioDetail As String = "select_scenarioDetail"

    Private Const SP_delete_scenarioAll As String = "delete_scenarioAll"
    Private Const SP_delete_scenarioDetailAll As String = "delete_scenarioDetailAll"

    Private Const SP_update_Scenario_ScenarioName As String = "Update_Scenario_ScenarioName"
    Private Const SP_update_ScenarioDetail_ScenarioName As String = "Update_ScenarioDetail_ScenarioName"


    Private Const SP_Select_Scenario_Name As String = "Select_Scenario_Name"

#End Region



    Public Function Select_Scenario() As DataTable
        Try
            data_access.ParamClear()
            data_access.Cmd_Text = SP_select_scenario
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Function select_scenario_Name() As DataTable
        Try
            data_access.ParamClear()
            data_access.AddParam("@ScenarioName", OleDbType.VarChar, 18, _ScenarioName)
            data_access.Cmd_Text = SP_Select_Scenario_Name
            Return data_access.FillList()
        Catch ex As Exception
            MsgBox(ex.ToString)
            Return Nothing
        End Try
    End Function

    Public Sub select_scenario_Name(ByVal strScenarioName As String)
        Try

            data_access.ParamClear()
            data_access.AddParam("@ScenarioName", OleDbType.VarChar, 18, strScenarioName)
            data_access.Cmd_Text = SP_Select_Scenario_Name
            dtScenarioMast = data_access.FillList()

            data_access.ParamClear()
            data_access.AddParam("@ScenarioName", OleDbType.VarChar, 18, strScenarioName)
            data_access.Cmd_Text = SP_select_scenarioDetail
            dtScenarioDetail = data_access.FillList()
            Dim drow As DataRow
            If dtScenarioMast.Rows.Count > 0 Then
                drow = dtScenarioMast.Rows(0)
                _ScenarioName = drow("ScenarioName").ToString
                _CMP = Val(drow("CMP") & "")
                _CVol = Val(drow("CVol") & "")
                _PVol = Val(drow("PVol") & "")
                _StartDate = CDate(drow("StartDate"))
                _EndDate = CDate(drow("EndDate"))
                _interval = Val(drow("interval") & "")
                _strike = Val(drow("strike") & "")
                _MStrike = Val(drow("MStrike") & "")
                _interval_type = drow("interval_type").ToString
                _SelectedDate = drow("SelectedDate").ToString
                _PNL = drow("PNL").ToString
            Else
                _ScenarioName = strScenarioName
                _CMP = 0
                _CVol = 0
                _PVol = 0
                _StartDate = Today.Date
                _EndDate = Today.Date
                _interval = 100
                _strike = 10
                _MStrike = 0
                _interval_type = "VALUE"
                _SelectedDate = "Expiry"
                _PNL = 0
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)

        End Try
    End Sub

    
    Public Sub Insert_scenario()
        Try
            data_access.ParamClear()
            data_access.AddParam("@ScenarioName", OleDbType.VarChar, 18, _ScenarioName)
            data_access.AddParam("@CMP", OleDbType.Double, 18, _CMP)
            data_access.AddParam("@CVol", OleDbType.Double, 18, _CVol)
            data_access.AddParam("@PVol", OleDbType.Double, 18, _PVol)
            data_access.AddParam("@StartDate", OleDbType.Date, 18, _StartDate)
            data_access.AddParam("@EndDate", OleDbType.Date, 18, _EndDate)
            data_access.AddParam("@interval", OleDbType.Double, 18, _interval)
            data_access.AddParam("@strike", OleDbType.Numeric, 18, _strike)
            data_access.AddParam("@MStrike", OleDbType.Double, 18, _MStrike)
            data_access.AddParam("@interval_type", OleDbType.VarChar, 18, _interval_type)
            data_access.AddParam("@SelectedDate", OleDbType.VarChar, 2500, _SelectedDate)
            data_access.AddParam("@PNL", OleDbType.Double, 18, _PNL)
            data_access.Cmd_Text = SP_insert_scenario
            data_access.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Sub Delete_scenario()

        If _ScenarioName <> "" Then



            data_access.ParamClear()
            data_access.AddParam("@ScenarioName", OleDbType.VarChar, 18, _ScenarioName)
            data_access.Cmd_Text = SP_delete_scenario
            data_access.ExecuteNonQuery()

            Delete_scenario_Detail()
        End If
    End Sub


    Public Sub Delete_scenario_Detail()
        If _ScenarioName <> "" Then
            data_access.ParamClear()
            data_access.AddParam("@ScenarioName", OleDbType.VarChar, 18, _ScenarioName)
            data_access.Cmd_Text = SP_delete_scenarioDetail
            data_access.ExecuteNonQuery()
        End If
    End Sub

    Public Sub Insert_scenario_Detail(ByVal Dtable As DataTable)
        Try


            'data_access.AddParam("@ScenarioName", OleDbType.VarChar, 18, _ScenarioName)
            'data_access.AddParam("@CMP", OleDbType.Double, 18, _CMP)
            'data_access.AddParam("@CVol", OleDbType.Double, 18, _CVol)
            'data_access.AddParam("@PVol", OleDbType.Double, 18, _PVol)
            'data_access.AddParam("@StartDate", OleDbType.Date, 18, _StartDate)
            'data_access.AddParam("@EndDate", OleDbType.Date, 18, _EndDate)
            'data_access.AddParam("@interval", OleDbType.Numeric, 18, _interval)
            'data_access.AddParam("@strike", OleDbType.Numeric, 18, _strike)
            'data_access.AddParam("@MStrike", OleDbType.Double, 18, _MStrike)
            'data_access.AddParam("@interval_type", OleDbType.VarChar, 18, _interval_type)
            'data_access.AddParam("@SelectedDate", OleDbType.VarChar, 18, _SelectedDate)


            For Each DROW As DataRow In Dtable.Rows
                data_access.ParamClear()
                data_access.AddParam("@ScenarioName", OleDbType.VarChar, 18, _ScenarioName)
                data_access.AddParam("@Flag", OleDbType.Boolean, 18, CBool(DROW("Flag")))
                data_access.AddParam("@StartDate", OleDbType.Date, 18, CDate(DROW("StartDate")))
                data_access.AddParam("@Expiry", OleDbType.Date, 18, CDate(DROW("Expiry")))
                data_access.AddParam("@CPF", OleDbType.VarChar, 18, DROW("CPF") & "")
                data_access.AddParam("@Underlying", OleDbType.Double, 18, Val(DROW("Underlying") & ""))
                data_access.AddParam("@Strike", OleDbType.Double, 18, Val(DROW("Strike") & ""))
                data_access.AddParam("@Qty", OleDbType.Double, 18, Val(DROW("Qty") & ""))
                data_access.AddParam("@LTP", OleDbType.Double, 18, Val(DROW("LTP") & ""))
                data_access.AddParam("@Vol", OleDbType.Double, 18, Val(DROW("Vol") & ""))
                data_access.AddParam("@VolORG", OleDbType.Double, 18, Val(DROW("VolORG") & ""))
                data_access.AddParam("@Rate", OleDbType.Double, 18, Val(DROW("Rate") & ""))
                data_access.AddParam("@Delta", OleDbType.Double, 18, Val(DROW("Delta") & ""))
                data_access.AddParam("@DelVal", OleDbType.Double, 18, Val(DROW("DelVal") & ""))
                data_access.AddParam("@Gamma", OleDbType.Double, 18, Val(DROW("Gamma") & ""))
                data_access.AddParam("@GamVal", OleDbType.Double, 18, Val(DROW("GamVal") & ""))
                data_access.AddParam("@Vega", OleDbType.Double, 18, Val(DROW("Vega") & ""))
                data_access.AddParam("@VgVal", OleDbType.Double, 18, Val(DROW("VgVal") & ""))
                data_access.AddParam("@Theta", OleDbType.Double, 18, Val(DROW("Theta") & ""))
                data_access.AddParam("@ThVal", OleDbType.Double, 18, Val(DROW("ThVal") & ""))
                data_access.AddParam("@Volga", OleDbType.Double, 18, Val(DROW("Volga") & ""))
                data_access.AddParam("@VoVal", OleDbType.Double, 18, Val(DROW("VoVal") & ""))
                data_access.AddParam("@Vanna", OleDbType.Double, 18, Val(DROW("Vanna") & ""))
                data_access.AddParam("@VaVal", OleDbType.Double, 18, Val(DROW("VaVal") & ""))
                data_access.AddParam("@LTP1", OleDbType.Double, 18, Val(DROW("LTP1") & ""))
                data_access.AddParam("@Delta1", OleDbType.Double, 18, Val(DROW("Delta1") & ""))
                data_access.AddParam("@DelVal1", OleDbType.Double, 18, Val(DROW("DelVal1") & ""))
                data_access.AddParam("@Gamma1", OleDbType.Double, 18, Val(DROW("Gamma1") & ""))
                data_access.AddParam("@GamVal1", OleDbType.Double, 18, Val(DROW("GamVal1") & ""))
                data_access.AddParam("@Vega1", OleDbType.Double, 18, Val(DROW("Vega1") & ""))
                data_access.AddParam("@VgVal1", OleDbType.Double, 18, Val(DROW("VgVal1") & ""))
                data_access.AddParam("@Theta1", OleDbType.Double, 18, Val(DROW("Theta1") & ""))
                data_access.AddParam("@ThVal1", OleDbType.Double, 18, Val(DROW("ThVal1") & ""))
                data_access.AddParam("@Volga1", OleDbType.Double, 18, Val(DROW("Volga1") & ""))
                data_access.AddParam("@VoVal1", OleDbType.Double, 18, Val(DROW("VoVal1") & ""))
                data_access.AddParam("@Vanna1", OleDbType.Double, 18, Val(DROW("Vanna1") & ""))
                data_access.AddParam("@VaVal1", OleDbType.Double, 18, Val(DROW("VaVal1") & ""))
                data_access.AddParam("@DifFactor", OleDbType.Double, 18, Val(DROW("DifFactor") & ""))
                data_access.Cmd_Text = SP_insert_scenarioDetail
                data_access.ExecuteNonQuery()
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub UpdateScenario(ByVal OldScenarioName As String, ByVal NewScenarioName As String)
        Try
            data_access.ParamClear()
            data_access.AddParam("@ScenarioName", OleDbType.VarChar, 20, NewScenarioName)
            data_access.AddParam("@OldScenarioName", OleDbType.VarChar, 20, OldScenarioName)
            data_access.Cmd_Text = SP_update_Scenario_ScenarioName
            data_access.ExecuteNonQuery()


            data_access.ParamClear()
            data_access.AddParam("@ScenarioName", OleDbType.VarChar, 20, NewScenarioName)
            data_access.AddParam("@OldScenarioName", OleDbType.VarChar, 20, OldScenarioName)
            data_access.Cmd_Text = SP_update_ScenarioDetail_ScenarioName
            data_access.ExecuteNonQuery()

            _ScenarioName = NewScenarioName
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Public Sub Delete_scenarioAll()

        data_access.ParamClear()
        data_access.Cmd_Text = SP_delete_scenarioAll
        data_access.ExecuteNonQuery()

        data_access.ParamClear()
        data_access.Cmd_Text = SP_delete_scenarioDetailAll
        data_access.ExecuteNonQuery()

    End Sub
    

End Class
