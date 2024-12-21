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
        public async Task<IActionResult> ObtenerTotalVentas()
        {
            var resultado = await cont_service.ObtenerTotalVentas();
            return Ok(new { data = resultado });
        }

        [HttpGet("Productos")]
        public async Task<IActionResult> ObtenerTotalProductos()
        {
            var resultado = await cont_service.ObtenerTotalProductos();
            return Ok(new { data = resultado });
        }

        [HttpGet("Categorias")]
        public async Task<IActionResult> ObtenerTotalCategorias()
        {
            var resultado = await cont_service.ObtenerTotalCategorias();
            return Ok(new { data = resultado });
        }

        [HttpGet("Marcas")]
        public async Task<IActionResult> ObtenerTotalMarcas()
        {
            var resultado = await cont_service.ObtenerTotalMarcas();
            return Ok(new { data = resultado });
        }

        [HttpGet("Clientes")]
        public async Task<IActionResult> ObtenerTotalClientes()
        {
            var resultado = await cont_service.ObtenerTotalClientes();
            return Ok(new { data = resultado });
        }

        [HttpGet("Empleados")]
        public async Task<IActionResult> ObtenerTotalEmpleados()
        {
            var resultado = await cont_service.ObtenerTotalEmpleados();
            return Ok(new { data = resultado });
        }

    }
}
