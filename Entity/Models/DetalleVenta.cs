using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public virtual Producto ProductoDetalle { get; set; } = null!;
        public virtual Venta IdVentaNavigation { get; set; } = null!;
    }
}
