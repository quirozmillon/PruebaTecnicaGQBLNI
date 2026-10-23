using Negocio.Dto.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Abstracciones.Servicios
{
    public interface IClienteServicio 
    {
        Task<ClienteRespuesta> CrearAsync(CrearClienteSolicitud solicitud);
        Task<ClienteRespuesta> BuscarPorIdAsync(int id);

    }
}
