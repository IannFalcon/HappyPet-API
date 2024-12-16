using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class DestinoVenta
{
    public int IdDestino { get; set; }

    public string Pais { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
