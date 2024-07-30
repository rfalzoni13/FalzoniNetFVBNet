Imports FalzoniNetFVBNet.Domain.Entities.Base

Namespace Entities.Register
    Public Class Customer
        Inherits BaseEntity

        Public Property Name As String
        Public Property DateBirth As Date
        Public Property Gender As String
        Public Property Email As String
        Public Property PhoneNumber As String
        Public Property CellPhoneNumber As String
        Public Property Document As String

        Public Overridable Property Addresses As ICollection(Of CustomerAddress)
    End Class
End Namespace

