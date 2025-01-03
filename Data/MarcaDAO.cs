﻿using Entity.Reponse;
using Entity.Request;
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
        public async Task<List<MarcaResponse>> ObtenerMarcas(string? nombre)
        {
            // Crear lista de marcas
            List<MarcaResponse> marcas = new List<MarcaResponse>();

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
                    while (await dr.ReadAsync())
                    {
                        MarcaResponse marca = new MarcaResponse
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

        // Nueva marca
        public async Task<string> NuevaMarca(RegistrarMarcaCategoriaRequest marca)
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
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

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
        public async Task<string> ActualizarMarca(RegistrarMarcaCategoriaRequest marca, int id_marca)
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
                    cmd.Parameters.AddWithValue("@id_marca", id_marca);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

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
        public async Task<string> EliminarMarca(int id_marca)
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
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

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
