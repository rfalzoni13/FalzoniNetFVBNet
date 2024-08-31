Imports System.Net.Http
Imports System.Web.Http
Imports FalzoniNetFVBNet.Domain.DTO.Stock
Imports FalzoniNetFVBNet.Presentation.Api.Attributes
Imports FalzoniNetFVBNet.Presentation.Api.Controllers.Base
Imports FalzoniNetFVBNet.Service.Stock

Namespace Controllers.Admin.Stock
    <CustomAuthorize(Roles:="Administrator")>
    Public Class ProductController
        Inherits BaseController(Of ProductDTO)

#Region "Constructor"
        Public Sub New(productService As ProductService)
            MyBase.New(productService)
        End Sub
#End Region

#Region "Getters"
        ' GET Api/Product/GetAll
        ''' <summary>
        ''' Listar todos os produtos
        ''' </summary>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Listagem de todos os produtos</remarks>
        ''' <returns></returns>
        <HttpGet>
        Public Overrides Function GetAll() As HttpResponseMessage
            Return MyBase.GetAll()
        End Function

        ' GET Api/Product/Get?id={Id}
        ''' <summary>
        ''' Listar produto pelo Id
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Retorna o produto através do Id do mesmo</remarks>
        ''' <param name="Id">Id do produto</param>
        ''' <returns></returns>
        <HttpGet>
        Public Overrides Function [Get](Id As Guid) As HttpResponseMessage
            Return MyBase.[Get](Id)
        End Function
#End Region

#Region "Add Product"
        ' POST Api/Product/Add
        ''' <summary>
        ''' Inserir produto
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Insere um novo produto passando um objeto no body da requisição no método POST</remarks>
        ''' <param name="dto">Objeto de registro produto</param>
        ''' <returns></returns>
        <HttpPost>
        Public Overrides Function Add(<FromBody> dto As ProductDTO) As HttpResponseMessage
            Return MyBase.Add(dto)
        End Function
#End Region

#Region "Update Product"
        'PUT Api/Product/Update
        ''' <summary>
        ''' Atualizar produto
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Atualiza o produto passando o objeto no body da requisição pelo método PUT</remarks>
        ''' <param name="dto">Objeto de registro produto</param>
        ''' <returns></returns>
        <HttpPut>
        Public Overrides Function Update(<FromBody> dto As ProductDTO) As HttpResponseMessage
            Return MyBase.Update(dto)
        End Function
#End Region

#Region "Delete Product"
        ' DELETE Api/Product/Delete
        ''' <summary>
        ''' Excluir produto
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Exclui o produto passando o objeto no body da requisição pelo método DELETE</remarks>
        ''' <param name="Id">Id de identificação do produto</param>
        ''' <returns></returns>
        <HttpDelete>
        <Route("Delete")>
        Public Overrides Function Delete(<FromUri> Id As Guid) As HttpResponseMessage
            Return MyBase.Delete(Id)
        End Function
#End Region
    End Class
End Namespace