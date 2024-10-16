using Entity.Reponse;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class ContadoresDAO
    {
        private string cnx;

        public ContadoresDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
        }

        public ContadorVentasResponse ObtenerTotalVentas()
        {

            // Query para obtener el total de ventas del mes actual
            string query = @"SELECT COUNT(*) AS TotalVentas, SUM(monto_total) AS TotalImporteVentas
                            FROM Venta 
                            WHERE MONTH(fec_venta) = MONTH(GETDATE())
                            AND YEAR(fec_venta) = YEAR(GETDATE())";

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                SqlDataReader dr = cmd.ExecuteReader();

                // Si se encontraron resultados
                if (dr.Read())
                {
                    ContadorVentasResponse contador = new ContadorVentasResponse
                    {
                        TotalVentas = dr.GetInt32(0),
                        TotalImporteVentas = dr.GetDecimal(1)
                    };

                    return contador;
                }

                // Cerrar conexión
                con.Close();

                // Retornar contador vacío
                return new ContadorVentasResponse();
            }
        }

        public int ObtenerTotalProductos()
        {
            int totalProductos = 0;

            // Query para obtener el total de productos
            string query = @"SELECT COUNT(*) FROM Producto WHERE eliminado = 'No'";

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                SqlDataReader dr = cmd.ExecuteReader();

                // Si se encontraron resultados
                if (dr.Read())
                {
                    totalProductos = dr.GetInt32(0);
                }

                // Cerrar conexión
                con.Close();

                // Retornar contador vacío
                return totalProductos;
            }
        }

        public int ObtenerTotalCategorias()
        {
            int totalCategorias = 0;

            // Query para obtener el total de categorías
            string query = @"SELECT COUNT(*) FROM Categoria";

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                SqlDataReader dr = cmd.ExecuteReader();

                // Si se encontraron resultados
                if (dr.Read())
                {
                    totalCategorias = dr.GetInt32(0);
                }

                // Cerrar conexión
                con.Close();

                // Retornar contador vacío
                return totalCategorias;
            }
        }

        public int ObtenerTotalMarcas()
        {
            int totalMarcas = 0;

            // Query para obtener el total de marcas
            string query = @"SELECT COUNT(*) FROM Marca";

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                SqlDataReader dr = cmd.ExecuteReader();

                // Si se encontraron resultados
                if (dr.Read())
                {
                    totalMarcas = dr.GetInt32(0);
                }

                // Cerrar conexión
                con.Close();

                // Retornar contador vacío
                return totalMarcas;
            }
        }

        public int ObtenerTotalUsuarios()
        {
            int totalUsuarios = 0;

            // Query para obtener el total de usuarios
            string query = @"SELECT COUNT(*) FROM Usuario WHERE activo = 'Si'";

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                SqlDataReader dr = cmd.ExecuteReader();

                // Si se encontraron resultados
                if (dr.Read())
                {
                    totalUsuarios = dr.GetInt32(0);
                }

                // Cerrar conexión
                con.Close();

                // Retornar contador vacío
                return totalUsuarios;
            }

        }

        public int ObtenerTotalClientes()
        {
            int totalClientes = 0;

            // Query para obtener el total de clientes
            string query = @"SELECT COUNT(*) FROM Usuario 
                            WHERE id_tipo_usuario = 1
                            AND activo = 'Si'";

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                SqlDataReader dr = cmd.ExecuteReader();

                // Si se encontraron resultados
                if (dr.Read())
                {
                    totalClientes = dr.GetInt32(0);
                }

                // Cerrar conexión
                con.Close();

                // Retornar contador vacío
                return totalClientes;
            }
        }

        public int ObtenerTotalVendedores()
        {
            int totalVendedores = 0;

            // Query para obtener el total de vendedores
            string query = @"SELECT COUNT(*) FROM Usuario 
                            WHERE id_tipo_usuario = 2
                            AND activo = 'Si'";

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                SqlDataReader dr = cmd.ExecuteReader();

                // Si se encontraron resultados
                if (dr.Read())
                {
                    totalVendedores = dr.GetInt32(0);
                }

                // Cerrar conexión
                con.Close();

                // Retornar contador vacío
                return totalVendedores;
            }
        }
    }
}
