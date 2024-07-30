Imports System.IO
Imports System.Web

Namespace Helpers
    Public Class FileHelper
        Public Shared Sub DeleteFile(path As String)
            If File.Exists(path) Then
                File.Delete(path)
            End If
        End Sub

        Public Shared Function ConvertArrayBytesToStream(bytes As Byte()) As MemoryStream
            Dim base64String As String = Convert.ToBase64String(bytes)
            Return New MemoryStream(System.Text.Encoding.UTF8.GetBytes(base64String))
        End Function

        Public Shared Function ConvertBase64StringToStream(base64String As String) As MemoryStream
            Return New MemoryStream(System.Text.Encoding.UTF8.GetBytes(base64String))
        End Function

        Public Shared Function ConvertStreamToArrayBytes(file As HttpPostedFileBase) As Byte()
            Dim br = New BinaryReader(file.InputStream)
            Return br.ReadBytes(file.ContentLength)
        End Function

        Public Shared Function ConvertArrayBytesToBase64String(bytes As Byte()) As String
            Return Convert.ToBase64String(bytes)
        End Function

        Public Shared Function ConvertStreamToBase64String(file As HttpPostedFileBase) As String
            Dim br = New BinaryReader(file.InputStream)
            Dim bytes As Byte() = br.ReadBytes(file.ContentLength)
            Return Convert.ToBase64String(bytes)
        End Function
    End Class
End Namespace
