Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Identity
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Base
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Configuration
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Interfaces.Register
Imports FalzoniNetFVBNet.Presentation.Administrator.Clients.Register
Imports Microsoft.Web.Infrastructure.DynamicModuleHelper
Imports Ninject
Imports Ninject.Web.Common
Imports Ninject.Web.Common.WebHost

<Assembly: WebActivatorEx.PreApplicationStartMethod(GetType(NinjectWebCommon), "Start")>
<Assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(GetType(NinjectWebCommon), "Stop")>

Public NotInheritable Class NinjectWebCommon
    Private Shared ReadOnly bootstrapper As Bootstrapper = New Bootstrapper()

    ''' <summary>
    ''' Starts the application.
    ''' </summary>
    Public Shared Sub Start()
        DynamicModuleUtility.RegisterModule(GetType(OnePerRequestHttpModule))
        DynamicModuleUtility.RegisterModule(GetType(NinjectHttpModule))
        bootstrapper.Initialize(AddressOf CreateKernel)
    End Sub

    ''' <summary>
    ''' Stops the application.
    ''' </summary>
    Public Shared Sub [Stop]()
        bootstrapper.ShutDown()
    End Sub

    ''' <summary>
    ''' Creates the kernel that will manage your application.
    ''' </summary>
    ''' <returns>The created kernel.</returns>
    Private Shared Function CreateKernel() As IKernel
        Dim kernel = New StandardKernel()
        Try
            kernel.Bind(Of Func(Of IKernel))().ToMethod(Function(ctx) Function() New Bootstrapper().Kernel)
            kernel.Bind(Of IHttpModule)().To(Of HttpApplicationInitializationHttpModule)()
            RegisterServices(kernel)
            Return kernel
        Catch ex As Exception
            kernel.Dispose()
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Load your modules Or register your services here!
    ''' </summary>
    ''' <param name="kernel">The kernel.</param>
    Private Shared Sub RegisterServices(kernel As IKernel)
        ' Dependency Injection of Client's Restful interfaces
        kernel.Bind(GetType(IBaseClient(Of)), GetType(BaseClient(Of)))

        kernel.Bind(Of IRoleClient)().To(Of RoleClient)().InRequestScope()
        kernel.Bind(Of IUserClient)().To(Of UserClient)().InRequestScope()
        kernel.Bind(Of ICustomerClient)().To(Of CustomerClient)().InRequestScope()

        kernel.Bind(Of AccountClient)().ToSelf().InRequestScope()

        'kernel.BindFilter(Of ProfileActionAttribute)(System.Web.Mvc.FilterScope.Global, 1).InRequestScope()
    End Sub
End Class