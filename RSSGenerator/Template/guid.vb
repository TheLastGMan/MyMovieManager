Namespace Template

    Public Class guid : Implements IRSSCreate

        Public Sub New(ByRef _guid As String, Optional ByVal _IsPermaLink As Boolean = False)
            guid = _guid
            IsPermaLink = _IsPermaLink
        End Sub

        Public Property guid As String
        Public Property IsPermaLink As Boolean

        Public Function Create() As String Implements IRSSCreate.Create
            Return vbTab & vbTab & vbTab & String.Format("<guid isPermaLink=""{0}"">{1}</guid>", IsPermaLink.ToString.ToLower, guid)
        End Function
    End Class

End Namespace

