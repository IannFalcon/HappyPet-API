using Business;
using Entity.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        private readonly CarritoService cart_service;

        public CarritoController(CarritoService cart_service)
        {
            this.cart_service = cart_service;
        }

        // GET: api/<CarritoController>
        [HttpGet("productos/{id_cliente}")]
        public async Task<IActionResult> ListarProductosCarrito(int id_cliente)
        {
            try
            {
                var resultado = await cart_service.ListarProductosCarrito(id_cliente);

                if (resultado.Count == 0)
                {
                    return Ok(new { mensaje = "No se encontraron productos en el carrito", data = resultado });
                }

                return Ok(new { mensaje = "Productos encontrados", data = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<CarritoController>
        [HttpPost("acciones")]
        public async Task<IActionResult> AgregarQuitarProductosCarrito(OperacionesCarritoRequest request)
        {
            try
            {
                var resultado = await cart_service.AgregarQuitarProductoCarrito(request);
                return Ok(new { mensaje = resultado.Mensaje });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
