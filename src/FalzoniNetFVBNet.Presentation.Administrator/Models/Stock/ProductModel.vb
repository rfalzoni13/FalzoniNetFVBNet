Imports System.ComponentModel.DataAnnotations
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Register

Namespace Models.Stock
    Public Class ProductModel
        Inherits BaseModel

        <Required(ErrorMessage:="A categoria do produto é obrigatória")>
        Public Property ProductCategoryId As Guid

        <Required(ErrorMessage:="O nome do produto é obrigatório")>
        Public Property Name As String

        Public Property Code As Single

        <Required(ErrorMessage:="A descrição do produto é obrigatório")>
        Public Property Description As String

        <Required(ErrorMessage:="O preço do produto é obrigatório")>
        <Range(1, Double.MaxValue, ErrorMessage:="O valor mínimo do preço deve ser acima de zero")>
        Public Property Price As Decimal

        Public Overridable Property Category As ProductCategoryModel
    End Class
End Namespace
