Imports System.Data.Entity.ModelConfiguration
Imports FalzoniNetFVBNet.Domain.Entities.Register

Namespace Configuration.Register
    Public Class CustomerMapConfiguration
        Inherits EntityTypeConfiguration(Of Customer)

        Public Sub New()
            HasKey(Function(c) c.Id)

            [Property](Function(c) c.Name).IsRequired().HasMaxLength(1024)

            [Property](Function(c) c.DateBirth).IsRequired()

            [Property](Function(c) c.Gender).IsRequired().HasMaxLength(50)

            [Property](Function(c) c.PhoneNumber).IsOptional().HasMaxLength(15)

            [Property](Function(c) c.CellPhoneNumber).IsRequired().HasMaxLength(15)

            [Property](Function(c) c.Document).IsRequired().HasMaxLength(20)

            [Property](Function(c) c.Email).IsRequired().HasMaxLength(128)

            [Property](Function(c) c.Created).IsRequired()

            [Property](Function(c) c.Modified).IsOptional()


            HasMany(Function(c) c.Addresses).WithRequired(Function(a) a.Customer).HasForeignKey(Function(a) a.CustomerId).WillCascadeOnDelete()
        End Sub

    End Class
End Namespace
