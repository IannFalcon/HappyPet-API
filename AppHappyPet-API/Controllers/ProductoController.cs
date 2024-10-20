using Business;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService prod_service;

        public ProductoController(ProductoService prod_service)
        {
            this.prod_service = prod_service;
        }

        // GET: api/<ProductoController>
        [HttpGet]
        public async Task<IActionResult> ListarProductos([FromQuery] int? id_categoria, [FromQuery] int? id_marca, [FromQuery] string? nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener productos
                var productos = await prod_service.ListarProductos(id_categoria, id_marca, nombre!);
                return Ok(new { mensaje = "Productos encontrados", data = productos });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id_producto}")]
        public async Task<IActionResult> ObtenerProductoPorId(int id_producto)
        {
            try
            {
                // Mandar a llamar al método de obtener productos
                var producto = await prod_service.ObtenerProductoPorId(id_producto);
                return Ok(new { mensaje = $"Se encontro el producto con id: {id_producto}", data = producto });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<ProductoController>
        [HttpPost]
        public async Task<IActionResult> RegistrarProductos([FromBody] Producto producto)
        {
            try
            {
                var resultado = await prod_service.RegistrarProducto(producto);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<ProductoController>/5
        [HttpPut]
        public async Task<IActionResult> ActualizarProducto([FromBody] Producto producto)
        {
            try
            {
                var resultado = await prod_service.ActualizarProducto(producto);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{idProducto}")]
        public async Task<IActionResult> EliminarProducto(int idProducto)
        {
            try
            {
                var resultado = await prod_service.EliminarProducto(idProducto);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<ProductoController>/5
        [HttpGet("exportar")]
        public async Task<IActionResult> ExportarProductos()
        {
            try
            {
                var content = await prod_service.ExportarListadoProductos();
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                var nombreArchivo = $"Listado-Productos-{fechaActual}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
