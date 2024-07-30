Imports FalzoniNetFVBNet.Infra.Data.Context
Imports Microsoft.AspNet.Identity
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Microsoft.Owin.Security.OAuth
Imports Owin

Namespace IdentityConfiguration
    Public Class AppBuilderConfiguration
        Public Shared Property OAuthOptions As OAuthAuthorizationServerOptions

        Public Shared Property PublicClientId As String

        Public Shared Sub ConfigureAuth(app As IAppBuilder)
            app.CreatePerOwinContext(AddressOf FalzoniContext.Create)
            app.CreatePerOwinContext(Of ApplicationUserManager)(AddressOf ApplicationUserManager.Create)
            app.CreatePerOwinContext(Of ApplicationSignInManager)(AddressOf ApplicationSignInManager.Create)
            app.CreatePerOwinContext(Of ApplicationRoleManager)(AddressOf ApplicationRoleManager.Create)
        End Sub

        'Public Shared Sub ConfigureCookieToken(app As IAppBuilder)
        '    app.UseCookieAuthentication(New CookieAuthenticationOptions With
        '    {
        '        .AuthenticationType = "ApplicationCookie",
        '        .LoginPath = New PathString("/Account/Login")
        '    })
        'End Sub

        Public Shared Sub ActivateAccessToken(app As IAppBuilder)
            PublicClientId = "self"
            OAuthOptions = New OAuthAuthorizationServerOptions With
            {
                .AllowInsecureHttp = True, 'true for development only
                .TokenEndpointPath = New PathString("/Api/Account/Login"),
                .AuthorizeEndpointPath = New PathString("/Api/Account/ExternalLogin"),
                .AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                .Provider = New ApplicationOAuthProvider(PublicClientId),
                .RefreshTokenProvider = New AccessRefreshTokenProvider()
            }

            app.UseCookieAuthentication(New CookieAuthenticationOptions())
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie)

            'app.UseOAuthAuthorizationServer(OAuthOptions)

            app.UseOAuthBearerTokens(OAuthOptions)

            'app.UseOAuthBearerAuthentication(New OAuthBearerAuthenticationOptions())
            'app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType)

            'Remova comentários das linhas a seguir para habilitar o login com provedores de login de terceiros
            'app.UseMicrosoftAccountAuthentication(
            '    clientId:="",
            '    clientSecret:="")

            'app.UseTwitterAuthentication(
            '    consumerKey:="",
            '    consumerSecret:="")

            'app.UseFacebookAuthentication(
            '    appId:="1699681303742860",
            '    appSecret:="1e2d0379d4f20856d12097b8399f1130")

            'app.UseGoogleAuthentication(New GoogleOAuth2AuthenticationOptions() With
            '{
            '    .ClientId = "",
            '    .ClientSecret = ""
            '})
        End Sub
    End Class
End Namespace
