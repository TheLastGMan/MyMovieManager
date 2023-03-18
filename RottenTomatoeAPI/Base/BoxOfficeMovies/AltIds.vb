Namespace _BoxOfficeMovies

    Public Class AltIds
        Public Property imdb As Integer

        Public ReadOnly Property ImdbUrl As String
            Get
                If imdb > 0 Then
                    Return "http://www.imdb.com/title/tt" & imdb.ToString.PadLeft(7, "0") & "/"
                Else
                    Return ""
                End If
            End Get
        End Property
    End Class

End Namespace
