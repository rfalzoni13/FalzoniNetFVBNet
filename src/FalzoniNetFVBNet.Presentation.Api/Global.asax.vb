Imports System.Web.Http

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Protected Sub Application_Start()
        'Moved to Startup Class
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
    End Sub
End Class
