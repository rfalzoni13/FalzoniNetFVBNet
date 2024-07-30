Imports System.ComponentModel.DataAnnotations
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Base

Namespace Models.Register
    Public Class CustomerAddressModel
        Inherits BaseModel

        Public Property CustomerId As Guid

        <Required(ErrorMessage:="O CEP é obrigatório!")>
        Public Property PostalCode As String

        <Required(ErrorMessage:="O Logradouro é obrigatório!")>
        Public Property AddressName As String

        <Required(ErrorMessage:="O Número é obrigatório!")>
        Public Property Number As Integer

        Public Property Complement As String

        <Required(ErrorMessage:="O Bairro é obrigatório!")>
        Public Property Neighborhood As String

        <Required(ErrorMessage:="A Cidade é obrigatória!")>
        Public Property City As String

        <Required(ErrorMessage:="O Estado é obrigatório!")>
        Public Property State As String


        Public Property Removed As Boolean
    End Class
End Namespace
