using Business;
using Entity.Models;
using Entity.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoService emp_service;

        public EmpleadoController(EmpleadoService emp_service)
        {
            this.emp_service = emp_service;
        }

        // GET: api/<EmpleadoController>
        [HttpGet("listar")]
        public async Task<IActionResult> ListarEmpleados([FromQuery] string? nro_documento, [FromQuery] string? nombre)
        {
            try
            {
                var vendedores = await emp_service.ListarEmpleados(nro_documento, nombre);
                return Ok(new { mensaje = "Empleados encontrados", data = vendedores });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message});
            }
        }

        // GET api/<EmpleadoController>/5
        [HttpGet("obtener/{id_empleado}")]
        public async Task<IActionResult> ObtenerEmpleadoId(int id_empleado)
        {
            try
            {
                var vendedor = await emp_service.ObtenerEmpleadoId(id_empleado);
                return Ok(new { mensaje = "Empleado encontrado", data = vendedor });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<UsuarioController>
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarEmpleado([FromBody] DatosEmpleadoRequest request)
        {
            try
            {
                var resultado = await emp_service.RegistrarEmpleado(request);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("actualizar/{id_empleado}")]
        public async Task<IActionResult> ActualizarEmpleado([FromBody] DatosEmpleadoRequest request, int id_empleado)
        {
            try
            {
                var resultado = await emp_service.ActualizarEmpleado(request, id_empleado);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("eliminar/{id_empleado}")]
        public async Task<IActionResult> EliminarEmpleado(int id_empleado)
        {
            try
            {
                var resultado = await emp_service.EliminarEmpleado(id_empleado);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("exportar")]
        public async Task<IActionResult> ExportarEmpleados()
        {
            try
            {
                var content = await emp_service.ExportarListaEmpleados();
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                var nombreArchivo = $"Listado-Vendedores-{fechaActual}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
