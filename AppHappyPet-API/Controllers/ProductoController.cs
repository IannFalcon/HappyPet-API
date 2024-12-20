using Business;
using Entity.Models;
using Entity.Request;
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
        [HttpGet("listar")]
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
        [HttpGet("obtener/{id_producto}")]
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

        // GET: api/<ProductoController>
        [HttpGet("listar-ingreso")]
        public async Task<IActionResult> ListarIngresoProductos([FromQuery] string? ruc_proveedor, [FromQuery] string? nombre_proveedor, [FromQuery] DateTime? fecha_ingreso)
        {
            try
            {
                // Mandar a llamar al método de obtener productos
                var productos = await prod_service.ListarIngresoProductos(ruc_proveedor, nombre_proveedor, fecha_ingreso);
                return Ok(new { mensaje = "Registros encontrados", data = productos });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<ProductoController>
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarProducto([FromBody] RegistrarProductoRequest producto)
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

        // POST api/<ProductoController>
        [HttpPost("registrar-ingreso")]
        public async Task<IActionResult> RegistrarIngresoProducto([FromBody] IngresoProductoRequest ingreso)
        {
            try
            {
                var resultado = await prod_service.RegistrarIngresoProducto(ingreso);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<ProductoController>/5
        [HttpPut("actualizar/{id_producto}")]
        public async Task<IActionResult> ActualizarProducto([FromBody] ActualizarProductoRequest producto, int id_producto)
        {
            try
            {
                var resultado = await prod_service.ActualizarProducto(producto, id_producto);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("eliminar/{id_producto}")]
        public async Task<IActionResult> EliminarProducto(int id_producto)
        {
            try
            {
                var resultado = await prod_service.EliminarProducto(id_producto);
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

        // GET api/<ProductoController>/5
        [HttpGet("exportar-lista-ingreso")]
        public async Task<IActionResult> ExportarIngresoProductos()
        {
            try
            {
                var content = await prod_service.ExportarListaIngresoProductos();
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                var nombreArchivo = $"Ingreso-Productos-{fechaActual}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
