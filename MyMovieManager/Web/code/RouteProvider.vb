Public Class RouteProvider : Implements Services.IRouteProvider

    Public ReadOnly Property Priority As Integer Implements Services.IRouteProvider.Priority
        Get
            Return 255
        End Get
    End Property

    Public Sub RegisterRoutes(ByRef routes As System.Web.Routing.RouteCollection) Implements Services.IRouteProvider.RegisterRoutes

        'Short Hand Search
        routes.MapRoute( _
            "SearchRoute", _
            "s/{query}", _
            New With {.controller = "Movie", .action = "Search", .query = UrlParameter.Optional})

        'Users Movie List View
        routes.MapRoute( _
            "UserMovieList", _
            "u/{username}/{Page}", _
            New With {.controller = "Movie", .action = "UsersMovies", .Page = UrlParameter.Optional})
        routes.MapRoute( _
            "UserMovieListID", _
            "ui/{fbid}/{Page}", _
            New With {.controller = "Movie", .action = "UsersMoviesId", .Page = UrlParameter.Optional})

        'Import/Export shorthand
        routes.MapRoute( _
            "MovieImport", _
            "mi", _
            New With {.controller = "Movie", .action = "ImportMovies"})
    End Sub

End Class
