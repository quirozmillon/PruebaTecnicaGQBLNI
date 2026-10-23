using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Abstracciones
{
    public interface ITransaccionRepositorio
    {
        void Agregar(Transaccion transaccion);

        Task<IList<Transaccion>> ObtenerPorNumeroCuentaAsync(string numeroCuenta);
    }
}
