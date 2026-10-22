using Datos.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Datos.Abstracciones
{
    public interface ITransaccionRepositorio
    {
        void Agregar(Transaccion transaccion);
    }
}
