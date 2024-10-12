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
        public IActionResult ListarProductos([FromQuery] int? id_categoria, [FromQuery] int? id_marca, [FromQuery] string? nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener productos
                var productos = prod_service.ListarProductos(id_categoria, id_marca, nombre!);
                return Ok(new { mensaje = "Productos encontrados", data = productos });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id_producto}")]
        public IActionResult ObtenerProductoPorId(int id_producto)
        {
            try
            {
                // Mandar a llamar al método de obtener productos
                var producto = prod_service.ObtenerProductoPorId(id_producto);
                return Ok(new { mensaje = $"Se encontro el producto con id: {id_producto}", data = producto });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<ProductoController>
        [HttpPost]
        public IActionResult RegistrarProductos([FromBody] Producto producto)
        {
            try
            {
                var resultado = prod_service.RegistrarProducto(producto);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<ProductoController>/5
        [HttpPut]
        public IActionResult ActualizarProducto([FromBody] Producto producto)
        {
            try
            {
                var resultado = prod_service.ActualizarProducto(producto);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{idProducto}")]
        public IActionResult EliminarProducto(int idProducto)
        {
            try
            {
                var resultado = prod_service.EliminarProducto(idProducto);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
