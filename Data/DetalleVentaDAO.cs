using Entity.Reponse;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class DetalleVentaDAO
    {
        private string cnx = string.Empty;
        private ProductoDAO dao_prod;

        public DetalleVentaDAO(IConfiguration cfg, ProductoDAO dao_prod)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
            this.dao_prod = dao_prod;
        }

        // Obtener detalles de venta
        public async Task<List<DetalleVentaResponse>> ObtenerDetallesVenta(int id_venta)
        {
            // Crear lista de detalles de venta
            List<DetalleVentaResponse> detallesVenta = new List<DetalleVentaResponse>();

            // Query para obtener detalles de venta
            string query = @"SELECT dv.cantidad, p.nombre, p.precio_unitario, dv.total
                            FROM DetalleVenta dv
                            INNER JOIN Producto p ON dv.id_producto = p.id_producto
                            WHERE id_venta = @id_venta";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@id_venta", id_venta);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    while (await dr.ReadAsync())
                    {
                        DetalleVentaResponse detalleVenta = new DetalleVentaResponse
                        {
                            Cantidad = dr.GetInt32(0),
                            NombreProducto = dr.GetString(1),
                            PrecioUnitario = dr.GetDecimal(2),
                            Total = dr.GetDecimal(3)
                        };
                        detallesVenta.Add(detalleVenta);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar detalles de venta
                    return detallesVenta;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
