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
        public IActionResult ListarClientes([FromQuery] string? nro_documento, [FromQuery] string? nombre)
        {
            try
            {
                var clientes = cli_service.ListarClientes(nro_documento!, nombre!);
                return Ok(new { mensaje = "Clientes encontrados", data = clientes });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{idUsuario}")]
        public IActionResult ObtenerClienteId(int idUsuario)
        {
            try
            {
                var cliente = cli_service.ObtenerClienteId(idUsuario);
                return Ok(new { mensaje = "Cliente encontrado", data = cliente });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult RegistrarCliente([FromBody] Usuario usuario)
        {
            string _mensaje = string.Empty;

            try
            {
                var respuesta = cli_service.RegistrarCliente(usuario);

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
        public IActionResult ActualizarCliente([FromBody] Usuario usuario)
        {
            try
            {
                var respuesta = cli_service.ActualizarCliente(usuario);
                return Ok(new { mensaje = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{idUsuario}")]
        public IActionResult EliminarCliente(int idUsuario)
        {
            try
            {
                var respuesta = cli_service.EliminarCliente(idUsuario);
                return Ok(new { mensaje = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
