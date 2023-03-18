@Code
    Layout=""
End Code

<div class="FilmCell">
    <table border="0" cellpadding="2" cellspacing="0" width="100%">
        <tr>
            <td style="width:35%; text-align:center;">
                @If User.Identity.IsAuthenticated Then
                    Using Html.BeginForm("SearchPost", "Movie", FormMethod.Post, New With {.id = "searchbar"})
                        @Html.TextBox("query", "", New With {.type="search", .class="search_box"}) @<input type="submit" value="Search" class="button_green" style="width:100px;" />
                    End Using            
                End If
            </td>
            <td>@Html.Action("FBLogIn", "Account")</td>
        </tr>
    </table>
</div>
