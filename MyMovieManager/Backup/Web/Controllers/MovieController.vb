Namespace Web

    <Authorize()>
    Public Class MovieController
        Inherits System.Web.Mvc.Controller

        Private UR As New Core.Repository.User
        Private UM As New Core.Repository.UserMovie
        Private IE As New Core.Repository.MovieImportExport

        Function Search(ByVal query As String) As ActionResult
            Dim model As New MoviesModel
            model.query = query
            If query IsNot Nothing AndAlso query.Length > 0 Then
                model.SearchResult = New Core.Repository.MovieSearch().Search_Match_IMDB(query)
            End If
            Return View(model)
        End Function

        <ChildActionOnly()>
        Function SearchMovieInfo(ByVal model As Data.UserMovie) As ViewResult
            Return View(GetSearchModel(model))
        End Function

        <HttpPost()>
        Function SearchMovieInfo(ByVal model As Data.UserMovie, ByVal removemovie As Byte) As ViewResult
            If removemovie = 1 Then
                model.FB_Id = User.Identity.Name
                UM.RemoveMovie(model)
            End If
            Return View(GetSearchModel(model))
        End Function

        <HttpPost()>
        Function SearchMovieAdd(ByVal model As MovieSearchModel) As ViewResult
            model.Movie.FB_Id = User.Identity.Name
            UM.AddMovie(model.Movie)
            Return View("SearchMovieInfo", GetSearchModel(model.Movie))
        End Function

        <NonAction()>
        Private Function GetSearchModel(ByRef model As Data.UserMovie) As MovieSearchModel
            Dim smodel As New MovieSearchModel
            smodel.HasMovie = UM.HasMovie(User.Identity.Name, model.IMDB_Id)
            smodel.Movie = model
            Return smodel
        End Function

        <HttpPost()>
        Function SearchMovieRemove(ByVal model As MovieSearchModel) As ViewResult
            Return View(model.Movie)
        End Function

        <HttpPost()>
        Function SearchMovieRemoveCancel(ByVal model As Data.UserMovie) As ViewResult
            Return SearchMovieInfo(model)
        End Function

        <HttpPost()>
        Function SearchPost(ByVal query As String) As ActionResult
            'redirect to user friendly url
            Return RedirectToRoute("SearchRoute", New With {.query = query.Trim()})
        End Function

        Function UsersMovies(ByVal username As String, Optional Page As Integer = 1) As ActionResult
            'get user id
            Return UsersMoviesId(UR.GetUserId_by_Username(username), Page)
        End Function

        Function UsersMoviesId(ByVal fbid As String, Optional Page As Integer = 1) As ActionResult
            'get model of users movies
            Dim model As New UsersMovieModel

            Dim usr = UR.GetUser_by_Id(fbid)

            If usr Is Nothing Then
                Return RedirectToAction("", "Home")
            End If

            model.userid = usr.FB_Id
            If usr.UserName IsNot Nothing Then
                model.user = usr.UserName
            Else
                model.user = usr.Name
            End If

            model.MaxPages = UM.GetMaxPages(fbid)
            If Page > model.MaxPages Then
                model.CurrentPage = model.MaxPages
            ElseIf Page < 1 Then
                model.CurrentPage = 1
            Else
                model.CurrentPage = Page
            End If

            model.TotalMovies = UM.GetUsersMoviesCount(fbid)
            model.TotalHours = UM.GetUsersTotalHours(fbid)

            For Each entry In UM.Ranking(fbid)
                model.Position = entry.Key
                model.TotalUsers = entry.Value
            Next

            Return View("UsersMovies", model)
        End Function

        <ChildActionOnly()>
        Function UserMovieList(ByVal uid As String, Optional ByVal page As Integer = 1) As ViewResult
            Return View(GetUserMovieList(uid, page))
        End Function

        <NonAction()>
        Private Function GetUserMovieList(ByVal uid As String, ByVal page As Integer) As UserMovieListModel
            Dim model As New UserMovieListModel
            model.Movies = UM.GetUsersMovies(uid, page)
            model.page = page
            model.TotalMovies = UM.GetUsersMoviesCount(uid)
            Return model
        End Function

        <ChildActionOnly()>
        Function UserMovieListItem(ByVal model As Data.UserMovie, ByVal totalmovies As Integer, Optional ByVal page As Integer = 1) As ViewResult
            Dim nmodel As New UserMovieListItemModel
            nmodel.movie = model
            nmodel.page = page
            nmodel.totalmovies = totalmovies
            Return View(nmodel)
        End Function

        <HttpPost()>
        Function UpdateUserMovie(ByVal IMDB As String, ByVal position As Integer, ByVal page As Integer) As ViewResult
            UM.UpdatePosition(IMDB, position, User.Identity.Name)
            Return View("UserMovieList", GetUserMovieList(User.Identity.Name, page))
        End Function

        <HttpPost()>
        Function RemoveUserMovieConfirm(ByVal IMDB As String, ByVal page As Integer, ByVal poster_url As String, ByVal position As Integer) As ViewResult
            Dim model As New UserMovieRemoveConfirmModel
            model.imdb = IMDB
            model.page = page
            model.poster_url = poster_url
            model.position = position
            Return View(model)
        End Function

        <HttpPost()>
        Function RemoveUserMovie(ByVal IMDb As String, ByVal page As Integer, ByVal position As Integer) As ViewResult
            UM.RemoveMovie(IMDb, User.Identity.Name, position)
            Return View("UserMovieList", GetUserMovieList(User.Identity.Name, page))
        End Function

        <HttpPost()>
        Function RemoveUserMovieCancel(ByVal page As Integer) As ViewResult
            Return View("UserMovieList", GetUserMovieList(User.Identity.Name, page))
        End Function

        Function ExportMovies() As FileContentResult
            Return File(Encoding.UTF8.GetBytes(IE.Export(User.Identity.Name)), "text/csv", User.Identity.Name & "_" & Now.ToShortDateString.Replace("/", "-") & ".csv")
        End Function

        Function ImportMovies() As ActionResult
            Return View(New MovieImportModel)
        End Function

        <HttpPost()>
        Function ImportMoviesFinalize(ByVal model As MovieImportModel) As ActionResult
            UM.BulkAdd(model.ImportList.Where(Function(f) Not f.Name.Contains("N/A")).ToList, User.Identity.Name)
            Return RedirectToAction("UsersMoviesId", New With {.fbid = User.Identity.Name})
        End Function

        <HttpPost()>
        Function ImportMoviesLoadFile(ByVal model As MovieImportModel, ByVal file As HttpPostedFileBase) As ActionResult
            model.ImportList.Clear()
            If ModelState.IsValid AndAlso file IsNot Nothing Then
                Dim bcontent As Byte() = New Byte(file.ContentLength) {}
                file.InputStream.Read(bcontent, 0, file.ContentLength)
                Dim fcontent As String = Text.Encoding.UTF8.GetString(bcontent)
                model.ImportList = IE.Import(fcontent, User.Identity.Name, UM.GetUsersMoviesCount(User.Identity.Name) + 1)
            End If
            Return View("ImportMovies", model)
        End Function

    End Class
End Namespace
