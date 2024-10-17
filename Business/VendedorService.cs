using Data;
using Entity.Models;

namespace Business
{
    public class VendedorService
    {
        private readonly VendedorDAO dao_ven;

        public VendedorService(VendedorDAO dao_ven)
        {
            this.dao_ven = dao_ven;
        }

        // Método para listar vendedores
        public List<Usuario> ListarVendedores(string? nro_documento, string? nombre)
        {
            try
            {
                var listado = dao_ven.ObtenerVendedores(nro_documento, nombre);
                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Ocurrió un error al obtener los vendedores: {ex.Message}");
            }
        }

        // Método para obtener un vendedor por ID
        public Usuario ObtenerVendedorId(int idUsuario)
        {
            try
            {
                var vendedor = dao_ven.ObtenerVendedorId(idUsuario);

                if (vendedor == null)
                {
                    throw new Exception($"El vendedor con ID: {idUsuario} no fue encontrado.");
                }

                return vendedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para registrar un vendedor
        public string RegistrarVendedor(Usuario vendedor)
        {
            try
            {
                if (vendedor == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos del vendedor");
                }

                if (vendedor.Nombre == null || vendedor.Nombre == "")
                {
                    throw new Exception("Error: El nombre del vendedor es requerido");
                }

                if (vendedor.ApellidoPaterno == null || vendedor.ApellidoPaterno == "")
                {
                    throw new Exception("Error: El apellido paterno del vendedor es requerido");
                }

                if (vendedor.ApellidoMaterno == null || vendedor.ApellidoMaterno == "")
                {
                    throw new Exception("Error: El apellido materno del vendedor es requerido");
                }

                if (vendedor.IdTipoDocumento == 0)
                {
                    throw new Exception("Error: El tipo de documento del vendedor es requerido");
                }

                if (vendedor.NroDocumento == null || vendedor.NroDocumento == "")
                {
                    throw new Exception("Error: El número de documento del vendedor es requerido");
                }

                if (vendedor.Telefono == null || vendedor.Telefono == "")
                {
                    throw new Exception("Error: El teléfono del vendedor es requerido");
                }

                if (vendedor.Direccion == null || vendedor.Direccion == "")
                {
                    throw new Exception("Error: La dirección del vendedor es requerida");
                }

                if (vendedor.Correo == null || vendedor.Correo == "")
                {
                    throw new Exception("Error: El correo del vendedor es requerido");
                }

                var resultado = dao_ven.NuevoVendedor(vendedor);

                if (resultado == "DNI_EXISTE")
                {
                    throw new Exception("Error: El número de documento ya existe.");
                }

                if (resultado == "TEL_EXISTE")
                {
                    throw new Exception("Error: El numero de teléfono ya existe.");
                }

                if (resultado == "CORREO_EXISTE")
                {
                    throw new Exception("Error: El correo ya existe.");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para actualizar un vendedor
        public string ActualizarVendedor(Usuario vendedor)
        {
            try
            {
                if (vendedor == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos del vendedor");
                }

                if (vendedor.IdUsuario == 0)
                {
                    throw new Exception("Error: El id del vendedor es requerido");
                }

                if (vendedor.Nombre == null || vendedor.Nombre == "")
                {
                    throw new Exception("Error: El nombre del vendedor es requerido");
                }

                if (vendedor.ApellidoPaterno == null || vendedor.ApellidoPaterno == "")
                {
                    throw new Exception("Error: El apellido paterno del vendedor es requerido");
                }

                if (vendedor.ApellidoMaterno == null || vendedor.ApellidoMaterno == "")
                {
                    throw new Exception("Error: El apellido materno del vendedor es requerido");
                }

                if (vendedor.IdTipoDocumento == 0)
                {
                    throw new Exception("Error: El tipo de documento del vendedor es requerido");
                }

                if (vendedor.NroDocumento == null || vendedor.NroDocumento == "")
                {
                    throw new Exception("Error: El número de documento del vendedor es requerido");
                }

                if (vendedor.Telefono == null || vendedor.Telefono == "")
                {
                    throw new Exception("Error: El teléfono del vendedor es requerido");
                }

                if (vendedor.Direccion == null || vendedor.Direccion == "")
                {
                    throw new Exception("Error: La dirección del vendedor es requerida");
                }

                if (vendedor.Correo == null || vendedor.Correo == "")
                {
                    throw new Exception("Error: El correo del vendedor es requerido");
                }

                var resultado = dao_ven.ActualizarVendedor(vendedor);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para eliminar un vendedor
        public string EliminarVendedor(int idUsuario)
        {
            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("Error: El id del vendedor es requerido");
                }

                var respuesta = dao_ven.EliminarVendedor(idUsuario);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
