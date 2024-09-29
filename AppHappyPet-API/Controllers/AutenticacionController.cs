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

        // POST api/<AutenticacionController>
        [HttpPost("login")]
        public IActionResult IniciarSesion([FromBody] LoginRequest request)
        {
            try
            {
                // Validaciones
                if (request.idTipoUsuario == 0)
                {
                    return BadRequest(new { mensaje = "Por favor seleccione su tipo de usuario"});
                }
                if (request.correo == "" || request.correo == null)
                {
                    return BadRequest(new { mensaje = "Por favor ingrese su correo" });
                }
                if (request.contrasenia == "" || request.contrasenia == null)
                {
                    return BadRequest(new { mensaje = "Por favor ingrese su contraseña" });
                }

                // Mandar a llamar al método de autenticación
                var respuesta = dao.IniciarSesion(request);

                // Validar si el usuario existe
                if (respuesta.IdUsuario != null && respuesta.IdUsuario != 0)
                {
                    return Ok(new { mensaje = respuesta.Mensaje, id = respuesta.IdUsuario });
                }
                else
                {
                    return Unauthorized(new { mensaje = respuesta.Mensaje, id = respuesta.IdUsuario });
                }
            } 
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error durante la autenticación: {ex.Message}");
            }
        }

    }
}
