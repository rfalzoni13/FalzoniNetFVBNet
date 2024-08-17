Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports FalzoniNetFVBNet.Application.ServiceApplication.Register
Imports FalzoniNetFVBNet.Presentation.Api.Attributes
Imports FalzoniNetFVBNet.Presentation.Api.Models.Register
Imports FalzoniNetFVBNet.Presentation.Api.Utils
Imports NLog

Namespace Controllers.Admin.Register
    <CustomAuthorize(Roles:="Administrator")>
    <RoutePrefix("Api/Customer")>
    Public Class CustomerController
        Inherits ApiController
#Region "Attributes"
        Private Shared ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()
        Private ReadOnly _customerServiceApplication As CustomerServiceApplication
#End Region

#Region "Constructor"
        Public Sub New(customerServiceApplication As CustomerServiceApplication)
            _customerServiceApplication = customerServiceApplication
        End Sub
#End Region

#Region "Getters"
        ' GET Api/Customer/GetAll
        ''' <summary>
        ''' Listar todos os clientes
        ''' </summary>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Listagem de todos os clientes</remarks>
        ''' <returns></returns>
        <HttpGet>
        <Route("GetAll")>
        Public Function GetAll() As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                Dim retorno = _customerServiceApplication.GetAll()

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, retorno)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' GET Api/Customer/Get?id={Id}
        ''' <summary>
        ''' Listar cliente pelo Id
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Retorna o cliente através do Id do mesmo</remarks>
        ''' <param name="Id">Id do cliente</param>
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

                Dim customer = _customerServiceApplication.Get(Id)

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, customer)
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function
#End Region

#Region "Add Customer"
        ' POST Api/Customer/Add
        ''' <summary>
        ''' Inserir cliente
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Insere um novo cliente passando um objeto no body da requisição no método POST</remarks>
        ''' <param name="model">Objeto de registro cliente</param>
        ''' <returns></returns>
        <HttpPost>
        <Route("Add")>
        Public Function Add(<FromBody> model As CustomerModel) As HttpResponseMessage
            Dim action = Me.ActionContext.ActionDescriptor.ActionName
            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim customerDTO = model.ConvertToDTO()

                    _customerServiceApplication.Add(customerDTO)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.Created, "Cliente incluído com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        '' POST: Api/Customer/AddAsync
        '''' <summary>
        '''' Inserir cliente modo assíncrono
        '''' </summary>
        '''' <response code="400">Bad Request</response>
        '''' <response code="401">Unauthorized</response>
        '''' <response code="500">Internal Server Error</response>
        '''' <remarks>Insere um novo cliente passando um objeto no body da requisição no método POST de forma assíncrona</remarks>
        '''' <param name="model">Objeto de registro cliente</param>
        '''' <returns></returns>
        '<HttpPost>
        '<Route("Add")>
        'Public Async Function AddAsync(<FromBody> model As CustomerModel) As Task(Of HttpResponseMessage)
        '    Dim action = Me.ActionContext.ActionDescriptor.ActionName
        '    Try
        '        If ModelState.IsValid Then
        '            _logger.Info(action + " - Iniciado")

        '            Dim customerDTO = model.ConvertToDTO()

        '            Await _customerServiceApplication.AddAsync(customerDTO)

        '            _logger.Info(action + " - Sucesso!")

        '            _logger.Info(action + " - Finalizado")

        '            Return Request.CreateResponse(HttpStatusCode.Created, "Cliente incluído com sucesso!")
        '        Else
        '            Throw New ApplicationException("Por favor, preencha os campos corretamente!")
        '        End If
        '    Catch ex As ApplicationException
        '        Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
        '    Catch ex As Exception
        '        Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
        '    End Try
        'End Function
#End Region

#Region "Update Customer"
        'PUT Api/Customer/Update
        ''' <summary>
        ''' Atualizar cliente
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Atualiza o cliente passando o objeto no body da requisição pelo método PUT</remarks>
        ''' <param name="model">Objeto de registro do cliente</param>
        ''' <returns></returns>
        <HttpPut>
        <Route("Update")>
        Public Function Update(<FromBody> model As CustomerModel) As HttpResponseMessage
            Dim action = Me.ActionContext.ActionDescriptor.ActionName
            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim customerDTO = model.ConvertToDTO()

                    _customerServiceApplication.Update(customerDTO)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.Created, "Cliente atualizado com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ''PUT: Api/Customer/UpdateAsync
        '''' <summary>
        '''' Atualizar cliente modo assíncrono
        '''' </summary>
        '''' <response code="400">Bad Request</response>
        '''' <response code="401">Unauthorized</response>
        '''' <response code="500">Internal Server Error</response>
        '''' <remarks>Atualiza o cliente passando o objeto no body da requisição pelo método PUT de forma assíncrona</remarks>
        '''' <param name="model">Objeto de registro do cliente</param>
        '<HttpPut>
        '<Route("UpdateAsync")>
        'Public Async Function UpdateAsync(<FromBody> model As CustomerModel) As Task(Of HttpResponseMessage)
        '    Dim action = Me.ActionContext.ActionDescriptor.ActionName
        '    Try
        '        If ModelState.IsValid Then
        '            _logger.Info(action + " - Iniciado")

        '            Dim customerDTO = model.ConvertToDTO()

        '            Await _customerServiceApplication.UpdateAsync(customerDTO)

        '            _logger.Info(action + " - Sucesso!")

        '            _logger.Info(action + " - Finalizado")

        '            Return Request.CreateResponse(HttpStatusCode.Created, "Cliente atualizado com sucesso!")
        '        Else
        '            Throw New ApplicationException("Por favor, preencha os campos corretamente!")
        '        End If
        '    Catch ex As ApplicationException
        '        Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
        '    Catch ex As Exception
        '        Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
        '    End Try
        'End Function
#End Region

#Region "Delete Customer"
        ' DELETE Api/Customer/Delete
        ''' <summary>
        ''' Excluir cliente
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Exclui o cliente passando o objeto no body da requisição pelo método DELETE</remarks>
        ''' <param name="model">Objeto de registro do cliente</param>
        ''' <returns></returns>
        <HttpDelete>
        <Route("Delete")>
        Public Function Delete(<FromBody> model As CustomerModel) As HttpResponseMessage
            Dim action = Me.ActionContext.ActionDescriptor.ActionName
            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim customerDTO = model.ConvertToDTO()

                    _customerServiceApplication.Delete(customerDTO)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.Created, "Cliente excluído com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        '''' <summary>
        '''' Excluir cliente assíncrono
        '''' </summary>
        '''' <response code="400">Bad Request</response>
        '''' <response code="401">Unauthorized</response>
        '''' <response code="500">Internal Server Error</response>
        '''' <remarks>Exclui o cliente passando o objeto no body da requisição pelo método DELETE de forma assíncrona</remarks>
        '''' <param name="model">Objeto de registro do cliente</param>
        '''' <returns></returns>
        '' DELETE: Api/Customer/DeleteAsync
        '<HttpDelete>
        '<Route("Delete")>
        'Public Async Function DeleteAsync(<FromBody> model As CustomerModel) As Task(Of HttpResponseMessage)
        '    Dim action = Me.ActionContext.ActionDescriptor.ActionName
        '    Try
        '        If ModelState.IsValid Then
        '            _logger.Info(action + " - Iniciado")

        '            Dim customerDTO = model.ConvertToDTO()

        '            Await _customerServiceApplication.DeleteAsync(customerDTO)

        '            _logger.Info(action + " - Sucesso!")

        '            _logger.Info(action + " - Finalizado")

        '            Return Request.CreateResponse(HttpStatusCode.Created, "Cliente excluído com sucesso!")
        '        Else
        '            Throw New ApplicationException("Por favor, preencha os campos corretamente!")
        '        End If
        '    Catch ex As ApplicationException
        '        Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
        '    Catch ex As Exception
        '        Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
        '    End Try
        'End Function
#End Region
    End Class
End Namespace