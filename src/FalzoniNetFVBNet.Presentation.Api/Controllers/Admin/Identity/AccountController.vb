Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Results
Imports FalzoniNetFVBNet.Application.IdentityConfiguration
Imports FalzoniNetFVBNet.Application.ServiceApplication.Identity
Imports FalzoniNetFVBNet.Presentation.Api.Models.Identity
Imports FalzoniNetFVBNet.Presentation.Api.Utils
Imports FalzoniNetFVBNet.Utils.Helpers
Imports Microsoft.AspNet.Identity
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Cookies
Imports NLog

Namespace Controllers.Admin.Identity
    Public Class AccountController
        Inherits ApiController
#Region "Attributes"
        Private Shared ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()
        Private ReadOnly _accountServiceApplication As AccountServiceApplication
#End Region

#Region "Constructor"
        Public Sub New(accountServiceApplication As AccountServiceApplication)
            _accountServiceApplication = accountServiceApplication
        End Sub
#End Region

#Region "Logout"
        ' POST Api/Account/Logout
        ''' <summary>
        ''' Logout
        ''' </summary>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Deslogar do Sistema</remarks>
        ''' <returns></returns>
        <HttpPost>
        Public Function Logout() As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                ApplicationOAuthProvider.Logout(Request.GetOwinContext(), CookieAuthenticationDefaults.AuthenticationType)

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(HttpStatusCode.OK)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function
#End Region

#Region "External Logins"
        ' GET Api/Account/ExternalLogin
        ''' <summary>
        ''' Login Externo
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="provider"></param>
        ''' <param name="error"></param>
        ''' <remarks>Efetuar login com provedores externos</remarks>
        ''' <returns></returns>
        <OverrideAuthentication>
        <HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)>
        <HttpGet>
        Public Async Function ExternalLogin(provider As String, Optional [error] As String = Nothing) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                If [error] IsNot Nothing Then
                    Throw New Exception([error])
                End If

                If Not User.Identity.IsAuthenticated Then
                    Return New ChallengeResult(provider, Me)
                End If

                Await ApplicationOAuthProvider.ExternalLogin(Request.GetOwinContext(), User, provider)
                Return Request.CreateResponse(HttpStatusCode.OK)
            Catch ex As UnauthorizedAccessException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As ApplicationException
                Return ResponseManager.ReturnBadRequest(ex, Request, _logger, action)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' GET Api/Account/ExternalLogin
        ''' <summary>
        ''' Obter Logins Externos
        ''' </summary>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="returnUrl"></param>
        ''' <param name="generateState"></param>
        ''' <remarks>Efetuar login com provedores externos</remarks>
        ''' <returns></returns>
        <OverrideAuthentication>
        <HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)>
        <HttpGet>
        Public Function GetExternalLogins(returnUrl As String, Optional generateState As Boolean = False) As HttpResponseMessage
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                Dim descriptions As IEnumerable(Of AuthenticationDescription) = ApplicationOAuthProvider.GetExternalAuthenticationTypes(Request.GetOwinContext())
                Dim logins As List(Of ExternalLoginModel) = New List(Of ExternalLoginModel)

                Dim state As String

                If generateState Then
                    Const strengthInBits As Int32 = 256
                    state = RandomOAuthStateGeneratorHelper.Generate(strengthInBits)
                Else
                    state = Nothing
                End If

                For Each description As AuthenticationDescription In descriptions
                    Dim login As ExternalLoginModel = New ExternalLoginModel With
                    {
                        .Name = description.Caption,
                        .Url = Url.Route("ExternalLogin", New With
                        {
                            .provider = description.AuthenticationType,
                            .response_type = "token",
                            .client_id = AppBuilderConfiguration.PublicClientId,
                            .redirect_uri = New Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                            .state = state
                        }),
                        .State = state
                    }
                    logins.Add(login)
                Next

                Return Request.CreateResponse(HttpStatusCode.OK, logins)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' POST Api/Account/AddExternalLogin
        ''' <summary>
        ''' Adicionar Login Externo
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="model"></param>
        ''' <remarks>Adiciona login externo</remarks>
        ''' <returns></returns>
        <HttpPost>
        Public Async Function AddExternalLogin(model As AddExternalLoginBindingModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                ApplicationOAuthProvider.Logout(Request.GetOwinContext(), DefaultAuthenticationTypes.ExternalCookie)

                Dim result = Await _accountServiceApplication.AddExternalLoginAsync(User.Identity.GetUserId(), model.ExternalAccessToken)

                If Not result.Succeeded Then
                    Return ResponseManager.ReturnErrorResult(Request, _logger, action, result.Errors)
                End If

                Return Request.CreateResponse(HttpStatusCode.OK, result)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' POST Api/Account/AddExternalUserLogin
        ''' <summary>
        ''' Adicionar Usuário ao Login Externo
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="model"></param>
        ''' <remarks>Adiciona usuário ao provedor de login externo</remarks>
        ''' <returns></returns>
        <HttpPost>
        Public Async Function AddExternalUserLogin(model As RegisterExternalBindingModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                Dim result = Await ApplicationOAuthProvider.RegisterExternal(Request.GetOwinContext(), model.Email, model.Email)

                If Not result.Succeeded Then
                    Return ResponseManager.ReturnErrorResult(Request, _logger, action, result.Errors)
                End If

                Return Request.CreateResponse(HttpStatusCode.OK, result)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' POST Api/Account/RemoveExternalLogin
        ''' <summary>
        ''' Remover Login Externo
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="model"></param>
        ''' <remarks>Remove provedor de login externo</remarks>
        ''' <returns></returns>
        <HttpPost>
        Public Async Function RemoveExternalLogin(model As RemoveLoginBindingModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try
                _logger.Info(action + " - Iniciado")

                Dim result = Await _accountServiceApplication.RemoveExternalLoginAsync(User.Identity.GetUserId(), model.LoginProvider, model.ProviderKey)

                If Not result.Succeeded Then
                    Return ResponseManager.ReturnErrorResult(Request, _logger, action, result.Errors)
                End If

                Return Request.CreateResponse(HttpStatusCode.OK, result)
            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function
#End Region
    End Class
End Namespace