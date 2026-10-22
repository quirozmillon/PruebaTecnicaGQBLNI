using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Dto.Cuentas
{
    public class CuentaRespuesta
    {
        public int ClienteId { get; set; }
        public string NumeroCuenta { get; set; }

        public decimal SaldoInicial { get; set; }

        public decimal Saldo { get; set; }

        public DateTime FechaCreacion { get; set; }



    }
}
