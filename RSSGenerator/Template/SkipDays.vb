Namespace Template

    Public Class SkipDays : Implements IRSSCreate

        Public Sub New(Optional ByVal skip_monday As Boolean = False, _
                        Optional ByVal skip_tuesday As Boolean = False, _
                        Optional ByVal skip_wednesday As Boolean = False, _
                        Optional ByVal skip_thursday As Boolean = False, _
                        Optional ByVal skip_friday As Boolean = False, _
                        Optional ByVal skip_saturday As Boolean = False, _
                        Optional ByVal skip_sunday As Boolean = False)
            SkipMonday = skip_monday
            SkipTuesday = skip_tuesday
            SkipWednesday = skip_wednesday
            SkipThursday = skip_thursday
            SkipFriday = skip_friday
            SkipSaturday = skip_saturday
            SkipSunday = skip_sunday
        End Sub

        Public Property SkipMonday As Boolean
        Public Property SkipTuesday As Boolean
        Public Property SkipWednesday As Boolean
        Public Property SkipThursday As Boolean
        Public Property SkipFriday As Boolean
        Public Property SkipSaturday As Boolean
        Public Property SkipSunday As Boolean

        Public Function Create() As String Implements IRSSCreate.Create
            Dim SB As New Text.StringBuilder

            SB.AppendLine(vbTab & vbTab & "<skipDays>")
            Dim attr As String = "day"
            If SkipMonday Then
                SB.AppendLine(XMLLine(attr, "Monday", 1))
            End If
            If SkipTuesday Then
                SB.AppendLine(XMLLine(attr, "Tuesday", 1))
            End If
            If SkipWednesday Then
                SB.AppendLine(XMLLine(attr, "Wednesday", 1))
            End If
            If SkipThursday Then
                SB.AppendLine(XMLLine(attr, "Thursday", 1))
            End If
            If SkipFriday Then
                SB.AppendLine(XMLLine(attr, "Friday", 1))
            End If
            If SkipSaturday Then
                SB.AppendLine(XMLLine(attr, "Saturday", 1))
            End If
            If SkipSunday Then
                SB.AppendLine(XMLLine(attr, "Sunday", 1))
            End If

            SB.AppendLine(vbTab & vbTab & "</skipDays>")
            Return SB.ToString
        End Function
    End Class

End Namespace
