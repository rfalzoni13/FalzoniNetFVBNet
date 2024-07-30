Imports FalzoniNetFVBNet.Infra.Data.Identity
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security

Namespace IdentityConfiguration
    Public Class ApplicationSignInManager
        Inherits SignInManager(Of ApplicationUser, String)

        Public Sub New(userManager As UserManager(Of ApplicationUser, String), authenticationManager As IAuthenticationManager)
            MyBase.New(userManager, authenticationManager)
        End Sub

        Public Shared Function Create([option] As IdentityFactoryOptions(Of ApplicationSignInManager), context As IOwinContext) As ApplicationSignInManager
            Dim manager = context.GetUserManager(Of ApplicationUserManager)()

            Dim sign = New ApplicationSignInManager(manager, context.Authentication)

            Return sign
        End Function
    End Class
End Namespace
