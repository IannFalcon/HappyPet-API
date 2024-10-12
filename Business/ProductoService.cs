using Data;
using Entity.Models;

namespace Business
{
    public class ProductoService
    {
        private readonly ProductoDAO dao;

        public ProductoService(ProductoDAO dao)
        {
            this.dao = dao;
        }

        // Método para listar productos
        public List<Producto> ListarProductos(int? id_categoria, int? id_marca, string nombre)
        {
            try
            {
                var listado = dao.ObtenerProductos(id_categoria, id_marca, nombre);
                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para obtener un producto por ID
        public Producto ObtenerProductoPorId(int id_producto)
        {
            try
            {
                if (id_producto <= 0)
                {
                    throw new Exception("El ID del producto no es válido.");
                }

                var producto = dao.ObtenerProductoPorId(id_producto);

                if (producto == null)
                {
                    throw new Exception($"El producto con ID: {id_producto} no fue encontrado.");
                }

                return producto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para agregar un producto
        public string RegistrarProducto(Producto producto)
        {
            try
            {
                if (producto == null)
                {
                    throw new Exception("No se recibieron datos para registrar el producto.");
                }

                if (producto.Nombre == null || producto.Nombre == "")
                {
                    throw new Exception("El nombre del producto es requerido.");
                }

                if (producto.IdCategoria <= 0)
                {
                    throw new Exception("La categoría del producto es requerida.");
                }

                if (producto.IdMarca <= 0)
                {
                    throw new Exception("La marca del producto es requerida.");
                }

                if (producto.PrecioUnitario <= 0)
                {
                    throw new Exception("El precio del producto es requerido.");
                }

                if (producto.Stock <= 0)
                {
                    throw new Exception("El stock del producto es requerido.");
                }

                var resultado = dao.NuevoProducto(producto);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para actualizar un producto
        public string ActualizarProducto(Producto producto)
        {
            try
            {
                if (producto == null)
                {
                    throw new Exception("No se recibieron datos para actualizar el producto.");
                }

                if (producto.IdProducto <= 0)
                {
                    throw new Exception("El ID del producto es requerido.");
                }

                if (producto.Nombre == null || producto.Nombre == "")
                {
                    throw new Exception("El nombre del producto es requerido.");
                }

                if (producto.IdCategoria <= 0)
                {
                    throw new Exception("La categoría del producto es requerida.");
                }

                if (producto.IdMarca <= 0)
                {
                    throw new Exception("La marca del producto es requerida.");
                }

                if (producto.PrecioUnitario <= 0)
                {
                    throw new Exception("El precio del producto es requerido.");
                }

                if (producto.Stock <= 0)
                {
                    throw new Exception("El stock del producto es requerido.");
                }

                var resultado = dao.ActualizarProducto(producto);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para eliminar un producto
        public string EliminarProducto(int id_producto)
        {
            try
            {
                if (id_producto <= 0)
                {
                    throw new Exception("El ID del producto es requerido.");
                }

                var resultado = dao.EliminarProducto(id_producto);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
