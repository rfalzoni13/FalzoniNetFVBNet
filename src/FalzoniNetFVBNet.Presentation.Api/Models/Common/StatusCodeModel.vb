Imports System.Net

Namespace Models.Common
    Public Class StatusCodeModel
        Public Overridable Property Status As HttpStatusCode

        Public Property Message As String

        Public Property ErrorsResult As String()
    End Class
End Namespace
