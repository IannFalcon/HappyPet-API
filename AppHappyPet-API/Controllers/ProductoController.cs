using AppHappyPet_API.DAO;
using AppHappyPet_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoDAO dao;

        public ProductoController(ProductoDAO dao)
        {
            this.dao = dao;
        }

        // GET: api/<ProductoController>
        [HttpGet]
        public IActionResult ListarProductos([FromQuery] int? id_categoria, [FromQuery] int? id_marca, [FromQuery] string? nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener productos
                var productos = dao.ObtenerProductos(id_categoria, id_marca, nombre);

                // Validar si existen productos
                if (productos == null || productos.Count == 0)
                {
                    return Ok(new { mensaje = "No se encontraron productos", data = productos });
                }

                return Ok(new { mensaje = "Productos encontrados", data = productos });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener los productos: {ex.Message}");
            }
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id_producto}")]
        public IActionResult ObtenerProductoPorId(int id_producto)
        {
            try
            {
                // Mandar a llamar al método de obtener productos
                var producto = dao.ObtenerProductoPorId(id_producto);

                // Validar si existen productos
                if (producto == null)
                {
                    return NotFound(new { mensaje = "Producto no encontrado", data = producto });
                }

                return Ok(new { mensaje = $"Se encontro el producto con id: {id_producto}", data = producto });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener los datos del producto: {ex.Message}");
            }
        }

        // POST api/<ProductoController>
        [HttpPost]
        public IActionResult RegistrarProductos([FromBody] Producto producto)
        {
            try
            {
                if (producto.Nombre == null || producto.Nombre == "")
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese en nombre del producto" });
                }

                if (producto.IdCategoria == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese la categoria del producto" });
                }

                if (producto.IdMarca == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese la marca del producto" });
                }

                if (producto.Descripcion == null || producto.Descripcion == "")
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese la descripcion del producto" });
                }

                if (producto.PrecioUnitario == 0 )
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el precio del producto" });
                }

                if (producto.Stock == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el stock del producto" });
                }

                // Mandar a llamar al método de registrar productos
                var resultado = dao.NuevoProducto(producto);

                // Obtener resultado
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al registrar el producto: {ex.Message}");
            }
        }

        // PUT api/<ProductoController>/5
        [HttpPut]
        public IActionResult ActualizarProducto([FromBody] Producto producto)
        {
            try
            {
                if (producto == null)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese los datos del producto" });
                }
                if (producto.IdProducto == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el id del producto" });
                }

                if (producto.Nombre == null || producto.Nombre == "")
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese en nombre del producto" });
                }

                if (producto.IdCategoria == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese la categoria del producto" });
                }

                if (producto.IdMarca == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese la marca del producto" });
                }

                if (producto.Descripcion == null || producto.Descripcion == "")
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese la descripcion del producto" });
                }

                if (producto.PrecioUnitario == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el precio del producto" });
                }

                if (producto.Stock == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el stock del producto" });
                }

                // Mandar a llamar al método de actualizar productos
                var resultado = dao.ActualizarProducto(producto);

                // Obtener resultado
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al actualizar el producto: {ex.Message}");
            }
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
