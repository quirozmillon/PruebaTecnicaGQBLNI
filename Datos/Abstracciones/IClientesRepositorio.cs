using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Abstracciones
{
    public interface IClientesRepositorio
    {
        void Agregar(Clientes cliente);

        Task<Clientes?> ObtenerPorId(int ClienteId);

        void Eliminar(int ClienteId);

        void Actualizar(Clientes cliente);
    }
}
