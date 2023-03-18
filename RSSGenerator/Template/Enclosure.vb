Namespace Template

    Public Class Enclosure : Implements IRSSCreate

        Public Sub New(ByRef _url As String, ByRef _length As Integer, ByRef _type As String)
            url = _url
            length = _length
            type = _type
        End Sub

        Public Property url As String
        Public Property length As Integer
        Public Property type As String

        Public Function Create() As String Implements IRSSCreate.Create
            Return vbTab & vbTab & String.Format("<enclosure url=""{0}"" length=""{1}"" type=""{2}"" />", url, length, type)
        End Function
    End Class

End Namespace
