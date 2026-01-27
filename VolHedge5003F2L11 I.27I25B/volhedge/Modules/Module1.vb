Module Module1

    Public Sub fullcontrol()
        On Error Resume Next
        Dim VarExplorerArgvv As String = "permission"
        Shell(Application.StartupPath & "\" & "FullControl.exe " & VarExplorerArgvv, AppWinStyle.Hide, False, -1)
        '//ShellExecute(this.Handle, "open", "notepad", "", "", 3);
        'Process p1 = new Process();
        'p1.StartInfo = new ProcessStartInfo(Application.StartupPath + "\\" + "FullControl.exe", VarExplorerArgvv);
        'p1.StartInfo.UseShellExecute = true;
        'p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        'p1.Start();
    End Sub

    





End Module
