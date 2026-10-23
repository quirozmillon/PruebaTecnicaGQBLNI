using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Negocio.Abstracciones.Servicios;
using Negocio.Dto.Cuentas;
using Negocio.Dto.Transacciones;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CuentasController(ICuentaServicios cuentaServicios) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Crear(CuentaSolicitud solicitud)
        {
            var resultado = await cuentaServicios.CrearAsync(solicitud);
            return CreatedAtAction(nameof(Crear), new { numeroCuenta = resultado.NumeroCuenta }, resultado);
        }

        [HttpGet("lista")]
        public async Task<IActionResult> Lista()
        {
            var resultado = await cuentaServicios.ObtenerTodasCuentasAsync();
            return Ok(resultado);
        }

        [HttpGet("{numeroCuenta}")]
        public async Task<IActionResult> BuscarPorNumeroCuenta(string numeroCuenta)
        {
            var resultado = await cuentaServicios.BuscarPorIdAsync(numeroCuenta);
            return Ok(resultado);
        }

        [HttpPost("{numeroCuenta}/deposito")]
        public async Task<IActionResult> Deposito(string numeroCuenta, [FromBody] TransaccionSolicitud solicitud)
        {
            var resultado = await cuentaServicios.DepositoAsync(solicitud);
            return Ok(resultado);
        }

        [HttpPost("{numeroCuenta}/retiro")]
        public async Task<IActionResult> Retiro(string numeroCuenta, [FromBody] TransaccionSolicitud solicitud)
        {
            var resultado = await cuentaServicios.RetiroAsync(solicitud);
            return Ok(resultado);
        }

        [HttpPost("{numeroCuenta}/historial")]
        public async Task<IActionResult> Historial(string numeroCuenta)
        {
            var resultado = await cuentaServicios.ObtenerTransaccionesPorCuentaAsync(numeroCuenta);
            return Ok(resultado);
        }

    }
}
