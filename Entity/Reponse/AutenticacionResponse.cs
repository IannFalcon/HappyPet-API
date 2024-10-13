namespace Entity.Reponse
{
    public class AutenticacionResponse
    {
        public int IdUsuario { get; set; }
        public int IdTipoUsuario { get; set; }
        public string? NombreUsuario { get; set; } = string.Empty;
    }
}
