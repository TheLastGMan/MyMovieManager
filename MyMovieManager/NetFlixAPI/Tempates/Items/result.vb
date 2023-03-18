Public Class result

    Public Property AverageRating As String
    Public Property BoxArt As BoxArt
    Public Property Id As String
    Public Property Instant As Instant
    Public Property Name As String
    Public Property NetflixApiId As String
    Public Property Rating As String
    Public Property ReleaseYear As Integer
    Public Property Url As String
    Public Property Runtime As Integer? 'in seconds

    Public ReadOnly Property RunTimeHMM As String
        Get
            If Runtime.HasValue Then
                Dim hours As Integer = Math.Floor(Runtime.Value / 3600)
                Dim mins As Integer = Math.Floor(Runtime.Value - (hours * 60))
                Return hours & ":" & mins.ToString.PadLeft(2, "0")
            Else
                Return ""
            End If
        End Get
    End Property

    Public Property ShortName As String
    Public Property ShortSynopsis As String
    Public Property Synopsis As String
    Public Property TinyUrl As String
    Public Property Type As String
    Public Property __metadata As movieinfo

    Public ReadOnly Property StreamUrl As String
        Get
            Dim MovieId As String = Url.Substring(Url.LastIndexOf("/") + 1)
            Return String.Format("https://movies.netflix.com/WiPlayer?movieid={0}", MovieId)
        End Get
    End Property

End Class
