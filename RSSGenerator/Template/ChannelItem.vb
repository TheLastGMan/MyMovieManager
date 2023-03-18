Namespace Template

    Public Class ChannelItem : Implements IRSSCreate

        Public Sub New(ByRef _title As String, ByRef _link As String, ByRef _description As String)
            title = HTMLSafe(_title)
            link = _link
            description = _description
        End Sub

        Public Property title As String
        Public Property link As String
        Public Property description As String
        Public Property author As String
        Public Property category As Category
        Public Property comments As String
        Public Property enclosure As Enclosure
        Public Property guid As guid
        Public Property pubDate As DateTime
        Public Property source As Source

        Public Function Create() As String Implements IRSSCreate.Create
            Dim SB As New Text.StringBuilder()
            SB.AppendLine(vbTab & vbTab & "<item>")

            If title IsNot Nothing Then SB.AppendLine(XMLLine("title", title, 1))
            If link IsNot Nothing Then SB.AppendLine(XMLLine("link", link, 1))
            If description IsNot Nothing Then SB.AppendLine(XMLLine("description", description, 1))
            If author IsNot Nothing Then SB.AppendLine(XMLLine("author", author, 1))
            If category IsNot Nothing Then SB.AppendLine(RSSFormat(category))
            If comments IsNot Nothing Then SB.AppendLine(XMLLine("comments", comments, 1))
            If enclosure IsNot Nothing Then SB.AppendLine(RSSFormat(enclosure))
            If guid IsNot Nothing Then SB.AppendLine(RSSFormat(guid))
            If pubDate > New Date(1900, 1, 1) Then SB.AppendLine(XMLLine("pubDate", pubDate.ToUniversalTime.ToString("ddd, d MMM yyyy HH:mm:ss") & " GMT", 1))
            If source IsNot Nothing Then SB.AppendLine(RSSFormat(source))

            SB.AppendLine(vbTab & vbTab & "</item>")
            Return SB.ToString
        End Function

    End Class

End Namespace
