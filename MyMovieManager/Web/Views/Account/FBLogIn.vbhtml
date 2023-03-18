@ModelType rpgcor.MMM.Web.FBModel

@code
    Layout= ""
End Code

<div id="fb-root"></div>
<script type="text/javascript">
    // Load the SDK Asynchronously
    (function (d) {
        var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement('script'); js.id = id; js.async = true;
        js.src = "//connect.facebook.net/en_US/all.js";
        ref.parentNode.insertBefore(js, ref);
    } (document));

    // Init the SDK upon load
    window.fbAsyncInit = function () {
        FB.init({
            appId: '@(ConfigurationManager.AppSettings("FB_App_Id"))', // App ID
            channelUrl: '//' + window.location.hostname + '/', // Path to your Channel File
            status: true, // check login status
            cookie: false, // enable cookies to allow the server to access the session
            xfbml: true  // parse XFBML
        });

        // listen for and handle auth.statusChange events
        FB.Event.subscribe('auth.statusChange', function (response) {
           if (response.authResponse) {
                FB.api('/me', function(me) {
                    if (me.name) {
                        @If (Not User.Identity.IsAuthenticated) Then
                            @Html.Raw("$('#FB_ID').attr('value', me.id);" & vbCrLf)
                            @Html.Raw("$('#FB_NAME').attr('value', me.name);" & vbCrLf)
                            @Html.Raw("$('#FB_USERNAME').attr('value', me.username);" & vbCrLf)
                            @Html.Raw("$('#auth-displayname').html(me.name);" & vbCrLf)
                            'have js submit login form
                            @html.Raw("$('#account_login_form').submit();")
                        End If
                    }
                });
            } else {
                //alert('non-event detected');
                //$('#account_logout_form').submit();
            }
        });
        try{
            // respond to clicks on the login and logout links
            $('#auth-loginlink').click(function(event){
                event.preventDefault();
                FB.login();
            });
            @If (Not User.Identity.IsAuthenticated) Then
                @html.Raw("FB.login();")
            End If
        } catch(err){};
    }
</script>

<div>
    <table border="0" cellpadding="1" cellspacing="0" width="100%" style="text-align:center; vertical-align:middle;">
        <tr>
            <td style="line-height:100%;">
                @If User.Identity.IsAuthenticated Then
                    @<img src="https://graph.facebook.com/@(User.Identity.Name)/picture/" alt="Profile Picture" style="width:45px;" />
                End If
                @If Not User.Identity.IsAuthenticated Then
                    Using Html.BeginForm("FBLogInByPass", "Account", FormMethod.Post, New With {.[id] = "account_login_form", .[autocomplete] = "off"})
                        @<input type="hidden" id="FB_ID" name="FB_ID" />
                        @<input type="hidden" id="FB_NAME" name="FB_NAME" />
                        @<input type="hidden" id="FB_USERNAME" name="FB_USERNAME" />
                        @Html.AntiForgeryToken()
                    End Using
                Else
                    @<span style="position:relative; top:-15px; padding-left:5px;"> Welcome <span id="auth-displayname">@(model.FB_Name)</span></span>
                End If
                @Using Html.BeginForm("FBLogOut", "Account", FormMethod.Post, New With {.[id] = "account_logout_form"})
                End Using
            </td>
        </tr>
    </table>
</div>
<div>
    @If Not User.Identity.IsAuthenticated Then
        @<a id="auth-loginlink" href="">Log In With Facebook</a>
    Else
        @<a href="@(Url.Action("UsersMovies", "Movie", New With {.username = Model.FB_UserName}))">My Movies</a>
        @<span>&nbsp;|&nbsp;</span>
        @Html.ActionLink("Stats", "", "Home")
    End If
</div>