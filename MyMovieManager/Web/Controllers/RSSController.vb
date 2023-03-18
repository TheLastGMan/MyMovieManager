Namespace RPGCor.MMM.Web
    Public Class RSSController
        Inherits System.Web.Mvc.Controller

        Private UR As New Core.Repository.User
        Private RSS As New Core.Repository.RSS

        Function CacheCount() As ActionResult
            Return View("StrXml", DirectCast(RSS.CacheCount(), Object))
        End Function

        Function RTDetail() As ActionResult
            Return View("StrXml", DirectCast(RSS.RottenTomatoeDetail(), Object))
        End Function

        Function BoxOffice() As ActionResult
            Return View("StrXml", DirectCast(RSS.BoxOffice(), Object))
        End Function

        Function MyMovies(ByVal id As String) As ActionResult
            Dim userid As New Long

            If Long.TryParse(id, New Long) Then
                'fbid
                userid = Long.Parse(id)
            Else
                'username
                userid = UR.GetUserId_by_Username(id)
            End If

            If userid > 0 Then
                'success
                Return View("StrXml", DirectCast(RSS.MyMovies(userid), Object))
            Else
                'failure
                Return View("StrXml", DirectCast("Invalid User", Object))
            End If

        End Function

    End Class
End Namespace
