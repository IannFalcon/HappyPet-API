using ClosedXML.Excel;
using Data;
using Entity.Models;
using Entity.Reponse;

namespace Business
{
    public class VentaService
    {
        private readonly VentaDAO dao_venta;

        public VentaService(VentaDAO dao_venta)
        {
            this.dao_venta = dao_venta;
        }

        // Metodo para listar ventas
        public async Task<List<VentaResponse>> ListarVentas()
        {
            try
            {
                var ventas = await dao_venta.ObtenerVentas();
                return ventas;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Ocurrió un error al obtener las ventas. ${ex.Message}");
            }
        }

        // Metodo para realizar venta
        public async Task<string> RealizarVenta(int idUsuario, string idTransaccion)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    throw new Exception("Error: El id del usuario no es válido.");
                }

                if (idTransaccion == null)
                {
                    throw new Exception("Error: El id de la transacción no es válido.");
                }

                var resultado = await dao_venta.RealizarVenta(idUsuario, idTransaccion);

                if (resultado == "VACIO")
                {
                    throw new Exception("Error: No existen productos en el carrito");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Metodo para exportar listado de ventas a un archivo Excel
        public async Task<byte[]> ExportarListadoVentas()
        {
            try
            {
                var ventas = await dao_venta.ObtenerVentas();

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Ventas");

                    worksheet.Cell(2, 2).Value = "Id Venta";
                    worksheet.Cell(2, 3).Value = "Id Transacción";
                    worksheet.Cell(2, 4).Value = "Nombre Cliente";
                    worksheet.Cell(2, 5).Value = "Dirección de envío";
                    worksheet.Cell(2, 6).Value = "Total Productos";
                    worksheet.Cell(2, 7).Value = "Monto Total";
                    worksheet.Cell(2, 8).Value = "Fecha Venta";

                    // Aplicar estilo al encabezado
                    var headerRange = worksheet.Range("B2:H2");
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    for (int i = 0; i < ventas.Count; i++)
                    {
                        worksheet.Cell(i + 3, 2).Value = ventas[i].IdVenta;
                        worksheet.Cell(i + 3, 3).Value = ventas[i].IdTransaccion;
                        worksheet.Cell(i + 3, 4).Value = ventas[i].NombreCliente;
                        worksheet.Cell(i + 3, 5).Value = ventas[i].DireccionEnvio;
                        worksheet.Cell(i + 3, 6).Value = ventas[i].TotalProductos;
                        worksheet.Cell(i + 3, 7).Value = ventas[i].MontoTotal;
                        worksheet.Cell(i + 3, 8).Value = ventas[i].FecVenta;

                        // Aplicar estilo a las celdas
                        worksheet.Cell(i + 3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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
