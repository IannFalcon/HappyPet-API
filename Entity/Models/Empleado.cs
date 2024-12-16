using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public int IdUsuario { get; set; }

    public int IdCargo { get; set; }

    public string Nombres { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public int IdTipoDoc { get; set; }

    public string NroDocumento { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public DateTime? FecRegistro { get; set; }

    public virtual Cargo IdCargoNavigation { get; set; } = null!;

    public virtual TipoDocumento IdTipoDocNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
