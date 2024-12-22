using Azure.Core;
using Data;
using Entity.Reponse;
using Entity.Request;

namespace Business
{
    public class ClienteDireccionService
    {
        private readonly ClienteDireccionDAO dao_cliente_direccion;

        public ClienteDireccionService(ClienteDireccionDAO dao_cliente_direccion)
        {
            this.dao_cliente_direccion = dao_cliente_direccion;
        }

        public async Task<List<DireccionClienteResponse>> ObtenerDireccionesCliente(int id_cliente)
        {
            return await dao_cliente_direccion.ObtenerDirecciones(id_cliente);
        }

        public async Task<string> NuevaDireccionCliente(DatosDireccionRequest direccion)
        {
            try
            {
                if (direccion == null)
                {
                    throw new Exception("La dirección no puede ser nula");
                }

                if (string.IsNullOrEmpty(direccion.NombreDireccion))
                {
                    throw new Exception("El nombre de la dirección no puede estar vacío");
                }

                if (string.IsNullOrEmpty(direccion.Pais))
                {
                    throw new Exception("El país no puede estar vacío");
                }

                if (string.IsNullOrEmpty(direccion.Ciudad))
                {
                    throw new Exception("La ciudad no puede estar vacía");
                }

                if (string.IsNullOrEmpty(direccion.Direccion))
                {
                    throw new Exception("La dirección no puede estar vacía");
                }

                if (string.IsNullOrEmpty(direccion.CodigoPostal))
                {
                    throw new Exception("El código postal no puede estar vacío");
                }

                return await dao_cliente_direccion.NuevaDireccion(direccion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
