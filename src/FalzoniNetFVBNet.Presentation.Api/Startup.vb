Imports System.Web.Http
Imports FalzoniNetFVBNet.Application.IdentityConfiguration
Imports FalzoniNetFVBNet.Infra.IoC
Imports Microsoft.Owin
Imports Microsoft.Owin.Cors
Imports Owin

<Assembly: OwinStartup(GetType(FalzoniNetFVBNet.Presentation.Api.Startup))>
Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        'Register configurations
        Dim config As HttpConfiguration = New HttpConfiguration()

        'GlobalConfiguration.Configure(WebApiConfig.Register)
        WebApiConfig.Register(config)

        'Injeção de dependência
        UnityConfig.Register(config)

        'Configure Owin            
        AppBuilderConfiguration.ConfigureAuth(app)
        app.UseCors(CorsOptions.AllowAll)
        AppBuilderConfiguration.ActivateAccessToken(app)

        app.UseWebApi(config)
        'AppBuilderConfiguration.ConfigureCors(app)
    End Sub
End Class
