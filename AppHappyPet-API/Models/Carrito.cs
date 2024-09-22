using System;
using System.Collections.Generic;

namespace AppHappyPet_API.Models
{
    public partial class Carrito
    {
        public int IdCarrito { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
