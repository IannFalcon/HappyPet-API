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

                if (respuesta == "NO_COINCIDE")
                {
                    throw new Exception("Error: Las contraseñas no coinciden.");
                }

                if (respuesta == "IGUAL_DNI")
                {
                    throw new Exception("Error: La nueva contraseña no puede ser igual a su numero de documento.");
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para crear una cuenta
        public string CrearCuenta(Usuario cliente)
        {
            try
            {
                if (cliente == null)
                {
                    throw new Exception("Error: Por favor ingrese sus datos");
                }

                if (cliente.Nombre == null || cliente.Nombre == "")
                {
                    throw new Exception("Error: El nombre es requerido");
                }

                if (cliente.ApellidoPaterno == null || cliente.ApellidoPaterno == "")
                {
                    throw new Exception("Error: El apellido paterno es requerido");
                }

                if (cliente.ApellidoMaterno == null || cliente.ApellidoMaterno == "")
                {
                    throw new Exception("Error: El apellido materno es requerido");
                }

                if (cliente.IdTipoDocumento == 0)
                {
                    throw new Exception("Error: El tipo de documento es requerido");
                }

                if (cliente.NroDocumento == null || cliente.NroDocumento == "")
                {
                    throw new Exception("Error: El número de documento es requerido");
                }

                if (cliente.Telefono == null || cliente.Telefono == "")
                {
                    throw new Exception("Error: El teléfono es requerido");
                }

                if (cliente.Direccion == null || cliente.Direccion == "")
                {
                    throw new Exception("Error: La dirección es requerida");
                }

                if (cliente.Correo == null || cliente.Correo == "")
                {
                    throw new Exception("Error: El correo es requerido");
                }

                if (cliente.Contrasenia == null || cliente.Contrasenia == "")
                {
                    throw new Exception("Error: La contraseña es requerida");
                }

                var respuesta = dao.CrearCuenta(cliente);

                if (respuesta == "DNI_EXISTE")
                {
                    throw new Exception("Error: El número de documento ya existe.");
                }

                if (respuesta == "TEL_EXISTE")
                {
                    throw new Exception("Error: El numero de teléfono ya existe.");
                }

                if (respuesta == "CORREO_EXISTE")
                {
                    throw new Exception("Error: El correo ya existe.");
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
