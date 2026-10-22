using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio.Abstracciones.Datos
{
    public interface ITrabajoUnico
    {
        Task <int> GuardarCambiosAsync();
    }
}
