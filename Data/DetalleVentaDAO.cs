using Entity.Models;
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
        public async Task<List<DetalleVenta>> ObtenerDetallesVenta(int id_venta)
        {
            // Crear lista de detalles de venta
            List<DetalleVenta> detallesVenta = new List<DetalleVenta>();

            // Query para obtener detalles de venta
            string query = @"SELECT * FROM DetalleVenta
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
                        DetalleVenta detalleVenta = new DetalleVenta
                        {
                            IdDetalleVenta = dr.GetInt32(0),
                            IdVenta = dr.GetInt32(1),
                            IdProducto = dr.GetInt32(2),
                            Cantidad = dr.GetInt32(3),
                            Total = dr.GetDecimal(4),
                            ProductoDetalle = await dao_prod.ObtenerProductoPorId(dr.GetInt32(2))
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
