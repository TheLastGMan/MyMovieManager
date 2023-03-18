Imports System.Threading.Tasks

Namespace Web

    Public Class MovieAsyncController
        Inherits System.Web.Mvc.AsyncController

        <HttpPost()>
        <Authorize()>
        Public Sub NetFlixStreamAsync(ByVal movie As String, Optional year As Integer = 0)
            AsyncManager.OutstandingOperations.Increment()
            Task.Factory.StartNew(Sub()
                                      Dim result As Core.Model.NetFlixMovie = New Core.Repository.NetFlixSearch().Search(movie, year)
                                      AsyncManager.Parameters("model") = result
                                      AsyncManager.OutstandingOperations.Decrement()
                                  End Sub)
        End Sub

        Public Function NetFlixStreamCompleted(ByVal model As Core.Model.NetFlixMovie) As ViewResult
            Return View("NetFlixIcon", New NetFlixMovieModel With {.result = model})
        End Function

    End Class


End Namespace
