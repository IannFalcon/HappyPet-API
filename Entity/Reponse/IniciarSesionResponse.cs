namespace Entity.Reponse
{
    public class IniciarSesionResponse
    {
        public int? IdUsuario { get; set; }
        public int? IdCliente { get; set; }
        public string? Rol { get; set; }
        public string? NombreUsuario { get; set; }
        public int? CambioContra { get; set; }
        public string? MensajeError { get; set; }
    }
}
