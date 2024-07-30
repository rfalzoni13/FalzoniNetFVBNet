Imports FalzoniNetFVBNet.Infra.Data.Context
Imports FalzoniNetFVBNet.Infra.Data.Identity
Imports FalzoniNetFVBNet.Service.Senders.Email
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security.DataProtection

Namespace IdentityConfiguration
    Public Class ApplicationUserManager
        Inherits UserManager(Of ApplicationUser)

        Public Sub New(store As IUserStore(Of ApplicationUser))
            MyBase.New(store)
        End Sub

        Public Shared Function Create([options] As IdentityFactoryOptions(Of ApplicationUserManager), context As IOwinContext) As ApplicationUserManager
            Dim appDbContext = context.Get(Of FalzoniContext)()
            Dim appUserManager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(appDbContext))

            'appUserManager.UserValidator = New UserValidator(Of ApplicationUser)(appUserManager) With
            '{
            '    .AllowOnlyAlphanumericUserNames = False,
            '    .RequireUniqueEmail = True
            '}

            'appUserManager.PasswordValidator = New PasswordValidator With
            '{
            '    .RequiredLength = 6,
            '    .RequireNonLetterOrDigit = True,
            '    .RequireDigit = True,
            '    .RequireLowercase = True,
            '    .RequireUppercase = True
            '}

            appUserManager.RegisterTwoFactorProvider(twoFactorProvider:="PhoneCode", New PhoneNumberTokenProvider(Of ApplicationUser) With
            {
                .MessageFormat = "Seu código de segurança é: {0}"
            })

            appUserManager.RegisterTwoFactorProvider(twoFactorProvider:="EmailCode", New EmailTokenProvider(Of ApplicationUser) With
            {
                .Subject = "Código de segurança",
                .BodyFormat = "Seu código de segurança é: {0}"
            })

            appUserManager.EmailService = New EmailIdentityMessageService()
            'appUserManager.SmsService = New SmsIdentityMessageService()

            Dim provider = New DpapiDataProtectionProvider(appName:="Projeto Falzoni")

            appUserManager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(protector:=provider.Create("Projeto Falzoni"))

            Return appUserManager
        End Function
    End Class
End Namespace
