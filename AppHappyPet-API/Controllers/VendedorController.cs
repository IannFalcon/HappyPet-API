using AppHappyPet_API.DAO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly VendedorDAO dao_vend;

        public VendedorController(VendedorDAO dao_vend)
        {
            this.dao_vend = dao_vend;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public IActionResult ListarVendedores([FromQuery] string? nro_documento, [FromQuery] string? nombre)
        {
            try
            {
                var vendedores = dao_vend.ObtenerVendedores(nro_documento, nombre);

                if (vendedores == null || vendedores.Count == 0)
                {
                    return Ok(new { mensaje = "No se encontraron vendedores", data = vendedores });
                }

                return Ok(new { mensaje = "Vendedores encontrados", data = vendedores });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener los vendedores: {ex.Message}");
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
