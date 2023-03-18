Module Common

    Public Function XMLLine(ByRef attribute As String, ByRef value As String, Optional ByVal numtabs As Integer = 0)
        If value IsNot Nothing AndAlso value.Length > 0 Then
            Dim rt As String = vbTab & vbTab
            For i As Integer = 1 To numtabs
                rt &= vbTab
            Next
            Return rt & String.Format("<{0}>{1}</{0}>", attribute, value)
        Else
            Return ""
        End If
    End Function

    Public Function HTMLSafe(ByRef line As String) As String
        Return line.Replace("&", "&amp;")
    End Function

    Public Function RSSFormat(ByRef obj As Object) As String
        Dim lobj As IRSSCreate = TryCast(obj, IRSSCreate)
        If lobj IsNot Nothing Then
            Return lobj.Create
        Else
            Return ""
        End If
    End Function

End Module
