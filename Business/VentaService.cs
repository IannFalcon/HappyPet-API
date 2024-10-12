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
    }
}
