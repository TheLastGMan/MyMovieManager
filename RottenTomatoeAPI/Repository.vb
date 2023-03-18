Imports System.Text

Public Class Repository

    Private _Params As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Public Function MovieSearch(ByRef q As String, Optional ByVal page_limit As Integer = 30, Optional ByVal page As Integer = 1) As MoviesSearch
        _Params.Add("q", q)
        _Params.Add("page_limit", IIf(page_limit <= 50, page_limit, 50))
        _Params.Add("page", page)
        Return RunRequest(Of MoviesSearch)(GenerateUrl("movies", _Params))
    End Function

    Public Function ListsDirectory() As ListsDirectory
        Return RunRequest(Of ListsDirectory)(GenerateUrl("lists"))
    End Function

    Public Function MovieListsDirectory() As MovieListDirectory
        Return RunRequest(Of MovieListDirectory)(GenerateUrl("lists/movies"))
    End Function

    Public Function BoxOfficeMovies(Optional ByVal limit As Integer = 10, Optional ByRef country As String = "us") As BoxOffice
        _Params.Add("limit", limit)
        _Params.Add("country", country.ToLower)
        Return RunRequest(Of BoxOffice)(GenerateUrl("lists/movies/box_office", _Params))
    End Function

    Public Function InTheaters(Optional ByVal page_limit As Integer = 16, Optional ByVal page As Integer = 1, Optional ByRef country As String = "us") As InTheaters
        _Params.Add("page_limit", iif(page_limit <= 50, page_limit, 50))
        _Params.Add("page", page)
        _Params.Add("country", country.ToLower)
        Return RunRequest(Of InTheaters)(GenerateUrl("lists/movies/in_theaters", _Params))
    End Function

    Public Function OpeningMovies(Optional ByVal limit As Integer = 16, Optional ByRef country As String = "us") As OpeningMovies
        _Params.Add("limit", limit)
        _Params.Add("country", country.ToLower)
        Return RunRequest(Of OpeningMovies)(GenerateUrl("lists/movies/opening", _Params))
    End Function

    Public Function UpcomingMovies(Optional ByVal page_limit As Integer = 16, Optional ByVal page As Integer = 1, Optional ByRef country As String = "us") As UpcomingMovies
        _Params.Add("page_limit", iif(page_limit <= 50, page_limit, 50))
        _Params.Add("page", page)
        _Params.Add("country", country.ToLower)
        Return RunRequest(Of UpcomingMovies)(GenerateUrl("lists/movies/upcoming", _Params))
    End Function

    Public Function DVDlists() As DVDLists
        Return RunRequest(Of DVDLists)(GenerateUrl("dvds"))
    End Function

    Public Function MovieAlias(ByRef IMDB_id As String) As _MovieSearch.Movies
        _Params.Add("type", "imdb")
        _Params.Add("id", IMDB_id.PadLeft(7, "0"))
        Return RunRequest(Of _MovieSearch.Movies)(GenerateUrl("movie_alias", _Params))
    End Function

    Private Function GenerateUrl(ByRef navpage As String, Optional ByRef params As Dictionary(Of String, String) = Nothing)
        Dim SB As New StringBuilder
        SB.Append(BaseUrl & navpage & ".json?apikey=" & APIKey)
        If params IsNot Nothing Then
            For Each obj As KeyValuePair(Of String, String) In params
                SB.Append("&" & obj.Key & "=" & obj.Value)
            Next
        End If
        Return SB.ToString
    End Function

    Private Function RunRequest(Of T As RTBase)(ByRef url As String) As T
        Try
            Dim wr As Net.WebRequest = Net.WebRequest.Create(New Uri(url))
            wr.Timeout = 15000
            Dim resp = wr.GetResponse()
            Dim str As New IO.StreamReader(wr.GetResponse.GetResponseStream())
            Dim response As String = str.ReadToEnd

            'clean up
            _Params.Clear()
            str.Dispose()

            Return Newtonsoft.Json.JsonConvert.DeserializeObject(Of T)(response)
        Catch ex As Exception
            Throw New Exception("External Movie Search API Returned: " & ex.Message)
        End Try
    End Function

    Public ReadOnly Property BaseUrl As String
        Get
            Return My.Settings.API_Url
        End Get
    End Property

    Public ReadOnly Property APIKey As String
        Get
            Return My.Settings.API_Key
        End Get
    End Property

End Class
