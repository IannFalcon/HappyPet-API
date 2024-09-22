using System;
using System.Collections.Generic;

namespace AppHappyPet_API.Models
{
    public partial class Ventum
    {
        public Ventum()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int IdVenta { get; set; }
        public int IdUsuario { get; set; }
        public int TotalProductos { get; set; }
        public decimal MontoTotal { get; set; }
        public string IdTransaccion { get; set; } = null!;
        public DateTime FecVenta { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
