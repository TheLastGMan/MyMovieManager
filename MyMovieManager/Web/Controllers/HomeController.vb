Namespace Web
    Public Class HomeController
        Inherits System.Web.Mvc.Controller

        Dim UMR As New Core.Repository.UserMovie

        Function Index() As ActionResult
            Dim model As New StatModel

            If User.Identity.IsAuthenticated Then
                model.topwatchers = UMR.TopWatchers
                model.topmovies = UMR.TopMovies
            End If

            Return View(model)
        End Function

        <ChildActionOnly()>
        Function BoxOffice() As ViewResult
            Return View(New Core.Repository.MovieSearch().BoxOffice())
        End Function

    End Class
End Namespace
