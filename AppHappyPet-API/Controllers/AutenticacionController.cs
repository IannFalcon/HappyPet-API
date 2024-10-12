using Business;
using Entity.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly AutenticacionService aut_service;

        public AutenticacionController(AutenticacionService aut_service)
        {
            this.aut_service = aut_service;
        }

        // POST api/<AutenticacionController>
        [HttpPost("login")]
        public IActionResult IniciarSesion([FromBody] LoginRequest request)
        {
            try
            {
                var respuesta = aut_service.IniciarSesion(request);
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message});
            }
        }

    }
}
