Public Class RottenTommatoeTask
    Inherits Repository.MovieSearch : Implements Services.ITask

    Public ReadOnly Property ExecMins As Short Implements Services.ITask.ExecMins
        Get
            Return 360
        End Get
    End Property

    Public Sub Execute() Implements Services.ITask.Execute
        'clear old cache
        Dim log As New Core.Repository.Logging()
        log.AddMessage("MMM", "RottenTomatoeTask: Start")

        Dim oldcache As List(Of String) = _MovieSearchCache.Where(Function(f) f.Value.Created.AddDays(_keep_alive) < Now).Select(Function(f) f.Key).ToList
        For Each keyword In oldcache
            _MovieSearchCache.Remove(keyword)
        Next

        If oldcache.Count > 0 Then
            log.AddMessage("MMM", "RottenTomatoeTask: Removed " & oldcache.Count & " Entries")
        End If
        log.AddMessage("MMM", "RottenTomatoeTask: Finish")
    End Sub
End Class
