Imports FalzoniNetFVBNet.Domain.Entities.Register
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Base

Namespace Interfaces.Repositories.Register
    Public Interface ICustomerAddressRepository
        Inherits IBaseRepository(Of CustomerAddress)

        Sub RemoveRange(ids As ICollection(Of Guid))
    End Interface
End Namespace
