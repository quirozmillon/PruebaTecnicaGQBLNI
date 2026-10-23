using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Abstracciones
{
    public interface ITrabajoUnico
    {
        Task <int> GuardarCambiosAsync();
    }
}
