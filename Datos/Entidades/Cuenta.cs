using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Entidades
{
    public class Cuenta
    {
        public int ClienteId { get; set; }
        public string NumeroCuenta { get; set; }

        public decimal SaldoInicial { get; set; }

        public decimal Saldo { get; set; }

        public DateTime FechaCreacion { get; set; }

        public virtual Clientes Cliente { get; set; }

        public virtual ICollection<Transaccion> Transacciones { get; set; }


    }


}
