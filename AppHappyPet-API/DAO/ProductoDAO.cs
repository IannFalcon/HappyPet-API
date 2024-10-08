﻿using AppHappyPet_API.Models;
using Microsoft.Data.SqlClient;

namespace AppHappyPet_API.DAO
{
    public class ProductoDAO
    {
        private string cnx = string.Empty;

        public ProductoDAO(IConfiguration cfg)
        {
            cnx = cfg.GetConnectionString("conexion_bd");
        }

        // Obtener productos
        public List<Producto> ObtenerProductos(int? id_categoria, int? id_marca, string? nombre)
        {
            // Crear lista de productos
            List<Producto> productos = new List<Producto>();

            // Query para obtener productos
            string query = @"SELECT p.id_producto, p.nombre, p.id_categoria, c.nombre AS NombreCategoria, 
                            p.id_marca, m.nombre NombreMarca, p.Descripcion, p.precio_unitario, 
                            p.Stock, p.nombre_imagen, p.ruta_imagen, p.eliminado, p.fec_vencimiento, p.fec_registro
                            FROM Producto p
                            JOIN Categoria c ON p.id_categoria = c.id_categoria
                            JOIN Marca m ON p.id_marca = m.id_marca
	                        WHERE p.eliminado = 'No'
	                        AND (@id_categoria IS NULL OR p.id_categoria = @id_categoria)
	                        AND (@id_marca IS NULL OR p.id_marca = @id_marca)
	                        AND (@nombre IS NULL OR p.nombre LIKE '%' + @nombre + '%')";

            // Crear conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear comando para ejecutar query
                SqlCommand cmd = new SqlCommand(query, con);

                // Agregar parámetros al comando
                cmd.Parameters.AddWithValue("@id_categoria", id_categoria.HasValue ? (object)id_categoria.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@id_marca", id_marca.HasValue ? (object)id_marca.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@nombre", string.IsNullOrEmpty(nombre) ? (object)DBNull.Value : nombre);

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
                        IdMarca = dr.GetInt32(4),
                        Descripcion = dr.GetString(6),
                        PrecioUnitario = dr.GetDecimal(7),
                        Stock = dr.GetInt32(8),
                        NombreImagen = dr.IsDBNull(9) ? null : dr.GetString(9),
                        RutaImagen = dr.IsDBNull(10) ? null : dr.GetString(10),
                        Eliminado = dr.GetString(11),
                        FecVencimiento = dr.IsDBNull(12) ? null : dr.GetDateTime(12),
                        FecRegistro = dr.GetDateTime(13),
                        ProductoCategoria = new Categoria
                        {
                            IdCategoria = dr.GetInt32(2),
                            Nombre = dr.GetString(3)
                        },
                        ProductoMarca = new Marca
                        {
                            IdMarca = dr.GetInt32(4),
                            Nombre = dr.GetString(5)
                        }
                    };
                    productos.Add(producto);
                }

                // Cerrar conexión
                con.Close();

                // Retornar lista de productos
                return productos;

            }

        }

        // Obtener producto por id
        public Producto ObtenerProductoPorId(int id_producto)
        {
            // Crear objeto de producto
            Producto? producto = null;

            // Query para obtener producto por id
            string query = "SELECT * FROM Producto WHERE id_producto = @id_producto";

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
                        FecRegistro = dr.GetDateTime(11)
                    };
                }

                // Cerrar conexión
                con.Close();

                // Retornar producto
                return producto!;

            }
        }

        // Nuevo producto
        public string NuevoProducto(Producto producto)
        {
            // Asignar la fecha de registro como la fecha actual si no está ya asignada
            if (producto.FecRegistro == default(DateTime))
            {
                producto.FecRegistro = DateTime.Now;
            }

            // Query para insertar producto
            string query = @"INSERT INTO Producto (nombre, id_categoria, id_marca, descripcion, precio_unitario, 
                                                   stock, nombre_imagen, ruta_imagen, fec_vencimiento, fec_registro)
                            VALUES (@nombre, @id_categoria, @id_marca, @Descripcion, @precio_unitario, 
                                    @stock, @nombre_imagen, @ruta_imagen, @fec_vencimiento, @fec_registro)";

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
                cmd.Parameters.AddWithValue("@fec_registro", producto.FecRegistro);

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

    }

}