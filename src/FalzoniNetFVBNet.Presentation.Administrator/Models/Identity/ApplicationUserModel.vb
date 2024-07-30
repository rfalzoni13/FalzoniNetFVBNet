Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Common
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Configuration

Namespace Models.Identity
    Public Class ApplicationUserModel
        Public Sub New(user As UserModel)
            Me.ID = If(user.Id = Guid.Empty, Nothing, user.Id.ToString())
            Me.Name = user.Name
            Me.Email = user.Email
            Me.UserName = user.UserName
            Me.Gender = user.Gender
            Me.PhoneNumber = user.PhoneNumber
            Me.DateBirth = user.DateBirth
            Me.Roles = user.Roles
            Me.AcceptTerms = False
            Me.File = If(user.File Is Nothing, Nothing, New FileModel With
            {
                .FileName = user.File.FileName,
                .Base64String = user.File.Base64String
            })

            If File IsNot Nothing Then
                File.RemoveHeaderBase64()
            End If
        End Sub

        Public Property ID As String
        Public Property Name As String
        Public Property Email As String
        Public Property UserName As String
        Public Property Gender As String
        Public Property PhoneNumber As String
        Public Property DateBirth As Date
        Public Property AcceptTerms As Boolean
        Public Property Roles As String()

        Public Overridable Property File As FileModel
    End Class
End Namespace
