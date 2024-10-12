using Business;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase
    {
        private readonly DetalleVentaService dv_service;

        public DetalleVentaController(DetalleVentaService dv_service)
        {
            this.dv_service = dv_service;
        }

        // GET: api/<DetalleVentaController>
        [HttpGet("{id_venta}")]
        public IActionResult ListarDetalleVenta(int id_venta)
        {
            try
            {
                var detalles_venta = dv_service.ListarDetalleVenta(id_venta);
                return Ok(new { mensaje = "Detalles de venta encontrados", data = detalles_venta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
