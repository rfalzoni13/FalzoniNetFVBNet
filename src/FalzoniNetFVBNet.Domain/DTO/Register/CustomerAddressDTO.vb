Imports FalzoniNetFVBNet.Domain.DTO.Base
Imports FalzoniNetFVBNet.Domain.Entities.Register

Namespace DTO.Register
    Public Class CustomerAddressDTO
        Inherits BaseDTO

        Public Sub New()
        End Sub

        Public Sub New(address As CustomerAddress)
            Me.Id = address.Id
            Me.CustomerId = address.CustomerId
            Me.PostalCode = address.PostalCode
            Me.AddressName = address.AddressName
            Me.Number = address.Number
            Me.Complement = address.Complement
            Me.Neighborhood = address.Neighborhood
            Me.City = address.City
            Me.State = address.State
            Me.Created = address.Created
            Me.Modified = address.Modified
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

#Region "Methods"
        Public Sub ConfigureNewEntity()
            Id = Guid.NewGuid()
        End Sub

        Public Function ConvertToEntity() As CustomerAddress

            Return New CustomerAddress With
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
            .Modified = Me.Modified
        }
        End Function
#End Region
    End Class
End Namespace
