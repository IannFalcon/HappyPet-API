using Data;
using Entity.Models;
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
        public async Task<IniciarSesionResponse> IniciarSesion(IniciarSesionRequest request)
        {
            try
            {
                // Validaciones
                if (request == null)
                {
                    throw new Exception("Error: Por favor ingrese sus credenciales.");
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
                var respuesta = await dao.IniciarSesion(request);

                if (respuesta.IdUsuario == null)
                {
                    throw new Exception($"Error: {respuesta.MensajeError}");
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para cambiar contraseña
        public async Task<AutenticacionResponse> CambiarContrasenia(CambiarContraseniaRequest request)
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

                if (request.ContraseniaActual == "" || request.ContraseniaActual == null)
                {
                    throw new Exception("Error: Por favor ingrese su contraseña actual.");
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
                var respuesta = await dao.CambiarContrasenia(request);

                if (respuesta.Exito == 0)
                {
                    throw new Exception($"Error: {respuesta.Mensaje}");
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para cambiar contraseña de un nuevo usuario creado desde la vista del administrador
        public async Task<AutenticacionResponse> CambiarContraseniaNuevoUsuario(CambiarContraseniaNuevoUsuarioRequest request)
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
                var respuesta = await dao.CambiarContraseniaNuevoUsuario(request);

                if (respuesta.Exito == 0)
                {
                    throw new Exception($"Error: {respuesta.Mensaje}");
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para crear una cuenta
        public async Task<AutenticacionResponse> CrearCuenta(CrearCuentaRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception("Error: Por favor ingrese sus datos");
                }

                if (request.Nombres == null || request.Nombres == "")
                {
                    throw new Exception("Error: El nombre es requerido");
                }

                if (request.ApellidoPaterno == null || request.ApellidoPaterno == "")
                {
                    throw new Exception("Error: El apellido paterno es requerido");
                }

                if (request.ApellidoMaterno == null || request.ApellidoMaterno == "")
                {
                    throw new Exception("Error: El apellido materno es requerido");
                }

                if (request.IdTipoDoc <= 0)
                {
                    throw new Exception("Error: El tipo de documento es requerido");
                }

                if (request.NroDocumento == null || request.NroDocumento == "")
                {
                    throw new Exception("Error: El número de documento es requerido");
                }

                if (request.Telefono == null || request.Telefono == "")
                {
                    throw new Exception("Error: El teléfono es requerido");
                }

                if (request.Correo == null || request.Correo == "")
                {
                    throw new Exception("Error: El correo es requerido");
                }

                if (request.Contrasenia == null || request.Contrasenia == "")
                {
                    throw new Exception("Error: La contraseña es requerida");
                }

                if (request.ConfirmarContrasenia == null || request.ConfirmarContrasenia == "")
                {
                    throw new Exception("Error: Es necesario confirmar su contraseña");
                }

                if (request.Contrasenia != request.ConfirmarContrasenia)
                {
                    throw new Exception("Error: Las contraseñas no coinciden");
                }

                var respuesta = await dao.CrearCuenta(request);

                if (respuesta.Exito == 0)
                {
                    throw new Exception($"Error: {respuesta.Mensaje}");
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
