using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string RucProveedor { get; set; } = null!;

    public string NombreProveedor { get; set; } = null!;

    public string NroTelefono { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public bool? Activo { get; set; }

    public DateTime? FecRegistro { get; set; }

    public virtual ICollection<IngresoProducto> IngresoProductos { get; set; } = new List<IngresoProducto>();
}
