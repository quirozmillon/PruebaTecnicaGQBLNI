using Datos;
using Datos.Abstracciones;
using Datos.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Repositorio
{
    public class ClientesRepositorio(PruebaDbContext dbContext) : IClientesRepositorio
    {
        public void Actualizar(Clientes cliente)
        {
            dbContext.Clientes.Update(cliente);
        }

        public void Agregar(Clientes cliente)
        {
            dbContext.Clientes.Add(cliente);
        }

        public void Eliminar(int ClienteId)
        {
            dbContext.Clientes.Remove(new Clientes { ClienteId = ClienteId });
        }

        public async Task<Clientes?> ObtenerPorId(int ClienteId)
        {
            return await dbContext.Clientes.FirstOrDefaultAsync(c => c.ClienteId == ClienteId);
        }
    }
}
