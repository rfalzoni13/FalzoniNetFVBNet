Imports System.Data.Entity.Infrastructure
Imports System.Transactions
Imports System.Web
Imports FalzoniNetFVBNet.Application.IdentityConfiguration
Imports FalzoniNetFVBNet.Domain.DTO.Identity
Imports FalzoniNetFVBNet.Infra.Data.Identity
Imports FalzoniNetFVBNet.Utils.Helpers
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security

Namespace ServiceApplication.Configuration
    Public Class UserServiceApplication
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

        Protected Property UserManager As ApplicationUserManager
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
        Public Function GetAll() As ICollection(Of ApplicationUserDTO)

            Dim users = UserManager.Users

            Return users.ToList().ConvertAll(Function(u) New ApplicationUserDTO With
        {
            .Id = GuidHelper.StringToGuid(u.Id),
            .Name = u.FullName,
            .Email = u.Email,
            .PhoneNumber = u.PhoneNumber,
            .PhotoPath = u.PhotoPath,
            .Gender = u.Gender,
            .DateBirth = u.DateBirth,
            .UserName = u.UserName,
            .Created = u.Created,
            .Modified = u.Modified,
            .Roles = UserManager.GetRoles(u.Id).ToArray()
        })
        End Function

        Public Function [Get](Id As Guid) As ApplicationUserDTO

            Dim user = UserManager.FindById(GuidHelper.GuidToString(Id))

            Return New ApplicationUserDTO With
        {
            .Id = GuidHelper.StringToGuid(user.Id),
            .Name = user.FullName,
            .Email = user.Email,
            .PhoneNumber = user.PhoneNumber,
            .PhotoPath = user.PhotoPath,
            .Gender = user.Gender,
            .DateBirth = user.DateBirth,
            .UserName = user.UserName,
            .Created = user.Created,
            .Modified = user.Modified,
            .Roles = UserManager.GetRoles(user.Id).ToArray()
        }
        End Function


#End Region

#Region "Services"
        Public Sub Add(register As ApplicationUserRegisterDTO)
            Using scope As New TransactionScope(TransactionScopeAsyncFlowOption.Enabled)
                Try
                    'Get user And rollback if exists
                    Dim user = UserManager.FindByNameAsync(register.UserName).Result

                    If user Is Nothing Then
                        Throw New ApplicationException("Já existe um usuário com estas informações!")
                    End If

                    user = New ApplicationUser With
                {
                    .FullName = register.Name,
                    .Email = register.Email,
                    .PhoneNumber = register.PhoneNumber,
                    .DateBirth = register.DateBirth,
                    .Gender = register.Gender,
                    .UserName = register.UserName,
                    .Active = True,
                    .Created = Date.Now
                }

                    'Add profile photo
                    If register.File IsNot Nothing Then
                        Dim path As String = $"Attachments\\User\\{user.FullName}"

                        ImageHelper.SaveImageFile(register.File.Base64String, RequestHelper.GetApplicationPath() + path, register.File.FileName, register.File.Format)

                        user.PhotoPath = $"{path}\\{register.File.FileName}"
                    End If


                    'Add user
                    Dim result = UserManager.Create(user)

                    If Not result.Succeeded Then
                        Throw New DbUpdateException("Erro ao incluir usuário!")
                    End If
                    'Add role
                    Dim i As Integer = 0

                    While i < register.Roles.Count()
                        Dim role = RoleManager.FindByName(register.Roles(i))

                        If role Is Nothing Then
                            Throw New ArgumentNullException("Nenhum registro de permissão de acesso encontrado!")
                        End If

                        result = UserManager.AddToRole(user.Id, role.Name)

                        If Not result.Succeeded Then
                            Throw New DbUpdateException("Erro ao incluir perfil de acesso!")
                        End If

                        i += 1

                    End While

                    scope.Complete()

                Catch ex As Exception
                    scope.Dispose()
                    Throw ex
                End Try
            End Using
        End Sub

        Public Sub Delete(Id As String)
            Using scope As New TransactionScope(TransactionScopeAsyncFlowOption.Enabled)
                Try
                    'Get user And rollback if Not exists
                    Dim user = UserManager.FindById(Id)

                    If user Is Nothing Then
                        Throw New ArgumentNullException("Nenhum usuário encontrado!")
                    End If

                    FileHelper.DeleteFile(RequestHelper.GetApplicationPath() + user.PhotoPath)

                    'Delete user
                    Dim result = UserManager.Delete(user)

                    If Not result.Succeeded Then
                        Throw New DbUpdateException("Erro ao excluir usuário!")
                    End If

                    scope.Complete()

                Catch ex As Exception
                    scope.Dispose()
                    Throw ex
                End Try
            End Using
        End Sub

        Public Sub Update(register As ApplicationUserRegisterDTO)
            Using scope As New TransactionScope(TransactionScopeAsyncFlowOption.Enabled)
                Try
                    'Get user And rollback if Not exists
                    Dim user = UserManager.FindById(register.ID)

                    If user Is Nothing Then
                        Throw New ApplicationException("Nenhum usuário encontrado!")
                    End If

                    user.FullName = register.Name
                    user.Email = register.Email
                    user.DateBirth = register.DateBirth
                    user.PhoneNumber = register.PhoneNumber
                    user.Modified = Date.Now

                    'Update profile photo
                    If register.File IsNot Nothing Then
                        Dim path As String = $"Attachments\\User\\{user.FullName}"

                        FileHelper.DeleteFile($"{RequestHelper.GetApplicationPath() + user.PhotoPath}")

                        ImageHelper.SaveImageFile(register.File.Base64String, RequestHelper.GetApplicationPath() + path, register.File.FileName, register.File.Format)

                        user.PhotoPath = $"{path}\\{register.File.FileName}"
                    End If

                    'Update user
                    Dim result = UserManager.Update(user)
                    If Not result.Succeeded Then
                        Throw New DbUpdateException("Erro ao atualizar usuário!")
                    End If

                    'Update roles
                    Dim i As Integer = 0

                    Do
                        Dim role = RoleManager.FindByName(register.Roles(i))
                        If role Is Nothing Then
                            Throw New ArgumentNullException("Nenhum registro de permissão de acesso encontrado!")
                        End If

                        Dim roles = UserManager.GetRolesAsync(user.Id).Result
                        If roles Is Nothing Or roles.Count() <= 0 Then
                            Throw New ApplicationException("Erro ao cadastrar novo perfil de acesso!")
                        End If

                        result = UserManager.RemoveFromRolesAsync(user.Id, roles.ToArray()).Result

                        If Not result.Succeeded Then
                            Throw New DbUpdateException("Erro ao realizar manutenção de acesso!")
                        End If

                        result = UserManager.AddToRoleAsync(user.Id, role.Name).Result

                        If Not result.Succeeded Then
                            Throw New DbUpdateException("Erro ao atualizar acesso!")
                        End If

                        i += 1
                    Loop While i < register.Roles.Count()

                    scope.Complete()

                Catch ex As Exception
                    scope.Dispose()
                    Throw ex
                End Try
            End Using
        End Sub
#End Region

#Region "Async Services"
        Public Async Function AddAsync(register As ApplicationUserRegisterDTO) As Task
            Using scope As New TransactionScope(TransactionScopeAsyncFlowOption.Enabled)
                Try
                    'Get user And rollback if exists
                    Dim user = Await UserManager.FindByNameAsync(register.UserName)

                    If user Is Nothing Then
                        Throw New ApplicationException("Já existe um usuário com estas informações!")
                    End If

                    user = New ApplicationUser With
                {
                    .FullName = register.Name,
                    .Email = register.Email,
                    .PhoneNumber = register.PhoneNumber,
                    .DateBirth = register.DateBirth,
                    .Gender = register.Gender,
                    .UserName = register.UserName,
                    .Active = True,
                    .Created = Date.Now
                }

                    'Add profile photo
                    If register.File IsNot Nothing Then
                        Dim path As String = $"Attachments\\User\\{user.FullName}"

                        ImageHelper.SaveImageFile(register.File.Base64String, RequestHelper.GetApplicationPath() + path, register.File.FileName, register.File.Format)

                        user.PhotoPath = $"{path}\\{register.File.FileName}"
                    End If


                    'Add user
                    Dim result = Await UserManager.CreateAsync(user)

                    If Not result.Succeeded Then
                        Throw New DbUpdateException("Erro ao incluir usuário!")
                    End If
                    'Add role
                    Dim i As Integer = 0

                    While i < register.Roles.Count()
                        Dim role = RoleManager.FindByName(register.Roles(i))

                        If role Is Nothing Then
                            Throw New ArgumentNullException("Nenhum registro de permissão de acesso encontrado!")
                        End If

                        result = Await UserManager.AddToRoleAsync(user.Id, role.Name)

                        If Not result.Succeeded Then
                            Throw New DbUpdateException("Erro ao incluir perfil de acesso!")
                        End If

                        i += 1

                    End While

                    scope.Complete()

                Catch ex As Exception
                    scope.Dispose()
                    Throw ex
                End Try
            End Using
        End Function

        Public Async Function DeleteAsync(Id As String) As Task
            Using scope As New TransactionScope(TransactionScopeAsyncFlowOption.Enabled)
                Try
                    'Get user And rollback if Not exists
                    Dim user = Await UserManager.FindByIdAsync(Id)

                    If user Is Nothing Then
                        Throw New ArgumentNullException("Nenhum usuário encontrado!")
                    End If

                    FileHelper.DeleteFile(RequestHelper.GetApplicationPath() + user.PhotoPath)

                    'Delete user
                    Dim result = Await UserManager.DeleteAsync(user)

                    If Not result.Succeeded Then
                        Throw New DbUpdateException("Erro ao excluir usuário!")
                    End If

                    scope.Complete()

                Catch ex As Exception
                    scope.Dispose()
                    Throw ex
                End Try
            End Using
        End Function

        Public Async Function UpdateAsync(register As ApplicationUserRegisterDTO) As Task
            Using scope As New TransactionScope(TransactionScopeAsyncFlowOption.Enabled)
                Try
                    'Get user And rollback if Not exists
                    Dim user = Await UserManager.FindByIdAsync(register.ID)

                    If user Is Nothing Then
                        Throw New ApplicationException("Nenhum usuário encontrado!")
                    End If

                    user.FullName = register.Name
                    user.Email = register.Email
                    user.DateBirth = register.DateBirth
                    user.PhoneNumber = register.PhoneNumber
                    user.Modified = Date.Now

                    'Update profile photo
                    If register.File IsNot Nothing Then
                        Dim path As String = $"Attachments\\User\\{user.FullName}"

                        FileHelper.DeleteFile($"{RequestHelper.GetApplicationPath() + user.PhotoPath}")

                        ImageHelper.SaveImageFile(register.File.Base64String, RequestHelper.GetApplicationPath() + path, register.File.FileName, register.File.Format)

                        user.PhotoPath = $"{path}\\{register.File.FileName}"
                    End If

                    'Update user
                    Dim result = Await UserManager.UpdateAsync(user)
                    If Not result.Succeeded Then
                        Throw New DbUpdateException("Erro ao atualizar usuário!")
                    End If

                    'Update roles
                    Dim i As Integer = 0

                    Do
                        Dim role = RoleManager.FindByName(register.Roles(i))
                        If role Is Nothing Then
                            Throw New ArgumentNullException("Nenhum registro de permissão de acesso encontrado!")
                        End If

                        Dim roles = Await UserManager.GetRolesAsync(user.Id)
                        If roles Is Nothing Or roles.Count() <= 0 Then
                            Throw New ApplicationException("Erro ao cadastrar novo perfil de acesso!")
                        End If

                        result = Await UserManager.RemoveFromRolesAsync(user.Id, roles.ToArray())

                        If Not result.Succeeded Then
                            Throw New DbUpdateException("Erro ao realizar manutenção de acesso!")
                        End If

                        result = Await UserManager.AddToRoleAsync(user.Id, role.Name)

                        If Not result.Succeeded Then
                            Throw New DbUpdateException("Erro ao atualizar acesso!")
                        End If

                        i += 1
                    Loop While i < register.Roles.Count()

                    scope.Complete()

                Catch ex As Exception
                    scope.Dispose()
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Dispose"
        Public Sub Dispose() Implements IDisposable.Dispose
            RoleManager.Dispose()
            SignInManager.Dispose()
            UserManager.Dispose()
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
