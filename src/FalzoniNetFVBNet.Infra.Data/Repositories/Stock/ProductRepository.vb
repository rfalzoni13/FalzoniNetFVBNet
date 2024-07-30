Imports FalzoniNetFVBNet.Domain.Entities.Stock
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Stock
Imports FalzoniNetFVBNet.Infra.Data.Repositories.Base

Namespace Repositories.Stock
    Public Class ProductRepository
        Inherits BaseRepository(Of Product)
        Implements IProductRepository
    End Class
End Namespace
