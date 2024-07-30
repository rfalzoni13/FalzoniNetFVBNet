Imports FalzoniNetFVBNet.Infra.Data.Context
Imports FalzoniNetFVBNet.Infra.Data.Identity
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin

Namespace IdentityConfiguration
    Public Class ApplicationRoleManager
        Inherits RoleManager(Of ApplicationRole)

        Public Sub New(store As IRoleStore(Of ApplicationRole, String))
            MyBase.New(store)
        End Sub

        Public Shared Function Create([options] As IdentityFactoryOptions(Of ApplicationRoleManager), context As IOwinContext) As ApplicationRoleManager
            Return New ApplicationRoleManager(New RoleStore(Of ApplicationRole)(context.Get(Of FalzoniContext)()))
        End Function
    End Class
End Namespace
