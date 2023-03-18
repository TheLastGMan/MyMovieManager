<DataAnnotations.Table("Logs")>
Public Class Logs

    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    <Key()>
    <Timestamp()>
    Public Property Id As Byte()
    <Required()>
    <MaxLength(64)>
    Public Property Application As String
    <Required()>
    <MaxLength(1024)>
    Public Property Message As String
    <Required()>
    Public Property Occured As DateTime

End Class
