using Datos.Abstracciones;
using Datos.Entidades;
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
    }
}
