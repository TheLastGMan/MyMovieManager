Namespace Template

    Public Class SkipHours : Implements IRSSCreate

        Public Sub New(ByVal ParamArray _hours() As Byte)
            Hours = _hours.ToList
        End Sub
        Public Sub New(ByRef _hours As List(Of Byte))
            Hours = _hours
        End Sub

        Public Property Hours As List(Of Byte)

        Public Function Create() As String Implements IRSSCreate.Create
            Dim SB As New Text.StringBuilder

            SB.AppendLine(vbTab & vbTab & "<skipHours>")
            For Each hr In Hours
                If hr < 0 Then
                    hr = 0
                ElseIf hr > 23 Then
                    hr = 23
                End If
                SB.AppendLine(XMLLine("hour", hr, 1))
            Next
            SB.AppendLine(vbTab & vbTab & "</skipHours>")

            Return SB.ToString
        End Function

    End Class

End Namespace
