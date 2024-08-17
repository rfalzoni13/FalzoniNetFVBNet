Imports System.Net.Http
Imports System.Threading.Tasks
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Stock
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Stock
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Stock
Imports FalzoniNetFVBNet.Utils.Helpers

Namespace Clients.Stock
    Public Class ProductClient
        Inherits BaseClient(Of ProductModel)
        Implements IProductClient

        Public Async Function GetTableAsync(url As String) As Task(Of ProductTableModel) Implements IProductClient.GetTableAsync
            Dim table = New ProductTableModel()

            Try
                Using client As New HttpClient()
                    client.DefaultRequestHeaders.Authorization = New System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token)

                    Dim response As HttpResponseMessage = Await client.GetAsync(url)
                    If response.IsSuccessStatusCode Then
                        Dim products = Await response.Content.ReadAsAsync(Of ICollection(Of ProductModel))()

                        For Each product In products
                            table.data.Add(New ProductListTableModel With
                        {
                            .Id = product.Id,
                            .Name = product.Name,
                            .Category = product.Category.Name,
                            .Price = product.Price,
                            .Code = product.Code,
                            .Created = product.Created,
                            .Modified = product.Modified
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
