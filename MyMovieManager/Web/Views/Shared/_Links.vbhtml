@Code
    Layout=""
End Code

<div class="FilmCell" style="text-align:center;">
    @Html.Action("FBLogIn", "Account")
</div>

@If User.Identity.IsAuthenticated Then
    @<div class="FilmCell" style="text-align:center;">
            @Using Html.BeginForm("SearchPost", "Movie", FormMethod.Post, New With {.id = "searchbar"})
                @Html.TextBox("query", "", New With {.type="search", .class="search_box"}) @<input type="submit" value="Search" class="button_green" style="width:100px;" />
            End Using            
    </div>
End If