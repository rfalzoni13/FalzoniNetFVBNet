Imports System.Net.Http
Imports System.Threading.Tasks
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Identity

Namespace Clients.Identity
    Public Class IdentityUtilityClient
#Region "TWO FACTORS"
        Public Async Function GetTwoFactorProviders() As Task(Of ICollection(Of String))
            Dim url = $"{PathUtils.GetApiPath()}/IdentityUtility/GetTwoFactorProviders"

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
            Dim url = $"{PathUtils.GetApiPath()}/IdentityUtility/SendTwoFactorProviderCode"

            Using client As New HttpClient()
                Dim response As HttpResponseMessage = Await client.PostAsJsonAsync(url, model)
                If Not response.IsSuccessStatusCode Then
                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Async Function VerifyCodeTwoFactor(model As VerifyCodeModel) As Task(Of ReturnVerifyCodeModel)
            Dim url = $"{PathUtils.GetApiPath()}/IdentityUtility/VerifyCodeTwoFactor"

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
            Dim url = $"{PathUtils.GetApiPath()}/IdentityUtility/SendEmailConfirmationCode"

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
            Dim url = $"{PathUtils.GetApiPath()}/IdentityUtility/SendPhoneConfirmationCode"

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
            Dim url = $"{PathUtils.GetApiPath()}/IdentityUtility/VerifyEmailConfirmationCode"

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
            Dim url = $"{PathUtils.GetApiPath()}/IdentityUtility/VerifyPhoneConfirmationCode"

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
