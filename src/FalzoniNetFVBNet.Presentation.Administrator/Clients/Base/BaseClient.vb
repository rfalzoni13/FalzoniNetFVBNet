Imports System.Net.Http
Imports System.Threading.Tasks
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Utils.Helpers
Imports Newtonsoft.Json

Namespace Clients.Base
    Public Class BaseClient(Of T As Class, TTable As Class)
        Implements IBaseClient(Of T, TTable)

        Protected token As String

        Public Sub New()
            token = RequestHelper.GetAccessToken()
        End Sub

        Public Overridable Function Add(url As String, obj As T) As String Implements IBaseClient(Of T, TTable).Add
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.PostAsJsonAsync(url, obj).Result
                If response.IsSuccessStatusCode Then
                    Return response.Content.ReadAsAsync(Of String)().Result
                Else
                    Dim statusCode As StatusCodeModel = response.Content.ReadAsAsync(Of StatusCodeModel)().Result

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Async Function AddAsync(url As String, obj As T) As Task(Of String) Implements IBaseClient(Of T, TTable).AddAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = Await client.PostAsJsonAsync(url, obj)
                If response.IsSuccessStatusCode Then
                    Return Await response.Content.ReadAsAsync(Of String)()
                Else
                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Function Update(url As String, obj As T) As String Implements IBaseClient(Of T, TTable).Update
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.PutAsJsonAsync(url, obj).Result
                If response.IsSuccessStatusCode Then
                    Return response.Content.ReadAsAsync(Of String)().Result
                Else
                    Dim statusCode As StatusCodeModel = response.Content.ReadAsAsync(Of StatusCodeModel)().Result

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Async Function UpdateAsync(url As String, obj As T) As Task(Of String) Implements IBaseClient(Of T, TTable).UpdateAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = Await client.PutAsJsonAsync(url, obj)
                If response.IsSuccessStatusCode Then
                    Return Await response.Content.ReadAsAsync(Of String)()
                Else
                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Function Delete(url As String, obj As T) As String Implements IBaseClient(Of T, TTable).Delete
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim request = New HttpRequestMessage With
                {
                    .Method = HttpMethod.Delete,
                    .RequestUri = New Uri(url),
                    .Content = New StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                }

                Dim response As HttpResponseMessage = client.SendAsync(request).Result
                If response.IsSuccessStatusCode Then
                    Return response.Content.ReadAsAsync(Of String)().Result
                Else
                    Dim statusCode As StatusCodeModel = response.Content.ReadAsAsync(Of StatusCodeModel)().Result

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Async Function DeleteAsync(url As String, obj As T) As Task(Of String) Implements IBaseClient(Of T, TTable).DeleteAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim request = New HttpRequestMessage With
                {
                    .Method = HttpMethod.Delete,
                    .RequestUri = New Uri(url),
                    .Content = New StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                }

                Dim response As HttpResponseMessage = Await client.SendAsync(request)
                If response.IsSuccessStatusCode Then
                    Return Await response.Content.ReadAsAsync(Of String)()
                Else
                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Function [Get](url As String, id As String) As T Implements IBaseClient(Of T, TTable).Get
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.GetAsync($"{url}?id={id}").Result
                If response.IsSuccessStatusCode Then
                    Return response.Content.ReadAsAsync(Of T).Result
                Else
                    Dim statusCode As StatusCodeModel = response.Content.ReadAsAsync(Of StatusCodeModel)().Result

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Async Function GetAsync(url As String, id As String) As Task(Of T) Implements IBaseClient(Of T, TTable).GetAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = Await client.GetAsync($"{url}?id={id}")
                If response.IsSuccessStatusCode Then
                    Return Await response.Content.ReadAsAsync(Of T)
                Else
                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Function GetAll(url As String) As ICollection(Of T) Implements IBaseClient(Of T, TTable).GetAll
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.GetAsync(url).Result
                If response.IsSuccessStatusCode Then
                    Return response.Content.ReadAsAsync(Of ICollection(Of T)).Result
                Else
                    Dim statusCode As StatusCodeModel = response.Content.ReadAsAsync(Of StatusCodeModel)().Result

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Async Function GetAllAsync(url As String) As Task(Of ICollection(Of T)) Implements IBaseClient(Of T, TTable).GetAllAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = Await client.GetAsync(url)
                If response.IsSuccessStatusCode Then
                    Return Await response.Content.ReadAsAsync(Of ICollection(Of T))
                Else
                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Function GetTable(url As String) As TTable Implements IBaseClient(Of T, TTable).GetTable
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.GetAsync(url).Result
                If response.IsSuccessStatusCode Then
                    Return response.Content.ReadAsAsync(Of TTable).Result
                Else
                    Dim statusCode As StatusCodeModel = response.Content.ReadAsAsync(Of StatusCodeModel)().Result

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function

        Public Overridable Async Function GetTableAsync(url As String) As Task(Of TTable) Implements IBaseClient(Of T, TTable).GetTableAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = Await client.GetAsync(url)
                If response.IsSuccessStatusCode Then
                    Return Await response.Content.ReadAsAsync(Of TTable)
                Else
                    Dim statusCode As StatusCodeModel = Await response.Content.ReadAsAsync(Of StatusCodeModel)()

                    Throw New ApplicationException(statusCode.Message)
                End If
            End Using
        End Function
    End Class
End Namespace
