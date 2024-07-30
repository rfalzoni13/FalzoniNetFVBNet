Imports System.Net
Imports System.Web.Mvc

Namespace Controllers
    Public Class ErrorController
        Inherits Controller

        ' GET: Error
        Function Index() As ActionResult
            Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError)

            Return View()
        End Function

        ' GET: Error/NotFound
        Function NotFound() As ActionResult
            Response.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound)

            Return View("NotFound")
        End Function
    End Class
End Namespace