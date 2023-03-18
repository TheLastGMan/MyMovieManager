@ModelType RPGCor.MMM.Web.MoviesModel

@Code
    ViewData("Title") = "Results For " & Model.query
End Code

<script type="text/javascript">
    function PostMovie(title, imdbid, poster) {
        var msg = {
            method: 'feed',
            message: 'I just saw ' + title,
            link: 'http://www.imdb.com/title/tt' + imdbid,
            picture: poster,
            name: title,
            /*caption: title,*/
            /*description: 'I just saw ' + title*/
        };

        FB.ui(msg, function (response) {
            $('#f' + imdbid).submit();
        });
    };
 </script>

@For i As Integer = 1 To Model.SearchResult.Movies.Count
    @<div id="c@(Model.SearchResult.Movies(i-1).IMDB_Id)" style="@(IIf(Model.SearchResult.FromCache, "border:1px solid #FFF;", ""))">
        @Html.Action("SearchMovieInfo", New With {.model = Model.SearchResult.Movies(i-1)})  
    </div>  
Next

@If Model.SearchResult.Movies.Count = 0 Then
    @<div class="FilmCell" style="height:100px; text-align:center;">
        <table border="0" cellpadding="0" cellspacing="0" style="width:100%; height:100%;">
            <tr>
                <td style="vertical-align:middle; font-family:MovieTitle; font-size:26px;">No<br />Results<br />Found</td>
            </tr>
        </table>
    </div>
End If
