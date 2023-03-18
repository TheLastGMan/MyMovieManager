Imports System
Imports System.Web.Mvc
Imports System.Runtime.CompilerServices

Public Module PagingHelpers

    <Extension()>
    Public Function PageLinks(helper As HtmlHelper, ByVal currentpage As Integer, ByVal maxpages As Integer, pageurl As Func(Of Integer, String)) As MvcHtmlString
        Dim result As New StringBuilder()

        If currentpage > 5 Then
            Dim tagba As New TagBuilder("a")
            'back all
            tagba.InnerHtml = "<<"
            tagba.MergeAttribute("href", pageurl(1))
            result.Append(tagba.ToString)

            'back single
            tagba = New TagBuilder("a")
            tagba.InnerHtml = "<"
            tagba.MergeAttribute("href", pageurl(currentpage - 1))
            result.Append(tagba.ToString)
        End If

        For i As Integer = IIf(currentpage - 5 < 1, 1, currentpage - 5) To IIf(currentpage + 5 > maxpages, maxpages, currentpage + 5)
            Dim tag As New TagBuilder("a")
            tag.MergeAttribute("href", pageurl(i))
            tag.InnerHtml = i.ToString
            If (i = currentpage) Then
                tag.AddCssClass("selected")
            Else
                tag.AddCssClass("not-selected")
            End If
            result.Append(tag.ToString)
        Next

        If currentpage < (maxpages - 5) Then
            Dim tagba As New TagBuilder("a")

            'forward single
            tagba.InnerHtml = ">"
            tagba.MergeAttribute("href", pageurl(currentpage + 1))
            result.Append(tagba.ToString)

            'forward all
            tagba = New TagBuilder("a")
            tagba.InnerHtml = ">>"
            tagba.MergeAttribute("href", pageurl(maxpages))
            result.Append(tagba.ToString)
        End If

        Return MvcHtmlString.Create(result.ToString)
    End Function

End Module
