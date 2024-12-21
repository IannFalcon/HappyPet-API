using Entity.Models;
using Entity.Reponse;
using Entity.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class ProveedorDAO
    {
        private string cnx = string.Empty;

        public ProveedorDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
        }

        // Obtener proveedores
        public async Task<List<DatosProveedorResponse>> ObtenerProveedores(string? ruc, string? nombre)
        {
            // Crear lista de proveedores
            List<DatosProveedorResponse> proveedores = new List<DatosProveedorResponse>();

            // Query para obtener proveedores
            string query = @"SELECT id_proveedor, ruc_proveedor, nombre_proveedor, nro_telefono, correo, direccion, fec_registro
                            FROM Proveedor
                            WHERE activo = 1
                            AND (@ruc_proveedor IS NULL OR ruc_proveedor LIKE '%' + @ruc_proveedor + '%')
                            AND (@nombre_proveedor IS NULL OR nombre_proveedor LIKE '%' + @nombre_proveedor + '%')";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@ruc_proveedor", string.IsNullOrEmpty(ruc) ? DBNull.Value : ruc);
                    cmd.Parameters.AddWithValue("@nombre_proveedor", string.IsNullOrEmpty(nombre) ? DBNull.Value : nombre);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    while (await dr.ReadAsync())
                    {
                        DatosProveedorResponse proveedor = new DatosProveedorResponse
                        {
                            IdProveedor = dr.GetInt32(0),
                            RucProveedor = dr.GetString(1),
                            NombreProveedor = dr.GetString(2),
                            NroTelefono = dr.GetString(3),
                            Correo = dr.GetString(4),
                            Direccion = dr.GetString(5),
                            FecRegistro = dr.GetDateTime(6),
                        };

                        proveedores.Add(proveedor);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar lista de proveedores
                    return proveedores;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener proveedor por id
        public async Task<DatosProveedorResponse> ObtenerProveedorPorId(int id_proveedor)
        {
            DatosProveedorResponse? proveedor = null;

            // Query para obtener proveedor
            string query = @"SELECT id_proveedor, ruc_proveedor, nombre_proveedor, nro_telefono, correo, direccion, fec_registro
                            FROM Proveedor
                            WHERE id_proveedor = @id_proveedor";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_proveedor", id_proveedor);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    if (await dr.ReadAsync())
                    {
                        proveedor = new DatosProveedorResponse
                        {
                            IdProveedor = dr.GetInt32(0),
                            RucProveedor = dr.GetString(1),
                            NombreProveedor = dr.GetString(2),
                            NroTelefono = dr.GetString(3),
                            Correo = dr.GetString(4),
                            Direccion = dr.GetString(5),
                            FecRegistro = dr.GetDateTime(6),
                        };
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar proveedor
                    return proveedor;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Nuevo proveedor
        public async Task<CrudResponse> RegistrarProveedor(DatosProveedorRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarProveedor",
                                                        request.RucProveedor,
                                                        request.NombreProveedor,
                                                        request.NroTelefono,
                                                        request.Correo,
                                                        request.Direccion);

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
                    throw new Exception("Error: Ocurrio un error al registrar el proveedor.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Actualizar proveedor
        public async Task<CrudResponse> ActualizarProveedor(DatosProveedorRequest request, int id_proveedor)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "ActualizarProveedor",
                                                        id_proveedor,
                                                        request.RucProveedor,
                                                        request.NombreProveedor,
                                                        request.NroTelefono,
                                                        request.Correo,
                                                        request.Direccion);

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
                    throw new Exception("Error: Ocurrio un error al registrar el proveedor.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar proveedor
        public async Task<string> EliminarProveedor(int id_proveedor)
        {
            // Query para eliminar proveedor
            string query = @"UPDATE Proveedor SET activo = 0 WHERE id_proveedor = @id_proveedor";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_proveedor", id_proveedor);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de éxito
                    return "Proveedor eliminado correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
