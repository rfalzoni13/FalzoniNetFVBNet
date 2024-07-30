Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Base

Namespace Models.Register
    Public Class CustomerModel
        Inherits BaseModel
        Public Sub New()
            Addresses = New List(Of CustomerAddressModel)()
        End Sub

        <Required(ErrorMessage:="O nome do cliente é obrigatório")>
        Public Property Name As String

        <Required(ErrorMessage:="A data de nascimento é obrigatória")>
        <DisplayName("Data de nascimento")>
        Public Property DateBirth As Date

        <Required(ErrorMessage:="Favor informar o gênero do cliente")>
        <DisplayName("Gênero")>
        Public Property Gender As String

        <Required(ErrorMessage:="O e-mail do cliente é obrigatório")>
        <EmailAddress(ErrorMessage:="Formato de E-mail inválido!")>
        <DisplayName("E-mail")>
        Public Property Email As String

        Public Property PhoneNumber As String

        <Required(ErrorMessage:="O celular do cliente é obrigatório")>
        Public Property CellPhoneNumber As String

        <Required(ErrorMessage:="O documento do cliente é obrigatório")>
        Public Property Document As String


        Public Overridable Property Addresses As List(Of CustomerAddressModel)

        Public Overridable ReadOnly Property Genders As List(Of SelectListItem)
            Get
                Return New List(Of SelectListItem) From
                {
                    New SelectListItem With
                    {
                        .Text = "Masculino",
                        .Value = "Masculino"
                    },
                    New SelectListItem With
                    {
                        .Text = "Feminino",
                        .Value = "Feminino"
                    }
                }
            End Get
        End Property
    End Class
End Namespace
