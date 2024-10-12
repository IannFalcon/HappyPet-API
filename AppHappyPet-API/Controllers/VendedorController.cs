using AppHappyPet_API.DAO;
using AppHappyPet_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        private readonly VendedorDAO dao_vend;

        public VendedorController(VendedorDAO dao_vend)
        {
            this.dao_vend = dao_vend;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public IActionResult ListarVendedores([FromQuery] string? nro_documento, [FromQuery] string? nombre)
        {
            try
            {
                var vendedores = dao_vend.ObtenerVendedores(nro_documento, nombre);

                if (vendedores == null || vendedores.Count == 0)
                {
                    return Ok(new { mensaje = "No se encontraron vendedores", data = vendedores });
                }

                return Ok(new { mensaje = "Vendedores encontrados", data = vendedores });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al obtener los vendedores: {ex.Message}");
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IActionResult RegistrarVendedor([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario.Nombre == null || usuario.Nombre == "")
                {
                    return BadRequest("Error: El nombre del vendedor es requerido");
                }

                if (usuario.ApellidoPaterno == null || usuario.ApellidoPaterno == "")
                {
                    return BadRequest("Error: El apellido paterno del vendedor es requerido");
                }

                if (usuario.ApellidoMaterno == null || usuario.ApellidoMaterno == "")
                {
                    return BadRequest("Error: El apellido materno del vendedor es requerido");
                }

                if (usuario.IdTipoDocumento == 0)
                {
                    return BadRequest("Error: El tipo de documento del vendedor es requerido");
                }

                if (usuario.NroDocumento == null || usuario.NroDocumento == "")
                {
                    return BadRequest("Error: El número de documento del vendedor es requerido");
                }

                if (usuario.Telefono == null || usuario.Telefono == "")
                {
                    return BadRequest("Error: El teléfono del vendedor es requerido");
                }

                if (usuario.Direccion == null || usuario.Direccion == "")
                {
                    return BadRequest("Error: La dirección del vendedor es requerida");
                }

                if (usuario.Correo == null || usuario.Correo == "")
                {
                    return BadRequest("Error: El correo del vendedor es requerido");
                }

                var resultado = dao_vend.NuevoVendedor(usuario);

                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al registrar el vendedor: {ex.Message}");
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut]
        public IActionResult ActualizarVendedor([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario.IdUsuario == 0)
                {
                    return BadRequest("Error: El id del vendedor es requerido");
                }

                if (usuario.Nombre == null || usuario.Nombre == "")
                {
                    return BadRequest("Error: El nombre del vendedor es requerido");
                }

                if (usuario.ApellidoPaterno == null || usuario.ApellidoPaterno == "")
                {
                    return BadRequest("Error: El apellido paterno del vendedor es requerido");
                }

                if (usuario.ApellidoMaterno == null || usuario.ApellidoMaterno == "")
                {
                    return BadRequest("Error: El apellido materno del vendedor es requerido");
                }

                if (usuario.IdTipoDocumento == 0)
                {
                    return BadRequest("Error: El tipo de documento del vendedor es requerido");
                }

                if (usuario.NroDocumento == null || usuario.NroDocumento == "")
                {
                    return BadRequest("Error: El número de documento del vendedor es requerido");
                }

                if (usuario.Telefono == null || usuario.Telefono == "")
                {
                    return BadRequest("Error: El teléfono del vendedor es requerido");
                }

                if (usuario.Direccion == null || usuario.Direccion == "")
                {
                    return BadRequest("Error: La dirección del vendedor es requerida");
                }

                if (usuario.Correo == null || usuario.Correo == "")
                {
                    return BadRequest("Error: El correo del vendedor es requerido");
                }

                var resultado = dao_vend.ActualizarVendedor(usuario);

                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: Ocurrió un error al actualizar el vendedor: {ex.Message}");
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
