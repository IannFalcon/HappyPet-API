using ClosedXML.Excel;
using Data;
using Entity.Reponse;
using Entity.Request;

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
        public async Task<List<MarcaResponse>> ListarMarcas(string nombre)
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
        public async Task<string> RegistrarMarcas(RegistrarMarcaCategoriaRequest marca)
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
        public async Task<string> ActualizarMarca(RegistrarMarcaCategoriaRequest marca, int id_marca)
        {
            try
            {
                if (marca == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos de la marca");
                }
                if (id_marca == 0)
                {
                    throw new Exception("Error: Por favor ingrese el id de la marca");
                }
                if (marca.Nombre == null || marca.Nombre == "")
                {
                    throw new Exception("Error: Por favor ingrese el nombre de la marca");
                }

                var resultado = await dao.ActualizarMarca(marca, id_marca);
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

        // Método para exportar marcas a un archivo Excel
        public async Task<byte[]> ExportarListadoMarcas()
        {
            try
            {
                var marcas = await dao.ObtenerMarcas(string.Empty);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Marcas");
                    worksheet.Cell(2, 2).Value = "ID Marca";
                    worksheet.Cell(2, 3).Value = "Nombre";

                    // Aplicar estilo al encabezado
                    var headerRange = worksheet.Range("B2:C2");
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    for (int i = 0; i < marcas.Count; i++)
                    {
                        worksheet.Cell(i + 3, 2).Value = marcas[i].IdMarca;
                        worksheet.Cell(i + 3, 3).Value = marcas[i].Nombre;

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
