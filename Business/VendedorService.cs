using ClosedXML.Excel;
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
        public async Task<List<Usuario>> ListarVendedores(string? nro_documento, string? nombre)
        {
            try
            {
                var listado = await dao_ven.ObtenerVendedores(nro_documento, nombre);
                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Ocurrió un error al obtener los vendedores: {ex.Message}");
            }
        }

        // Método para obtener un vendedor por ID
        public async Task<Usuario> ObtenerVendedorId(int idUsuario)
        {
            try
            {
                var vendedor = await dao_ven.ObtenerVendedorId(idUsuario);

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
        public async Task<string> RegistrarVendedor(Usuario vendedor)
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

                var resultado = await dao_ven.NuevoVendedor(vendedor);

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
        public async Task<string> ActualizarVendedor(Usuario vendedor)
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

                var resultado = await dao_ven.ActualizarVendedor(vendedor);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para eliminar un vendedor
        public async Task<string> EliminarVendedor(int idUsuario)
        {
            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("Error: El id del vendedor es requerido");
                }

                var respuesta = await dao_ven.EliminarVendedor(idUsuario);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para exportar listado de vendedores a un archivo Excel
        public async Task<byte[]> ExportarListadoVendedores()
        {
            try
            {
                var vendedores = await dao_ven.ObtenerVendedores(null, null);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Vendedores");
                    worksheet.Cell(2, 2).Value = "ID Vendedor";
                    worksheet.Cell(2, 3).Value = "Nombre";
                    worksheet.Cell(2, 4).Value = "Apellido Paterno";
                    worksheet.Cell(2, 5).Value = "Apellido Materno";
                    worksheet.Cell(2, 6).Value = "Tipo de Documento";
                    worksheet.Cell(2, 7).Value = "Número de Documento";
                    worksheet.Cell(2, 8).Value = "Teléfono";
                    worksheet.Cell(2, 9).Value = "Dirección";
                    worksheet.Cell(2, 10).Value = "Correo";
                    worksheet.Cell(2, 11).Value = "Fecha de Registro";

                    // Aplicar estilo al encabezado
                    var headerRange = worksheet.Range("B2:K2");
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    for (int i = 0; i < vendedores.Count; i++)
                    {
                        worksheet.Cell(i + 3, 2).Value = vendedores[i].IdUsuario;
                        worksheet.Cell(i + 3, 3).Value = vendedores[i].Nombre;
                        worksheet.Cell(i + 3, 4).Value = vendedores[i].ApellidoPaterno;
                        worksheet.Cell(i + 3, 5).Value = vendedores[i].ApellidoMaterno;
                        worksheet.Cell(i + 3, 6).Value = vendedores[i].UsuTipoDoc?.Descripcion;
                        worksheet.Cell(i + 3, 7).Value = vendedores[i].NroDocumento;
                        worksheet.Cell(i + 3, 8).Value = vendedores[i].Telefono;
                        worksheet.Cell(i + 3, 9).Value = vendedores[i].Direccion;
                        worksheet.Cell(i + 3, 10).Value = vendedores[i].Correo;
                        worksheet.Cell(i + 3, 11).Value = vendedores[i].FecRegistro;

                        // Aplicar estilo a las celdas
                        worksheet.Cell(i + 3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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
