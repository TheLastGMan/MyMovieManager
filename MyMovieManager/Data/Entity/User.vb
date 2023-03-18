<DataAnnotations.Table("FB_UserProfile")>
Public Class User

    <Key()>
    <DatabaseGenerated(DatabaseGeneratedOption.None)>
    Public Property FB_Id As Long
    <Required()>
    <MaxLength(128)>
    Public Property Name As String
    <MaxLength(256)>
    Public Property UserName As String
    <Required()>
    Public Property Created As Date = Now

End Class
