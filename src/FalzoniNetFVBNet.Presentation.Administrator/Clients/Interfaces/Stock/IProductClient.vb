Imports System.Threading.Tasks
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Stock
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Stock

Namespace Clients.Interfaces.Stock
    Public Interface IProductClient
        Inherits IBaseClient(Of ProductModel)
        Function GetTableAsync() As Task(Of ProductTableModel)
    End Interface
End Namespace
