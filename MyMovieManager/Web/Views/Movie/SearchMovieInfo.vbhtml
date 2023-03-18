@ModelType MovieSearchModel
    
@code
    Layout = Nothing
    Dim form As String = IIf(Model.HasMovie, "SearchMovieRemove", "SearchMovieAdd")
End Code

<div class="FilmCell">
    <div id="u@(Model.Movie.IMDB_Id)" style="background-color:#FFF; display:none; width:900px; height:200px; background-color:rgba(0,0,0,0.25); position:fixed; margin:-10px; text-align:center;"><p>Updating...</p></div>
    <table class="search_result" cellpadding="0" cellspacing="0">
        <tr>
            <td class="posterbox">
                <a target="_blank" href="http://www.imdb.com/title/tt@(Model.Movie.IMDB_Id.ToString.PadLeft(7,"0"))" style="border:none;">
                    <img class="Movie_Poster" src="@(Model.Movie.poster_url)" alt="Movie Poster" />
                </a>
            </td>
            <td class="titlebox">
                <b>@(Model.Movie.Name & " (" & Model.Movie.Year & ")")</b><br />
                Rated : @(Model.Movie.mpaa)<br />
                Runtime : @(Model.Movie.RunTime)<br />
            </td>
            <td class="buttonbox">
                @Using Ajax.BeginForm(form, Nothing, New AjaxOptions() With {.Url = Url.Action(form), _
                                                                                           .UpdateTargetId="c" & Model.Movie.IMDB_Id, _
                                                                                           .LoadingElementId="u" & Model.Movie.IMDB_Id, _
                                                                                           .LoadingElementDuration=0}, _
                                                                                        New With {.id = "f" & Model.Movie.IMDB_Id})
                    @Html.HiddenFor(Function(f) f.Movie.IMDB_Id)
                    @Html.HiddenFor(Function(f) f.Movie.poster_url)
                    @Html.HiddenFor(Function(f) f.Movie.mpaa)
                    @Html.HiddenFor(Function(f) f.Movie.Name)
                    @Html.HiddenFor(Function(f) f.Movie.Year)
                    @Html.HiddenFor(Function(f) f.Movie.RunTime)
                    @Html.Hidden("removemovie", "2")
                    If Model.HasMovie Then
                        @<input type="submit" class="button_red" value="Remove" />     
                    Else
                        @<input type="button" class="button_green" value="Add" onclick="PostMovie('@(Model.Movie.Name.Replace("'",""))', '@(Model.Movie.IMDB_Id)', '@(Model.Movie.poster_url)')" />
                    End If
                End Using
            </td>
        </tr>
    </table>
</div>