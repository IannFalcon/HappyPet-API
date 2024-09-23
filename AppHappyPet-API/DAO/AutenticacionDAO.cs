using AppHappyPet_API.Reponse;
using AppHappyPet_API.Request;
using Microsoft.Data.SqlClient;

namespace AppHappyPet_API.DAO
{
    public class AutenticacionDAO
    {
        private string cnx = string.Empty;

        public AutenticacionDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd");
        }

        // Validar inicio de sesión
        public AutenticacionResponse IniciarSesion(LoginRequest loginRequest)
        {
            SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "ValidarInicioSesion", 
                                                       loginRequest.idTipoUsuario, 
                                                       loginRequest.correo, 
                                                       loginRequest.contrasenia);

            if(dr.Read())
            {
                var resultado = new AutenticacionResponse
                {
                    Mensaje = dr.GetString(0),
                    IdUsuario = dr.GetInt32(1)
                };

                return resultado;
            }
            else
            {
                return new AutenticacionResponse
                {
                    Mensaje = dr.GetString(0),
                    IdUsuario = dr.GetInt32(1)
                };
            }

        }

    }
}
