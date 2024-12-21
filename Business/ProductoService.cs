using ClosedXML.Excel;
using Data;
using Entity.Reponse;
using Entity.Request;

namespace Business
{
    public class ProductoService
    {
        private readonly ProductoDAO dao;

        public ProductoService(ProductoDAO dao)
        {
            this.dao = dao;
        }

        // Método para listar productos
        public async Task<List<DatosProductoResponse>> ListarProductos(int? id_categoria, int? id_marca, string nombre)
        {
            try
            {
                var listado = await dao.ObtenerProductos(id_categoria, id_marca, nombre);
                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para obtener un producto por ID
        public async Task<DatosProductoResponse> ObtenerProductoPorId(int id_producto)
        {
            try
            {
                if (id_producto <= 0)
                {
                    throw new Exception("El ID del producto no es válido.");
                }

                var producto = await dao.ObtenerProductoPorId(id_producto);

                if (producto == null)
                {
                    throw new Exception($"El producto con ID: {id_producto} no fue encontrado.");
                }

                return producto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Metodo para obtener el ingreso de productos
        public async Task<List<IngresoProductosResponse>> ListarIngresoProductos(string? ruc_proveedor, string? nombre_proveedor, DateTime? fecha_ingreso)
        {
            try
            {
                var ingresoProductos = await dao.ObtenerIngresoProductos(ruc_proveedor, nombre_proveedor, fecha_ingreso);
                return ingresoProductos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para agregar un producto
        public async Task<CrudResponse> RegistrarProducto(RegistrarProductoRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception("No se recibieron datos para registrar el producto.");
                }

                if (request.Nombre == null || request.Nombre == "")
                {
                    throw new Exception("El nombre del producto es requerido.");
                }

                if (request.Descripcion == null || request.Descripcion == "")
                {
                    throw new Exception("La descripción del producto es requerida.");
                }

                if (request.IdCategoria <= 0)
                {
                    throw new Exception("La categoría del producto es requerida.");
                }

                if (request.IdMarca <= 0)
                {
                    throw new Exception("La marca del producto es requerida.");
                }

                if (request.PrecioUnitario <= 0)
                {
                    throw new Exception("El precio del producto es requerido.");
                }

                if (request.IdProveedor <= 0)
                {
                    throw new Exception("El proveedor del producto es requerido.");
                }

                if (request.CantidadProductos <= 0)
                {
                    throw new Exception("La cantidad de productos es requerida.");
                }

                var resultado = await dao.NuevoProducto(request);

                if (resultado.Exito == 0)
                {
                    throw new Exception(resultado.Mensaje);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para registrar el ingreso de productos
        public async Task<CrudResponse> RegistrarIngresoProducto(IngresoProductoRequest ingreso)
        {
            try
            {
                if (ingreso == null)
                {
                    throw new Exception("No se recibieron datos para registrar el ingreso de productos.");
                }

                if (ingreso.IdProveedor <= 0)
                {
                    throw new Exception("Ningun proveedor seleccionado, por favor seleccione un proveedor.");
                }

                if (ingreso.IdProducto <= 0)
                {
                    throw new Exception("Ningun producto seleccionado, por favor seleccione un producto.");
                }

                if (ingreso.Cantidad <= 0)
                {
                    throw new Exception("La cantidad de productos es requerida.");
                }

                var resultado = await dao.RegistrarIngresoProducto(ingreso);

                if (resultado.Exito == 0)
                {
                    throw new Exception(resultado.Mensaje);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para actualizar un producto
        public async Task<string> ActualizarProducto(ActualizarProductoRequest producto, int id_producto)
        {
            try
            {
                if (producto == null)
                {
                    throw new Exception("No se recibieron datos para actualizar el producto.");
                }

                if (id_producto <= 0)
                {
                    throw new Exception("El ID del producto es requerido.");
                }

                if (producto.Nombre == null || producto.Nombre == "")
                {
                    throw new Exception("El nombre del producto es requerido.");
                }

                if (producto.IdCategoria <= 0)
                {
                    throw new Exception("La categoría del producto es requerida.");
                }

                if (producto.IdMarca <= 0)
                {
                    throw new Exception("La marca del producto es requerida.");
                }

                if (producto.PrecioUnitario <= 0)
                {
                    throw new Exception("El precio del producto es requerido.");
                }

                var resultado = await dao.ActualizarProducto(producto, id_producto);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para eliminar un producto
        public async Task<string> EliminarProducto(int id_producto)
        {
            try
            {
                if (id_producto <= 0)
                {
                    throw new Exception("El ID del producto es requerido.");
                }

                var resultado = await dao.EliminarProducto(id_producto);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para exportar listado de productos a un archivo Excel
        public async Task<byte[]> ExportarListadoProductos()
        {
            try
            {
                var productos = await dao.ObtenerProductos(null, null, null);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Productos");
                    worksheet.Cell(2, 2).Value = "ID Producto";
                    worksheet.Cell(2, 3).Value = "Nombre";
                    worksheet.Cell(2, 4).Value = "Categoría";
                    worksheet.Cell(2, 5).Value = "Marca";
                    worksheet.Cell(2, 6).Value = "Descripción";
                    worksheet.Cell(2, 7).Value = "Precio Unitario";
                    worksheet.Cell(2, 8).Value = "Stock";
                    worksheet.Cell(2, 9).Value = "Fecha de Registro";
                    worksheet.Cell(2, 10).Value = "Fecha de Vencimiento";

                    // Aplicar estilo al encabezado
                    var headerRange = worksheet.Range("B2:J2");
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    for (int i = 0; i < productos.Count; i++)
                    {
                        worksheet.Cell(i + 3, 2).Value = productos[i].IdProducto;
                        worksheet.Cell(i + 3, 3).Value = productos[i].Nombre;
                        worksheet.Cell(i + 3, 4).Value = productos[i].Categoria!.Nombre;
                        worksheet.Cell(i + 3, 5).Value = productos[i].Marca!.Nombre;
                        worksheet.Cell(i + 3, 6).Value = productos[i].Descripcion;
                        worksheet.Cell(i + 3, 7).Value = productos[i].PrecioUnitario;
                        worksheet.Cell(i + 3, 8).Value = productos[i].Stock;
                        worksheet.Cell(i + 3, 9).Value = productos[i].FecRegistro;
                        worksheet.Cell(i + 3, 10).Value = productos[i].FecVencimiento;

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

        // Método para exportar el ingreso de productos a un archivo Excel
        public async Task<byte[]> ExportarListaIngresoProductos()
        {
            try
            {
                var ingresoProductos = await dao.ObtenerIngresoProductos(null, null, null);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Ingreso de Productos");
                    worksheet.Cell(2, 2).Value = "Proveedor";
                    worksheet.Cell(2, 3).Value = "Producto";
                    worksheet.Cell(2, 4).Value = "Cantidad";
                    worksheet.Cell(2, 5).Value = "Fecha de Ingreso";

                    // Aplicar estilo al encabezado
                    var headerRange = worksheet.Range("B2:E2");
                    headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                    for (int i = 0; i < ingresoProductos.Count; i++)
                    {
                        worksheet.Cell(i + 3, 2).Value = ingresoProductos[i].NombreProveedor;
                        worksheet.Cell(i + 3, 3).Value = ingresoProductos[i].NombreProducto;
                        worksheet.Cell(i + 3, 4).Value = ingresoProductos[i].Cantidad;
                        worksheet.Cell(i + 3, 5).Value = ingresoProductos[i].FecIngreso;

                        // Aplicar estilo a las celdas
                        worksheet.Cell(i + 3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 3, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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
