using ClosedXML.Excel;
using Data;
using Entity.Reponse;
using Entity.Request;

namespace Business
{
    public class ProveedorService
    {
        private readonly ProveedorDAO dao;

        public ProveedorService(ProveedorDAO dao)
        {
            this.dao = dao;
        }

        // Metodo para listar proveedores
        public async Task<List<DatosProveedorResponse>> ListarProveedores(string? ruc, string? nombre)
        {
            try
            {
                var listado = await dao.ObtenerProveedores(ruc, nombre);
                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Metodo para obtener un proveedor por ID
        public async Task<DatosProveedorResponse> ObtenerProveedorPorId(int id_proveedor)
        {
            try
            {
                if (id_proveedor <= 0)
                {
                    throw new Exception("El ID del proveedor no es válido.");
                }

                var proveedor = await dao.ObtenerProveedorPorId(id_proveedor);

                if (proveedor == null)
                {
                    throw new Exception($"El proveedor con ID: {id_proveedor} no fue encontrado.");
                }

                return proveedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Metodo para agregar un proveedor
        public async Task<CrudResponse> RegistrarProveedor(DatosProveedorRequest proveedor)
        {
            try
            {
                if (string.IsNullOrEmpty(proveedor.RucProveedor))
                {
                    throw new Exception("El RUC del proveedor es obligatorio.");
                }

                if (string.IsNullOrEmpty(proveedor.NombreProveedor))
                {
                    throw new Exception("El nombre del proveedor es obligatorio.");
                }

                if (string.IsNullOrEmpty(proveedor.NroTelefono))
                {
                    throw new Exception("El número de teléfono del proveedor es obligatorio.");
                }

                if (string.IsNullOrEmpty(proveedor.Correo))
                {
                    throw new Exception("El correo del proveedor es obligatorio.");
                }

                if (string.IsNullOrEmpty(proveedor.Direccion))
                {
                    throw new Exception("La dirección del proveedor es obligatoria.");
                }

                var resultado = await dao.RegistrarProveedor(proveedor);

                if (resultado.Exito == 0)
                {
                    throw new Exception(resultado.Mensaje);
                }

                if (resultado == null) 
                {
                    throw new Exception("No se pudo registrar el proveedor.");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Metodo para actualizar un proveedor
        public async Task<CrudResponse> ActualizarProveedor(DatosProveedorRequest proveedor, int id_proveedor)
        {
            try
            {
                if (id_proveedor <= 0)
                {
                    throw new Exception("El ID del proveedor no es válido.");
                }

                if (string.IsNullOrEmpty(proveedor.RucProveedor))
                {
                    throw new Exception("El RUC del proveedor es obligatorio.");
                }

                if (string.IsNullOrEmpty(proveedor.NombreProveedor))
                {
                    throw new Exception("El nombre del proveedor es obligatorio.");
                }

                if (string.IsNullOrEmpty(proveedor.NroTelefono))
                {
                    throw new Exception("El número de teléfono del proveedor es obligatorio.");
                }

                if (string.IsNullOrEmpty(proveedor.Correo))
                {
                    throw new Exception("El correo del proveedor es obligatorio.");
                }

                if (string.IsNullOrEmpty(proveedor.Direccion))
                {
                    throw new Exception("La dirección del proveedor es obligatoria.");
                }

                var resultado = await dao.ActualizarProveedor(proveedor, id_proveedor);

                if (resultado.Exito == 0)
                {
                    throw new Exception(resultado.Mensaje);
                }

                if (resultado == null)
                {
                    throw new Exception("No se pudo actualizar el proveedor.");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Metodo para eliminar un proveedor
        public async Task<string> EliminarProveedor(int id_proveedor)
        {
            try
            {
                if (id_proveedor <= 0)
                {
                    throw new Exception("El ID del proveedor no es válido.");
                }

                var resultado = await dao.EliminarProveedor(id_proveedor);

                if (resultado == null)
                {
                    throw new Exception("No se pudo eliminar el proveedor.");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Metodo para exportar listado de proveedores a un archivo Excel
        public async Task<byte[]> ExportarListadoProveedores()
        {
            try
            {
                var productos = await dao.ObtenerProveedores(null, null);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Productos");
                    worksheet.Cell(2, 2).Value = "RUC";
                    worksheet.Cell(2, 3).Value = "Nombre";
                    worksheet.Cell(2, 4).Value = "Nro. Telefono";
                    worksheet.Cell(2, 5).Value = "Correo";
                    worksheet.Cell(2, 6).Value = "Dirección";
                    worksheet.Cell(2, 7).Value = "Fec. Registro";

                    // Aplicar estilo al encabezado
                    var headerRange = worksheet.Range("B2:G2");
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    for (int i = 0; i < productos.Count; i++)
                    {
                        worksheet.Cell(i + 3, 2).Value = productos[i].RucProveedor;
                        worksheet.Cell(i + 3, 3).Value = productos[i].NombreProveedor;
                        worksheet.Cell(i + 3, 4).Value = productos[i].NroTelefono;
                        worksheet.Cell(i + 3, 5).Value = productos[i].Correo;
                        worksheet.Cell(i + 3, 6).Value = productos[i].Direccion;
                        worksheet.Cell(i + 3, 7).Value = productos[i].FecRegistro;

                        // Aplicar estilo a las celdas
                        worksheet.Cell(i + 3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }

                    // Ajustar el ancho de las columnas
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
