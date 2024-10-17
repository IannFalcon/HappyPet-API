using Data;
using Entity.Models;

namespace Business
{
    public class ClienteService
    {
        private readonly ClienteDAO dao_cliente;

        public ClienteService(ClienteDAO dao_cliente)
        {
            this.dao_cliente = dao_cliente;
        }

        // Método para listar clientes
        public List<Usuario> ListarClientes(string nro_documento, string nombre)
        {
            try
            {
                var clientes = dao_cliente.ObtenerClientes(nro_documento, nombre);
                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Ocurrió un error al obtener los clientes: {ex.Message}");
            }
        }

        // Método para obtener un cliente por ID
        public Usuario ObtenerClienteId(int idUsuario)
        {
            try
            {
                var cliente = dao_cliente.ObtenerClienteId(idUsuario);

                if (cliente == null)
                {
                    throw new Exception($"El cliente con ID: {idUsuario} no fue encontrado.");
                }

                return cliente!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para registrar un cliente desde la vista de administrador
        public string RegistrarCliente(Usuario cliente)
        {
            try
            {
                if (cliente == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos del cliente");
                }

                if (cliente.Nombre == null || cliente.Nombre == "")
                {
                    throw new Exception("Error: El nombre del cliente es requerido");
                }

                if (cliente.ApellidoPaterno == null || cliente.ApellidoPaterno == "")
                {
                    throw new Exception("Error: El apellido paterno del cliente es requerido");
                }

                if (cliente.ApellidoMaterno == null || cliente.ApellidoMaterno == "")
                {
                    throw new Exception("Error: El apellido materno del cliente es requerido");
                }

                if (cliente.IdTipoDocumento == 0)
                {
                    throw new Exception("Error: El tipo de documento del cliente es requerido");
                }

                if (cliente.NroDocumento == null || cliente.NroDocumento == "")
                {
                    throw new Exception("Error: El número de documento del cliente es requerido");
                }

                if (cliente.Telefono == null || cliente.Telefono == "")
                {
                    throw new Exception("Error: El teléfono del cliente es requerido");
                }

                if (cliente.Direccion == null || cliente.Direccion == "")
                {
                    throw new Exception("Error: La dirección del cliente es requerida");
                }

                if (cliente.Correo == null || cliente.Correo == "")
                {
                    throw new Exception("Error: El correo del cliente es requerido");
                }

                var respuesta = dao_cliente.NuevoCliente(cliente);

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

        // Método para actualizar un cliente
        public string ActualizarCliente(Usuario cliente)
        {
            try
            {
                if (cliente == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos del cliente");
                }

                if (cliente.Nombre == null || cliente.Nombre == "")
                {
                    throw new Exception("Error: El nombre del cliente es requerido");
                }

                if (cliente.ApellidoPaterno == null || cliente.ApellidoPaterno == "")
                {
                    throw new Exception("Error: El apellido paterno del cliente es requerido");
                }

                if (cliente.ApellidoMaterno == null || cliente.ApellidoMaterno == "")
                {
                    throw new Exception("Error: El apellido materno del cliente es requerido");
                }

                if (cliente.IdTipoDocumento == 0)
                {
                    throw new Exception("Error: El tipo de documento del cliente es requerido");
                }

                if (cliente.NroDocumento == null || cliente.NroDocumento == "")
                {
                    throw new Exception("Error: El número de documento del cliente es requerido");
                }

                if (cliente.Telefono == null || cliente.Telefono == "")
                {
                    throw new Exception("Error: El teléfono del cliente es requerido");
                }

                if (cliente.Direccion == null || cliente.Direccion == "")
                {
                    throw new Exception("Error: La dirección del cliente es requerida");
                }

                if (cliente.Correo == null || cliente.Correo == "")
                {
                    throw new Exception("Error: El correo del cliente es requerido");
                }

                var respuesta = dao_cliente.ActualizarCliente(cliente);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para eliminar un cliente
        public string EliminarCliente(int idUsuario)
        {
            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("Error: El id del cliente es requerido");
                }

                var respuesta = dao_cliente.EliminarCliente(idUsuario);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
