﻿using Entity.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class VendedorDAO
    {
        private string cnx = string.Empty;

        public VendedorDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
        }

        // Obtener vendedores
        public async Task<List<Usuario>> ObtenerVendedores(string? nro_documento, string? nombre)
        {
            // Crear lista de vendedores
            List<Usuario> vendedores = new List<Usuario>();

            // Query para obtener vendedores
            string query = @"SELECT id_usuario, u.id_tipo_usuario, tu.descripcion, nombre, apellido_paterno, apellido_materno,
                            u.id_tipo_documento, td.descripcion, nro_documento, telefono, direccion, correo, fec_registro 
                            FROM Usuario u
                            INNER JOIN TipoUsuario tu ON u.id_tipo_usuario = tu.id_tipo_usuario
                            INNER JOIN TipoDocumento td ON u.id_tipo_documento = td.id_tipo_documento
                            WHERE u.id_tipo_usuario = 2
                            AND u.activo = 'Si'
                            AND (@nombre IS NULL OR CONCAT(nombre, ' ', apellido_paterno, ' ', apellido_materno) LIKE '%' + @nombre + '%')
                            AND (@nro_documento IS NULL OR u.nro_documento LIKE @nro_documento + '%')";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nro_documento", string.IsNullOrEmpty(nro_documento) ? DBNull.Value : nro_documento);
                    cmd.Parameters.AddWithValue("@nombre", string.IsNullOrEmpty(nombre) ? DBNull.Value : nombre);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    while (await dr.ReadAsync())
                    {
                        Usuario vendedor = new Usuario
                        {
                            IdUsuario = dr.GetInt32(0),
                            IdTipoUsuario = dr.GetInt32(1),
                            Nombre = dr.GetString(3),
                            ApellidoPaterno = dr.GetString(4),
                            ApellidoMaterno = dr.GetString(5),
                            IdTipoDocumento = dr.GetInt32(6),
                            NroDocumento = dr.GetString(8),
                            Telefono = dr.GetString(9),
                            Direccion = dr.GetString(10),
                            Correo = dr.GetString(11),
                            FecRegistro = dr.GetDateTime(12),
                            UsuTipoUsu = new TipoUsuario
                            {
                                IdTipoUsuario = dr.GetInt32(1),
                                Descripcion = dr.GetString(2)
                            },
                            UsuTipoDoc = new TipoDocumento
                            {
                                IdTipoDocumento = dr.GetInt32(6),
                                Descripcion = dr.GetString(7)
                            }
                        };

                        vendedores.Add(vendedor);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar lista de clientes
                    return vendedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener vendedor por id
        public async Task<Usuario> ObtenerVendedorId(int id_usuario)
        {
            Usuario? usuario = null;

            // Query para obtener cliente por id
            string query = @"SELECT id_usuario, u.id_tipo_usuario, tu.descripcion, nombre, apellido_paterno, apellido_materno,
                            u.id_tipo_documento, td.descripcion, nro_documento, telefono, direccion, correo, fec_registro 
                            FROM Usuario u
                            INNER JOIN TipoUsuario tu ON u.id_tipo_usuario = tu.id_tipo_usuario
                            INNER JOIN TipoDocumento td ON u.id_tipo_documento = td.id_tipo_documento
                            WHERE u.id_tipo_usuario = 2
                            AND u.id_usuario = @id_usuario";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_usuario", id_usuario);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    if (await dr.ReadAsync())
                    {
                        usuario = new Usuario
                        {
                            IdUsuario = dr.GetInt32(0),
                            IdTipoUsuario = dr.GetInt32(1),
                            Nombre = dr.GetString(3),
                            ApellidoPaterno = dr.GetString(4),
                            ApellidoMaterno = dr.GetString(5),
                            IdTipoDocumento = dr.GetInt32(6),
                            NroDocumento = dr.GetString(8),
                            Telefono = dr.GetString(9),
                            Direccion = dr.GetString(10),
                            Correo = dr.GetString(11),
                            FecRegistro = dr.GetDateTime(12),
                            UsuTipoUsu = new TipoUsuario
                            {
                                IdTipoUsuario = dr.GetInt32(1),
                                Descripcion = dr.GetString(2)
                            },
                            UsuTipoDoc = new TipoDocumento
                            {
                                IdTipoDocumento = dr.GetInt32(6),
                                Descripcion = dr.GetString(7)
                            }
                        };
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar cliente
                    return usuario!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Nuevo vendedor
        public async Task<string> NuevoVendedor(Usuario vendedor)
        {
            string mensaje = string.Empty;

            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarVendedor",
                                                        vendedor.Nombre,
                                                        vendedor.ApellidoPaterno,
                                                        vendedor.ApellidoMaterno,
                                                        vendedor.IdTipoDocumento,
                                                        vendedor.NroDocumento,
                                                        vendedor.Telefono,
                                                        vendedor.Direccion,
                                                        vendedor.Correo,
                                                        vendedor.NroDocumento);

                if (await dr.ReadAsync())
                {
                    mensaje = dr.GetString(0);
                    return mensaje;
                }
                else
                {
                    throw new Exception("Error: Ocurrio un error al registrar al vendedor.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Actualizar vendedor
        public async Task<string> ActualizarVendedor(Usuario vendedor)
        {
            // Query para actualizar vendedor
            string query = @"UPDATE Usuario SET 
                            nombre = @nombre, 
                            apellido_paterno = @apellido_paterno, 
                            apellido_materno = @apellido_materno,
                            id_tipo_documento = @id_tipo_documento, 
                            nro_documento = @nro_documento, 
                            telefono = @telefono,
                            direccion = @direccion, 
                            correo = @correo
                            WHERE id_usuario = @id_usuario";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre", vendedor.Nombre);
                    cmd.Parameters.AddWithValue("@apellido_paterno", vendedor.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@apellido_materno", vendedor.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@id_tipo_documento", vendedor.IdTipoDocumento);
                    cmd.Parameters.AddWithValue("@nro_documento", vendedor.NroDocumento);
                    cmd.Parameters.AddWithValue("@telefono", vendedor.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", vendedor.Direccion);
                    cmd.Parameters.AddWithValue("@correo", vendedor.Correo);
                    cmd.Parameters.AddWithValue("@id_usuario", vendedor.IdUsuario);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    // Retornar cantidad de filas afectadas
                    return $"El vendedor {vendedor.Nombre} {vendedor.ApellidoPaterno} {vendedor.ApellidoMaterno} fue actualizado correctamente.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar vendedor
        public async Task<string> EliminarVendedor(int id_usuario)
        {
            // Query para eliminar vendedor
            string query = @"UPDATE Usuario SET activo = 'No' WHERE id_usuario = @id_usuario";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_usuario", id_usuario);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    // Retornar cantidad de filas afectadas
                    return $"El vendedor con ID {id_usuario} ha sido eliminado correctamente.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
