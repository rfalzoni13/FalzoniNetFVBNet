Imports FalzoniNetFVBNet.Application.ServiceApplication.Register
Imports FalzoniNetFVBNet.Application.ServiceApplication.Stock
Imports FalzoniNetFVBNet.Application.ServiceApplication.Configuration
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Base
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Register
Imports FalzoniNetFVBNet.Application.ServiceApplication.Identity
Imports FalzoniNetFVBNet.Infra.Data.Context.MySql
Imports FalzoniNetFVBNet.Infra.Data.Context.SqlServer
Imports FalzoniNetFVBNet.Infra.Data.Repositories.Base
Imports FalzoniNetFVBNet.Infra.Data.Repositories.Register
Imports FalzoniNetFVBNet.Service.Register
Imports FalzoniNetFVBNet.Utils.Helpers
Imports FalzoniNetFVBNet.Domain.Interfaces.Repositories.Stock
Imports FalzoniNetFVBNet.Infra.Data.Repositories.Stock
Imports FalzoniNetFVBNet.Service.Stock
Imports Unity
Imports System.Data.Entity

Public Class UnityModule
    Public Shared Function LoadModules() As UnityContainer
        Dim container = New UnityContainer()

#Region "Repositories"
        container.RegisterType(GetType(IBaseRepository(Of)), GetType(BaseRepository(Of)))

        container.RegisterType(Of ICustomerRepository, CustomerRepository)()
        container.RegisterType(Of ICustomerAddressRepository, CustomerAddressRepository)()
        container.RegisterType(Of IProductRepository, ProductRepository)()
        container.RegisterType(Of IProductCategoryRepository, ProductCategoryRepository)()
#End Region

#Region "Services"
        container.RegisterType(Of CustomerService)()
        container.RegisterType(Of ProductService)()
#End Region

#Region "Application"
        container.RegisterType(Of RoleServiceApplication)()
        container.RegisterType(Of AccountServiceApplication)()
        container.RegisterType(Of IdentityUtilityServiceApplication)()
        container.RegisterType(Of UserServiceApplication)()

        container.RegisterType(Of CustomerServiceApplication)()
        container.RegisterType(Of ProductServiceApplication)()
#End Region

        'Complements
        container.RegisterType(Of IUnitOfWork, UnitOfWork)()

        'Context
        Select Case ConfigurationHelper.ProviderName
            Case "SqlServer"
                container.RegisterType(Of DbContext, FalzoniSqlServerContext)()
                Exit Select
            Case "MySql"
                container.RegisterType(Of DbContext, FalzoniMySqlContext)()
                Exit Select
        End Select

        Return container

    End Function

End Class
