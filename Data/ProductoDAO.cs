using Entity.Models;
using Microsoft.Data.SqlClient;
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
        public List<Producto> ObtenerProductos(int? id_categoria, int? id_marca, string? nombre)
        {
            // Crear lista de productos
            List<Producto> productos = new List<Producto>();

            // Query para obtener productos
            string query = @"SELECT id_producto, nombre, id_categoria, id_marca, descripcion, precio_unitario, 
                            stock, nombre_imagen, ruta_imagen, eliminado, fec_vencimiento, fec_registro
                            FROM Producto
	                        WHERE eliminado = 'No'
	                        AND (@id_categoria IS NULL OR id_categoria = @id_categoria)
	                        AND (@id_marca IS NULL OR id_marca = @id_marca)
	                        AND (@nombre IS NULL OR nombre LIKE '%' + @nombre + '%')";

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
                    while (dr.Read())
                    {
                        Producto producto = new Producto
                        {
                            IdProducto = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                            IdCategoria = dr.GetInt32(2),
                            IdMarca = dr.GetInt32(3),
                            Descripcion = dr.GetString(4),
                            PrecioUnitario = dr.GetDecimal(5),
                            Stock = dr.GetInt32(6),
                            NombreImagen = dr.IsDBNull(7) ? null : dr.GetString(7),
                            RutaImagen = dr.IsDBNull(8) ? null : dr.GetString(8),
                            Eliminado = dr.GetString(9),
                            FecVencimiento = dr.IsDBNull(10) ? null : dr.GetDateTime(10),
                            FecRegistro = dr.GetDateTime(11),
                            ProductoCategoria = dao_cate.ObtenerCategoriaPorId(dr.GetInt32(2)),
                            ProductoMarca = dao_marca.ObtenerMarcaPorId(dr.GetInt32(3))
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
        public Producto ObtenerProductoPorId(int id_producto)
        {
            // Crear objeto de producto
            Producto? producto = null;

            // Query para obtener producto por id
            string query = "SELECT * FROM Producto WHERE id_producto = @id_producto";

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
                    if (dr.Read())
                    {
                        producto = new Producto
                        {
                            IdProducto = dr.GetInt32(0),
                            Nombre = dr.GetString(1),
                            IdCategoria = dr.GetInt32(2),
                            IdMarca = dr.GetInt32(3),
                            Descripcion = dr.GetString(4),
                            PrecioUnitario = dr.GetDecimal(5),
                            Stock = dr.GetInt32(6),
                            NombreImagen = dr.IsDBNull(7) ? null : dr.GetString(7),
                            RutaImagen = dr.IsDBNull(8) ? null : dr.GetString(8),
                            Eliminado = dr.GetString(9),
                            FecVencimiento = dr.IsDBNull(10) ? null : dr.GetDateTime(10),
                            FecRegistro = dr.GetDateTime(11),
                            ProductoCategoria = dao_cate.ObtenerCategoriaPorId(dr.GetInt32(2)),
                            ProductoMarca = dao_marca.ObtenerMarcaPorId(dr.GetInt32(3))
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

        // Nuevo producto
        public string NuevoProducto(Producto producto)
        {
            // Query para insertar producto
            string query = @"INSERT INTO Producto (nombre, id_categoria, id_marca, descripcion, precio_unitario, 
                                                   stock, nombre_imagen, ruta_imagen, fec_vencimiento)
                            VALUES (@nombre, @id_categoria, @id_marca, @Descripcion, @precio_unitario, 
                                    @stock, @nombre_imagen, @ruta_imagen, @fec_vencimiento)";

            try
            {
                // Crear conexión a la base de datos
                using (SqlConnection con = new SqlConnection(cnx))
                {
                    // Crear comando para ejecutar query
                    SqlCommand cmd = new SqlCommand(query, con);

                    // Agregar parámetros al comando
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                    cmd.Parameters.AddWithValue("@id_marca", producto.IdMarca);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio_unitario", producto.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@nombre_imagen", producto.NombreImagen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ruta_imagen", producto.RutaImagen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@fec_vencimiento", producto.FecVencimiento ?? (object)DBNull.Value);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    cmd.ExecuteNonQuery();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de éxito
                    return $"El producto {producto.Nombre} fue registrado correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Actualizar producto
        public string ActualizarProducto(Producto producto)
        {
            // Query para actualizar producto
            string query = @"UPDATE Producto SET 
                            nombre = @nombre, 
                            id_categoria = @id_categoria, 
                            id_marca = @id_marca, 
                            descripcion = @descripcion, 
                            precio_unitario = @precio_unitario, stock = @stock, 
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
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);
                    cmd.Parameters.AddWithValue("@id_marca", producto.IdMarca);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@precio_unitario", producto.PrecioUnitario);
                    cmd.Parameters.AddWithValue("@stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@nombre_imagen", producto.NombreImagen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ruta_imagen", producto.RutaImagen ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@fec_vencimiento", producto.FecVencimiento ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_producto", producto.IdProducto);

                    // Abrir conexión
                    con.Open();

                    // Ejecutar query
                    cmd.ExecuteNonQuery();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de éxito
                    return $"El producto {producto.Nombre} fue actualizado correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Eliminar producto
        public string EliminarProducto(int id_producto)
        {
            // Query para eliminar producto
            string query = "UPDATE Producto SET eliminado = 'Si' WHERE id_producto = @id_producto";

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
                    cmd.ExecuteNonQuery();

                    // Cerrar conexión
                    con.Close();

                    // Retornar mensaje de éxito
                    return $"El producto con id {id_producto} fue eliminado correctamente";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}