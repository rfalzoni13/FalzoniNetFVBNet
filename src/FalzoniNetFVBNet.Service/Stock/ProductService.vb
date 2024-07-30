Imports FalzoniNetFVBNet.Domain.DTO.Stock
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Base
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Register
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Stock

Namespace Stock
    Public Class ProductService
        Private ReadOnly _productRepository As IProductRepository
        Private ReadOnly _productCategoryRepository As IProductCategoryRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Public Sub New(productRepository As IProductRepository,
        productCategoryRepository As IProductCategoryRepository,
        unitOfWork As IUnitOfWork)

            _productRepository = productRepository
            '_productCategoryRepository = productCategoryRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function [Get](Id As Guid) As ProductDTO
            Dim product = _productRepository.Get(Id)
            Return New ProductDTO(product)
        End Function

        Public Function GetAll() As List(Of ProductDTO)
            Dim products = _productRepository.GetAll()
            Return products.ToList().ConvertAll(Function(c) New ProductDTO(c))
        End Function

        Public Sub Add(productDTO As ProductDTO)
            Using transaction = _unitOfWork.BeginTransaction()
                Try
                    productDTO.ConfigureNewEntity()

                    Dim product = productDTO.ConvertToEntity()

                    _productRepository.Add(product)

                    transaction.Commit()
                Catch ex As Exception
                    transaction.Rollback()
                    Throw ex
                End Try
            End Using
        End Sub

        Public Sub Update(productDTO As ProductDTO)
            Using transaction = _unitOfWork.BeginTransaction()
                Try
                    Dim product = _productRepository.Get(productDTO.Id)

                    'Update principal data
                    product.Name = productDTO.Name
                    product.Description = productDTO.Description
                    product.Price = productDTO.Price

                    'Update modified entity data
                    product.Modified = Date.Now

                    _productRepository.Update(product)

                    transaction.Commit()
                Catch ex As Exception
                    transaction.Rollback()
                    Throw ex
                End Try
            End Using
        End Sub

        Public Sub Delete(productDTO As ProductDTO)
            Using transaction = _unitOfWork.BeginTransaction()
                Try

                    _productRepository.Delete(productDTO.Id)

                    transaction.Commit()
                Catch ex As Exception
                    transaction.Rollback()
                    Throw ex
                End Try
            End Using
        End Sub
    End Class
End Namespace
