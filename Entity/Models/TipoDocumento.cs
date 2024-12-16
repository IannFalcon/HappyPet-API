using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class TipoDocumento
{
    public int IdTipoDoc { get; set; }

    public string NombreTipoDoc { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
