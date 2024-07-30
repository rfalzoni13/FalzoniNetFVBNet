Imports FalzoniNetFVBNet.Utils.Helpers
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.Cookies
Imports Owin

<Assembly: OwinStartup(GetType(Startup))>
Public Module Startup
    Public Sub Configuration(app As IAppBuilder)
        app.UseCookieAuthentication(New CookieAuthenticationOptions With
        {
            .AuthenticationType = "ApplicationCookie",
            .LoginPath = New PathString("/Account/Login")
        })

        UrlConfigurationHelper.SetUrlList()
    End Sub
End Module
