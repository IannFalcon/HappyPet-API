using AppHappyPet_API.DAO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VentaDAO dao_venta;

        public VentaController(VentaDAO dao_venta)
        {
            this.dao_venta = dao_venta;
        }

        // GET: api/<VentaController>
        [HttpGet]
        public IActionResult ListarVentas()
        {
            try
            {
                var ventas = dao_venta.ObtenerVentas();

                if (ventas == null || ventas.Count == 0)
                {
                    return Ok(new { mensaje = "No se encontraron ventas", data = ventas });
                }

                return Ok(new { mensaje = "Ventas encontradas", data = ventas });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener las ventas. ${ex.Message}");
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

        // PUT api/<VentaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VentaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
