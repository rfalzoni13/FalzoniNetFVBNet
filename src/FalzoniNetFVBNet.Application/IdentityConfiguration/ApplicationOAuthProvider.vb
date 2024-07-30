Imports System.Data.SqlClient
Imports System.Security.Claims
Imports System.Security.Principal
Imports System.Web
Imports FalzoniNetFVBNet.Infra.Data.Identity
Imports FalzoniNetFVBNet.Utils.Helpers
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OAuth

Namespace IdentityConfiguration
    Public Class ApplicationOAuthProvider
        Inherits OAuthAuthorizationServerProvider

        Private ReadOnly _publicClientId As String

        Public Sub New(publicClientId As String)
            If publicClientId Is Nothing Then
                Throw New ArgumentNullException("publicClientId")
            End If

            _publicClientId = publicClientId
        End Sub

        Public Overrides Async Function GrantResourceOwnerCredentials(context As OAuthGrantResourceOwnerCredentialsContext) As Task
            Try
                Dim signInManager = HttpContext.Current.GetOwinContext().Get(Of ApplicationSignInManager)()
                Dim userManager = HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)()

                'Dim email = New EmailAddressAttribute()

                Dim user As ApplicationUser = Await userManager.FindByEmailAsync(context.UserName)
                'If user Is Nothing Then
                '    user = Await userManager.FindByNameAsync(context.UserName)
                'End If

                Dim login As Boolean = Await userManager.CheckPasswordAsync(user, context.Password)

                If Not login Then
                    context.SetError("invalid_grant", "Usuário ou senha incorretos!")
                    Return
                End If

                'Dim twoFactorEnabled As Boolean = Await userManager.GetTwoFactorEnabledAsync(user.Id)
                'If twoFactorEnabled Then
                '    Dim code = Await userManager.GenerateTwoFactorTokenAsync(user.Id, "PhoneCode")
                '    Dim notificationResult As IdentityResult = Await userManager.NotifyTwoFactorTokenAsync(user.Id, "PhoneCode", code)
                '    If Not notificationResult.Succeeded Then
                '        'you can add your own validation here
                '        context.SetError("invalid_grant", "Failed to send OTP")
                '    End If
                'End If

                Dim oAuthIdentity As ClaimsIdentity = Await user.GenerateUserIdentityAsync(userManager,
                   OAuthDefaults.AuthenticationType)
                Dim cookiesIdentity As ClaimsIdentity = Await user.GenerateUserIdentityAsync(userManager,
                    CookieAuthenticationDefaults.AuthenticationType)

                Dim properties As AuthenticationProperties = CreateProperties(user)
                Dim ticket As AuthenticationTicket = New AuthenticationTicket(oAuthIdentity, properties)
                context.Validated(ticket)
                context.Request.Context.Authentication.SignIn(cookiesIdentity)
            Catch e As SqlException
                context.SetError("Conection error: ", ExceptionHelper.CatchMessageFromException(e))
            Catch e As Exception
                context.SetError("Authentication error: " + ExceptionHelper.CatchMessageFromException(e))
            End Try
        End Function

        Public Overrides Function TokenEndpoint(context As OAuthTokenEndpointContext) As Task
            For Each [property] As KeyValuePair(Of String, String) In context.Properties.Dictionary
                context.AdditionalResponseParameters.Add([property].Key, [property].Value)
            Next

            Return Task.FromResult(Of Object)(Nothing)
        End Function

        Public Overrides Function ValidateClientAuthentication(context As OAuthValidateClientAuthenticationContext) As Task
            context.Validated()
            Return Task.FromResult(Of Object)(Nothing)
        End Function

        Public Overrides Function ValidateClientRedirectUri(context As OAuthValidateClientRedirectUriContext) As Task
            If context.ClientId = _publicClientId Then
                Dim expectedRootUri As Uri = New Uri(baseUri:=context.Request.Uri, relativeUri:="/")
                If expectedRootUri.AbsoluteUri = context.RedirectUri Then
                    context.Validated()
                End If
            End If

            Return Task.FromResult(Of Object)(Nothing)
        End Function

        Public Overrides Function GrantRefreshToken(context As OAuthGrantRefreshTokenContext) As Task
            Dim identity = New ClaimsIdentity(context.Ticket.Identity)

            Dim newTicket = New AuthenticationTicket(identity, context.Ticket.Properties)

            context.Validated(newTicket)

            Return Task.FromResult(Of Object)(Nothing)
        End Function

        Public Shared Async Function ExternalLogin(context As IOwinContext, user As IPrincipal, provider As String) As Task
            Dim userManager = HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)()

            Dim externalLoginData As ExternalLoginData = ExternalLoginData.FromIdentity(CType(user.Identity, ClaimsIdentity))

            If externalLoginData Is Nothing Then
                Throw New Exception("Login não encontrado!")
            End If

            If externalLoginData.LoginProvider <> provider Then
                Logout(context, DefaultAuthenticationTypes.ExternalCookie)
                Throw New ApplicationException("Usuário não cadastrado com provedor de login")
            End If


            Dim applicationUser As ApplicationUser = Await userManager.FindAsync(New UserLoginInfo(externalLoginData.LoginProvider, externalLoginData.ProviderKey))

            Dim hasRegistered As Boolean = applicationUser IsNot Nothing

            If hasRegistered Then
                Logout(context, DefaultAuthenticationTypes.ExternalCookie)

                Dim oAuthIdentity As ClaimsIdentity = Await applicationUser.GenerateUserIdentityAsync(userManager,
                   OAuthDefaults.AuthenticationType)
                Dim cookieIdentity As ClaimsIdentity = Await applicationUser.GenerateUserIdentityAsync(userManager,
                    CookieAuthenticationDefaults.AuthenticationType)

                Dim properties As AuthenticationProperties = ApplicationOAuthProvider.CreateProperties(applicationUser)
                context.Authentication.SignIn(properties, oAuthIdentity, cookieIdentity)
            Else
                Dim claims As IEnumerable(Of Claim) = externalLoginData.GetClaims()
                Dim ClaimIdentity As ClaimsIdentity = New ClaimsIdentity(claims, OAuthDefaults.AuthenticationType)
                context.Authentication.SignIn(ClaimIdentity)
            End If
        End Function

        Public Shared Async Function RegisterExternal(context As IOwinContext, userName As String, email As String) As Task(Of IdentityResult)
            Dim userManager = HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)()

            Dim info = Await context.Authentication.GetExternalLoginInfoAsync()
            If info Is Nothing Then
                Throw New Exception("Informações não encontradas!")
            End If

            Dim user = New ApplicationUser() With {.UserName = userName, .Email = email}

            Dim result As IdentityResult = Await userManager.CreateAsync(user)
            If Not result.Succeeded Then
                Return result
            End If

            result = Await userManager.AddLoginAsync(user.Id, info.Login)

            Return result
        End Function

        Public Shared Sub Logout(context As IOwinContext, authenticationType As String)
            Dim authentication = context.Authentication

            authentication.SignOut(authenticationType)
        End Sub

        Public Shared Function GetExternalAuthenticationTypes(context As IOwinContext) As IEnumerable(Of AuthenticationDescription)
            Return context.Authentication.GetExternalAuthenticationTypes()
        End Function

        Public Shared Function CreateProperties(user As ApplicationUser)
            Dim data As IDictionary(Of String, String) = New Dictionary(Of String, String) From
            {
                {"userId", user.Id},
                {"roleId", user.Roles.Select(Function(x) x.RoleId).FirstOrDefault()}
            }

            Return New AuthenticationProperties(data)
        End Function
    End Class
End Namespace
