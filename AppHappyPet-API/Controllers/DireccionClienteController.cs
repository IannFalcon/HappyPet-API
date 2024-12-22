using Business;
using Entity.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionClienteController : ControllerBase
    {
        private readonly ClienteDireccionService direccion_service;

        public DireccionClienteController(ClienteDireccionService direccion_service)
        {
            this.direccion_service = direccion_service;
        }

        // GET: api/<DireccionClienteController>
        [HttpGet("listar/{id_cliente}")]
        public async Task<IActionResult> ListarDireccionesCliente(int id_cliente)
        {
            try
            {
                var direcciones = await direccion_service.ObtenerDireccionesCliente(id_cliente);
                return Ok(new { mensaje = "Direcciones encontrados", data = direcciones });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<DireccionClienteController>
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarDireccionCliente([FromBody] DatosDireccionRequest request)
        {
            try
            {
                var respuesta = await direccion_service.NuevaDireccionCliente(request);
                return Ok(new { mensaje = respuesta });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
