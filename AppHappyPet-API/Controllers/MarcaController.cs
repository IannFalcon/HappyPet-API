using Business;
using Entity.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly MarcaService mar_service;

        public MarcaController(MarcaService mar_service)
        {
            this.mar_service = mar_service;
        }

        // GET: api/<MarcaController>
        [HttpGet("listar")]
        public async Task<IActionResult> ListarMarcas([FromQuery] string? nombre)
        {
            try
            {
                var marcas = await mar_service.ListarMarcas(nombre!);
                return Ok(new { mensaje = "Marcas encontradas", data = marcas });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }

        // POST api/<MarcaController>
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarMarcas([FromBody] RegistrarMarcaCategoriaRequest marca)
        {
            try
            {
                var resultado = await mar_service.RegistrarMarcas(marca);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<MarcaController>/5
        [HttpPut("actualizar/{id_marca}")]
        public async Task<IActionResult> ActualizarMarca([FromBody] RegistrarMarcaCategoriaRequest marca, int id_marca)
        {
            try
            {
                var resultado = await mar_service.ActualizarMarca(marca, id_marca);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<MarcaController>/5
        [HttpDelete("eliminar/{id_marca}")]
        public async Task<IActionResult> EliminarMarca(int id_marca)
        {
            try
            {
                var resultado = await mar_service.EliminarMarca(id_marca);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<MarcaController>/5
        [HttpGet("exportar")]
        public async Task<IActionResult> ExportarMarcas()
        {
            try
            {
                var content = await mar_service.ExportarListadoMarcas();
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                var nombreArchivo = $"Listado-Marcas-{fechaActual}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
