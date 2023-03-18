Namespace Repository

    Public Class MovieImportExport

        Private titles As String() = {"IMDB Id", "Position", "Movie Name", "Year Release", "Run Time", "MPAA", "Added"}

        Public Function Export(ByRef FB_ID As String) As String
            Dim SB As New Text.StringBuilder

            Dim movies As List(Of Data.UserMovie) = New UserMovie().GetAllUsersMovies(FB_ID)
            SB.AppendLine(String.Join(",", titles))

            For Each movie In movies
                With movie
                    SB.AppendLine(String.Join(",", New String() {.IMDB_Id, .position, .Name.Replace(",", ";"), .Year, .RunTime, .mpaa, .Added}))
                End With
            Next

            Return SB.ToString
        End Function

        Public Function Import(ByRef Contents As String, ByRef FB_Id As String, ByVal start_cnt As Integer) As List(Of Data.UserMovie)
            Dim lst As New List(Of Data.UserMovie)

            Dim UM As New UserMovie()
            Dim SC As New MovieSearch()

            For Each line As String In Contents.Split(vbCrLf)
                Dim IMDB As String = line.Split(",").GetValue(0).ToString.Trim()
                If Integer.TryParse(IMDB, New Integer) Then
                    'valid
                    'check db
                    Dim movie = UM.GetMovie_by_IMDB(IMDB)
                    If movie IsNot Nothing Then
                        'valid movie
                        lst.Add(movie)
                    Else
                        'check the cache
                        Dim rtmovie = SC.GetMovie_by_IMDBId(IMDB)
                        If rtmovie IsNot Nothing Then
                            'valid
                            lst.Add(rtmovie)
                        Else
                            'run against movie API
                            Dim rtlst = SC.Search_Match_IMDB("imdb{" & IMDB & "}")
                            If rtlst.Movies.Count > 0 Then
                                'copy over
                                lst.Add(rtlst.Movies(0))
                            Else
                                'invalid
                                Dim emovie As New Data.UserMovie
                                emovie.Name = "N/A - Error Loading (" & line.Split(",").GetValue(2) & ") IMDB {" & IMDB & "}"
                                lst.Add(emovie)
                            End If
                        End If
                    End If
                Else
                    'invalid
                    Dim movie As New Data.UserMovie
                    movie.Name = "N/A - Error Loading (" & line.Split(",").GetValue(2) & ") IMDB {" & IMDB & "}"
                    lst.Add(movie)
                End If
            Next

            'update list
            DistinctMovies(lst)

            Return lst
        End Function

        Private Sub DistinctMovies(ByRef movielist As List(Of Data.UserMovie))
            Dim _plst As New List(Of Data.UserMovie)

            For Each movie In movielist
                Dim lmovie = movie
                If movie.Name.Contains("N/A") Or Not _plst.Where(Function(f) f.IMDB_Id = lmovie.IMDB_Id).Count > 0 Then
                    _plst.Add(movie)
                End If
            Next

            movielist = _plst
        End Sub

        Public Function RTMovie_Transpose(ByRef FB_Id As String, ByRef start_count As Integer, ByRef rtmovie As RottenTomatoeAPI._MovieSearch.Movies) As Data.UserMovie
            Dim um As New Data.UserMovie
            With um
                start_count += 1
                um.Added = Now
                um.FB_Id = 0
                um.IMDB_Id = rtmovie.alternate_ids.imdb
                um.mpaa = rtmovie.mpaa_rating
                um.Name = rtmovie.title
                um.poster_url = rtmovie.posters.profile
                um.RunTime = rtmovie.runtime_HHmm
                Integer.TryParse(rtmovie.year, um.Year)
            End With
            Return um
        End Function

    End Class

End Namespace
