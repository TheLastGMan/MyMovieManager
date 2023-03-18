Namespace Template

    Public Class Cloud : Implements IRSSCreate

        Public Sub New(ByRef _domain As String, _
                       ByRef _port As Short, _
                       ByRef _path As String, _
                       ByRef _registerProc As String, _
                       ByRef _protocol As String)
            domain = _domain
            port = _port
            path = _path
            registerProcedure = _registerProc
            protocol = _protocol
        End Sub

        Public Property domain As String
        Public Property port As Short = 80
        Public Property path As String
        Public Property registerProcedure As String
        Public Property protocol As String

        Public Function Create() As String Implements IRSSCreate.Create
            Return vbTab & vbTab & String.Format("<cloud domain=""{0}"" port=""{1}"" path=""{2}"" registerProcedure=""{3}"" protocol=""{4}"" />", _
                                 domain, port, path, registerProcedure, protocol)
        End Function
    End Class

End Namespace
