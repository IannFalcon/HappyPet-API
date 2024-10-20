using Business;
using Microsoft.AspNetCore.Mvc;
using PayPal;
using PayPal.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppHappyPet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VentaService venta_service;
        private readonly IConfiguration configuration;

        public VentaController(VentaService venta_service, IConfiguration configuration)
        {
            this.venta_service = venta_service;
            this.configuration = configuration;
        }

        // GET: api/<VentaController>
        [HttpGet]
        public async Task<IActionResult> ListarVentas()
        {
            try
            {
                var ventas = await venta_service.ListarVentas();
                return Ok(new { mensaje = "Ventas encontradas", data = ventas });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // GET api/<VentaController>/5
        [HttpGet("exportar")]
        public async Task<IActionResult> ExportarVentas()
        {
            try
            {
                var context = await venta_service.ExportarListadoVentas();
                var fechaActual = DateTime.Now.ToString("yyyy-MM-dd");
                var nombreArchivo = $"Listado-Ventas-{fechaActual}.xlsx";
                return File(context, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // POST api/<VentaController>
        [HttpPost("{idUsuario}/{totalPago}")]
        public async Task<IActionResult> RealizarVenta(int idUsuario, decimal totalPago)
        {
            try
            {
                // Obtener el contexto de la API de PayPal
                var apiContext = GetAPIContext();

                // Validar que el total de pago sea mayor a 0
                if (totalPago <= 0)
                {
                    return BadRequest(new { mensaje = "Por favor, agregue productos al carrito para continuar." });
                }

                // Crear el objeto de pago
                var payment = new Payment
                {
                    intent = "sale", // Tipo de pago
                    payer = new Payer { payment_method = "paypal" }, // Método de pago
                    transactions = new List<Transaction>
                    {
                        new Transaction // Detalles de la transacción
                        {
                            description = "Venta de productos",
                            amount = new Amount // Monto de la transacción
                            {
                                currency = "USD", // Tipo de moneda
                                total = totalPago.ToString("F2").Replace(",", ".") // Total de la transacción
                            }
                        }
                    },
                    redirect_urls = new RedirectUrls // Redirección de la transacción
                    {
                        return_url = "http://localhost:3000/carrito", // URL de retorno
                        cancel_url = "http://localhost:3000/cancelar-pago" // URL de cancelación
                    }
                };

                // Crear el pago
                var createdPayment = await Task.Run(() => payment.Create(apiContext)); 

                // Obtener la URL de aprobación
                var approvalUrl = createdPayment.links.FirstOrDefault(link => link.rel == "approval_url")?.href;

                // Validar si se obtuvo la URL de aprobación
                if (approvalUrl == null)
                {
                    return BadRequest(new { mensaje = "No se pudo obtener la URL de aprobación." });
                }

                // Retornar la URL de aprobación
                return Ok(new { urlAprobada = approvalUrl });
            }
            catch (PayPalException ex)
            {
                return StatusCode(500, new { mensaje = "Error al procesar el pago: " + ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpGet("ConfirmarVenta")]
        public async Task<IActionResult> ConfirmarVenta(string paymentId, string token, string PayerID, int idUsuario)
        {
            try
            {
                // Obtener el contexto de la API de PayPal
                var apiContext = GetAPIContext();

                // Crear el objeto de pago
                var payment = new Payment() { id = paymentId };

                // Obtener el pago
                var executedPayment = Payment.Get(apiContext, paymentId); // Obtener el estado del pago

                // Validar si el pago ya ha sido realizado
                if (executedPayment.state.ToLower() == "approved")
                {
                    return BadRequest(new { mensaje = "El pago ya ha sido realizado para este carrito." });
                }

                // Ejecutar el pago
                var paymentExecution = new PaymentExecution() { payer_id = PayerID };
                executedPayment = payment.Execute(apiContext, paymentExecution);

                // Validar si el pago fue aprobado
                if (executedPayment.state.ToLower() != "approved")
                {
                    return BadRequest(new { mensaje = "El pago no fue aprobado." });
                }

                // Obtener el id de la transacción
                var idTransaccion = executedPayment.id;

                // Realizar la venta
                var respuesta = await venta_service.RealizarVenta(idUsuario, idTransaccion);

                // Validar si la venta fue realizada con éxito
                if (respuesta != "EXITO")
                {
                    return BadRequest(new { mensaje = respuesta });
                }

                // Retornar mensaje de éxito
                return Ok(new { mensaje = "Su compra fue realizada con éxito." });
            }
            catch (PayPalException ex)
            {
                return StatusCode(500, new { mensaje = "Error al confirmar el pago: " + ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        private APIContext GetAPIContext()
        {
            // Obtener las credenciales de la API de PayPal
            var clientId = configuration["PayPal:ClientId"];
            var clientSecret = configuration["PayPal:ClientSecret"];
            var mode = configuration["PayPal:Mode"];

            // Configurar las credenciales de la API de PayPal
            var config = new Dictionary<string, string>
            {
                { "clientId", clientId! },
                { "clientSecret", clientSecret! },
                { "mode", mode! }
            };

            // Obtener el token de acceso
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();

            // Retornar el contexto de la API de PayPal
            return new APIContext(accessToken);
        }
    }
}
