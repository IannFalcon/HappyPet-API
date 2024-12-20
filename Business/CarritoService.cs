using Data;
using Entity.Models;
using Entity.Reponse;
using Entity.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class CarritoService
    {
        private readonly CarritoDAO dao_carrito;

        public CarritoService(CarritoDAO dao_carrito)
        {
            this.dao_carrito = dao_carrito;
        }

        // Método para listar los productos del carrito
        public async Task<List<ProductosCarritoResponse>> ListarProductosCarrito(int id_cliente)
        {
            try
            {
                if (id_cliente == 0)
                {
                    throw new Exception("Error: Por favor inicie sesión para continuar.");
                }

                var listado = await dao_carrito.ListarProductosCarrito(id_cliente);
                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Metodo para agregar o quitar productos del carrito
        public async Task<CrudResponse> AgregarQuitarProductoCarrito(OperacionesCarritoRequest request)
        {
            try
            {
                if (request.IdCliente <= 0)
                {
                    throw new Exception("Error: Por favor inicie sesión para continuar.");
                }

                if (request.IdProducto <= 0)
                {
                    throw new Exception("Error: Por favor seleccione un producto.");
                }

                if (request.Cantidad <= 0)
                {
                    throw new Exception("Error: La cantidad debe ser mayor a 0.");
                }

                var resultado = await dao_carrito.AccionesCarrito(request);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
