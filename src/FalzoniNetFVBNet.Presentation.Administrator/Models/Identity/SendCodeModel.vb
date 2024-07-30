Imports System.Web.Mvc

Namespace Models.Identity
    Public Class SendCodeModel
        Public Property Providers As List(Of SelectListItem)
        Public Property ReturnUrl As String
        Public Property RememberMe As Boolean
        Public Property SelectedProvider As String
    End Class
End Namespace
