Namespace Base
    Public MustInherit Class ServiceBase(Of T As Class)
        Public MustOverride Sub Add(ByVal obj As T)

        Public MustOverride Sub Delete(Id As Guid)

        Public MustOverride Sub Update(obj As T)

        Public MustOverride Function [Get](Id As Guid) As T

        Public MustOverride Function GetAll() As IEnumerable(Of T)
    End Class
End Namespace
