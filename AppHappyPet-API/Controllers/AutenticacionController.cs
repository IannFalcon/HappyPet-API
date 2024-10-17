using Business;
using Entity.Models;
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

                if (respuesta.Estado == "NO_VALIDADO")
                {
                    return Ok(new { mensaje = "Es necesario cambiar la contraseña", data = respuesta });
                }

                return Ok(new { mensaje = "Inicio de sesión exitoso", data = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<AutenticacionController>
        [HttpPost("cambiar-contrasenia-nuevo-usuario")]
        public IActionResult CambiarContraseniaNuevoUsuario([FromBody] CambiarContraseniaRequest request)
        {
            try
            {
                var respuesta = aut_service.CambiarContraseniaNuevoUsuario(request);
                return Ok(new { mensaje = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<AutenticacionController>
        [HttpPost("crear-cuenta")]
        public IActionResult RegistrarClienteDesdeWeb([FromBody] Usuario usuario)
        {
            string _mensaje = string.Empty;

            try
            {
                var respuesta = aut_service.CrearCuenta(usuario);

                if (respuesta == "EXITO")
                {
                    _mensaje = "Su cuenta ha sido creada exitosamente. Por favor inicie sesión.";
                }

                return Ok(new { mensaje = _mensaje });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
