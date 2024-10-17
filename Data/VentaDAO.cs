using Entity.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class VentaDAO
    {
        private string cnx = string.Empty;
        private readonly ClienteDAO dao_cli;

        public VentaDAO(IConfiguration cfg, ClienteDAO dao_cli)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
            this.dao_cli = dao_cli;
        }

        // Obtener ventas
        public List<Venta> ObtenerVentas()
        {
            // Crear lista de ventas
            List<Venta> ventas = new List<Venta>();

            // Query para obtener ventas
            string query = @"SELECT * FROM Venta";

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

                    // Leer resultados
                    while (dr.Read())
                    {
                        Venta venta = new Venta
                        {
                            IdVenta = dr.GetInt32(0),
                            IdUsuario = dr.GetInt32(1),
                            TotalProductos = dr.GetInt32(2),
                            MontoTotal = dr.GetDecimal(3),
                            IdTransaccion = dr.GetString(4),
                            FecVenta = dr.GetDateTime(5),
                            UsuarioVenta = dao_cli.ObtenerClienteId(dr.GetInt32(1))
                        };

                        ventas.Add(venta);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar lista de ventas
                    return ventas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Realizar nueva venta
        public string RealizarVenta(int idUsuario, string idTransaccion)
        {
            string mensaje = string.Empty;

            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarVenta", 
                                                            idUsuario, idTransaccion);

                if (dr.Read())
                {
                    mensaje = dr.GetString(0);        
                }

                return mensaje;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
