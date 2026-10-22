using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Configuracion
{
    public class ClientesConfiguracion  : IEntityTypeConfiguration<Clientes>
    {
        public void Configure(EntityTypeBuilder<Clientes> builder)
        {
            builder.HasKey(cliente => cliente.ClienteId);
            builder.Property(cliente => cliente.NombreCompleto)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("varchar(100)");
            builder.Property(cliente => cliente.FechaNacimiento)
                   .IsRequired()
                   .HasColumnType("date");
            builder.Property(cliente => cliente.Sexo) 
                    .IsRequired()
                    .HasColumnType("varchar(1)");
            builder.Property(cliente => cliente.IngresosMensuales)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(cliente => cliente.FechaCreacion)
                   .IsRequired()
                   .HasColumnType("datetime");
        }
    }
}
