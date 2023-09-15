using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class HamburguesaConfiguration : IEntityTypeConfiguration<Hamburguesa>
{
    public void Configure(EntityTypeBuilder<Hamburguesa> builder)
    {
        builder.ToTable("Hamburguesas");

        builder.Property(p=>p.Nombre)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(p=>p.Precio)
        .HasPrecision(20,5)
        .IsRequired();

        builder.HasOne(p=>p.Categoria)
        .WithMany(p=>p.Hamburguesas)
        .HasForeignKey(p=>p.Categoria_id);

        builder.HasOne(p=>p.Chef)
        .WithMany(p=>p.Hamburguesas)
        .HasForeignKey(p=>p.Chef_id);
    }
}
