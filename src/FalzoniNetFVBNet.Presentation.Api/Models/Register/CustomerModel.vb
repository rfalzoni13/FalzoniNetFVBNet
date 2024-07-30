Imports FalzoniNetFVBNet.Domain.DTO.Register
Imports FalzoniNetFVBNet.Presentation.Api.Models.Base

Namespace Models.Register
    Public Class CustomerModel
        Inherits BaseModel

        Public Sub New(customerDTO As CustomerDTO)
            Me.Id = customerDTO.Id
            Me.Name = customerDTO.Name
            Me.DateBirth = customerDTO.DateBirth
            Me.Gender = customerDTO.Gender
            Me.PhoneNumber = customerDTO.PhoneNumber
            Me.CellPhoneNumber = customerDTO.CellPhoneNumber
            Me.Document = customerDTO.Document
            Me.Created = customerDTO.Created
            Me.Modified = customerDTO.Modified
            Me.Addresses = If(customerDTO.Addresses Is Nothing, Nothing, customerDTO.Addresses.ConvertAll(Function(e) New CustomerAddressModel(e)))
        End Sub

        Public Property Name As String
        Public Property DateBirth As Date
        Public Property Gender As String
        Public Property PhoneNumber As String
        Public Property CellPhoneNumber As String
        Public Property Document As String

        Public Overridable Property Addresses As List(Of CustomerAddressModel)

#Region "Methods"
        Public Function ConvertToDTO() As CustomerDTO
            Return New CustomerDTO With
            {
                .Id = Me.Id,
                .Name = Me.Name,
                .DateBirth = Me.DateBirth,
                .Gender = Me.Gender,
                .PhoneNumber = Me.PhoneNumber,
                .CellPhoneNumber = Me.CellPhoneNumber,
                .Document = Me.Document,
                .Created = Me.Created,
                .Modified = Me.Modified,
                .Addresses = If(Me.Addresses Is Nothing, Nothing, Me.Addresses.ToList().ConvertAll(Function(e) New CustomerAddressDTO With
                {
                    .Id = e.Id,
                    .CustomerId = e.CustomerId,
                    .PostalCode = e.PostalCode,
                    .AddressName = e.AddressName,
                    .Number = e.Number,
                    .Complement = e.Complement,
                    .Neighborhood = e.Neighborhood,
                    .City = e.City,
                    .State = e.State,
                    .Created = e.Created,
                    .Modified = e.Modified
                }))
            }
        End Function
#End Region
    End Class
End Namespace
