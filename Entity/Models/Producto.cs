﻿using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Carritos = new HashSet<Carrito>();
            DetalleVenta = new HashSet<DetalleVenta>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public string Descripcion { get; set; } = null!;
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public string? NombreImagen { get; set; }
        public string? RutaImagen { get; set; }
        public string? Eliminado { get; set; }
        public DateTime? FecVencimiento { get; set; }
        public DateTime? FecRegistro { get; set; }

        public virtual Categoria? ProductoCategoria { get; set; }
        public virtual Marca? ProductoMarca { get; set; }
        public virtual ICollection<Carrito>? Carritos { get; set; }
        public virtual ICollection<DetalleVenta>? DetalleVenta { get; set; }
    }
}
