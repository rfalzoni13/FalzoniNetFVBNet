Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common

Namespace Utils
    Public NotInheritable Class ResponseUtils(Of T As Class)
        Public Shared Function ReturnMessage(response As HttpResponseMessage) As T
            If response.IsSuccessStatusCode Then
                Return response.Content.ReadAsAsync(Of T).Result
            End If

            Select Case response.StatusCode
                Case HttpStatusCode.NotFound
                    Throw New ApplicationException("Caminho ou serviço não encontrado")
                Case Else
                    Dim statusCode As StatusCodeModel = response.Content.ReadAsAsync(Of StatusCodeModel).Result
                    Throw New ApplicationException(statusCode.Message)
            End Select
        End Function

        Public Shared Async Function ReturnMessageAsync(response As HttpResponseMessage) As Task(Of T)
            If response.IsSuccessStatusCode Then
                Return Await response.Content.ReadAsAsync(Of T)
            End If

            Select Case response.StatusCode
                Case HttpStatusCode.NotFound
                    Throw New ApplicationException("Caminho ou serviço não encontrado")
                Case Else
                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)
                    Throw New ApplicationException(statusCode.Message)
            End Select
        End Function
    End Class
End Namespace
