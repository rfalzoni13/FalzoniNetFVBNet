Imports System.Security.Claims
Imports System.Web

Namespace Helpers
    Public Class RequestHelper
        Public Shared Function GetAccessToken() As String
            Return HttpContext.Current.GetOwinContext().Authentication.User.Claims.
            FirstOrDefault(Function(x) x.Type.Contains("AccessToken"))?.Value
        End Function


        Public Shared Function GetRefreshToken() As String
            Return HttpContext.Current.GetOwinContext().Authentication.User.Claims.
                FirstOrDefault(Function(x) x.Type.Contains("RefreshToken"))?.Value
        End Function




        Public Shared Function GetTokenExpire() As String
            Return HttpContext.Current.GetOwinContext().Authentication.User.Claims.
                FirstOrDefault(Function(x) x.Type = ClaimTypes.Expired)?.Value
        End Function

        Public Shared Function GetApplicationPath() As String
            Return If(Debugger.IsAttached, System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath,
            System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        End Function
    End Class
End Namespace
