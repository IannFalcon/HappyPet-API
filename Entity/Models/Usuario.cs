﻿using System;
using System.Collections.Generic;

namespace Entity.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Carritos = new HashSet<Carrito>();
            Venta = new HashSet<Venta>();
        }

        public int IdUsuario { get; set; }
        public int? IdTipoUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public int IdTipoDocumento { get; set; }
        public string NroDocumento { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string? Contrasenia { get; set; }
        public string? Activo { get; set; }
        public DateTime? FecRegistro { get; set; }

        public virtual TipoDocumento? UsuTipoDoc { get; set; }
        public virtual TipoUsuario? UsuTipoUsu { get; set; }
        public virtual ICollection<Carrito>? Carritos { get; set; }
        public virtual ICollection<Venta>? Venta { get; set; }
    }
}
