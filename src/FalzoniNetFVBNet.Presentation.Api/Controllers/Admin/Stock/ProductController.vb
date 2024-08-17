Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports FalzoniNetFVBNet.Application.ServiceApplication.Register
Imports FalzoniNetFVBNet.Application.ServiceApplication.Stock
Imports FalzoniNetFVBNet.Presentation.Api.Attributes
Imports FalzoniNetFVBNet.Presentation.Api.Models.Stock
Imports FalzoniNetFVBNet.Presentation.Api.Utils
Imports NLog

Namespace Controllers.Admin.Stock
    <CustomAuthorize(Roles:="Administrator")>
    <RoutePrefix("Api/Product")>
    Public Class ProductController
        Inherits ApiController
#Region "Attributes"
        Private Shared ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()
        Private ReadOnly _productServiceApplication As ProductServiceApplication
#End Region

#Region "Constructor"
        Public Sub New(productServiceApplication As ProductServiceApplication)
            _productServiceApplication = productServiceApplication
        End Sub
#End Region

#Region "Getters"
        ' GET Api/Product/GetAll
        ''' <summary>
        ''' Listar todos os produtos
        ''' </summary>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Listagem de todos os produtos</remarks>
        ''' <returns></returns>
        <HttpGet>
        <Route("GetAll")>
        Public Function GetAll() As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                Dim retorno = _productServiceApplication.GetAll()

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, retorno)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' GET Api/Product/Get?id={Id}
        ''' <summary>
        ''' Listar produto pelo Id
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Retorna o produto através do Id do mesmo</remarks>
        ''' <param name="Id">Id do produto</param>
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

                Dim product = _productServiceApplication.Get(Id)

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, product)
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function
#End Region

#Region "Add Product"
        ' POST Api/Product/Add
        ''' <summary>
        ''' Inserir produto
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Insere um novo produto passando um objeto no body da requisição no método POST</remarks>
        ''' <param name="model">Objeto de registro produto</param>
        ''' <returns></returns>
        <HttpPost>
        <Route("Add")>
        Public Function Add(<FromBody> model As ProductModel) As HttpResponseMessage
            Dim action = Me.ActionContext.ActionDescriptor.ActionName
            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim productDTO = model.ConvertToDTO()

                    _productServiceApplication.Add(productDTO)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.Created, "Produto incluído com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        '' POST: Api/Product/AddAsync
        '''' <summary>
        '''' Inserir produto modo assíncrono
        '''' </summary>
        '''' <response code="400">Bad Request</response>
        '''' <response code="401">Unauthorized</response>
        '''' <response code="500">Internal Server Error</response>
        '''' <remarks>Insere um novo produto passando um objeto no body da requisição no método POST de forma assíncrona</remarks>
        '''' <param name="model">Objeto de registro produto</param>
        '''' <returns></returns>
        '<HttpPost>
        '<Route("Add")>
        'Public Async Function AddAsync(<FromBody> model As ProductModel) As Task(Of HttpResponseMessage)
        '    Dim action = Me.ActionContext.ActionDescriptor.ActionName
        '    Try
        '        If ModelState.IsValid Then
        '            _logger.Info(action + " - Iniciado")

        '            Dim productDTO = model.ConvertToDTO()

        '            Await _productServiceApplication.AddAsync(productDTO)

        '            _logger.Info(action + " - Sucesso!")

        '            _logger.Info(action + " - Finalizado")

        '            Return Request.CreateResponse(HttpStatusCode.Created, "Produto incluído com sucesso!")
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

#Region "Update Product"
        'PUT Api/Product/Update
        ''' <summary>
        ''' Atualizar produto
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Atualiza o produto passando o objeto no body da requisição pelo método PUT</remarks>
        ''' <param name="model">Objeto de registro do produto</param>
        ''' <returns></returns>
        <HttpPut>
        <Route("Update")>
        Public Function Update(<FromBody> model As ProductModel) As HttpResponseMessage
            Dim action = Me.ActionContext.ActionDescriptor.ActionName
            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim productDTO = model.ConvertToDTO()

                    _productServiceApplication.Update(productDTO)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.Created, "Produto atualizado com sucesso!")
                Else
                    Throw New ApplicationException("Por favor, preencha os campos corretamente!")
                End If
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ''PUT: Api/Product/UpdateAsync
        '''' <summary>
        '''' Atualizar produto modo assíncrono
        '''' </summary>
        '''' <response code="400">Bad Request</response>
        '''' <response code="401">Unauthorized</response>
        '''' <response code="500">Internal Server Error</response>
        '''' <remarks>Atualiza o produto passando o objeto no body da requisição pelo método PUT de forma assíncrona</remarks>
        '''' <param name="model">Objeto de registro do produto</param>
        '<HttpPut>
        '<Route("UpdateAsync")>
        'Public Async Function UpdateAsync(<FromBody> model As ProductModel) As Task(Of HttpResponseMessage)
        '    Dim action = Me.ActionContext.ActionDescriptor.ActionName
        '    Try
        '        If ModelState.IsValid Then
        '            _logger.Info(action + " - Iniciado")

        '            Dim productDTO = model.ConvertToDTO()

        '            Await _productServiceApplication.UpdateAsync(productDTO)

        '            _logger.Info(action + " - Sucesso!")

        '            _logger.Info(action + " - Finalizado")

        '            Return Request.CreateResponse(HttpStatusCode.Created, "Produto atualizado com sucesso!")
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

#Region "Delete Product"
        ' DELETE Api/Product/Delete
        ''' <summary>
        ''' Excluir produto
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Exclui o produto passando o objeto no body da requisição pelo método DELETE</remarks>
        ''' <param name="model">Objeto de registro do produto</param>
        ''' <returns></returns>
        <HttpDelete>
        <Route("Delete")>
        Public Function Delete(<FromBody> model As ProductModel) As HttpResponseMessage
            Dim action = Me.ActionContext.ActionDescriptor.ActionName
            Try
                If ModelState.IsValid Then
                    _logger.Info(action + " - Iniciado")

                    Dim productDTO = model.ConvertToDTO()

                    _productServiceApplication.Delete(productDTO)

                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")

                    Return Request.CreateResponse(HttpStatusCode.Created, "Produto excluído com sucesso!")
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
        '''' Excluir produto assíncrono
        '''' </summary>
        '''' <response code="400">Bad Request</response>
        '''' <response code="401">Unauthorized</response>
        '''' <response code="500">Internal Server Error</response>
        '''' <remarks>Exclui o produto passando o objeto no body da requisição pelo método DELETE de forma assíncrona</remarks>
        '''' <param name="model">Objeto de registro do produto</param>
        '''' <returns></returns>
        '' DELETE: Api/Product/DeleteAsync
        '<HttpDelete>
        '<Route("Delete")>
        'Public Async Function DeleteAsync(<FromBody> model As ProductModel) As Task(Of HttpResponseMessage)
        '    Dim action = Me.ActionContext.ActionDescriptor.ActionName
        '    Try
        '        If ModelState.IsValid Then
        '            _logger.Info(action + " - Iniciado")

        '            Dim productDTO = model.ConvertToDTO()

        '            Await _productServiceApplication.DeleteAsync(productDTO)

        '            _logger.Info(action + " - Sucesso!")

        '            _logger.Info(action + " - Finalizado")

        '            Return Request.CreateResponse(HttpStatusCode.Created, "Produto excluído com sucesso!")
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