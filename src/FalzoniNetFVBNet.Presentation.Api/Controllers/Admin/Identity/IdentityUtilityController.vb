Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports FalzoniNetFVBNet.Application.ServiceApplication.Identity
Imports FalzoniNetFVBNet.Domain.DTO.Identity
Imports FalzoniNetFVBNet.Presentation.Api.Attributes
Imports FalzoniNetFVBNet.Presentation.Api.Models.Identity
Imports FalzoniNetFVBNet.Presentation.Api.Utils
Imports NLog

Namespace Controllers.Admin.Identity
    Public Class IdentityUtilityController
        Inherits ApiController
#Region "Attributes"
        Private Shared ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()
        Private ReadOnly _identityUtilityServiceApplication As IdentityUtilityServiceApplication
#End Region

#Region "Constructor"
        Public Sub New(identityUtilityServiceApplication As IdentityUtilityServiceApplication)
            _identityUtilityServiceApplication = identityUtilityServiceApplication
        End Sub
#End Region

#Region "Two Factor"
        ' GET /IdentityUtitlity/GetTwoFactorProviders
        ''' <summary>
        ''' Obter Autenticação Dois Fatores
        ''' </summary>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="email"></param>
        ''' <param name="returnUrl"></param>
        ''' <remarks>Obtém as opções de autencicação de dois fatores</remarks>
        ''' <returns></returns>
        <HttpGet>
        Public Async Function GetTwoFactorProviders(email As String, Optional returnUrl As String = Nothing) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

            Try

                Dim userFactors = Await _identityUtilityServiceApplication.GetTwoFactorProvidersAsync(email)

                Return Request.CreateResponse(HttpStatusCode.OK, userFactors)

            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function


        ' POST /IdentityUtitlity/SendTwoFactorProviderCode
        ''' <summary>
        ''' Enviar código de dois fatores
        ''' </summary>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="sendCodeModel"></param>
        ''' <remarks>Efetua o envio do código de dois fatores</remarks>
        ''' <returns></returns>
        <CustomAuthorize>
        <HttpPost>
        Public Async Function SendTwoFactorProviderCode(sendCodeModel As SendCodeModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

            Try
                Dim SendCodeDTO = New SendCodeDTO With
                {
                    .UserId = sendCodeModel.UserId,
                    .SelectedProvider = sendCodeModel.SelectedProvider
                }

                Await _identityUtilityServiceApplication.SendCodeAsync(SendCodeDTO)

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Código enviado com sucesso!")

            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' POST /IdentityUtitlity/VerifyCodeTwoFactor
        ''' <summary>
        ''' Verificar código de dois fatores
        ''' </summary>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="verifiyCodeModel"></param>
        ''' <remarks>Efetua verificação de código de dois fatores</remarks>
        ''' <returns></returns>
        <CustomAuthorize>
        <HttpPost>
        Public Async Function VerifyCodeTwoFactor(verifiyCodeModel As VerifyCodeModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName

            Try
                If Not User.Identity.IsAuthenticated Then
                    Throw New Exception("Usuário não autenticado!")
                End If

                _logger.Info(action + " - Iniciado")
                Dim verifiyCodeDTO = New VerifyCodeDTO With
                {
                    .UserId = verifiyCodeModel.UserId,
                    .Code = verifiyCodeModel.Code,
                    .Provider = verifiyCodeModel.Provider,
                    .ReturnUrl = verifiyCodeModel.ReturnUrl
                }

                Dim retornoCodigo = Await _identityUtilityServiceApplication.VerifyCodeAsync(verifiyCodeDTO)

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(System.Net.HttpStatusCode.OK, retornoCodigo)

            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

#End Region

#Region "Phone and E-mail confirmation"
        ' POST: /IdentityUtitlity/SendEmailConfirmationCode
        ''' <summary>
        ''' Enviar Código de Confirmação de E-mail
        ''' </summary>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="generateTokenEmailModel"></param>
        ''' <remarks>Envio de código de confirmação de e-mail</remarks>
        ''' <returns></returns>
        <HttpPost>
        Public Async Function SendEmailConfirmationCode(generateTokenEmailModel As GenerateTokenEmailModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try

                _logger.Info(action + " - Iniciado")
                Dim generateTokenEmailDTO = New GenerateTokenEmailDTO With
                {
                    .UserId = generateTokenEmailModel.UserId,
                    .Url = generateTokenEmailModel.Url
                }

                Await _identityUtilityServiceApplication.SendEmailConfirmationCodeAsync(generateTokenEmailDTO)

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Código enviado com sucesso!")

            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function


        ' POST /IdentityUtitlity/SendPhoneConfirmationCode
        ''' <summary>
        ''' Enviar Código de Confirmação de Telefone
        ''' </summary>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="generateTokenPhoneModel"></param>
        ''' <remarks>Envio de código de confirmação de telefone</remarks>
        ''' <returns></returns>
        <HttpPost>
        Public Async Function SendPhoneConfirmationCode(generateTokenPhoneModel As GenerateTokenPhoneModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try

                _logger.Info(action + " - Iniciado")
                Dim generateTokenPhoneDTO = New GenerateTokenPhoneDTO With
                {
                    .UserId = generateTokenPhoneModel.UserId,
                    .Phone = generateTokenPhoneModel.Phone
                }

                Await _identityUtilityServiceApplication.SendPhoneConfirmationCodeAsync(generateTokenPhoneDTO)

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Código enviado com sucesso!")

            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

        ' POST /IdentityUtitlity/VerifyEmailConfirmationCode
        ''' <summary>
        ''' Verificar Código de Confirmação de E-mail
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="confirmEmailCodeModel"></param>
        ''' <remarks>Verificação de código de confirmação do e-mail do usuário</remarks>
        ''' <returns></returns>
        <HttpPost>
        Public Async Function VerifyEmailConfirmationCode(confirmEmailCodeModel As ConfirmEmailCodeModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try

                _logger.Info(action + " - Iniciado")
                Dim confirmEmailCodeDTO = New ConfirmEmailCodeDTO With
                {
                    .UserId = confirmEmailCodeModel.UserId,
                    .Code = confirmEmailCodeModel.Code
                }

                Dim result = Await _identityUtilityServiceApplication.VerifyEmailConfirmationCodeAsync(confirmEmailCodeDTO)
                If Not result.Succeeded Then
                    Return ResponseManager.ReturnErrorResult(Request, _logger, action, result.Errors)
                End If

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Email confirmado com sucesso!")

            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function


        ' POST: /IdentityUtitlity/VerifyPhoneConfirmationCode
        ''' <summary>
        ''' Verificar Código de Confirmação de Telefone
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <param name="confirmPhoneCodeModel"></param>
        ''' <remarks>Verificação de código de confirmação do telefone do usuário</remarks>
        ''' <returns></returns>
        <HttpPost>
        Public Async Function VerifyPhoneConfirmationCode(confirmPhoneCodeModel As ConfirmPhoneCodeModel) As Task(Of HttpResponseMessage)
            Dim action As String = Me.ActionContext.ActionDescriptor.ActionName
            Try

                _logger.Info(action + " - Iniciado")
                Dim confirmPhoneCodeDTO = New ConfirmPhoneCodeDTO With
                {
                    .UserId = confirmPhoneCodeModel.UserId,
                    .Phone = confirmPhoneCodeModel.Phone,
                    .Code = confirmPhoneCodeModel.Code
                }

                Dim result = Await _identityUtilityServiceApplication.VerifyPhoneConfirmationCodeAsync(confirmPhoneCodeDTO)
                If Not result.Succeeded Then
                    Return ResponseManager.ReturnErrorResult(Request, _logger, action, result.Errors)
                End If

                _logger.Info(action + " - Sucesso!")

                _logger.Info(action + " - Finalizado")
                Return Request.CreateResponse(System.Net.HttpStatusCode.OK, "Telefone confirmado com sucesso!")

            Catch ex As Exception
                Return ResponseManager.ReturnExceptionInternalServerError(ex, Request, _logger, action)
            End Try
        End Function

#End Region
    End Class
End Namespace