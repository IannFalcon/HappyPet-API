using Business;
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
        public IActionResult ListarCategorias([FromQuery] string? nombre)
        {
            try
            {
                var categorias = cat_service.ListarCategorias(nombre!);
                return Ok(new { mensaje = "Categorias encontradas", data = categorias });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public IActionResult RegistrarCategorias([FromBody] Categoria categoria)
        {
            try
            {
                var resultado = cat_service.RegistrarCategorias(categoria);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<CategoriaController>/5
        [HttpPut]
        public IActionResult ActualizarCategoria([FromBody] Categoria categoria)
        {
            try
            {
                var resultado = cat_service.ActualizarCategoria(categoria);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{idCategoria}")]
        public IActionResult EliminarCategoria(int idCategoria)
        {
            try
            {
                var resultado = cat_service.EliminarCategoria(idCategoria);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
