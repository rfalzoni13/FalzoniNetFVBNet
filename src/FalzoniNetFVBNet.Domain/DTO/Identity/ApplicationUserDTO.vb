Imports FalzoniNetFVBNet.Domain.DTO.Base

Namespace DTO.Identity
    Public Class ApplicationUserDTO
        Inherits BaseDTO
        Public Name As String
        Public Email As String
        Public Gender As String
        Public DateBirth As Date
        Public UserName As String
        Public Password As String
        Public PhotoPath As String
        Public PhoneNumber As String
        Public Roles() As String
    End Class
End Namespace
