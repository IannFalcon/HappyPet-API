using Business;
using Entity.Request;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ProveedorService pro_service;

        public ProveedorController(ProveedorService pro_service)
        {
            this.pro_service = pro_service;
        }

        // GET: api/<ProveedorController>
        [HttpGet("listar")]
        public async Task<IActionResult> ListarProveedores([FromQuery] string? ruc, [FromQuery] string? nombre)
        {
            try
            {
                // Mandar a llamar al metodo para listar proveedores
                var productos = await pro_service.ListarProveedores(ruc, nombre);
                return Ok(new { mensaje = "Proveedores encontrados", data = productos });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<ProveedorController>/5
        [HttpGet("obtener/{id_proveedor}")]
        public async Task<IActionResult> ObtenerProveedorId(int id_proveedor)
        {
            try
            {
                // Mandar a llamar al método de obtener proveedores por id
                var producto = await pro_service.ObtenerProveedorPorId(id_proveedor);
                return Ok(new { mensaje = $"Se encontro el proveedor con id: {id_proveedor}", data = producto });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<ProveedorController>
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarProveedor([FromBody] DatosProveedorRequest request)
        {
            try
            {
                var resultado = await pro_service.RegistrarProveedor(request);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT api/<ProveedorController>/5
        [HttpPut("actualizar/{id_proveedor}")]
        public async Task<IActionResult> ActualizarProducto([FromBody] DatosProveedorRequest request, int id_proveedor)
        {
            try
            {
                var resultado = await pro_service.ActualizarProveedor(request, id_proveedor);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE api/<ProveedorController>/5
        [HttpDelete("eliminar/{id_proveedor}")]
        public async Task<IActionResult> EliminarProducto(int id_proveedor)
        {
            try
            {
                var resultado = await pro_service.EliminarProveedor(id_proveedor);
                return Ok(new { mensaje = resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<ProveedorController>/5
        [HttpGet("exportar")]
        public async Task<IActionResult> ExportarListadoProveedores()
        {
            try
            {
                var content = await pro_service.ExportarListadoProveedores();
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                var nombreArchivo = $"Lista-Proveedores-{fechaActual}.xlsx";
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
