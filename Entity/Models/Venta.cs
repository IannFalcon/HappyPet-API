using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Venta
    {
        public Venta()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public int IdVenta { get; set; }
        public int IdUsuario { get; set; }
        public int TotalProductos { get; set; }
        public decimal MontoTotal { get; set; }
        public string IdTransaccion { get; set; } = null!;
        public DateTime? FecVenta { get; set; }

        public virtual Usuario? UsuarioVenta { get; set; } = null!;
        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
    }
}
