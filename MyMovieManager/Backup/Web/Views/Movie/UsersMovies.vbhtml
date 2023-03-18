@ModelType RPGCor.MMM.Web.UsersMovieModel

@Code
    ViewData("Title") = Model.user & "'s Movies"
End Code

<div class="FilmCell">
    <div style="text-align:center; font-family:MovieTitle;">
        <span style="font-family:MovieTitle; font-size:32px;">@(model.user)</span>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
        @If Model.TotalMovies > 0 Then
            @<tr style="font-size:18px;">
                <td style="text-align:right; vertical-align:middle; font-family:MovieTitle;">
                    Ranking :&nbsp;
                </td>
                <td style="text-align:left;">
                    @(model.Position) of @(model.TotalUsers)
                </td>
            </tr>
        End If
        <tr style="font-size:18px;">
            <td style="text-align:right; vertical-align:middle; width:50%; font-family:MovieTitle;">
                Movies :&nbsp;
            </td>
            <td style="text-align:left;">
                @(Model.TotalMovies)
            </td>
        </tr>
        <tr style="font-size:18px;">
            <td style="text-align:right; font-family:MovieTitle;">
                Hours :&nbsp;
            </td>
            <td style="text-align:left;">
                @(Model.TotalHours)
            </td>
        </tr>
    </table>
</div>

@If Model.TotalMovies > 0 AndAlso Model.userid = User.Identity.Name Then
    @<script type="text/javascript">
        $(function () {
            $("#export_button").click(function (event) {
                if ($.browser.msie) {
                    event.preventDefault();
                    $("#" + $(this).attr("form")).submit();
                }
            });
        });
    </script>
    @<div class="FilmCell" style="text-align:center;">
        @Using Html.BeginForm("ImportMovies", "Movie", FormMethod.Get)
            @<input type="submit" class="button_green" value="Import" />@<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>@<input id="export_button" type="submit" class="button_red" value="Export" form="exportmovies" /> 
        End Using
        @Using Html.BeginForm("ExportMovies", "Movie", Nothing, FormMethod.Post, New With {.id="exportmovies"})    
        End Using
    </div>
End If

@If Model.MaxPages > 1 Then
    @<div class="FilmCell" id="PagerLinks" style="text-align:center;">
        Reel Number :&nbsp;@Html.PageLinks(Model.CurrentPage, Model.MaxPages, Function(f) Url.Action("UsersMovies", "Movie", New With {.username = Model.user, .Page = f}))
    </div>
End If

<div id="mymovielistupdate" style="display:none; position:fixed; left:0px; width:100%; height:100%; background-color:rgba(0,0,0,0.25); text-align:center;">Updating...</div>
<div id="mymovielist">
    @Html.Action("UserMovieList", New With {.uid = Model.userid, .page = Model.CurrentPage})
</div>

@If Model.TotalMovies = 0 Then
    @<div class="FilmCell" style="height:100px; text-align:center;">
        <table border="0" cellpadding="0" cellspacing="0" style="width:100%; height:100%;">
            <tr>
                <td style="vertical-align:middle; font-family:MovieTitle; font-size:26px;">No<br />Movies<br />Found</td>
            </tr>
        </table>
    </div>
End If

@If Model.MaxPages > 1 Then
    @<div class="FilmCell" id="PagerLinks" style="text-align:center;">
        Reel Number :&nbsp;@Html.PageLinks(Model.CurrentPage, Model.MaxPages, Function(f) Url.Action("UsersMovies", "Movie", New With {.username = Model.user, .Page = f}))
    </div>
End If