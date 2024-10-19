using Entity.Models;
using Entity.Reponse;
using Entity.Request;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class AutenticacionDAO
    {
        private string cnx = string.Empty;

        public AutenticacionDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
        }

        // Validar inicio de sesión
        public async Task<AutenticacionResponse> IniciarSesion(LoginRequest loginRequest)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "ValidarInicioSesion",
                                                       loginRequest.idTipoUsuario,
                                                       loginRequest.correo,
                                                       loginRequest.contrasenia);

                if (await dr.ReadAsync())
                {
                    var resultado = new AutenticacionResponse
                    {
                        Estado = dr.GetString(0),
                        IdUsuario = dr.GetInt32(1),
                        IdTipoUsuario = dr.GetInt32(2),
                        NombreUsuario = dr.IsDBNull(3) ? null : dr.GetString(3)
                    };

                    return resultado;
                }
                else
                {
                    throw new Exception("Error: Ocurrio un error al iniciar sesión.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Cambiar contraseña
        public async Task<string> CambiarContraseniaNuevoUsuario(CambiarContraseniaRequest request)
        {
            string mensaje = string.Empty;

            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "CambiarContraseniaNuevoUsuario",
                                                       request.IdUsuario,
                                                       request.NuevaContrasenia,
                                                       request.ConfirmarContrasenia);

                if (await dr.ReadAsync())
                {
                    mensaje = dr.GetString(0);
                    return mensaje;
                }
                else
                {
                    throw new Exception("Error: Ocurrio un error al cambiar la contraseña.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Crear cuenta
        public async Task<string> CrearCuenta(Usuario usuario)
        {
            string mensaje = string.Empty;

            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarCliente",
                                                        usuario.Nombre,
                                                        usuario.ApellidoPaterno,
                                                        usuario.ApellidoMaterno,
                                                        usuario.IdTipoDocumento,
                                                        usuario.NroDocumento,
                                                        usuario.Telefono,
                                                        usuario.Direccion,
                                                        usuario.Correo,
                                                        usuario.Contrasenia!);

                if (await dr.ReadAsync())
                {
                    mensaje = dr.GetString(0);
                    return mensaje;
                }
                else
                {
                    throw new Exception("Error: Ocurrio un durante el registro.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
