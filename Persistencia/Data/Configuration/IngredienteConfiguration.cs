using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class IngredienteConfiguration : IEntityTypeConfiguration<Ingrediente>
{


    public void Configure(EntityTypeBuilder<Ingrediente> builder)
    {
        builder.ToTable("Ingredientes");

        builder.Property(p => p.Nombre)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(p => p.Descripcion)
        .HasMaxLength(200)
        .IsRequired();

        builder.Property(p => p.Precio)
        .HasPrecision(20, 5)
        .IsRequired();

        builder.Property(p => p.stock)
        .HasPrecision(20, 5)
        .IsRequired();
    }
}
