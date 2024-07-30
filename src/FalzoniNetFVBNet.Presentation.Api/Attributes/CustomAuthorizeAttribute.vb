Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Formatting
Imports System.Web.Http
Imports System.Web.Http.Controllers
Imports FalzoniNetFVBNet.Presentation.Api.Models.Common

Namespace Attributes
    Public Class CustomAuthorizeAttribute
        Inherits AuthorizeAttribute

        Protected Overrides Sub HandleUnauthorizedRequest(actionContext As HttpActionContext)
            Dim stats As StatusCodeModel = New StatusCodeModel()
            stats.Status = HttpStatusCode.Unauthorized
            stats.Message = "Você não esta autorizado a acessar este conteúdo!"

            actionContext.Response = New HttpResponseMessage With
        {
            .StatusCode = HttpStatusCode.Unauthorized,
            .Content = New ObjectContent(stats.GetType(), stats, New JsonMediaTypeFormatter())
        }
        End Sub

    End Class
End Namespace
