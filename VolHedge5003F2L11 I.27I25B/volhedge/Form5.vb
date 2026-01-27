Public Class Form5


    Dim lv As New ListView
    'Keeping track of these processes uses a lot of CPU!
    'Use a timer to only poll every few seconds
    Dim WithEvents t As New Timer
    'Arraylist to hold PerformanceCounter measuring the %CPU
    Dim ProcessorPerformanceCounters As ArrayList = New ArrayList
    'Arraylist to hold PerformanceCounter measuring the memory used
    Dim MemoryPerformanceCounters As ArrayList = New ArrayList

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'for this example we get the processes that exist when the form loads only...
        Me.Controls.Add(lv)
        lv.View = View.Details
        lv.Dock = DockStyle.Fill
        lv.Columns.Add("Process Name", 300, HorizontalAlignment.Left)
        lv.Columns.Add("% Processor time", 100, HorizontalAlignment.Left)
        lv.Columns.Add("Private Bytes (kb)", 100, HorizontalAlignment.Left)
        Me.WindowState = FormWindowState.Maximized

        Dim Processes As Process() = Process.GetProcesses
        For Each p As Process In Processes
            ' Create performance counters for each process
            Dim PCounter1 As New PerformanceCounter
            PCounter1.CategoryName = "Process"
            PCounter1.CounterName = "% Processor Time"
            PCounter1.InstanceName = p.ProcessName
            ProcessorPerformanceCounters.Add(PCounter1)
            Dim PCounter2 As New PerformanceCounter
            'Private bytes is the memory reserved for the exclusive
            'use of this process. You won't find anything that matches 
            'the task manager. This one will detect memory leaks.
            PCounter2.CategoryName = "Process"
            PCounter2.CounterName = "Private Bytes"
            PCounter2.InstanceName = p.ProcessName
            MemoryPerformanceCounters.Add(PCounter2)
            Dim lvi As New ListViewItem(p.ProcessName)
            lvi.SubItems.Add("0") ' start value %cpu
            lvi.SubItems.Add("0") ' start value memory
            lv.Items.Add(lvi)
        Next
        t.Interval = 3000
        t.Start()
    End Sub

    Private Sub t_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles t.Tick
        For i As Integer = 0 To ProcessorPerformanceCounters.Count - 1
            'Poll the Performance counters and add to the listview
            lv.Items(i).SubItems(1).Text = CInt(ProcessorPerformanceCounters(i).NextValue).ToString
            lv.Items(i).SubItems(2).Text = CInt(MemoryPerformanceCounters(i).NextValue / 1024).ToString
        Next
        lv.Invalidate()
    End Sub
End Class