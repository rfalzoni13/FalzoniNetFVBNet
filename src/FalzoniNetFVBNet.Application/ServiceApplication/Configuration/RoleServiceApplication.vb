Imports System.Web
Imports FalzoniNetFVBNet.Application.IdentityConfiguration
Imports Microsoft.Owin.Security
Imports Microsoft.AspNet.Identity.Owin
Imports FalzoniNetFVBNet.Domain.DTO.Identity
Imports FalzoniNetFVBNet.Utils.Helpers
Imports Microsoft.AspNet.Identity

Namespace ServiceApplication.Configuration
    Public Class RoleServiceApplication
        Implements IDisposable
#Region "Attributes"
        Private _signInManager As ApplicationSignInManager
        Private _userManager As ApplicationUserManager
        Private _roleManager As ApplicationRoleManager

        Public Property AccessTokenFormat As ISecureDataFormat(Of AuthenticationTicket)

        Protected Property RoleManager As ApplicationRoleManager
            Get
                Return If(_roleManager, HttpContext.Current.GetOwinContext().Get(Of ApplicationRoleManager)())
            End Get
            Set(value As ApplicationRoleManager)
                _roleManager = value
            End Set
        End Property

        Protected Property Usermanager As ApplicationUserManager
            Get
                Return If(_userManager, HttpContext.Current.GetOwinContext().GetUserManager(Of ApplicationUserManager)())
            End Get
            Set(value As ApplicationUserManager)
                _userManager = value
            End Set
        End Property

        Protected Property SignInManager As ApplicationSignInManager
            Get
                Return If(_signInManager, HttpContext.Current.GetOwinContext().Get(Of ApplicationSignInManager)())
            End Get
            Set(value As ApplicationSignInManager)
                _signInManager = value
            End Set
        End Property
#End Region

#Region "Getters"
        Public Function GelAllNames() As List(Of String)
            Dim roles = RoleManager.Roles

            Return roles.Select(Function(x) x.Name).Distinct().ToList()
        End Function

        Public Function GetAll() As List(Of ApplicationRoleDTO)
            Dim roles = RoleManager.Roles

            Return roles.ToList().ConvertAll(Function(r) New ApplicationRoleDTO With
            {
                .Id = GuidHelper.StringToGuid(r.Id),
                .Name = r.Name,
                .Created = r.Created,
                .Modified = r.Modified
            })
        End Function

        Public Function [Get](Id As Guid) As ApplicationRoleDTO
            Dim role = RoleManager.FindById(GuidHelper.GuidToString(Id))

            Return New ApplicationRoleDTO With
            {
                .Id = GuidHelper.StringToGuid(role.Id),
                .Name = role.Name,
                .Created = role.Created,
                .Modified = role.Modified
            }
        End Function
#End Region

#Region "Dispose"
        Public Sub Dispose() Implements IDisposable.Dispose
            RoleManager.Dispose()
            SignInManager.Dispose()
            Usermanager.Dispose()
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
