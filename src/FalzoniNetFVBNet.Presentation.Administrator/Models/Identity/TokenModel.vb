Imports Newtonsoft.Json

Namespace Models.Identity
    Public Class TokenModel

        <JsonProperty("access_token")>
        Public Property AccessToken As String

        <JsonProperty("refresh_token")>
        Public Property RefreshToken As String

        <JsonProperty(".expires")>
        Public Property Expire As Date?

        <JsonProperty(".issued")>
        Public Property Issue As Date?

        <JsonProperty("userId")>
        Public Property UserId As String

        <JsonProperty("roleId")>
        Public Property RoleId As String
    End Class
End Namespace
