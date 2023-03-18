Imports System.Reflection

Public Class EmbeddedViewResolver

    Private _typeFinder As TypeFinder = New TypeFinder()

    Public Function GetEmbeddedViews() As EmbeddedViewTable
        Dim assemblies = _typeFinder.getAssemblies()
        If assemblies Is Nothing Or assemblies.Count = 0 Then
            Return Nothing
        End If

        Dim table = New EmbeddedViewTable()

        Dim allowed_namespaces As String() = {".views.", ".vbhtml", ".chtml"}

        For Each assembly In assemblies
            Dim names = GetNamesOfAssemblyResources(assembly)
            If (names Is Nothing Or names.Length = 0) Then Continue For

            For Each name In names
                Dim key = name.ToLowerInvariant()

                If (Not allowed_namespaces.Where(Function(f) key.Contains(f)).Count > 0) Then Continue For
                table.AddView(name, assembly.FullName)
            Next
        Next

        Return table
    End Function

    Private Function GetNamesOfAssemblyResources(ByRef assembly As Assembly) As String()
        Try
            Return assembly.GetManifestResourceNames()
        Catch ex As Exception
            Return New String() {}
        End Try
    End Function

End Class
