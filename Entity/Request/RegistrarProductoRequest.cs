﻿namespace Entity.Request
{
    public class RegistrarProductoRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public string? NombreImagen { get; set; } = string.Empty;
        public string? RutaImagen { get; set; } = string.Empty;
        public DateTime? FecVencimiento { get; set; } = null;
        public int IdProveedor { get; set; }
        public int CantidadProductos { get; set; }
    }
}