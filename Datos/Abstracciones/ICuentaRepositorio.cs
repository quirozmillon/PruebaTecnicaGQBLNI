using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Abstracciones
{
    public interface ICuentaRepositorio
    {
        void Agregar(Cuenta cuenta);

        Task<Cuenta?> ObtenerPorIdCuenta(string numeroCuenta);

        void Eliminar(string numeroCuenta);

        void Actualizar(Cuenta cuenta);

        Task<IList<Cuenta>> ObtenerCuentasAsync();
    }
}
