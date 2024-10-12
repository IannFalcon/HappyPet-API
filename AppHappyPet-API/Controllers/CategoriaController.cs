using AppHappyPet_API.DAO;
using AppHappyPet_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaDAO dao;

        public CategoriaController(CategoriaDAO cate_dao)
        {
            dao = cate_dao;
        }

        // GET: api/<CategoriaController>
        [HttpGet]
        public IActionResult ListarCategorias([FromQuery] string? nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener categorias
                var categorias = dao.ObtenerCategorias(nombre);

                // Validar si existen categorias
                if (categorias == null || categorias.Count == 0)
                {
                    return Ok(new { mensaje = "No se encontraron categorias", data = categorias });
                }

                return Ok(new { mensaje = "Categorias encontradas", data = categorias });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener las categorias: {ex.Message}");
            }
        }

        // POST api/<CategoriaController>
        [HttpPost]
        public IActionResult RegistrarCategorias([FromBody] Categoria categoria)
        {
            try
            {
                if (categoria == null || categoria.Nombre == null || categoria.Nombre == "") 
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el nombre de la categoría" });
                }

                // Mandar a llamar al método de registrar categorias
                var resultado = dao.NuevaCategoria(categoria);

                // Obtener resultado
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al registrar la categoria: {ex.Message}");
            }
        }

        // PUT api/<CategoriaController>/5
        [HttpPut]
        public IActionResult ActualizarCategoria([FromBody] Categoria categoria)
        {
            try
            {
                if (categoria == null)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese los datos de la categoría" });
                }
                if (categoria.IdCategoria == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el id de la categoría" });
                }
                if (categoria.Nombre == null || categoria.Nombre == "")
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el nombre de la categoría" });
                }

                // Mandar a llamar al método de actualizar categorias
                var resultado = dao.ActualizarCatergoria(categoria);

                // Obtener resultado
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al actualizar la categoria: {ex.Message}");
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{idCategoria}")]
        public IActionResult EliminarCategoria(int idCategoria)
        {
            try
            {
                if (idCategoria == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el id de la categoría" });
                }

                // Mandar a llamar al método de eliminar categorias
                var resultado = dao.EliminarCategoria(idCategoria);

                // Obtener resultado
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al eliminar la categoria: {ex.Message}");
            }
        }
    }
}
