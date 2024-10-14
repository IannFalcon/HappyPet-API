using Entity.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data
{
    public class CarritoDAO
    {
        private readonly string cnx;
        private readonly ProductoDAO dao_prod;

        public CarritoDAO(IConfiguration cfg, ProductoDAO dao_prod)
        {
            cnx = cfg.GetConnectionString("conexion_bd")!;
            this.dao_prod = dao_prod;
        }

        public List<Carrito> ListarProductosCarrito(int idUsuario)
        {
            List<Carrito> listado = new List<Carrito>();

            // Query para listar los productos del carrito
            string query = "SELECT * FROM Carrito WHERE id_usuario = @idUsuario";

            // Conexión a la base de datos
            using (SqlConnection con = new SqlConnection(cnx))
            {
                // Crear un comando para ejecutar la consulta
                SqlCommand cmd = new SqlCommand(query, con);

                // Agregar el parámetro idUsuario
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                // Abrir la conexión
                con.Open();

                // Ejecutar la consulta
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Carrito carrito = new Carrito
                    {
                        IdCarrito = dr.GetInt32(0),
                        IdUsuario = dr.GetInt32(1),
                        IdProducto = dr.GetInt32(2),
                        Cantidad = dr.GetInt32(3),
                        ProductosCarrito = dao_prod.ObtenerProductoPorId(dr.GetInt32(2))
                    };

                    listado.Add(carrito);
                }

                // Cerrar la conexión
                con.Close();

                return listado;
            }
        }

        public bool AccionesCarrito(int idUsuario, int idProducto, bool accion)
        {
            try
            {
                SqlDataReader dr = SqlHelper.ExecuteReader(cnx, "AccionesCarrito",
                                                    idUsuario,
                                                    idProducto,
                                                    accion);

                if (accion == false)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
