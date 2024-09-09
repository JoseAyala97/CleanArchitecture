using CleanArchitecture.API.Errors;
using CleanArchitecture.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace CleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware
    {
        //pipeline que pasara a la siguiente fase en caso de que no haya exception
        private readonly RequestDelegate _next;
        //logger
        private readonly ILogger<ExceptionMiddleware> _logger;
        //para conocer si se esta debug o en produccion
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //que procese el request
                await _next(context);
            }catch (Exception ex)
            {
                //si hay error que imprima el detalle del error
                _logger.LogError(ex, ex.Message);
                //mensaje que se le mostrara
                context.Response.ContentType = "application/json";
                //error por default - se modifica, debe crearse como una variable
                var statusCode = (int)HttpStatusCode.InternalServerError;
                // variable que representa el detalle de la exception
                var result = string.Empty;

                switch(ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                        //cuando ocurra un error por validacion
                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        //para que convierta en Json el objeto ValidationException - usando .Errors - va a devolver toda la lista de errores en validacion
                        var validationJson = JsonConvert.SerializeObject(validationException.Errors);
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, validationJson));
                        break;
                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        break;
                }
                //en caso de que el result siga en blanco o null
                if(string.IsNullOrEmpty(result))
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex.StackTrace));

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(result);
            }
        }
    }
}
