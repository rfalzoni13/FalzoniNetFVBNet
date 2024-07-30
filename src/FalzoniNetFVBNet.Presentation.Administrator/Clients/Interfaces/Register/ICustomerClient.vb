Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Register
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Register

Namespace Clients.Interfaces.Register
    Public Interface ICustomerClient
        Inherits IBaseClient(Of CustomerModel, CustomerTableModel)
    End Interface
End Namespace
