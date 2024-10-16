using Business;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly VendedorService ven_service;

        public VendedorController(VendedorService ven_service)
        {
            this.ven_service = ven_service;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public IActionResult ListarVendedores([FromQuery] string? nro_documento, [FromQuery] string? nombre)
        {
            try
            {
                var vendedores = ven_service.ListarVendedores(nro_documento, nombre);
                return Ok(new { mensaje = "Vendedores encontrados", data = vendedores });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message});
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{idUsuario}")]
        public IActionResult ObtenerVendedorId(int idUsuario)
        {
            try
            {
                var vendedor = ven_service.ObtenerVendedorId(idUsuario);
                return Ok(new { mensaje = "Vendedor encontrado", data = vendedor });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult RegistrarVendedor([FromBody] Usuario usuario)
        {
            try
            {
                var resultado = ven_service.RegistrarVendedor(usuario);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut]
        public IActionResult ActualizarVendedor([FromBody] Usuario usuario)
        {
            try
            {
                var resultado = ven_service.ActualizarVendedor(usuario);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{idUsuario}")]
        public IActionResult EliminarVendedor(int idUsuario)
        {
            try
            {
                var resultado = ven_service.EliminarVendedor(idUsuario);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
