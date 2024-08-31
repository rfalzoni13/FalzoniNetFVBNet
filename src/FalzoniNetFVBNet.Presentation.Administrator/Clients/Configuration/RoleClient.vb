Imports System.Net.Http
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Configuration
Imports FalzoniNetFVBNet.Utils.Helpers

Namespace Clients.Register
    Public Class RoleClient
        Inherits BaseClient(Of RoleModel)
        Implements IRoleClient

        Public Sub New()
            MyBase.New()

            url += "/Role"
        End Sub

        Public Function GetAllNames() As List(Of String) Implements IRoleClient.GetAllNames
            Using client As New HttpClient()
                client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.GetAsync($"{url}/GetAllNames").Result
                If response.IsSuccessStatusCode Then
                    Dim roles = response.Content.ReadAsAsync(Of List(Of String))().Result

                    Return roles
                Else
                    Dim statusCode = response.Content.ReadAsAsync(Of StatusCodeModel)().Result

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function
    End Class
End Namespace
