Imports System
Imports FalzoniNetFVBNet.Domain.Entities.Register
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Base

Namespace Interfaces.Repositories.Register
    Public Interface ICustomerRepository
        Inherits IBaseRepository(Of Customer)

        Function GetWithInclude(Id As Guid) As Customer
    End Interface
End Namespace
