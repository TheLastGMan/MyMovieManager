Imports System.Data.Entity

Public Class Context : Inherits DbContext

    Public Property Users As DbSet(Of Data.User)
    Public Property UsersMovies As DbSet(Of Data.UserMovie)
    Public Property Logs As DbSet(Of Data.Logs)

End Class
