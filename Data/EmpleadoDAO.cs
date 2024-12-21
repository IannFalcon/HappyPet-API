using Entity.Models;
using Entity.Reponse;
using Entity.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class EmpleadoDAO
    {
        private string cnx = string.Empty;

        public EmpleadoDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
        }

        // Obtener empleados
        public async Task<List<DatosEmpleadoResponse>> ObtenerEmpleados(string? nro_documento, string? nombre)
        {
            // Crear lista de empleados
            List<DatosEmpleadoResponse> empleados = new List<DatosEmpleadoResponse>();

            // Query para obtener lista de empleados
            string query = @"SELECT e.id_empleado, c.id_cargo, c.nombre_cargo, e.nombres, e.apellido_paterno, e.apellido_materno, 
                            td.id_tipo_doc, td.nombre_tipo_doc, e.nro_documento, e.telefono, e.correo, e.direccion, e.fec_registro
                            FROM Empleado e
                            INNER JOIN Cargo c ON e.id_cargo = c.id_cargo
                            INNER JOIN Usuario u ON e.id_usuario = u.id_usuario
                            INNER JOIN TipoDocumento td ON e.id_tipo_doc = td.id_tipo_doc
                            WHERE u.activo = 1
                            AND (@nombre IS NULL OR CONCAT(e.nombres, ' ', e.apellido_paterno, ' ', e.apellido_materno) LIKE '%' + @nombre + '%')
                            AND (@nro_documento IS NULL OR e.nro_documento LIKE @nro_documento + '%')";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre", string.IsNullOrEmpty(nombre) ? DBNull.Value : nombre);
                    cmd.Parameters.AddWithValue("@nro_documento", string.IsNullOrEmpty(nro_documento) ? DBNull.Value : nro_documento);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    while (await dr.ReadAsync())
                    {
                        DatosEmpleadoResponse empleado = new DatosEmpleadoResponse
                        {
                            IdEmpleado = dr.GetInt32(0),
                            Cargo = new CargoEmpleadoResponse
                            {
                                IdCargo = dr.GetInt32(1),
                                NombreCargo = dr.GetString(2),
                            },
                            Nombres = dr.GetString(3),
                            ApellidoPaterno = dr.GetString(4),
                            ApellidoMaterno = dr.GetString(5),
                            TipoDocumento = new TipoDocumentoResponse
                            {
                                IdTipoDoc = dr.GetInt32(6),
                                NombreTipoDoc = dr.GetString(7),
                            },
                            NroDocumento = dr.GetString(8),
                            Telefono = dr.GetString(9),
                            Correo = dr.GetString(10),
                            Direccion = dr.GetString(11),
                            FecRegistro = dr.GetDateTime(12),
                        };

                        empleados.Add(empleado);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar lista de empleados
                    return empleados;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener empleado por id
        public async Task<DatosEmpleadoResponse> ObtenerEmpleadoId(int id_empleado)
        {
            DatosEmpleadoResponse? empleado = null;

            // Query para obtener empleado por id
            string query = @"SELECT e.id_empleado, c.id_cargo, c.nombre_cargo, e.nombres, e.apellido_paterno, e.apellido_materno, 
                            td.id_tipo_doc, td.nombre_tipo_doc, e.nro_documento, e.telefono, e.correo, e.direccion, e.fec_registro
                            FROM Empleado e
                            INNER JOIN Cargo c ON e.id_cargo = c.id_cargo
                            INNER JOIN Usuario u ON e.id_usuario = u.id_usuario
                            INNER JOIN TipoDocumento td ON e.id_tipo_doc = td.id_tipo_doc
                            WHERE e.id_empleado = @id_empleado";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_empleado", id_empleado);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    if (await dr.ReadAsync())
                    {
                        empleado = new DatosEmpleadoResponse
                        {
                            IdEmpleado = dr.GetInt32(0),
                            Cargo = new CargoEmpleadoResponse
                            {
                                IdCargo = dr.GetInt32(1),
                                NombreCargo = dr.GetString(2),
                            },
                            Nombres = dr.GetString(3),
                            ApellidoPaterno = dr.GetString(4),
                            ApellidoMaterno = dr.GetString(5),
                            TipoDocumento = new TipoDocumentoResponse
                            {
                                IdTipoDoc = dr.GetInt32(6),
                                NombreTipoDoc = dr.GetString(7),
                            },
                            NroDocumento = dr.GetString(8),
                            Telefono = dr.GetString(9),
                            Direccion = dr.GetString(10),
                            Correo = dr.GetString(11),
                            FecRegistro = dr.GetDateTime(12),
                        };
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar empleado
                    return empleado!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Nuevo empleado
        public async Task<CrudResponse> NuevoEmpleado(DatosEmpleadoRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarEmpleado",
                                                        request.IdCargo,
                                                        request.Nombres,
                                                        request.ApellidoPaterno,
                                                        request.ApellidoMaterno,
                                                        request.IdTipoDoc,
                                                        request.NroDocumento,
                                                        request.Telefono,
                                                        request.Direccion,
                                                        request.Correo,
                                                        request.NroDocumento);

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
                    throw new Exception("Error: Ocurrio un error al registrar al empleado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Actualizar empleado
        public async Task<CrudResponse> ActualizarEmpleado(DatosEmpleadoRequest request, int id_empleado)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "ActualizarEmpleado",
                                                        id_empleado,
                                                        request.IdCargo,
                                                        request.Nombres,
                                                        request.ApellidoPaterno,
                                                        request.ApellidoMaterno,
                                                        request.IdTipoDoc,
                                                        request.NroDocumento,
                                                        request.Telefono,
                                                        request.Correo,
                                                        request.Direccion);

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
                    throw new Exception("Error: Ocurrio un error al actualizar al empleado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar empleado
        public async Task<CrudResponse> EliminarEmpleado(int id_empleado)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "EliminarEmpleado", id_empleado);

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
                    throw new Exception("Error: Ocurrio un error al eliminar al empleado.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
