Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Utils.Helpers

Namespace Models.Configuration
    Public Class UserModel
        Inherits BaseModel

        <Required(ErrorMessage:="O nome é obrigatório")>
        Public Property Name As String

        <DisplayName("E-mail")>
        <Required(ErrorMessage:="O e-mail é obrigatório")>
        Public Property Email As String

        <DisplayName("Gênero")>
        <Required(ErrorMessage:="O gênero é obrigatório")>
        Public Property Gender As String

        <DisplayName("Data de nascimento")>
        Public Property DateBirth As Date

        Public Property UserName As String

        Public Property PhoneNumber As String

        Public Property PhotoPath As String

        Public Property Roles As String()

        Public Overridable Property File As FileModel

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

        'Methods
        Public Sub LoadProfilePhoto()
            Me.PhotoPath = If(Not String.IsNullOrEmpty(PhotoPath), $"{PathUtils.GetApiPath()}/{PhotoPath}", Me.PhotoPath)
        End Sub
    End Class
End Namespace
