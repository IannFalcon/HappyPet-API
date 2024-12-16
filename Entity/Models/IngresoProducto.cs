using System;
using System.Collections.Generic;

namespace Entity.Models;
public partial class IngresoProducto
{
    public int IdIngreso { get; set; }

    public int IdProducto { get; set; }

    public int IdProveedor { get; set; }

    public int Cantidad { get; set; }

    public DateTime? FecIngreso { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}
