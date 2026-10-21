using Datos.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Entidades
{
    public class Transaccion
    {
        public int TransaccionId { get; set; }

        /// <summary>
        /// Llave foreing de cuenta
        /// </summary>
        public string NumeroCuenta { get; set; }

        public TipoTransaccion Tipo { get; set; }

        public decimal Monto { get; set; }

        public DateTime FechaProceso { get; set; }

        public string Descripcion { get; set; }
    }
}
