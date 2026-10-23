using Datos.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Dto.Clientes
{
    public class CrearClienteSolicitud
    {
        public string NombreCompleto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public Genero Sexo { get; set; }

        public decimal IngresosMensuales { get; set; }

    }
}
