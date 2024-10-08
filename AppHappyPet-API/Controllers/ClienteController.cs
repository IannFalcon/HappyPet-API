using AppHappyPet_API.DAO;
using AppHappyPet_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteDAO dao_cliente;

        public ClienteController(ClienteDAO dao_cliente)
        {
            this.dao_cliente = dao_cliente;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public IActionResult ListarClientes([FromQuery] string? nro_documento, [FromQuery] string? nombre)
        {
            try
            {
                var clientes = dao_cliente.ObtenerClientes(nro_documento, nombre);

                if (clientes == null || clientes.Count == 0)
                {
                    return Ok(new { mensaje = "No se encontraron clientes", data = clientes });
                }

                return Ok(new { mensaje = "Clientes encontrados", data = clientes });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener los clientes: {ex.Message}");
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult RegistrarCliente([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario.Nombre == null || usuario.Nombre == "")
                {
                    return BadRequest("Error: El nombre del vendedor es requerido");
                }

                if (usuario.ApellidoPaterno == null || usuario.ApellidoPaterno == "")
                {
                    return BadRequest("Error: El apellido paterno del vendedor es requerido");
                }

                if (usuario.ApellidoMaterno == null || usuario.ApellidoMaterno == "")
                {
                    return BadRequest("Error: El apellido materno del vendedor es requerido");
                }

                if (usuario.IdTipoDocumento == 0)
                {
                    return BadRequest("Error: El tipo de documento del vendedor es requerido");
                }

                if (usuario.NroDocumento == null || usuario.NroDocumento == "")
                {
                    return BadRequest("Error: El número de documento del vendedor es requerido");
                }

                if (usuario.Telefono == null || usuario.Telefono == "")
                {
                    return BadRequest("Error: El teléfono del vendedor es requerido");
                }

                if (usuario.Direccion == null || usuario.Direccion == "")
                {
                    return BadRequest("Error: La dirección del vendedor es requerida");
                }

                if (usuario.Correo == null || usuario.Correo == "")
                {
                    return BadRequest("Error: El correo del vendedor es requerido");
                }

                // Mandar a llamar al método de registrar cliente
                var respuesta = dao_cliente.NuevoCliente(usuario);

                // Retornar respuesta
                return Ok(new { mensaje = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al registrar el cliente: {ex.Message}");
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
