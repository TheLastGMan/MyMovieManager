<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="initial-scale=1.0,width=device-width" />
    <title>@ViewData("Title")</title>
    <link rel="icon" type="image/png" href="@Url.Content("~/Content/favicon.png")" />
    <link rel="Shortcut Icon" type="image/vnd.microsoft.icon" href="@Url.Content("~/Content/favicon.ico")" />
    <link href="@Url.Content("~/content/site.min.css")" rel="stylesheet" type="text/css" />
    <link href="@Url.Content("~/content/mobile.min.css")" rel="stylesheet" media="screen and (max-device-width:600px), screen and (max-width:600px)" />
    <script src="@Url.Content("~/scripts/common.min.js")" type="text/javascript"> </script>
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
