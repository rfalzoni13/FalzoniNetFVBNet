Imports System.Data.Entity.ModelConfiguration
Imports FalzoniNetFVBNet.Domain.Entities.Register

Namespace Configuration.Register
    Public Class CustomerAddressMapConfiguration
        Inherits EntityTypeConfiguration(Of CustomerAddress)

        Public Sub New()
            HasKey(Function(c) c.Id)

            [Property](Function(c) c.PostalCode).IsRequired().HasMaxLength(10)

            [Property](Function(c) c.AddressName).IsRequired().HasMaxLength(512)

            [Property](Function(c) c.Number).IsRequired()

            [Property](Function(c) c.Complement).IsOptional().HasMaxLength(256)

            [Property](Function(c) c.Neighborhood).IsRequired().HasMaxLength(256)

            [Property](Function(c) c.City).IsRequired().HasMaxLength(128)

            [Property](Function(c) c.State).IsRequired().HasMaxLength(2)

            [Property](Function(c) c.Created).IsRequired()

            [Property](Function(c) c.Modified).IsOptional()
        End Sub

    End Class
End Namespace
