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
        public AutenticacionResponse IniciarSesion(LoginRequest loginRequest)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "ValidarInicioSesion",
                                                       loginRequest.idTipoUsuario,
                                                       loginRequest.correo,
                                                       loginRequest.contrasenia);

                if (dr.Read())
                {
                    var resultado = new AutenticacionResponse
                    {
                        IdUsuario = dr.GetInt32(0),
                        IdTipoUsuario = dr.GetInt32(1),
                        NombreUsuario = dr.IsDBNull(2) ? null : dr.GetString(2)
                    };

                    if(resultado.IdUsuario == 0)
                    {
                        throw new Exception("Error: Credenciales incorrectas");
                    }

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

    }
}
