Namespace RPGCor.MMM.Web
    Public Class RSSController
        Inherits System.Web.Mvc.Controller

        Private RSS As New Core.Repository.RSS

        Function CacheCount() As ActionResult
            Return View("StrXml", RSS.CacheCount())
        End Function

        Function RTDetail() As ActionResult
            Return View("StrXml", RSS.RottenTomatoeDetail())
        End Function

    End Class
End Namespace
