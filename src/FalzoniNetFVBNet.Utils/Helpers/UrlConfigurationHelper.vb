Imports System.Configuration

Namespace Helpers
    Public Class UrlConfigurationHelper
#Region "Base"
        Public Shared Property PathUrl As String
#End Region

#Region "Account"
        Public Shared Property AccountLogin As String
        Public Shared Property AccountLogout As String
        Public Shared Property AccountExternalLogin As String
        Public Shared Property AccountGetExternalLogins As String
        Public Shared Property AccountAddExternalLogin As String
        Public Shared Property AccountAddUserExternalLogin As String
        Public Shared Property AccountRemoveExternalLogin As String
        Public Shared Property AccountChangePassword As String
        Public Shared Property AccountForgotPassword As String
        Public Shared Property AccountResetPassword As String
#End Region

#Region "IdentityUtility"
        Public Shared Property IdentityUtilityGetTwoFactorProviders As String
        Public Shared Property IdentityUtilitySendTwoFactorProviderCode As String
        Public Shared Property IdentityUtilityVerifyCodeTwoFactor As String
        Public Shared Property IdentityUtilitySendEmailConfirmationCode As String
        Public Shared Property IdentityUtilitySendPhoneConfirmationCode As String
        Public Shared Property IdentityUtilityVerifyEmailConfirmationCode As String
        Public Shared Property IdentityUtilityVerifyPhoneConfirmationCode As String
#End Region

#Region "Role"
        Public Shared Property RoleGetAllNames As String
        Public Shared Property RoleGetAll As String
        Public Shared Property RoleGet As String
        Public Shared Property RoleCreate As String
        Public Shared Property RoleEdit As String
        Public Shared Property RoleDelete As String
#End Region

#Region "User"
        Public Shared Property UserGetAll As String
        Public Shared Property UserGet As String
        Public Shared Property UserCreate As String
        Public Shared Property UserEdit As String
        Public Shared Property UserDelete As String
#End Region

#Region "Customer"
        Public Shared Property CustomerGetAll As String
        Public Shared Property CustomerGet As String
        Public Shared Property CustomerCreate As String
        Public Shared Property CustomerEdit As String
        Public Shared Property CustomerDelete As String
#End Region

#Region "Product"
        Public Shared Property ProductGetAll As String
        Public Shared Property ProductGet As String
        Public Shared Property ProductCreate As String
        Public Shared Property ProductEdit As String
        Public Shared Property ProductDelete As String
#End Region


        Public Shared Sub SetUrlList()
#Region "Path"
            PathUrl = If(Not Debugger.IsAttached, ConfigurationManager.AppSettings("UrlApiProd"), ConfigurationManager.AppSettings("UrlApiDev"))
#End Region

#Region "Account"
            AccountLogin = $"{PathUrl}/{ConfigurationManager.AppSettings("AccountUrl")}/Login"
            AccountLogout = $"{PathUrl}/{ConfigurationManager.AppSettings("AccountUrl")}/Logout"
            AccountExternalLogin = $"{PathUrl}/{ConfigurationManager.AppSettings("AccountUrl")}/ExternalLogin"
            AccountGetExternalLogins = $"{PathUrl}/{ConfigurationManager.AppSettings("AccountUrl")}/GetExternalLogins"
            AccountAddExternalLogin = $"{PathUrl}/{ConfigurationManager.AppSettings("AccountUrl")}/AddExternalLogin"
            AccountAddUserExternalLogin = $"{PathUrl}/{ConfigurationManager.AppSettings("AccountUrl")}/AddExternalUserLogin"
            AccountRemoveExternalLogin = $"{PathUrl}/{ConfigurationManager.AppSettings("AccountUrl")}/RemoveExternalLogin"
            AccountChangePassword = $"{PathUrl}/{ConfigurationManager.AppSettings("AccountUrl")}/ChangePassword"
            AccountForgotPassword = $"{PathUrl}/{ConfigurationManager.AppSettings("AccountUrl")}/ForgotPassword"
            AccountResetPassword = $"{PathUrl}/{ConfigurationManager.AppSettings("AccountUrl")}/RecoverPassword"
#End Region

#Region "IdentityUtility"
            IdentityUtilityGetTwoFactorProviders = $"{PathUrl}/{ConfigurationManager.AppSettings("IdentityUtilityUrl")}/GetTwoFactorProviders"
            IdentityUtilitySendTwoFactorProviderCode = $"{PathUrl}/{ConfigurationManager.AppSettings("IdentityUtilityUrl")}/SendTwoFactorProviderCode"
            IdentityUtilityVerifyCodeTwoFactor = $"{PathUrl}/{ConfigurationManager.AppSettings("IdentityUtilityUrl")}/VerifyCodeTwoFactor"
            IdentityUtilitySendEmailConfirmationCode = $"{PathUrl}/{ConfigurationManager.AppSettings("IdentityUtilityUrl")}/SendEmailConfirmationCode"
            IdentityUtilitySendPhoneConfirmationCode = $"{PathUrl}/{ConfigurationManager.AppSettings("IdentityUtilityUrl")}/SendPhoneConfirmationCode"
            IdentityUtilityVerifyEmailConfirmationCode = $"{PathUrl}/{ConfigurationManager.AppSettings("IdentityUtilityUrl")}/VerifyEmailConfirmationCode"
            IdentityUtilityVerifyPhoneConfirmationCode = $"{PathUrl}/{ConfigurationManager.AppSettings("IdentityUtilityUrl")}/VerifyPhoneConfirmationCode"
#End Region

#Region "Role"
            RoleGetAllNames = $"{PathUrl}/{ConfigurationManager.AppSettings("RoleUrl")}/GetAllNames"
            RoleGetAll = $"{PathUrl}/{ConfigurationManager.AppSettings("RoleUrl")}/GetAll"
            RoleGet = $"{PathUrl}/{ConfigurationManager.AppSettings("RoleUrl")}/Get"
            RoleCreate = $"{PathUrl}/{ConfigurationManager.AppSettings("RoleUrl")}/Create"
            RoleEdit = $"{PathUrl}/{ConfigurationManager.AppSettings("RoleUrl")}/Update"
            RoleDelete = $"{PathUrl}/{ConfigurationManager.AppSettings("RoleUrl")}/Delete"
#End Region

#Region "User"
            UserGetAll = $"{PathUrl}/{ConfigurationManager.AppSettings("UserUrl")}/GetAll"
            UserGet = $"{PathUrl}/{ConfigurationManager.AppSettings("UserUrl")}/Get"
            UserCreate = $"{PathUrl}/{ConfigurationManager.AppSettings("UserUrl")}/Create"
            UserEdit = $"{PathUrl}/{ConfigurationManager.AppSettings("UserUrl")}/Update"
            UserDelete = $"{PathUrl}/{ConfigurationManager.AppSettings("UserUrl")}/Delete"
#End Region

#Region "Customer"
            CustomerGetAll = $"{PathUrl}/{ConfigurationManager.AppSettings("CustomerUrl")}/GetAll"
            CustomerGet = $"{PathUrl}/{ConfigurationManager.AppSettings("CustomerUrl")}/Get"
            CustomerCreate = $"{PathUrl}/{ConfigurationManager.AppSettings("CustomerUrl")}/Create"
            CustomerEdit = $"{PathUrl}/{ConfigurationManager.AppSettings("CustomerUrl")}/Update"
            CustomerDelete = $"{PathUrl}/{ConfigurationManager.AppSettings("CustomerUrl")}/Delete"
#End Region

#Region "Product"
            ProductGetAll = $"{PathUrl}/{ConfigurationManager.AppSettings("ProductUrl")}/GetAll"
            ProductGet = $"{PathUrl}/{ConfigurationManager.AppSettings("ProductUrl")}/Get"
            ProductCreate = $"{PathUrl}/{ConfigurationManager.AppSettings("ProductUrl")}/Create"
            ProductEdit = $"{PathUrl}/{ConfigurationManager.AppSettings("ProductUrl")}/Update"
            ProductDelete = $"{PathUrl}/{ConfigurationManager.AppSettings("ProductUrl")}/Delete"
#End Region
        End Sub
    End Class
End Namespace
