Imports System.Threading.Tasks

Namespace Clients.Interfaces.Base
    Public Interface IBaseClient(Of T As Class, TTable As Class)
        Function [Get](url As String, id As String) As T

        Function GetAsync(url As String, id As String) As Task(Of T)

        Function GetAll(url As String) As ICollection(Of T)

        Function GetAllAsync(url As String) As Task(Of ICollection(Of T))

        Function GetTable(url As String) As TTable

        Function GetTableAsync(url As String) As Task(Of TTable)

        Function Add(url As String, obj As T) As String

        Function AddAsync(url As String, obj As T) As Task(Of String)

        Function Update(url As String, obj As T) As String

        Function UpdateAsync(url As String, obj As T) As Task(Of String)

        Function Delete(url As String, obj As T) As String

        Function DeleteAsync(url As String, obj As T) As Task(Of String)
    End Interface
End Namespace
