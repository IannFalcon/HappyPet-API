using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int IdCliente { get; set; }

    public int IdDestino { get; set; }

    public int TotalProductos { get; set; }

    public decimal MontoTotal { get; set; }

    public string IdTransaccion { get; set; } = null!;

    public DateTime? FecVenta { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual DestinoVenta Destino { get; set; } = null!;
}
