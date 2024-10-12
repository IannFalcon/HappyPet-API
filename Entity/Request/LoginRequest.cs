namespace Entity.Request
{
    public class LoginRequest
    {
        public int idTipoUsuario { get; set; }
        public string correo { get; set; } = string.Empty;
        public string contrasenia { get; set; } = string.Empty;
    }
}
