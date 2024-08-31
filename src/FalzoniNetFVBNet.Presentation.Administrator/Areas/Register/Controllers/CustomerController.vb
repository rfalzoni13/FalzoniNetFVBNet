Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Register
Imports FalzoniNetFVBNet.Presentation.Administrator.Controllers.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Register
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Register
Imports NLog

Namespace Areas.Register.Controllers
    <Authorize>
    Public Class CustomerController
        Inherits BaseController(Of CustomerModel)

        Private ReadOnly _customerClient As ICustomerClient

        Public Sub New(customerClient As ICustomerClient)
            MyBase.New(customerClient)
            _customerClient = customerClient
        End Sub

        'GET Register/Customer/LoadTable
        <HttpGet>
        Public Async Function LoadTable() As Task(Of JsonResult)

            Dim table = New CustomerTableModel()

            Try

                table = Await _customerClient.GetTableAsync()

            Catch ex As Exception
                _logger.Fatal("Ocorreu um erro: " + ex.ToString())
            End Try

            Return Json(table, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace