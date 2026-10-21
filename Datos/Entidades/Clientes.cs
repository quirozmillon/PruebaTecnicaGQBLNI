using Datos.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Entidades
{
    public class Clientes
    {
        public int ClienteId { get; set; }
        /// <summary>
        /// Nombre completo del cliente
        /// </summary>
        public string NombreCompleto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public Genero Sexo { get; set; }

        /// <summary>
        /// Ingresos mensuales del cliente
        /// </summary>
        public decimal IngresosMensuales { get; set; }
    }
}
