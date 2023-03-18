Namespace Template

    Public Class Category : Implements IRSSCreate

        Public Sub New(ByRef _desc As String, Optional ByRef _domain As String = "")

        End Sub

        Public Property description As String
        Public Property domain As String

        Public Function Create() As String Implements IRSSCreate.Create
            Dim ld As String = IIf(domain.Length > 0, " domain=""" & domain & """", "")
            Return vbTab & vbTab & String.Format("<category{0}>{1}</category>", ld, description)
        End Function
    End Class

End Namespace
