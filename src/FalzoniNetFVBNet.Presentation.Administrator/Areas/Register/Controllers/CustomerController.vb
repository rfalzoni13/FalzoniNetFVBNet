Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Register
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Register
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Register
Imports FalzoniNetFVBNet.Utils.Helpers
Imports NLog

Namespace Areas.Register.Controllers
    <Authorize>
    Public Class CustomerController
        Inherits Controller

        Private ReadOnly _customerClient As ICustomerClient
        Private Shared ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        Public Sub New(customerClient As ICustomerClient)
            _customerClient = customerClient
        End Sub

        ' GET Register/Customer
        Public Function Index() As ActionResult
            Return View()
        End Function

        'GET Register/Customer/LoadTable
        <HttpGet>
        Public Async Function LoadTable() As Task(Of JsonResult)

            Dim table = New CustomerTableModel()

            Try

                table = Await _customerClient.GetTableAsync(UrlConfigurationHelper.CustomerGetAll)

            Catch ex As Exception
                _logger.Fatal("Ocorreu um erro: " + ex.ToString())
            End Try

            Return Json(table, JsonRequestBehavior.AllowGet)
        End Function

        'GET Register/Customer/Create
        <HttpGet>
        Public Function Create() As ActionResult

            Dim model = New CustomerModel()

            Try

                Return View(model)

            Catch ex As ApplicationException
                _logger.Error("Ocorreu um erro: " + ex.ToString())

                TempData("Return") = New ReturnModel With
                {
                    .Type = "Error",
                    .Message = ex.Message
                }

                Return View("Index")

            Catch ex As Exception
                _logger.Fatal("Ocorreu um erro: " + ex.ToString())
                Throw
            End Try
        End Function

        ' POST Register/Customer/Create
        <HttpPost>
        Public Function Create(model As CustomerModel) As ActionResult

            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Try

                Dim result As String = _customerClient.Add(UrlConfigurationHelper.CustomerCreate, model)

                TempData("Return") = New ReturnModel With
                {
                    .Type = "Success",
                    .Message = result
                }

                Return View("Index")

            Catch ex As ApplicationException

                _logger.Error("Ocorreu um erro: " + ex.ToString())

                ModelState.AddModelError(String.Empty, ex.Message)

                Return View(model)

            Catch ex As Exception

                _logger.Error("Ocorreu um erro: " + ex.ToString())
                Throw
            End Try
        End Function

        ' GET Register/Customer/Edit
        <HttpGet>
        Public Async Function Edit(id As String) As Task(Of ActionResult)
            Try

                Dim model = Await _customerClient.GetAsync(UrlConfigurationHelper.CustomerGet, id)

                Return View(model)

            Catch ex As ApplicationException
                _logger.Error("Ocorreu um erro: " + ex.ToString())

                TempData("Return") = New ReturnModel With
                {
                    .Type = "Error",
                    .Message = ex.Message
                }

                Return View("Index")
            Catch ex As Exception

                _logger.Error("Ocorreu um erro: " + ex.ToString())
                Throw
            End Try
        End Function

        ' POST Register/Customer/Edit
        <HttpPost>
        Public Function Edit(model As CustomerModel) As ActionResult

            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Try

                Dim result As String = _customerClient.Update(UrlConfigurationHelper.CustomerEdit, model)

                TempData("Return") = New ReturnModel With
                {
                    .Type = "Success",
                    .Message = result
                }

                Return View("Index")

            Catch ex As ApplicationException

                _logger.Error("Ocorreu um erro: " + ex.ToString())

                ModelState.AddModelError(String.Empty, ex.Message)

                Return View(model)

            Catch ex As Exception

                _logger.Error("Ocorreu um erro: " + ex.ToString())
                Throw
            End Try
        End Function

        'POST Register/Customer/Delete
        <HttpPost>
        Public Async Function Delete(model As CustomerModel) As Task(Of ActionResult)

            'List<string> errorsList = New List<string>()

            Try

                Dim result As String = Await _customerClient.DeleteAsync(UrlConfigurationHelper.CustomerDelete, model)

                Return Json(New With {.success = True, .message = result})
            Catch ex As ApplicationException

                _logger.Error("Ocorreu um erro: " + ex.ToString())
                Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest)

                'errorsList.Add(ex.Message)

                'Return Json(New With {.success = False, .errors = errorsList})
                Return Json(New With {.success = False, .error = ex.Message})
            Catch ex As Exception
                _logger.Error("Ocorreu um erro: " + ex.ToString())

                'errorsList.Add(ex.Message)

                If Debugger.IsAttached Then
                    'errorsList.Add("Ocorreu um erro, verifique o arquivo de log e tente novamente!")
                    Return Json(New With {.success = False, .error = "Ocorreu um erro, verifique o arquivo de log e tente novamente!"})
                End If

                'return Json(New With{ .success = False, .errors = errorsList })
                Return Json(New With {.success = False, .error = ex.Message})
            End Try
        End Function
    End Class
End Namespace