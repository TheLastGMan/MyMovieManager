Namespace _MovieSearch

    Public Class Movies : Inherits RTBase

        Public Property title As String
        Public Property year As String
        Public Property runtime As String
        Public Property release_dates As New ReleaseDates
        Public Property ratings As New Ratings
        Public Property synopsis As String
        Public Property posters As New Posters
        Public Property abridged_cast As New List(Of AbridgedCast)
        Public Property links As New _BoxOfficeMovies.Links

        Public Property mpaa_rating As String
        Public Property critics_consensus As String
        Public Property alternate_ids As New _BoxOfficeMovies.AltIds

        Public ReadOnly Property runtime_HHmm As String
            Get
                If runtime IsNot Nothing AndAlso runtime.Length > 0 Then
                    Dim hours As Integer = Math.Floor(runtime / 60)
                    Dim mins As Integer = Math.Floor(runtime - (hours * 60))
                    Return hours.ToString.PadLeft(2, "0") & ":" & mins.ToString.PadLeft(2, "0")
                Else
                    Return runtime
                End If
            End Get
        End Property

    End Class

End Namespace
