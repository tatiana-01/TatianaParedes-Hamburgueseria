using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class HamburguesaIngredienteConfiguration : IEntityTypeConfiguration<HamburguesaIngrediente>
{
    public void Configure(EntityTypeBuilder<HamburguesaIngrediente> builder)
    {
        builder.ToTable("HamburguesaIngredientes");

        builder.HasOne(p=>p.Hamburguesa)
        .WithMany(p=>p.HamburguesaIngredientes)
        .HasForeignKey(p=>p.Hamburguesa_id);

        builder.HasOne(p=>p.Ingrediente)
        .WithMany(p=>p.HamburguesaIngredientes)
        .HasForeignKey(p=>p.Ingrediente_id);

        builder.HasKey(p=> new {p.Hamburguesa_id, p.Ingrediente_id});
    }
}
