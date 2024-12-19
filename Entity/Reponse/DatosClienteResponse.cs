using Entity.Models;

namespace Entity.Reponse
{
    public class DatosClienteResponse
    {
        public int IdCliente { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public TipoDocumentoResponse TipoDocumento { get; set; } = new TipoDocumentoResponse();
        public string NroDocumento { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FecRegistro { get; set; }
    }
}
