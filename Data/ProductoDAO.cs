using Entity.Models;
using Entity.Reponse;
using Entity.Request;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class ProductoDAO
    {
        private string cnx = string.Empty;
        private readonly CategoriaDAO dao_cate;
        private readonly MarcaDAO dao_marca;

        public ProductoDAO(IConfiguration cfg, CategoriaDAO dao_cate, MarcaDAO dao_marca)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
            this.dao_cate = dao_cate;
            this.dao_marca = dao_marca;
        }

        // Obtener productos
        public async Task<List<DatosProductoResponse>> ObtenerProductos(int? id_categoria, int? id_marca, string? nombre)
        {
            // Crear lista de productos
            List<DatosProductoResponse> productos = new List<DatosProductoResponse>();

            // Query para obtener productos
            string query = @"SELECT p.id_producto, p.nombre, p.id_categoria, c.nombre, p.id_marca, m.nombre, descripcion, 
                            precio_unitario, stock, nombre_imagen, ruta_imagen, activo, fec_vencimiento, fec_registro
                            FROM Producto p
                            INNER JOIN Categoria c ON p.id_categoria = c.id_categoria
                            INNER JOIN Marca m ON p.id_marca = m.id_marca
                            WHERE activo = 1
	                        AND (@id_categoria IS NULL OR p.id_categoria = @id_categoria)
	                        AND (@id_marca IS NULL OR p.id_marca = @id_marca)
	                        AND (@nombre IS NULL OR p.nombre LIKE '%' + @nombre + '%')";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@id_categoria", id_categoria.HasValue ? id_categoria.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_marca", id_marca.HasValue ? id_marca.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@nombre", string.IsNullOrEmpty(nombre) ? DBNull.Value : nombre);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    while (await dr.ReadAsync())
                    {
                        DatosProductoResponse producto = new DatosProductoResponse
                        {
                            IdProducto = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                            Categoria = new CategoriaResponse
                            {
                                IdCategoria = dr.GetInt32(2),
                                Nombre = dr.GetString(3)
                            },
                            Marca = new MarcaResponse
                            {
                                IdMarca = dr.GetInt32(4),
                                Nombre = dr.GetString(5)
                            },
                            Descripcion = dr.GetString(6),
                            PrecioUnitario = dr.GetDecimal(7),
                            Stock = dr.GetInt32(8),
                            NombreImagen = dr.IsDBNull(9) ? null : dr.GetString(9),
                            RutaImagen = dr.IsDBNull(10) ? null : dr.GetString(10),
                            FecVencimiento = dr.IsDBNull(11) ? null : dr.GetDateTime(11),
                            FecRegistro = dr.GetDateTime(12)
                        };
                        productos.Add(producto);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar lista de productos
                    return productos;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Obtener producto por id
        public async Task<DatosProductoResponse> ObtenerProductoPorId(int id_producto)
        {
            // Crear objeto de producto
            DatosProductoResponse? producto = null;

            // Query para obtener producto por id
            string query = @"SELECT p.id_producto, p.nombre, p.id_categoria, c.nombre, p.id_marca, m.nombre, descripcion, 
                            precio_unitario, stock, nombre_imagen, ruta_imagen, activo, fec_vencimiento, fec_registro
                            FROM Producto p
                            INNER JOIN Categoria c ON p.id_categoria = c.id_categoria
                            INNER JOIN Marca m ON p.id_marca = m.id_marca
                            WHERE p.producto = @id_producto";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetro al comando
                    cmd.Parameters.AddWithValue("@id_producto", id_producto);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Validar si se encontró un producto
                    if (await dr.ReadAsync())
                    {
                        producto = new DatosProductoResponse
                        {
                            IdProducto = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                            Categoria = new CategoriaResponse
                            {
                                IdCategoria = dr.GetInt32(2),
                                Nombre = dr.GetString(3)
                            },
                            Marca = new MarcaResponse
                            {
                                IdMarca = dr.GetInt32(4),
                                Nombre = dr.GetString(5)
                            },
                            Descripcion = dr.GetString(6),
                            PrecioUnitario = dr.GetDecimal(7),
                            Stock = dr.GetInt32(8),
                            NombreImagen = dr.IsDBNull(9) ? null : dr.GetString(9),
                            RutaImagen = dr.IsDBNull(10) ? null : dr.GetString(10),
                            FecVencimiento = dr.IsDBNull(11) ? null : dr.GetDateTime(11),
                            FecRegistro = dr.GetDateTime(12)
                        };
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar producto
                    return producto!;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Metodo para obtener el ingreso de productos
        public async Task<List<IngresoProductosResponse>> ObtenerIngresoProductos(string? nombre_proveedor, string? nombre_producto, DateTime? fecha_ingreso)
        {
            // Crear lista de ingreso de productos
            List<IngresoProductosResponse> ingresoProductos = new List<IngresoProductosResponse>();

            // Query para obtener ingreso de productos
            string query = @"SELECT pr.nombre_proveedor, p.nombre, ip.cantidad, ip.fec_ingreso
                            FROM IngresoProducto ip
                            INNER JOIN Producto p ON ip.id_producto = p.id_producto
                            INNER JOIN Proveedor pr ON ip.id_proveedor = pr.id_proveedor
                            WHERE (@nombre_proveedor IS NULL OR pr.nombre_proveedor LIKE '%' + @nombre_proveedor + '%')
                            AND (@nombre_producto IS NULL OR p.nombre LIKE '%' + @nombre_producto + '%')
                            AND (@fecha_ingreso IS NULL OR CAST(ip.fec_ingreso AS DATE) = @fecha_ingreso)";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre_proveedor", string.IsNullOrEmpty(nombre_proveedor) ? DBNull.Value : nombre_proveedor);
                    cmd.Parameters.AddWithValue("@nombre_producto", string.IsNullOrEmpty(nombre_producto) ? DBNull.Value : nombre_producto);
                    cmd.Parameters.AddWithValue("@fecha_ingreso", fecha_ingreso == DateTime.MinValue ? DBNull.Value : fecha_ingreso);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    SqlDataReader dr = cmd.ExecuteReader();

                    // Leer resultados
                    while (await dr.ReadAsync())
                    {
                        IngresoProductosResponse ingresoProducto = new IngresoProductosResponse
                        {
                            NombreProveedor = dr.GetString(0),
                            NombreProducto = dr.GetString(1),
                            Cantidad = dr.GetInt32(2),
                            FecIngreso = dr.GetDateTime(3)
                        };
                        ingresoProductos.Add(ingresoProducto);
                    }

                    // Cerrar conexión
                    con.Close();

                    // Retornar lista de ingreso de productos
                    return ingresoProductos;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Nuevo producto
        public async Task<CrudResponse> NuevoProducto(RegistrarProductoRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarProducto",
                                                        request.Nombre,
                                                        request.IdCategoria,
                                                        request.IdMarca,
                                                        request.Descripcion,
                                                        request.NombreImagen!,
                                                        request.RutaImagen!,
                                                        request.FecVencimiento!,
                                                        request.IdProveedor,
                                                        request.CantidadProductos);

                if (await dr.ReadAsync())
                {
                    var resultado = new CrudResponse
                    {
                        Exito = dr.GetInt32(0),
                        Mensaje = dr.GetString(1),
                    };

                    return resultado;
                }
                else
                {
                    throw new Exception("Error: Ocurrio un error al registrar el producto.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Registrar el ingreso de los productos
        public async Task<CrudResponse> RegistrarIngresoProducto(IngresoProductoRequest request)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "RegistrarIngresoProducto",
                                                        request.IdProducto,
                                                        request.IdProveedor,
                                                        request.Cantidad);

                if (await dr.ReadAsync())
                {
                    var resultado = new CrudResponse
                    {
                        Exito = dr.GetInt32(0),
                        Mensaje = dr.GetString(1),
                    };

                    return resultado;
                }
                else
                {
                    throw new Exception("Error: Ocurrio un error al registrar el ingreso de productos.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Actualizar producto
        public async Task<string> ActualizarProducto(ActualizarProductoRequest request, int id_producto)
        {
            // Query para actualizar producto
            string query = @"UPDATE Producto SET 
                            nombre = @nombre, 
                            id_categoria = @id_categoria, 
                            id_marca = @id_marca, 
                            descripcion = @descripcion, 
                            precio_unitario = @precio_unitario,
                            nombre_imagen = @nombre_imagen, 
                            ruta_imagen = @ruta_imagen, 
                            fec_vencimiento = @fec_vencimiento
                            WHERE id_producto = @id_producto";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre", request.Nombre);
                    cmd.Parameters.AddWithValue("@id_categoria", request.IdCategoria);
                    cmd.Parameters.AddWithValue("@id_marca", request.IdMarca);
                    cmd.Parameters.AddWithValue("@descripcion", request.Descripcion);
                    cmd.Parameters.AddWithValue("@precio_unitario", request.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@nombre_imagen", request.NombreImagen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ruta_imagen", request.RutaImagen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@fec_vencimiento", request.FecVencimiento ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_producto", id_producto);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de éxito
                    return $"El producto {request.Nombre} fue actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar producto
        public async Task<string> EliminarProducto(int id_producto)
        {
            // Query para eliminar producto
            string query = "UPDATE Producto SET activo = 0 WHERE id_producto = @id_producto";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetro al comando
                    cmd.Parameters.AddWithValue("@id_producto", id_producto);

                    // Abrir conexión
                    await con.OpenAsync();

                    // Ejecutar query
                    await cmd.ExecuteNonQueryAsync();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de éxito
                    return "El producto fue eliminado con exito.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}