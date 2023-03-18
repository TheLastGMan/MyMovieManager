Public Class RSSResult

    Private _RSSFeed As String = ""
    Public ReadOnly Property RSSFeed As String
        Get
            Return _RSSFeed
        End Get
    End Property

    Friend Sub New(ByRef RSS As Template.Channel)
        _RSSFeed = RSS.Create()
    End Sub

End Class
