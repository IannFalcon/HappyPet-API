using ClosedXML.Excel;
using Data;
using Entity.Models;

namespace Business
{
    public class ClienteService
    {
        private readonly ClienteDAO dao_cliente;

        public ClienteService(ClienteDAO dao_cliente)
        {
            this.dao_cliente = dao_cliente;
        }

        // Método para listar clientes
        public async Task<List<Usuario>> ListarClientes(string nro_documento, string nombre)
        {
            try
            {
                var clientes = await dao_cliente.ObtenerClientes(nro_documento, nombre);
                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: Ocurrió un error al obtener los clientes: {ex.Message}");
            }
        }

        // Método para obtener un cliente por ID
        public async Task<Usuario> ObtenerClienteId(int idUsuario)
        {
            try
            {
                var cliente = await dao_cliente.ObtenerClienteId(idUsuario);

                if (cliente == null)
                {
                    throw new Exception($"El cliente con ID: {idUsuario} no fue encontrado.");
                }

                return cliente!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para registrar un cliente desde la vista de administrador
        public async Task<string> RegistrarCliente(Usuario cliente)
        {
            try
            {
                if (cliente == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos del cliente");
                }

                if (cliente.Nombre == null || cliente.Nombre == "")
                {
                    throw new Exception("Error: El nombre del cliente es requerido");
                }

                if (cliente.ApellidoPaterno == null || cliente.ApellidoPaterno == "")
                {
                    throw new Exception("Error: El apellido paterno del cliente es requerido");
                }

                if (cliente.ApellidoMaterno == null || cliente.ApellidoMaterno == "")
                {
                    throw new Exception("Error: El apellido materno del cliente es requerido");
                }

                if (cliente.IdTipoDocumento == 0)
                {
                    throw new Exception("Error: El tipo de documento del cliente es requerido");
                }

                if (cliente.NroDocumento == null || cliente.NroDocumento == "")
                {
                    throw new Exception("Error: El número de documento del cliente es requerido");
                }

                if (cliente.Telefono == null || cliente.Telefono == "")
                {
                    throw new Exception("Error: El teléfono del cliente es requerido");
                }

                if (cliente.Direccion == null || cliente.Direccion == "")
                {
                    throw new Exception("Error: La dirección del cliente es requerida");
                }

                if (cliente.Correo == null || cliente.Correo == "")
                {
                    throw new Exception("Error: El correo del cliente es requerido");
                }

                var respuesta = await dao_cliente.NuevoCliente(cliente);

                if (respuesta == "DNI_EXISTE")
                {
                    throw new Exception("Error: El número de documento ya existe.");
                }

                if (respuesta == "TEL_EXISTE")
                {
                    throw new Exception("Error: El numero de teléfono ya existe.");
                }

                if (respuesta == "CORREO_EXISTE")
                {
                    throw new Exception("Error: El correo ya existe.");
                }

                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para actualizar un cliente
        public async Task<string> ActualizarCliente(Usuario cliente)
        {
            try
            {
                if (cliente == null)
                {
                    throw new Exception("Error: Por favor ingrese los datos del cliente");
                }

                if (cliente.Nombre == null || cliente.Nombre == "")
                {
                    throw new Exception("Error: El nombre del cliente es requerido");
                }

                if (cliente.ApellidoPaterno == null || cliente.ApellidoPaterno == "")
                {
                    throw new Exception("Error: El apellido paterno del cliente es requerido");
                }

                if (cliente.ApellidoMaterno == null || cliente.ApellidoMaterno == "")
                {
                    throw new Exception("Error: El apellido materno del cliente es requerido");
                }

                if (cliente.IdTipoDocumento == 0)
                {
                    throw new Exception("Error: El tipo de documento del cliente es requerido");
                }

                if (cliente.NroDocumento == null || cliente.NroDocumento == "")
                {
                    throw new Exception("Error: El número de documento del cliente es requerido");
                }

                if (cliente.Telefono == null || cliente.Telefono == "")
                {
                    throw new Exception("Error: El teléfono del cliente es requerido");
                }

                if (cliente.Direccion == null || cliente.Direccion == "")
                {
                    throw new Exception("Error: La dirección del cliente es requerida");
                }

                if (cliente.Correo == null || cliente.Correo == "")
                {
                    throw new Exception("Error: El correo del cliente es requerido");
                }

                var respuesta = await dao_cliente.ActualizarCliente(cliente);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para eliminar un cliente
        public async Task<string> EliminarCliente(int idUsuario)
        {
            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("Error: El id del cliente es requerido");
                }

                var respuesta = await dao_cliente.EliminarCliente(idUsuario);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para exportar listado de clientes a un archivo Excel
        public async Task<byte[]> ExportarListadoClientes()
        {
            try
            {
                var clientes = await dao_cliente.ObtenerClientes("", "");

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Clientes");
                    worksheet.Cell(2, 2).Value = "ID Cliente";
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

                    for (int i = 0; i < clientes.Count; i++)
                    {
                        worksheet.Cell(i + 3, 2).Value = clientes[i].IdUsuario;
                        worksheet.Cell(i + 3, 3).Value = clientes[i].Nombre;
                        worksheet.Cell(i + 3, 4).Value = clientes[i].ApellidoPaterno;
                        worksheet.Cell(i + 3, 5).Value = clientes[i].ApellidoMaterno;
                        worksheet.Cell(i + 3, 6).Value = clientes[i].UsuTipoDoc?.Descripcion;
                        worksheet.Cell(i + 3, 7).Value = clientes[i].NroDocumento;
                        worksheet.Cell(i + 3, 8).Value = clientes[i].Telefono;
                        worksheet.Cell(i + 3, 9).Value = clientes[i].Direccion;
                        worksheet.Cell(i + 3, 10).Value = clientes[i].Correo;
                        worksheet.Cell(i + 3, 11).Value = clientes[i].FecRegistro;

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
