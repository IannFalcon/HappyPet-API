using Business;
using Entity.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService cli_service;

        public ClienteController(ClienteService cli_service)
        {
            this.cli_service = cli_service;
        }

        // GET: api/<UsuarioController>
        [HttpGet("listar")]
        public async Task<IActionResult> ListarClientes([FromQuery] string? nro_documento, [FromQuery] string? nombre)
        {
            try
            {
                var clientes = await cli_service.ListarClientes(nro_documento!, nombre!);
                return Ok(new { mensaje = "Clientes encontrados", data = clientes });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("obtener/{id_cliente}")]
        public async Task<IActionResult> ObtenerClienteId(int id_cliente)
        {
            try
            {
                var cliente = await cli_service.ObtenerClienteId(id_cliente);
                return Ok(new { mensaje = "Cliente encontrado", data = cliente });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<UsuarioController>
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarCliente([FromBody] DatosClienteRequest request)
        {
            try
            {
                var respuesta = await cli_service.RegistrarCliente(request);
                return Ok(new { mensaje = respuesta.Mensaje });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("actualizar/{id_cliente}")]
        public async Task<IActionResult> ActualizarCliente([FromBody] DatosClienteRequest request, int id_cliente)
        {
            try
            {
                var respuesta = await cli_service.ActualizarCliente(request, id_cliente);
                return Ok(new { mensaje = respuesta.Mensaje });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("eliminar/{id_cliente}")]
        public async Task<IActionResult> EliminarCliente(int id_cliente)
        {
            try
            {
                var respuesta = await cli_service.EliminarCliente(id_cliente);
                return Ok(new { mensaje = respuesta.Mensaje });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("exportar")]
        public async Task<ActionResult> ExportarClientes()
        {
            try
            {
                var content = await cli_service.ExportarListadoClientes();
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                var nombreArchivo = $"Listado-Clientes-{fechaActual}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
