@ModelType RPGCor.MMM.Data.UserMovie

@code
    Layout = Nothing
End Code

<div class="FilmCell">
    <div id="u@(Model.IMDB_Id)" style="background-color:#FFF; display:none; width:900px; height:200px; background-color:rgba(0,0,0,0.25); position:fixed; margin:-10px; text-align:center;"><p>Updating...</p></div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family:MovieText; text-align:center;">
        <tr>
            <td style="text-align:center; width:200px;">
                <a target="_blank" href="http://www.imdb.com/title/tt@(Model.IMDB_Id.ToString.PadLeft(7, "0"))" style="border:none;">
                    <img width="100px" class="Movie_Poster" src="@(Model.poster_url)" alt="Movie Poster" />
                </a>
            </td>
            <td style="text-align:center; font-family:MovieTitle; font-size:32px;">
                <p>
                    Are You Sure?
                </p>
            </td>
            <td style="width:175px;">
                @Using Ajax.BeginForm("SearchMovieInfo", New AjaxOptions() With {.Url = Url.Action("SearchMovieInfo"), _
                                                            .UpdateTargetId = "c" & Model.IMDB_Id, _
                                                            .LoadingElementId = "u" & Model.IMDB_Id, _
                                                            .LoadingElementDuration = 0})
                    
                    @Html.HiddenFor(Function(f) f.IMDB_Id)
                    @Html.HiddenFor(Function(f) f.poster_url)
                    @Html.HiddenFor(Function(f) f.mpaa)
                    @Html.HiddenFor(Function(f) f.Name)
                    @Html.HiddenFor(Function(f) f.Year)
                    @Html.HiddenFor(Function(f) f.RunTime)
                    @Html.Hidden("removemovie", "1")
                    @<input type="submit" class="button_green" value="Yes" />@<br />@<br />
                End Using
                @Using Ajax.BeginForm("SearchMovieInfo", New AjaxOptions() With {.Url = Url.Action("SearchMovieInfo"), _
                                                            .UpdateTargetId = "c" & Model.IMDB_Id, _
                                                            .LoadingElementId = "u" & Model.IMDB_Id, _
                                                            .LoadingElementDuration = 0})
                    
                    @Html.HiddenFor(Function(f) f.IMDB_Id)
                    @Html.HiddenFor(Function(f) f.poster_url)
                    @Html.HiddenFor(Function(f) f.mpaa)
                    @Html.HiddenFor(Function(f) f.Name)
                    @Html.HiddenFor(Function(f) f.Year)
                    @Html.HiddenFor(Function(f) f.RunTime)
                    @Html.Hidden("removemovie", "0")
                    @<input type="submit" class="button_red" value="No" />
                End Using
            </td>
        </tr>
    </table>
</div>