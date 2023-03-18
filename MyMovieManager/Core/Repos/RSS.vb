Imports RSSGenerator
Imports System.Web

Namespace Repository

    Public Class RSS

        Public Function CacheCount() As String
            Dim RSS As Template.Channel = GenHeader("Cache Counts", "http://mmm.rpgcor.com", "My Movie Manager")

            RSS.Items.Add(New Template.ChannelItem("RottenTomatoe Cache", "http://mmm.rpgcor.com/RSS/RTDetail", "Count: " & MovieSearch.MovieCache.Count) With {.guid = New Template.guid(Guid.NewGuid.ToString), .pubDate = RSS.pubDate})
            RSS.Items.Add(New Template.ChannelItem("NetFlix Cache", "http://mmm.rpgcor.com", "Count: " & NetFlixSearch.NFCache.Count) With {.guid = New Template.guid(Guid.NewGuid.ToString), .pubDate = RSS.pubDate})
            Return RSS.Make().RSSFeed
        End Function

        Public Function RottenTomatoeDetail() As String
            Dim RSS As Template.Channel = GenHeader("Rotten Tomato Detail", "http://mmm.rpgcor.com", "My Movie Manager")

            For Each movie In MovieSearch.MovieCache
                Dim itm As New Template.ChannelItem(movie.Key, "http://mmm.rpgcor.com/s/" & movie.Key, "Found: " & movie.Value.Movies.Count)
                itm.pubDate = movie.Value.Created
                itm.guid = New Template.guid(itm.link, True)
                RSS.Items.Add(itm)
            Next

            Return RSS.Make().RSSFeed
        End Function

        Public Function BoxOffice() As String
            Dim RSS As Template.Channel = GenHeader("Box Office Movies", "http://mmm.rpgcor.com", "My Movie Manager")
            RSS.ttl = 720

            For Each movie As Model.BoxOffice In New MovieSearch().BoxOffice
                Dim SB As New Text.StringBuilder()
                SB.AppendLine(DualCol("Released : ", movie.Released))
                SB.AppendLine(DualCol("Rating : ", movie.Rating))
                SB.AppendLine(DualCol("Run Time : ", movie.RunTime))
                SB.AppendLine(DualCol("Staring : ", String.Join("<br/>Staring : ", movie.Cast.ToArray)))

                Dim item As New Template.ChannelItem(movie.Title, movie.Url, HttpUtility.HtmlEncode(SB.ToString))
                item.guid = New Template.guid(Guid.NewGuid.ToString)
                item.pubDate = movie.Released
                RSS.Items.Add(item)
            Next

            Return RSS.Make().RSSFeed
        End Function

        Public Function MyMovies(ByRef uid As Long) As String
            Dim user As Data.User = New User().GetUser_by_Id(uid)
            Dim RSS As Template.Channel = GenHeader(user.UserName & "'s Movie List", "http://mmm.rpgcor.com/ui/" & uid, "My Movie Manager")

            For Each movie As Data.UserMovie In New UserMovie().GetAllUsersMovies(uid)
                Dim SB As New Text.StringBuilder()
                SB.AppendLine("<div style=""float:left; border-right:1px solid black;""><img src=""" & movie.poster_url & """ /></div>")
                SB.AppendLine(DualCol("Released : ", movie.Year))
                SB.AppendLine(DualCol("Rating : ", movie.mpaa))
                SB.AppendLine(DualCol("Added : ", movie.Added.ToString))
                SB.AppendLine(DualCol("Position : ", movie.position))

                Dim url As String = "http://www.imdb.com/title/tt" & movie.IMDB_Id.ToString.PadLeft(7, "0")
                Dim item As New Template.ChannelItem(movie.Name, url, HttpUtility.HtmlEncode(SB.ToString))
                item.pubDate = movie.Added
                item.guid = New Template.guid(url, True)
                RSS.Items.Add(item)
            Next

            Return RSS.Make.RSSFeed
        End Function

        Private Function Span(ByRef content As String) As String
            Return String.Format("<span style=""font-size:16px; padding-left:5px;"">{0}</span>", content)
        End Function

        Private Function DualCol(ByRef col1 As String, ByRef col2 As String) As String
            Return "<div>" & col1 & " " & col2 & "</div>"
        End Function

        Private Function GenHeader(ByRef title As String, ByRef link As String, ByRef desc As String) As Template.Channel
            Dim RSS As New RSSGenerator.Template.Channel(title, "http://mmm.rpgcor.com", desc)
            RSS.pubDate = Now
            RSS.lastBuildDate = RSS.pubDate
            RSS.copyright = Now.Year
            RSS.ttl = 15
            Return RSS
        End Function

    End Class

End Namespace
