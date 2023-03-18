Public Class KeepAlive : Implements Services.ITask

    Public ReadOnly Property ExecMins As Short Implements Services.ITask.ExecMins
        Get
            'once a minute
            Return 1
        End Get
    End Property

    Public Sub Execute() Implements Services.ITask.Execute
        'keep alive ping
        Dim Log As New Repository.Logging
        Try
            Using client As New Net.WebClient
                Dim response As String = client.DownloadString("http://mmm.rpgcor.com/WebServices/Ping.ashx")
            End Using
            Log.AddMessage("MMM", "Keep Alive Ran | NFC: " & Repository.NetFlixSearch.NFCache.Count & " | RTC: " & Repository.MovieSearch.MovieCache.Count)
        Catch ex As Exception
            Log.AddMessage("MMM", "Keep Alive Error: " & ex.Message)
            'Throw New Exception("KeepAlive Error: " & ex.Message)
        End Try
    End Sub

End Class
