Imports FalzoniNetFVBNet.Domain.DTO.Stock
Imports FalzoniNetFVBNet.Presentation.Api.Models.Base
Imports FalzoniNetFVBNet.Presentation.Api.Models.Register

Namespace Models.Stock
    Public Class ProductModel
        Inherits BaseModel

        Public Sub New(productDTO As ProductDTO)
            Me.Id = productDTO.Id
            Me.ProductCategoryId = productDTO.ProductCategoryId
            Me.Name = productDTO.Name
            Me.Code = productDTO.Code
            Me.Description = productDTO.Description
            Me.Price = productDTO.Price
            Me.Created = productDTO.Created
            Me.Modified = productDTO.Modified
        End Sub

        Public Property ProductCategoryId As Guid

        Public Property Name As String

        Public Property Code As Single

        Public Property Description As String

        Public Property Price As Decimal


        Public Overridable Property Category As ProductCategoryModel

#Region "Methods"
        Public Function ConvertToDTO() As ProductDTO
            Return New ProductDTO With
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
