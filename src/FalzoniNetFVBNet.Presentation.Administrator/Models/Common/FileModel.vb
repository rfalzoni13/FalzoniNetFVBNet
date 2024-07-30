Imports System.Drawing.Imaging

Namespace Models.Common
    Public Class FileModel
        Public Property FileName As String

        Public Property Base64String As String

        Public Formato As ImageFormat

        Public Sub RemoveHeaderBase64()
            Dim mimeType = Base64String.Split(",").FirstOrDefault()

            Select Case mimeType
                Case "data:image/jpeg;base64"
                    Me.Formato = ImageFormat.Jpeg
                    Exit Select

                Case "data:image/png;base64"
                    Me.Formato = ImageFormat.Png
                    Exit Select
            End Select

            Me.Base64String = System.Text.RegularExpressions.Regex.Replace(Base64String, "^data:image\/[a-zA-Z]+;base64,", String.Empty)
        End Sub
    End Class
End Namespace
