Imports System.Threading.Tasks

Namespace Clients.Interfaces.Base
    Public Interface IBaseClient(Of T As Class)
        Function [Get](id As String) As T

        Function GetAsync(id As String) As Task(Of T)

        Function GetAll() As ICollection(Of T)

        Function GetAllAsync() As Task(Of ICollection(Of T))

        Function Add(obj As T) As String

        Function AddAsync(obj As T) As Task(Of String)

        Function Update(obj As T) As String

        Function UpdateAsync(obj As T) As Task(Of String)

        Function Delete(obj As T) As String

        Function DeleteAsync(obj As T) As Task(Of String)
    End Interface
End Namespace
