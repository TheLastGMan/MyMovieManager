Imports System.Web.Hosting
Imports System.Reflection

Public Class EmbeddedResourceVirtualFile
    Inherits VirtualFile

    Private ReadOnly _embeddedViewMetadata As EmbeddedViewMetadata

    Public Sub New(ByRef embeddedViewMetadata As EmbeddedViewMetadata, ByRef virtualPath As String)
        MyBase.New(virtualPath)

        If embeddedViewMetadata Is Nothing Then
            Throw New ArgumentNullException("embeddedViewMetadata")
        End If
        _embeddedViewMetadata = embeddedViewMetadata
    End Sub

    Public Overrides Function Open() As System.IO.Stream
        Dim assembly As Assembly = GetResourceAssembly()
        If assembly Is Nothing Then
            Return Nothing
        Else
            Dim stream As IO.Stream = assembly.GetManifestResourceStream(_embeddedViewMetadata.Name)
            Return stream
        End If
    End Function

    Protected Function GetResourceAssembly() As Assembly
        Return AppDomain.CurrentDomain.GetAssemblies().Where(Function(f) String.Equals(f.FullName, _embeddedViewMetadata.AssemblyFullName, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()
    End Function

End Class
