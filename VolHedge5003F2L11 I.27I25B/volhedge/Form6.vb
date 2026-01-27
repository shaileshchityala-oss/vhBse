Imports System.Threading

Imports System.Diagnostics
Public Class Form6
    Dim cpuUsage As New PerformanceCounter("Processor", "% Processor Time", "_Total")
    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

      


    End Sub


    Private Sub Timer1_Tick_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ListBox1.Items.Add(cpuUsage.NextValue)
        'Dim PC As New PerformanceCounter()
        'PC.CategoryName = "Processor"
        'PC.CounterName = "% Processor Time"
        'PC.InstanceName = "_Total"
        'MessageBox.Show(PC.NextValue().ToString())
    End Sub
End Class