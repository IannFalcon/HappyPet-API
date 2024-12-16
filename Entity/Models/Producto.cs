using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdCategoria { get; set; }

    public int IdMarca { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal PrecioUnitario { get; set; }

    public int Stock { get; set; }

    public string? NombreImagen { get; set; }

    public string? RutaImagen { get; set; }

    public bool? Activo { get; set; }

    public DateTime? FecVencimiento { get; set; }

    public DateTime? FecRegistro { get; set; }

    public virtual ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categoria Categoria { get; set; } = null!;

    public virtual Marca Marca { get; set; } = null!;

    public virtual ICollection<IngresoProducto> IngresoProductos { get; set; } = new List<IngresoProducto>();
}
