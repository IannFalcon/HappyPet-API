using AppHappyPet_API.Models;
using Microsoft.Data.SqlClient;

namespace AppHappyPet_API.DAO
{
    public class VendedorDAO
    {
        private string cnx = string.Empty;

        public VendedorDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd");
        }

        // Obtener vendedores
        public List<Usuario> ObtenerVendedores(string? nro_documento, string? nombre)
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

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Agregar parámetros al comando
                cmd.Parameters.AddWithValue("@nro_documento", string.IsNullOrEmpty(nro_documento) ? (object)DBNull.Value : nro_documento);
                cmd.Parameters.AddWithValue("@nombre", string.IsNullOrEmpty(nombre) ? (object)DBNull.Value : nombre);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                SqlDataReader dr = cmd.ExecuteReader();

                // Leer resultados
                while (dr.Read())
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

        // Nuevo vendedor
        public string NuevoVendedor(Usuario vendedor)
        {
            // Query para insertar vendedor
            string query = @"INSERT INTO Usuario (id_tipo_usuario, nombre, apellido_paterno, apellido_materno, id_tipo_documento, nro_documento, telefono, direccion, correo, contrasenia)
                            VALUES (2, @nombre, @apellido_paterno, @apellido_materno, @id_tipo_documento, @nro_documento, @telefono, @direccion, @correo, @contrasenia)";

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
                cmd.Parameters.AddWithValue("@contrasenia", vendedor.NroDocumento);

                // Abrir conexión
                con.Open();

                // Ejecutar query
                cmd.ExecuteNonQuery();

                // Cerrar conexión
                con.Close();

                // Retornar mensaje de éxito
                return $"El vendedor {vendedor.Nombre} {vendedor.ApellidoPaterno} {vendedor.ApellidoMaterno} ha sido registrado exitosamente";
            }
        }
    }
}
