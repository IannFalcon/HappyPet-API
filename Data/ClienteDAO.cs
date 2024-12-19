using Entity.Models;
using Entity.Reponse;
using Entity.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
        public async Task<List<DatosClienteResponse>> ObtenerClientes(string? nro_documento, string? nombre)
        {
            // Crear lista de clientes
            List<DatosClienteResponse> clientes = new List<DatosClienteResponse>();

            // Query para obtener clientes
            string query = @"SELECT c.id_cliente, c.nombres, c.apellido_paterno, c.apellido_materno, td.id_tipo_doc, 
                            td.nombre_tipo_doc, c.nro_documento, c.telefono, c.correo, c.fec_registro
                            FROM Cliente c
                            INNER JOIN Usuario u ON c.id_usuario = u.id_usuario
                            INNER JOIN TipoDocumento td ON c.id_tipo_doc = td.id_tipo_doc
                            WHERE u.activo = 1
                            AND (@nombre IS NULL OR CONCAT(c.nombres, ' ', c.apellido_paterno, ' ', c.apellido_materno) LIKE '%' + @nombre + '%')
                            AND (@nro_documento IS NULL OR c.nro_documento LIKE @nro_documento + '%')";

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
                        DatosClienteResponse cliente = new DatosClienteResponse
                        {
                            IdCliente = dr.GetInt32(0),
                            Nombres = dr.GetString(1),
                            ApellidoPaterno = dr.GetString(2),
                            ApellidoMaterno = dr.GetString(3),
                            TipoDocumento = new TipoDocumentoResponse
                            {
                                IdTipoDoc = dr.GetInt32(4),
                                NombreTipoDoc = dr.GetString(5),
                            },
                            NroDocumento = dr.GetString(6),
                            Telefono = dr.GetString(7),
                            Correo = dr.GetString(8),
                            FecRegistro = dr.GetDateTime(9),
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
        public async Task<DatosClienteResponse> ObtenerClienteId(int id_cliente)
        {
            DatosClienteResponse? usuario = null;

            // Query para obtener cliente por id
            string query = @"SELECT c.id_cliente, c.nombres, c.apellido_paterno, c.apellido_materno, td.id_tipo_doc, 
                            td.nombre_tipo_doc, c.nro_documento, c.telefono, c.correo, c.fec_registro
                            FROM Cliente c
                            INNER JOIN Usuario u ON c.id_usuario = u.id_usuario
                            INNER JOIN TipoDocumento td ON c.id_tipo_doc = td.id_tipo_doc
                            WHERE c.id_cliente = @id_cliente";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_cliente", id_cliente);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    if (await dr.ReadAsync())
                    {
                        usuario = new DatosClienteResponse
                        {
                            IdCliente = dr.GetInt32(0),
                            Nombres = dr.GetString(1),
                            ApellidoPaterno = dr.GetString(2),
                            ApellidoMaterno = dr.GetString(3),
                            TipoDocumento = new TipoDocumentoResponse
                            {
                                IdTipoDoc = dr.GetInt32(4),
                                NombreTipoDoc = dr.GetString(5),
                            },
                            NroDocumento = dr.GetString(6),
                            Telefono = dr.GetString(7),
                            Correo = dr.GetString(8),
                            FecRegistro = dr.GetDateTime(9),
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

        // Nuevo cliente
        public async Task<CrudResponse> NuevoCliente(DatosClienteRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarCliente",
                                                        request.Nombres,
                                                        request.ApellidoPaterno,
                                                        request.ApellidoMaterno,
                                                        request.IdTipoDoc,
                                                        request.NroDocumento,
                                                        request.Telefono,
                                                        request.Correo);

                if (await dr.ReadAsync())
                {
                    var resultado = new CrudResponse
                    {
                        Exito = dr.GetInt32(0),
                        Mensaje = dr.GetString(1),
                    };

                    return resultado;
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
        public async Task<string> ActualizarCliente(DatosClienteRequest request, int id_cliente)
        {
            // Query para actualizar cliente
            string query = @"UPDATE Cliente SET 
                            nombres = @nombre, 
                            apellido_paterno = @apellido_paterno, 
                            apellido_materno = @apellido_materno,
                            id_tipo_doc = @id_tipo_documento, 
                            nro_documento = @nro_documento, 
                            telefono = @telefono,
                            correo = @correo
                            WHERE id_cliente = @id_cliente";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre", request.Nombres);
                    cmd.Parameters.AddWithValue("@apellido_paterno", request.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@apellido_materno", request.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@id_tipo_documento", request.IdTipoDoc);
                    cmd.Parameters.AddWithValue("@nro_documento", request.NroDocumento);
                    cmd.Parameters.AddWithValue("@telefono", request.Telefono);
                    cmd.Parameters.AddWithValue("@correo", request.Correo);
                    cmd.Parameters.AddWithValue("@id_cliente", id_cliente);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de exito
                    return $"El cliente {request.Nombres} {request.ApellidoPaterno} {request.ApellidoMaterno} fue actualizado correctamente.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar cliente
        public async Task<CrudResponse> EliminarCliente(int id_cliente)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "EliminarCliente", id_cliente);

                if (await dr.ReadAsync())
                {
                    var resultado = new CrudResponse
                    {
                        Exito = dr.GetInt32(0),
                        Mensaje = dr.GetString(1),
                    };

                    return resultado;
                }
                else
                {
                    throw new Exception("Error: Ocurrio un error al eliminar al cliente.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
