Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports FalzoniNetFVBNet.Application.ServiceApplication.Configuration
Imports FalzoniNetFVBNet.Presentation.Api.Attributes
Imports FalzoniNetFVBNet.Presentation.Api.Models.Identity
Imports FalzoniNetFVBNet.Presentation.Api.Utils
Imports NLog

Namespace Controllers.Admin.Configuration
    <CustomAuthorize(Roles:="Administrator")>
    <RoutePrefix("Api/User")>
    Public Class UserController
        Inherits ApiController
#Region "Attributes"
        Private Shared ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()
        Private ReadOnly _userServiceApplication As UserServiceApplication
#End Region

#Region "Constructor"
        Public Sub New(userServiceApplication As UserServiceApplication)
            _userServiceApplication = userServiceApplication
        End Sub
#End Region

#Region "Getters"
        ' GET Api/User/GetAll
        ''' <summary>
        ''' Listar todos os usuarios
        ''' </summary>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Listagem de todos os usuarios</remarks>
        ''' <returns></returns>
        <HttpGet>
        <Route("GetAll")>
        Public Function GetAll() As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                Dim retorno = _userServiceApplication.GetAll()

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, retorno)

            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' GET Api/User/Get?id={Id}
        ''' <summary>
        ''' Listar usuário pelo Id
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Retorna o usuário através do Id do mesmo</remarks>
        ''' <param name="Id">Id do usuário</param>
        ''' <returns></returns>
        <HttpGet>
        <Route("Get")>
        Public Function [Get](Id As Guid) As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                If Guid.Equals(Id, Guid.Empty) Then
                    Throw New ApplicationException("Parâmetro inválido")
                End If

                Dim user = _userServiceApplication.Get(Id)

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, user)
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function
#End Region

#Region "Add User"
        ' POST Api/User/Add
        ''' <summary>
        ''' Inserir usuário
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Insere um novo usuário passando um objeto no body da requisição no método POST</remarks>
        ''' <param name="applicationUserRegisterModel">Objeto de registro usuário</param>
        ''' <returns></returns>
        <HttpPost>
        <Route("Add")>
        Public Function Add(<FromBody> applicationUserRegisterModel As ApplicationUserRegisterModel) As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim userDto = applicationUserRegisterModel.ConvertToDTO()

                    _userServiceApplication.Add(userDto)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.Created, "Usuário incluído com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' POST Api/User/AddAsync
        ''' <summary>
        ''' Inserir usuário modo assíncrono
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Insere um novo usuário passando um objeto no body da requisição no método POST de forma assíncrona</remarks>
        ''' <param name="applicationUserRegisterModel">Objeto de registro usuário</param>
        ''' <returns></returns>
        <HttpPost>
        <Route("AddAsync")>
        Public Async Function AddAsync(<FromBody> applicationUserRegisterModel As ApplicationUserRegisterModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim userDto = applicationUserRegisterModel.ConvertToDTO()

                    Await _userServiceApplication.AddAsync(userDto)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.Created, "Usuário incluído com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function
#End Region

#Region "Update User"
        ' PUT Api/User/Update
        ''' <summary>
        ''' Atualizar usuário
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Atualiza o usuário passando o objeto no body da requisição pelo método PUT</remarks>
        ''' <param name="applicationUserRegisterModel">Objeto de registro do usuário</param>
        ''' <returns></returns>
        <HttpPut>
        <Route("Update")>
        Public Function Update(<FromBody> applicationUserRegisterModel As ApplicationUserRegisterModel) As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim userDto = applicationUserRegisterModel.ConvertToDTO()

                    _userServiceApplication.Update(userDto)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.OK, "Usuário atualizado com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' PUT Api/User/UpdateAsync
        ''' <summary>
        ''' Atualizar usuário modo assíncrono
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Atualiza o usuário passando o objeto no body da requisição pelo método PUT de forma assíncrona</remarks>
        ''' <param name="applicationUserRegisterModel">Objeto de registro do usuário</param>
        <HttpPut>
        <Route("UpdateAsync")>
        Public Async Function UpdateAsync(<FromBody> applicationUserRegisterModel As ApplicationUserRegisterModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim userDto = applicationUserRegisterModel.ConvertToDTO()

                    Await _userServiceApplication.UpdateAsync(userDto)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.OK, "Usuário atualizado com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function
#End Region

#Region "Delete User"
        ' DELETE Api/User/Delete
        ''' <summary>
        ''' Excluir usuario
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Exclui o usuario passando o objeto no body da requisição pelo método DELETE</remarks>
        ''' <param name="applicationUserRegisterModel">Objeto de registro do usuario</param>
        ''' <returns></returns>
        <HttpDelete>
        <Route("Delete")>
        Public Function Delete(<FromBody> applicationUserRegisterModel As ApplicationUserRegisterModel) As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim userDto = applicationUserRegisterModel.ConvertToDTO()

                    _userServiceApplication.Delete(userDto)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.OK, "Usuário excluído com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' DELETE Api/User/DeleteAsync
        ''' <summary>
        ''' Excluir usuario assíncrono
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Exclui o usuario passando o objeto no body da requisição pelo método DELETE de forma assíncrona</remarks>
        ''' <param name="applicationUserRegisterModel">Objeto de registro do usuario</param>
        ''' <returns></returns>
        <HttpDelete>
        <Route("DeleteAsync")>
        Public Async Function DeleteAsync(<FromBody> applicationUserRegisterModel As ApplicationUserRegisterModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim userDto = applicationUserRegisterModel.ConvertToDTO()

                    Await _userServiceApplication.DeleteAsync(userDto)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.OK, "Usuário excluído com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try

        End Function
#End Region

    End Class
End Namespace