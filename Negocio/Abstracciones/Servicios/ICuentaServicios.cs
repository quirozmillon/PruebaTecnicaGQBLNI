using Negocio.Dto.Cuentas;
using Negocio.Dto.Transacciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Abstracciones.Servicios

{
    public interface ICuentaServicios
    {
        Task<CuentaRespuesta> CrearAsync(CuentaSolicitud solicitud);
        Task<CuentaRespuesta> BuscarPorIdAsync(string numeroCuenta);
        Task<CuentaRespuesta> DepositoAsync(TransaccionSolicitud solicitud);
        Task<CuentaRespuesta> RetiroAsync(TransaccionSolicitud solicitud);
        Task<IList<CuentaRespuesta>> ObtenerTodasCuentasAsync();
        Task<IList<TransaccionRespuesta>> ObtenerTransaccionesPorCuentaAsync(string numeroCuenta);
    }
}
