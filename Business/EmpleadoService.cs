using ClosedXML.Excel;
using Data;
using Entity.Models;
using Entity.Reponse;
using Entity.Request;

namespace Business
{
    public class EmpleadoService
    {
        private readonly EmpleadoDAO dao_ven;

        public EmpleadoService(EmpleadoDAO dao_ven)
        {
            this.dao_ven = dao_ven;
        }

        // Método para listar empleados
        public async Task<List<DatosEmpleadoResponse>> ListarEmpleados(string? nro_documento, string? nombre)
        {
            try
            {
                var listado = await dao_ven.ObtenerEmpleados(nro_documento, nombre);
                return listado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Ocurrió un error al obtener a los empleados: {ex.Message}");
            }
        }

        // Método para obtener a un empleado por ID
        public async Task<DatosEmpleadoResponse> ObtenerEmpleadoId(int id_empleado)
        {
            try
            {
                var vendedor = await dao_ven.ObtenerEmpleadoId(id_empleado);

                if (vendedor == null)
                {
                    throw new Exception($"El vendedor con ID: {id_empleado} no fue encontrado.");
                }

                return vendedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para registrar a un empleado
        public async Task<CrudResponse> RegistrarEmpleado(DatosEmpleadoRequest request)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos del empleado");
                }

                if (request.IdCargo <= 0)
                {
                    throw new Exception("Error: El cargo del empleado es requerido");
                }

                if (request.Nombres == null || request.Nombres == "")
                {
                    throw new Exception("Error: El nombre del empleado es requerido");
                }

                if (request.ApellidoPaterno == null || request.ApellidoPaterno == "")
                {
                    throw new Exception("Error: El apellido paterno del empleado es requerido");
                }

                if (request.ApellidoMaterno == null || request.ApellidoMaterno == "")
                {
                    throw new Exception("Error: El apellido materno del empleado es requerido");
                }

                if (request.IdTipoDoc <= 0)
                {
                    throw new Exception("Error: El tipo de documento del empleado es requerido");
                }

                if (request.NroDocumento == null || request.NroDocumento == "")
                {
                    throw new Exception("Error: El número de documento del empleado es requerido");
                }

                if (request.Telefono == null || request.Telefono == "")
                {
                    throw new Exception("Error: El teléfono del empleado es requerido");
                }

                if (request.Direccion == null || request.Direccion == "")
                {
                    throw new Exception("Error: La dirección del empleado es requerida");
                }

                if (request.Correo == null || request.Correo == "")
                {
                    throw new Exception("Error: El correo del empleado es requerido");
                }

                var resultado = await dao_ven.NuevoEmpleado(request);

                if (resultado.Exito == 0)
                {
                    throw new Exception($"Error: {resultado.Mensaje}");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para actualizar a un empleado
        public async Task<CrudResponse> ActualizarEmpleado(DatosEmpleadoRequest request, int id_empleado)
        {
            try
            {
                if (request == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos del empleado");
                }

                if (id_empleado <= 0)
                {
                    throw new Exception("Error: El id del empleado es requerido");
                }

                if (request.IdCargo <= 0)
                {
                    throw new Exception("Error: El cargo del empleado es requerido");
                }

                if (request.Nombres == null || request.Nombres == "")
                {
                    throw new Exception("Error: El nombre del empleado es requerido");
                }

                if (request.ApellidoPaterno == null || request.ApellidoPaterno == "")
                {
                    throw new Exception("Error: El apellido paterno del empleado es requerido");
                }

                if (request.ApellidoMaterno == null || request.ApellidoMaterno == "")
                {
                    throw new Exception("Error: El apellido materno del empleado es requerido");
                }

                if (request.IdTipoDoc <= 0)
                {
                    throw new Exception("Error: El tipo de documento del empleado es requerido");
                }

                if (request.NroDocumento == null || request.NroDocumento == "")
                {
                    throw new Exception("Error: El número de documento del empleado es requerido");
                }

                if (request.Telefono == null || request.Telefono == "")
                {
                    throw new Exception("Error: El teléfono del empleado es requerido");
                }

                if (request.Direccion == null || request.Direccion == "")
                {
                    throw new Exception("Error: La dirección del empleado es requerida");
                }

                if (request.Correo == null || request.Correo == "")
                {
                    throw new Exception("Error: El correo del vendedor es requerido");
                }

                var resultado = await dao_ven.ActualizarEmpleado(request, id_empleado);

                if (resultado.Exito == 0)
                {
                    throw new Exception($"Error: {resultado.Mensaje}");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para eliminar a un empleado
        public async Task<CrudResponse> EliminarEmpleado(int idUsuario)
        {
            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("Error: El id del vendedor es requerido");
                }

                var respuesta = await dao_ven.EliminarEmpleado(idUsuario);

                if (respuesta.Exito == 0)
                {
                    throw new Exception($"Error: {respuesta.Mensaje}");
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para exportar listado de vendedores a un archivo Excel
        public async Task<byte[]> ExportarListaEmpleados()
        {
            try
            {
                var vendedores = await dao_ven.ObtenerEmpleados(null, null);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Empleados");
                    worksheet.Cell(2, 2).Value = "Cargo de empleado";
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
                        worksheet.Cell(i + 3, 2).Value = vendedores[i].Cargo.NombreCargo;
                        worksheet.Cell(i + 3, 3).Value = vendedores[i].Nombres;
                        worksheet.Cell(i + 3, 4).Value = vendedores[i].ApellidoPaterno;
                        worksheet.Cell(i + 3, 5).Value = vendedores[i].ApellidoMaterno;
                        worksheet.Cell(i + 3, 6).Value = vendedores[i].TipoDocumento.NombreTipoDoc;
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
