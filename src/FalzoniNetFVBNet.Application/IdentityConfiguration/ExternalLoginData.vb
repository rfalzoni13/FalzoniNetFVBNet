Imports System.Security.Claims
Imports System.Security.Cryptography
Imports System.Web
Imports Microsoft.AspNet.Identity

Namespace IdentityConfiguration
    Public Class ExternalLoginData
        Public Property LoginProvider As String
        Public Property ProviderKey As String
        Public Property UserName As String

        Public Function GetClaims() As IList(Of Claim)
            Dim claims As IList(Of Claim) = New List(Of Claim)()
            claims.Add(New Claim(ClaimTypes.NameIdentifier, ProviderKey, Nothing, LoginProvider))

            If UserName IsNot Nothing Then

                claims.Add(New Claim(ClaimTypes.Name, UserName, Nothing, LoginProvider))
            End If

            Return claims
        End Function

        Public Shared Function FromIdentity(identity As ClaimsIdentity) As ExternalLoginData
            If identity Is Nothing Then
                Return Nothing
            End If

            Dim providerKeyClaim As Claim = identity.FindFirst(ClaimTypes.NameIdentifier)

            If providerKeyClaim Is Nothing Or String.IsNullOrEmpty(providerKeyClaim.Issuer) Or
            String.IsNullOrEmpty(providerKeyClaim.Value) Then
                Return Nothing
            End If


            If providerKeyClaim.Issuer = ClaimsIdentity.DefaultIssuer Then
                Return Nothing
            End If


            Return New ExternalLoginData With
            {
                .LoginProvider = providerKeyClaim.Issuer,
                .ProviderKey = providerKeyClaim.Value,
                .UserName = identity.FindFirstValue(ClaimTypes.Name)
            }
        End Function

        Protected NotInheritable Class RandomOAuthStateGenerator
            Private Shared _random As RandomNumberGenerator = New RNGCryptoServiceProvider()

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
    End Class
End Namespace
