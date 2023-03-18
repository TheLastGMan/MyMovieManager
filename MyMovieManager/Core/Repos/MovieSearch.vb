Imports RottenTomatoeAPI
Imports RPGCor.MMM.Core.Model

Namespace Repository

    Public Class MovieSearch
        Private ReadOnly _per_page As Integer = 50
        Protected ReadOnly _keep_alive As Byte = 3
        Private RTR As New RottenTomatoeAPI.Repository
        Private MI As New MovieImportExport

        Protected Shared _MovieSearchCache As New Dictionary(Of String, CacheHolder)

        Public Shared ReadOnly Property MovieCache As Dictionary(Of String, CacheHolder)
            Get
                Return _MovieSearchCache
            End Get
        End Property

        Public Function GetMovie_by_IMDBId(ByVal IMDB_Id As String) As Data.UserMovie
            Try
                Return _MovieSearchCache.Select(Function(f) f.Value.Movies(0)).Where(Function(f) f.IMDB_Id = IMDB_Id).SingleOrDefault
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function Search_Match_IMDB(ByRef query As String) As SearchListResult
            Dim lquery As String = query.ToLower.Trim()

            Dim imdb_num As String = ""
            If query.StartsWith("imdb") Then
                'check cache
                Dim start As Integer = query.IndexOf("{") + 1
                imdb_num = query.Substring(start, query.LastIndexOf("}") - start)
                Dim movie = GetMovie_by_IMDBId(imdb_num)
                If movie IsNot Nothing Then
                    Dim tlst As New List(Of Data.UserMovie)
                    tlst.Add(movie)
                    Return New SearchListResult With {.Movies = tlst, .FromCache = True}
                End If
            End If

            If _MovieSearchCache.ContainsKey(lquery) Then
                Return New SearchListResult With {.Movies = _MovieSearchCache(lquery).Movies, .FromCache = True}
            Else
                'load movies
                Dim movie_list As New List(Of _MovieSearch.Movies)
                If Not query.StartsWith("imdb") Then
                    movie_list = RTR.MovieSearch(lquery, _per_page).movies.Where(Function(f) f.alternate_ids.imdb > 0).OrderByDescending(Function(f) f.year).ToList()
                Else
                    Dim smovie = RTR.MovieAlias(imdb_num)
                    If smovie.title IsNot Nothing Then
                        movie_list.Add(RTR.MovieAlias(imdb_num))
                    End If
                End If

                'add to cache and return
                If movie_list.Count > 0 Then
                    Try
                        _MovieSearchCache.Add(lquery, New CacheHolder() With {.Movies = movie_list.Select(Function(m) MI.RTMovie_Transpose("", 0, m)).ToList, .Created = Now})
                    Catch ex As Exception
                        'do nothing, search already exists
                    End Try
                End If
                Return New SearchListResult With {.Movies = movie_list.Select(Function(m) MI.RTMovie_Transpose("", 0, m)).ToList}
            End If

        End Function

        Public Structure CacheHolder
            Public Property Movies As List(Of Data.UserMovie)
            Public Property Created As DateTime
        End Structure

        Public Function BoxOffice() As List(Of Model.BoxOffice)
            Dim res As New List(Of Model.BoxOffice)
            For Each movie In RTR.BoxOfficeMovies().movies
                Dim bo As New Model.BoxOffice
                With bo
                    bo.Title = movie.title
                    bo.Rating = movie.mpaa_rating
                    bo.RunTime = movie.runtime_HHmm
                    bo.Url = movie.alternate_ids.ImdbUrl
                    bo.Released = movie.release_dates.theater

                    Dim castcount As Integer = movie.abridged_cast.Count
                    For i As Integer = 1 To Math.Min(3, castcount)
                        Dim ci = movie.abridged_cast(i - 1)
                        Dim character As String = "[Unknown]"
                        If ci.characters.Count > 0 Then
                            character = ci.characters(0)
                        End If
                        bo.Cast.Add(String.Format("{0} as {1}", ci.name, character))
                    Next

                End With
                res.Add(bo)
            Next
            Return res
        End Function

    End Class

End Namespace