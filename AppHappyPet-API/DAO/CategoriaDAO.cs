using AppHappyPet_API.Models;
using Microsoft.Data.SqlClient;

namespace AppHappyPet_API.DAO
{
    public class CategoriaDAO
    {
        private string cnx = string.Empty;

        public CategoriaDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd");
        }

        // Obtener categorías
        public List<Categoria> ObtenerCategorias(string? nombre)
        {
            // Crear lista de categorías
            List<Categoria> categorias = new List<Categoria>();

            // Query para obtener categorías
            string query = @"SELECT id_categoria, nombre FROM Categoria
                            WHERE (@nombre IS NULL OR nombre LIKE '%' + @nombre + '%')";

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Agregar parámetros al comando
                cmd.Parameters.AddWithValue("@nombre", string.IsNullOrEmpty(nombre) ? (object)DBNull.Value : nombre);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                SqlDataReader dr = cmd.ExecuteReader();

                // Leer resultados
                while (dr.Read())
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

        // Obtener categoria por id
        public Categoria ObtenerCategoriaPorId(int id_categoria)
        {
            Categoria? categoria = null;

            // Query para obtener categoría
            string query = "SELECT id_categoria, nombre FROM Categoria WHERE id_categoria = @id_categoria";

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
                if (dr.Read())
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

        // Nueva categoría
        public string NuevaCategoria(Categoria categoria)
        {
            // Query para insertar categoría
            string query = "INSERT INTO Categoria (nombre) VALUES (@nombre)";

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Agregar parámetros al comando
                cmd.Parameters.AddWithValue("@nombre", categoria.Nombre);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                cmd.ExecuteNonQuery();

                // Cerrar conexión
                con.Close();

                return $"La categoría {categoria.Nombre} fue registrada correctamente";
            }

        }
    }
}
