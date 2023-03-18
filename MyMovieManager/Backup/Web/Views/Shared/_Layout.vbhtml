<!DOCTYPE html>
<html>
<head>
    <title>@ViewData("Title")</title>
    <link rel="icon" type="image/png" href="@Url.Content("~/Content/favicon.png")" />
    <link rel="Shortcut Icon" type="image/vnd.microsoft.icon" href="@Url.Content("~/Content/favicon.ico")" />
    <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet" type="text/css" />
    <script src="@Url.Content("~/Scripts/modernizr-2.6.2.js")" type="text/javascript"> </script>
    <script src="@Url.Content("~/Scripts/jquery-1.8.2.min.js")" type="text/javascript"> </script>
    <script src="@Url.Content("~/Scripts/jquery-ui-1.9.0.min.js")" type="text/javascript"> </script>
    <script src="@Url.Content("~/Scripts/jquery.unobtrusive-ajax.min.js")" type="text/javascript"> </script>
</head>

<body>
    <table border="0" cellpadding="0" cellspacing="0" class="main_container">
        <tr style="height:100%;">
            <td class="animate">
                <div class="container">&nbsp;</div>
            </td>
            <td class="content" style="vertical-align:top;">
                <div class="FilmCell" style="text-align:center;" class="PageHeader">
                    <span class="TitleSequence">My Movie Manager</span>
                </div>
                <noscript>
                    <div class="FilmCell" style="text-align:center;">
                        <span class="TitleSequence">
                            Javascript Is Required
                        </span>
                    </div>
                </noscript>
                <div>@Html.Partial("_Links")</div>
                @RenderBody()
                @Html.Partial("_Footer")
            </td>
            <td class="animate">
                <div class="container">&nbsp;</div>
            </td>
        </tr>
    </table>
</body>
</html>
