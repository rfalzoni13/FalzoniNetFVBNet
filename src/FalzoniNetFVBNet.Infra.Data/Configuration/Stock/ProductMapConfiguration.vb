Imports System.Data.Entity.ModelConfiguration
Imports FalzoniNetFVBNet.Domain.Entities.Stock

Namespace Configuration.Stock
    Public Class ProductMapConfiguration
        Inherits EntityTypeConfiguration(Of Product)

        Public Sub New()
            HasKey(Function(p) p.Id)

            [Property](Function(p) p.Name).IsRequired().HasMaxLength(500)

            [Property](Function(p) p.Code).IsRequired()

            [Property](Function(p) p.Description).HasMaxLength(500).HasColumnType("text")

            [Property](Function(p) p.Price).IsRequired()

            [Property](Function(p) p.Created).IsRequired()

            [Property](Function(p) p.Modified).IsOptional()


            HasRequired(Function(p) p.Category).WithRequiredPrincipal().Map(Function(f) f.MapKey("ProductCategoryId"))
        End Sub
    End Class
End Namespace
