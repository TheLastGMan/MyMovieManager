Public Class TaskManager

    Public Shared Tasks As New List(Of Services.ITask)
    Public Shared Clocks As New List(Of Timers.Timer)

    Public Sub Init()
        Dim ltasks As List(Of Type) = New Services.TypeFinder().FindClassesOfType(Of Services.ITask)()

        If ltasks.Count > Tasks.Count Then
            'different
            StopClocks()
            Tasks.Clear()
            Clocks.Clear()

            For Each task In ltasks
                Tasks.Add(DirectCast(Activator.CreateInstance(task), ITask))
            Next

            ProcThread()
        End If

    End Sub

    Public Sub Clear()
        Tasks.Clear()
        Clocks.Clear()
    End Sub

    Public Sub [Stop]()
        StopClocks()
    End Sub

    Private Sub ProcThread()
        LoadClocks()
        StartClocks()
    End Sub

    Private Sub LoadClocks()

        For Each interval As Short In Tasks.Select(Function(f) f.ExecMins).Distinct.ToList
            Dim timer As New Timers.Timer()
            timer.Interval = interval * 60000
            timer.Stop()
            AddHandler timer.Elapsed, AddressOf ClockTick
            Clocks.Add(timer)
        Next

    End Sub

    Private Sub StartClocks()
        For Each clock In Clocks
            clock.Start()
        Next
    End Sub

    Private Sub StopClocks()
        For Each clock In Clocks
            clock.Stop()
        Next
    End Sub

    Private Sub ClockTick(ByVal sender As Object, ByVal e As Timers.ElapsedEventArgs)
        Dim timer As Timers.Timer = DirectCast(sender, Timers.Timer)
        timer.Stop()

        For Each task In Tasks.Where(Function(f) f.ExecMins = (timer.Interval / 60000)).ToList
            task.Execute()
        Next

        timer.Start()
    End Sub

End Class
