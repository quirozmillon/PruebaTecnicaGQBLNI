using Datos.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Dto.Clientes
{
    public class ClienteRespuesta
    {
        public int ClienteId { get; set; }
        public string NombreCompleto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public Genero Sexo { get; set; }

        public decimal IngresosMensuales { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
