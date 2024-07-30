Imports System.Web
Imports FalzoniNetFVBNet.Application.IdentityConfiguration
Imports FalzoniNetFVBNet.Domain.DTO.Identity
Imports FalzoniNetFVBNet.Infra.Data.Identity
Imports FalzoniNetFVBNet.Utils.Helpers
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security

Namespace ServiceApplication.Identity
    Public Class IdentityUtilityServiceApplication
        Implements IDisposable
#Region "Attributes"
        Private _signInManager As ApplicationSignInManager
        Private _userManager As ApplicationUserManager
        Private _roleManager As ApplicationRoleManager

        Public Property AccessTokenFormat As ISecureDataFormat(Of AuthenticationTicket)

        Protected Property RoleManager As ApplicationRoleManager
            Get
                Return If(_roleManager, HttpContext.Current.GetOwinContext().Get(Of ApplicationRoleManager)())
            End Get
            Set(value As ApplicationRoleManager)
                _roleManager = value
            End Set
        End Property

        Protected Property UserManager As ApplicationUserManager
            Get
                Return If(_userManager, HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
            End Get
            Set(value As ApplicationUserManager)
                _userManager = value
            End Set
        End Property

        Protected Property SignInManager As ApplicationSignInManager
            Get
                Return If(_signInManager, HttpContext.Current.GetOwinContext().Get(Of ApplicationSignInManager)())
            End Get
            Set(value As ApplicationSignInManager)
                _signInManager = value
            End Set
        End Property

#End Region

#Region "Services"
        Public Sub SendCode(sendCode As SendCodeDTO)
            Try

                'Gerar o token e enviá-lo
                Dim user As ApplicationUser = UserManager.FindById(sendCode.UserId)
                If user Is Nothing Then
                    Throw New ApplicationException("Usuário não encontrado!")
                End If

                Dim token = UserManager.GenerateTwoFactorToken(sendCode.UserId, sendCode.SelectedProvider)

                Dim result = UserManager.NotifyTwoFactorToken(sendCode.UserId, sendCode.SelectedProvider, token)
                If Not result.Succeeded Then
                    Throw New ApplicationException("Erro ao enviar código!")
                End If


            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Sub SendEmailConfirmationCode(generateTokenEmailDTO As GenerateTokenEmailDTO)
            Try
                'Gerar o token de confirmação e enviá-lo
                Dim user As ApplicationUser = UserManager.FindById(generateTokenEmailDTO.UserId)
                If user Is Nothing Then
                    Throw New ApplicationException("Usuário não encontrado!")
                End If

                Dim token = UserManager.GenerateEmailConfirmationToken(generateTokenEmailDTO.UserId)

                Dim uri As Uri = Nothing

                If Uri.TryCreate(generateTokenEmailDTO.Url, UriKind.Absolute, uri) And uri.Scheme = Uri.UriSchemeHttp Then
                    Dim uriBuilder = New UriBuilder(uri)

                    Dim query = HttpUtility.ParseQueryString(uriBuilder.Query)
                    query("userId") = generateTokenEmailDTO.UserId
                    query("code") = StringHelper.Base64ForUrlEncode(token)

                    uriBuilder.Query = query.ToString()

                    UserManager.SendEmail(generateTokenEmailDTO.UserId, "Confirmação de Email!", String.Format("Olá, para confirmar seu e-mail, clique neste <a href='{0}' />link</a>!", uriBuilder.ToString()))
                Else
                    Throw New ApplicationException("Url inválida!")
                End If

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Sub SendPhoneConfirmationCode(generateTokenPhoneDTO As GenerateTokenPhoneDTO)
            Try

                'Gerar o token de confirmação e enviá-lo
                Dim user As ApplicationUser = UserManager.FindById(generateTokenPhoneDTO.UserId)
                If user Is Nothing Then
                    Throw New ApplicationException("Usuário não encontrado!")
                End If

                Dim token = UserManager.GenerateChangePhoneNumberToken(generateTokenPhoneDTO.UserId, generateTokenPhoneDTO.Phone)

                UserManager.SendEmail(generateTokenPhoneDTO.UserId, "Confirmação de Telefone!", String.Format("Olá, seu código para confirmar seu telefone é: {0}", token))

            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Public Function GetTwoFactorProviders(email As String) As List(Of String)
            Dim user = UserManager.FindByEmail(email)

            If user Is Nothing Then
                Throw New ApplicationException("Usuário não encontrado!")
            End If

            Dim userFactors = UserManager.GetValidTwoFactorProviders(user.Id)

            Return userFactors.ToList()
        End Function


        Public Function VerifyCode(verifiyCode As VerifyCodeDTO) As ReturnVerifyCodeDTO
            Try
                'Verificar token recebido!
                Dim user As ApplicationUser = UserManager.FindById(verifiyCode.UserId)
                If user Is Nothing Then
                    Throw New ApplicationException("Usuário não encontrado!")
                End If

                Dim token As Boolean = UserManager.VerifyTwoFactorToken(verifiyCode.UserId, verifiyCode.Provider, verifiyCode.Code)

                If Not token Then
                    Throw New ApplicationException("Código inválido!")
                End If

                Return New ReturnVerifyCodeDTO With
                {
                    .ReturnUrl = verifiyCode.ReturnUrl
                }

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function VerifyEmailConfirmationCode(confirmEmailCodeDTO As ConfirmEmailCodeDTO) As IdentityResultCodeDTO
            Try
                Dim identityResult = UserManager.ConfirmEmail(confirmEmailCodeDTO.UserId, StringHelper.Base64ForUrlDecode(confirmEmailCodeDTO.Code))
                Dim result = New IdentityResultCodeDTO With
                {
                    .Succeeded = identityResult.Succeeded,
                    .Errors = identityResult.Errors
                }

                Return result
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function VerifyPhoneConfirmationCode(confirmPhoneCodeDTO As ConfirmPhoneCodeDTO) As IdentityResultCodeDTO
            Try
                Dim identityResult = UserManager.ChangePhoneNumber(confirmPhoneCodeDTO.UserId, confirmPhoneCodeDTO.Phone, confirmPhoneCodeDTO.Code)
                Dim result = New IdentityResultCodeDTO With
                {
                    .Succeeded = identityResult.Succeeded,
                    .Errors = identityResult.Errors
                }

                Return result
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Async Services"
        Public Async Function SendCodeAsync(sendCode As SendCodeDTO) As Task
            Try

                'Gerar o token e enviá-lo
                Dim user As ApplicationUser = Await UserManager.FindByIdAsync(sendCode.UserId)
                If user Is Nothing Then
                    Throw New ApplicationException("Usuário não encontrado!")
                End If

                Dim token = Await UserManager.GenerateTwoFactorTokenAsync(sendCode.UserId, sendCode.SelectedProvider)

                Dim result = Await UserManager.NotifyTwoFactorTokenAsync(sendCode.UserId, sendCode.SelectedProvider, token)
                If Not result.Succeeded Then
                    Throw New ApplicationException("Erro ao enviar código!")
                End If


            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Async Function SendEmailConfirmationCodeAsync(generateTokenEmailDTO As GenerateTokenEmailDTO) As Task
            Try
                'Gerar o token de confirmação e enviá-lo
                Dim user As ApplicationUser = Await UserManager.FindByIdAsync(generateTokenEmailDTO.UserId)
                If user Is Nothing Then
                    Throw New ApplicationException("Usuário não encontrado!")
                End If

                Dim token = Await UserManager.GenerateEmailConfirmationTokenAsync(generateTokenEmailDTO.UserId)

                Dim uri As Uri = Nothing

                If Uri.TryCreate(generateTokenEmailDTO.Url, UriKind.Absolute, uri) And uri.Scheme = Uri.UriSchemeHttp Then
                    Dim uriBuilder = New UriBuilder(uri)

                    Dim query = HttpUtility.ParseQueryString(uriBuilder.Query)
                    query("userId") = generateTokenEmailDTO.UserId
                    query("code") = StringHelper.Base64ForUrlEncode(token)

                    uriBuilder.Query = query.ToString()

                    Await UserManager.SendEmailAsync(generateTokenEmailDTO.UserId, "Confirmação de Email!", String.Format("Olá, para confirmar seu e-mail, clique neste <a href='{0}' />link</a>!", uriBuilder.ToString()))
                Else
                    Throw New ApplicationException("Url inválida!")
                End If

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Async Function SendPhoneConfirmationCodeAsync(generateTokenPhoneDTO As GenerateTokenPhoneDTO) As Task
            Try

                'Gerar o token de confirmação e enviá-lo
                Dim user As ApplicationUser = Await UserManager.FindByIdAsync(generateTokenPhoneDTO.UserId)
                If user Is Nothing Then
                    Throw New ApplicationException("Usuário não encontrado!")
                End If

                Dim token = Await UserManager.GenerateChangePhoneNumberTokenAsync(generateTokenPhoneDTO.UserId, generateTokenPhoneDTO.Phone)

                Await UserManager.SendEmailAsync(generateTokenPhoneDTO.UserId, "Confirmação de Telefone!", String.Format("Olá, seu código para confirmar seu telefone é: {0}", token))

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Async Function GetTwoFactorProvidersAsync(email As String) As Task(Of List(Of String))
            Dim user = Await UserManager.FindByEmailAsync(email)

            If user Is Nothing Then
                Throw New ApplicationException("Usuário não encontrado!")
            End If

            Dim userFactors = Await UserManager.GetValidTwoFactorProvidersAsync(user.Id)

            Return userFactors.ToList()
        End Function


        Public Async Function VerifyCodeAsync(verifiyCode As VerifyCodeDTO) As Task(Of ReturnVerifyCodeDTO)
            Try
                'Verificar token recebido!
                Dim user As ApplicationUser = Await UserManager.FindByIdAsync(verifiyCode.UserId)
                If user Is Nothing Then
                    Throw New ApplicationException("Usuário não encontrado!")
                End If

                Dim token As Boolean = Await UserManager.VerifyTwoFactorTokenAsync(verifiyCode.UserId, verifiyCode.Provider, verifiyCode.Code)

                If Not token Then
                    Throw New ApplicationException("Código inválido!")
                End If

                Return New ReturnVerifyCodeDTO With
                {
                    .ReturnUrl = verifiyCode.ReturnUrl
                }

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Async Function VerifyEmailConfirmationCodeAsync(confirmEmailCodeDTO As ConfirmEmailCodeDTO) As Task(Of IdentityResultCodeDTO)
            Try
                Dim identityResult = Await UserManager.ConfirmEmailAsync(confirmEmailCodeDTO.UserId, StringHelper.Base64ForUrlDecode(confirmEmailCodeDTO.Code))
                Dim result = New IdentityResultCodeDTO With
                {
                    .Succeeded = identityResult.Succeeded,
                    .Errors = identityResult.Errors
                }

                Return result
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Async Function VerifyPhoneConfirmationCodeAsync(confirmPhoneCodeDTO As ConfirmPhoneCodeDTO) As Task(Of IdentityResultCodeDTO)
            Try
                Dim identityResult = Await UserManager.ChangePhoneNumberAsync(confirmPhoneCodeDTO.UserId, confirmPhoneCodeDTO.Phone, confirmPhoneCodeDTO.Code)
                Dim result = New IdentityResultCodeDTO With
                {
                    .Succeeded = identityResult.Succeeded,
                    .Errors = identityResult.Errors
                }

                Return result
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Dispose"
        Public Sub Dispose() Implements IDisposable.Dispose
            Throw New NotImplementedException()
        End Sub
#End Region

    End Class
End Namespace
