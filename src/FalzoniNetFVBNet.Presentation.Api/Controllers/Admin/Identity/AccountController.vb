Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports FalzoniNetFVBNet.Application.ServiceApplication.Configuration
Imports FalzoniNetFVBNet.Presentation.Api.Utils
Imports NLog

Namespace Controllers.Admin.Identity
    <RoutePrefix("Api/Account")>
    Public Class AccountController
        Inherits ApiController
#Region "Attributes"
        Private Shared ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()
        Private ReadOnly _roleServiceApplication As RoleServiceApplication
#End Region

#Region "Constructor"
        Public Sub New(roleServiceApplication As RoleServiceApplication)
            _roleServiceApplication = roleServiceApplication
        End Sub
#End Region

#Region "Getters"
        ' GET Api/Role/GelAllNames
        ''' <summary>
        ''' Listar nomes de Acessos
        ''' </summary>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="404">Not Found</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Listagem de todos os acessos pelos nomes</remarks>
        ''' <returns></returns>
        <HttpGet>
        <Route("GelAllNames")>
        Public Function GelAllNames() As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                Dim retorno = _roleServiceApplication.GelAllNames()
                If retorno IsNot Nothing And retorno.Count() > 0 Then
                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")
                    Return Request.CreateResponse(HttpStatusCode.OK, retorno)
                Else
                    Throw New HttpResponseException(HttpStatusCode.NotFound)
                End If
            Catch ex As HttpResponseException
                If ex.Response.StatusCode = HttpStatusCode.NotFound Then
                    Return ResponseManager.ReturnExceptionNotFound(ex, Request, _logger, action, "Nenhum registro encontrado!")
                End If

                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' GET Api/Role/GetAll
        ''' <summary>
        ''' Listar todos os acessos
        ''' </summary>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="404">Not Found</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Listagem de todos os acessos</remarks>
        ''' <returns></returns>
        <HttpGet>
        <Route("GetAll")>
        Public Function GetAll() As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                Dim retorno = _roleServiceApplication.GetAll()
                If retorno IsNot Nothing And retorno.Count() > 0 Then
                    _logger.Info(action + " - Sucesso!")

                    _logger.Info(action + " - Finalizado")
                    Return Request.CreateResponse(HttpStatusCode.OK, retorno)
                Else
                    Throw New HttpResponseException(HttpStatusCode.NotFound)
                End If
            Catch ex As HttpResponseException
                If ex.Response.StatusCode = HttpStatusCode.NotFound Then
                    Return ResponseManager.ReturnExceptionNotFound(ex, Request, _logger, action, "Nenhum registro encontrado!")
                End If

                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' GET Api/Role/Get?id={Id}
        ''' <summary>
        ''' Listar usuário pelo Id
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="404">Not Found</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Retorna o usuário através do Id do mesmo</remarks>
        ''' <param name="Id">Id do usuário</param>
        ''' <returns></returns>
        <HttpGet>
        <Route("Get")>
        Public Function [Get](Id As Guid) As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                If Id <> Nothing Then
                    Dim role = _roleServiceApplication.Get(Id)

                    If role IsNot Nothing Then

                        _logger.Info(action + " - Sucesso!")

                        _logger.Info(action + " - Finalizado")
                        Return Request.CreateResponse(HttpStatusCode.OK, role)
                    Else
                        Throw New HttpResponseException(HttpStatusCode.NotFound)
                    End If
                Else
                    Return ResponseManager.ReturnBadRequest(Request, _logger, action, "Parâmetro incorreto!")
                End If
            Catch ex As HttpResponseException
                If ex.Response.StatusCode = HttpStatusCode.NotFound Then
                    Return ResponseManager.ReturnExceptionNotFound(ex, Request, _logger, action, "Nenhum registro encontrado!")
                End If

                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function
#End Region
    End Class
End Namespace