using Microsoft.Extensions.DependencyInjection;
using Negocio.Abstracciones;
using Negocio.Abstracciones.Servicios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddLogicaNegocio(this IServiceCollection services)
        {
            services.AddScoped<IClienteServicio, ClienteServicios>();
            services.AddScoped<ICuentaServicios, CuentaServicios>();
            services.AddScoped<IGeneradorNoCuenta, GeneradorNoCuenta>();
            return services;
        }

    }
}
