using AppHappyPet_API.DAO;
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
        public void Post([FromBody] string value)
        {
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
