using Data;
using Entity.Models;

namespace Business
{
    public class VentaService
    {
        private readonly VentaDAO dao_venta;

        public VentaService(VentaDAO dao_venta)
        {
            this.dao_venta = dao_venta;
        }

        // Metodo para listar ventas
        public List<Venta> ListarVentas()
        {
            try
            {
                var ventas = dao_venta.ObtenerVentas();
                return ventas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Ocurrió un error al obtener las ventas. ${ex.Message}");
            }
        }

        // Metodo para realizar venta
        public string RealizarVenta(int idUsuario, string idTransaccion)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    throw new Exception("Error: El id del usuario no es válido.");
                }

                if (idTransaccion == null)
                {
                    throw new Exception("Error: El id de la transacción no es válido.");
                }

                var resultado = dao_venta.RealizarVenta(idUsuario, idTransaccion);

                if (resultado == "VACIO")
                {
                    throw new Exception("Error: No existen productos en el carrito");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
