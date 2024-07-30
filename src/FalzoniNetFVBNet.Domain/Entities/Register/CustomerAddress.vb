Imports FalzoniNetFVBNet.Domain.Entities.Base

Namespace Entities.Register
    Public Class CustomerAddress
        Inherits BaseEntity

        Public Property CustomerId As Guid
        Public Property PostalCode As String
        Public Property AddressName As String
        Public Property Number As Integer
        Public Property Complement As String
        Public Property Neighborhood As String
        Public Property City As String
        Public Property State As String

        Public Overridable Property Customer As Customer
    End Class
End Namespace
