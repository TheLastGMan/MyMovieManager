@ModelType RPGCor.MMM.Web.StatModel
@If User.Identity.IsAuthenticated Then
    @<div class="FilmCell" style="text-align:center; padding-top:10px;">
        <span style="font-family:MovieTitle; font-size:26px;">Top &nbsp;@(Model.topwatchers.Count())&nbsp; Watchers</span><br />
        <table border="1" cellpadding="5" cellspacing="0" style="width:95%; margin:10px auto; background-color:#CCC;">
        <tr style="font-family:MovieTitle; font-size:20px;">
            <td>#</td>
            <td>User</td>
            <td>Movies</td>
        </tr>
        @For i As Integer = 1 To Model.topwatchers.count
        Dim local As RPGCor.MMM.Data.TopWatchers = Model.topwatchers(i - 1)
        Dim user As RPGCor.MMM.Data.User = local.user
            @<tr style="font-size:16px;">
                <td width="15%">@(i)</td>
                <td width="70%">
                    @If user.UserName IsNot Nothing Then
                        @<a href="@(Url.Action("UsersMovies", "Movie", New With {.username = user.UserName}))">@(user.UserName)</a>
        Else
                        @<a href="@(Url.Action("UsersMoviesId", "Movie", New With {.fbid = user.FB_Id}))">@(user.Name)</a>
        End If
                </td>
                <td>@(local.movies)</td>
            </tr>
    Next
        </table>
    </div>
    @<div class="FilmCell" style="text-align:center; padding-top:10px;">
        @Html.Action("BoxOffice", "Home")
    </div>
End If