Namespace Web
    Public Class AccountController
        Inherits System.Web.Mvc.Controller

        Private UR As New Core.Repository.User

        <ChildActionOnly()>
        Function FBLogIn() As ActionResult
            Dim model As New FBModel
            If User.Identity.IsAuthenticated Then
                Dim usr = UR.GetUser_by_Id(User.Identity.Name)
                If usr IsNot Nothing Then
                    model.FB_ID = usr.FB_Id
                    model.FB_Name = usr.Name
                    model.FB_UserName = usr.UserName
                End If
            End If
            Return View(model)
        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function FBLogInBypass(ByVal model As FBModel) As ActionResult
            FormsAuthentication.SetAuthCookie(model.FB_ID, False)
            'run creation step
            UR.AddUser(model.FB_ID, model.FB_Name, model.FB_UserName)
            Return RedirectToAction("", "Home")
        End Function

        Function LogOut() As ActionResult
            FormsAuthentication.SignOut()
            Return RedirectToAction("", "Home")
        End Function

        <HttpPost()>
        Function FBLogOut() As ActionResult
            FormsAuthentication.SignOut()
            Return RedirectToAction("", "Home")
        End Function

    End Class
End Namespace
