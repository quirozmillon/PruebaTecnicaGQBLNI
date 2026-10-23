using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Dto.Transacciones
{
    public class TransaccionSolicitud
    {
        public string NumeroCuenta { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
    }
}
