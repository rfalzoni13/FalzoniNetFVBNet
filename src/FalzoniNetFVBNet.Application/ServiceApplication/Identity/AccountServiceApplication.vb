Imports System.Web
Imports FalzoniNetFVBNet.Application.IdentityConfiguration
Imports FalzoniNetFVBNet.Domain.DTO.Identity
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security

Namespace ServiceApplication.Identity
    Public Class AccountServiceApplication
        Implements IDisposable

#Region "Attributes"
        Private Const LocalLoginProvider As String = "Local"
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

        Protected Property Usermanager As ApplicationUserManager
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
        Public Function AddExternalLogin(userId As String, externalAccessToken As String) As IdentityResultCodeDTO
            Dim ticket As AuthenticationTicket = AccessTokenFormat.Unprotect(externalAccessToken)

            If ticket Is Nothing Or ticket.Identity Is Nothing Or (ticket.Properties IsNot Nothing And
                 ticket.Properties.ExpiresUtc.HasValue And
                ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow) Then

                Throw New ApplicationException("Falha no login externo.")

            End If

            Dim externalData As ExternalLoginData = ExternalLoginData.FromIdentity(ticket.Identity)

            If externalData Is Nothing Then
                Throw New ApplicationException("O login externo já está associado a uma conta.")
            End If

            Dim result As IdentityResult = Usermanager.AddLogin(userId,
                New UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey))

            Return New IdentityResultCodeDTO With
            {
                .Succeeded = result.Succeeded,
                .Errors = result.Errors
            }
        End Function

        Public Function ChangePassword(changePasswordBindingDTO As ChangePasswordBindingDTO) As IdentityResultCodeDTO
            Dim result = Usermanager.ChangePassword(changePasswordBindingDTO.UserId, changePasswordBindingDTO.OldPassword,
                                        changePasswordBindingDTO.NewPassword)
            If Not result.Succeeded Then
                Throw New Exception("Erro ao alterar senha!")
            End If

            Return New IdentityResultCodeDTO With
            {
                .Succeeded = result.Succeeded,
                .Errors = result.Errors
            }
        End Function

        Public Function ResetPassword(resetPasswordDTO As ResetPasswordDTO) As IdentityResultCodeDTO
            Dim user = Usermanager.FindByEmail(resetPasswordDTO.Email)
            If user Is Nothing Then
                Throw New Exception("Usuário não encontrado!")
            End If

            Dim identityResult As IdentityResult = Usermanager.ResetPassword(user.Id, resetPasswordDTO.Code, resetPasswordDTO.Password)

            Dim result = New IdentityResultCodeDTO With
            {
                .Succeeded = identityResult.Succeeded,
                .Errors = identityResult.Errors
            }

            Return result

        End Function

        Public Sub SendEmailResetPassowrd(confirmEmailCodeDTO As ConfirmEmailCodeDTO)
            Usermanager.SendEmail(confirmEmailCodeDTO.UserId, "Redefinir senha", "Redefina sua senha, clicando <a href=\"" + confirmEmailCodeDTO.CallBackUrl + " \ ">aqui</a>")
        End Sub

        Public Function GeneratePasswordResetToken(email As String) As ConfirmEmailCodeDTO
            Dim user = Usermanager.FindByEmail(email)
            If user Is Nothing Or Not (Usermanager.IsEmailConfirmed(user.Id)) Then
                'Não revelar que o usuário não existe ou não está confirmado
                Throw New Exception("Usuário/Email não existente ou não confirmado!")
            End If

            'Para obter mais informações sobre como habilitar a confirmação da conta e redefinição de senha, visite https//go.microsoft.com/fwlink/?LinkID=320771
            'Enviar um email com este link
            Dim code As String = Usermanager.GeneratePasswordResetToken(user.Id)

            Return New ConfirmEmailCodeDTO With
            {
                .UserId = user.Id,
                .Code = code
            }
        End Function




        Public Function RemoveExternalLogin(userId As String, loginProvider As String, loginKey As String) As IdentityResultCodeDTO
            Dim result As IdentityResult

            If loginProvider = LocalLoginProvider Then
                result = Usermanager.RemovePassword(userId)
            Else
                result = Usermanager.RemoveLogin(userId, New UserLoginInfo(loginProvider, loginKey))
            End If

            Return New IdentityResultCodeDTO With
            {
                .Succeeded = result.Succeeded,
                .Errors = result.Errors
            }
        End Function
#End Region

#Region "Async Services"
        Public Async Function AddExternalLoginAsync(userId As String, externalAccessToken As String) As Task(Of IdentityResultCodeDTO)
            Dim ticket As AuthenticationTicket = AccessTokenFormat.Unprotect(externalAccessToken)

            If ticket Is Nothing Or ticket.Identity Is Nothing Or (ticket.Properties IsNot Nothing And
                 ticket.Properties.ExpiresUtc.HasValue And
                ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow) Then

                Throw New ApplicationException("Falha no login externo.")

            End If

            Dim externalData As ExternalLoginData = ExternalLoginData.FromIdentity(ticket.Identity)

            If externalData Is Nothing Then
                Throw New ApplicationException("O login externo já está associado a uma conta.")
            End If

            Dim result As IdentityResult = Await Usermanager.AddLoginAsync(userId,
                New UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey))

            Return New IdentityResultCodeDTO With
            {
                .Succeeded = result.Succeeded,
                .Errors = result.Errors
            }
        End Function

        Public Async Function ChangePasswordAsync(changePasswordBindingDTO As ChangePasswordBindingDTO) As Task(Of IdentityResultCodeDTO)
            Dim result = Await Usermanager.ChangePasswordAsync(changePasswordBindingDTO.UserId, changePasswordBindingDTO.OldPassword,
                                        changePasswordBindingDTO.NewPassword)
            If Not result.Succeeded Then
                Throw New Exception("Erro ao alterar senha!")
            End If

            Return New IdentityResultCodeDTO With
            {
                .Succeeded = result.Succeeded,
                .Errors = result.Errors
            }
        End Function

        Public Async Function ResetPasswordAsync(resetPasswordDTO As ResetPasswordDTO) As Task(Of IdentityResultCodeDTO)
            Dim user = Await Usermanager.FindByEmailAsync(resetPasswordDTO.Email)
            If user Is Nothing Then
                Throw New Exception("Usuário não encontrado!")
            End If

            Dim identityResult As IdentityResult = Await Usermanager.ResetPasswordAsync(user.Id, resetPasswordDTO.Code, resetPasswordDTO.Password)

            Dim result = New IdentityResultCodeDTO With
            {
                .Succeeded = identityResult.Succeeded,
                .Errors = identityResult.Errors
            }

            Return result

        End Function

        Public Async Function SendEmailResetPassowrdAsync(confirmEmailCodeDTO As ConfirmEmailCodeDTO) As Task
            Await Usermanager.SendEmailAsync(confirmEmailCodeDTO.UserId, "Redefinir senha", "Redefina sua senha, clicando <a href=\"" + confirmEmailCodeDTO.CallBackUrl + " \ ">aqui</a>")
        End Function

        Public Async Function GeneratePasswordResetTokenAsync(email As String) As Task(Of ConfirmEmailCodeDTO)
            Dim user = Await Usermanager.FindByEmailAsync(email)
            If user Is Nothing Or Not (Usermanager.IsEmailConfirmed(user.Id)) Then
                'Não revelar que o usuário não existe ou não está confirmado
                Throw New Exception("Usuário/Email não existente ou não confirmado!")
            End If

            'Para obter mais informações sobre como habilitar a confirmação da conta e redefinição de senha, visite https//go.microsoft.com/fwlink/?LinkID=320771
            'Enviar um email com este link
            Dim code As String = Await Usermanager.GeneratePasswordResetTokenAsync(user.Id)

            Return New ConfirmEmailCodeDTO With
            {
                .UserId = user.Id,
                .Code = code
            }
        End Function

        Public Async Function RemoveExternalLoginAsync(userId As String, loginProvider As String, loginKey As String) As Task(Of IdentityResultCodeDTO)
            Dim result As IdentityResult

            If loginProvider = LocalLoginProvider Then
                result = Await Usermanager.RemovePasswordAsync(userId)
            Else
                result = Await Usermanager.RemoveLoginAsync(userId, New UserLoginInfo(loginProvider, loginKey))
            End If

            Return New IdentityResultCodeDTO With
            {
                .Succeeded = result.Succeeded,
                .Errors = result.Errors
            }
        End Function
#End Region


#Region "Dispose"
        Public Sub Dispose() Implements IDisposable.Dispose
            Throw New NotImplementedException()
        End Sub
#End Region

    End Class
End Namespace
