using Entity.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace Data
{
    public class ClienteDAO
    {
        private string cnx = string.Empty;

        public ClienteDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
        }

        // Obtener clientes
        public async Task<List<Usuario>> ObtenerClientes(string? nro_documento, string? nombre)
        {
            // Crear lista de clientes
            List<Usuario> clientes = new List<Usuario>();

            // Query para obtener clientes
            string query = @"SELECT id_usuario, u.id_tipo_usuario, tu.descripcion, nombre, apellido_paterno, apellido_materno,
                            u.id_tipo_documento, td.descripcion, nro_documento, telefono, direccion, correo, fec_registro 
                            FROM Usuario u
                            INNER JOIN TipoUsuario tu ON u.id_tipo_usuario = tu.id_tipo_usuario
                            INNER JOIN TipoDocumento td ON u.id_tipo_documento = td.id_tipo_documento
                            WHERE u.id_tipo_usuario = 1
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
                        Usuario cliente = new Usuario
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

                        clientes.Add(cliente);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar lista de clientes
                    return clientes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener cliente por id
        public async Task<Usuario> ObtenerClienteId(int id_usuario)
        {
            Usuario? usuario = null;

            // Query para obtener cliente por id
            string query = @"SELECT id_usuario, u.id_tipo_usuario, tu.descripcion, nombre, apellido_paterno, apellido_materno,
                            u.id_tipo_documento, td.descripcion, nro_documento, telefono, direccion, correo, fec_registro 
                            FROM Usuario u
                            INNER JOIN TipoUsuario tu ON u.id_tipo_usuario = tu.id_tipo_usuario
                            INNER JOIN TipoDocumento td ON u.id_tipo_documento = td.id_tipo_documento
                            WHERE u.id_tipo_usuario = 1
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

        // Nuevo cliente desde la vista de administrador
        public async Task<string> NuevoCliente(Usuario cliente)
        {
            string mensaje = string.Empty;

            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarCliente",
                                                        cliente.Nombre,
                                                        cliente.ApellidoPaterno,
                                                        cliente.ApellidoMaterno,
                                                        cliente.IdTipoDocumento,
                                                        cliente.NroDocumento,
                                                        cliente.Telefono,
                                                        cliente.Direccion,
                                                        cliente.Correo,
                                                        cliente.NroDocumento);

                if (await dr.ReadAsync())
                {
                    mensaje = dr.GetString(0);
                    return mensaje;
                }
                else
                {
                    throw new Exception("Error: Ocurrio un error al registrar el cliente.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Actualizar cliente
        public async Task<string> ActualizarCliente(Usuario cliente)
        {
            // Query para actualizar cliente
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
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@apellido_paterno", cliente.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@apellido_materno", cliente.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@id_tipo_documento", cliente.IdTipoDocumento);
                    cmd.Parameters.AddWithValue("@nro_documento", cliente.NroDocumento);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                    cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                    cmd.Parameters.AddWithValue("@id_usuario", cliente.IdUsuario);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    // Retornar cantidad de filas afectadas
                    return $"El cliente {cliente.Nombre} {cliente.ApellidoPaterno} {cliente.ApellidoMaterno} fue actualizado correctamente.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar cliente
        public async Task<string> EliminarCliente(int id_usuario)
        {
            // Query para eliminar cliente
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
                    return $"El cliente con id {id_usuario} fue eliminado correctamente.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
