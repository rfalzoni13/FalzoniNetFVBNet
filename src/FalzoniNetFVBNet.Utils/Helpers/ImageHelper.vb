Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Namespace Helpers
    Public Class ImageHelper
        Public Shared Sub SaveImageFile(base64String As String, path As String, fileName As String, format As ImageFormat)
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If

            Dim bytes As Byte() = Convert.FromBase64String(base64String)

            Using ms As New MemoryStream(bytes, 0, bytes.Length)
                Dim im As Image = Image.FromStream(ms, True)

                im.Save($"{path}\\{fileName}", format)
            End Using
        End Sub
    End Class
End Namespace
