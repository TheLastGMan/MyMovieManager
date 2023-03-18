@Code
    ViewData("Title") = "My Movie Manager"
End Code

@Html.Partial("_Stats")

@If Not User.Identity.IsAuthenticated Then
    @<div class="FilmCell" style="text-align:center; padding:10px 0;">
        Please Log In To Access Features
    </div>
End If