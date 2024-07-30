Imports System.Collections.Concurrent
Imports Microsoft.Owin.Security
Imports Microsoft.Owin.Security.Infrastructure

Namespace IdentityConfiguration
    Public Class AccessRefreshTokenProvider
        Implements IAuthenticationTokenProvider
        Private Shared _refreshTokens As ConcurrentDictionary(Of String, AuthenticationTicket) = New ConcurrentDictionary(Of String, AuthenticationTicket)()

#Region "CreateAsync"
        Public Async Function CreateAsync(context As AuthenticationTokenCreateContext) As Task Implements IAuthenticationTokenProvider.CreateAsync

            Dim g As String = Guid.NewGuid().ToString()

            Dim refreshTokenProperties = New AuthenticationProperties(context.Ticket.Properties.Dictionary) With
            {
                .IssuedUtc = context.Ticket.Properties.IssuedUtc,
                .ExpiresUtc = DateTime.UtcNow.AddDays(1)
            }

            Dim refreshTokenTicket = Await Task.Run(Function() New AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties))

            _refreshTokens.TryAdd(g, refreshTokenTicket)
            context.SetToken(g)
        End Function
#End Region

#Region "ReceiveAsync"
        Public Async Function ReceiveAsync(context As AuthenticationTokenReceiveContext) As Task Implements IAuthenticationTokenProvider.ReceiveAsync

            Dim ticket As AuthenticationTicket = Nothing
            Dim header As String = Await Task.Run(Function() context.OwinContext.Request.Headers("Authorization"))

            If _refreshTokens.TryRemove(context.Token, ticket) Then
                context.SetTicket(ticket)
            End If
        End Function
#End Region

#Region "Create & Receive Synchronous methods"
        Public Sub Create(context As AuthenticationTokenCreateContext) Implements IAuthenticationTokenProvider.Create
            Throw New NotImplementedException()
        End Sub

        Public Sub Receive(context As AuthenticationTokenReceiveContext) Implements IAuthenticationTokenProvider.Receive
            Throw New NotImplementedException()
        End Sub
#End Region
    End Class
End Namespace
