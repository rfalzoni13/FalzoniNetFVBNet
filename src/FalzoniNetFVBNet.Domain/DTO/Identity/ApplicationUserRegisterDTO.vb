Imports FalzoniNetFVBNet.Domain.DTO.Common

Namespace DTO.Identity
    Public Class ApplicationUserRegisterDTO
        Public Property ID As String
        Public Property Name As String
        Public Property Email As String
        Public Property UserName As String
        Public Property Password As String
        Public Property Gender As String
        Public Property PhoneNumber As String
        Public Property DateBirth As Date
        Public Property AcceptTerms As Boolean
        Public Property Roles As String()
        Public Overridable Property File As FileDTO
    End Class
End Namespace
