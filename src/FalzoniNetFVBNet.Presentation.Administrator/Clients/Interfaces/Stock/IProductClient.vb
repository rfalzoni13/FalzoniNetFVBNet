Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Stock
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Stock

Namespace Clients.Interfaces.Stock
    Public Interface IProductClient
        Inherits IBaseClient(Of ProductModel, ProductTableModel)
    End Interface
End Namespace
