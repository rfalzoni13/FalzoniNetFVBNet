Imports System.ComponentModel
Imports System.Reflection
Imports System.Text
Imports System.Web

Namespace Helpers
    Public NotInheritable Class StringHelper
        Public Shared Function GetEnumDescription(value As System.Enum)
            Dim fi As FieldInfo = value.GetType().GetField(value.ToString())

            Dim attributes As DescriptionAttribute() = fi.GetCustomAttributes(GetType(DescriptionAttribute), False)

            If attributes IsNot Nothing And attributes.Any() Then
                Return attributes.FirstOrDefault().Description
            End If

            Return value.ToString()
        End Function

        Public Shared Function Base64ForUrlEncode(str As String) As String
            Dim encbuff = Encoding.UTF8.GetBytes(str)
            Return HttpServerUtility.UrlTokenEncode(encbuff)
        End Function

        Public Shared Function Base64ForUrlDecode(str As String) As String
            Dim decbuff = HttpServerUtility.UrlTokenDecode(str)
            Return If(decbuff IsNot Nothing, Encoding.UTF8.GetString(decbuff), Nothing)
        End Function

        Public Shared Function SetDashboardName(name As String) As String
            Return $"{name.Split(" ").FirstOrDefault()} {name.Split(" ").LastOrDefault()}"
        End Function
    End Class
End Namespace
