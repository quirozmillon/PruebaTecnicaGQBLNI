using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Negocio.Abstracciones.Datos;
using Negocio.Abstracciones.Servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PruebaDbContext>(options => options.UseSqlite(connectionString));
            services.AddScoped<IClientesRepositorio, ClientesRepositorio>();
            services.AddScoped<ITrabajoUnico, TrabajoUnico>();

            return services;
        }

    }
}
