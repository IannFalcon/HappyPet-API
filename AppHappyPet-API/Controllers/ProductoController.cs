using AppHappyPet_API.DAO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoDAO dao;

        public ProductoController(ProductoDAO prod_dao)
        {
            dao = prod_dao;
        }

        // GET: api/<ProductoController>
        [HttpGet("{id_categoria}&{id_marca}&{nombre}")]
        public IActionResult ListarProductos(int id_categoria, int id_marca, string nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener productos
                var productos = dao.ObtenerProductos(id_categoria, id_marca, nombre);

                // Validar si existen productos
                if (productos == null || productos.Count == 0)
                {
                    return NotFound(new { mensaje = "No se encontraron productos", data = productos });
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
