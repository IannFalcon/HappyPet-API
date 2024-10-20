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
        public async Task<IActionResult> ListarVendedores([FromQuery] string? nro_documento, [FromQuery] string? nombre)
        {
            try
            {
                var vendedores = await ven_service.ListarVendedores(nro_documento, nombre);
                return Ok(new { mensaje = "Vendedores encontrados", data = vendedores });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message});
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> ObtenerVendedorId(int idUsuario)
        {
            try
            {
                var vendedor = await ven_service.ObtenerVendedorId(idUsuario);
                return Ok(new { mensaje = "Vendedor encontrado", data = vendedor });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<IActionResult> RegistrarVendedor([FromBody] Usuario usuario)
        {
            try
            {
                var resultado = await ven_service.RegistrarVendedor(usuario);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut]
        public async Task<IActionResult> ActualizarVendedor([FromBody] Usuario usuario)
        {
            try
            {
                var resultado = await ven_service.ActualizarVendedor(usuario);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{idUsuario}")]
        public async Task<IActionResult> EliminarVendedor(int idUsuario)
        {
            try
            {
                var resultado = await ven_service.EliminarVendedor(idUsuario);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("exportar")]
        public async Task<IActionResult> ExportarVendedores()
        {
            try
            {
                var content = await ven_service.ExportarListadoVendedores();
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                var nombreArchivo = $"Listado-Vendedores-{fechaActual}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
