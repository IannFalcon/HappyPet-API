using Business;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VentaService venta_service;

        public VentaController(VentaService venta_service)
        {
            this.venta_service = venta_service;
        }

        // GET: api/<VentaController>
        [HttpGet]
        public IActionResult ListarVentas()
        {
            try
            {
                var ventas = venta_service.ListarVentas();
                return Ok(new { mensaje = "Ventas encontradas", data = ventas });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<VentaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VentaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
