using AppHappyPet_API.DAO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly MarcaDAO dao;

        public MarcaController(MarcaDAO marca_dao)
        {
            dao = marca_dao;
        }

        // GET: api/<MarcaController>
        [HttpGet]
        public IActionResult ListarProductos([FromQuery] string? nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener marcas
                var marcas = dao.ObtenerMarcas(nombre);

                // Validar si existen marcas
                if (marcas == null || marcas.Count == 0)
                {
                    return NotFound(new { mensaje = "No se encontraron marcas", data = marcas });
                }

                return Ok(new { mensaje = "Marcas encontradas", data = marcas });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener las marcas: {ex.Message}");
            }

        }

        // GET api/<MarcaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MarcaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MarcaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MarcaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
