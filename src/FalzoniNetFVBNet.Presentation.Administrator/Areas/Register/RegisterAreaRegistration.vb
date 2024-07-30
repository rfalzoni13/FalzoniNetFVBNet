Imports System.Web.Mvc

Namespace Areas.Register
    Public Class RegisterAreaRegistration
        Inherits AreaRegistration

        Public Overrides ReadOnly Property AreaName() As String
            Get
                Return "Register"
            End Get
        End Property

        Public Overrides Sub RegisterArea(ByVal context As AreaRegistrationContext)
            context.MapRoute(
                "Register_default",
                "Register/{controller}/{action}/{id}",
                New With {.action = "Index", .id = UrlParameter.Optional}
            )
        End Sub
    End Class
End Namespace