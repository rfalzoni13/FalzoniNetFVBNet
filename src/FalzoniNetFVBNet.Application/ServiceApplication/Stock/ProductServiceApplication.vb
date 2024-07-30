Imports FalzoniNetFVBNet.Domain.DTO.Stock
Imports FalzoniNetFVBNet.Service.Stock

Namespace ServiceApplication.Stock
    Public Class ProductServiceApplication
        Private ReadOnly _productService As ProductService

        Public Sub New(productService As ProductService)
            _productService = productService
        End Sub

        Public Function [Get](Id As Guid) As ProductDTO
            If Id = Guid.Empty Then
                Throw New ApplicationException("Erro ao buscar usuário!")
            End If

            Return _productService.Get(Id)
        End Function

        Public Function GetAll() As List(Of ProductDTO)
            Return _productService.GetAll()
        End Function

        Public Sub Add(productDTO As ProductDTO)
            Validate(productDTO)

            _productService.Add(productDTO)
        End Sub

        Public Sub Update(productDTO As ProductDTO)
            Validate(productDTO)

            _productService.Update(productDTO)
        End Sub

        Public Sub Delete(productDTO As ProductDTO)
            If productDTO.Id = Guid.Empty Then
                Throw New ApplicationException("Erro ao buscar usuário!")
            End If

            _productService.Delete(productDTO)
        End Sub

        'Private METHODS
        Public Sub Validate(productDTO As ProductDTO)
            If String.IsNullOrEmpty(productDTO.Name) Then
                Throw New ApplicationException("Necessário informar o nome do Produto")
            End If

            If productDTO.Code <= 0 Then
                Throw New ApplicationException("Necessário informar o código do Produto")
            End If

            If String.IsNullOrEmpty(productDTO.Description) Then
                Throw New ApplicationException("Necessário informar a descrição do Produto")
            End If

            If productDTO.Price <= 0 Then
                Throw New ApplicationException("Necessário estabelecer o preço do produto")
            End If

            If productDTO.ProductCategoryId = Guid.Empty Then
                Throw New ApplicationException("Necessário informar a categoria do produto")
            End If
        End Sub
    End Class
End Namespace
