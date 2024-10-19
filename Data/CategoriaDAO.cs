using Entity.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class CategoriaDAO
    {
        private string cnx = string.Empty;

        public CategoriaDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
        }

        // Obtener categorías
        public async Task<List<Categoria>> ObtenerCategorias(string? nombre)
        {
            // Crear lista de categorías
            List<Categoria> categorias = new List<Categoria>();

            // Query para obtener categorías
            string query = @"SELECT id_categoria, nombre FROM Categoria
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
                    while (await dr.ReadAsync())
                    {
                        Categoria categoria = new Categoria
                        {
                            IdCategoria = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                        };

                        categorias.Add(categoria);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar lista de categorías
                    return categorias;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Obtener categoria por id
        public async Task<Categoria> ObtenerCategoriaPorId(int id_categoria)
        {
            Categoria? categoria = null;

            // Query para obtener categoría
            string query = "SELECT id_categoria, nombre FROM Categoria WHERE id_categoria = @id_categoria";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_categoria", id_categoria);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    if (await dr.ReadAsync())
                    {
                        categoria = new Categoria
                        {
                            IdCategoria = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                        };
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar nulo
                    return categoria!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Nueva categoría
        public async Task<string> NuevaCategoria(Categoria categoria)
        {
            // Query para insertar categoría
            string query = "INSERT INTO Categoria (nombre) VALUES (@nombre)";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    return $"La categoría {categoria.Nombre} fue registrada correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Actualizar categoría
        public async Task<string> ActualizarCatergoria(Categoria categoria)
        {
            // Query para actualizar categoría
            string query = "UPDATE Categoria SET nombre = @nombre WHERE id_categoria = @id_categoria";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);
                    cmd.Parameters.AddWithValue("@id_categoria", categoria.IdCategoria);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    return $"La categoría {categoria.Nombre} fue actualizada correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar categoría
        public async Task<string> EliminarCategoria(int id_categoria)
        {
            // Query para eliminar categoría
            string query = "DELETE FROM Categoria WHERE id_categoria = @id_categoria";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_categoria", id_categoria);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    return $"La categoría con id {id_categoria} fue eliminada correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
