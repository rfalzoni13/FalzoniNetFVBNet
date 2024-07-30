Imports FalzoniNetFVBNet.Domain.Entities.Register
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Register
Imports FalzoniNetFVBNet.Infra.Data.Repositories.Base

Namespace Repositories.Register
    Public Class ProductCategoryRepository
        Inherits BaseRepository(Of ProductCategory)
        Implements IProductCategoryRepository
    End Class
End Namespace
