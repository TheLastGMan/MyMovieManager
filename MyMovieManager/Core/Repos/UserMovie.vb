Namespace Repository

    Public Class UserMovie

        Private DBC As New Data.Context

        Protected ReadOnly Property UsersMovie As List(Of Data.UserMovie)
            Get
                Return DBC.UsersMovies.ToList
            End Get
        End Property

        Public Function GetUsersMovies(ByVal user_id As String, Optional ByVal Page As Integer = 1, Optional ByVal PerPage As Integer = 50) As List(Of Data.UserMovie)
            Return UsersMovie.Where(Function(f) f.FB_Id = user_id).OrderBy(Function(f) f.position).Skip(PerPage * (Page - 1)).Take(PerPage).ToList
        End Function

        Public Function GetMaxPages(ByRef fbid As String, Optional ByVal PerPage As Integer = 50) As Integer
            Return Math.Ceiling(GetUsersMoviesCount(fbid) / PerPage)
        End Function

        Public Function GetAllUsersMovies(ByVal user_id As String) As List(Of Data.UserMovie)
            Return UsersMovie.Where(Function(f) f.FB_Id = user_id).OrderByDescending(Function(f) f.position).ToList
        End Function

        Public Function GetUsersMoviesCount(ByVal user_id As String) As Integer
            Return UsersMovie.Where(Function(f) f.FB_Id = user_id).Select(Function(f) f.FB_Id).Count
        End Function

        Public Function GetUsersTotalHours(ByVal FBID As String) As String
            Dim hours As Integer = UsersMovie.Where(Function(u) u.FB_Id = FBID And u.RunTime IsNot Nothing).Sum(Function(s) IIf(s.RunTime.Contains(":"), s.RunTime.Split(":").GetValue(0), 0))
            Dim mins As Integer = UsersMovie.Where(Function(u) u.FB_Id = FBID And u.RunTime IsNot Nothing).Sum(Function(s) IIf(s.RunTime.Contains(":"), s.RunTime.Split(":").GetValue(1), 0))
            Dim ts As New TimeSpan(hours, mins, 0)
            Return Math.Floor(ts.Days) & "d " & ts.Hours & ":" & ts.Minutes.ToString.PadLeft(2, "0")
        End Function

        Public Function GetMovie_by_IMDB(ByVal IMDB_Id As String) As Data.UserMovie
            'return last as it should be the most updated
            Return UsersMovie.Where(Function(f) f.IMDB_Id = IMDB_Id).LastOrDefault()
        End Function

        Public Function TopWatchers(Optional limit As Integer = 10) As List(Of Data.TopWatchers)
            Dim lst As New List(Of Data.TopWatchers)

            Dim list = UsersMovie.GroupBy(Function(fid) fid.FB_Id).OrderByDescending(Function(f) f.Select(Function(u) u.FB_Id).Count).Select(Function(s) New Data.TopWatchers() With {.userId = s.Key, .movies = GetUsersMoviesCount(s.Key)}).Take(limit).ToList()
            Dim UM As New User
            For Each itm In list
                itm.user = UM.GetUser_by_Id(itm.userId)
            Next
            lst = list

            Return lst
        End Function

        Public Function TopMovies(Optional limit As Integer = 10) As List(Of Data.TopMovies)
            Dim list = UsersMovie.GroupBy(Function(imdb) imdb.IMDB_Id).OrderBy(Function(f) f.Select(Function(n) n.Name).FirstOrDefault).OrderByDescending(Function(cnt) cnt.Count(Function(c) c.IMDB_Id)).Select(Function(s) New Data.TopMovies() With {.Name = s.Select(Function(a) a.Name).FirstOrDefault, .Seen = s.Count}).Take(limit).ToList
            Return list
        End Function

        Public Function Ranking(ByVal fbid As Long) As Dictionary(Of Integer, Integer)
            Dim list As List(Of Integer) = UsersMovie.GroupBy(Function(fid) fid.FB_Id).Select(Function(f) f.Count(Function(ms) ms.FB_Id)).Distinct.OrderByDescending(Function(f) f).ToList
            Dim moviecount As Integer = GetUsersMoviesCount(fbid)

            Dim kvp As New Dictionary(Of Integer, Integer)

            For i As Integer = 1 To list.Count
                If list(i - 1) = moviecount Then
                    kvp.Add(i, list.Count)
                    Return kvp
                End If
            Next

            kvp.Add(list.Count, list.Count)
            Return kvp
        End Function

        Public Function HasMovie(ByVal FBID As String, ByVal IMDB As String) As Boolean
            If UsersMovie.Where(Function(w) w.FB_Id = FBID And w.IMDB_Id = IMDB).Select(Function(s) s.GUID).Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function BulkAdd(ByRef movielist As List(Of Data.UserMovie), ByRef fbid As String) As Boolean
            Try
                For Each movie In movielist
                    If Not HasMovie(fbid, movie.IMDB_Id) Then
                        movie.FB_Id = fbid
                        movie.Added = Now
                        DBC.UsersMovies.Add(movie)
                    End If
                Next
                DBC.SaveChanges()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function AddMovie(ByVal movie As Data.UserMovie) As Boolean
            Try
                movie.Added = Now
                movie.position = GetUsersMoviesCount(movie.FB_Id) + 1
                DBC.UsersMovies.Add(movie)
                DBC.SaveChanges()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function UpdatePosition(ByVal IMDBx As String, ByVal NPOS As Integer, ByVal UID As String) As Boolean
            Try
                Dim opos As Integer = UsersMovie.Where(Function(f) f.FB_Id = UID).Where(Function(f) f.IMDB_Id = IMDBx).Select(Function(f) f.position).FirstOrDefault

                'check for valid movie position
                Dim utm As Integer = GetUsersMoviesCount(UID)
                If npos < 1 Then
                    npos = 1
                ElseIf npos > utm Then
                    npos = utm
                End If

                'update movies next in line
                Dim lst As New List(Of Data.UserMovie)
                Dim diff As Integer = 1
                If npos > opos Then
                    lst = UsersMovie.Where(Function(f) f.FB_Id = UID).Where(Function(p) p.position > opos And p.position <= NPOS).ToList
                    diff = -1
                Else
                    'npos < opos
                    lst = UsersMovie.Where(Function(f) f.FB_Id = UID).Where(Function(p) p.position < opos And p.position >= NPOS).ToList
                End If
                For Each movie In lst
                    movie.position += diff
                Next

                Dim umovie As Data.UserMovie = UsersMovie.Where(Function(f) (f.FB_Id = UID) And (f.IMDB_Id = IMDBx)).FirstOrDefault
                umovie.position = npos

                DBC.SaveChanges()

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function RemoveMovie(ByVal movie As Data.UserMovie) As Boolean
            Return RemoveMovie(movie.IMDB_Id, movie.FB_Id, movie.position)
        End Function

        Public Function RemoveMovie(ByVal imdb As String, ByVal uid As String, ByVal pos As Integer) As Boolean
            Try
                Dim rmovie As Data.UserMovie = UsersMovie.Where(Function(f) f.FB_Id = uid).Where(Function(f) f.IMDB_Id = imdb).FirstOrDefault
                pos = rmovie.position
                DBC.UsersMovies.Remove(rmovie)
                DBC.SaveChanges()
                'update movies next in line
                For Each mov In UsersMovie.Where(Function(f) f.FB_Id = uid And f.position > pos).ToList
                    mov.position -= 1
                Next
                DBC.SaveChanges()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Class

End Namespace
