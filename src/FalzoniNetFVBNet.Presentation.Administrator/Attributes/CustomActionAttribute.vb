Imports System.Web.Mvc
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Identity
Imports FalzoniNetFVBNet.Utils.Helpers
Imports Microsoft.Owin

Namespace Attributes
    Public Class CustomActionAttribute
        Inherits ActionFilterAttribute
        Private ReadOnly _accountClient As AccountClient

        Public Sub New(accountClient As AccountClient)
            _accountClient = accountClient
        End Sub

        Public Overrides Async Sub OnActionExecuting(filterContext As ActionExecutingContext)
            If Not GetOwinContext().Authentication.User.Identity.IsAuthenticated Then
                Return
            End If

            Dim dateLimit As Date = Convert.ToDateTime(RequestHelper.GetTokenExpire())

            If dateLimit >= Date.Now Then
                Await _accountClient.RefreshToken()
            End If

            MyBase.OnActionExecuting(filterContext)
        End Sub

        Private Function GetOwinContext() As IOwinContext
            Return HttpContext.Current.GetOwinContext()
        End Function
    End Class
End Namespace
