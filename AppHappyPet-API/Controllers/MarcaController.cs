﻿using AppHappyPet_API.DAO;
using AppHappyPet_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly MarcaDAO dao;

        public MarcaController(MarcaDAO marca_dao)
        {
            dao = marca_dao;
        }

        // GET: api/<MarcaController>
        [HttpGet]
        public IActionResult ListarMarcas([FromQuery] string? nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener marcas
                var marcas = dao.ObtenerMarcas(nombre);

                // Validar si existen marcas
                if (marcas == null || marcas.Count == 0)
                {
                    return Ok(new { mensaje = "No se encontraron marcas", data = marcas });
                }

                return Ok(new { mensaje = "Marcas encontradas", data = marcas });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener las marcas: {ex.Message}");
            }

        }

        // POST api/<MarcaController>
        [HttpPost]
        public IActionResult RegistrarMarcas([FromBody] Marca marca)
        {
            try
            {
                if (marca == null || marca.Nombre == null || marca.Nombre == "")
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el nombre de la marca" });
                }

                // Mandar a llamar al método de registrar marcas
                var resultado = dao.NuevaMarca(marca);

                // Obtener resultado
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al registrar la marca: {ex.Message}");
            }
        }

        // PUT api/<MarcaController>/5
        [HttpPut]
        public IActionResult ActualizarMarca([FromBody] Marca marca)
        {
            try
            {
                if (marca == null)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese los datos de la marca" });
                }
                if (marca.IdMarca == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el id de la marca" });
                }
                if (marca.Nombre == null || marca.Nombre == "")
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el nombre de la marca" });
                }

                // Mandar a llamar al método de actualizar marca
                var resultado = dao.ActualizarMarca(marca);

                // Obtener resultado
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al actualizar la marca: {ex.Message}");
            }
        }

        // DELETE api/<MarcaController>/5
        [HttpDelete("{idMarca}")]
        public IActionResult EliminarMarca(int idMarca)
        {
            try
            {
                if (idMarca == 0)
                {
                    return BadRequest(new { mensaje = "Error: Por favor ingrese el id de la marca" });
                }

                // Mandar a llamar al método de eliminar marca
                var resultado = dao.EliminarMarca(idMarca);

                // Obtener resultado
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al eliminar la marca: {ex.Message}");
            }
        }
    }
}
