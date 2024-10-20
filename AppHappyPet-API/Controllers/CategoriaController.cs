using Business;
using ClosedXML.Excel;
using Entity.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService cat_service;

        public CategoriaController(CategoriaService cat_service)
        {
            this.cat_service = cat_service;
        }

        // GET: api/<CategoriaController>
        [HttpGet]
        public async Task<IActionResult> ListarCategorias([FromQuery] string? nombre)
        {
            try
            {
                var categorias = await cat_service.ListarCategorias(nombre!);
                return Ok(new { mensaje = "Categorias encontradas", data = categorias });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public async Task<IActionResult> RegistrarCategorias([FromBody] Categoria categoria)
        {
            try
            {
                var resultado = await cat_service.RegistrarCategorias(categoria);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<CategoriaController>/5
        [HttpPut]
        public async Task<IActionResult> ActualizarCategoria([FromBody] Categoria categoria)
        {
            try
            {
                var resultado = await cat_service.ActualizarCategoria(categoria);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{idCategoria}")]
        public async Task<IActionResult> EliminarCategoria(int idCategoria)
        {
            try
            {
                var resultado = await cat_service.EliminarCategoria(idCategoria);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<CategoriaController>/5
        [HttpGet("exportar")]
        public async Task<IActionResult> ExportarCategorias()
        {
            try
            {
                var content = await cat_service.ExportarListadoCategorias();
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                var nombreArchivo = $"Listado-Categorias-{fechaActual}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
