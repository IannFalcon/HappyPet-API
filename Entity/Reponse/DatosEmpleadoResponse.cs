using Entity.Models;

namespace Entity.Reponse
{
    public class DatosEmpleadoResponse
    {
        public int IdEmpleado { get; set; }
        public CargoEmpleadoResponse Cargo { get; set; } = new CargoEmpleadoResponse();
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public TipoDocumentoResponse TipoDocumento { get; set; } = new TipoDocumentoResponse();
        public string NroDocumento { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FecRegistro { get; set; }
    }
}
