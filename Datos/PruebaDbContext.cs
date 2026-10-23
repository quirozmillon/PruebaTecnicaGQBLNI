using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    public class PruebaDbContext : DbContext
    {
        public DbSet<Cuenta> Cuentas { get; set; }

        public DbSet<Transaccion> Transacciones { get; set; }

        public DbSet<Clientes> Clientes { get; set; }


        public PruebaDbContext(DbContextOptions<PruebaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PruebaDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
