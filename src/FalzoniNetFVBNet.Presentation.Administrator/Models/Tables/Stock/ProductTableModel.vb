Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Models.Tables.Base

Namespace Models.Tables.Stock
    Public Class ProductTableModel
        Inherits TableBase

        Public Sub New()
            data = New List(Of ProductListTableModel)()
        End Sub

        Public Overridable Property data As List(Of ProductListTableModel)
    End Class

    Public Class ProductListTableModel
        Inherits BaseModel

        Public Property Name As String
        Public Property Price As Decimal
        Public Property Category As String
        Public Property Code As Single
    End Class
End Namespace
