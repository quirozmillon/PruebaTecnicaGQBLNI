using Negocio.Abstracciones.Datos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    public class TrabajoUnico(PruebaDbContext dbContext) : ITrabajoUnico
    {
        public Task<int> GuardarCambiosAsync() => dbContext.SaveChangesAsync();
        
    }
}
