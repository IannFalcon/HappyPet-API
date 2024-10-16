using Data;
using Entity.Reponse;

namespace Business
{
    public class ContadoresService
    {
        private readonly ContadoresDAO dao_contador;

        public ContadoresService(ContadoresDAO dao_contador)
        {
            this.dao_contador = dao_contador;
        }

        public ContadorVentasResponse ObtenerTotalVentas()
        {
            return dao_contador.ObtenerTotalVentas();
        }

        public int ObtenerTotalProductos()
        {
            return dao_contador.ObtenerTotalProductos();
        }

        public int ObtenerTotalCategorias()
        {
            return dao_contador.ObtenerTotalCategorias();
        }

        public int ObtenerTotalMarcas()
        {
            return dao_contador.ObtenerTotalMarcas();
        }

        public int ObtenerTotalUsuarios()
        {
            return dao_contador.ObtenerTotalUsuarios();
        }

        public int ObtenerTotalClientes()
        {
            return dao_contador.ObtenerTotalClientes();
        }

        public int ObtenerTotalVendedores()
        {
            return dao_contador.ObtenerTotalVendedores();
        }
    }
}
