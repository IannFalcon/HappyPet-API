using Entity.Models;
using Entity.Reponse;
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
        public async Task<List<VentaResponse>> ObtenerVentas()
        {
            // Crear lista de ventas
            List<VentaResponse> ventas = new List<VentaResponse>();

            // Query para obtener ventas
            string query = @"SELECT v.id_venta, v.id_transaccion,
	                       CONCAT(c.nombres, ' ', c.apellido_paterno, ' ', c.apellido_materno) as nombre_cliente,
	                       CONCAT(dv.pais, ', ', dv.ciudad,'. ', dv.direccion, ' ', dv.codigo_postal) as direccion_envio,
	                       v.total_productos, v.monto_total, v.fec_venta
                           FROM Venta v
                           INNER JOIN DestinoVenta dv ON v.id_destino = dv.id_destino
                           INNER JOIN Cliente c ON v.id_cliente = c.id_cliente";

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
                    while (await dr.ReadAsync())
                    {
                        VentaResponse venta = new VentaResponse
                        {
                            IdVenta = dr.GetInt32(0),
                            IdTransaccion = dr.GetString(1),
                            NombreCliente = dr.GetString(2),
                            DireccionEnvio = dr.GetString(3),
                            TotalProductos = dr.GetInt32(4),
                            MontoTotal = dr.GetDecimal(5),
                            FecVenta = dr.GetDateTime(6)
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
        public async Task<string> RealizarVenta(int idUsuario, string idTransaccion)
        {
            string mensaje = string.Empty;

            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarVenta", 
                                                            idUsuario, idTransaccion);

                if (await dr.ReadAsync())
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
