@ModelType RPGCor.MMM.Web.UserMovieRemoveConfirmModel

@Code
    Layout = Nothing
End Code

<div class="FilmCell">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="font-family:MovieText; text-align:center;">
        <tr>
            <td style="text-align:center; width:200px;">
                <a target="_blank" href="http://www.imdb.com/title/tt@(Model.imdb.ToString.PadLeft(7, "0"))" style="border:none;">
                    <img width="100px" class="Movie_Poster" src="@(Model.poster_url)" alt="Movie Poster" />
                </a>
            </td>
            <td style="text-align:center; font-family:MovieTitle; font-size:32px;">
                <p>
                    Are You Sure?
                </p>
            </td>
            <td style="width:175px;">
                @Using Ajax.BeginForm(New AjaxOptions() With {.Url = Url.Action("RemoveUserMovie"), _
                                                                 .UpdateTargetId = "mymovielist", _
                                                                 .LoadingElementId = "mymovielistupdate", _
                                                                 .LoadingElementDuration = 0})
                    @Html.HiddenFor(Function(f) f.IMDB)
                    @Html.HiddenFor(Function(f) f.page)
                    @Html.HiddenFor(Function(f) f.position)
                    @<input type="submit" class="button_green" value="Yes" />@<br />@<br />
                End Using
                @Using Ajax.BeginForm(New AjaxOptions() With {.Url = Url.Action("RemoveUserMovieCancel"), _
                                                                 .UpdateTargetId = "mymovielist", _
                                                                 .LoadingElementId = "mymovielistupdate", _
                                                                 .LoadingElementDuration = 0})
                    @Html.Hidden("page", Model.page)
                    @<input type="submit" class="button_red" value="No" />
                End Using
            </td>
        </tr>
    </table>
</div>