Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Base

Namespace Models.Tables.Register
    Public Class CustomerTableModel
        Inherits TableBase

        Public Sub New()
            data = New List(Of CustomerListTableModel)()
        End Sub

        Public Overridable Property data As List(Of CustomerListTableModel)
    End Class

    Public Class CustomerListTableModel
        Inherits BaseModel

        Public Property Name As String

        Public Property Gender As String

        Public Property Email As String

        Public Property Document As String
    End Class
End Namespace
