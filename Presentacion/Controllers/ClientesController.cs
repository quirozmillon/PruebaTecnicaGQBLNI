using Microsoft.AspNetCore.Mvc;
using Negocio.Abstracciones.Servicios;
using Negocio.Dto.Clientes;

namespace Presentacion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController(IClienteServicio clienteServicio) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Crear(CrearClienteSolicitud solicitud)
        {
            var resultado = await clienteServicio.CrearAsync(solicitud);
            return CreatedAtAction(nameof(Crear), new { id = resultado.ClienteId }, resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var resultado = await clienteServicio.BuscarPorIdAsync(id);
            return Ok(resultado);
        }
    }
}
