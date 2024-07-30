Imports FalzoniNetFVBNet.Domain.DTO.Base
Imports FalzoniNetFVBNet.Domain.DTO.Stock
Imports FalzoniNetFVBNet.Domain.Entities.Register

Namespace DTO.Register
    Public Class ProductCategoryDTO
        Inherits BaseDTO

        Public Sub New()
        End Sub

        Public Sub New(category As ProductCategory)
            Me.Id = category.Id
            Me.Name = category.Name
            Me.Created = category.Created
            Me.Modified = category.Modified
            Me.Product = If(category.Product Is Nothing, Nothing, New ProductDTO(category.Product))
        End Sub

        Public Property Name As String

        Public Overridable Property Product As ProductDTO

#Region "Methods"
        Public Sub ConfigureNewEntity()
            Me.Id = Guid.NewGuid()
        End Sub

        Public Function ConvertToEntity() As ProductCategory
            Return New ProductCategory With
        {
            .Id = Me.Id,
            .Name = Me.Name,
            .Created = Me.Created,
            .Modified = Me.Modified
        }
        End Function
#End Region
    End Class
End Namespace
