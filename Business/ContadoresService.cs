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

        public async Task<ContadorVentasResponse> ObtenerTotalVentas()
        {
            return await dao_contador.ObtenerTotalVentas();
        }

        public async Task<int> ObtenerTotalProductos()
        {
            return await dao_contador.ObtenerTotalProductos();
        }

        public async Task<int> ObtenerTotalCategorias()
        {
            return await dao_contador.ObtenerTotalCategorias();
        }

        public async Task<int> ObtenerTotalMarcas()
        {
            return await dao_contador.ObtenerTotalMarcas();
        }

        public async Task<int> ObtenerTotalClientes()
        {
            return await dao_contador.ObtenerTotalClientes();
        }

        public async Task<int> ObtenerTotalEmpleados()
        {
            return await dao_contador.ObtenerTotalEmpleados();
        }
    }
}
