Imports FalzoniNetFVBNet.Domain.DTO.Base
Imports FalzoniNetFVBNet.Domain.Entities.Register

Namespace DTO.Register
    Public Class CustomerDTO
        Inherits BaseDTO

        Public Sub New()
        End Sub

        Public Sub New(customer As Customer)
            Me.Id = customer.Id
            Me.Name = customer.Name
            Me.DateBirth = customer.DateBirth
            Me.Gender = customer.Gender
            Me.Email = customer.Email
            Me.PhoneNumber = customer.PhoneNumber
            Me.CellPhoneNumber = customer.CellPhoneNumber
            Me.Document = customer.Document
            Me.Created = customer.Created
            Me.Modified = customer.Modified
            Me.Addresses = If(customer.Addresses Is Nothing,
            Nothing, customer.Addresses.ToList().ConvertAll(Function(e) New CustomerAddressDTO(e)))

        End Sub

        Public Property Name As String
        Public Property DateBirth As Date
        Public Property Gender As String
        Public Property Email As String
        Public Property PhoneNumber As String
        Public Property CellPhoneNumber As String
        Public Property Document As String

        Public Overridable Property Addresses As List(Of CustomerAddressDTO)

#Region "Methods"
        Public Sub ConfigureNewEntity()
            Me.Id = Guid.NewGuid()
            Me.Created = Date.Now
            If Addresses Is Nothing And Addresses.Count > 0 Then
                Addresses.ForEach(
                Function(e)
                    Return e.Id
                    e.CustomerId = Id
                    e.Created = Date.Now
                End Function)
            End If

        End Sub

        Public Function ConvertToEntity() As Customer
            Return New Customer With
        {
            .Id = Me.Id,
            .Name = Me.Name,
            .DateBirth = Me.DateBirth,
            .Gender = Me.Gender,
            .PhoneNumber = Me.PhoneNumber,
            .CellPhoneNumber = Me.CellPhoneNumber,
            .Document = Me.Document,
            .Email = Me.Email,
            .Created = Me.Created,
            .Modified = Me.Modified,
            .Addresses = If(Me.Addresses Is Nothing,
            Nothing, Me.Addresses.ToList().ConvertAll(Function(e) New CustomerAddress With
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
            }
            ))
        }
        End Function
#End Region


    End Class
End Namespace
