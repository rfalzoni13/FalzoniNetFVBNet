Imports System.Web.Mvc

Namespace Controllers
    <Authorize>
    Public Class HomeController
        Inherits Controller

        ' GET: Home
        Function Index() As ActionResult
            Return View()
        End Function
    End Class
End Namespace