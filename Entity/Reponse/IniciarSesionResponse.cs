namespace Entity.Reponse
{
    public class IniciarSesionResponse
    {
        public int? IdUsuario { get; set; }
        public int? IdCliente { get; set; }
        public string? Rol { get; set; }
        public string? NombreUsuario { get; set; }
        public bool? CambioContra { get; set; }
        public string? MensajeError { get; set; }
    }
}
