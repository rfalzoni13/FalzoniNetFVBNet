Imports FalzoniNetFVBNet.Domain.DTO.Register
Imports FalzoniNetFVBNet.Domain.DTO.Stock
Imports FalzoniNetFVBNet.Presentation.Api.Models.Base
Imports FalzoniNetFVBNet.Presentation.Api.Models.Stock

Namespace Models.Register
    Public Class ProductCategoryModel
        Inherits BaseModel

        Public Sub New(categoryDTO As ProductCategoryDTO)
            Me.Id = categoryDTO.Id
            Me.Name = categoryDTO.Name
            Me.Created = categoryDTO.Created
            Me.Modified = categoryDTO.Modified
            Me.Product = If(categoryDTO.Product Is Nothing, Nothing,
            New ProductModel(categoryDTO.Product))
        End Sub

        Public Property Name As String

        Public Property Product As ProductModel

#Region "Methods"
        Public Function ConvertToDTO() As ProductCategoryDTO
            Return New ProductCategoryDTO With
            {
                .Id = Me.Id,
                .Name = Me.Name,
                .Created = Me.Created,
                .Modified = Me.Modified,
                .Product = If(Me.Product Is Nothing, Nothing,
                New ProductDTO With
                {
                    .Id = Me.Product.Id,
                    .ProductCategoryId = Me.Product.ProductCategoryId,
                    .Name = Me.Product.Name,
                    .Code = Me.Product.Code,
                    .Description = Me.Product.Description,
                    .Price = Me.Product.Price,
                    .Created = Me.Product.Created,
                    .Modified = Me.Product.Modified
                })
            }
        End Function
#End Region
    End Class
End Namespace
