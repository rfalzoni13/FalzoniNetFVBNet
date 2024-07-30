Imports System.ComponentModel.DataAnnotations
Imports Microsoft.AspNet.Identity.EntityFramework

Namespace Identity
    Public Class ApplicationRole
        Inherits IdentityRole

        <Required>
        Public Property Created As Date

        Public Property Modified As Date?
    End Class
End Namespace
