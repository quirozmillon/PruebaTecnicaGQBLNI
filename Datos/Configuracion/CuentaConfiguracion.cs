using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Configuracion
{
    public class CuentaConfiguracion : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.HasKey(cuenta => cuenta.NumeroCuenta);

            builder.Property(cuenta => cuenta.NumeroCuenta)
                   .IsRequired()
                   .HasColumnType("varchar(17)");

            builder.Property(cuenta => cuenta.SaldoInicial)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(cuenta => cuenta.Saldo)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(cuenta => cuenta.FechaCreacion)
                    .IsRequired()
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("GETDATE()");

            builder.HasOne(cuenta => cuenta.Cliente)
                    .WithMany()
                    .HasForeignKey(cuenta => cuenta.ClienteId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(cuentas => cuentas.Transacciones)
                    .WithOne()
                    .HasForeignKey(transaccion => transaccion.NumeroCuenta) 
                    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
