using AppHappyPet_API.Models;
using Microsoft.Data.SqlClient;

namespace AppHappyPet_API.DAO
{
    public class MarcaDAO
    {
        private string cnx = string.Empty;

        public MarcaDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd");
        }

        // Obtener marcas
        public List<Marca> ObtenerMarcas(string? nombre)
        {
            // Crear lista de marcas
            List<Marca> marcas = new List<Marca>();

            // Query para obtener marcas
            string query = @"SELECT id_marca, nombre FROM Marca
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

    }
}
