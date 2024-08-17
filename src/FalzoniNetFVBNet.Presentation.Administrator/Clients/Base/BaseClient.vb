Imports System.Net.Http
Imports System.Threading.Tasks
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Utils
Imports FalzoniNetFVBNet.Utils.Helpers
Imports Newtonsoft.Json

Namespace Clients.Base
    Public Class BaseClient(Of T As Class)
        Implements IBaseClient(Of T)

        Protected token As String

        Public Sub New()
            token = RequestHelper.GetAccessToken()
        End Sub

        Public Overridable Function Add(url As String, obj As T) As String Implements IBaseClient(Of T).Add
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.PostAsJsonAsync(url, obj).Result
                Dim message = ResponseUtils(Of String).ReturnMessage(response)
                Return message
            End Using
        End Function

        Public Overridable Async Function AddAsync(url As String, obj As T) As Task(Of String) Implements IBaseClient(Of T).AddAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = Await client.PostAsJsonAsync(url, obj)
                Dim message = Await ResponseUtils(Of String).ReturnMessageAsync(response)
                Return message
            End Using
        End Function

        Public Overridable Function Update(url As String, obj As T) As String Implements IBaseClient(Of T).Update
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.PutAsJsonAsync(url, obj).Result
                Dim message = ResponseUtils(Of String).ReturnMessage(response)
                Return message
            End Using
        End Function

        Public Overridable Async Function UpdateAsync(url As String, obj As T) As Task(Of String) Implements IBaseClient(Of T).UpdateAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = Await client.PutAsJsonAsync(url, obj)
                Dim message = Await ResponseUtils(Of String).ReturnMessageAsync(response)
                Return message
            End Using
        End Function

        Public Overridable Function Delete(url As String, obj As T) As String Implements IBaseClient(Of T).Delete
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim request = New HttpRequestMessage With
                {
                    .Method = HttpMethod.Delete,
                    .RequestUri = New Uri(url),
                    .Content = New StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                }

                Dim response As HttpResponseMessage = client.SendAsync(request).Result
                Dim message = ResponseUtils(Of String).ReturnMessage(response)
                Return message
            End Using
        End Function

        Public Overridable Async Function DeleteAsync(url As String, obj As T) As Task(Of String) Implements IBaseClient(Of T).DeleteAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim request = New HttpRequestMessage With
                {
                    .Method = HttpMethod.Delete,
                    .RequestUri = New Uri(url),
                    .Content = New StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                }

                Dim response As HttpResponseMessage = Await client.SendAsync(request)
                Dim message = Await ResponseUtils(Of String).ReturnMessageAsync(response)
                Return message
            End Using
        End Function

        Public Overridable Function [Get](url As String, id As String) As T Implements IBaseClient(Of T).Get
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.GetAsync($"{url}?id={id}").Result
                Dim obj = ResponseUtils(Of T).ReturnMessage(response)
                Return obj
            End Using
        End Function

        Public Overridable Async Function GetAsync(url As String, id As String) As Task(Of T) Implements IBaseClient(Of T).GetAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = Await client.GetAsync($"{url}?id={id}")
                Dim obj = Await ResponseUtils(Of T).ReturnMessageAsync(response)
                Return obj
            End Using
        End Function

        Public Overridable Function GetAll(url As String) As ICollection(Of T) Implements IBaseClient(Of T).GetAll
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = client.GetAsync(url).Result
                Dim list = ResponseUtils(Of ICollection(Of T)).ReturnMessage(response)
                Return list
            End Using
        End Function

        Public Overridable Async Function GetAllAsync(url As String) As Task(Of ICollection(Of T)) Implements IBaseClient(Of T).GetAllAsync
            Using client As New HttpClient
                client.DefaultRequestHeaders.Authorization = New Headers.AuthenticationHeaderValue("Bearer", token)

                Dim response As HttpResponseMessage = Await client.GetAsync(url)
                Dim list = Await ResponseUtils(Of ICollection(Of T)).ReturnMessageAsync(response)
                Return list
            End Using
        End Function
    End Class
End Namespace
