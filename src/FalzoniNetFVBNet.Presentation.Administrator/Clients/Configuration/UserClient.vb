Imports System.Net.Http
Imports System.Threading.Tasks
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Identity
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Configuration
Imports FalzoniNetFVBNet.Utils.Helpers

Namespace Clients.Register
    Public Class UserClient
        Inherits BaseClient(Of UserModel)
        Implements IUserClient

        Public Sub New()
            MyBase.New()

            url += "/User"
        End Sub

        Public Overrides Async Function GetAsync(id As String) As Task(Of UserModel)
            Using client As New HttpClient()
                client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = Await client.GetAsync($"{url}/Get/{id}")
                If response.IsSuccessStatusCode Then
                    Dim model = Await response.Content.ReadAsAsync(Of UserModel)()

                    'Carregar foto do perfil
                    model.LoadProfilePhoto()

                    Return model
                Else
                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()
                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Async Function GetTableAsync() As Task(Of UserTableModel) Implements IUserClient.GetTableAsync
            Dim table = New UserTableModel()

            Try
                Using client As New HttpClient()
                    client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token)

                    Dim response As HttpResponseMessage = Await client.GetAsync($"{url}/GetAll")
                    If response.IsSuccessStatusCode Then
                        Dim users = Await response.Content.ReadAsAsync(Of ICollection(Of UserModel))()

                        For Each user In users
                            table.data.Add(New UserListTableModel With
                            {
                                .Id = user.Id,
                                .Name = user.Name,
                                .Email = user.Email,
                                .UserName = user.UserName,
                                .Gender = user.Gender,
                                .Created = user.Created,
                                .Modified = user.Modified
                            })
                        Next

                        table.recordsFiltered = table.data.Count
                        table.recordsTotal = table.data.Count
                    Else
                        Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()
                        Throw New ApplicationException(statusCode.Message)
                    End If
                End Using
            Catch ex As Exception
                table.error = ExceptionHelper.CatchMessageFromException(ex)
            End Try

            Return Await Task.FromResult(table)
        End Function

        Public Overrides Function Add(obj As UserModel) As String
            Dim model = New ApplicationUserModel(obj)

            Using client As New HttpClient()
                client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.PostAsJsonAsync($"{url}/Add", model).Result
                If response.IsSuccessStatusCode Then
                    Return response.Content.ReadAsAsync(Of String)().Result
                Else
                    Dim statusCode As StatusCodeModel = response.Content.ReadAsAsync(Of StatusCodeModel)().Result

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overrides Function Update(obj As UserModel) As String
            Dim model = New ApplicationUserModel(obj)

            Using client As New HttpClient()
                client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.PutAsJsonAsync($"{url}/Update", model).Result
                If response.IsSuccessStatusCode Then
                    Dim retorno As String = response.Content.ReadAsAsync(Of String)().Result
                    Return retorno
                Else
                    Dim statusCode As StatusCodeModel = response.Content.ReadAsAsync(Of StatusCodeModel)().Result

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function
    End Class
End Namespace
