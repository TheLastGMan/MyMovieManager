@ModelType NetFlixMovieModel
@Code
    Layout = Nothing
End Code

@If Model.result.url.Length > 0 Then
    @<a target="_blank" href="@(Model.result.url)" style="border:none;"><img style="border:none;" width="50px" src="@(Url.Content("~/Content/images/" & IIf(Model.result.instant_stream, "netflix_movie.gif", "netflix.gif")))" alt="@(Model.result.movie)" /></a>
End If
