using Datos.Entidades;
using Negocio.Abstracciones.Datos;
using Negocio.Abstracciones.Servicios;
using Negocio.Dto.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
{
    public class ClienteServicios(
        IClientesRepositorio clienteRepositorio,
        ITrabajoUnico trabajoUnico
        ) : IClienteServicio
    {
        public async Task<ClienteRespuesta> CrearAsync(CrearClienteSolicitud solicitud)
        {
            var nuevoCliente = new Clientes
            {
                NombreCompleto = solicitud.NombreCompleto,
                FechaNacimiento = solicitud.FechaNacimiento,
                Sexo = solicitud.Sexo,
                IngresosMensuales = solicitud.IngresosMensuales,
                FechaCreacion = DateTime.UtcNow
            };

            clienteRepositorio.Agregar(nuevoCliente);
            await trabajoUnico.GuardarCambiosAsync();

            var respuesta = new ClienteRespuesta
            {
                ClienteId = nuevoCliente.ClienteId,
                NombreCompleto = nuevoCliente.NombreCompleto,
                FechaNacimiento = nuevoCliente.FechaNacimiento,
                Sexo = nuevoCliente.Sexo,
                IngresosMensuales = nuevoCliente.IngresosMensuales,
                FechaCreacion = nuevoCliente.FechaCreacion
            };

            return respuesta;
        }
    }
}
