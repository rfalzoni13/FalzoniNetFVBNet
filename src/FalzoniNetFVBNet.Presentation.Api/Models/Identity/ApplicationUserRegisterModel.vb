Imports FalzoniNetFVBNet.Domain.DTO.Identity
Imports FalzoniNetFVBNet.Presentation.Api.Models.Common

Namespace Models.Identity
    Public Class ApplicationUserRegisterModel
        Public Property ID As String
        Public Property Name As String
        Public Property Email As String
        Public Property UserName As String
        Public Property PhoneNumber As String
        Public Property Gender As String
        Public Property DateBirth As Date
        Public Property AcceptTerms As Boolean
        Public Property Roles As String()

        Public Property File As FileModel

        Public Function ConvertToDTO() As ApplicationUserRegisterDTO
            Return New ApplicationUserRegisterDTO With
            {
                .ID = Me.ID,
                .Name = Me.Name,
                .Email = Me.Email,
                .UserName = Me.UserName,
                .PhoneNumber = Me.PhoneNumber,
                .Gender = Me.Gender,
                .DateBirth = Me.DateBirth.Date,
                .Roles = Me.Roles,
                .AcceptTerms = Me.AcceptTerms,
                .File = If(Me.File Is Nothing, Nothing, New Domain.DTO.Common.FileDTO With
                {
                    .FileName = Me.File.FileName,
                    .Base64String = Me.File.Base64String,
                    .Format = Me.File.Format
                })
            }
        End Function
    End Class
End Namespace
