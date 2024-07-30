Imports System.Web.Http
Imports FalzoniNetFVBNet.Utils.Helpers
Imports Microsoft.Owin.Security.OAuth
Imports Newtonsoft.Json

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        'Serviços e configuração da API da Web
        ConfigurationHelper.ProviderName = ConfigurationManager.AppSettings("ProviderName")

        'Configuração e serviços de API Web
        'Configure a API Web para usar somente a autenticação de token de portador.
        config.SuppressDefaultHostAuthentication()
        'config.Filters.Add(New CustomAuthorizeAttribute())
        config.Filters.Add(New HostAuthenticationFilter(OAuthDefaults.AuthenticationType))

        ' Rotas de Web API
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )

        'Redirecionar automaticamente para o Swagger
        config.Routes.MapHttpRoute(
                name:="Swagger",
                routeTemplate:=String.Empty,
                defaults:=Nothing,
                constraints:=Nothing,
                handler:=New Swashbuckle.Application.RedirectHandler((Function(message) message.RequestUri.ToString()), "swagger")
            )

        config.Formatters.Remove(config.Formatters.XmlFormatter)

        config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None
        config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented
        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(New System.Net.Http.Headers.MediaTypeHeaderValue("multipart/form-data"))
    End Sub
End Module
