Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports FalzoniNetFVBNet.Application.ServiceApplication.Configuration
Imports FalzoniNetFVBNet.Presentation.Api.Attributes
Imports FalzoniNetFVBNet.Presentation.Api.Utils
Imports NLog

Namespace Controllers.Admin.Configuration
    <CustomAuthorize(Roles:="Administrator")>
    <RoutePrefix("Api/Role")>
    Public Class RoleController
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
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Listagem de todos os acessos pelos nomes</remarks>
        ''' <returns></returns>
        <HttpGet>
        <Route("GelAllNames")>
        Public Function GelAllNames() As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                Dim retorno = _roleServiceApplication.GelAllNames()

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, retorno)

            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' GET Api/Role/GetAll
        ''' <summary>
        ''' Listar todos os acessos
        ''' </summary>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Listagem de todos os acessos</remarks>
        ''' <returns></returns>
        <HttpGet>
        <Route("GetAll")>
        Public Function GetAll() As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")
                Dim retorno = _roleServiceApplication.GetAll()
                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, retorno)
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

                Dim role = _roleServiceApplication.Get(Id)


                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK, role)
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function
#End Region

    End Class
End Namespace