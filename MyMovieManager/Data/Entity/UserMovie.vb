<Table("MMM_UserMovie")>
Public Class UserMovie

    <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
    <Key()>
    <Timestamp()>
    Public Property GUID As Byte()
    <Required()>
    Public Property FB_Id As Long
    <Required()>
    Public Property IMDB_Id As Integer
    <Required()>
    <MaxLength(512)>
    Public Property Name As String
    <Required()>
    Public Property Year As Integer
    <StringLength(8)>
    Public Property RunTime As String
    <MaxLength(8)>
    Public Property mpaa As String
    <MaxLength(2048)>
    Public Property poster_url As String
    Public Property position As Integer = 0
    <Required()>
    Public Property Added As Date

End Class
