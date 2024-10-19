using Data;
using Entity.Models;
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
        public async Task<List<Carrito>> ListarProductosCarrito(int idUsuario)
        {
            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("Error: Por favor ingrese un idUsuario.");
                }

                var listado = await dao_carrito.ListarProductosCarrito(idUsuario);
                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Metodo para agregar o quitar productos del carrito
        public async Task<string> AgregarQuitarProductoCarrito(int idUsuario, int idProducto, bool accion)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    throw new Exception("Error: Por favor inicie sesión para continuar.");
                }

                if (idProducto <= 0)
                {
                    throw new Exception("Error: Por favor seleccione un producto.");
                }

                var resultado = await dao_carrito.AccionesCarrito(idUsuario, idProducto, accion);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
