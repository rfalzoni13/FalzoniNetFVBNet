Imports System.Security.Cryptography
Imports System.Web

Namespace Helpers
    Public NotInheritable Class RandomOAuthStateGeneratorHelper
        Public Shared Property _random As RandomNumberGenerator = New RNGCryptoServiceProvider()

        Public Shared Function Generate(strengthInBits As Integer) As String
            Const bitsPerByte As Integer = 8

            If strengthInBits Mod bitsPerByte <> 0 Then
                Throw New ArgumentException("strengthInBits deve ser divisível por 8.", "strengthInBits")
            End If

            Dim strengthInBytes As Integer = strengthInBits / bitsPerByte

            Dim data As Byte() = New Byte() {strengthInBytes}
            _random.GetBytes(data)
            Return HttpServerUtility.UrlTokenEncode(data)
        End Function
    End Class
End Namespace
