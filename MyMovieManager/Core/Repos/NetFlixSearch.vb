Imports RPGCor.MMM.Core.Model

Namespace Repository

    Public Class NetFlixSearch

        Protected ReadOnly DaysToLive As Byte = 7 '7 days
        Protected Shared _NetFlixStreamCache As New List(Of NetFlixMovie)

        Public Shared ReadOnly Property NFCache As List(Of NetFlixMovie)
            Get
                Return _NetFlixStreamCache
            End Get
        End Property

        Private Function ContainsMovie(ByVal movie As String) As Boolean
            If _NetFlixStreamCache.Where(Function(f) f.movie = movie).Count() > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Private Function ReturnStream(ByVal movie As String) As NetFlixMovie
            Dim result As NetFlixMovie = _NetFlixStreamCache.Where(Function(f) f.movie = movie).FirstOrDefault
            Return result
        End Function

        Public Function Search(ByVal movie As String, Optional ByVal year As Integer = 0) As NetFlixMovie
            'movie = movie.Replace("'", "''")
            If movie.Contains("'s") Then
                movie = movie.Substring(movie.IndexOf("'s") + 2)
            End If
            If movie.Contains("(") Then
                movie = movie.Substring(0, movie.IndexOf("("))
            End If
            movie = movie.Trim()

            If ContainsMovie(movie) Then
                Return ReturnStream(movie)
            Else
                'search
                Dim res As NetFlixAPI.NetFlixMovie = New NetFlixAPI.Repository().Search(movie, year)
                Dim movie_res As New NetFlixMovie With {.movie = movie, .instant_stream = False, .added = Now, .url = ""}

                If res IsNot Nothing AndAlso res.d.results.Count > 0 AndAlso res.d.results(0).StreamUrl IsNot Nothing Then
                    'exists add
                    movie_res.instant_stream = res.d.results(0).Instant.Available
                    If res.d.results(0).Instant.AvailableTo IsNot Nothing Then
                        Dim ticklong As String = res.d.results(0).Instant.AvailableTo
                        Dim start As Integer = ticklong.IndexOf("(") + 1
                        Dim epoc As String = ticklong.Substring(start, ticklong.LastIndexOf(")") - start)
                        Dim date1 As DateTime = New DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(epoc / 1000)
                        movie_res.instant_until = date1
                    End If
                    If movie_res.instant_stream Then
                        movie_res.url = res.d.results(0).StreamUrl
                    Else
                        movie_res.url = res.d.results(0).TinyUrl
                    End If

                    Try
                        _NetFlixStreamCache.Add(movie_res)
                    Catch ex As Exception
                        'do nothing, search already exists
                    End Try

                End If

                Return ReturnStream(movie)
            End If

        End Function

    End Class


End Namespace
