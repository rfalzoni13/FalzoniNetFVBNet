Imports System.Net
Imports System.Web.Mvc

Namespace Controllers
    Public Class ErrorController
        Inherits Controller

        ' GET: Error
        Function Index() As ActionResult
            Return View()
        End Function

        ' GET: Error/NotFound
        Function NotFound() As ActionResult
            Return View("NotFound")
        End Function
    End Class
End Namespace