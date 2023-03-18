Namespace Template

    Public Class Image : Implements IRSSCreate

        Public Sub New(ByRef _url As String, ByRef _title As String, ByRef _link As String)
            url = _url
            title = _title
            link = _link
        End Sub

        Public Property url As String
        Public Property title As String
        Public Property link As String

        Public Property description As String
        Private Property _width As Short = 88
        Public Property width As Short
            Get
                Return _width
            End Get
            Set(value As Short)
                If value < 1 Then
                    _width = 1
                ElseIf value > 144 Then
                    _width = 144
                Else
                    _width = value
                End If
            End Set
        End Property
        Private Property _height As Short = 31
        Public Property height As Short
            Get
                Return _height
            End Get
            Set(value As Short)
                If value < 1 Then
                    _height = 1
                ElseIf value > 400 Then
                    _height = 400
                Else
                    _height = value
                End If
            End Set
        End Property

        Public Function Create() As String Implements IRSSCreate.Create
            Dim SB As New Text.StringBuilder()
            SB.AppendLine(vbTab & vbTab & "<image>")
            SB.AppendLine(XMLLine("title", title, 1))
            SB.AppendLine(XMLLine("link", link, 1))
            SB.AppendLine(XMLLine("url", url, 1))
            SB.AppendLine(vbTab & vbTab & "</image>")
            Return SB.ToString
        End Function
    End Class

End Namespace
