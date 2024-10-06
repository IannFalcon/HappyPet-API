using AppHappyPet_API.DAO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaDAO dao;

        public CategoriaController(CategoriaDAO cate_dao)
        {
            dao = cate_dao;
        }

        // GET: api/<CategoriaController>
        [HttpGet]
        public IActionResult ListarCategias([FromQuery] string? nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener categorias
                var categorias = dao.ObtenerCategorias(nombre);

                // Validar si existen categorias
                if (categorias == null || categorias.Count == 0)
                {
                    return NotFound(new { mensaje = "No se encontraron categorias", data = categorias });
                }

                return Ok(new { mensaje = "Categorias encontradas", data = categorias });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener las categorias: {ex.Message}");
            }
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
