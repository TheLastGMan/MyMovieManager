@ModelType UserMovieListModel
@code
    Layout = Nothing
End Code

<script type="text/javascript">
    $(document).ready(function () {
        $('form[id^="nf"]').submit();
    });
</script>

@For i As Integer = 1 To Model.Movies.Count
    Dim lmovie = Model.Movies(i - 1)
    @Html.Action("UserMovieListItem", New With {.model = lmovie, .page = Model.page, .totalmovies = Model.TotalMovies})
Next

