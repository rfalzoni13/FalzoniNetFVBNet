Imports System.ComponentModel.DataAnnotations
Imports System.Security.Claims
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework

Namespace Identity
    Public Class ApplicationUser
        Inherits IdentityUser

        <Required()>
        <MaxLength(256)>
        Public Property FullName As String

        <Required>
        <MaxLength(100)>
        Public Property Gender As String

        <MaxLength(1024)>
        Public Property PhotoPath As String

        <Required>
        Public Property Active As Boolean

        <Required>
        Public Property DateBirth As Date

        <Required>
        Public Property Created As Date

        Public Property Modified As Date?

        Public Async Function GenerateUserIdentityAsync(manager As UserManager(Of ApplicationUser), authenticationType As String) As Task(Of ClaimsIdentity)
            'authenticationType deve corresponder a um definido em CookieAuthenticationOptions.AuthenticationType
            Dim userIdentity = Await manager.CreateIdentityAsync(Me, authenticationType)

            'Adicione declarações de usuários aqui
            userIdentity.AddClaim(New Claim(ClaimTypes.Name, userIdentity.GetUserName()))
            userIdentity.AddClaim(New Claim(ClaimTypes.NameIdentifier, userIdentity.GetUserId()))

            Dim roles = manager.GetRoles(userIdentity.GetUserId()).ToList()

            If roles IsNot Nothing And roles.Count > 0 Then
                For Each role In roles
                    userIdentity.AddClaim(New Claim(ClaimTypes.Role, role))
                Next
            End If
            Return userIdentity
        End Function
    End Class
End Namespace
