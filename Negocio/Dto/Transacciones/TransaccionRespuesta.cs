using Datos.Enum;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Negocio.Dto.Transacciones
{
    public class TransaccionRespuesta
    {
        public int TransaccionId { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public TipoTransaccion TipoTransaccion { get; set; }
        public decimal Monto { get; set; }
    }
}
