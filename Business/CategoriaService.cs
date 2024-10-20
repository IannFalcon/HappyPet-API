using ClosedXML.Excel;
using Data;
using Entity.Models;
using System.Drawing;

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
        public async Task<List<Categoria>> ListarCategorias(string nombre)
        {
            try
            {
                // Mandar a llamar al método de obtener categorias
                var categorias = await dao.ObtenerCategorias(nombre);
                return categorias;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Ocurrio un error al obtener las categorías: {ex.Message}");
            }
        }

        // Método para registrar categorias
        public async Task<string> RegistrarCategorias(Categoria categoria)
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

                var resultado = await dao.NuevaCategoria(categoria);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para actualizar categorias
        public async Task<string> ActualizarCategoria(Categoria categoria)
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

                var resultado = await dao.ActualizarCatergoria(categoria);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para eliminar categorias
        public async Task<string> EliminarCategoria(int idCategoria)
        {
            try
            {
                if (idCategoria == 0)
                {
                    throw new Exception("Error: Por favor ingrese el id de la categoría");
                }

                var resultado = await dao.EliminarCategoria(idCategoria);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para exportar categorias a un archivo Excel
        public async Task<byte[]> ExportarListadoCategorias()
        {
            try
            {
                var categorias = await dao.ObtenerCategorias(string.Empty);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Categorias");
                    worksheet.Cell(2, 2).Value = "ID Categoria";
                    worksheet.Cell(2, 3).Value = "Nombre";

                    // Aplicar estilo al encabezado
                    var headerRange = worksheet.Range("B2:C2");
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    for (int i = 0; i < categorias.Count; i++)
                    {
                        worksheet.Cell(i + 3, 2).Value = categorias[i].IdCategoria;
                        worksheet.Cell(i + 3, 3).Value = categorias[i].Nombre;

                        // Aplicar estilo a las celdas
                        worksheet.Cell(i + 3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }

                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
