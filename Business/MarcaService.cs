using Data;
using Entity.Models;

namespace Business
{
    public class MarcaService
    {
        private readonly MarcaDAO dao;

        public MarcaService(MarcaDAO marca_dao)
        {
            dao = marca_dao;
        }

        // Método para listar marcas
        public async Task<List<Marca>> ListarMarcas(string nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener marcas
                var marcas = await dao.ObtenerMarcas(nombre);
                return marcas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // Método para registrar marcas
        public async Task<string> RegistrarMarcas(Marca marca)
        {
            try
            {
                if (marca == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos de la marca");
                }

                if (marca.Nombre == null || marca.Nombre == "")
                {
                    throw new Exception("Error: Por favor ingrese el nombre de la marca");
                }

                var resultado = await dao.NuevaMarca(marca);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para actualizar marcas
        public async Task<string> ActualizarMarca(Marca marca)
        {
            try
            {
                if (marca == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos de la marca");
                }
                if (marca.IdMarca == 0)
                {
                    throw new Exception("Error: Por favor ingrese el id de la marca");
                }
                if (marca.Nombre == null || marca.Nombre == "")
                {
                    throw new Exception("Error: Por favor ingrese el nombre de la marca");
                }

                var resultado = await dao.ActualizarMarca(marca);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para eliminar marcas
        public async Task<string> EliminarMarca(int idMarca)
        {
            try
            {
                if (idMarca == 0)
                {
                    throw new Exception("Error: Por favor ingrese el id de la marca");
                }

                var resultado = await dao.EliminarMarca(idMarca);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
