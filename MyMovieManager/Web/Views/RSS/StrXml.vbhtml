@modeltype String
@Html.Raw(Model)
@Code
    Response.ContentType = "text/xml"
    Layout = Nothing
End Code