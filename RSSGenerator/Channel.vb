Namespace Template

    Public Class Channel : Implements IRSSCreate

        Public Sub New(ByRef _title As String, ByRef _link As String, ByRef Desc As String)
            title = HTMLSafe(_title)
            link = _link
            description = Desc
        End Sub

        'Required
        Private Property title As String
        Private Property link As String
        Private Property description As String

        'Optional
        Public Property language As String
        Public Property copyright As String
        Public Property managingEditor As String
        Public Property webMaster As String
        Public Property pubDate As New DateTime
        Public Property lastBuildDate As New DateTime
        Public Property category As String
        Private ReadOnly Property generator
            Get
                Return "RPGCor RSS"
            End Get
        End Property
        Private ReadOnly Property docs As String
            Get
                Return "http://www.rssboard.org/rss-specification"
            End Get
        End Property
        Public Property cloud As Template.Cloud
        Public Property ttl As Short
        Public Property image As Template.Image
        Public Property rating As String
        Public Property textInput As Template.textInput
        Public Property skipHours As Template.SkipHours
        Public Property skipDays As Template.SkipDays

        Public Property Items As New List(Of Template.ChannelItem)

        Public Function Make() As RSSResult
            Return New RSSResult(Me)
        End Function

        Friend Function Create() As String Implements IRSSCreate.Create
            Dim SB As New Text.StringBuilder
            SB.AppendLine("<?xml version=""1.0"" encoding=""UTF-8"" ?>")
            SB.AppendLine("<rss version=""2.0"" xmlns:cf=""http://www.microsoft.com/schemas/rss/core/2005"">")
            SB.AppendLine(vbTab & "<channel>")

            If title IsNot Nothing Then SB.AppendLine(XMLLine("title", title))
            If link IsNot Nothing Then SB.AppendLine(XMLLine("link", link))
            If description IsNot Nothing Then SB.AppendLine(XMLLine("description", description))
            If language IsNot Nothing Then SB.AppendLine(XMLLine("language", language))
            If copyright IsNot Nothing Then SB.AppendLine(XMLLine("copyright", copyright))
            If managingEditor IsNot Nothing Then SB.AppendLine(XMLLine("managingEditor", managingEditor))
            If webMaster IsNot Nothing Then SB.AppendLine(XMLLine("webMaster", webMaster))
            If pubDate > New Date(1900, 1, 1) Then SB.AppendLine(XMLLine("pubDate", pubDate.ToUniversalTime.ToString("ddd, d MMM yyyy HH:mm:ss") & " GMT"))
            If lastBuildDate > New Date(1900, 1, 1) Then SB.AppendLine(XMLLine("lastBuildDate", lastBuildDate.ToUniversalTime.ToString("ddd, d MMM yyyy HH:mm:ss") & " GMT"))
            If category IsNot Nothing Then SB.AppendLine(XMLLine("category", category))
            If generator IsNot Nothing Then SB.AppendLine(XMLLine("generator", generator))
            If docs IsNot Nothing Then SB.AppendLine(XMLLine("docs", docs))
            If cloud IsNot Nothing Then SB.AppendLine(RSSFormat(cloud))
            If ttl > 0 Then SB.AppendLine(XMLLine("ttl", ttl))
            If image IsNot Nothing Then SB.AppendLine(RSSFormat(image))
            If rating IsNot Nothing Then SB.AppendLine(XMLLine("rating", rating))
            If textInput IsNot Nothing Then SB.AppendLine(RSSFormat(textInput))
            If skipHours IsNot Nothing Then SB.AppendLine(RSSFormat(skipHours))
            If skipDays IsNot Nothing Then SB.AppendLine(RSSFormat(skipDays))

            For Each itm In Items
                SB.AppendLine(itm.Create)
            Next

            SB.AppendLine(vbTab & "</channel>")
            SB.AppendLine("</rss>")
            Dim ret As String = SB.ToString
            Return SB.ToString
        End Function
    End Class

End Namespace
