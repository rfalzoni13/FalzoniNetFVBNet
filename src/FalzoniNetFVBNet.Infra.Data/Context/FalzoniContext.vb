Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports System.Reflection
Imports FalzoniNetFVBNet.Domain.Entities.Register
Imports FalzoniNetFVBNet.Domain.Entities.Stock
Imports FalzoniNetFVBNet.Infra.Data.Context.MySql
Imports FalzoniNetFVBNet.Infra.Data.Context.SqlServer
Imports FalzoniNetFVBNet.Infra.Data.Identity
Imports FalzoniNetFVBNet.Utils.Helpers
Imports Microsoft.AspNet.Identity.EntityFramework

Namespace Context
    Public MustInherit Class FalzoniContext
        Inherits IdentityDbContext(Of ApplicationUser)

#Region "Attributes"
        Public Property Customer As DbSet(Of Customer)
        Public Property CustomerAddress As DbSet(Of CustomerAddress)
        Public Property Product As DbSet(Of Product)
        Public Property ProductCategory As DbSet(Of ProductCategory)
#End Region

        Public Sub New()
            MyBase.New("Falzoni", throwIfV1Schema:=False)

            Configuration.LazyLoadingEnabled = False
            Configuration.ProxyCreationEnabled = False
        End Sub

        Public Shared Function Create() As FalzoniContext
            Select Case ConfigurationHelper.ProviderName
                Case "SqlServer"
                    Return New FalzoniSqlServerContext
                Case "MySql"
                    Return New FalzoniMySqlContext
                Case Else
                    Throw New Exception("Erro ao definir provider")
            End Select
        End Function

        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            'Create Generic instances of EntityBaseTypeConfiguration
            Dim typesToRegister = Assembly.GetExecutingAssembly().GetTypes().
            Where(Function(type) Not String.IsNullOrEmpty(type.Namespace)).
            Where(Function(type) type.BaseType IsNot Nothing And type.BaseType.IsGenericType AndAlso
            type.BaseType.GetGenericTypeDefinition() Is GetType(EntityTypeConfiguration(Of)))

            For Each type In typesToRegister
                Dim configurationInstance = Activator.CreateInstance(type)
                modelBuilder.Configurations.Add(configurationInstance)
            Next

            modelBuilder.Conventions.Remove(Of PluralizingTableNameConvention)()
            'modelBuilder.Conventions.Remove(Of OneToManyCascadeDeleteConvention)()
            'modelBuilder.Conventions.Remove(Of ManyToManyCascadeDeleteConvention)()

            MyBase.OnModelCreating(modelBuilder)
        End Sub
    End Class
End Namespace
