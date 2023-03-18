Namespace Template

    Public Class textInput : Implements IRSSCreate

        Public Sub New(ByRef _title As String, ByRef _desc As String, ByRef _name As String, ByRef _link As String)
            title = _title
            description = _desc
            name = _name
            link = _link
        End Sub

        Public Property title As String
        Public Property description As String
        Public Property name As String
        Public Property link As String

        Public Function Create() As String Implements IRSSCreate.Create
            Dim SB As New Text.StringBuilder()
            SB.AppendLine(vbTab & vbTab & "<textInput>")
            SB.AppendLine(XMLLine("title", title, 1))
            SB.AppendLine(XMLLine("description", description, 1))
            SB.AppendLine(XMLLine("name", name, 1))
            SB.AppendLine(XMLLine("link", link, 1))
            SB.AppendLine(vbTab & vbTab & "</textInput>")
            Return SB.ToString
        End Function
    End Class

End Namespace
