using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Negocio.Excepciones;

namespace Presentacion.Excepcion
{
    public class ExcepcionGlobal : IExceptionHandler
    {
        private readonly ILogger<ExcepcionGlobal> _logger;

        public ExcepcionGlobal(ILogger<ExcepcionGlobal> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {

            _logger.LogError(exception, "Ocurrió un error procesando la petición: {Message}", exception.Message);


            var (statusCode, title) = exception switch
            {
                ExcepcionNegocio ex => (ex.StatusCode, "Error de Negocio"),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Recurso no encontrado"),
                _ => (StatusCodes.Status500InternalServerError, "Error interno del servidor")
            };


            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };


            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);


            return true;

        }
    }

}

