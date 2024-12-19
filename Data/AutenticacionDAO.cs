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
        public async Task<IniciarSesionResponse> IniciarSesion(IniciarSesionRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "ValidarInicioSesion",
                                                       request.correo,
                                                       request.contrasenia);

                if (await dr.ReadAsync())
                {
                    var resultado = new IniciarSesionResponse
                    {
                        IdUsuario = dr.IsDBNull(0) ? null : dr.GetInt32(0),
                        IdCliente = dr.IsDBNull(1) ? null : dr.GetInt32(1),
                        Rol = dr.IsDBNull(2) ? null : dr.GetString(2),
                        NombreUsuario = dr.IsDBNull(3) ? null : dr.GetString(3),
                        CambioContra = dr.IsDBNull(4) ? null : dr.GetBoolean(4),
                        MensajeError = dr.IsDBNull(5) ? null : dr.GetString(5),
                    };

                    return resultado;
                }
                else
                {
                    throw new Exception("Error: Ocurrio un error durante el inicio de sesión.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Cambio de contraseña
        public async Task<AutenticacionResponse> CambiarContrasenia(CambiarContraseniaRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "CambiarContrasenia",
                                                       request.IdUsuario,
                                                       request.ContraseniaActual,
                                                       request.NuevaContrasenia,
                                                       request.ConfirmarContrasenia);

                if (await dr.ReadAsync())
                {
                    var resultado = new AutenticacionResponse
                    {
                        Exito = dr.GetInt32(0),
                        Mensaje = dr.GetString(1),
                    };

                    return resultado;
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

        // Cambio de contraseña para usuarios registrados desde la vista del administrador
        public async Task<AutenticacionResponse> CambiarContraseniaNuevoUsuario(CambiarContraseniaNuevoUsuarioRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "CambiarContraseniaNuevoUsuario",
                                                       request.IdUsuario,
                                                       request.NuevaContrasenia,
                                                       request.ConfirmarContrasenia);

                if (await dr.ReadAsync())
                {
                    var resultado = new AutenticacionResponse
                    {
                        Exito = dr.GetInt32(0),
                        Mensaje = dr.GetString(1),
                    };

                    return resultado;
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
        public async Task<AutenticacionResponse> CrearCuenta(CrearCuentaRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "CrearCuenta",
                                                        request.Nombres,
                                                        request.ApellidoPaterno,
                                                        request.ApellidoMaterno,
                                                        request.IdTipoDoc,
                                                        request.NroDocumento,
                                                        request.Telefono,
                                                        request.Correo,
                                                        request.Contrasenia,
                                                        request.ConfirmarContrasenia);

                if (await dr.ReadAsync())
                {
                    var resultado = new AutenticacionResponse
                    {
                        Exito = dr.GetInt32(0),
                        Mensaje = dr.GetString(1),
                    };

                    return resultado;
                }
                else
                {
                    throw new Exception("Error: Ocurrio un durante la creación de su cuenta.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
