Imports FalzoniNetFVBNet.Domain.DTO.Base
Imports FalzoniNetFVBNet.Domain.DTO.Register
Imports FalzoniNetFVBNet.Domain.Entities.Stock

Namespace DTO.Stock
    Public Class ProductDTO
        Inherits BaseDTO

        Public Sub New()
        End Sub

        Public Sub New(product As Product)
            Me.Id = product.Id
            Me.ProductCategoryId = product.ProductCategoryId
            Me.Name = product.Name
            Me.Code = product.Code
            Me.Description = product.Description
            Me.Price = product.Price
            Me.Created = product.Created
            Me.Modified = product.Modified
        End Sub

        Public Property ProductCategoryId As Guid

        Public Property Name As String

        Public Property Code As Single

        Public Property Description As String

        Public Property Price As Decimal


        Public Overridable Property Category As ProductCategoryDTO


#Region "Methods"
        Public Sub ConfigureNewEntity()
            Me.Id = Guid.NewGuid()
        End Sub

        Public Function ConvertToEntity() As Product
            Return New Product With
        {
            .Id = Me.Id,
            .ProductCategoryId = Me.ProductCategoryId,
            .Name = Me.Name,
            .Code = Me.Code,
            .Description = Me.Description,
            .Price = Me.Price,
            .Created = Me.Created,
            .Modified = Me.Modified
        }
        End Function
#End Region
    End Class
End Namespace
