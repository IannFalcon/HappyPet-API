using Entity.Reponse;
using Entity.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class CarritoDAO
    {
        private readonly string cnx;
        private readonly ProductoDAO dao_prod;

        public CarritoDAO(IConfiguration cfg, ProductoDAO dao_prod)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
            this.dao_prod = dao_prod;
        }

        public async Task<List<ProductosCarritoResponse>> ListarProductosCarrito(int id_cliente)
        {
            List<ProductosCarritoResponse> listado = new List<ProductosCarritoResponse>();

            // Query para listar los productos del carrito
            string query = @"SELECT c.id_producto, p.ruta_imagen, p.nombre, p.descripcion, p.precio_unitario, c.cantidad
                            FROM Carrito c
                            INNER JOIN Producto p ON c.id_producto = p.id_producto
                            WHERE c.id_cliente = @id_cliente";

            try
            {
                // Conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear un comando para ejecutar la consulta
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar el parámetro idUsuario
                    cmd.Parameters.AddWithValue("@id_cliente", id_cliente);

                    // Abrir la conexión
                    con.Open();

                    // Ejecutar la consulta
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (await dr.ReadAsync())
                    {
                        ProductosCarritoResponse carrito = new ProductosCarritoResponse
                        {
                            IdProducto = dr.GetInt32(0),
                            RutaImagen = dr.IsDBNull(1) ? null : dr.GetString(1),
                            Nombre = dr.GetString(2),
                            Descripcion = dr.GetString(3),
                            PrecioUnitario = dr.GetDecimal(4),
                            Cantidad = dr.GetInt32(5)
                        };

                        listado.Add(carrito);
                    }

                    // Cerrar la conexión
                    con.Close();

                    return listado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CrudResponse> AccionesCarrito(OperacionesCarritoRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "AccionesCarrito",
                                                    request.IdCliente,
                                                    request.IdProducto,
                                                    request.Cantidad,
                                                    request.Accion);

                if (await dr.ReadAsync())
                {
                    var resultado = new CrudResponse
                    {
                        Exito = dr.GetInt32(0),
                        Mensaje = dr.GetString(1)
                    };
                    
                    return resultado;
                }
                else
                {
                    throw new Exception("Error: No se pudo realizar la operación");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
