using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Configuracion
{
    public class TransaccionConfiguracion : IEntityTypeConfiguration<Transaccion>
    {
        public void Configure(EntityTypeBuilder<Transaccion> builder)
        {
           builder.HasKey(transaccion => transaccion.TransaccionId);
            builder.Property(transaccion => transaccion.NumeroCuenta)
                 .IsRequired()
                 .HasColumnType("varchar(17)");

            builder.Property(transaccion => transaccion.Tipo)
                .IsRequired()
                .HasColumnType("varchar(1)");

            builder.Property(transaccion => transaccion.Monto)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(transaccion => transaccion.FechaProceso)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(transaccion => transaccion.Descripcion)
                .HasMaxLength(200)
                .HasColumnType("varchar(200)");

            builder.HasIndex(transaccion => new { transaccion.FechaProceso, transaccion.NumeroCuenta });    
        }
    }
}
