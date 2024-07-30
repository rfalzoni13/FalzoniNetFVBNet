Imports System.Net.Http
Imports System.Threading.Tasks
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Register
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Register
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Register
Imports FalzoniNetFVBNet.Utils.Helpers

Namespace Clients.Register
    Public Class CustomerClient
        Inherits BaseClient(Of CustomerModel, CustomerTableModel)
        Implements ICustomerClient

        Public Overrides Async Function GetTableAsync(url As String) As Task(Of CustomerTableModel)
            Dim table = New CustomerTableModel()

            Try
                Using client As New HttpClient()
                    client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token)

                    Dim response As HttpResponseMessage = Await client.GetAsync(url)
                    If response.IsSuccessStatusCode Then
                        Dim customers = Await response.Content.ReadAsAsync(Of ICollection(Of CustomerModel))()

                        For Each customer In customers
                            table.data.Add(New CustomerListTableModel With
                        {
                            .Id = customer.Id,
                            .Name = customer.Name,
                            .Email = customer.Email,
                            .Document = customer.Document,
                            .Gender = customer.Gender,
                            .Created = customer.Created,
                            .Modified = customer.Modified
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
    End Class
End Namespace
