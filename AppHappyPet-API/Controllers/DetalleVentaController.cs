using AppHappyPet_API.DAO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {
        private readonly DetalleVentaDAO dao_dv;

        public DetalleVentaController(DetalleVentaDAO dao_dv)
        {
            this.dao_dv = dao_dv;
        }

        // GET: api/<DetalleVentaController>
        [HttpGet("{id_venta}")]
        public IActionResult ListarDetalleVenta(int id_venta)
        {
            try
            {
                var detalles_venta = dao_dv.ObtenerDetallesVenta(id_venta);

                if (detalles_venta == null || detalles_venta.Count == 0)
                {
                    return Ok(new { mensaje = "No se encontraron detalles de venta", data = detalles_venta });
                }

                return Ok(new { mensaje = "Detalles de venta encontrados", data = detalles_venta });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener los detalles de la venta. ${ex.Message}");
            }
        }
    }
}
