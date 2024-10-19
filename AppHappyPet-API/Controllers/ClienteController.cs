using Business;
using Entity.Models;
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
        [HttpGet]
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
        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> ObtenerClienteId(int idUsuario)
        {
            try
            {
                var cliente = await cli_service.ObtenerClienteId(idUsuario);
                return Ok(new { mensaje = "Cliente encontrado", data = cliente });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> RegistrarCliente([FromBody] Usuario usuario)
        {
            string _mensaje = string.Empty;

            try
            {
                var respuesta = await cli_service.RegistrarCliente(usuario);

                if (respuesta == "EXITO")
                {
                    _mensaje = "El cliente fue registrado con exito.";
                }

                return Ok(new { mensaje = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut]
        public async Task<IActionResult> ActualizarCliente([FromBody] Usuario usuario)
        {
            try
            {
                var respuesta = await cli_service.ActualizarCliente(usuario);
                return Ok(new { mensaje = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{idUsuario}")]
        public async Task<IActionResult> EliminarCliente(int idUsuario)
        {
            try
            {
                var respuesta = await cli_service.EliminarCliente(idUsuario);
                return Ok(new { mensaje = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
