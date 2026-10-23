using Datos.Abstracciones;
using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Repositorio
{
    public class TransaccionRepositorio(PruebaDbContext dbContext) : ITransaccionRepositorio
    {
        public void Agregar(Transaccion transaccion)
        {
            dbContext.Transacciones.Add(transaccion);
        }

        public async Task<IList<Transaccion>> ObtenerPorNumeroCuentaAsync(string numeroCuenta)
        {
            return await dbContext.Transacciones
                .AsNoTracking()
                .Where(t => t.NumeroCuenta == numeroCuenta)
                .OrderBy(transaccion => transaccion.FechaProceso)
                .ThenBy(transaccion => transaccion.TransaccionId)
                .ToListAsync();
        }
    }
}
