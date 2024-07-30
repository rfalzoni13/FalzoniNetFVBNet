Imports System.Net.Http
Imports System.Threading.Tasks
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Identity
Imports FalzoniNetFVBNet.Utils.Helpers

Namespace Clients.Identity
    Public Class IdentityUtilityClient
#Region "TWO FACTORS"
        Public Async Function GetTwoFactorProviders() As Task(Of ICollection(Of String))
            Dim url = UrlConfigurationHelper.IdentityUtilityGetTwoFactorProviders

            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.GetAsync(url)
                If response.IsSuccessStatusCode Then

                    Return Await response.Content.ReadAsAsync(Of ICollection(Of String))()

                Else

                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New Exception(statusCode.Message)
                End If
            End Using
        End Function

        Public Async Function SendTwoFactorProviderCode(model As SendCodeModel) As Task
            Dim url = UrlConfigurationHelper.IdentityUtilitySendTwoFactorProviderCode

            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.PostAsJsonAsync(url, model)
                If Not response.IsSuccessStatusCode Then
                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Async Function VerifyCodeTwoFactor(model As VerifyCodeModel) As Task(Of ReturnVerifyCodeModel)
            Dim url = UrlConfigurationHelper.IdentityUtilityVerifyCodeTwoFactor

            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.PostAsJsonAsync(url, model)
                If response.IsSuccessStatusCode Then

                    Return Await response.Content.ReadAsAsync(Of ReturnVerifyCodeModel)()

                Else

                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New Exception(statusCode.Message)
                End If
            End Using
        End Function
#End Region

#Region "PHONE AND E-MAIL CONFIRMATION"
        Public Async Function SendEmailConfirmationCode(model As GenerateTokenEmailModel) As Task(Of String)
            Dim url = UrlConfigurationHelper.IdentityUtilitySendEmailConfirmationCode

            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.PostAsJsonAsync(url, model)
                If response.IsSuccessStatusCode Then

                    Return Await response.Content.ReadAsAsync(Of String)()

                Else

                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New Exception(statusCode.Message)
                End If
            End Using
        End Function

        Public Async Function SendPhoneConfirmationCode(model As GenerateTokenPhoneModel) As Task(Of String)
            Dim url = UrlConfigurationHelper.IdentityUtilitySendPhoneConfirmationCode

            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.PostAsJsonAsync(url, model)
                If response.IsSuccessStatusCode Then

                    Return Await response.Content.ReadAsAsync(Of String)()

                Else

                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New Exception(statusCode.Message)
                End If
            End Using
        End Function

        Public Async Function VerifyEmailConfirmationCode(model As ConfirmEmailCodeModel) As Task(Of String)
            Dim url = UrlConfigurationHelper.IdentityUtilityVerifyEmailConfirmationCode

            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.PostAsJsonAsync(url, model)
                If response.IsSuccessStatusCode Then

                    Return Await response.Content.ReadAsAsync(Of String)()

                Else

                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New Exception(statusCode.Message)
                End If
            End Using
        End Function

        Public Async Function VerifyPhoneConfirmationCode(model As ConfirmPhoneCodeModel) As Task(Of String)
            Dim url = UrlConfigurationHelper.IdentityUtilityVerifyPhoneConfirmationCode

            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.PostAsJsonAsync(url, model)
                If response.IsSuccessStatusCode Then

                    Return Await response.Content.ReadAsAsync(Of String)()

                Else

                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New Exception(statusCode.Message)
                End If
            End Using
        End Function
#End Region
    End Class
End Namespace
