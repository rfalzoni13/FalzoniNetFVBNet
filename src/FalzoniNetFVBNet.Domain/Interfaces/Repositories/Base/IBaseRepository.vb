Namespace Interfaces.Repositories.Base
    Public Interface IBaseRepository(Of T As Class)
        Sub Add(obj As T)

        Sub Update(obj As T)

        Sub Delete(Id As Guid)

        Function [Get](Id As Guid) As T

        Function GetAll() As IEnumerable(Of T)
    End Interface
End Namespace
