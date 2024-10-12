using Data;
using Entity.Models;

namespace Business
{
    public class CategoriaService
    {
        private readonly CategoriaDAO dao;

        public CategoriaService(CategoriaDAO cate_dao)
        {
            dao = cate_dao;
        }

        // Método para listar categorias
        public List<Categoria> ListarCategorias(string nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener categorias
                var categorias = dao.ObtenerCategorias(nombre);
                return categorias;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Ocurrio un error al obtener las categorías: {ex.Message}");
            }
        }

        // Método para registrar categorias
        public string RegistrarCategorias(Categoria categoria)
        {
            try
            {
                if (categoria == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos de la categoría");
                }

                if (categoria.Nombre == null || categoria.Nombre == "")
                {
                    throw new Exception("Error: Por favor ingrese el nombre de la categoría");
                }

                var resultado = dao.NuevaCategoria(categoria);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para actualizar categorias
        public string ActualizarCategoria(Categoria categoria)
        {
            try
            {
                if (categoria == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos de la categoría");
                }

                if (categoria.IdCategoria == 0)
                {
                    throw new Exception("Error: Por favor ingrese el id de la categoría");
                }

                if (categoria.Nombre == null || categoria.Nombre == "")
                {
                    throw new Exception("Error: Por favor ingrese el nombre de la categoría");
                }

                var resultado = dao.ActualizarCatergoria(categoria);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para eliminar categorias
        public string EliminarCategoria(int idCategoria)
        {
            try
            {
                if (idCategoria == 0)
                {
                    throw new Exception("Error: Por favor ingrese el id de la categoría");
                }

                var resultado = dao.EliminarCategoria(idCategoria);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
