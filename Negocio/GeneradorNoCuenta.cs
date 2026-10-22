using Negocio.Abstracciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Negocio
{
    public class GeneradorNoCuenta : IGeneradorNoCuenta
    {
        public string GenerarNoCuenta()
        {
            const string Prefijo = "ACC";
            var fechaCreacion = DateTime.Now.ToString("yyyyMMdd");
            var numerosAleatorios = NumerosAleatorios();

            return $"{Prefijo}-{fechaCreacion}-{numerosAleatorios}" ;
        }

        private string NumerosAleatorios()
        {
            Random random = new Random();

            int numeroAleatorio = random.Next(0, 10000);

            string numeroFormateado = numeroAleatorio.ToString("D4");

            return numeroFormateado;
        }

    }
}
