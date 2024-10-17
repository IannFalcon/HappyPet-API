using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Request
{
    public class CambiarContraseniaRequest
    {
        public int IdUsuario { get; set; }
        public string NuevaContrasenia { get; set; } = string.Empty;
        public string ConfirmarContrasenia { get; set; } = string.Empty;
    }
}
