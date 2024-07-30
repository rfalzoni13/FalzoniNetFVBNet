Imports System.Threading.Tasks
Imports System.Web.Mvc
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Identity
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Identity
Imports FalzoniNetFVBNet.Utils.Helpers
Imports NLog

Namespace Controllers
    Public Class AccountController
        Inherits Controller

        Private Shared ReadOnly _logger As Logger = LogManager.GetCurrentClassLogger()
        Private ReadOnly _accountClient As AccountClient
        Private ReadOnly _identityUtilityClient As IdentityUtilityClient

        Public Sub New(accountClient As AccountClient, identityUtilityClient As IdentityUtilityClient)
            _accountClient = accountClient
            _identityUtilityClient = identityUtilityClient
        End Sub

#Region "Login"
        ' GET Account/Login
        <HttpGet>
        Public Function Login() As ActionResult

            If Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated Then

                Return RedirectToAction("Index", "Home")
            End If

            Try
                Dim model = New LoginModel()
                Return View(model)

            Catch ex As Exception
                _logger.Fatal("Ocorreu um erro: " + ex.ToString())
                Throw
            End Try
        End Function

        ' POST Account/Login
        <HttpPost>
        Public Async Function Login(model As LoginModel, returnUrl As String) As Task(Of ActionResult)

            Try
                Await _accountClient.Login(model, Request)

                Return RedirectToAction("Index", "Home")

            Catch ex As ApplicationException

                ModelState.Clear()

                ModelState.AddModelError(String.Empty, ExceptionHelper.CatchMessageFromException(ex))

                Return View()
            Catch ex As TaskCanceledException
                _logger.Error("Ocorreu um erro interno do servidor: " + ex.ToString())
                ModelState.Clear()

                ModelState.AddModelError(String.Empty, ExceptionHelper.CatchMessageFromException(ex))

                Return View()
            Catch ex As Exception
                _logger.Fatal("Ocorreu um erro: " + ex.ToString())
                Throw
            End Try
        End Function
#End Region

#Region "Logout"
        'POST Account/LogOut
        <HttpPost>
        Public Async Function LogOut() As Task(Of ActionResult)

            Try
                Await _accountClient.Logout(Request)

                Return RedirectToAction("Login")

            Catch ex As Exception

                _logger.Fatal(ex, "Erro fatal!")
                Throw
            End Try
        End Function
#End Region

#Region "ExternalLogin"
        ' POST: /Account/ExternalLogin
        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function ExternalLogin(provider As String, returnUrl As String) As Task(Of ActionResult)

            Await _accountClient.ExternalLogin(provider)

            Return RedirectToAction("Index", "Home")
        End Function
#End Region

#Region "SendCode"
        ' GET /Account/SendCode
        <AllowAnonymous>
        Public Async Function SendCode(returnUrl As String, rememberMe As Boolean) As Task(Of ActionResult)
            Try

                Dim userFactors = Await _identityUtilityClient.GetTwoFactorProviders()

                Dim factorOptions = userFactors.Select(Function(purpose) New SelectListItem With {.Text = purpose, .Value = purpose}).ToList()

                Return View(New SendCodeModel With {.Providers = factorOptions, .ReturnUrl = returnUrl, .RememberMe = rememberMe})

            Catch ex As Exception
                _logger.Fatal(ex, "Erro fatal!")
                Throw
            End Try
        End Function

        ' POST /Account/SendCode
        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function SendCode(model As SendCodeModel) As Task(Of ActionResult)

            Try

                Await _identityUtilityClient.SendTwoFactorProviderCode(model)

                Return RedirectToAction("VerifyCode", New With {.Provider = model.SelectedProvider, model.ReturnUrl, model.RememberMe})

            Catch ex As Exception
                _logger.Fatal(ex, "Erro fatal!")
                Throw
            End Try

        End Function
#End Region

#Region "VerifyCode"
        ' GET /Account/VerifyCode
        <AllowAnonymous>
        Public Function VerifyCode(provider As String, returnUrl As String, rememberMe As Boolean) As ActionResult
            Try

                If Not Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated Then
                    Throw New Exception("Não autorizado!")
                End If

                Return View(New VerifyCodeModel With {.Provider = provider, .ReturnUrl = returnUrl, .RememberMe = rememberMe})

            Catch ex As Exception
                _logger.Fatal(ex, "Erro fatal!")
                Throw
            End Try
        End Function

        '
        ' POST: /Account/VerifyCode
        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function VerifyCode(model As VerifyCodeModel) As Task(Of ActionResult)
            Dim result = Await _identityUtilityClient.VerifyCodeTwoFactor(model)

            Return RedirectToLocal(result.ReturnUrl)
        End Function
#End Region

#Region "ForgotPassword"
        'GET /Account/ForgotPassword
        <HttpGet>
        Public Function ForgotPassword() As ActionResult

            Dim model = New ForgotPasswordModel()

            Return View(model)
        End Function

        ' POST: /Account/ForgotPassword
        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function ForgotPassword(model As ForgotPasswordModel) As Task(Of ActionResult)
            Try
                model.CallBackUrl = Url.Action("ResetPassword", "Account", New With {.userId = "{0}", .code = "{1}"}, protocol:=Request.Url.Scheme)

                Await _accountClient.ForgotPassword(model)

                Return View(model)

            Catch ex As ApplicationException
                _logger.Error(ex, ex.Message)
                Return RedirectToAction("ConfirmResetPassword", "Account")

            Catch ex As Exception
                _logger.Fatal(ex, "Erro fatal!")
                Throw
            End Try
        End Function
#End Region

#Region "ResetPassword"
        '
        ' GET: /Account/ResetPassword
        <AllowAnonymous>
        Public Function ResetPassword(code As String) As ActionResult
            Return If(code = Nothing, View("Error"), View())
        End Function

        ' POST: /Account/ResetPassword
        <HttpPost>
        <AllowAnonymous>
        <ValidateAntiForgeryToken>
        Public Async Function ResetPassword(model As ResetPasswordModel) As Task(Of ActionResult)

            Try

                Dim result = Await _accountClient.ResetPassword(model)

                If result.Succeeded Then
                    Return View(model)
                Else
                    Return RedirectToAction("ConfirmResetPassword", "Account")
                End If

            Catch ex As ApplicationException
                _logger.Error(ex, ex.Message)
                Return RedirectToAction("ConfirmResetPassword", "Account")

            Catch ex As Exception
                _logger.Fatal(ex, "Erro fatal!")
                Throw
            End Try
        End Function
#End Region

#Region "ConfirmResetPassword"
        ' GET: /Account/ConfirmResetPassword
        <AllowAnonymous>
        Public Function ConfirmResetPassword() As ActionResult
            Return View()
        End Function
#End Region

#Region "Private METHODS"
        Private Function RedirectToLocal(returnUrl As String) As ActionResult

            If Url.IsLocalUrl(returnUrl) Then
                Return Redirect(returnUrl)
            End If
            Return RedirectToAction("Index", "Home")
        End Function
#End Region
    End Class
End Namespace