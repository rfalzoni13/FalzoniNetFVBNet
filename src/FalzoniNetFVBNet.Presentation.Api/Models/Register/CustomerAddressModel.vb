Imports FalzoniNetFVBNet.Domain.DTO.Register
Imports FalzoniNetFVBNet.Presentation.Api.Models.Base

Namespace Models.Register
    Public Class CustomerAddressModel
        Inherits BaseModel

        Public Sub New(addressDTO As CustomerAddressDTO)
            Me.Id = addressDTO.Id
            Me.CustomerId = addressDTO.CustomerId
            Me.PostalCode = addressDTO.PostalCode
            Me.AddressName = addressDTO.AddressName
            Me.Number = addressDTO.Number
            Me.Complement = addressDTO.Complement
            Me.Neighborhood = addressDTO.Neighborhood
            Me.City = addressDTO.City
            Me.State = addressDTO.State
            Me.Created = addressDTO.Created
            Me.Modified = addressDTO.Modified
            Me.Removed = addressDTO.Removed
        End Sub

        Public Property CustomerId As Guid

        Public Property PostalCode As String

        Public Property AddressName As String

        Public Property Number As Integer

        Public Property Complement As String

        Public Property Neighborhood As String

        Public Property City As String

        Public Property State As String

        Public Property Removed As Boolean


        Public Function ConvertToDTO() As CustomerAddressDTO
            Return New CustomerAddressDTO With
            {
                .Id = Me.Id,
                .CustomerId = Me.CustomerId,
                .PostalCode = Me.PostalCode,
                .AddressName = Me.AddressName,
                .Number = Me.Number,
                .Complement = Me.Complement,
                .Neighborhood = Me.Neighborhood,
                .City = Me.City,
                .State = Me.State,
                .Created = Me.Created,
                .Modified = Me.Modified,
                .Removed = Me.Removed
            }
        End Function
    End Class
End Namespace
