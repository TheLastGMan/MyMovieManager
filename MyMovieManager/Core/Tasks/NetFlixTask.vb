Imports RPGCor.MMM.Core.Model
Public Class NetFlixTask
    Inherits Repository.NetFlixSearch : Implements Services.ITask

    Public ReadOnly Property ExecMins As Short Implements Services.ITask.ExecMins
        Get
            Return 360
        End Get
    End Property

    Public Sub Execute() Implements Services.ITask.Execute
        'clear old cache
        Dim log As New Core.Repository.Logging()
        log.AddMessage("MMM", "NetFlixTask: Start")

        Dim oldcache As List(Of NetFlixMovie) = _NetFlixStreamCache.Where(Function(f) f.added.AddDays(DaysToLive) < Now).ToList
        For Each nfmovie In oldcache
            _NetFlixStreamCache.Remove(nfmovie)
        Next

        If oldcache.Count > 0 Then
            log.AddMessage("MMM", "NetFlixTask: Removed " & oldcache.Count & " Entries")
        End If
        log.AddMessage("MMM", "NetFlixTask: Finish")
    End Sub

End Class
