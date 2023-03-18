Imports System.Web.Hosting
Imports System.Web

Public Class EmbeddedViewVirtualPathProvider : Inherits VirtualPathProvider

    Private ReadOnly _embeddedViews As EmbeddedViewTable

    Public Sub New(ByRef embeddedViews As EmbeddedViewTable)
        If embeddedViews Is Nothing Then
            Throw New ArgumentNullException("embeddedViews")
        End If
        _embeddedViews = embeddedViews
    End Sub

    Private Function IsEmbeddedView(ByRef virtualPath As String) As Boolean
        If String.IsNullOrEmpty(virtualPath) Then
            Return False
        End If

        Dim virtualPathAppRelative As String = VirtualPathUtility.ToAppRelative(virtualPath)
        If Not virtualPath.StartsWith("/Views/", StringComparison.InvariantCultureIgnoreCase) Then
            Return False
        End If
        Dim fullyQualifiedViewName = virtualPathAppRelative.Substring(virtualPathAppRelative.LastIndexOf("/") + 1, virtualPathAppRelative.Length - 1 - virtualPathAppRelative.LastIndexOf("/"))

        Return _embeddedViews.ContainsEmbeddedView(fullyQualifiedViewName)
    End Function

    Public Overrides Function FileExists(virtualPath As String) As Boolean
        Dim exists As Boolean = (IsEmbeddedView(virtualPath) Or Previous.FileExists(virtualPath))
        Return exists
    End Function

    Public Overrides Function GetFile(virtualPath As String) As System.Web.Hosting.VirtualFile
        If IsEmbeddedView(virtualPath) Then
            Dim virtualPathAppRelative As String = VirtualPathUtility.ToAppRelative(virtualPath)
            Dim FQVN = virtualPathAppRelative.Substring(virtualPathAppRelative.LastIndexOf("/") + 1, virtualPathAppRelative.Length - 1 - virtualPathAppRelative.LastIndexOf("/"))

            Dim embeddedViewMetadata = _embeddedViews.FindEmbeddedView(FQVN)
            Return New EmbeddedResourceVirtualFile(embeddedViewMetadata, virtualPath)
        End If

        Return Previous.GetFile(virtualPath)
    End Function

    Public Overrides Function GetCacheDependency(virtualPath As String, virtualPathDependencies As System.Collections.IEnumerable, utcStart As Date) As System.Web.Caching.CacheDependency
        If IsEmbeddedView(virtualPath) Then
            Return Nothing
        Else
            Return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart)
        End If
    End Function

End Class
