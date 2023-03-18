@Code
    Layout = Nothing
End Code

<div class="FilmCell" style="text-align:center; padding-top:10px;">
    <span style="font-family:MovieTitle; font-size:26px;">Top &nbsp;@(Model.topmovies.Count())&nbsp; Movies</span><br />
    <table border="1" cellpadding="5" cellspacing="0" style="width:95%; margin:10px auto; background-color:#CCC;">
    <tr style="font-family:MovieTitle; font-size:20px;">
        <td>#</td>
        <td>Movie</td>
        <td>Lists</td>
    </tr>
    @For i As Integer = 1 To Model.topmovies.count
    Dim local As RPGCor.MMM.Data.TopMovies = Model.topmovies(i - 1)
        @<tr style="font-size:16px;">
            <td width="15%">@(i)</td>
            <td width="70%">@(local.Name)</td>
            <td>@(local.Seen)</td>
        </tr>
Next
    </table>
</div>