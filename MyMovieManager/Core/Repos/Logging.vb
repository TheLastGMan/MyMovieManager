Namespace Repository

    Public Class Logging

        Private DBC As New Data.Context

        Protected ReadOnly Property Logs As List(Of Data.Logs)
            Get
                Return DBC.Logs.ToList
            End Get
        End Property

        Public Function AddMessage(ByVal Application As String, ByVal Message As String) As Boolean
            Try
                DBC.Logs.Add(New Data.Logs With {.Application = Application, .Message = Message, .Occured = Now})
                DBC.SaveChanges()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Protected Function DeleteOldLogs(ByVal [date] As DateTime) As Boolean
            Try
                For Each log In Logs.Where(Function(f) f.Occured < [date]).ToList
                    DBC.Logs.Remove(log)
                Next
                DBC.SaveChanges()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Class

End Namespace
