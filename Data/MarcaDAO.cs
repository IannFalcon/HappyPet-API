using Entity.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class MarcaDAO
    {
        private string cnx = string.Empty;

        public MarcaDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
        }

        // Obtener marcas
        public List<Marca> ObtenerMarcas(string? nombre)
        {
            // Crear lista de marcas
            List<Marca> marcas = new List<Marca>();

            // Query para obtener marcas
            string query = @"SELECT id_marca, nombre FROM Marca
                            WHERE (@nombre IS NULL OR nombre LIKE '%' + @nombre + '%')";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre", string.IsNullOrEmpty(nombre) ? DBNull.Value : nombre);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    while (dr.Read())
                    {
                        Marca marca = new Marca
                        {
                            IdMarca = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                        };

                        marcas.Add(marca);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar lista de marcas
                    return marcas;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener marca por id
        public Marca ObtenerMarcaPorId(int id_categoria)
        {
            Marca? categoria = null;

            // Query para obtener marca por id
            string query = "SELECT id_marca, nombre FROM Marca WHERE id_marca = @id_marca";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_marca", id_categoria);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    if (dr.Read())
                    {
                        categoria = new Marca
                        {
                            IdMarca = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                        };
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar marca
                    return categoria!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Nueva marca
        public string NuevaMarca(Marca marca)
        {
            // Query para insertar marca
            string query = "INSERT INTO Marca (nombre) VALUES (@nombre)";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre", marca.Nombre);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    cmd.ExecuteNonQuery();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de éxito
                    return $"La marca {marca.Nombre} fue registrada correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Actualizar marca
        public string ActualizarMarca(Marca marca)
        {
            // Query para actualizar marca
            string query = "UPDATE Marca SET nombre = @nombre WHERE id_marca = @id_marca";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre", marca.Nombre);
                    cmd.Parameters.AddWithValue("@id_marca", marca.IdMarca);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    cmd.ExecuteNonQuery();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de éxito
                    return $"La marca {marca.Nombre} fue actualizada correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar marca
        public string EliminarMarca(int id_marca)
        {
            // Query para eliminar marca
            string query = "DELETE FROM Marca WHERE id_marca = @id_marca";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_marca", id_marca);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    cmd.ExecuteNonQuery();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de éxito
                    return $"La marca con id {id_marca} fue eliminada correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
