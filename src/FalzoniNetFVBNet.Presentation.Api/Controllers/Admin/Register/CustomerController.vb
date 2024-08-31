Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports FalzoniNetFVBNet.Domain.DTO.Register
Imports FalzoniNetFVBNet.Presentation.Api.Attributes
Imports FalzoniNetFVBNet.Presentation.Api.Controllers.Base
Imports FalzoniNetFVBNet.Presentation.Api.Utils
Imports FalzoniNetFVBNet.Service.Register

Namespace Controllers.Admin.Register
    <CustomAuthorize(Roles:="Administrator")>
    Public Class CustomerController
        Inherits BaseController(Of CustomerDTO)

#Region "Constructor"
        Public Sub New(customerService As CustomerService)
            MyBase.New(customerService)
        End Sub
#End Region

#Region "Getters"
        ' GET Api/Customer/GetAll
        ''' <summary>
        ''' Listar todos os clientes
        ''' </summary>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Listagem de todos os clientes</remarks>
        ''' <returns></returns>
        <HttpGet>
        Public Overrides Function GetAll() As HttpResponseMessage
            Return MyBase.GetAll()
        End Function

        ' GET Api/Customer/Get?id={Id}
        ''' <summary>
        ''' Listar cliente pelo Id
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Retorna o cliente através do Id do mesmo</remarks>
        ''' <param name="Id">Id do cliente</param>
        ''' <returns></returns>
        <HttpGet>
        Public Overrides Function [Get](Id As Guid) As HttpResponseMessage
            Return MyBase.[Get](Id)
        End Function
#End Region

#Region "Add Customer"
        ' POST Api/Customer/Add
        ''' <summary>
        ''' Inserir cliente
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Insere um novo cliente passando um objeto no body da requisição no método POST</remarks>
        ''' <param name="dto">Objeto de registro cliente</param>
        ''' <returns></returns>
        <HttpPost>
        Public Overrides Function Add(<FromBody> dto As CustomerDTO) As HttpResponseMessage
            Return MyBase.Add(dto)
        End Function
#End Region

#Region "Update Customer"
        'PUT Api/Customer/Update
        ''' <summary>
        ''' Atualizar cliente
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Atualiza o cliente passando o objeto no body da requisição pelo método PUT</remarks>
        ''' <param name="dto">Objeto de registro do cliente</param>
        ''' <returns></returns>
        <HttpPut>
        Public Overrides Function Update(<FromBody> dto As CustomerDTO) As HttpResponseMessage
            Return MyBase.Update(dto)
        End Function
#End Region

#Region "Delete Customer"
        ' DELETE Api/Customer/Delete
        ''' <summary>
        ''' Excluir cliente
        ''' </summary>
        ''' <response code="400">Bad Request</response>
        ''' <response code="401">Unauthorized</response>
        ''' <response code="500">Internal Server Error</response>
        ''' <remarks>Exclui o cliente passando o objeto no body da requisição pelo método DELETE</remarks>
        ''' <param name="Id">Id de registro do cliente</param>
        ''' <returns></returns>
        <HttpDelete>
        Public Overrides Function Delete(<FromUri> Id As Guid) As HttpResponseMessage
            Return MyBase.Delete(Id)
        End Function
#End Region
    End Class
End Namespace