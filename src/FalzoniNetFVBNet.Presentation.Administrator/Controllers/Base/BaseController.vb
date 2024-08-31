Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports FalzoniNetFVBNet.Presentation.Administrator.Attributes
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Configuration
Imports NLog

Namespace Controllers.Base
    Public Class BaseController(Of TModel As {Class, New})
        Inherits Controller

        Private ReadOnly _baseClient As IBaseClient(Of TModel)
        Protected ReadOnly _logger As ILogger

        Public Sub New(baseClient As IBaseClient(Of TModel))
            _logger = LogManager.GetCurrentClassLogger()
            _baseClient = baseClient
        End Sub

        ' GET: Base
        Public Function Index() As ActionResult
            Return View()
        End Function

        'GET: Create
        <UserConfiguration>
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

        ' POST: Create
        <UserConfiguration>
        <HttpPost>
        Public Function Create(model As TModel) As ActionResult

            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Try

                Dim result As String = _baseClient.Add(model)

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

        ' GET :Edit
        <UserConfiguration>
        <HttpGet>
        Public Async Function Edit(id As String) As Task(Of ActionResult)
            Try

                Dim model = Await _baseClient.GetAsync(id)

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

        ' POST: Edit
        <UserConfiguration>
        <HttpPost>
        Public Function Edit(model As TModel) As ActionResult

            If Not ModelState.IsValid Then
                Return View(model)
            End If

            Try

                Dim result As String = _baseClient.Update(model)

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

        'POST Delete
        <HttpPost>
        Public Async Function Delete(model As TModel) As Task(Of ActionResult)

            'List<string> errorsList = New List<string>()

            Try

                Dim result As String = Await _baseClient.DeleteAsync(model)

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