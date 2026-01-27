
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports VolHedge.DAL



Public Class UserDefScenario
    Dim ObjScenarioDet As New scenarioDetail

    Public sTagName As String
    Public bIsValid As Boolean = False




    Public Sub Update_Scenario(ByVal OldScenarioName As String, ByVal NewScenarioName As String)
        ObjScenarioDet.UpdateScenario(OldScenarioName, NewScenarioName)
    End Sub

    Public Sub Insert_Scenario(ByVal sScenarioName As String, ByVal CMP As Double, ByVal CVol As Double, ByVal PVol As Double, ByVal StartDate As Date, ByVal EndDate As Date, ByVal interval As Double, ByVal strike As Double, ByVal MStrike As Double, ByVal interval_type As String, ByVal SelectedDate As String)

        ObjScenarioDet.ScenarioName = sScenarioName
        ObjScenarioDet.CMP = CMP
        ObjScenarioDet.CVol = CVol
        ObjScenarioDet.PVol = PVol
        ObjScenarioDet.StartDate = StartDate
        ObjScenarioDet.EndDate = EndDate
        ObjScenarioDet.interval = interval
        ObjScenarioDet.strike = strike
        ObjScenarioDet.MStrike = MStrike
        ObjScenarioDet.interval_type = interval_type
        ObjScenarioDet.SelectedDate = SelectedDate

        ObjScenarioDet.Delete_scenario()

        ObjScenarioDet.Insert_scenario()


    End Sub



End Class
