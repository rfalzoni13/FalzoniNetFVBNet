Namespace Models.Identity
    Public Class RegisterModel
        Public Property ID As String
        Public Property Name As String
        Public Property Email As String
        Public Property UserName As String
        Public Property Password As String
        Public Property Gender As String
        Public Property DateBirth As Date
        Public Property RoleId As Guid
        Public Property PhotoPath As String
        Public Property AcceptTerms As Boolean
    End Class
End Namespace