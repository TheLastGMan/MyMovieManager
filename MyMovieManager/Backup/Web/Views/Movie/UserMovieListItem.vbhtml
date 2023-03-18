@ModelType UserMovieListItemModel
@Code
    Layout = Nothing
End Code

<div id="l@(Model.movie.IMDB_Id)">
    <div class="FilmCell">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family:MovieText; text-align:center;">
            <tr>
                <td style="text-align:center; width:200px;">
                    <a target="_blank" href="http://imdb.com/title/tt@(Model.movie.IMDB_Id.ToString.PadLeft(7,"0"))" style="border:none;">
                        <img width="100px" class="Movie_Poster" src="@(Model.movie.poster_url)" alt="Movie Poster" />
                    </a>
                </td>
                @If Model.movie.FB_Id = User.Identity.Name Then
                    @<td style="text-align:center; vertical-align:middle; width:55px;" id="nf@(Model.movie.IMDB_Id)">
                        @Using Ajax.BeginForm(Url.Action("NetFlixStream", "MovieAsync"), Nothing, New AjaxOptions With {.UpdateTargetId = "nf" & Model.movie.IMDB_Id, _
                                                                .Url = Url.Action("NetFlixStream", "MovieAsync")}, New With {.id = "nf" & Model.movie.IMDB_Id})
                        @Html.Hidden("movie", Model.movie.Name)
                        @Html.Hidden("year", Model.movie.Year)
                        End Using
                    </td>  
                        End If
                <td>
                    <b>@(Model.movie.Name & " (" & Model.movie.Year & ")")</b><br />
                    Added : @(Model.movie.Added.ToShortDateString)<br />
                    Rated : @(Model.movie.mpaa)<br />
                    Runtime : @(Model.movie.RunTime)<br />
                </td>
                @If Model.movie.FB_Id = User.Identity.Name Then
                    @<td style="width:175px;">
                    @Using Ajax.BeginForm(New AjaxOptions() With {.Url = Url.Action("UpdateUserMovie"), _
                                                                 .UpdateTargetId = "mymovielist", _
                                                                 .LoadingElementId = "mymovielistupdate", _
                                                                 .LoadingElementDuration = 0})
                        @Html.Hidden("IMDB", Model.movie.IMDB_Id)
                        @Html.Hidden("page", Model.page)
                        @<span><b>Position</b></span>@Html.TextBox("position", Model.movie.position, New With {.type = "number", .width = "50px", .min = "1", .max = Model.totalmovies})
                        @<br />@<br />
                        @<input type="submit" class="button_green" value="Update" />
                    End Using
                    <br />
                    @Using Ajax.BeginForm(New AjaxOptions() With {.Url = Url.Action("RemoveUserMovieConfirm"), _
                                                                 .UpdateTargetId = "l" & Model.movie.IMDB_Id, _
                                                                 .LoadingElementId = "mymovielistupdate", _
                                                                 .LoadingElementDuration = 0})
                        @Html.Hidden("IMDB", Model.movie.IMDB_Id)
                        @Html.Hidden("page", Model.page)
                        @Html.Hidden("poster_url", Model.movie.poster_url)
                        @Html.Hidden("position", Model.movie.position)
                        @<input type="submit" class="button_red" value="Remove" />  
                    End Using   
                </td>
                Else
                    @<td style="font-size:38px; font-family:MovieTitleAlt; text-align:center; width:175px;">
                        @(Model.movie.position)
                    </td>
                End If
            </tr>
        </table>
    </div>
</div>