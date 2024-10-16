using Business;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContadorController : ControllerBase
    {
        private readonly ContadoresService cont_service;

        public ContadorController(ContadoresService cont_service)
        {
            this.cont_service = cont_service;
        }

        // GET: api/<ContadorController>
        [HttpGet("Ventas")]
        public IActionResult ObtenerTotalVentas()
        {
            var resultado = cont_service.ObtenerTotalVentas();
            return Ok(new { data = resultado });
        }

        [HttpGet("Productos")]
        public IActionResult ObtenerTotalProductos()
        {
            var resultado = cont_service.ObtenerTotalProductos();
            return Ok(new { data = resultado });
        }

        [HttpGet("Categorias")]
        public IActionResult ObtenerTotalCategorias()
        {
            var resultado = cont_service.ObtenerTotalCategorias();
            return Ok(new { data = resultado });
        }

        [HttpGet("Marcas")]
        public IActionResult ObtenerTotalMarcas()
        {
            var resultado = cont_service.ObtenerTotalMarcas();
            return Ok(new { data = resultado });
        }

        [HttpGet("Usuarios")]
        public IActionResult ObtenerTotalUsuarios()
        {
            var resultado = cont_service.ObtenerTotalUsuarios();
            return Ok(new { data = resultado });
        }

        [HttpGet("Clientes")]
        public IActionResult ObtenerTotalClientes()
        {
            var resultado = cont_service.ObtenerTotalClientes();
            return Ok(new { data = resultado });
        }

        [HttpGet("Vendedores")]
        public IActionResult ObtenerTotalVendedores()
        {
            var resultado = cont_service.ObtenerTotalVendedores();
            return Ok(new { data = resultado });
        }

    }
}
