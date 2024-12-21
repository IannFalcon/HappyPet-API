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
        [HttpPost("iniciar-sesion")]
        public async Task<IActionResult> IniciarSesion([FromBody] IniciarSesionRequest request)
        {
            try
            {
                var respuesta = await aut_service.IniciarSesion(request);

                if (respuesta.CambioContra == true)
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
        [HttpPost("cambiar-contrasenia")]
        public async Task<IActionResult> CambiarContrasenia([FromBody] CambiarContraseniaRequest request)
        {
            try
            {
                var respuesta = await aut_service.CambiarContrasenia(request);
                return Ok(new { mensaje = $"{respuesta.Mensaje}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<AutenticacionController>
        [HttpPost("cambiar-contrasenia-nuevo")]
        public async Task<IActionResult> CambiarContraseniaNuevoUsuario([FromBody] CambiarContraseniaNuevoUsuarioRequest request)
        {
            try
            {
                var respuesta = await aut_service.CambiarContraseniaNuevoUsuario(request);
                return Ok(new { mensaje = $"{respuesta.Mensaje} Ya puede iniciar sesión." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<AutenticacionController>
        [HttpPost("crear-cuenta")]
        public async Task<IActionResult> RegistrarClienteDesdeWeb([FromBody] CrearCuentaRequest request)
        {
            try
            {
                var respuesta = await aut_service.CrearCuenta(request);
                return Ok(new { mensaje = $"{respuesta.Mensaje} Ya puede iniciar sesión." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
