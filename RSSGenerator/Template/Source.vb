Namespace Template

    Public Class Source : Implements IRSSCreate

        Public Property url As String
        Public Property name As String

        Public Function Create() As String Implements IRSSCreate.Create
            Return vbTab & vbTab & String.Format("<source url=""{0}"">{1}</source>", url, name)
        End Function

    End Class

End Namespace
