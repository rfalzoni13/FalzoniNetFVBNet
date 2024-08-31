Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Stock
Imports FalzoniNetFVBNet.Presentation.Administrator.Controllers.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Stock
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Stock

Namespace Areas.Stock.Controllers
    Public Class ProductController
        Inherits BaseController(Of ProductModel)

        Private ReadOnly _productClient As IProductClient

        Public Sub New(productClient As IProductClient)
            MyBase.New(productClient)
            _productClient = productClient
        End Sub

        'GET Stock/Product/LoadTable
        <HttpGet>
        Public Async Function LoadTable() As Task(Of JsonResult)

            Dim table = New ProductTableModel()

            Try

                table = Await _productClient.GetTableAsync()

            Catch ex As Exception
                _logger.Fatal("Ocorreu um erro: " + ex.ToString())
            End Try

            Return Json(table, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace