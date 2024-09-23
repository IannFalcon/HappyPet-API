using AppHappyPet_API.DAO;
using AppHappyPet_API.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly AutenticacionDAO dao;

        public AutenticacionController(AutenticacionDAO aut_dao)
        {
            dao = aut_dao;
        }

        // GET: api/<AutenticacionController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AutenticacionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AutenticacionController>
        [HttpPost("login")]
        public IActionResult IniciarSesion([FromBody] LoginRequest request)
        {
            try
            {
                var respuesta = dao.IniciarSesion(request);
                if (respuesta.IdUsuario != null)
                {
                    return Ok(new { mensaje = respuesta.Mensaje, id = respuesta.IdUsuario });
                }
                else
                {
                    return Unauthorized(new { mensaje = respuesta.Mensaje });
                }
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: Ocurrió un error durante la autenticación: ${ex.Message}");
            }
        }

        // PUT api/<AutenticacionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AutenticacionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
