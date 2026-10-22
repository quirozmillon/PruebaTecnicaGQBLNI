using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;


namespace Negocio.Excepciones
{
    public class ExcepcionNegocio : Exception
    {
        public int StatusCode { get; }

        public ExcepcionNegocio(string message, int statusCode = StatusCodes.Status400BadRequest)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
