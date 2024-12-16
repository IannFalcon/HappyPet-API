using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Carrito
{
    public int IdCarrito { get; set; }

    public int IdCliente { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
