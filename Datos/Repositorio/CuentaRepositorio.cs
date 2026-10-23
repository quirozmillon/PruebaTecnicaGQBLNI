using Datos.Entidades;
using Datos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Abstracciones
{
    public class CuentaRepositorio(PruebaDbContext dbContext) : ICuentaRepositorio
    {
        public void Actualizar(Cuenta cuenta)
        {
            dbContext.Cuentas.Update(cuenta);
        }

        public void Agregar(Cuenta cuenta)
        {
            dbContext.Cuentas.Add(cuenta);
        }

        public void Eliminar(string numeroCuenta)
        {
            var cuenta = dbContext.Cuentas.Find(numeroCuenta);
            if (cuenta != null)
            {
                dbContext.Cuentas.Remove(cuenta);
            }
        }

        public async Task<IList<Cuenta>> ObtenerCuentasAsync()
        {
            return await dbContext.Cuentas.AsNoTracking().ToListAsync();
        }

        public Task<Cuenta?> ObtenerPorIdCuenta(string numeroCuenta)
        {
            return dbContext.Cuentas.FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);
        }
    }
}

