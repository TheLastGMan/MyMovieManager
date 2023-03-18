Public Class Repository

    Private ReadOnly BaseUrl As String = "http://odata.netflix.com/Catalog/"

    Public Function Search(ByVal movie As String, Optional ByRef year As Integer = 0) As NetFlixMovie
        Dim REQ As New Text.StringBuilder
        REQ.Append(BaseUrl)
        'REQ.Append(String.Format("Titles?$filter=Name eq '{0}'", movie))
        REQ.Append(String.Format("Titles?$filter=endswith(Name, '{0}')", movie))
        If year > 0 Then
            REQ.Append(String.Format(" and ReleaseYear eq {0}", year))
        End If
        REQ.Append("&$format=json")
        Return RunRequest(Of NetFlixMovie)(REQ.ToString)
    End Function

    Private Function RunRequest(Of T)(ByRef url As String) As T
        Try
            Dim wr As Net.WebRequest = Net.WebRequest.Create(New Uri(url))
            wr.Timeout = 5000
            Dim resp = wr.GetResponse()
            Dim str As New IO.StreamReader(wr.GetResponse.GetResponseStream())
            Dim response As String = str.ReadToEnd

            'clean up
            str.Dispose()

            Return Newtonsoft.Json.JsonConvert.DeserializeObject(Of T)(response)
        Catch ex As Exception
            Dim err As String = ex.Message
            'Throw New Exception("External Movie Search API Returned: " & ex.Message)
        End Try
    End Function

End Class
