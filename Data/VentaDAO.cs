using Entity.Models;
using Entity.Reponse;
using Entity.Request;
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
        public async Task<CrudResponse> RealizarVenta(RegistrarVentaRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarVenta", 
                                                            request.IdCliente,
                                                            request.IdTransaccion,
                                                            request.Pais,
                                                            request.Ciudad,
                                                            request.Direccion,
                                                            request.CodigoPostal);

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
                    throw new Exception("Error: Ocurrio un error al registrar la venta.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
