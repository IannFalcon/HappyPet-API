using Data;
using Entity.Reponse;
using Entity.Request;

namespace Business
{
    public class AutenticacionService
    {
        private readonly AutenticacionDAO dao;

        public AutenticacionService(AutenticacionDAO aut_dao)
        {
            dao = aut_dao;
        }

        // Método para iniciar sesión
        public AutenticacionResponse IniciarSesion(LoginRequest request)
        {
            try
            {
                // Validaciones
                if (request == null)
                {
                    throw new Exception("Error: Por favor ingrese sus credenciales.");
                }

                if (request.idTipoUsuario == 0)
                {
                    throw new Exception("Error: Por favor seleccione su tipo de usuario.");
                }

                if (request.correo == "" || request.correo == null)
                {
                    throw new Exception("Error: Por favor ingrese su correo.");
                }

                if (request.contrasenia == "" || request.contrasenia == null)
                {
                    throw new Exception("Error: Por favor ingrese su contraseña.");
                }

                // Mandar a llamar al método de autenticación
                var respuesta = dao.IniciarSesion(request);

                if (respuesta.Estado == "NO_EXISTE" && respuesta.IdUsuario == 0)
                {
                    throw new Exception("Error: Credenciales incorrectas.");
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para cambiar contraseña
        public string CambiarContraseniaNuevoUsuario(CambiarContraseniaRequest request)
        {
            try
            {
                // Validaciones
                if (request == null)
                {
                    throw new Exception("Error: Por favor ingrese sus credenciales.");
                }

                if (request.IdUsuario == 0)
                {
                    throw new Exception("Error: Por favor ingrese su id de usuario.");
                }

                if (request.NuevaContrasenia == "" || request.NuevaContrasenia == null)
                {
                    throw new Exception("Error: Por favor ingrese su nueva contraseña.");
                }

                if (request.ConfirmarContrasenia == "" || request.ConfirmarContrasenia == null)
                {
                    throw new Exception("Error: Por favor confirme su nueva contraseña.");
                }

                if (request.NuevaContrasenia != request.ConfirmarContrasenia)
                {
                    throw new Exception("Error: Las contraseñas no coinciden.");
                }

                // Mandar a llamar al método de cambio de contraseña
                var respuesta = dao.CambiarContraseniaNuevoUsuario(request);

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
