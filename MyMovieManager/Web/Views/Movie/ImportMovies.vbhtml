@ModelType RPGCor.MMM.Web.MovieImportModel

@Code
    ViewData("Title") = "Movie Importer"
End Code

<div class="FilmCell">
    @Using Html.BeginForm("ImportMoviesLoadFile", "Movie", FormMethod.Post, New With {.enctype="multipart/form-data"})
        @<span>File To Upload: </span>@<input type="file" id="file" name="file" for="file" />
        @<input type="submit" value="Upload" class="button_green" />
    End Using
</div>
@If Model.ImportList.Count > 0 Then
    Using Html.BeginForm("ImportMoviesFinalize", "Movie")
    @<div class="FilmCell">
        <table border="1" cellpadding="2" cellspacing="0" style="width:95%; margin:10px auto; background:#CCC;">
            <tr style="text-align:center;">
                <td>IMDB Id</td>
                <td>Title</td>
                <td>Year</td>
                <td>MPAA</td>
                <td>RunTime</td>
            </tr>
            @For i As Integer = 1 To Model.ImportList.Count
                    Dim movie = Model.ImportList(i - 1)
                @<tr style="@(iif(movie.Name.Contains("N/A"),"background-color:red;", ""))">
                    @Html.HiddenFor(Function(f) f.ImportList(i - 1).IMDB_Id)
                    @Html.HiddenFor(Function(f) f.ImportList(i - 1).mpaa)
                    @Html.HiddenFor(Function(f) f.ImportList(i - 1).Name)
                    @Html.HiddenFor(Function(f) f.ImportList(i - 1).poster_url)
                    @Html.HiddenFor(Function(f) f.ImportList(i - 1).RunTime)
                    @Html.HiddenFor(Function(f) f.ImportList(i - 1).Year)
                    @Html.HiddenFor(Function(f) f.ImportList(i - 1).position)
                    <td>@(movie.IMDB_Id)</td>
                    <td>@(movie.Name)</td>
                    <td>@(movie.Year)</td>
                    <td>@(movie.mpaa)</td>
                    <td>@(movie.RunTime)</td>
                </tr>
            Next
        </table>
    </div>
    @<div class="FilmCell" style="text-align:center;">
        <input type="submit" value="Import" class="button_green" />
    </div>
    End Using
End If