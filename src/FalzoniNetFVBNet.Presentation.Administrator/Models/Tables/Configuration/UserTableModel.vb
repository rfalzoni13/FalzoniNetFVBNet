Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Base

Namespace Models.Tables.Configuration
    Public Class UserTableModel
        Inherits TableBase
        Public Sub New()
            data = New List(Of UserListTableModel)()
        End Sub

        Public Overridable Property data As List(Of UserListTableModel)
    End Class

    Public Class UserListTableModel
        Inherits BaseModel

        Public Property Name As String

        Public Property Gender As String

        Public Property Email As String

        Public Property UserName As String
    End Class
End Namespace
