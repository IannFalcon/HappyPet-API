using Entity.Reponse;
using Entity.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class ClienteDireccionDAO
    {
        private string cnx = string.Empty;

        public ClienteDireccionDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
        }

        // Obtener las direcciones de un cliente
        public async Task<List<DireccionClienteResponse>> ObtenerDirecciones(int id_cliente)
        {
            List<DireccionClienteResponse> direcciones = new List<DireccionClienteResponse>();

            string query = @"SELECT id_direccion, nombre_direccion, pais, ciudad, direccion, codigo_postal FROM ClienteDireccion
                             WHERE id_cliente = @id_cliente";

            try
            {
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@id_cliente", id_cliente);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (await dr.ReadAsync())
                    {
                        DireccionClienteResponse direccion = new DireccionClienteResponse
                        {
                            IdDireccion = dr.GetInt32(0),
                            NombreDireccion = dr.GetString(1),
                            Pais = dr.GetString(2),
                            Ciudad = dr.GetString(3),
                            Direccion = dr.GetString(4),
                            CodigoPostal = dr.GetString(5)
                        };

                        direcciones.Add(direccion);
                    }

                    con.Close();

                    return direcciones;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Agregar una dirección a un cliente
        public async Task<string> NuevaDireccion(DatosDireccionRequest request)
        {
            // Query para agregar direcciones
            string query = @"INSERT INTO ClienteDireccion (id_cliente, nombre_direccion, pais, ciudad, direccion, codigo_postal)
                             VALUES (@id_cliente, @nombre_direccion, @pais, @ciudad, @direccion, @codigo_postal)";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_cliente", request.IdCliente);
                    cmd.Parameters.AddWithValue("@nombre_direccion", request.NombreDireccion);
                    cmd.Parameters.AddWithValue("@pais", request.Pais);
                    cmd.Parameters.AddWithValue("@ciudad", request.Ciudad);
                    cmd.Parameters.AddWithValue("@direccion", request.Direccion);
                    cmd.Parameters.AddWithValue("@codigo_postal", request.CodigoPostal);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de éxito
                    return $"La dirección fue registrada correctamente.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
