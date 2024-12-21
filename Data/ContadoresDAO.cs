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

        public async Task<ContadorVentasResponse> ObtenerTotalVentas()
        {

            // Query para obtener el total de ventas del mes actual
            string query = @"SELECT COUNT(*) AS TotalVentas, SUM(monto_total) AS TotalImporteVentas
                            FROM Venta 
                            WHERE MONTH(fec_venta) = MONTH(GETDATE())
                            AND YEAR(fec_venta) = YEAR(GETDATE())";

            try
            {
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
                    if (await dr.ReadAsync())
                    {
                        ContadorVentasResponse contador = new ContadorVentasResponse
                        {
                            TotalVentas = dr.GetInt32(0),
                            TotalImporteVentas = dr.IsDBNull(1) ? 0 : dr.GetDecimal(1)
                        };

                        return contador;
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar contador vacío
                    return new ContadorVentasResponse();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ObtenerTotalProductos()
        {
            int totalProductos = 0;

            try
            {
                // Query para obtener el total de productos
                string query = @"SELECT COUNT(*) FROM Producto WHERE activo = 1";

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
                    if (await dr.ReadAsync())
                    {
                        totalProductos = dr.GetInt32(0);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar contador vacío
                    return totalProductos;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ObtenerTotalCategorias()
        {
            int totalCategorias = 0;

            try
            {
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
                    if (await dr.ReadAsync())
                    {
                        totalCategorias = dr.GetInt32(0);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar contador vacío
                    return totalCategorias;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ObtenerTotalMarcas()
        {
            int totalMarcas = 0;

            try
            {
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
                    if (await dr.ReadAsync())
                    {
                        totalMarcas = dr.GetInt32(0);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar contador vacío
                    return totalMarcas;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ObtenerTotalClientes()
        {
            int totalClientes = 0;

            // Query para obtener el total de clientes
            string query = @"SELECT COUNT(*) FROM Cliente c
                            INNER JOIN Usuario u ON c.id_usuario = u.id_usuario
                            WHERE u.activo = 1";

            try
            {
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
                    if (await dr.ReadAsync())
                    {
                        totalClientes = dr.GetInt32(0);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar contador vacío
                    return totalClientes;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> ObtenerTotalEmpleados()
        {
            int totalVendedores = 0;

            // Query para obtener el total de vendedores
            string query = @"SELECT COUNT(*) FROM Empleado e
                            INNER JOIN Usuario u ON e.id_usuario = u.id_usuario
                            WHERE u.activo = 1";

            try
            {
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
                    if (await dr.ReadAsync())
                    {
                        totalVendedores = dr.GetInt32(0);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar contador vacío
                    return totalVendedores;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
