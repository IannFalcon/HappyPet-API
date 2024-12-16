using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class ClienteDireccion
{
    public int IdDireccion { get; set; }

    public int IdCliente { get; set; }

    public string NombreDireccion { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string CodigoPostal { get; set; } = null!;

    public virtual Cliente IdClienteNavigation { get; set; } = null!;
}
