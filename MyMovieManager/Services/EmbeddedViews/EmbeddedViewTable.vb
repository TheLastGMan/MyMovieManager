<Serializable()>
Public Class EmbeddedViewTable

    Private Shared ReadOnly _lock As Object = New Object()
    Private ReadOnly _viewCache As New Dictionary(Of String, EmbeddedViewMetadata)

    Public Sub AddView(ByRef viewName As String, ByRef assemblyName As String)
        SyncLock _lock
            _viewCache(viewName) = New EmbeddedViewMetadata With {.name = viewName, .AssemblyFullName = assemblyName}
        End SyncLock
    End Sub

    Public Function ContainsEmbeddedView(ByRef fullyQualifiedViewName) As Boolean
        Dim foundView = FindEmbeddedView(fullyQualifiedViewName)
        Return IIf(foundView IsNot Nothing, True, False)
    End Function

    Public Function FindEmbeddedView(ByRef fullyQualifedViewName) As EmbeddedViewMetadata
        Dim name As String = fullyQualifedViewName
        If String.IsNullOrEmpty(name) Then
            Return Nothing
        End If

        Return Views.Where(Function(f) f.Name.ToLowerInvariant.Equals(name.ToLowerInvariant)).SingleOrDefault
    End Function

    Public ReadOnly Property Views As List(Of EmbeddedViewMetadata)
        Get
            Return _viewCache.Values.ToList
        End Get
    End Property

End Class
