Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports FalzoniNetFVBNet.Presentation.Administrator.Attributes
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Configuration
Imports FalzoniNetFVBNet.Utils.Helpers
Imports NLog

Namespace Areas.Configuration.Controllers
    <Authorize>
    Public Class UserController
        Inherits Controller

        Private ReadOnly _userClient As IUserClient
        Private Shared ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()

        Public Sub New(userClient As IUserClient)
            _userClient = userClient
        End Sub

        ' GET Register/User
        Public Function Index() As ActionResult
            Return View()
        End Function

        'GET Register/User/LoadTable
        <HttpGet>
        Public Async Function LoadTable() As Task(Of JsonResult)

            Dim table = New UserTableModel()

            Try

                table = Await _userClient.GetTableAsync(UrlConfigurationHelper.UserGetAll)

            Catch ex As Exception
                _logger.Fatal("Ocorreu um erro: " + ex.ToString())
            End Try

            Return Json(table, JsonRequestBehavior.AllowGet)
        End Function

        'GET Register/User/Create
        <UserRegister>
        <HttpGet>
        Public Function Create() As ActionResult

            Dim model = New UserModel()

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

        ' POST Register/User/Create
        <UserRegister>
        <HttpPost>
        Public Function Create(model As UserModel) As ActionResult

            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Try

                Dim result As String = _userClient.Add(UrlConfigurationHelper.UserCreate, model)

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

        ' GET Register/User/Edit
        <UserRegister>
        <HttpGet>
        Public Async Function Edit(id As String) As Task(Of ActionResult)
            Try

                Dim model = Await _userClient.GetAsync(UrlConfigurationHelper.UserGet, id)

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

        ' POST Register/User/Edit
        <UserRegister>
        <HttpPost>
        Public Function Edit(model As UserModel) As ActionResult

            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Try

                Dim result As String = _userClient.Update(UrlConfigurationHelper.UserEdit, model)

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

        'POST Register/User/Delete
        <HttpPost>
        Public Async Function Delete(model As UserModel) As Task(Of ActionResult)

            'List<string> errorsList = New List<string>()

            Try

                Dim result As String = Await _userClient.DeleteAsync(UrlConfigurationHelper.UserDelete, model)

                Return Json(New With {.success = True, .message = result})
            Catch ex As ApplicationException

                _logger.Error("Ocorreu um erro: " + ex.ToString())
                Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest)

                'errorsList.Add(ex.Message)

                'Return Json(New With{.success = False, .errors = errorsList})
                Return Json(New With {.success = False, .error = ex.Message})
            Catch ex As Exception
                _logger.Error("Ocorreu um erro: " + ex.ToString())

                'errorsList.Add(ex.Message)

                If Debugger.IsAttached Then
                    'errorsList.Add("Ocorreu um erro, verifique o arquivo de log e tente novamente!")
                    Return Json(New With {.success = False, .error = "Ocorreu um erro, verifique o arquivo de log e tente novamente!"})
                End If

                'return Json(New With { .success = False, .errors = errorsList })
                Return Json(New With {.success = False, .error = ex.Message})
            End Try
        End Function
    End Class
End Namespace