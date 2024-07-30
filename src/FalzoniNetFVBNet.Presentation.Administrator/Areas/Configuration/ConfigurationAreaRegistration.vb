Imports System.Web.Mvc

Namespace Areas.Configuration
    Public Class ConfigurationAreaRegistration
        Inherits AreaRegistration

        Public Overrides ReadOnly Property AreaName() As String
            Get
                Return "Configuration"
            End Get
        End Property

        Public Overrides Sub RegisterArea(ByVal context As AreaRegistrationContext)
            context.MapRoute(
                "Configuration_default",
                "Configuration/{controller}/{action}/{id}",
                New With {.action = "Index", .id = UrlParameter.Optional}
            )
        End Sub
    End Class
End Namespace